using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Collections;

namespace NGN_to_IMS_Migration
{
    public partial class FormSettings : Form
    {
        Form MainForm = new Form();

        Dictionary<string, string> ParamKeys = new Dictionary<string, string> { };

        public FormSettings(Form sender)
        {
            sender.Hide();

            MainForm = sender;

            InitializeComponent();

            this.Show();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            ReturnToMainForm(sender, e);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            reTry:

            ParamKeys.Clear();

            try
            {

                ResourceReader ResRead = new ResourceReader(@".\ConnectResources.resources");

                ResourceSet ResSet = new ResourceSet(@".\ConnectResources.resources");

                DataSet dS = new DataSet();
                dS.Tables.Add("Settings");
                dS.Tables["Settings"].Columns.Add("parameter");
                dS.Tables["Settings"].Columns.Add("value");
                
                IDictionaryEnumerator id = ResSet.GetEnumerator();
                
                while (id.MoveNext())
                {
                    DataRow dTr = dS.Tables["Settings"].NewRow();
                    dTr["parameter"] = id.Key;
                    dTr["value"] = id.Value;

                    ParamKeys.Add(id.Key.ToString (), id.Value.ToString());

                    if (id.Key.ToString().ToLower().Contains("pass"))
                        dTr["value"] = "****";                    

                    dS.Tables["Settings"].Rows.Add(dTr);
                }               
                

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dS;
                dataGridView1.DataMember = dS.Tables["Settings"].TableName;

                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);               

                ResRead.Close();
                ResSet.Close();

                ResRead.Dispose();
                ResSet.Dispose();
            }
            catch (Exception)
            {
                using (ResourceWriter rw = new ResourceWriter(@".\ConnectResources.resources"))
                {
                    rw.AddResource("UrlPGW", "http://10.215.32.30:8001;http://10.215.34.30:8001;");
                    rw.AddResource("urlSPG", "http://10.215.134.76:8080/spg;http://10.215.23.12:8080");
                    rw.AddResource("stpIP", "boNpdb=10.215.32.37;soNpdb=10.215.34.37");
                    rw.AddResource("stpIndex", "0:D95; 1:D12; 2:D11; 3:D83; 4:D84; 5:D85; 6:D92; 7:D81; 8:D82; 9:D13; 10:B14; 11:B12; 12:B11; 13:D20; 14:D21; 15:D90");
                    rw.AddResource("stpUser", "itmediation");
                    rw.AddResource("stpPass", Program.Base64Encode ("itmediation")); //itmediation
                    rw.AddResource("pgwUser", "itbilling");
                    rw.AddResource("pgwPass", Program.Base64Encode ("itbilling")); //itbilling
                    rw.AddResource("spgUser", "bsstest");
                    rw.AddResource("spgPass", Program.Base64Encode ("Huawei!@34")); //Huawei!@34
                    rw.AddResource("DefaultRealm", "ims.ooredoo.om");
                    rw.AddResource("imsEnsZone", "8.6.9.e164.arpa");
                    rw.AddResource("userIFC", "10");
                    rw.AddResource("trunkIFC", "25");
                    rw.AddResource("imsSrvKey", "80");
                    rw.AddResource("imsScfAddr", "96895001060");
                    rw.AddResource("trunkSCSCF", "sip:boscscf1.ims.ooredoo.om");
                    rw.AddResource("AGCF", "BOAGCF");
                    rw.AddResource("ManualXmlInputDestination", "SPG");
                    rw.Generate();

                    rw.Close();
                    rw.Dispose();
                    
                }

                goto reTry;
            }                       



        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Are you sure about the changes you made?", "Sure!", MessageBoxButtons.YesNo))
            {
                case DialogResult.Yes:
                    ResourceWriter rw = new ResourceWriter(@".\ConnectResources.resources");
                   
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string key = dataGridView1[0, i].Value.ToString();
                        string val = dataGridView1[1, i].Value.ToString();

                        if (val == "****")
                            val = ParamKeys[key];                        

                        rw.AddResource (key, val);
                    }

                    rw.Generate();

                    rw.Dispose();

                    MessageBox.Show("Great job, new values updated.", "Update success", MessageBoxButtons.OK);

                    break;
                case DialogResult.No:
                    MessageBox.Show("Get more self-confidence and come again later.", "Changes aborted.", MessageBoxButtons.OK);
                    break;
                default:
                    break;
            }

            Form2_Load(sender, e);

            ReturnToMainForm(sender, e);
        }
      
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            ReturnToMainForm(sender, e);
        }

        private void ReturnToMainForm(object sender, EventArgs e)
        {
            MainForm.Show();
            
            this.Dispose(true);

        }
    }
}
