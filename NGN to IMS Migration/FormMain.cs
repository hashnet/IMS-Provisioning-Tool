using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace NGN_to_IMS_Migration
{


    public partial class FormMain : Form
    {

        //INPUT_FIELD userType = INPUT_FIELD.INDX_CALL_OUT_AUTHORITY;

        //int iPROG_BAR_MAX = 0;        

        const int INDX_SIP_SUBSCRIBER_TYPE = 7;
        const int INDX_SIP_SUBSCRIBER_NUMBER_ALL_DATA = 1;
        const int INDX_SIP_SUBSCRIBER_NUMBER_SIP_DATA = 3;
        const int INDX_SIP_DEVICE_ID = 10;
        const int INDX_SIP_SUBSCRIBER_STATUS = 6;
        const int INDX_SIP_CALL_SOURCE_CODE = 22;
        const int INDX_SIP_CALL_OUT_AUTHORITY = 27;
        const int INDX_SIP_FILE_LINE_START = 6;

        const int INDX_ESL_CALL_SOURCE_CODE = 23;
        const int INDX_ESL_CALL_OUT_AUTHORITY = 29;
        const int INDX_ESL_TERMINATION_ID = 12;
        const int INDX_ESL_MEDIAGATEWAY_ID = 5;
        const int INDX_ESL_GATEWAY_TYPE = 13;
        const int INDX_ESL_PROTOCOL_TYPE = 14;
        const int INDX_ESL_GATEWAY_DESC = 1;

        const string S_SIP_SUBSCRIBER_TYPE = "SIP USER";
        const string S_ESL_SUBSCRIBER_TYPE = "ESL";

        const string S_SIP_SUBSCRIBER_TABLE = "SIP USER";
        const string S_ESL_SUBSCRIBER_TABLE = "ESL";

        const string S_DEFAULT_TABLE = "SIP USER";

        const int I_TIMER_INTERVAL = 30000;

        string S_CURRENT_TABLE = S_DEFAULT_TABLE;

        bool bPauseProcess = false;

        public string webRef = string.Empty;

        DataSet dS = new DataSet();

        static int iProgBarProgress = 0;
        static int iCurrentProgress = 1;


        public FormMain()
        {
            InitializeComponent();


            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dS.Clear();

            dS.Tables.Clear();

            dS.Reset();

            //Create main NGN SIP datatable for input collection.
            dS.Tables.Add("SIP USER");

            dS.Tables["SIP USER"].Columns.Add("SUBSCRIBER_ID");
            dS.Tables["SIP USER"].Columns.Add("SUBSCRIBER_TYPE");
            dS.Tables["SIP USER"].Columns.Add("SUBSCRIBER_STATUS");
            dS.Tables["SIP USER"].Columns.Add("USERNAME");
            dS.Tables["SIP USER"].Columns.Add("PASSWORD");
            dS.Tables["SIP USER"].Columns.Add("CALL_SOURCE_CODE");
            dS.Tables["SIP USER"].Columns.Add("CALL_OUT_AUTH");
            dS.Tables["SIP USER"].Columns.Add("COMMENT").DefaultValue = "NONE";
            dS.Tables["SIP USER"].PrimaryKey = new DataColumn[] { dS.Tables["SIP USER"].Columns["SUBSCRIBER_ID"] };


            //Create main NGN ESL datatable for input collection.
            dS.Tables.Add("ESL");

            dS.Tables["ESL"].Columns.Add("SUBSCRIBER_ID");      //
            dS.Tables["ESL"].Columns.Add("PROTOCOL_TYPE");      //
            dS.Tables["ESL"].Columns.Add("GATEWAY_ID");
            dS.Tables["ESL"].Columns.Add("GATEWAY_TYPE");       //
            dS.Tables["ESL"].Columns.Add("GATEWAY_DESC");       //            
            dS.Tables["ESL"].Columns.Add("PASSWORD");
            dS.Tables["ESL"].Columns.Add("TID");                //
            dS.Tables["ESL"].Columns.Add("SUBSCRIBER_TYPE");    //
            dS.Tables["ESL"].Columns.Add("SUBSCRIBER_STATUS");
            dS.Tables["ESL"].Columns.Add("CALL_SOURCE_CODE");
            dS.Tables["ESL"].Columns.Add("CALL_OUT_AUTH");
            dS.Tables["ESL"].PrimaryKey = new DataColumn[] { dS.Tables["ESL"].Columns["SUBSCRIBER_ID"] };

            //// Create Individual datatable for individual process
            //dS.Tables.Add("Individual");

            //dS.Tables["Individual"].Columns.Add("SUBSCRIBER_ID");
            //dS.Tables["Individual"].Columns.Add("PROTOCOL_TYPE");
            //dS.Tables["Individual"].Columns.Add("GATEWAY_TYPE");
            //dS.Tables["Individual"].Columns.Add("MID"); //USERNAME
            //dS.Tables["Individual"].Columns.Add("USERNAME");
            //dS.Tables["Individual"].Columns.Add("PASSWORD");
            //dS.Tables["Individual"].Columns.Add("TID");
            //dS.Tables["Individual"].Columns.Add("SUBSCRIBER_TYPE");
            //dS.Tables["Individual"].Columns.Add("SUBSCRIBER_STATUS");
            //dS.Tables["Individual"].Columns.Add("CALL_SOURCE_CODE");
            //dS.Tables["Individual"].Columns.Add("CALL_OUT_AUTH");

            // Create View datatable used when user filter specific subscribers number SIP User
            dS.Tables.Add("SearchViewSIP");

            dS.Tables["SearchViewSIP"].Columns.Add("SUBSCRIBER_ID");
            dS.Tables["SearchViewSIP"].Columns.Add("SUBSCRIBER_TYPE");
            dS.Tables["SearchViewSIP"].Columns.Add("SUBSCRIBER_STATUS");
            dS.Tables["SearchViewSIP"].Columns.Add("USERNAME");
            dS.Tables["SearchViewSIP"].Columns.Add("PASSWORD");
            dS.Tables["SearchViewSIP"].Columns.Add("CALL_SOURCE_CODE");
            dS.Tables["SearchViewSIP"].Columns.Add("CALL_OUT_AUTH");
            dS.Tables["SearchViewSIP"].Columns.Add("COMMENT");

            // Create View datatable used when user filter specific subscribers number ESL User
            dS.Tables.Add("SearchViewESL");

            dS.Tables["SearchViewESL"].Columns.Add("SUBSCRIBER_ID");
            dS.Tables["SearchViewESL"].Columns.Add("PROTOCOL_TYPE");
            dS.Tables["SearchViewESL"].Columns.Add("GATEWAY_TYPE");
            dS.Tables["SearchViewESL"].Columns.Add("MID"); //USERNAME
            dS.Tables["SearchViewESL"].Columns.Add("USERNAME");
            dS.Tables["SearchViewESL"].Columns.Add("PASSWORD");
            dS.Tables["SearchViewESL"].Columns.Add("TID");
            dS.Tables["SearchViewESL"].Columns.Add("SUBSCRIBER_TYPE");
            dS.Tables["SearchViewESL"].Columns.Add("SUBSCRIBER_STATUS");
            dS.Tables["SearchViewESL"].Columns.Add("CALL_SOURCE_CODE");
            dS.Tables["SearchViewESL"].Columns.Add("CALL_OUT_AUTH");

            // Set the current DataGridView to Main datatable.

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dS;
            dataGridView1.DataMember = dS.Tables["SIP USER"].TableName;

            cb_fileType.Items.Clear();
            cb_fileType.Items.Add("SIP USER");
            cb_fileType.Items.Add("ESL");

            cb_inputFile.Items.Clear();

            btn_open.Enabled = false;
            btn_process.Enabled = false;

            importViewToolStripMenuItem.Enabled = false;
        }

        private void processInputFile(string[] files, FileType fileType, UserType userType)
        {
            int iNumOfUsers = 0;

            try
            {
                foreach (var item in files)
                {
                    switch (fileType)
                    {
                        case FileType.allUsrData:
                            processFileAllUserData(item, userType, ref dS, ref iNumOfUsers);
                            break;

                        case FileType.sipUsrData:
                            processFileSIPUserData(item, ref dS, ref iNumOfUsers);
                            break;

                        case FileType.eslUsrData:
                            processFileESLUserData(item, ref dS, ref iNumOfUsers);
                            break;

                        case FileType.eslMgwData:
                            processFileMGWFile(item, ref dS, ref iNumOfUsers);
                            break;

                        case FileType.password:
                            processFilePassword(item, ref dS, ref iNumOfUsers);
                            break;

                        case FileType.passwordcsv:
                            processFilePaswordCSV(item, ref dS, ref iNumOfUsers);
                            break;

                        case FileType.none:
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading input Data, Please make sure of file selection and filter mode.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dS;

            switch (userType)
            {
                case UserType.sipuser:
                    S_CURRENT_TABLE = S_SIP_SUBSCRIBER_TABLE;
                    break;

                case UserType.esluser:
                    S_CURRENT_TABLE = S_ESL_SUBSCRIBER_TABLE;
                    break;
                default:
                    break;
            }

            int iNumOfRows = dS.Tables[S_CURRENT_TABLE].Rows.Count;

            Program.UpdateProgressBar(progBarMainForm, iNumOfRows, 0);

            txt_status.Invoke(new Action(() => txt_status.Text = string.Format("{0} Total Customers", iNumOfRows)));
            txt_subNum.Invoke(new Action(() => txt_subNum.Enabled = true));
            btn_open.Invoke(new Action(() => btn_open.Enabled = true));            
            btn_process.Invoke(new Action(() => btn_process.Text = string.Format("Process {0} Entries", iNumOfRows)));

            cbExecutionMode.Invoke(new Action(() => cbExecutionMode.Enabled = true));           

            numFrom.Invoke(new Action(() => numFrom.Maximum = iNumOfRows));
            numFrom.Invoke(new Action(() => numFrom.Value = iNumOfRows == 0 ? 0: 1));
            numFrom.Invoke(new Action(() => numFrom.Enabled = true));

            numTo.Invoke(new Action(() => numTo.Maximum = iNumOfRows));
            numTo.Invoke(new Action(() => numTo.Value = iNumOfRows));                        
            numTo.Invoke(new Action(() => numTo.Enabled = true));

            dataGridView1.Invoke(new Action(() => ChangeCurrentTableOnGrid(S_CURRENT_TABLE, true)));

            if (userType == UserType.esluser && fileType == FileType.allUsrData)
            {
                IEnumerable<DataRow> myQuery = from dtr1 in dS.Tables[S_ESL_SUBSCRIBER_TABLE].AsEnumerable()
                                               orderby dtr1.Field<string>("TID")                                               
                                               select dtr1;

                DataTable myNewTable = myQuery.CopyToDataTable<DataRow>();

                myQuery = from dtr1 in myNewTable.AsEnumerable()
                          orderby dtr1.Field<string>("GATEWAY_ID")
                          select dtr1;

                myNewTable = myQuery.CopyToDataTable<DataRow>();
               
                myNewTable.TableName = S_ESL_SUBSCRIBER_TABLE;
                dS.Tables.Remove(S_ESL_SUBSCRIBER_TABLE);

                dS.Tables.Add(myNewTable);

                dS.Tables[S_ESL_SUBSCRIBER_TABLE].PrimaryKey = new DataColumn[] { dS.Tables[S_ESL_SUBSCRIBER_TABLE].Columns["SUBSCRIBER_ID"] };                
                
            }

            RefreshUpdateGridView();

            if (userType == UserType.esluser)
                cbEslMode.Invoke (new Action(() => cbEslMode.Enabled = true));
        }

        private void processFileAllUserData(string file, UserType userType, ref DataSet dS, ref int iNumOfUsers)
        {
            StreamReader sRead = new StreamReader(file);

            for (int i = 0; i < INDX_SIP_FILE_LINE_START - 1; i++)
                sRead.ReadLine();

            StringBuilder sBuild = new StringBuilder(sRead.ReadToEnd());

            sRead.Close();

            string[] lines = sBuild.ToString().Split(new string[] { "\n\r", "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int iUserCount = iNumOfUsers;

            foreach (string line in lines)
            {                
                string[] param = line.Split(new string[] { "," }, StringSplitOptions.None);

                string inSubNum = param.ElementAt(INDX_SIP_SUBSCRIBER_NUMBER_ALL_DATA);
                string inDevice = param.ElementAt(INDX_SIP_DEVICE_ID);
                string inUserType = param.ElementAt(INDX_SIP_SUBSCRIBER_TYPE);
                string sTermID = param.ElementAt(INDX_ESL_TERMINATION_ID);
                string inProType = param.ElementAt(INDX_ESL_PROTOCOL_TYPE);
                string inGateType = param.ElementAt(INDX_ESL_GATEWAY_TYPE);

                switch (userType)
                {
                    case UserType.sipuser:
                        if (inUserType.ToLower().Equals(S_SIP_SUBSCRIBER_TYPE.ToLower()))
                        {
                            DataRow dTrSIP = dS.Tables[S_SIP_SUBSCRIBER_TABLE].Rows.Find(inSubNum);

                            if (dTrSIP is null)
                            {
                                dTrSIP = dS.Tables[S_SIP_SUBSCRIBER_TABLE].NewRow();
                                dTrSIP["SUBSCRIBER_ID"] = inSubNum;
                                dS.Tables[S_SIP_SUBSCRIBER_TABLE].Rows.Add(dTrSIP);
                            }

                            //dTr["SUBSCRIBER_ID"] = inSubNum;
                            dTrSIP["SUBSCRIBER_TYPE"] = inUserType;
                            dTrSIP["USERNAME"] = inDevice;

                            iUserCount++;

                            txt_status.Invoke(new Action(() => txt_status.Text = string.Format("Loading {0} Users", iUserCount)));

                            continue;
                        }
                        break;

                    case UserType.esluser:
                        if (!inUserType.ToUpper().Equals(S_ESL_SUBSCRIBER_TYPE.ToUpper())) continue;

                        DataRow dTrESL = dS.Tables[S_ESL_SUBSCRIBER_TABLE].Rows.Find(inSubNum);

                        if (dTrESL is null)
                        {
                            dTrESL = dS.Tables[S_ESL_SUBSCRIBER_TABLE].NewRow();

                            dTrESL["SUBSCRIBER_ID"] = inSubNum;

                            dS.Tables[S_ESL_SUBSCRIBER_TABLE].Rows.Add(dTrESL);
                            iNumOfUsers++;
                        }


                        dTrESL["SUBSCRIBER_TYPE"] = inUserType;
                        dTrESL["GATEWAY_ID"] = inDevice;
                        dTrESL["TID"] = sTermID.Replace("'", "");
                        dTrESL["PROTOCOL_TYPE"] = inProType;
                        dTrESL["GATEWAY_TYPE"] = inGateType;

                        iUserCount++;

                        txt_status.Invoke(new Action(() => txt_status.Text = string.Format("Loading {0} Users", iUserCount)));

                        continue;
                    default:
                        break;
                }
            }         

            iNumOfUsers = iUserCount;      

        }

        private void processFileSIPUserData(string file, ref DataSet dS, ref int iNumOfUsers)
        {
           
            StreamReader sRead = new StreamReader(file);

            for (int i = 0; i < INDX_SIP_FILE_LINE_START - 1; i++)
                sRead.ReadLine();

            StringBuilder sBuild = new StringBuilder(sRead.ReadToEnd());

            sRead.Close();

            int iUserCount = iNumOfUsers;

            string[] lines = sBuild.ToString().Split(new string[] { "\n\r", "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {                
                string[] param = line.Split(new string[] { "," }, StringSplitOptions.None);

                string inSubNum = param.ElementAt(INDX_SIP_SUBSCRIBER_NUMBER_SIP_DATA);
                string inSrcCode = param.ElementAt(INDX_SIP_CALL_SOURCE_CODE);
                string inSubStat = param.ElementAt(INDX_SIP_SUBSCRIBER_STATUS);
                string inCallOA = param.ElementAt(INDX_SIP_CALL_OUT_AUTHORITY);

                DataRow dTr = dS.Tables[S_SIP_SUBSCRIBER_TABLE].Rows.Find(inSubNum);

                if (dTr is null)
                {
                    dTr = dS.Tables[S_SIP_SUBSCRIBER_TABLE].NewRow();

                    dTr["SUBSCRIBER_ID"] = inSubNum;

                    dS.Tables[S_SIP_SUBSCRIBER_TABLE].Rows.Add(dTr);
                    iNumOfUsers++;
                }

                dTr["SUBSCRIBER_STATUS"] = inSubStat;
                dTr["CALL_SOURCE_CODE"] = inSrcCode;
                dTr["CALL_OUT_AUTH"] = inCallOA;

                iUserCount++;

                txt_status.Invoke(new Action(() => txt_status.Text = string.Format("Loading {0} Users", iUserCount)));
            }

            iNumOfUsers = iUserCount;
        }

        private void processFileESLUserData(string file, ref DataSet dS, ref int iNumOfUsers)
        {
            StreamReader sRead = new StreamReader(file);            

            for (int i = 0; i < INDX_SIP_FILE_LINE_START - 1; i++)
                sRead.ReadLine();

            StringBuilder sBuild = new StringBuilder(sRead.ReadToEnd());
            sRead.Close();

            int iUserCount = iNumOfUsers;

            string[] lines = sBuild.ToString().Split(new string[] { "\n\r", "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string[] param = line.Split(new string[] { "," }, StringSplitOptions.None);
                
                string sSubNum = param.ElementAt(INDX_SIP_SUBSCRIBER_NUMBER_SIP_DATA);
                string sSrcCode = param.ElementAt(INDX_ESL_CALL_SOURCE_CODE);
                string sSubStat = param.ElementAt(INDX_SIP_SUBSCRIBER_STATUS);
                string sCallOA = param.ElementAt(INDX_ESL_CALL_OUT_AUTHORITY);                

                DataRow dTr = dS.Tables[S_ESL_SUBSCRIBER_TABLE].Rows.Find(sSubNum);

                if (dTr is null)
                {
                    dTr = dS.Tables[S_ESL_SUBSCRIBER_TABLE].NewRow();

                    dTr["SUBSCRIBER_ID"] = sSubNum;

                    dS.Tables[S_ESL_SUBSCRIBER_TABLE].Rows.Add(dTr);
                    iNumOfUsers++;
                }

                dTr["SUBSCRIBER_STATUS"] = sSubStat;
                dTr["CALL_SOURCE_CODE"] = sSrcCode;
                dTr["CALL_OUT_AUTH"] = sCallOA;                

                iUserCount++;

                txt_status.Invoke(new Action(() => txt_status.Text = string.Format("Loading {0} Users", iUserCount)));

            }

            iNumOfUsers = iUserCount;
        }

        private void processFileMGWFile(string file, ref DataSet dS, ref int iNumOfUsers)
        {
            StreamReader sRead = new StreamReader(file);

            for (int i = 0; i < INDX_SIP_FILE_LINE_START - 1; i++)
                sRead.ReadLine();

            StringBuilder sBuild = new StringBuilder(sRead.ReadToEnd());
            sRead.Close();

            int iUserCount = iNumOfUsers;

            string[] lines = sBuild.ToString().Split(new string[] { "\n\r", "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string[] param = line.Split(new string[] { "," }, StringSplitOptions.None);

                string sMID = param.ElementAt(INDX_ESL_MEDIAGATEWAY_ID);
                string sMIDDesc = param.ElementAt(INDX_ESL_GATEWAY_DESC);

                DataRow [] dTr = dS.Tables[S_ESL_SUBSCRIBER_TABLE].Select (string.Format("GATEWAY_ID = '{0}'", sMID));               

                foreach (DataRow item in dTr)
                {
                    item["GATEWAY_DESC"] = sMIDDesc;                   

                    iUserCount++;
                    txt_status.Invoke(new Action(() => txt_status.Text = string.Format("Loading {0} Users", iUserCount)));
                }            
            }

            iNumOfUsers = iUserCount;
        }

        private void processFilePassword(string file, ref DataSet dS, ref int iNumOfUsers)
        {
            StreamReader sRead = new StreamReader(file);

            int iUserCount = iNumOfUsers;

            string inSubNum = string.Empty;
            string inSubPassword = string.Empty;

            while (sRead.Peek() > -1)
            {
                string line = sRead.ReadLine();
                line = line.Trim();

                string[] param = line.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

                if (param is null | param.Length != 2)
                    continue;

                switch (param.ElementAt(0).Trim().ToLower())
                {
                    case "subscriber number":
                        inSubNum = param.ElementAt(1).Trim();

                        continue;
                    case "authorization password":
                        inSubPassword = param.ElementAt(1).Trim();

                        break;

                    default:
                        continue;
                }

                DataRow dTr = dS.Tables[S_SIP_SUBSCRIBER_TABLE].Rows.Find(inSubNum);

                if (dTr is null)
                    continue;

                dTr["PASSWORD"] = inSubPassword;
                iUserCount++;

                txt_status.Invoke(new Action(() => txt_status.Text = string.Format("Loading Password for {0} Users ", iUserCount)));
            }

            iNumOfUsers = iUserCount;

            sRead.Close();
        }

        private void processFilePaswordCSV (string file, ref DataSet dS, ref int iNumOfUsers)
        {

            StreamReader sRead = new StreamReader(file);

            sRead.ReadLine();

            StringBuilder sBuild = new StringBuilder(sRead.ReadToEnd());

            sRead.Close();

            int iUserCount = iNumOfUsers;

            string[] lines = sBuild.ToString().Split(new string[] { "\n\r", "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string sInput = Microsoft.VisualBasic.Interaction.InputBox("Index of Password field: ", "Password field location", "5");

            int iPassword = -1;

            int.TryParse(sInput, out iPassword);

            if (iPassword == -1)
                iPassword = 5;

            foreach (string line in lines)
            {
                string[] param = line.Split(new string[] { "," }, StringSplitOptions.None);

                string sSubNum = param.ElementAt(0);
                string sPassword= param.ElementAt(iPassword - 1);

                DataRow dTr = dS.Tables[S_SIP_SUBSCRIBER_TABLE].Rows.Find(sSubNum);

                if (dTr is null) continue;

                dTr["PASSWORD"] = sPassword;

                iUserCount++;

                txt_status.Invoke(new Action(() => txt_status.Text = string.Format("Loading {0} Users", iUserCount)));
            }

            iNumOfUsers = iUserCount;          
        }

        private void txt_subNum_TextChanged(object sender, EventArgs e)
        {

            switch (S_CURRENT_TABLE)
            {
                case "SearchViewSIP":
                    S_CURRENT_TABLE = S_SIP_SUBSCRIBER_TABLE;
                    break;

                case "SearchViewESL":
                    S_CURRENT_TABLE = S_ESL_SUBSCRIBER_TABLE;
                    break;

                default:
                    break;
            }

            if (string.IsNullOrEmpty(txt_subNum.Text))
            {
                dataGridView1.DataSource = dS;
                dataGridView1.DataMember = dS.Tables[S_CURRENT_TABLE].TableName;
                updateNumericFromTo();
                return;
            }

            EnumerableRowCollection<DataRow> query =
                from sub in dS.Tables[S_CURRENT_TABLE].AsEnumerable()
                where sub.Field<string>("SUBSCRIBER_ID").StartsWith(txt_subNum.Text)
                select sub;

            DataView view = query.AsDataView();

            dataGridView1.DataSource = view;

            string viewTable = "SearchViewSIP";

            if (S_CURRENT_TABLE == "ESL")
                viewTable = "SearchViewESL";

            dS.Tables[viewTable].Clear();

            query.CopyToDataTable(dS.Tables[viewTable], LoadOption.OverwriteChanges);

            int i = dS.Tables[viewTable].Rows.Count;

            btn_process.Text = string.Format("Process {0} Entries", i);

            S_CURRENT_TABLE = viewTable;

            updateNumericFromTo();
        }

        private void updateNumericFromTo()
        {
            int iNumOfRows = dS.Tables[S_CURRENT_TABLE].Rows.Count;

            numFrom.Invoke(new Action(() => numFrom.Maximum = iNumOfRows));
            numFrom.Invoke(new Action(() => numFrom.Value = numFrom.Maximum > 1? 1: 0));

            numTo.Invoke(new Action(() => numTo.Maximum = iNumOfRows));
            numTo.Invoke(new Action(() => numTo.Value = iNumOfRows));

            int i = dS.Tables[S_CURRENT_TABLE].Rows.Count;
            btn_process.Invoke(new Action(() => btn_process.Text = string.Format("Process {0} Entries", i)));            
        }

        private void btn_process_Click(object sender, EventArgs e)
        {
            iProgBarProgress = 0;

            bPauseProcess = false;

            stopProcessingToolStripMenuItem.Enabled = true;

            ExecMode exMode = ExecMode.none;

            switch (cbExecutionMode.SelectedIndex)
            {
                case 0: //prompt
                    exMode = ExecMode.prompt;
                    break;

                case 1: //ignore
                    exMode = ExecMode.ignore;
                    break;

                case 2: //fix
                    exMode = ExecMode.fix;

                    break;
                default:
                    break;
            }

            EslMode eslMode = EslMode.none;

            switch (cbEslMode.SelectedIndex)
            {
                case 0: //device only
                    eslMode = EslMode.deviceOnly;
                    break;

                case 1: //subscribers only
                    eslMode = EslMode.subsOnly;
                    break;

                default:
                    eslMode = EslMode.none;
                    break;
            }

            Program.UpdateProgressBar(progBarMainForm, dS.Tables[S_CURRENT_TABLE].Rows.Count, 0);

            btn_process.Enabled = false;
            cbExecutionMode.Enabled = false;
            btn_open.Enabled = false;
            cb_fileType.Enabled = false;
            cb_inputFile.Enabled = false;

            ThreadStart threadProcessDataStart = delegate { ProvisionInputData(S_CURRENT_TABLE, exMode, eslMode); };
            Thread threadProcessData = new Thread(threadProcessDataStart);
            threadProcessData.Start();
        }

        private void ProvisionInputData(string S_TABLE_NAME, ExecMode exMode, EslMode eslMode)
        {
            switch (S_TABLE_NAME)
            {
                case "SearchViewSIP":
                case "SIP USER":
                    provisionSipUserTable(S_TABLE_NAME, exMode);

                    break;

                case "SearchViewESL":
                case "ESL":
                    provisionEslUserTable (S_TABLE_NAME, exMode, eslMode);

                    break;

                default:
                    break;
            }

            new Task(new Action(() => stopProcessingToolStripMenuItem.Enabled = true));
        }

        private void timerCallBack(object callback)
        {
            int iLastProgrss = iCurrentProgress;

            iCurrentProgress = iProgBarProgress - iLastProgrss == 0 ? 1 : iProgBarProgress - iLastProgrss;

            decimal dTotNum = numTo.Value - numFrom.Value;

            int iTotNum = Convert.ToInt32(dTotNum) - iProgBarProgress;

            int imSec = (iTotNum * I_TIMER_INTERVAL) / iCurrentProgress;

            TimeSpan ts1 = new TimeSpan(0, 0, 0, 0, imSec);

            txt_eta.Invoke(new Action(() => txt_eta.Text = string.Format("ETA {0}{1}", Environment.NewLine, ts1.ToString())));

            iCurrentProgress = iProgBarProgress;

        }
            
        private bool InitiateProvisioningSequenceSIP(string inSubNum, string inSubUsername, string inSubPassword, string inSubStatus, string inCOA, int iCallSrcCode, bool PrepaidCustomer, bool DeleteBefore, out string sResult)
        {
            if (DeleteBefore) RollBackSubscriber(inSubNum, null, webRef, EslMode.none);

            XmlRequests iRequest = new XmlRequests();
            bool bResult = false;            

            string aResult = string.Empty;
            int iResult = 9999;

            iRequest.ADD_HHSSSUB(inSubNum, inSubUsername, inSubUsername, inSubPassword, false, null, 1, webRef, out sResult);

            bResult = int.TryParse(sResult, out iResult);
            if (!bResult | iResult > 0)
                return ReturnLastResult(inSubNum, "NOT SUCCESSFUL", "FAILED TO CREATE CUSTOMER IN IMSHSS: " + sResult, false);

            iRequest.HSIFC (inSubNum, 1, webRef, int.Parse(parameters.userIFC()), servRequest.prov, out sResult);

            bResult = int.TryParse(sResult, out iResult);
            if (!bResult | iResult > 0)
                return ReturnLastResult(inSubNum, "NOT SUCCESSFUL", "FAILED TO ASSIGN IFC TO CUSTOMER: " + sResult, false);

            iRequest.ADD_DNAPTRREC(inSubNum, 1, webRef, out sResult);

            bResult = int.TryParse(sResult, out iResult);
            if (!bResult | iResult > 0)
                return ReturnLastResult(inSubNum, "NOT SUCCESSFUL", "FAILED TO CREATE CUSTOMER IN DNS: " + sResult, false);

            iRequest.ADD_MSR (inSubNum, PrepaidCustomer, 1, iCallSrcCode, out sResult);

            bResult = int.TryParse(sResult, out iResult);
            if (!bResult | iResult > 0)
                return ReturnLastResult(inSubNum, "NOT SUCCESSFUL", "FAILED TO CREATE CUSTOMER IN ATS: " + sResult, false);

            ServiceType serviceType = new ServiceType();

            switch (inSubStatus.ToLower().Trim())
            {
                case "normal":
                    serviceType = ServiceType.normal;
                    break;

                case "owing charge in call-out":
                    serviceType = ServiceType.odbActivate;
                    break;

                case "owing charge in call-out and call-in":
                    serviceType = ServiceType.suspend;
                    break;

                default:
                    break;
            }

            if (serviceType != ServiceType.normal)
            {
                Thread.Sleep(500);
                iRequest.VOICE_BARRING_SUSPENSION(inSubNum, serviceType, 1, out sResult);

                if (!bResult | iResult > 0)
                    return ReturnLastResult(inSubNum, "NOT SUCCESSFUL", "FAILED TO MODIFY VOICE AUTHORITY: " + sResult, false);

            }

            switch (inCOA.Substring(4, 1))
            {
                case "0":
                    break;

                case "1":
                    Thread.Sleep(500);
                    iRequest.IDD_SERVICE(inSubNum, false, 1, out sResult);
                    bResult = int.TryParse(sResult, out iResult);

                    if (!bResult | iResult > 0)
                        return ReturnLastResult(inSubNum, "NOT SUCCESSFUL", "FAILED TO MODIFY IDD SERVICE: " + sResult, false);

                    break;

                default:
                    break;
            }

            return ReturnLastResult(inSubNum, "SUCCESSFUL", String.Empty, true);
        }

        private bool InitiateProvisioningSequenceESL(string sSubscriberNumber, string sPassword, string sSubscriberSatus, string sCOA, int iCallSrcCode, string sEquipmentID, string sMediaGwDescription, string sTerminationID, int iGatewayType, int iProtocolType, bool bDeleteBefore, EslMode eslMode, out string sResult)
        {
            if (bDeleteBefore) RollBackSubscriber(sSubscriberNumber, sEquipmentID, webRef, eslMode);

            XmlRequests iRequest = new XmlRequests();
            bool bResult = false;            

            string aResult = string.Empty;
            int iResult = 9999;

            switch (eslMode)
            {
                case EslMode.deviceOnly:
                    iRequest.AGCF_MGW(sEquipmentID, sMediaGwDescription, iGatewayType, iProtocolType, servRequest.prov, 1, out sResult);

                    bResult = int.TryParse(sResult, out iResult);
                    if (!bResult | iResult > 0) return ReturnLastResult(sSubscriberNumber, "NOT SUCCESSFUL", "FAILED TO CREATE CUSTOMER IN AGCF-MGW: " + sResult, false);

                    return ReturnLastResult(sEquipmentID, "SUCCESSFUL", String.Empty, true);

                case EslMode.subsOnly:
                    break;

                case EslMode.none:
                    sResult = string.Empty;
                    return ReturnLastResult(string.Empty, "NOTHING TO DO", String.Empty, true);

                default:
                    sResult = string.Empty;
                    return ReturnLastResult(string.Empty, "NOTHING TO DO", String.Empty, true);
            }


            iRequest.ADD_HHSSSUB(sSubscriberNumber, sPassword, 1, webRef, out sResult);

            bResult = int.TryParse(sResult, out iResult);
            if (!bResult | iResult > 0) return ReturnLastResult(sSubscriberNumber, "NOT SUCCESSFUL", "FAILED TO CREATE CUSTOMER IN IMSHSS: " + sResult, false);

            iRequest.HSIFC(sSubscriberNumber,1, null, 11, servRequest.prov, out sResult);

            bResult = int.TryParse(sResult, out iResult);
            if (!bResult | iResult > 0) return ReturnLastResult(sSubscriberNumber, "NOT SUCCESSFUL", "FAILED TO ASSIGN IFC TO CUSTOMER: " + sResult, false);

            iRequest.ADD_DNAPTRREC (sSubscriberNumber, 1, webRef, out sResult);

            bResult = int.TryParse(sResult, out iResult);
            if (!bResult | iResult > 0) return ReturnLastResult(sSubscriberNumber, "NOT SUCCESSFUL", "FAILED TO CREATE CUSTOMER IN DNS: " + sResult, false);

            iRequest.ADD_MSR (sSubscriberNumber, false, 1, iCallSrcCode, out sResult);

            bResult = int.TryParse(sResult, out iResult);
            if (!bResult | iResult > 0) return ReturnLastResult(sSubscriberNumber, "NOT SUCCESSFUL", "FAILED TO CREATE CUSTOMER IN ATS: " + sResult, false);

            iRequest.AGCF_ASBR (sSubscriberNumber, sEquipmentID, sTerminationID, sPassword, iGatewayType, iProtocolType, servRequest.prov, 1, out sResult);

            bResult = int.TryParse(sResult, out iResult);
            if (!bResult | iResult > 0) return ReturnLastResult(sSubscriberNumber, "NOT SUCCESSFUL", "FAILED TO CREATE CUSTOMER IN AGCF-ASBR: " + sResult, false);

            ServiceType serviceType = new ServiceType();

            switch (sSubscriberSatus.ToLower().Trim())
            {
                case "normal":
                    serviceType = ServiceType.normal;
                    break;

                case "owing charge in call-out":
                    serviceType = ServiceType.odbActivate;
                    break;

                case "owing charge in call-out and call-in":
                    serviceType = ServiceType.suspend;
                    break;

                default:
                    break;
            }

            if (serviceType != ServiceType.normal)
            {
                Thread.Sleep(500);
                iRequest.VOICE_BARRING_SUSPENSION(sSubscriberNumber, serviceType, 1, out sResult);

                if (!bResult | iResult > 0)
                    return ReturnLastResult(sSubscriberNumber, "NOT SUCCESSFUL", "FAILED TO MODIFY VOICE AUTHORITY: " + sResult, false);

            }

            switch (sCOA.Substring(4, 1))
            {
                case "0":
                    break;

                case "1":
                    Thread.Sleep(500);
                    iRequest.IDD_SERVICE(sSubscriberNumber, false, 1, out sResult);
                    bResult = int.TryParse(sResult, out iResult);

                    if (!bResult | iResult > 0)
                        return ReturnLastResult(sSubscriberNumber, "NOT SUCCESSFUL", "FAILED TO MODIFY IDD SERVICE: " + sResult, false);

                    break;

                default:
                    break;
            }

            return ReturnLastResult(sSubscriberNumber, "SUCCESSFUL", String.Empty, true);
        }
        
        private void ProvisionIndividualSubscriber(SipUserData sipUserData)
        {
            string sSubscriberNumber = sipUserData.inSubNum;
            string sSubUserName = sipUserData.inSubUserName;
            string sSubPassword = sipUserData.inSubPassword;
            string sSubStatus = sipUserData.inSubStatus;
            string sCOA = sipUserData.inCallOutAuth;
            int iCSC = sipUserData.iCallSrcCode;
            bool bPrepaidInAts = sipUserData.ATSPrepaid;
            ExecMode exMode = sipUserData.exMode;

            bool bFixExecuted = false;
            bool bRemoveUserFirst = false;

            reTry:

            bool bResult = InitiateProvisioningSequenceSIP(sSubscriberNumber, sSubUserName, sSubPassword, sSubStatus, sCOA, iCSC, bPrepaidInAts, bRemoveUserFirst, out string sResult);

            if (!bResult)
            {
                if (string.IsNullOrEmpty(sResult) || sResult.Contains("ERR5004") || string.IsNullOrEmpty(webRef)) //session invalid. re-login
                {
                    XmlRequests iReq = new XmlRequests();

                    while (!iReq.PGW_LGI(0, out webRef))
                        Thread.Sleep(2000);

                    exMode = ExecMode.fix;
                    bFixExecuted = false;
                }

                switch (exMode)
                {
                    case ExecMode.prompt:
                        switch (MessageBox.Show(string.Format("{0}{1}{2}", "Subscriber Number: ", sSubscriberNumber + Environment.NewLine + Environment.NewLine, "Result: "), "Failure Report", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2))
                        {
                            case DialogResult.Abort:
                                bPauseProcess = true;
                                return;

                            case DialogResult.Retry:
                                if (MessageBox.Show("Do you like to remove user data in IMS before Re-Trying?" + Environment.NewLine + Environment.NewLine + "Yes: remove user data before re-Trying" + Environment.NewLine + " No: Just Re-Try.", "Failure Report", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    bRemoveUserFirst = true;

                                goto reTry;

                            case DialogResult.Ignore:
                                break;

                            default:
                                break;
                        }
                        break;
                    case ExecMode.ignore:
                        break;

                    case ExecMode.fix:
                        if (bFixExecuted)
                        {
                            ReturnLastResult(sSubscriberNumber, "NOT SUCCESSFUL", sResult, true);

                            break;
                        }
                        bFixExecuted = true;
                        bRemoveUserFirst = true;

                        goto reTry;

                    case ExecMode.none:
                        break;

                    default:
                        break;
                }
            }
        }

        private void ProvisionSIPSubscriber(object UserDataClass)
        {
            SipUserData sipUserData = (SipUserData)UserDataClass;

            int i = 0;

            lock (this)
            {
                i = ++iProgBarProgress; //Interlocked.Increment(ref intProgBarProgress);
            }

            txt_status.Invoke(new Action(() => txt_status.Text = string.Format("Trying user {0}, Count: {1}  Of: {2}", sipUserData.inSubNum, iProgBarProgress, progBarMainForm.Maximum)));


            ThreadStart threadProgBarStart = delegate { Program.UpdateProgressBar(progBarMainForm, 0, iProgBarProgress); };
            Thread threadProgBar = new Thread(threadProgBarStart);
            threadProgBar.Start();

            if (i < sipUserData.dFrom | i > sipUserData.dTo)
                return;

            Program.WriteLog(sipUserData.inSubNum, i.ToString() + ",BEG", false);

            ProvisionIndividualSubscriber(sipUserData);

            Program.WriteLog(sipUserData.inSubNum, i.ToString() + ",END", false);
        }

        private void ProvisionESLSubscriber(object UserDataClass)
        {
            EslUserData eslUserData = (EslUserData)UserDataClass;

            int i = 0;

            lock (this)
            {
                i = ++iProgBarProgress; //Interlocked.Increment(ref intProgBarProgress);
            }

            txt_status.Invoke(new Action(() => txt_status.Text = string.Format("Trying user {0}, Count: {1}  Of: {2}", eslUserData.sSubscriberNumber, iProgBarProgress, progBarMainForm.Maximum)));


            ThreadStart threadProgBarStart = delegate { Program.UpdateProgressBar(progBarMainForm, 0, iProgBarProgress); };
            Thread threadProgBar = new Thread(threadProgBarStart);
            threadProgBar.Start();

            if (i < eslUserData.dFrom | i > eslUserData.dTo)
                return;

            Program.WriteLog (eslUserData.sSubscriberNumber, i.ToString() + ",BEG", false);
            
            ExecMode exMode = eslUserData.exMode;

            bool bFixExecuted = false;
            bool bRemoveUserFirst = false;

            reTry:
            bool bResult = InitiateProvisioningSequenceESL(eslUserData.sSubscriberNumber, eslUserData.sPassword, eslUserData.sSubscriberStatus, 
                eslUserData.sCallOutAuth, eslUserData.iCallSrcCode, eslUserData.sEquipmentID,  
                eslUserData.sGatewayDescription, eslUserData.sTerminationID, eslUserData.iGatewayType, 
                eslUserData.iProtocolType, bRemoveUserFirst, eslUserData.eslMode, out string sResult);

            if (!bResult)
            {
                if (string.IsNullOrEmpty(sResult) || sResult.Contains("ERR5004") || string.IsNullOrEmpty(webRef)) //session invalid. re-login
                {
                    XmlRequests iReq = new XmlRequests();

                    while (!iReq.PGW_LGI(0, out webRef))
                        Thread.Sleep(2000);

                    exMode = ExecMode.fix;
                    bFixExecuted = false;
                }

                switch (exMode)
                {
                    case ExecMode.prompt:
                        switch (MessageBox.Show(string.Format("{0}{1}{2}", "Subscriber Number: ", eslUserData.sSubscriberNumber + Environment.NewLine + Environment.NewLine, "Result: "), "Failure Report", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2))
                        {
                            case DialogResult.Abort:
                                bPauseProcess = true;
                                return;

                            case DialogResult.Retry:
                                if (MessageBox.Show("Do you like to remove user data before Re-Trying?" + Environment.NewLine + Environment.NewLine + "Yes: remove user data before re-Trying" + Environment.NewLine + " No: Just Re-Try.", "Failure Report", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    bRemoveUserFirst = true;

                                goto reTry;

                            case DialogResult.Ignore:
                                break;

                            default:
                                break;
                        }
                        break;
                    case ExecMode.ignore:
                        break;

                    case ExecMode.fix:
                        if (bFixExecuted)
                        {
                            ReturnLastResult(eslUserData.sSubscriberNumber, "NOT SUCCESSFUL", sResult, true);

                            break;
                        }
                        bFixExecuted = true;
                        bRemoveUserFirst = true;

                        goto reTry;

                    case ExecMode.none:
                        break;

                    default:
                        break;
                }
            }

            Program.WriteLog(eslUserData.sSubscriberNumber, i.ToString() + ",END", false);
        }

        private void RollBackSubscriber(string sSubscriberNumber, string sEquipmentID, string webRef, EslMode eslMode)
        {
            
            XmlRequests iRequest = new XmlRequests();

            string sResult = string.Empty;

            switch (eslMode)
            {
                case EslMode.deviceOnly:
                    iRequest.AGCF_MGW(sEquipmentID, null, -1, -1, servRequest.remove, 2, out sResult);
                    return;

                case EslMode.subsOnly:
                    iRequest.RMV_HHSSSUB(sSubscriberNumber, 2, webRef, out sResult);
                    iRequest.RMV_DNAPTRREC(sSubscriberNumber, 2, webRef, out sResult);
                    iRequest.AGCF_ASBR(sSubscriberNumber, null, null, null, -1, -1, servRequest.remove, 2, out sResult);
                    return;

                case EslMode.none:
                    iRequest.RMV_HHSSSUB(sSubscriberNumber, 2, webRef, out sResult);
                    iRequest.RMV_DNAPTRREC(sSubscriberNumber, 2, webRef, out sResult);
                    return;

                default:
                    break;
            }
        }

        private bool ReturnLastResult(string sSubNum, string result, string comment, bool bResult)
        {

            Program.WriteLog(sSubNum, result + "," + comment, bResult);

            return bResult;
        }

        private void UpdateUserType(out UserType userType)
        {
            userType = UserType.none;

            switch (cb_fileType.SelectedItem.ToString())
            {
                case "SIP USER":
                    S_CURRENT_TABLE = S_SIP_SUBSCRIBER_TABLE;
                    userType = UserType.sipuser;
                    break;

                case "ESL":
                    S_CURRENT_TABLE = S_ESL_SUBSCRIBER_TABLE;
                    userType = UserType.esluser;
                    break;

                default:
                    break;
            }
        }

        private void UpdateEslMode (out EslMode eslMode)
        {
            eslMode = EslMode.none;

            switch (cbEslMode.SelectedItem.ToString ())
            {                
                case "DEVICE ONLY":
                    S_CURRENT_TABLE = S_ESL_SUBSCRIBER_TABLE;
                    eslMode = EslMode.deviceOnly;
                    break;

                case "SUBSCRIBERS ONLY":
                    S_CURRENT_TABLE = S_ESL_SUBSCRIBER_TABLE;
                    eslMode = EslMode.subsOnly;
                    break;

                default:
                    break;
            }
        }
        
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings ResourceManagerForm = new FormSettings(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void manualProvisioningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManual ManProv = new FormManual(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutFrm = new AboutBox1(this);
        }

        private void exportView(string S_TABLE_NAME)
        {

            string FileDialogTitle = "Select passwords input file";
            string FileType = "CSV files(*.csv)|*.csv|XML files(*.xml)|*.xml";

            string sFileName = string.Format("Export_{0}", DateTime.Now.ToString("ddMMyyyy_hhmmss"));

            try
            {
                bool bResult = Program.SaveFileDialog(FileDialogTitle, FileType, sFileName, out string sOutFile, out int iFormatIndex);

                DataTable dt = dS.Tables[S_TABLE_NAME];

                switch (iFormatIndex)
                {
                    case 2:
                        dt.WriteXml(sOutFile, true);
                        MessageBox.Show("View Exported as XML", "Export Success", MessageBoxButtons.OK);

                        return;
                    case 1:
                        StringBuilder sb = new StringBuilder();

                        IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                                          Select(column => column.ColumnName);
                        sb.AppendLine(string.Join(",", columnNames));

                        int i = 1;

                        foreach (DataRow row in dt.Rows)
                        {
                            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                            sb.AppendLine(string.Join(",", fields));

                            txt_status.Text = string.Format("Exporting {0}", ++i);
                            txt_status.Refresh();
                        }

                        File.WriteAllText(sOutFile, sb.ToString());

                        //MessageBox.Show("View Exported as CSV", "Export Success", MessageBoxButtons.OK);

                        txt_status.Text = string.Format("{0} rows exported to {1}", i, sOutFile);

                        break;

                    default:

                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void exportViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            exportView(S_CURRENT_TABLE);
        }
    
        private void ChangeCurrentTableOnGrid(string tableName, bool SwapAll)
        {

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dS;

            if (SwapAll)
            {
                dataGridView1.DataMember = dS.Tables[S_SIP_SUBSCRIBER_TABLE].TableName;
                dataGridView1.DataMember = dS.Tables[S_ESL_SUBSCRIBER_TABLE].TableName;
            }

            dataGridView1.DataMember = dS.Tables[tableName].TableName;

            S_CURRENT_TABLE = tableName;

            txt_subNum.Clear();
        }

        private void cb_fileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_fileType.SelectedIndex >= 0)
            {
                btn_open.Enabled = true;
                cb_inputFile.Enabled = true;
            }

            cb_inputFile.Items.Clear();
            cb_inputFile.Items.Add("ALL USER DATA");
            cbExecutionMode.SelectedIndex = -1;
            cbEslMode.SelectedIndex = -1;            
            cbEslMode.Enabled = false;

            switch (cb_fileType.SelectedItem.ToString())
            {
                case "SIP USER":
                    cb_inputFile.Items.Add("SIP USER DATA");
                    cb_inputFile.Items.Add("PASSWORD FROM N2000");
                    cb_inputFile.Items.Add("PASSWORD FROM CSV");
                    S_CURRENT_TABLE = S_SIP_SUBSCRIBER_TABLE;
                    cntxMenu.Items["generatePasswordToolStripMenuItem"].Enabled = false;
                    break;

                case "ESL":                    
                    cb_inputFile.Items.Add("ESL USER DATA");
                    cb_inputFile.Items.Add("ESL MGW DATA");
                    S_CURRENT_TABLE = S_ESL_SUBSCRIBER_TABLE;
                    cntxMenu.Items["generatePasswordToolStripMenuItem"].Enabled = true;
                    break;

                default:
                    break;
            }

            dataGridView1.DataSource = dS.Tables[S_CURRENT_TABLE];

            //dataGridView1.DataMember = dS.Tables[S_CURRENT_TABLE].TableName;

            cbExecutionMode_SelectedIndexChanged(sender, e);
            updateNumericFromTo();            

            cb_inputFile.SelectedIndex = 0;

            importViewToolStripMenuItem.Enabled = true;
        }

        private void btn_open_Click(object sender, EventArgs e)
        {

            UserType userType = UserType.none;

            UpdateUserType (out userType);

            string FileDialogTitle = "Locate a File ";
            string FileExtension = "CSV files(*.csv)|*.csv|All files(*.*)|*.*";

            string InputFile = (string)cb_inputFile.SelectedItem;

            FileType fileType = FileType.none;

            switch (InputFile)
            {
                case "ALL USER DATA":
                    FileDialogTitle = "Select ALL USER DATA Input File(s)";
                    fileType = FileType.allUsrData;

                    break;

                case "SIP USER DATA":
                    FileDialogTitle = "Select SIP USER DATA Input File(s)";                    
                    fileType = FileType.sipUsrData;

                    break;

                case "ESL USER DATA":
                    FileDialogTitle = "Select ESL USER DATA Input File(s)";                    
                    fileType = FileType.eslUsrData;

                    break;

                case "ESL MGW DATA":
                    FileDialogTitle = "Select ESL MGW DATA Input File(s)";
                    //cb_fileType.SelectedItem = "ESL";
                    fileType = FileType.eslMgwData;

                    break;

                case "PASSWORD FROM N2000":
                    FileDialogTitle = "Select N2000 Password input file(s)";
                    FileExtension = "TXT files(*.txt)|*.txt|LOG files(*.log)|*.log|All files(*.*)|*.*";
                    fileType = FileType.password;

                    break;

                case "PASSWORD FROM CSV":
                    FileDialogTitle = "Select CSV Password input file";
                    FileExtension = "CSV files(*.csv)|*.csv";
                    fileType = FileType.passwordcsv;

                    break;

                default:
                    break;
            }

            string[] files = new string[] { };

            try
            {
                Program.OpenFileDialog(FileDialogTitle, FileExtension, out files);

            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Input file error", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                {
                    btn_open_Click(sender, e);
                    return;
                }
                else
                    return;
            }

            btn_open.Enabled = false;
            btn_process.Enabled = false;
            txt_subNum.Enabled = false;
            cbExecutionMode.Enabled = false;
            cbEslMode.Enabled = false;

            dataGridView1.DataSource = dS;

            Task.Run(() => processInputFile(files, fileType, userType));
        }

        private void provisionSipUserTable(string S_TABLE_NAME, ExecMode exMode)
        {
            int iTotUsers = dS.Tables[S_TABLE_NAME].Rows.Count;

            XmlRequests iRequest = new XmlRequests();

            if (!iRequest.PGW_LGI(0, out webRef))
                return;

            try
            {
                string fileName = string.Format("{0} SIP_USER_LOG_{1}.log", Program.S_LOG_PATH, DateTime.Now.ToString("ddMMyyyy_hhmmss"));

                using (File.CreateText(fileName))

                    if (File.Exists(fileName))
                    {
                        Program.S_LOG_FILE = fileName;
                        Program.S_RESULT_FILE = Program.S_LOG_FILE + ".result";
                    }

            }
            catch (Exception)
            {

            }

            decimal dFrom = numFrom.Value;

            System.Threading.Timer t1 = new System.Threading.Timer(timerCallBack, null, I_TIMER_INTERVAL, I_TIMER_INTERVAL);

            foreach (DataRow iRow in dS.Tables[S_TABLE_NAME].Rows)
            {
                while (bPauseProcess)
                    Thread.Sleep(2000);

                string[] sSubData = new string[] { };

                string sSubNum = iRow.ItemArray.ToArray().ElementAt(0).ToString();
                string sSubType = iRow.ItemArray.ToArray().ElementAt(1).ToString();
                string sSubStatus = iRow.ItemArray.ToArray().ElementAt(2).ToString();
                string sSubUsername = iRow.ItemArray.ToArray().ElementAt(3).ToString();
                string sSubPassword = iRow.ItemArray.ToArray().ElementAt(4).ToString();
                string sCallSrcCode = iRow.ItemArray.ToArray().ElementAt(5).ToString();
                string sCallOutAuth = iRow.ItemArray.ToArray().ElementAt(6).ToString();

                if (iRow.ItemArray.ToArray().Contains(string.Empty) | iRow.ItemArray.ToArray().Contains(null) | iRow.ItemArray.Contains(DBNull.Value))
                {
                    Thread.Sleep(1000);

                    Program.WriteLog(sSubNum, "NOT EXECUTED" + "SOME OF SUBSCRIBER INFORMATION ARE MISSING. CHECK INPUT DATA.", true);

                    iProgBarProgress++;

                    continue;
                }

                int iCallSrcCode = -1;

                int.TryParse(sCallSrcCode, out iCallSrcCode);

                bool bPrepaidSubscriber = false;

                switch (iCallSrcCode)
                {
                    case 301: //postpaid
                        bPrepaidSubscriber = false;
                        iCallSrcCode = 0;
                        break;

                    case 401: //prepaid
                        bPrepaidSubscriber = true;
                        iCallSrcCode = 1;
                        break;

                    case 501: //business postpaid
                        bPrepaidSubscriber = false;
                        iCallSrcCode = 0;
                        break;

                    default: //anything else.
                        bPrepaidSubscriber = false;
                        iCallSrcCode = 0;
                        break;
                }

                try
                {
                    decimal dTo = numTo.Value;

                    SipUserData subDetails = new SipUserData(sSubNum, sSubUsername, sSubPassword, sSubStatus, sCallOutAuth, iCallSrcCode, bPrepaidSubscriber, exMode, webRef, dFrom, dTo);
                    ProvisionSIPSubscriber(subDetails);
                }
                catch (Exception ex)
                {
                    Program.WriteLog(sSubNum, string.Format("--- Exception while processing subscriber number {0}. Exception details {1}. ", sSubNum, ex.Message), true);
                    Program.WriteLog(sSubNum, ex.StackTrace, true);
                }
            }

            txt_eta.Invoke(new Action(() => txt_eta.Clear()));

            cb_inputFile.Invoke(new Action(() => cb_inputFile.Enabled = true));
            cb_fileType.Invoke(new Action(() => cb_fileType.Enabled = true));

            cbExecutionMode.Invoke(new Action(() => cbExecutionMode.Enabled = true));
        }

        private void provisionEslUserTable(string S_TABLE_NAME, ExecMode exMode, EslMode eslMode)
        {
            // login to PGW
            XmlRequests iRequest = new XmlRequests();
            if (!iRequest.PGW_LGI(0, out webRef)) return;

            // prepare log file
            try
            {
                string fileName = string.Format("{0} ESL_USER_LOG_{1}.log", Program.S_LOG_PATH, DateTime.Now.ToString("ddMMyyyy_hhmmss"));

                using (File.CreateText(fileName))

                    if (File.Exists(fileName))
                    {
                        Program.S_LOG_FILE = fileName;
                        Program.S_RESULT_FILE = Program.S_LOG_FILE + ".result";
                    }

            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to create log file " + e.Message, "Log Error", MessageBoxButtons.OK);
                return;
            }

            //prepare call-back timer for ETA calculation.            
            System.Threading.Timer t1 = new System.Threading.Timer(timerCallBack, null, I_TIMER_INTERVAL, I_TIMER_INTERVAL);

            switch (eslMode)
            {
                case EslMode.deviceOnly:

                    IEnumerable <IGrouping<string, DataRow>> myQueryGroup = from dtr1 in dS.Tables[S_ESL_SUBSCRIBER_TABLE].AsEnumerable()
                                                   group dtr1 by dtr1.Field<string> ("GATEWAY_ID") ;

                    DataTable tbl1 = dS.Tables[S_ESL_SUBSCRIBER_TABLE].Clone();
                    tbl1.TableName = "ESL_DEVICES";

                    dS.Tables.Add(tbl1);

                    IEnumerable<DataRow> myRows = myQueryGroup.Select(x => x.ElementAt(0));

                    myRows.CopyToDataTable(dS.Tables["ESL_DEVICES"], LoadOption.OverwriteChanges);

                    S_TABLE_NAME = "ESL_DEVICES";
                    break;

                case EslMode.subsOnly:
                    break;
                case EslMode.none:
                    break;
                default:
                    break;
            }

            Program.UpdateProgressBar(progBarMainForm, dS.Tables[S_TABLE_NAME].Rows.Count, 0);

            // initialize counters.
            decimal dFrom = numFrom.Value;
            int iTotUsers = dS.Tables[S_TABLE_NAME].Rows.Count;

            // going through the data-table
            foreach (DataRow iRow in dS.Tables[S_TABLE_NAME].Rows)
            {
                // check if all cell complete in this individual row.
                if (iRow.ItemArray.ToArray().Contains(string.Empty) || iRow.ItemArray.ToArray().Contains(null) || iRow.ItemArray.Contains(DBNull.Value))
                {
                    Thread.Sleep(1000);

                    Program.WriteLog(iRow.ItemArray.ToArray().ElementAt(0).ToString(), "NOT EXECUTED" + "SOME OF SUBSCRIBER INFORMATION ARE MISSING. CHECK INPUT DATA.", true);

                    iProgBarProgress++;

                    continue;
                }             

                // read individual parameter values.
                string sSubscriberNumber = iRow.ItemArray.ToArray().ElementAt(0).ToString();
                string sProtocolType = iRow.ItemArray.ToArray().ElementAt(1).ToString();

                string sEquipmentID = iRow.ItemArray.ToArray().ElementAt(2).ToString();
                string sGatewayType = iRow.ItemArray.ToArray().ElementAt(3).ToString();
                string sGatewayDescription = iRow.ItemArray.ToArray().ElementAt(4).ToString();               
                
                string sPassword = iRow.ItemArray.ToArray().ElementAt(5).ToString();

                string sTerminationID = iRow.ItemArray.ToArray().ElementAt(6).ToString();
                int iTerminationID = -1;
                int.TryParse(sTerminationID, out iTerminationID);

                string sSubType = iRow.ItemArray.ToArray().ElementAt(7).ToString();
                string sSubStatus = iRow.ItemArray.ToArray().ElementAt(8).ToString();
                
                int iCallSrcCode = -1;
                int.TryParse(iRow.ItemArray.ToArray().ElementAt(9).ToString(), out iCallSrcCode);

                string sCallOutAuth = iRow.ItemArray.ToArray().ElementAt(10).ToString();
                
                // set the call-source-code & iProtocolType
                int iProtocolType = -1;
                switch (sProtocolType.ToLower())
                {
                    case "mgcp":
                        iCallSrcCode = 2;
                        iProtocolType = 0;
                        break;

                    case "h248":
                        iCallSrcCode = 3;
                        iProtocolType = 1;
                        break;

                    default:
                        Program.WriteLog(sSubscriberNumber, "NOT EXECUTED" + "UNEXPECTED PROTOCOL TYPE :" + sProtocolType.ToLower(), true);
                        continue;
                }

                int iGatewayType = -1;
                switch (sGatewayType.ToLower())
                {
                    case "integrated access gateway":
                        iGatewayType = 2;
                        break;
                    case "access gateway":
                        iGatewayType = 0;
                        break;

                    default:
                        Program.WriteLog(sSubscriberNumber, "NOT EXECUTED" + "UNEXPECTED GATEWAY TYPE :" + sGatewayType.ToLower(), true);
                        continue;
                }

                try
                {
                    decimal dTo = numTo.Value;

                    EslUserData eslUser = new EslUserData(sEquipmentID, sGatewayDescription, sSubscriberNumber, sPassword, sTerminationID, sCallOutAuth, sSubStatus, iGatewayType, iProtocolType, iCallSrcCode, false, exMode, eslMode, webRef, dFrom, dTo);

                    ProvisionESLSubscriber(eslUser);
                }
                catch (Exception ex)
                {
                    Program.WriteLog(sSubscriberNumber, string.Format("--- Exception while processing subscriber number {0}. Exception details {1}. ", sSubscriberNumber, ex.Message), true);
                    Program.WriteLog(sSubscriberNumber, ex.StackTrace, true);
                }
            }

            txt_eta.Invoke(new Action(() => txt_eta.Clear()));

            cb_inputFile.Invoke(new Action(() => cb_inputFile.Enabled = true));
            cb_fileType.Invoke(new Action(() => cb_fileType.Enabled = true));

            cbExecutionMode.Invoke(new Action(() => cbExecutionMode.Enabled = true));
        
        }

        private void cbExecutionMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbExecutionMode.SelectedIndex > -1 && cbEslMode.SelectedIndex > -1)
                btn_process.Enabled = true;
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RefreshUpdateGridView();
        }

        private void RefreshUpdateGridView ()
        {
            dataGridView1.Invoke(new Action(() => dataGridView1.DataSource = dS.Tables[S_SIP_SUBSCRIBER_TABLE]));
            dataGridView1.Invoke(new Action(() => dataGridView1.DataSource = dS.Tables[S_ESL_SUBSCRIBER_TABLE]));
            dataGridView1.Invoke(new Action(() => dataGridView1.DataSource = dS.Tables[S_CURRENT_TABLE]));
        }

        private void stopProcessingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (bPauseProcess)
            {
                case true:
                    bPauseProcess = false;
                    stopProcessingToolStripMenuItem.Text = "Pause Processing";
                    numTo.Enabled = false;
                    break;

                default:
                    bPauseProcess = true;
                    stopProcessingToolStripMenuItem.Text = "Resume Processing";
                    numTo.Enabled = true;
                    break;
            }
        }

        private void numFrom_ValueChanged(object sender, EventArgs e)
        {

            if (numFrom.Value >= numTo.Value)
            {
                btn_process.Enabled = false;
                return;
            }

            cbExecutionMode_SelectedIndexChanged(sender, e);

            btn_process.Text = string.Format("Process {0} Entries", (numTo.Value - numFrom.Value) + 1);
        }

        private void numTo_ValueChanged(object sender, EventArgs e)
        {
            if (numFrom.Value >= numTo.Value)
            {
                btn_process.Enabled = false;
                return;
            }

            cbExecutionMode_SelectedIndexChanged(sender, e);

            btn_process.Text = string.Format("Process {0} Entries", (numTo.Value - numFrom.Value) + 1);

        }
        
        private void importViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

            UpdateUserType(out UserType userType);

            string sFileName = string.Empty;

            openFileDialog1.Title = "Choose csv File Data-Table to Import";
            openFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog1.FileName = "*.csv";
            openFileDialog1.Multiselect = false;

            DialogResult dResult = openFileDialog1.ShowDialog();

            switch (dResult)
            {
                case DialogResult.None:
                    return;

                case DialogResult.OK:
                    sFileName = openFileDialog1.FileName;
                    break;

                default:
                    return;
            }

            if (!File.Exists(sFileName)) return;

            dS.Tables[S_CURRENT_TABLE].Clear();

            refreshToolStripMenuItem1_Click(sender, e);

            StreamReader sRead = new StreamReader(sFileName);

            sRead.ReadLine();

            StringBuilder sBuild = new StringBuilder(sRead.ReadToEnd());

            sRead.Close();

            IEnumerable<string[]> items = sBuild.ToString().Split(new string[] { "\n\r", "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Split(new string[] { "," }, StringSplitOptions.None));
            
            int i = 1;

            dataGridView1.DataMember = null;

            foreach (string[] row in items)
            {
                dS.Tables[S_CURRENT_TABLE].Rows.Add(row);

                txt_status.Text = string.Format("Importing {0}", ++i);
                txt_status.Refresh();

            }            

            txt_status.Text = string.Format("{0} rows imported.", i);

            processInputFile(new string[] { }, FileType.none, userType);

            btn_process.Enabled = false;
        }

        private void generatePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iLen = 8;

            string sLen = Microsoft.VisualBasic.Interaction.InputBox("Enter Password Length:", "Password Length?", "8");

            int.TryParse(sLen, out iLen);

            DataRow[] dtr = dS.Tables[S_ESL_SUBSCRIBER_TABLE].Select("PASSWORD is null");

            foreach (DataRow item in dtr)            
                item["PASSWORD"] = Program.GetRandomString(iLen);            
                
        }

        private void cbEslMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (S_CURRENT_TABLE)
            {
                case S_ESL_SUBSCRIBER_TABLE:
                    if (cbExecutionMode.SelectedIndex > -1 && cbEslMode.SelectedIndex > -1) btn_process.Enabled = true;
                    break;

                case S_SIP_SUBSCRIBER_TABLE:
                    if (cbExecutionMode.SelectedIndex > -1) btn_process.Enabled = true;
                    break;

                default:
                    break;
            }            
        }

        private void txt_eta_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

