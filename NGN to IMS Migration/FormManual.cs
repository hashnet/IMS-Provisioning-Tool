using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NGN_to_IMS_Migration
{
    

    public partial class FormManual : Form
    {

        Dictionary<string, string> MyKeys = new Dictionary<string, string> { };

        Form MainForm = new Form();     

        public UserType userType;

        bool bStopProcess = false;

        public FormManual(Form sender)
        {
            sender.Hide();

            MainForm = sender;

            InitializeComponent();

            this.Show();
        }      

        private void ManualProvision_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.Show();
            this.Dispose(true);
        }

        private void updateKeys ()
        {
            MyKeys = new Dictionary<string, string> { };

            foreach (DataGridViewRow item in dataGridViewInput.Rows)
            {
                if (item.Cells[1].Value is null)
                    continue;

                MyKeys.Add(item.Cells[0].Value.ToString(), item.Cells[1].Value.ToString());
            }
        }

        private void btn_add_hhsssub_Click(object sender, EventArgs e)
        {
            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;
            
            updateKeys();            

            if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]) || string.IsNullOrEmpty(MyKeys["PASSWORD"]))
            {
                MessageBox.Show("subscriber information missing, please fill in all required inforamtion.", "required input missing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (userType == UserType.trunkuser && cb_pilot.Checked && string.IsNullOrEmpty (MyKeys["PBX ID"]))
            {
                MessageBox.Show("pbx id is missing, please fill in all required inforamtion.", "required input missing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            iRequest.ADD_HHSSSUB(MyKeys, userType, out sResult);
         
            Button btn = (Button)sender;
            updateDataGrid(btn.Text, sResult, MyKeys["SUBSCRIBER NUMBER"]);
        }

        private void btn_add_hsifc_Click(object sender, EventArgs e)
        {
            updateKeys();

            if (string.IsNullOrEmpty (MyKeys["SUBSCRIBER NUMBER"]) | string.IsNullOrEmpty (MyKeys["IFC"]))
            {
                MessageBox.Show("subscriber number or IFC is missing, please fill in all required inforamtion.", "required input missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            int ifc = -1;

            if (!int.TryParse (MyKeys["IFC"], out ifc))
            {
                MessageBox.Show("Invalid IFC value.", "invalid IFC", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;

            }

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.HSIFC (MyKeys["SUBSCRIBER NUMBER"], 0, null, ifc, servRequest.prov, out sResult);

            Button btn = (Button)sender;
            updateDataGrid(btn.Text, sResult, MyKeys["SUBSCRIBER NUMBER"]);

        }

        private void btm_add_dnaptrrec_Click(object sender, EventArgs e)
        {
            updateKeys();

            if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]))
            {
                MessageBox.Show("subscriber number is missing, please fill in all required inforamtion.", "required input missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.ADD_DNAPTRREC(MyKeys["SUBSCRIBER NUMBER"], 0, out sResult);

            Button btn = (Button)sender;
            updateDataGrid(btn.Text, sResult, MyKeys["SUBSCRIBER NUMBER"]);
        }

        private void btn_add_msr_Click(object sender, EventArgs e)
        {
            try
            {
                updateKeys();

                string sSubscriberNumber = MyKeys["SUBSCRIBER NUMBER"];

                if (string.IsNullOrEmpty(sSubscriberNumber))
                    throw new Exception("'SUBSCRIBER NUMBER' can't be empty");

                bool prepaid = false;
                int iCallSrcCode = -1;

                switch (userType)
                {
                    case UserType.sipuser:
                        if (MyKeys["SUBSCRIBER TYPE"] == "PREPAID")
                        {
                            prepaid = true;
                            iCallSrcCode = 1;
                        }
                        else
                        {
                            prepaid = false;
                            iCallSrcCode = 0;
                        }

                        break;
                    case UserType.trunkuser:
                        ADD_MSR_SIP_TRUNK();

                        return;
                    case UserType.esluser:
                        int iProtocolType = -1;
                        int.TryParse(MyKeys["PROTOCOL TYPE"], out iProtocolType);

                        switch (iProtocolType)
                        {
                            case 0:
                                iCallSrcCode = 2;
                                break;

                            case 1:
                                iCallSrcCode = 3;
                                break;

                            default:
                                throw new Exception("Invalid 'PROTOCOL TYPE'");
                        }
                        
                        break;
                    case UserType.password:
                        return;
                    case UserType.stp:
                        return;
                    case UserType.none:
                        return;
                    default:
                        break;
                }

                XmlRequests iRequest = new XmlRequests();              

                iRequest.ADD_MSR (sSubscriberNumber, prepaid, 0, iCallSrcCode, out string sResult);

                Button btn = (Button)sender;
                updateDataGrid(btn.Text, sResult, MyKeys["SUBSCRIBER NUMBER"]);
            }          
            catch (KeyNotFoundException)
            {
                MessageBox.Show("Mandatory Parameter value is missing. check input parameters");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btn_rmv_hhsssub_Click(object sender, EventArgs e)
        {
            updateKeys();

            if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]))
            {
                MessageBox.Show("Subscriber number can not be empty.", "check subscriber number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.RMV_HHSSSUB(MyKeys["SUBSCRIBER NUMBER"], 0, out sResult);

            Button btn = (Button)sender;
            updateDataGrid(btn.Text, sResult, MyKeys["SUBSCRIBER NUMBER"]);

        }      

        private void rmv_dnaptrrec_Click(object sender, EventArgs e)
        {
            updateKeys();

            if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]))
            {
                MessageBox.Show("Subscriber number can not be empty.", "check subscriber number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.RMV_DNAPTRREC (MyKeys["SUBSCRIBER NUMBER"], 0, out sResult);

            Button btn = (Button)sender;
            updateDataGrid(btn.Text, sResult, MyKeys["SUBSCRIBER NUMBER"]);
        }

        private void processCallServices (ServiceType serviceType)
        {
            if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]))
            {
                MessageBox.Show("Subscriber number can not be empty.", "check subscriber number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.VOICE_BARRING_SUSPENSION(MyKeys["SUBSCRIBER NUMBER"], serviceType, 0, out sResult);

            updateDataGrid(serviceType.ToString(), sResult, MyKeys["SUBSCRIBER NUMBER"]);
        }

        private void processIDDservices(bool Withdraw)
        {
            if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]))
            {
                MessageBox.Show("Subscriber number can not be empty.", "check subscriber number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.IDD_SERVICE(MyKeys["SUBSCRIBER NUMBER"], Withdraw, 0, out sResult);

            updateDataGrid("IDD-" + Withdraw.ToString(), sResult, MyKeys["SUBSCRIBER NUMBER"]);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings ResourceManagerForm = new FormSettings(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMain mainForm = new FormMain();
            this.Hide();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            //txt_output.Clear();
            //dataGridViewOutput.Rows.Clear();
            txt_detail_output.Clear();
        }

        private void rmv_msr_Click(object sender, EventArgs e)
        {
            updateKeys();

            if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]))
            {
                MessageBox.Show("Subscriber number can not be empty.", "check subscriber number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.RMV_MSR(MyKeys["SUBSCRIBER NUMBER"], 0, out sResult);

            Button btn = (Button)sender;
            updateDataGrid(btn.Text, sResult, MyKeys["SUBSCRIBER NUMBER"]);
        }

        private void processPrintOut (string SelectedItem, int SelectedIndex)
        {          
            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            string sComment = string.Empty;

            string command = "N/A";

            try
            {
                switch (SelectedItem)
                {
                    case "LST HHSSSUB":
                        iRequest.LST_HSUB(MyKeys["SUBSCRIBER NUMBER"], 0, 999, out sResult);
                        command = "LST HHSSSUB";

                        break;

                    case "LST HHSSSUB (SIP ID/IMPI)":
                        iRequest.LST_HSUB(MyKeys["SIP ID"], 0, 999, out sResult, 0);                        
                        updateDataGrid("LST HHSSSUB (SIP ID/IMPI)", sResult, MyKeys["SIP ID"]);
                        return;

                    case "LST HHSSSUB (FDN/IMPU)":
                        iRequest.LST_HSUB(MyKeys["SUBSCRIBER NUMBER"], 0, 999, out sResult);
                        command = "LST HHSSSUB (FDN/IMPU)";
                        break;

                    case "LST HSIFC":
                        iRequest.HSIFC(MyKeys["SUBSCRIBER NUMBER"], 0, null, -1, servRequest.printOut, out sResult);
                        command = "LST HSIFC";

                        break;

                    case "LST DNAPTRREC":
                        iRequest.LST_DNAPTRREC(MyKeys["SUBSCRIBER NUMBER"], 0, out sResult);
                        command = "LST DNAPTRREC";

                        break;

                    case "LST MSR":
                        iRequest.LST_MSR(MyKeys["SUBSCRIBER NUMBER"], 0, out sResult);
                        command = "LST MSR";

                        break;

                    case "LST HCAPSCSCF":
                        iRequest.HCAPSCSCF(MyKeys["SUBSCRIBER NUMBER"], null, null, 0, servRequest.printOut, out sResult);
                        command = "LST HCAPSCSCF";

                        break;

                    case "NPDB PRINTOUT(STP)":
                        Dictionary<string, string> npdbResult = new Dictionary<string, string> { };

                        txt_detail_output.Text = "Printout in progress, please wait \n.";
                        txt_detail_output.Refresh();

                        if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]))
                            throw new KeyNotFoundException("'SUBSCRIBER NUMBER' is empty.");                            

                        System.Threading.ThreadStart threadReadNpDb = delegate { Program.ProcessOpNpdbStatus(MyKeys["SUBSCRIBER NUMBER"], out npdbResult, true); };
                        System.Threading.Thread threadProcessData = new System.Threading.Thread(threadReadNpDb);
                        threadProcessData.Start();

                        while (threadProcessData.IsAlive)
                        {
                            txt_detail_output.Invoke(new Action(() => txt_detail_output.AppendText(".")));
                            txt_detail_output.Invoke(new Action(() => txt_detail_output.Refresh()));

                            System.Threading.Thread.Sleep(500);
                        }

                        int iCount = 0;
                        sResult = "";

                        foreach (var item in npdbResult)
                        {
                            iCount++;

                            string sKey = item.Key;
                            string sVal = item.Value;

                            if (iCount % 2 == 0)
                            {
                                string[] keyArr = item.Key.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                                sResult += string.Format("{0}={1}\n", keyArr[1], item.Value);

                                updateDataGrid(keyArr[0], sResult, MyKeys["SUBSCRIBER NUMBER"]);

                                sResult = "";
                            }
                            else
                            {
                                string[] keyArr = item.Key.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                                sResult += string.Format("{0}={1}\n", keyArr[1], item.Value);
                            }
                        }

                        return;

                    case "LST SUB (HLR)":
                        iRequest.LST_SUB(MyKeys["SUBSCRIBER NUMBER"], null, 0, out sResult);
                        command = "LST SUB";

                        break;

                    case "LST AGCF_MGW":
                        CommandAGCFMGw(servRequest.printOut);
                        return;

                    case "LST AGCF_ASBR (FDN/PUI)":
                        CommandAGCFAsbr(servRequest.printOut);
                        return;

                    case "LST AGCF_ASBR (MID/EID)":
                        CommandAGCFAsbr(servRequest.printAsbrMgw);
                        return;

                    default:
                        break;
                }

                updateDataGrid(command, sResult, MyKeys["SUBSCRIBER NUMBER"]);
            }
            catch  (KeyNotFoundException)
            {
                MessageBox.Show("missing parameter");

            }           

        }

        private void btn_execute_Click(object sender, EventArgs e)
        {
            updateKeys();

            try
            {
                XmlRequests iRequest = new XmlRequests();
                string sResult = string.Empty;

                switch (parameters.ManualXmlInputDestination().ToUpper())
                {
                    case "SPG":
                        iRequest.USER_INPUT_SPG(txt_soapinput.Text, 0, out sResult);
                        break;

                    case "PGW":
                        iRequest.USER_INPUT_PGW(txt_soapinput.Text, 0, out sResult);
                        break;

                    default:
                        break;
                }

                Button btn = (Button)sender;
                updateDataGrid(btn.Text, sResult, MyKeys["SUBSCRIBER NUMBER"]);

            }
            catch (Exception ex)
            {
                Button btn = (Button)sender;
                updateDataGrid(btn.Text, ex.Message, MyKeys["SUBSCRIBER NUMBER"]);
            }

        }
     
        private void btn_back_Click(object sender, EventArgs e)
        {
            exitToolStripMenuItem_Click(sender, e);
        }      

        private void btn_rmv_hsifc_Click(object sender, EventArgs e)
        {
            updateKeys();

            if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]) | string.IsNullOrEmpty(MyKeys["IFC"]))
            {
                MessageBox.Show("subscriber number or IFC is missing, please fill in all required inforamtion.", "required input missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            int ifc = -1;

            if (!int.TryParse(MyKeys["IFC"], out ifc))
            {
                MessageBox.Show("Invalid IFC value.", "invalid IFC", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;

            }

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.HSIFC(MyKeys["SUBSCRIBER NUMBER"],0, null, ifc, servRequest.remove, out sResult);

            Button btn = (Button)sender;
            updateDataGrid(btn.Text, sResult, MyKeys["SUBSCRIBER NUMBER"]);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutFrm = new AboutBox1(this);
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormSettings ResourceManagerForm = new FormSettings(this);
        }

        private void btn_back_Click_1(object sender, EventArgs e)
        {
            MainForm.Show();
            this.Close();
        }

        private void ChangeTextBoxColorForMissingInput (ref TextBox textbox, Color color, string messageText)
        {
            textbox.ForeColor = color;
            MessageBox.Show(messageText, "required input missing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }        
 
        private void FormManual_Load(object sender, EventArgs e)
        {
            FormSettings frm = new FormSettings(this);
            frm.Close();

            sIPUserToolStripMenuItem_Click(sender, e);

        }

        private void btn_add_hcapscscf_Click(object sender, EventArgs e)
        {
            updateKeys();

            if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]) | string.IsNullOrEmpty(MyKeys["HCAP CSCF"]) | string.IsNullOrEmpty (MyKeys["HCAP CSCF"]))
            {
                MessageBox.Show("subscriber number or HCAP SCSCF is missing, please fill in all required inforamtion.", "required input missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }           

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.HCAPSCSCF(MyKeys["SUBSCRIBER NUMBER"].Trim(), MyKeys["HCAP CSCF"].Trim(), MyKeys["PBX ID"].Trim(), 0, servRequest.prov, out sResult);

            Button btn = (Button)sender;
            updateDataGrid(btn.Text, sResult, MyKeys["SUBSCRIBER NUMBER"]);

        }

        private void btn_rmv_hcapscscf_Click(object sender, EventArgs e)
        {
            updateKeys();

            if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]) | string.IsNullOrEmpty(MyKeys["HCAP CSCF"]) | string.IsNullOrEmpty(MyKeys["HCAP CSCF"]))
            {
                MessageBox.Show("subscriber number or HCAP SCSCF is missing, please fill in all required inforamtion.", "required input missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.HCAPSCSCF (MyKeys["SUBSCRIBER NUMBER"].Trim(), MyKeys["HCAP CSCF"].Trim(), MyKeys["PBX ID"].Trim(), 0, servRequest.remove, out sResult);

            Button btn = (Button)sender;
            updateDataGrid(btn.Text, sResult, MyKeys["SUBSCRIBER NUMBER"]);
        }

        private void btn_lst_hcapscscf_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]))
            {
                MessageBox.Show("subscriber number is missing, please fill in all required inforamtion.", "required input missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.HCAPSCSCF (MyKeys["SUBSCRIBER NUMBER"].Trim(), null, null, 0, servRequest.printOut, out sResult);

            Button btn = (Button)sender;
            updateDataGrid(btn.Text, sResult, MyKeys["SUBSCRIBER NUMBER"]);
        }

        private void CommandAGCFMGw (servRequest eRequest)
        {
            updateKeys();

            if (userType != UserType.esluser)
            {
                MessageBox.Show("Incorrect mode, please choose ESL in Mode selection for this command to work.");
                return;
            }

            MyKeys.TryGetValue("EQUIPMENT ID", out string sEquipmentID);
            MyKeys.TryGetValue("SUBSCRIBER NUMBER", out string sSubscriberNumber);
            MyKeys.TryGetValue("MGW DESCRIPTION", out string sMediaGatewayDesc);
            MyKeys.TryGetValue("GATEWAY TYPE", out string sGatewayType);
            MyKeys.TryGetValue("PROTOCOL TYPE", out string sProtocolType);

            int.TryParse(sGatewayType, out int iGatewayType);
            int.TryParse(sProtocolType, out int iProtocolType);

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.AGCF_MGW(sEquipmentID, sMediaGatewayDesc, iGatewayType, iProtocolType, eRequest, 0, out sResult);

            updateDataGrid("AGCF-MGW", sResult, sEquipmentID);            

        }

        private bool checkParamer (string param)
        {
            if (MyKeys.ContainsKey (param) && !string.IsNullOrEmpty(MyKeys[param]))
                return true;

            MessageBox.Show("Parameter '" + param + "' is required.");

            return false;
        }

        private void CommandAGCFAsbr(servRequest eRequest)
        {
            updateKeys();

            if (userType != UserType.esluser)
            {
                MessageBox.Show("Incorrect mode, please choose ESL in Mode selection for this command to work.");
                return;
            }          

            MyKeys.TryGetValue("EQUIPMENT ID", out string sEquipmentID);            
            MyKeys.TryGetValue ("SUBSCRIBER NUMBER", out string sSubscriberNumber);
            MyKeys.TryGetValue("GATEWAY TYPE", out string sGatewayType);
            MyKeys.TryGetValue("PROTOCOL TYPE", out string sProtocolType);
            MyKeys.TryGetValue ("TERMINATION ID", out string sTerminationID);
            MyKeys.TryGetValue("PASSWORD", out string sPassword);

            switch (eRequest)
            {
                case servRequest.prov:
                    if (!checkParamer("EQUIPMENT ID")) return;
                    if (!checkParamer("SUBSCRIBER NUMBER")) return;
                    if (!checkParamer("GATEWAY TYPE")) return;
                    if (!checkParamer("TERMINATION ID")) return;
                    if (!checkParamer("PASSWORD")) return;

                    break;
                case servRequest.remove:
                    if (!checkParamer("SUBSCRIBER NUMBER")) return;
                    break;
                case servRequest.printOut:
                    if (!checkParamer("SUBSCRIBER NUMBER")) return;
                    break;
                case servRequest.printAsbrMgw:
                    if (!checkParamer("EQUIPMENT ID")) return;
                    break;
                case servRequest.none:
                    break;
                default:
                    break;
            }

            int.TryParse(sGatewayType, out int iGatewayType);
            int.TryParse(sProtocolType, out int iProtocolType);

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            iRequest.AGCF_ASBR(sSubscriberNumber, sEquipmentID, sTerminationID, sPassword, iGatewayType, iProtocolType, eRequest, 0, out sResult);

            updateDataGrid("AGCF-ASBR", sResult, sSubscriberNumber);
        }

        private void ADD_MSR_SIP_TRUNK()
        {
            if (string.IsNullOrEmpty(MyKeys["SUBSCRIBER NUMBER"]))
            {
                MessageBox.Show("Subscriber number can not be empty.", "check subscriber number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!cb_pilot.Checked && string.IsNullOrEmpty (MyKeys["PBX ID"]))
            {
                MessageBox.Show("PBX ID can not be empty.", "check PBX ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            XmlRequests iRequest = new XmlRequests();
            string sResult = string.Empty;

            string trunkID = MyKeys["PBX ID"].Trim();

            iRequest.ADD_MSR(MyKeys["SUBSCRIBER NUMBER"], cb_pilot.Checked, trunkID, 0, out sResult);
            
            updateDataGrid("ADD MSR-SIP TRUNK", sResult, MyKeys["SUBSCRIBER NUMBER"]);
        }

        private void cb_service_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbServices.SelectedIndex >= 0) btn_submit.Enabled = true;
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            updateKeys();

            ServiceType serviceType = ServiceType.normal;

            switch (cbServices.SelectedIndex)
            {
                case 0: //outgoing calls barred
                    serviceType = ServiceType.odbActivate;
                    processCallServices(serviceType);

                    break;
                case 1: //outgoing calls unbarred
                    serviceType = ServiceType.odbDeactivate;
                    processCallServices(serviceType);

                    break;
                case 2: //voice suspension
                    serviceType = ServiceType.suspend;
                    processCallServices(serviceType);

                    break;
                case 3: //voice resume
                    serviceType = ServiceType.resume;
                    processCallServices(serviceType);

                    break;
                case 4:// idd allow
                    processIDDservices(false);
                    
                    break;
                case 5://idd prohibit
                    processIDDservices(true);
                    
                    break;
                default:
                    MessageBox.Show("Option is not implemented yet.");
                    break;
            }
        }

        private void updateDataGrid (string command, string result, string comments)
        {
            XmlResponses response = new XmlResponses();

            string code = "N/A";

            try
            {
                code = response.GetResultCode(result, command);
            }
            catch (Exception)
            {

            }

            string[] array = new string[] { command, result, code, comments };

            dataGridViewOutput.Invoke(new Action(() => dataGridViewOutput.Rows.Add(array)));
        }      

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int i = dataGridViewOutput.VerticalScrollingOffset;
            Point p = new Point(1000, 100000);

            dataGridViewOutput.FirstDisplayedScrollingRowIndex = dataGridViewOutput.Rows.Count - 1;

            dataGridViewOutput.Rows[e.RowIndex].Selected = true;
            txt_detail_output.Text = dataGridViewOutput.SelectedCells[1].Value.ToString();

        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            int i = dataGridViewOutput.VerticalScrollingOffset;
            Point p = new Point(1000, 100000);
        }

        private void copyCellContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_detail_output.SelectedText.Count() <= 0) return;

            Clipboard.SetText(txt_detail_output.SelectedText);
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            toolTip1.Show(dataGridViewOutput.SelectedCells[0].Value.ToString(), dataGridViewOutput);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_detail_output.Text = dataGridViewOutput.SelectedCells[0].Value.ToString();
        }
     
        private void btn_printout_Click(object sender, EventArgs e)
        {
            updateKeys();

            processPrintOut(cbPrintout.SelectedItem.ToString(), cbPrintout.SelectedIndex);
        }

        private void cb_printout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPrintout.SelectedIndex >= 0) btn_printout.Enabled = true;
        }

        private void sIPUserToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            userType = UserType.sipuser;
            UpdateMode(userType);            
        }

        private void sIPTrunkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userType = UserType.trunkuser;
            UpdateMode(userType);
        }

        private void bulkCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            switch (item.Checked)
            {
                case true:
                    item.Checked = false;
                    lbl_manual.Text = "Manual SOAP Input";
                    btn_execute.Enabled = true;
                    lbl_bulk.Visible = false;
                    btn_bulk.Visible = false;
                    cb_bulk.Visible = false;

                    break;

                case false:
                    item.Checked = true;
                    lbl_manual.Text = "List of Subscribers for Bulk Commands";
                    btn_execute.Enabled = false;
                    lbl_bulk.Visible = true;
                    btn_bulk.Visible = true;
                    cb_bulk.Visible = true;

                    break;
                default:
                    break;
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dT = new DataTable("Output");

            foreach (DataGridViewColumn item in dataGridViewOutput.Columns)            
                dT.Columns.Add(item.Name);
            
            foreach (DataGridViewRow row in dataGridViewOutput.Rows )
            {
                DataRow dtR = dT.NewRow();

                foreach (DataGridViewTextBoxCell item in row.Cells)                                   
                    dtR[item.ColumnIndex] = item.Value;
                
                dT.Rows.Add(dtR);
            }

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.FileName = "Export Output log as XML";
            fileDialog.Filter = "xml file (*.xml)|*.xml";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                dT.WriteXml(fileDialog.FileName);

                MessageBox.Show("Done!");                
            }

        }
    
        private void dataGridViewInput_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int Rindx = e.RowIndex;
            int Cindx = e.ColumnIndex;          

            DataGridView dv = (DataGridView)sender;

            if (dv.Rows[Rindx].Cells[0].Value.ToString() == "SUBSCRIBER TYPE" && (dv.Rows[Rindx].Cells[1].Value is null || (dv.Rows[Rindx].Cells[1].Value.ToString() != "POSTPAID" && dv.Rows[Rindx].Cells[1].Value.ToString() != "PREPAID")))
            {
                MessageBox.Show("Invalid value for SUBSCRIBER TYPE in Input parameters.", "Invalid parameter value", MessageBoxButtons.OK, MessageBoxIcon.Error);

                dataGridViewInput[Cindx, Rindx].Value = "POSTPAID";
            }
        }

        private void btn_bulk_Click(object sender, EventArgs e)
        {           
            bStopProcess = false;

            updateKeys();

            txt_status.Clear();

            int indx = cb_bulk.SelectedIndex;
            
            string[] array = txt_soapinput.Text.Split(new string[] { " ", Environment.NewLine, ";", ",", "\n", "\t" }, StringSplitOptions.RemoveEmptyEntries);

            txt_soapinput.Text = string.Join("\n", array);

            Task t1 = new Task(new Action(() => processBulkCommand(indx, array)));
            t1.Start();                       
            
        }

        private void processBulkCommand (int indx, string [] array)
        {
            updateKeys();

            XmlRequests iRequest = new XmlRequests();

            int iCount = 1;

            foreach (var item in array)
            {
                if (bStopProcess) return;

                string sResult = string.Empty;

                txt_status.Invoke(new Action(() => txt_status.Text = string.Format("Processing {0} of {1}", iCount, array.Count())));

                iCount++;

                switch (indx)
                {
                    case 0: //LST HHSSSUB
                        iRequest.LST_HSUB(item, 0, 999, out sResult);
                        updateDataGrid("LST HHSSSUB", sResult, item);                        

                        break;
                    case 1://LST HSIFC
                        iRequest.HSIFC(item, 0, null, -1, servRequest.printOut, out sResult);
                        updateDataGrid("LST HSIFC", sResult, item);                        

                        break;
                    case 2: //LST DNAPTRREC
                        iRequest.LST_DNAPTRREC(item, 0, out sResult);
                        updateDataGrid("LST DNAPTRREC", sResult, item);

                        break;
                    case 3: //LST MSR
                        iRequest.LST_MSR(item, 0, out sResult);
                        updateDataGrid("LST MSR", sResult, item);                        

                        break;
                    case 4: //LST HCAPSCSCF
                        iRequest.HCAPSCSCF(item, null, null, 0, servRequest.printOut, out sResult);
                        updateDataGrid("LST HCAPSCSCF", sResult, item);

                        break;
                    case 5: //ADD HSIFC
                        int ifc = int.Parse(MyKeys["IFC"]);

                        iRequest.HSIFC(item, 0, null, ifc, servRequest.prov, out sResult);
                        updateDataGrid("ADD HSIFC", sResult, item);

                        break;
                    case 6: //ADD DNAPTRREC
                        iRequest.ADD_DNAPTRREC (item, 0, out sResult);
                        updateDataGrid("ADD DNAPTRREC", sResult, item);

                        break;
                    case 7: //RMV HHSSSUB
                        iRequest.RMV_HHSSSUB(item, 0, out sResult);
                        updateDataGrid("RMV HHSSSUB", sResult, item);

                        break;
                    case 8: //RMV HSFIC
                        iRequest.HSIFC (item, 0, null, int.Parse (MyKeys["IFC"]), servRequest.remove, out sResult);
                        updateDataGrid("RMV HSFIC", sResult, item);

                        break;
                    case 9: //RMV DNAPTRREC
                        iRequest.RMV_DNAPTRREC (item, 0, out sResult);
                        updateDataGrid("RMV DNAPTRREC", sResult, item);

                        break;
                    case 10: //RMV MSR
                        iRequest.RMV_MSR (item, 0, out sResult);
                        updateDataGrid("RMV MSR", sResult, item);

                        break;
                    case 11: //LST STP
                        Dictionary<string, string> npdbResult = new Dictionary<string, string> { };

                        Program.ProcessOpNpdbStatus(item, out npdbResult, false);

                        foreach (var stp in npdbResult)                        
                            sResult += string.Format("{0}={1}\n", stp.Key, stp.Value);                        

                        updateDataGrid("LST STP", sResult, item);

                        break;
                    case 12: //RMV IMS (RMV HSUB + RMV DNS
                        iRequest.RMV_HHSSSUB(item, 0, out sResult);
                        updateDataGrid("RMV IMS-HHSSSUB", sResult, item);

                        iRequest.RMV_DNAPTRREC(item, 0, out sResult);
                        updateDataGrid("RMV IMS-DNAPTRREC", sResult, item);

                        break;

                    default:
                        break;
                } 
            }         

        }

        private void cb_bulk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_bulk.SelectedIndex >= 0) btn_bulk.Enabled = true;
        }

        private void ProcessBtnClick (object sender, EventArgs e, servRequest sRequest, string SelectedText, int SelectedItem)
        {
            string sCommand = SelectedText.Substring(4).ToUpper();  
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            updateKeys();

            string sCommand = cbCreate.SelectedItem.ToString().ToUpper();

            switch (sCommand)
            {
                case "ADD HHSSSUB":
                    btn_add_hhsssub_Click(sender, e);
                    return;
                case "ADD HSIFC":
                    btn_add_hsifc_Click(sender, e);
                    return;
                case "ADD DNAPTRREC":
                    btm_add_dnaptrrec_Click(sender, e);
                    return;
                case "ADD MSR":
                    btn_add_msr_Click(sender, e);
                    return;
                case "ADD HCAPSCSCF":
                    btn_add_hcapscscf_Click(sender, e);
                    return;
                case "ADD AGCF_MGW":
                    CommandAGCFMGw(servRequest.prov);
                    return;
                case "ADD AGCF_ASBR":
                    CommandAGCFAsbr(servRequest.prov);
                    return;
                case "NPDB CREATE (STP)":
                    txt_detail_output.Text = "create command in progress, please wait \n.";
                    txt_detail_output.Refresh();

                    Dictionary<string, string> OutResult = new Dictionary<string, string> { };

                    System.Threading.ThreadStart threadReadNpDb = delegate { Program.ProcessOpNpdbChange(MyKeys, servRequest.prov, out OutResult); };
                    System.Threading.Thread threadProcessData = new System.Threading.Thread(threadReadNpDb);
                    threadProcessData.Start();

                    while (threadProcessData.IsAlive)
                    {
                        txt_detail_output.Invoke(new Action(() => txt_detail_output.AppendText(".")));
                        txt_detail_output.Invoke(new Action(() => txt_detail_output.Refresh()));

                        System.Threading.Thread.Sleep(500);
                    }


                    if (OutResult is null) return;

                    foreach (var item in OutResult)                                            
                        updateDataGrid("NPDB CREATE(STP)" , item.Key + " = " + item.Value, MyKeys["SUBSCRIBER NUMBER"]);
                    
                    return;
            }
        }

        private void eSLUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userType = UserType.esluser;
            UpdateMode(userType);
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            updateKeys();

            string sCommand = cbRemove.SelectedItem.ToString().ToUpper();

            switch (sCommand)
            {
                case "RMV HHSSSUB":
                    btn_rmv_hhsssub_Click(sender, e);
                    return;
                case "RMV HSIFC":
                    btn_rmv_hsifc_Click (sender, e);
                    return;
                case "RMV DNAPTRREC":
                    rmv_dnaptrrec_Click (sender, e);
                    return;
                case "RMV MSR":
                    rmv_msr_Click (sender, e);
                    return;
                case "RMV HCAPSCSCF":
                    btn_rmv_hcapscscf_Click (sender, e);
                    return;
                case "RMV AGCF_MGW":
                    CommandAGCFMGw(servRequest.remove);
                    return;
                case "RMV AGCF_ASBR":
                    CommandAGCFAsbr(servRequest.remove);
                    return;
                case "NPDB REMOVE (STP)":
                    txt_detail_output.Text = "remove command in progress, please wait \n.";
                    txt_detail_output.Refresh();

                    Dictionary<string, string> OutResult = new Dictionary<string, string> { };

                    System.Threading.ThreadStart threadReadNpDb = delegate { Program.ProcessOpNpdbChange(MyKeys, servRequest.remove, out OutResult); };
                    System.Threading.Thread threadProcessData = new System.Threading.Thread(threadReadNpDb);
                    threadProcessData.Start();

                    while (threadProcessData.IsAlive)
                    {
                        txt_detail_output.Invoke(new Action(() => txt_detail_output.AppendText(".")));
                        txt_detail_output.Invoke(new Action(() => txt_detail_output.Refresh()));

                        System.Threading.Thread.Sleep(500);
                    }                    

                    if (OutResult is null) return;

                    foreach (var item in OutResult)
                        updateDataGrid("NPDB REMOVE(STP)", item.Key + " = " + item.Value, MyKeys["SUBSCRIBER NUMBER"]);

                    return;
            }
        }

        private void cbCreate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCreate.SelectedIndex >= 0) btn_create.Enabled = true;
        }

        private void cbRemove_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRemove.SelectedIndex >= 0) btn_remove.Enabled = true;
        }

        private void dataGridViewOutput_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= -1)
                return;

            dataGridViewOutput.Rows[e.RowIndex].Selected = true;
            txt_detail_output.Text = dataGridViewOutput.SelectedCells[1].Value.ToString();
        }

        private void sTPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userType = UserType.stp;
            UpdateMode(userType);
        }

        private void UpdateMode (UserType userType)
        { 

            string[] stp_printout = new string[] { "NPDB PRINTOUT(STP)", "LST SUB (HLR)" };
            string[] stp_create = new string[] { "NPDB CREATE (STP)" };
            string[] stp_remove = new string[] { "NPDB REMOVE (STP)" };

            string[] esl_printout = new string[] { "LST HHSSSUB", "LST HSIFC", "LST DNAPTRREC", "LST MSR",  "NPDB PRINTOUT(STP)", "LST SUB (HLR)", "LST AGCF_ASBR (FDN/PUI)", "LST AGCF_ASBR (MID/EID)", "LST AGCF_MGW" };
            string[] esl_create = new string[] { "ADD HHSSSUB", "ADD HSIFC", "ADD DNAPTRREC", "ADD MSR", "ADD AGCF_MGW", "ADD AGCF_ASBR" };
            string[] esl_remove = new string[] { "RMV HHSSSUB", "RMV HSIFC", "RMV DNAPTRREC", "RMV MSR", "RMV AGCF_ASBR", "RMV AGCF_MGW" };

            string[] sipUser_printout = new string[] { "LST HHSSSUB (FDN/IMPU)", "LST HHSSSUB (SIP ID/IMPI)", "LST HSIFC", "LST DNAPTRREC", "LST MSR", "NPDB PRINTOUT(STP)", "LST SUB (HLR)"};
            string[] sipUser_create = new string[] { "ADD HHSSSUB", "ADD HSIFC", "ADD DNAPTRREC", "ADD MSR"};
            string[] sipUser_remove = new string[] { "RMV HHSSSUB", "RMV HSIFC", "RMV DNAPTRREC", "RMV MSR"};

            string[] sipTrunk_printout = new string[] { "LST HHSSSUB", "LST HSIFC", "LST DNAPTRREC", "LST MSR", "LST HCAPSCSCF", "NPDB PRINTOUT(STP)", "LST SUB (HLR)" };
            string[] sipTrunk_create = new string[] { "ADD HHSSSUB", "ADD HSIFC", "ADD DNAPTRREC", "ADD MSR" , "ADD HCAPSCSCF"};
            string[] sipTrunk_remove = new string[] { "RMV HHSSSUB", "RMV HSIFC", "RMV DNAPTRREC", "RMV MSR" , "RMV HCAPSCSCF"};

            cbCreate.Enabled = true;
            cbRemove.Enabled = true;
            cbServices.Enabled = true;
            cbServices.Enabled = true;

            cb_pilot.Visible = false;

            cbCreate.Items.Clear();
            cbRemove.Items.Clear();
            cbPrintout.Items.Clear();

            dataGridViewInput.Rows.Clear();
            txt_status.Clear();

            dataGridViewInput.ContextMenuStrip = null;

            switch (userType)
            {
                case UserType.sipuser:
                    dataGridViewInput.Rows.Add(new string[] { "SUBSCRIBER NUMBER", "" });
                    dataGridViewInput.Rows.Add(new string[] { "SIP ID", "" });
                    dataGridViewInput.Rows.Add(new string[] { "PASSWORD", "" });
                    dataGridViewInput.Rows.Add(new string[] { "IFC", parameters.userIFC() });
                    dataGridViewInput.Rows.Add(new string[] { "SUBSCRIBER TYPE", "POSTPAID" });
                    dataGridViewInput[1, 4].ToolTipText = "POSTPAID or PREPAID";

                    cbCreate.Items.AddRange(sipUser_create);
                    cbRemove.Items.AddRange(sipUser_remove);
                    cbPrintout.Items.AddRange(sipUser_printout);
                    break;

                case UserType.trunkuser:
                    dataGridViewInput.Rows.Add(new string[] { "SUBSCRIBER NUMBER", "" });                    
                    dataGridViewInput.Rows.Add(new string[] { "PASSWORD", "" });
                    dataGridViewInput.Rows.Add(new string[] { "IFC", parameters.trunkIFC() });
                    dataGridViewInput.Rows.Add(new string[] { "PBX ID", "" });
                    dataGridViewInput.Rows.Add(new string[] { "HCAP CSCF", parameters.trunkSCSCF() });

                    cb_pilot.Visible = true;

                    cbCreate.Items.AddRange(sipTrunk_create);
                    cbRemove.Items.AddRange(sipTrunk_remove);
                    cbPrintout.Items.AddRange(sipTrunk_printout);
                    break;

                case UserType.trunkpilot:
                    break;

                case UserType.esluser:
                    dataGridViewInput.Rows.Add(new string[] { "SUBSCRIBER NUMBER", "" });
                    dataGridViewInput.Rows.Add(new string[] { "EQUIPMENT ID", "" });
                    dataGridViewInput.Rows.Add(new string[] { "TERMINATION ID", "" });
                    dataGridViewInput.Rows.Add(new string[] { "MGW DESCRIPTION", "" });
                    dataGridViewInput.Rows.Add(new string[] { "PASSWORD", Program.GetRandomString(8) });
                    dataGridViewInput.Rows.Add(new string[] { "IFC", "11" });
                    dataGridViewInput.Rows.Add(new string[] { "GATEWAY TYPE", "0" });
                    dataGridViewInput.Rows.Add(new string[] { "PROTOCOL TYPE", "0" });
                    dataGridViewInput[0, 6].ToolTipText = "possible values \n 2: Integrated access device \n 0: Access Gateway";
                    dataGridViewInput[1, 6].ToolTipText = "possible values \n 2: Integrated access device \n 0: Access Gateway";
                    dataGridViewInput[0, 7].ToolTipText = "possible values as \n 0: MGCP \n 1: H248";
                    dataGridViewInput[1, 7].ToolTipText = "possible values as \n 0: MGCP \n 1: H248";
                    dataGridViewInput.ContextMenuStrip  = contextMenuInput;

                    cbCreate.Items.AddRange(esl_create);
                    cbRemove.Items.AddRange(esl_remove);
                    cbPrintout.Items.AddRange(esl_printout);
                    break;

                case UserType.password:
                    break;

                case UserType.stp:
                    dataGridViewInput.Rows.Add(new string[] { "SUBSCRIBER NUMBER", "" });
                    dataGridViewInput.Rows.Add(new string[] { "DESCRIPTION", "" });
                    dataGridViewInput.Rows.Add(new string[] { "INDEX NUMBER", "" });

                    cbServices.Enabled = false;      
                    
                    cbCreate.Items.AddRange(stp_create);
                    cbRemove.Items.AddRange(stp_remove);
                    cbPrintout.Items.AddRange(stp_printout);
                    break;

                case UserType.none:
                    break;

                default:
                    break;
            }          
        }

        private void generateRandomPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridViewInput.Rows)
            {
                if (item.Cells[0].Value.ToString() == "PASSWORD")
                {
                    dataGridViewInput[1, item.Index].Value = Program.GetRandomString(8);
                    return;
                }
            }                
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
