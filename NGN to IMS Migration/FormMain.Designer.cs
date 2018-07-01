namespace NGN_to_IMS_Migration
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_open = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.progBarMainForm = new System.Windows.Forms.ProgressBar();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cntxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportViewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.generatePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataSet1 = new System.Data.DataSet();
            this.btn_process = new System.Windows.Forms.Button();
            this.txt_status = new System.Windows.Forms.TextBox();
            this.cb_inputFile = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_subNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_fileType = new System.Windows.Forms.ComboBox();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualProvisioningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.manualProvisioningToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.switchViewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.manualProvisioningToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopProcessingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.cbExecutionMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numFrom = new System.Windows.Forms.NumericUpDown();
            this.numTo = new System.Windows.Forms.NumericUpDown();
            this.txt_eta = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbEslMode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.cntxMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTo)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_open
            // 
            this.btn_open.Enabled = false;
            this.btn_open.Location = new System.Drawing.Point(11, 38);
            this.btn_open.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(114, 38);
            this.btn_open.TabIndex = 0;
            this.btn_open.Text = "Load Input File";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // progBarMainForm
            // 
            this.progBarMainForm.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.progBarMainForm.Location = new System.Drawing.Point(12, 318);
            this.progBarMainForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progBarMainForm.Maximum = 0;
            this.progBarMainForm.Name = "progBarMainForm";
            this.progBarMainForm.Size = new System.Drawing.Size(357, 22);
            this.progBarMainForm.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.cntxMenu;
            this.dataGridView1.Location = new System.Drawing.Point(11, 84);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(711, 202);
            this.dataGridView1.TabIndex = 6;
            // 
            // cntxMenu
            // 
            this.cntxMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cntxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.exportViewToolStripMenuItem1,
            this.importViewToolStripMenuItem,
            this.toolStripMenuItem3,
            this.generatePasswordToolStripMenuItem});
            this.cntxMenu.Name = "contextMenuStrip1";
            this.cntxMenu.Size = new System.Drawing.Size(175, 104);
            // 
            // refreshToolStripMenuItem1
            // 
            this.refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
            this.refreshToolStripMenuItem1.Size = new System.Drawing.Size(174, 22);
            this.refreshToolStripMenuItem1.Text = "Refresh";
            this.refreshToolStripMenuItem1.Click += new System.EventHandler(this.refreshToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(171, 6);
            // 
            // exportViewToolStripMenuItem1
            // 
            this.exportViewToolStripMenuItem1.Name = "exportViewToolStripMenuItem1";
            this.exportViewToolStripMenuItem1.Size = new System.Drawing.Size(174, 22);
            this.exportViewToolStripMenuItem1.Text = "Export View";
            this.exportViewToolStripMenuItem1.Click += new System.EventHandler(this.exportViewToolStripMenuItem1_Click);
            // 
            // importViewToolStripMenuItem
            // 
            this.importViewToolStripMenuItem.Name = "importViewToolStripMenuItem";
            this.importViewToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.importViewToolStripMenuItem.Text = "Import View";
            this.importViewToolStripMenuItem.Click += new System.EventHandler(this.importViewToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(171, 6);
            // 
            // generatePasswordToolStripMenuItem
            // 
            this.generatePasswordToolStripMenuItem.Enabled = false;
            this.generatePasswordToolStripMenuItem.Name = "generatePasswordToolStripMenuItem";
            this.generatePasswordToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.generatePasswordToolStripMenuItem.Text = "Generate Password";
            this.generatePasswordToolStripMenuItem.Click += new System.EventHandler(this.generatePasswordToolStripMenuItem_Click);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // btn_process
            // 
            this.btn_process.Location = new System.Drawing.Point(724, 301);
            this.btn_process.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_process.Name = "btn_process";
            this.btn_process.Size = new System.Drawing.Size(132, 39);
            this.btn_process.TabIndex = 8;
            this.btn_process.Text = "Process All";
            this.btn_process.UseVisualStyleBackColor = true;
            this.btn_process.Click += new System.EventHandler(this.btn_process_Click);
            // 
            // txt_status
            // 
            this.txt_status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_status.Enabled = false;
            this.txt_status.Location = new System.Drawing.Point(12, 292);
            this.txt_status.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_status.Name = "txt_status";
            this.txt_status.ReadOnly = true;
            this.txt_status.Size = new System.Drawing.Size(580, 13);
            this.txt_status.TabIndex = 9;
            // 
            // cb_inputFile
            // 
            this.cb_inputFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_inputFile.Enabled = false;
            this.cb_inputFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb_inputFile.Items.AddRange(new object[] {
            "ALL USER DATA",
            "ESL USER DATA",
            "SIP USER DATA",
            "PASSWORD FROM N2000",
            "PASSWORD FROM CSV",
            "ESL MGW DATA"});
            this.cb_inputFile.Location = new System.Drawing.Point(260, 53);
            this.cb_inputFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_inputFile.Name = "cb_inputFile";
            this.cb_inputFile.Size = new System.Drawing.Size(153, 21);
            this.cb_inputFile.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select input file type";
            // 
            // txt_subNum
            // 
            this.txt_subNum.AcceptsReturn = true;
            this.txt_subNum.Enabled = false;
            this.txt_subNum.Location = new System.Drawing.Point(589, 56);
            this.txt_subNum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_subNum.Name = "txt_subNum";
            this.txt_subNum.Size = new System.Drawing.Size(133, 20);
            this.txt_subNum.TabIndex = 12;
            this.txt_subNum.WordWrap = false;
            this.txt_subNum.TextChanged += new System.EventHandler(this.txt_subNum_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(594, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Search for Subscriber";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Filter Type";
            // 
            // cb_fileType
            // 
            this.cb_fileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_fileType.Items.AddRange(new object[] {
            "SIP USER",
            "ESL"});
            this.cb_fileType.Location = new System.Drawing.Point(131, 53);
            this.cb_fileType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_fileType.Name = "cb_fileType";
            this.cb_fileType.Size = new System.Drawing.Size(109, 21);
            this.cb_fileType.TabIndex = 17;
            this.cb_fileType.SelectedIndexChanged += new System.EventHandler(this.cb_fileType_SelectedIndexChanged);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // manualProvisioningToolStripMenuItem
            // 
            this.manualProvisioningToolStripMenuItem.Name = "manualProvisioningToolStripMenuItem";
            this.manualProvisioningToolStripMenuItem.Size = new System.Drawing.Size(155, 24);
            this.manualProvisioningToolStripMenuItem.Text = "Manual Provisioning";
            this.manualProvisioningToolStripMenuItem.Click += new System.EventHandler(this.manualProvisioningToolStripMenuItem_Click);
            // 
            // switchViewToolStripMenuItem
            // 
            this.switchViewToolStripMenuItem.Name = "switchViewToolStripMenuItem";
            this.switchViewToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(45, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(25, 24);
            this.toolStripMenuItem1.Text = " ";
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem1.Text = "Settings";
            // 
            // settingsToolStripMenuItem2
            // 
            this.settingsToolStripMenuItem2.Name = "settingsToolStripMenuItem2";
            this.settingsToolStripMenuItem2.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem2.Text = "Settings";
            this.settingsToolStripMenuItem2.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // manualProvisioningToolStripMenuItem1
            // 
            this.manualProvisioningToolStripMenuItem1.Name = "manualProvisioningToolStripMenuItem1";
            this.manualProvisioningToolStripMenuItem1.Size = new System.Drawing.Size(155, 24);
            this.manualProvisioningToolStripMenuItem1.Text = "Manual Provisioning";
            // 
            // switchViewToolStripMenuItem1
            // 
            this.switchViewToolStripMenuItem1.Name = "switchViewToolStripMenuItem1";
            this.switchViewToolStripMenuItem1.Size = new System.Drawing.Size(100, 24);
            this.switchViewToolStripMenuItem1.Text = "Switch View";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem1.Text = "About";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(45, 24);
            this.exitToolStripMenuItem1.Text = "Exit";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem3,
            this.manualProvisioningToolStripMenuItem2,
            this.stopProcessingToolStripMenuItem,
            this.aboutToolStripMenuItem2,
            this.exitToolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(868, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem3
            // 
            this.settingsToolStripMenuItem3.AutoToolTip = true;
            this.settingsToolStripMenuItem3.Name = "settingsToolStripMenuItem3";
            this.settingsToolStripMenuItem3.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem3.Text = "Settings";
            this.settingsToolStripMenuItem3.ToolTipText = "Navigate to Settings Menu";
            this.settingsToolStripMenuItem3.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // manualProvisioningToolStripMenuItem2
            // 
            this.manualProvisioningToolStripMenuItem2.Name = "manualProvisioningToolStripMenuItem2";
            this.manualProvisioningToolStripMenuItem2.Size = new System.Drawing.Size(128, 20);
            this.manualProvisioningToolStripMenuItem2.Text = "Manual Provisioning";
            this.manualProvisioningToolStripMenuItem2.Click += new System.EventHandler(this.manualProvisioningToolStripMenuItem_Click);
            // 
            // stopProcessingToolStripMenuItem
            // 
            this.stopProcessingToolStripMenuItem.Enabled = false;
            this.stopProcessingToolStripMenuItem.Name = "stopProcessingToolStripMenuItem";
            this.stopProcessingToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.stopProcessingToolStripMenuItem.Text = "Pause Processing";
            this.stopProcessingToolStripMenuItem.Click += new System.EventHandler(this.stopProcessingToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem2
            // 
            this.aboutToolStripMenuItem2.Name = "aboutToolStripMenuItem2";
            this.aboutToolStripMenuItem2.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem2.Text = "About";
            this.aboutToolStripMenuItem2.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem2
            // 
            this.exitToolStripMenuItem2.Name = "exitToolStripMenuItem2";
            this.exitToolStripMenuItem2.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem2.Text = "Exit";
            this.exitToolStripMenuItem2.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(576, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Execution Mode";
            // 
            // cbExecutionMode
            // 
            this.cbExecutionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExecutionMode.Enabled = false;
            this.cbExecutionMode.Items.AddRange(new object[] {
            "PROMPT ON ERROR",
            "IGNORE AND CONTINUE",
            "TRY FIX"});
            this.cbExecutionMode.Location = new System.Drawing.Point(553, 318);
            this.cbExecutionMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbExecutionMode.Name = "cbExecutionMode";
            this.cbExecutionMode.Size = new System.Drawing.Size(135, 21);
            this.cbExecutionMode.TabIndex = 23;
            this.cbExecutionMode.SelectedIndexChanged += new System.EventHandler(this.cbExecutionMode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(738, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "From";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(749, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "To";
            // 
            // numFrom
            // 
            this.numFrom.Enabled = false;
            this.numFrom.Location = new System.Drawing.Point(769, 80);
            this.numFrom.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFrom.Name = "numFrom";
            this.numFrom.Size = new System.Drawing.Size(87, 20);
            this.numFrom.TabIndex = 27;
            this.numFrom.ThousandsSeparator = true;
            this.numFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFrom.ValueChanged += new System.EventHandler(this.numFrom_ValueChanged);
            // 
            // numTo
            // 
            this.numTo.Enabled = false;
            this.numTo.Location = new System.Drawing.Point(769, 106);
            this.numTo.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numTo.Name = "numTo";
            this.numTo.Size = new System.Drawing.Size(87, 20);
            this.numTo.TabIndex = 28;
            this.numTo.ThousandsSeparator = true;
            this.numTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTo.ValueChanged += new System.EventHandler(this.numTo_ValueChanged);
            // 
            // txt_eta
            // 
            this.txt_eta.BackColor = System.Drawing.SystemColors.Control;
            this.txt_eta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_eta.Location = new System.Drawing.Point(741, 171);
            this.txt_eta.Multiline = true;
            this.txt_eta.Name = "txt_eta";
            this.txt_eta.Size = new System.Drawing.Size(115, 80);
            this.txt_eta.TabIndex = 29;
            this.txt_eta.TextChanged += new System.EventHandler(this.txt_eta_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(417, 304);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "ESL Creation Mode";
            // 
            // cbEslMode
            // 
            this.cbEslMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEslMode.Enabled = false;
            this.cbEslMode.Items.AddRange(new object[] {
            "DEVICE ONLY",
            "SUBSCRIBERS ONLY"});
            this.cbEslMode.Location = new System.Drawing.Point(400, 319);
            this.cbEslMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbEslMode.Name = "cbEslMode";
            this.cbEslMode.Size = new System.Drawing.Size(135, 21);
            this.cbEslMode.TabIndex = 30;
            this.cbEslMode.SelectedIndexChanged += new System.EventHandler(this.cbEslMode_SelectedIndexChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 349);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbEslMode);
            this.Controls.Add(this.txt_eta);
            this.Controls.Add(this.numTo);
            this.Controls.Add(this.numFrom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbExecutionMode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_fileType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_subNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_inputFile);
            this.Controls.Add(this.txt_status);
            this.Controls.Add(this.btn_process);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.progBarMainForm);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "IMS Provisioning App";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.cntxMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;        
        private System.Windows.Forms.ProgressBar progBarMainForm;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.Button btn_process;
        private System.Windows.Forms.TextBox txt_status;
        private System.Windows.Forms.ComboBox cb_inputFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_subNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_fileType;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualProvisioningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem manualProvisioningToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem switchViewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem manualProvisioningToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ContextMenuStrip cntxMenu;
        private System.Windows.Forms.ToolStripMenuItem exportViewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbExecutionMode;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stopProcessingToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numFrom;
        private System.Windows.Forms.NumericUpDown numTo;
        private System.Windows.Forms.TextBox txt_eta;
        private System.Windows.Forms.ToolStripMenuItem importViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem generatePasswordToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbEslMode;
    }
}

