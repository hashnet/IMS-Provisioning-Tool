using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NGN_to_IMS_Migration
{
    class tcpconnect
    {
           
        const int portnumber = 8000;

        public static Socket s = new Socket(AddressFamily.InterNetwork,
           SocketType.Stream,
           ProtocolType.Tcp);              

        // Synchronous connect using IPAddress to resolve the  
        // host name. 
        private void ConnectToHost(string serverIp)
        {
            IPAddress[] IPs = Dns.GetHostAddresses(serverIp);            
          
            s.Connect(IPs[0], portnumber);

        }

        public void DisconnectHost (string serverIp)
        {
            s.Close();
            s.Dispose();
        }

        private bool IsConnected (string serverIp)
        {
            int i = 0;

            while (!s.Connected)
            {

                if (i >= 5)
                    return false;

                try
                {
                    s.Close();
                    s.Dispose();

                    s = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream,
                            ProtocolType.Tcp);

                    ConnectToHost(serverIp);

                    return true;

                }
                catch (Exception)
                {

                }

            }

            return true;
        }
           

        private bool Login(string serverIp)
        {

            //LGI: OP="user", PWD="pass" ;
            string sCommand = "LGI: OP=" + "\"" + parameters.userSTP() + "\"" + ", PWD=" + "\"" + parameters.passSTP() + "\"" + ";";

            if (SendCommand(sCommand, serverIp, out sCommand))
                return true;

            return false;
        }

        private bool Logout(string serverIp)
        {
            string sCommand = "LGO:;";

            if (SendCommand(sCommand, serverIp, out sCommand))
                return true;

            return false;
        }

        public bool NpdbCommand (string command, string serverIp, out string sResult)
        {
            sResult = string.Empty;

            if (!Login(serverIp))
                return false;

            if (!SendCommand(command, serverIp, out sResult))
            {
                Logout(serverIp);

                return false;
            }
            
            Logout(serverIp);

            return true;
        }

        private bool SendCommand (string command, string serverIp, out string sResult)
        {

            sResult = string.Empty;

            if (!IsConnected(serverIp))
                return false;

            byte[] cmdBytes = Encoding.ASCII.GetBytes(command);
            s.Send(cmdBytes);
            
            byte[] buffer = new byte[2048];

            //int iResult = 0;
            int iResult = buffer.Length;                      

            do
            {               

                IAsyncResult asyncResult = s.BeginReceive(buffer, 0, iResult, SocketFlags.Partial , null, null);

                asyncResult.AsyncWaitHandle.WaitOne();

                iResult = s.EndReceive(asyncResult);

                sResult += Encoding.Default.GetString(buffer, 0, iResult);

            } while (iResult >= buffer.Length || !sResult.Contains ("---    END"));

            if (sResult.Contains("RETCODE = 1520"))
                return false;

            return true;
        }
    }
}