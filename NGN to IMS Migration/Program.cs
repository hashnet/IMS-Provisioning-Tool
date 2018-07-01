using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Resources;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace NGN_to_IMS_Migration
{
        
    public enum PayType
    {
        postpaid, prepaid, none
    }

    public enum UserType
    {
        sipuser, trunkuser, trunkpilot, esluser, password, stp, none

    }

    public enum FileType
    {
        allUsrData, sipUsrData, eslUsrData, password, passwordcsv, eslMgwData, none

    }

    public enum ExecMode
    {
        prompt, ignore, fix, none
    }

    public enum EslMode
    {
        deviceOnly, subsOnly, none
    }

    public enum servRequest
    {
        prov, remove, printOut, printAsbrMgw, none
    }

    public class SipUserData
    {

        public string inSubNum, inSubUserName, inSubPassword, inSubStatus, inCallOutAuth, webRef;
        public int iCallSrcCode;
        public bool ATSPrepaid;
        public decimal dFrom, dTo;
        public ExecMode exMode;

        public SipUserData(string inSubNum, string inSubUserName, string inSubPassword, string inSubStatus, string inCallOutAuth, int iCallSrcCode, bool ATSPrepaid, ExecMode exMode, string webRef, decimal dFrom, decimal dTo)
        {
            this.inSubNum = inSubNum;
            this.inSubUserName = inSubUserName;
            this.inSubPassword = inSubPassword;
            this.inSubStatus = inSubStatus;
            this.inCallOutAuth = inCallOutAuth;
            this.iCallSrcCode = iCallSrcCode;
            this.ATSPrepaid = ATSPrepaid;
            this.exMode = exMode;
            this.webRef = webRef;
            this.dFrom = dFrom;
            this.dTo = dTo;
        }
    }

    public class EslUserData
    {
        public string sEquipmentID, sGatewayDescription, sSubscriberNumber, sPassword, sTerminationID, sCallOutAuth, sSubscriberStatus, webRef;
        public int iGatewayType, iProtocolType, iCallSrcCode;
        public bool bPrepaid;
        public decimal dFrom, dTo;
        public ExecMode exMode;
        public EslMode eslMode;

        public EslUserData(string sEquipmentID, string sGatewayDescription, string sSubscriberNumber, string sPassword, string sTerminationID, string sCallOutAuth, string sSubscriberStatus, int iGatewayType, int iProtocolType, int iCallSrcCode,  bool bPrepaid, ExecMode exMode, EslMode eslMode, string webRef, decimal dFrom, decimal dTo)
        {
            this.sEquipmentID = sEquipmentID;
            this.sGatewayDescription = sGatewayDescription;
            this.sSubscriberNumber = sSubscriberNumber;
            this.sPassword = sPassword;
            this.sTerminationID = sTerminationID;
            this.iGatewayType = iGatewayType;
            this.iProtocolType = iProtocolType;
            this.iCallSrcCode = iCallSrcCode;
            this.sCallOutAuth = sCallOutAuth;
            this.sSubscriberStatus = sSubscriberStatus;
            this.bPrepaid = bPrepaid;
            this.exMode = exMode;
            this.eslMode = eslMode;
            this.webRef = webRef;
            this.dFrom = dFrom;
            this.dTo = dTo;
        }
    }


    public static class parameters
    {

        public static string trunkSCSCF()
        {

            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("trunkSCSCF", true);
            }
        }

        public static string agcf()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("agcf", true);
            }
        }


        public static string UrlPGW()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("urlpgw", true);
            }        
        } 

        public static string urlSPG()
        {
            //return Properties.Resources.ResourceManager.GetString("urlSPG");

            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("urlSPG", true);
            }
        }

        public static string stpIP()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("stpIP", true);
            }
        }

        public static string stpIndex()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("stpIndex", true);
            }
        }

        public static string userPGW()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("pgwUser", true);
            }
        }

        public static string passPGW()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return Program.Base64Encode(ResSet.GetString("pgwPass", true), false);
            }
        }

        public static string userSTP()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("stpUser", true);
            }
        }

        public static string passSTP()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return Program.Base64Encode (ResSet.GetString("stpPass", true), false);
            }
        }

        public static string userSPG()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("spgUser", true);
            }
        }

        public static string passSPG()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return Program.Base64Encode (ResSet.GetString("spgPass", true), false);
            }
        }

        public static string defaultRealm()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("defaultRealm", true);
            }
        }

        public static string imsEnsZone()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("imsEnsZone", true);
            }
        }

        public static string userIFC()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("userIFC", true);
            }
        }

        public static string trunkIFC()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("trunkIFC", true);
            }
        }

        public static string imsSrvKey()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("imsSrvKey", true);
            }
        }

        public static string imsScfAddr()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("imsScfAddr", true);
            }
        }

        public static string ManualXmlInputDestination()
        {
            using (ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources"))
            {

                return ResSet.GetString("ManualXmlInputDestination", true);
            }
        }

    }

    static class Program
    {
        static Random rand = new Random();

        const string isdnNormalizeRegExp = @"(tel:|sip:)?(\+|00)?(968)?((?(4)((9|2|7)\d{7})|((9|2|7)\d{7})))";
        const string isdnCC = "968";

        public static string S_LOG_PATH = @"c:\hss\";

        public static string S_LOG_FILE = S_LOG_PATH + "ngn2ims.log";
        public static string S_RESULT_FILE = S_LOG_FILE + ".result";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormMain());
            Application.Run(new FormManual(new Form()));
            //Application.Run(new FormLogin());
        }

        public static string Base64Encode(string inputString, bool encode = true)
        {

            switch (encode)
            {
                case true: //encode
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(inputString);
                    return Convert.ToBase64String(plainTextBytes);

                case false: //decode
                    var base64EncodedBytes = Convert.FromBase64String(inputString);
                    return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                default:
                    return null;
            }


        }
        
        public static void WriteLog(string subNumber, string status, bool final)
        {

            try
            {
                switch (final)
                {
                    case true:
                        using (StreamWriter swF = File.AppendText(S_RESULT_FILE))
                        {
                            swF.WriteLine(string.Format("{0},{1},{2}", subNumber, DateTime.Now.ToString("HH:mm:ss.fff"), status));
                        }

                        using (StreamWriter sw = File.AppendText(S_LOG_FILE))
                        {
                            sw.WriteLine(string.Format("{0},{1},{2}", subNumber, DateTime.Now.ToString("HH:mm:ss.fff"), status));
                        }
                        break;

                    default:
                        using (StreamWriter sw = File.AppendText(S_LOG_FILE))
                        {
                            sw.WriteLine(string.Format("{0},{1},{2}", subNumber, DateTime.Now.ToString("HH:mm:ss.fff"), status));
                        }
                        break;
                }
            }
            catch (Exception)
            {


            }

        }
        
        public static bool OpenFileDialog(string Title, string Filter, out string [] files)
        {
            files = new string[] { };

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.Title = Title;
            openFile.Filter = Filter;            

            if (openFile.ShowDialog() != DialogResult.OK)
                return false;
            
            files = openFile.FileNames;

            foreach (var item in files)
            {
                if (!openFile.CheckFileExists)
                    throw new Exception("Selected file is not exist. please check file name.");

                if (!openFile.CheckPathExists)
                    throw new Exception("Invalid file path.");
            }         

            return true;
        }

        public static bool SaveFileDialog(string sTitle, string sFilter, string sDefaultFileName, out string sOutFile, out int iFormatIndex)
        {
            iFormatIndex = -1;
            sOutFile = string.Empty;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = sTitle;
            saveFileDialog.Filter = sFilter;
            saveFileDialog.FileName = sDefaultFileName;
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                throw new Exception("No file was selected.");

            sOutFile = saveFileDialog.FileName;            

            if (saveFileDialog.CheckFileExists && !saveFileDialog.OverwritePrompt)
                throw new Exception("File already exists in the path, please choose another file.");

            if (!saveFileDialog.CheckPathExists)
                throw new Exception("Invalid file path.");

            iFormatIndex = saveFileDialog.FilterIndex;

            return true;
        }

        /// <summary>
        /// This function checks if the input ISDN number is valid or not.
        /// If the input ISDN is not valid, it will return NULL, otherwise it
        /// will normalize the ISDN to the desired format based on input TON      
        /// </summary>
        /// 
        /// <param name="isdn"></param>
        /// <param name="TON"> 
        /// TON = 1 International    (96895103209)
        /// 
        /// TON = 2 Unknown          (0096895103209)
        /// 
        /// TON = 4 National         (95103209)
        /// 
        /// TON = 10 INTL.(+)        (+96895103209)
        /// 
        /// TON = 11 IMPI (+)        (+96895103209@ims.ooredoo.om)
        /// 
        /// TON = 12 IMPU SIP        (sip:+96895103209@ims.ooredoo.om)
        /// 
        /// TON = 13 IMPU TEL        (tel:+96895103209)
        /// 
        /// TON = 21 Intl. or Def.   (used when a value should be returned either as it is or normalized).
        /// 
        /// TON = anything else      (invalid, returns NULL)
        /// 
        /// </param>
        /// <returns></returns>
        public static string NormalizeMSISDN(string isdn, int TON) // Regular Expression Module
        {
            //// This function checks if the input ISDN number is valid or not.
            //// If the input ISDN is not valid, it will return NULL, otherwise it
            //// will normalize the ISDN to the desired format based on input TON

            //// The table below describes possible TON values.
            //// ----------------------------------------------
            //// -    TON                  OUTPUT EXAMPLE     -
            //// ----------------------------------------------
            //// TON = 1 International    (96895103209)
            //// TON = 2 Unknown          (0096895103209)
            //// TON = 4 National         (95103209)
            //// TON = 10 INTL.(+)        (+96895103209)
            //// TON = 11 IMPI (+)        (+96895103209@ims.ooredoo.om)
            //// TON = 12 IMPU SIP        (sip:+96895103209@ims.ooredoo.om)
            //// TON = 13 IMPU TEL        (tel:+96895103209)
            //// TON = 21 Intl. or Def.   (used when a value should be returned either as it is or normalized).
            //// TON = anything else      (invalid, returns NULL)

            if (TON == 21)
            {
                string tmpISDN = isdn;
                isdn = NormalizeMSISDN(isdn, 1);

                return string.IsNullOrEmpty(isdn) ? tmpISDN : isdn;
            }

            isdn = isdn.Trim();

            ////@"(tel:|sip:)?(\+|00)?(968[792]\d{7}|[792]\d{7})"; old v1            
            //string regExpPattern = @"(tel:|sip:)?(\+|00)?(" + appConfig.isdnCC + @"[" + isdnNDC + @"]\d{" + (isdnMin - 1).ToString() + @"}|[" + isdnNDC + @"]\d{" + (isdnMin - 1).ToString() + @"})";
            string regExpPattern = isdnNormalizeRegExp;

            Regex regExp = new Regex(regExpPattern);

            Match regExpMatch = regExp.Match(isdn);

            isdn = regExpMatch.Groups[4].ToString();

            if (string.IsNullOrEmpty(isdn))
                return string.Empty;

            // normalize the ISDN to BNT-1 format
            isdn = isdnCC + isdn;

            switch (TON)
            {
                case 1:  //International
                    return isdn;

                case 2:  //Unknwon
                    return "00" + isdn;

                case 4:  //National
                    return isdn.Substring(isdnCC.Length);

                case 10: //International (+)
                    return "+" + isdn;

                case 11: // IMPI
                    return "+" + isdn + "@" + parameters.defaultRealm();

                case 12: // IMPU - SIPURI
                    return "sip:+" + isdn + "@" + parameters.defaultRealm();

                case 13: // IMPU - TELURI
                    return "tel:+" + isdn;



                default:
                    return string.Empty;
            }

        }

        public static string NormalizeIMSI(string imsi)
        {
            if (string.IsNullOrEmpty(imsi))
                return null;

            imsi = imsi.Trim();

            if (imsi.Length != 15)
                return null;

            if (!imsi.StartsWith("42203"))
                return null;

            return imsi;
        }

        public static void ProcessOpNpdbStatus(string isdn, out Dictionary <string, string> KeyNpdb, bool numberRange)
        {

            string[] ArrNpdbIndex = parameters.stpIndex().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, string> KeyNpdbIndex = new Dictionary<string, string> { };

            foreach (string item in ArrNpdbIndex)
            {
                string[] ArrIndex = item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                KeyNpdbIndex.Add(ArrIndex.GetValue(0).ToString().Trim(), ArrIndex.GetValue(1).ToString().Trim());
            }

            KeyNpdb = new Dictionary<string, string> { };

            isdn = NormalizeMSISDN(isdn, 4);

            if (!isdn.StartsWith("2"))
                isdn = NormalizeMSISDN(isdn, 1);


            string sCommand = "LST SERUATTR: MODE=NUM,USRNUM=" + "\"" + isdn + "\"" + ";";

            string[] sCommands = new string[3];

            sCommands.SetValue(sCommand, 0);

            if (numberRange)
            {
                sCommand = "LST MNPFRGNRNG: MODE=number,NUMBER=" + "\"" + isdn + "\"" + ";";
                sCommands.SetValue(sCommand, 1);

                //sCommand = "LST MNPLOCNRNG: MODE=number,NUMBER=" + "\"" + isdn + "\"" + ";";
                //sCommands.SetValue(sCommand, 2);               
            }


            foreach (var item in sCommands)
            {
                if (string.IsNullOrEmpty(item))
                    continue;

                sCommand = item.Substring(3, 10);
                sCommand = sCommand.Replace(':', ' ');
                sCommand = sCommand.Trim();

                //KeyNpdb.Add(sCommand, isdn);

                string[] npdbIPs = parameters.stpIP().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var sIP in npdbIPs)
                {                    

                    string sResult = string.Empty;

                    string sNode = sIP.Substring(0, sIP.IndexOf("="));
                    string sNodeIP = sIP.Substring(sNode.Length + 1);

                    string sKey = sCommand + "-" + sNode;

                    KeyNpdb.Add(sKey, null);

                    tcpconnect tcp = new tcpconnect();

                    tcp.NpdbCommand(item, sNodeIP, out sResult);

                    tcp.DisconnectHost(sNodeIP);

                    sResult = sResult.ToUpper();

                    string[] npdbOut = sResult.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);                    

                    if (sCommand == "SERUATTR")
                    {
                        npdbOut = Array.FindAll(npdbOut, s => s.Contains("ROUTING NUMBER INDEX"));

                        if (npdbOut.Length == 0)
                        {
                            KeyNpdb[sKey] = "no data found.";
                            continue;
                        }

                        npdbOut = npdbOut[0].Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);

                        int iIndex = Array.IndexOf(npdbOut, "ROUTING NUMBER INDEX");

                        if (iIndex < 0)
                        {
                            KeyNpdb[sKey] = "printout error, please check original output. counld not find string (Routing Number Index)";
                            continue;
                        }

                        npdbOut = sResult.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                        npdbOut = Array.FindAll(npdbOut, s => s.Contains(" " + isdn + " "));

                        if (npdbOut.Length == 0)
                        {
                            KeyNpdb[sKey] = "printout error, please check original output. counld not find string " + isdn;
                            continue;
                        }

                        npdbOut = npdbOut[0].Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);

                        sResult = npdbOut.GetValue(iIndex).ToString().Trim();

                        KeyNpdb[sKey] = KeyNpdbIndex[sResult].ToLower();

                        continue;
                    }
                    else
                    {
                        KeyNpdb[sKey] = string.Join(Environment.NewLine, npdbOut.Skip(4));                        
                    }                    
                }
            }
        }

        public static void ProcessOpNpdbChange(Dictionary<string, string> InputKeys, servRequest eRequest, out Dictionary<string, string> KeyNpdb)
        {
            InputKeys.TryGetValue("SUBSCRIBER NUMBER", out string isdn);
            InputKeys.TryGetValue("DESCRIPTION", out string desc);            

            if (string.IsNullOrEmpty(isdn))
            {
                MessageBox.Show("'SUBSCRIBER NUMBER' can't be empty.", "missing parameter");
                KeyNpdb = null;
                return;
            }

            isdn = Program.NormalizeMSISDN(isdn, 4);

            if (!isdn.StartsWith("2"))
                isdn = NormalizeMSISDN(isdn, 1);           

            string sCommand = string.Empty;

            switch (eRequest)
            {
                case servRequest.prov:
                    if (!int.TryParse(InputKeys["INDEX NUMBER"], out int index))
                    {
                        MessageBox.Show("check 'INDEX NUMBER'", "invalid parameter value");
                        KeyNpdb = null;
                        return;
                    }

                    if (string.IsNullOrEmpty(desc)) desc = isdn;

                    sCommand = "ADD SERUATTR: USRNUM=" + "\"" + isdn + "\"" + ", DESC=" + "\"" + desc + "\"" + " ,SERVATTR=OMNP-1, NUMTYPE=TYPE1, RNIDXTYPE=RN, RNIDX2=" + index + ";";
                    break;
                case servRequest.remove:
                    sCommand = "RMV SERUATTR: MODE = num, USRNUM=" + "\"" + isdn + "\"" + ";";
                    break;

                default:
                    break;
            }

            KeyNpdb = new Dictionary<string, string> { };

            KeyNpdb.Add("isdn", isdn);

            string[] npdbIPs = parameters.stpIP().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in npdbIPs)
            {
                string sResult = string.Empty;

                string sNode = item.Substring(0, item.IndexOf("="));
                string sNodeIP = item.Substring(sNode.Length + 1);

                KeyNpdb.Add(sNode, null);

                tcpconnect tcp = new tcpconnect();

                tcp.NpdbCommand(sCommand, sNodeIP, out sResult);

                tcp.DisconnectHost(sNodeIP);

                string regExpPattern = string.Concat("RETCODE", ".*?=.*?", "(.*)");

                Regex regExp = new Regex(regExpPattern, RegexOptions.IgnoreCase);

                Match regExpMatch = regExp.Match(sResult);

                if (!regExpMatch.Success)
                {
                    KeyNpdb[sNode] = "result unknown, check with network display.";

                    continue;
                }

                KeyNpdb[sNode] = regExpMatch.Groups[1].ToString();
            }            
        }
        
        public static string GetRandomString(int length)
        {
            string chars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890abcdefghijklmnopqrstuvwxyz`~!@#$%^*()-_=+|[{}];:'.>/?";

            string newRandom = new string(Enumerable.Repeat(chars, length)
                                .Select(s => s[rand.Next(s.Length)]).ToArray());

            return newRandom; 
        }

        public static void UpdateProgressBar (ProgressBar progrssBar, int MaxVal, int Val)
        {
            try
            {
                if (MaxVal > 0)
                    progrssBar.Invoke(new Action(() => progrssBar.Maximum = MaxVal));

                if (progrssBar.Maximum == 0)
                    return;
                
                progrssBar.Invoke(new Action(() => progrssBar.Value = Val));                
            }
            catch (Exception)
            {
                
            }
        }
    }
}
