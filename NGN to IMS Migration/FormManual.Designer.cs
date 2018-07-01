namespace NGN_to_IMS_Migration
{
    partial class FormManual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormManual));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_remove = new System.Windows.Forms.Button();
            this.cbRemove = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_create = new System.Windows.Forms.Button();
            this.cbCreate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_printout = new System.Windows.Forms.Button();
            this.cbPrintout = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_submit = new System.Windows.Forms.Button();
            this.cb_pilot = new System.Windows.Forms.CheckBox();
            this.cbServices = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btn_rmv_hcapscscf = new System.Windows.Forms.Button();
            this.btn_add_hcapscscf = new System.Windows.Forms.Button();
            this.btn_add_hsifc = new System.Windows.Forms.Button();
            this.btn_rmv_hsifc = new System.Windows.Forms.Button();
            this.btm_add_dnaptrrec = new System.Windows.Forms.Button();
            this.btn_add_msr = new System.Windows.Forms.Button();
            this.rmv_msr = new System.Windows.Forms.Button();
            this.btn_add_hhsssub = new System.Windows.Forms.Button();
            this.btn_rmv_hhsssub = new System.Windows.Forms.Button();
            this.rmv_dnaptrrec = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyCellContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bulkModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sIPUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sIPTrunkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eSLUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sTPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.bulkCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.manualSoapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopBulkExecutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backToMainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_soapinput = new System.Windows.Forms.RichTextBox();
            this.btn_execute = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewOutput = new System.Windows.Forms.DataGridView();
            this.Command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewInput = new System.Windows.Forms.DataGridView();
            this.Parameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuInput = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.generateRandomPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_detail_output = new System.Windows.Forms.RichTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.lbl_manual = new System.Windows.Forms.Label();
            this.btn_bulk = new System.Windows.Forms.Button();
            this.cb_bulk = new System.Windows.Forms.ComboBox();
            this.lbl_bulk = new System.Windows.Forms.Label();
            this.txt_status = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).BeginInit();
            this.contextMenuInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_remove);
            this.groupBox1.Controls.Add(this.cbRemove);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_create);
            this.groupBox1.Controls.Add(this.cbCreate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_printout);
            this.groupBox1.Controls.Add(this.cbPrintout);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.btn_submit);
            this.groupBox1.Controls.Add(this.cb_pilot);
            this.groupBox1.Controls.Add(this.cbServices);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(9, 27);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(449, 246);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Commands";
            // 
            // btn_remove
            // 
            this.btn_remove.Enabled = false;
            this.btn_remove.Location = new System.Drawing.Point(28, 171);
            this.btn_remove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(161, 37);
            this.btn_remove.TabIndex = 72;
            this.btn_remove.Text = "Submit";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // cbRemove
            // 
            this.cbRemove.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbRemove.FormattingEnabled = true;
            this.cbRemove.Items.AddRange(new object[] {
            "RMV HHSSSUB",
            "RMV HSIFC",
            "RMV DNAPTREC",
            "RMV MSR",
            "RMV HCAPSCSCF",
            "RMV AGCF_MGW",
            "RMV AGCF_ASBR"});
            this.cbRemove.Location = new System.Drawing.Point(28, 146);
            this.cbRemove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbRemove.Name = "cbRemove";
            this.cbRemove.Size = new System.Drawing.Size(162, 21);
            this.cbRemove.TabIndex = 71;
            this.cbRemove.SelectedIndexChanged += new System.EventHandler(this.cbRemove_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 70;
            this.label3.Text = "Remove Commands";
            // 
            // btn_create
            // 
            this.btn_create.Enabled = false;
            this.btn_create.Location = new System.Drawing.Point(27, 65);
            this.btn_create.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(161, 37);
            this.btn_create.TabIndex = 69;
            this.btn_create.Text = "Submit";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // cbCreate
            // 
            this.cbCreate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCreate.FormattingEnabled = true;
            this.cbCreate.Items.AddRange(new object[] {
            "ADD HHSSSUB",
            "ADD HSIFC",
            "ADD DNAPTREC",
            "ADD MSR",
            "ADD HCAPSCSCF",
            "ADD AGCF_MGW",
            "ADD AGCF_ASBR"});
            this.cbCreate.Location = new System.Drawing.Point(27, 40);
            this.cbCreate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCreate.Name = "cbCreate";
            this.cbCreate.Size = new System.Drawing.Size(162, 21);
            this.cbCreate.TabIndex = 68;
            this.cbCreate.SelectedIndexChanged += new System.EventHandler(this.cbCreate_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "Create Commands";
            // 
            // btn_printout
            // 
            this.btn_printout.Enabled = false;
            this.btn_printout.Location = new System.Drawing.Point(273, 171);
            this.btn_printout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_printout.Name = "btn_printout";
            this.btn_printout.Size = new System.Drawing.Size(161, 37);
            this.btn_printout.TabIndex = 62;
            this.btn_printout.Text = "Submit";
            this.btn_printout.UseVisualStyleBackColor = true;
            this.btn_printout.Click += new System.EventHandler(this.btn_printout_Click);
            // 
            // cbPrintout
            // 
            this.cbPrintout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrintout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPrintout.FormattingEnabled = true;
            this.cbPrintout.Items.AddRange(new object[] {
            "LST HHSSSUB",
            "LST HSIFC",
            "LST DNAPTRREC",
            "LST MSR",
            "LST HCAPSCSCF",
            "NPDB PRINTOUT (STP)",
            "LST SUB (HLR)",
            "LST AGCF_MGW",
            "LST AGCF_ASBR"});
            this.cbPrintout.Location = new System.Drawing.Point(273, 146);
            this.cbPrintout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbPrintout.Name = "cbPrintout";
            this.cbPrintout.Size = new System.Drawing.Size(162, 21);
            this.cbPrintout.TabIndex = 61;
            this.cbPrintout.SelectedIndexChanged += new System.EventHandler(this.cb_printout_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(270, 128);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 13);
            this.label13.TabIndex = 60;
            this.label13.Text = "Printout Commands";
            // 
            // btn_submit
            // 
            this.btn_submit.Enabled = false;
            this.btn_submit.Location = new System.Drawing.Point(273, 65);
            this.btn_submit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(161, 37);
            this.btn_submit.TabIndex = 59;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // cb_pilot
            // 
            this.cb_pilot.AutoSize = true;
            this.cb_pilot.Location = new System.Drawing.Point(27, 218);
            this.cb_pilot.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_pilot.Name = "cb_pilot";
            this.cb_pilot.Size = new System.Drawing.Size(86, 17);
            this.cb_pilot.TabIndex = 56;
            this.cb_pilot.Text = "Pilot Number";
            this.toolTip1.SetToolTip(this.cb_pilot, "Pilot number?");
            this.cb_pilot.UseVisualStyleBackColor = true;
            this.cb_pilot.Visible = false;
            // 
            // cbServices
            // 
            this.cbServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbServices.FormattingEnabled = true;
            this.cbServices.Items.AddRange(new object[] {
            "OUTGOING CALLS BLOCKING",
            "OUTGOING CALLS UNBLOCKING",
            "VOICE SUSPENSION",
            "VOICE RESUME",
            "IDD ALLOW",
            "IDD PROHIBIT"});
            this.cbServices.Location = new System.Drawing.Point(273, 40);
            this.cbServices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbServices.Name = "cbServices";
            this.cbServices.Size = new System.Drawing.Size(162, 21);
            this.cbServices.TabIndex = 58;
            this.cbServices.SelectedIndexChanged += new System.EventHandler(this.cb_service_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(270, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 13);
            this.label12.TabIndex = 57;
            this.label12.Text = "Other Services";
            // 
            // btn_rmv_hcapscscf
            // 
            this.btn_rmv_hcapscscf.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_rmv_hcapscscf.Location = new System.Drawing.Point(952, 96);
            this.btn_rmv_hcapscscf.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_rmv_hcapscscf.Name = "btn_rmv_hcapscscf";
            this.btn_rmv_hcapscscf.Size = new System.Drawing.Size(116, 37);
            this.btn_rmv_hcapscscf.TabIndex = 41;
            this.btn_rmv_hcapscscf.Text = "RMV HCAPSCSCF";
            this.btn_rmv_hcapscscf.UseVisualStyleBackColor = true;
            this.btn_rmv_hcapscscf.Visible = false;
            this.btn_rmv_hcapscscf.Click += new System.EventHandler(this.btn_rmv_hcapscscf_Click);
            // 
            // btn_add_hcapscscf
            // 
            this.btn_add_hcapscscf.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_add_hcapscscf.Location = new System.Drawing.Point(952, 96);
            this.btn_add_hcapscscf.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_add_hcapscscf.Name = "btn_add_hcapscscf";
            this.btn_add_hcapscscf.Size = new System.Drawing.Size(116, 37);
            this.btn_add_hcapscscf.TabIndex = 31;
            this.btn_add_hcapscscf.Text = "ADD HCAPSCSCF";
            this.btn_add_hcapscscf.UseVisualStyleBackColor = true;
            this.btn_add_hcapscscf.Visible = false;
            this.btn_add_hcapscscf.Click += new System.EventHandler(this.btn_add_hcapscscf_Click);
            // 
            // btn_add_hsifc
            // 
            this.btn_add_hsifc.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_add_hsifc.Location = new System.Drawing.Point(952, 94);
            this.btn_add_hsifc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_add_hsifc.Name = "btn_add_hsifc";
            this.btn_add_hsifc.Size = new System.Drawing.Size(116, 37);
            this.btn_add_hsifc.TabIndex = 32;
            this.btn_add_hsifc.Text = "ADD HSIFC";
            this.btn_add_hsifc.UseVisualStyleBackColor = true;
            this.btn_add_hsifc.Visible = false;
            this.btn_add_hsifc.Click += new System.EventHandler(this.btn_add_hsifc_Click);
            // 
            // btn_rmv_hsifc
            // 
            this.btn_rmv_hsifc.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_rmv_hsifc.Location = new System.Drawing.Point(952, 92);
            this.btn_rmv_hsifc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_rmv_hsifc.Name = "btn_rmv_hsifc";
            this.btn_rmv_hsifc.Size = new System.Drawing.Size(116, 37);
            this.btn_rmv_hsifc.TabIndex = 42;
            this.btn_rmv_hsifc.Text = "RMV HSIFC";
            this.btn_rmv_hsifc.UseVisualStyleBackColor = true;
            this.btn_rmv_hsifc.Visible = false;
            this.btn_rmv_hsifc.Click += new System.EventHandler(this.btn_rmv_hsifc_Click);
            // 
            // btm_add_dnaptrrec
            // 
            this.btm_add_dnaptrrec.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btm_add_dnaptrrec.Location = new System.Drawing.Point(952, 96);
            this.btm_add_dnaptrrec.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btm_add_dnaptrrec.Name = "btm_add_dnaptrrec";
            this.btm_add_dnaptrrec.Size = new System.Drawing.Size(116, 37);
            this.btm_add_dnaptrrec.TabIndex = 33;
            this.btm_add_dnaptrrec.Text = "ADD DNAPTRREC";
            this.btm_add_dnaptrrec.UseVisualStyleBackColor = true;
            this.btm_add_dnaptrrec.Visible = false;
            this.btm_add_dnaptrrec.Click += new System.EventHandler(this.btm_add_dnaptrrec_Click);
            // 
            // btn_add_msr
            // 
            this.btn_add_msr.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_add_msr.Location = new System.Drawing.Point(952, 96);
            this.btn_add_msr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_add_msr.Name = "btn_add_msr";
            this.btn_add_msr.Size = new System.Drawing.Size(116, 37);
            this.btn_add_msr.TabIndex = 34;
            this.btn_add_msr.Text = "ADD MSR";
            this.btn_add_msr.UseVisualStyleBackColor = true;
            this.btn_add_msr.Visible = false;
            this.btn_add_msr.Click += new System.EventHandler(this.btn_add_msr_Click);
            // 
            // rmv_msr
            // 
            this.rmv_msr.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.rmv_msr.Location = new System.Drawing.Point(952, 106);
            this.rmv_msr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rmv_msr.Name = "rmv_msr";
            this.rmv_msr.Size = new System.Drawing.Size(116, 37);
            this.rmv_msr.TabIndex = 44;
            this.rmv_msr.Text = "RMV MSR";
            this.rmv_msr.UseVisualStyleBackColor = true;
            this.rmv_msr.Visible = false;
            this.rmv_msr.Click += new System.EventHandler(this.rmv_msr_Click);
            // 
            // btn_add_hhsssub
            // 
            this.btn_add_hhsssub.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_add_hhsssub.Location = new System.Drawing.Point(952, 96);
            this.btn_add_hhsssub.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_add_hhsssub.Name = "btn_add_hhsssub";
            this.btn_add_hhsssub.Size = new System.Drawing.Size(116, 37);
            this.btn_add_hhsssub.TabIndex = 30;
            this.btn_add_hhsssub.Text = "ADD HHSSSUB";
            this.btn_add_hhsssub.UseVisualStyleBackColor = true;
            this.btn_add_hhsssub.Visible = false;
            this.btn_add_hhsssub.Click += new System.EventHandler(this.btn_add_hhsssub_Click);
            // 
            // btn_rmv_hhsssub
            // 
            this.btn_rmv_hhsssub.Location = new System.Drawing.Point(952, 96);
            this.btn_rmv_hhsssub.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_rmv_hhsssub.Name = "btn_rmv_hhsssub";
            this.btn_rmv_hhsssub.Size = new System.Drawing.Size(116, 37);
            this.btn_rmv_hhsssub.TabIndex = 40;
            this.btn_rmv_hhsssub.Text = "RMV HHSSSUB";
            this.btn_rmv_hhsssub.UseVisualStyleBackColor = true;
            this.btn_rmv_hhsssub.Visible = false;
            this.btn_rmv_hhsssub.Click += new System.EventHandler(this.btn_rmv_hhsssub_Click);
            // 
            // rmv_dnaptrrec
            // 
            this.rmv_dnaptrrec.Location = new System.Drawing.Point(952, 96);
            this.rmv_dnaptrrec.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rmv_dnaptrrec.Name = "rmv_dnaptrrec";
            this.rmv_dnaptrrec.Size = new System.Drawing.Size(116, 37);
            this.rmv_dnaptrrec.TabIndex = 43;
            this.rmv_dnaptrrec.Text = "RMV DNAPTRREC";
            this.rmv_dnaptrrec.UseVisualStyleBackColor = true;
            this.rmv_dnaptrrec.Visible = false;
            this.rmv_dnaptrrec.Click += new System.EventHandler(this.rmv_dnaptrrec_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyCellContentToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 26);
            // 
            // copyCellContentToolStripMenuItem
            // 
            this.copyCellContentToolStripMenuItem.Name = "copyCellContentToolStripMenuItem";
            this.copyCellContentToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyCellContentToolStripMenuItem.Text = "Copy";
            this.copyCellContentToolStripMenuItem.Click += new System.EventHandler(this.copyCellContentToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(474, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Output";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bulkModeToolStripMenuItem,
            this.settingsToolStripMenuItem1,
            this.stopBulkExecutionToolStripMenuItem,
            this.backToMainMenuToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1209, 24);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bulkModeToolStripMenuItem
            // 
            this.bulkModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sIPUserToolStripMenuItem,
            this.sIPTrunkToolStripMenuItem,
            this.eSLUserToolStripMenuItem,
            this.sTPToolStripMenuItem,
            this.toolStripMenuItem1,
            this.bulkCommandToolStripMenuItem,
            this.toolStripMenuItem2,
            this.manualSoapToolStripMenuItem});
            this.bulkModeToolStripMenuItem.Name = "bulkModeToolStripMenuItem";
            this.bulkModeToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.bulkModeToolStripMenuItem.Text = "Mode";
            // 
            // sIPUserToolStripMenuItem
            // 
            this.sIPUserToolStripMenuItem.Name = "sIPUserToolStripMenuItem";
            this.sIPUserToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.sIPUserToolStripMenuItem.Text = "SIP User";
            this.sIPUserToolStripMenuItem.Click += new System.EventHandler(this.sIPUserToolStripMenuItem_Click);
            // 
            // sIPTrunkToolStripMenuItem
            // 
            this.sIPTrunkToolStripMenuItem.Name = "sIPTrunkToolStripMenuItem";
            this.sIPTrunkToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.sIPTrunkToolStripMenuItem.Text = "SIP Trunk";
            this.sIPTrunkToolStripMenuItem.Click += new System.EventHandler(this.sIPTrunkToolStripMenuItem_Click);
            // 
            // eSLUserToolStripMenuItem
            // 
            this.eSLUserToolStripMenuItem.Name = "eSLUserToolStripMenuItem";
            this.eSLUserToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.eSLUserToolStripMenuItem.Text = "ESL User";
            this.eSLUserToolStripMenuItem.Click += new System.EventHandler(this.eSLUserToolStripMenuItem_Click);
            // 
            // sTPToolStripMenuItem
            // 
            this.sTPToolStripMenuItem.Name = "sTPToolStripMenuItem";
            this.sTPToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.sTPToolStripMenuItem.Text = "STP";
            this.sTPToolStripMenuItem.Click += new System.EventHandler(this.sTPToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(188, 6);
            // 
            // bulkCommandToolStripMenuItem
            // 
            this.bulkCommandToolStripMenuItem.Name = "bulkCommandToolStripMenuItem";
            this.bulkCommandToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.bulkCommandToolStripMenuItem.Text = "Bulk Command Mode";
            this.bulkCommandToolStripMenuItem.Click += new System.EventHandler(this.bulkCommandToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(188, 6);
            // 
            // manualSoapToolStripMenuItem
            // 
            this.manualSoapToolStripMenuItem.Name = "manualSoapToolStripMenuItem";
            this.manualSoapToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.manualSoapToolStripMenuItem.Text = "Manual Soap";
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem1.Text = "Settings";
            this.settingsToolStripMenuItem1.Click += new System.EventHandler(this.settingsToolStripMenuItem1_Click);
            // 
            // stopBulkExecutionToolStripMenuItem
            // 
            this.stopBulkExecutionToolStripMenuItem.Enabled = false;
            this.stopBulkExecutionToolStripMenuItem.Name = "stopBulkExecutionToolStripMenuItem";
            this.stopBulkExecutionToolStripMenuItem.Size = new System.Drawing.Size(123, 20);
            this.stopBulkExecutionToolStripMenuItem.Text = "Stop Bulk Execution";
            // 
            // backToMainMenuToolStripMenuItem
            // 
            this.backToMainMenuToolStripMenuItem.Name = "backToMainMenuToolStripMenuItem";
            this.backToMainMenuToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.backToMainMenuToolStripMenuItem.Text = "Back";
            this.backToMainMenuToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.exitToolStripMenuItem.Text = "Back / Cancel";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // txt_soapinput
            // 
            this.txt_soapinput.Location = new System.Drawing.Point(9, 315);
            this.txt_soapinput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_soapinput.Name = "txt_soapinput";
            this.txt_soapinput.Size = new System.Drawing.Size(264, 88);
            this.txt_soapinput.TabIndex = 70;
            this.txt_soapinput.Text = "";
            this.toolTip1.SetToolTip(this.txt_soapinput, "Place here the xml input of the command you wish to fire. You\'ll need ");
            // 
            // btn_execute
            // 
            this.btn_execute.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_execute.Location = new System.Drawing.Point(9, 407);
            this.btn_execute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_execute.Name = "btn_execute";
            this.btn_execute.Size = new System.Drawing.Size(172, 37);
            this.btn_execute.TabIndex = 71;
            this.btn_execute.Text = "EXECUTE SOAP COMMAND";
            this.btn_execute.UseVisualStyleBackColor = true;
            this.btn_execute.Click += new System.EventHandler(this.btn_execute_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 15000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 20;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // dataGridViewOutput
            // 
            this.dataGridViewOutput.AllowUserToAddRows = false;
            this.dataGridViewOutput.AllowUserToDeleteRows = false;
            this.dataGridViewOutput.AllowUserToOrderColumns = true;
            this.dataGridViewOutput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOutput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Command,
            this.Result,
            this.Code,
            this.Comments});
            this.dataGridViewOutput.Location = new System.Drawing.Point(474, 261);
            this.dataGridViewOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewOutput.MultiSelect = false;
            this.dataGridViewOutput.Name = "dataGridViewOutput";
            this.dataGridViewOutput.ReadOnly = true;
            this.dataGridViewOutput.RowTemplate.Height = 26;
            this.dataGridViewOutput.Size = new System.Drawing.Size(351, 181);
            this.dataGridViewOutput.TabIndex = 1008;
            this.toolTip1.SetToolTip(this.dataGridViewOutput, "Presents output for commands results.");
            this.dataGridViewOutput.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOutput_CellClick);
            this.dataGridViewOutput.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOutput_CellClick);
            this.dataGridViewOutput.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridViewOutput.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView1_Scroll);
            // 
            // Command
            // 
            this.Command.HeaderText = "Command";
            this.Command.Name = "Command";
            this.Command.ReadOnly = true;
            // 
            // Result
            // 
            this.Result.HeaderText = "Result";
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // Comments
            // 
            this.Comments.HeaderText = "Comments";
            this.Comments.Name = "Comments";
            this.Comments.ReadOnly = true;
            // 
            // dataGridViewInput
            // 
            this.dataGridViewInput.AllowUserToAddRows = false;
            this.dataGridViewInput.AllowUserToDeleteRows = false;
            this.dataGridViewInput.AllowUserToOrderColumns = true;
            this.dataGridViewInput.AllowUserToResizeRows = false;
            this.dataGridViewInput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Parameter,
            this.Value});
            this.dataGridViewInput.ContextMenuStrip = this.contextMenuInput;
            this.dataGridViewInput.Location = new System.Drawing.Point(474, 46);
            this.dataGridViewInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewInput.MultiSelect = false;
            this.dataGridViewInput.Name = "dataGridViewInput";
            this.dataGridViewInput.RowTemplate.Height = 26;
            this.dataGridViewInput.Size = new System.Drawing.Size(351, 193);
            this.dataGridViewInput.TabIndex = 1009;
            this.toolTip1.SetToolTip(this.dataGridViewInput, "Fill in required input parameter\'s values.");
            this.dataGridViewInput.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewInput_CellEndEdit);
            // 
            // Parameter
            // 
            this.Parameter.HeaderText = "Parameter";
            this.Parameter.Name = "Parameter";
            this.Parameter.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // contextMenuInput
            // 
            this.contextMenuInput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateRandomPasswordToolStripMenuItem});
            this.contextMenuInput.Name = "contextMenuInput";
            this.contextMenuInput.Size = new System.Drawing.Size(223, 26);
            // 
            // generateRandomPasswordToolStripMenuItem
            // 
            this.generateRandomPasswordToolStripMenuItem.Name = "generateRandomPasswordToolStripMenuItem";
            this.generateRandomPasswordToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.generateRandomPasswordToolStripMenuItem.Text = "Generate Random Password";
            this.generateRandomPasswordToolStripMenuItem.Click += new System.EventHandler(this.generateRandomPasswordToolStripMenuItem_Click);
            // 
            // txt_detail_output
            // 
            this.txt_detail_output.AutoWordSelection = true;
            this.txt_detail_output.ContextMenuStrip = this.contextMenuStrip1;
            this.txt_detail_output.Location = new System.Drawing.Point(840, 46);
            this.txt_detail_output.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_detail_output.Name = "txt_detail_output";
            this.txt_detail_output.ReadOnly = true;
            this.txt_detail_output.Size = new System.Drawing.Size(360, 396);
            this.txt_detail_output.TabIndex = 1016;
            this.txt_detail_output.Text = "";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(474, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 13);
            this.label14.TabIndex = 1010;
            this.label14.Text = "Input";
            // 
            // lbl_manual
            // 
            this.lbl_manual.AutoSize = true;
            this.lbl_manual.Location = new System.Drawing.Point(10, 299);
            this.lbl_manual.Name = "lbl_manual";
            this.lbl_manual.Size = new System.Drawing.Size(100, 13);
            this.lbl_manual.TabIndex = 1011;
            this.lbl_manual.Text = "Manual SOAP Input";
            // 
            // btn_bulk
            // 
            this.btn_bulk.Enabled = false;
            this.btn_bulk.Location = new System.Drawing.Point(282, 351);
            this.btn_bulk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_bulk.Name = "btn_bulk";
            this.btn_bulk.Size = new System.Drawing.Size(161, 37);
            this.btn_bulk.TabIndex = 1014;
            this.btn_bulk.Text = "Submit";
            this.btn_bulk.UseVisualStyleBackColor = true;
            this.btn_bulk.Visible = false;
            this.btn_bulk.Click += new System.EventHandler(this.btn_bulk_Click);
            // 
            // cb_bulk
            // 
            this.cb_bulk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_bulk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_bulk.FormattingEnabled = true;
            this.cb_bulk.Items.AddRange(new object[] {
            "LST HHSSSUB",
            "LST HSIFC",
            "LST DNAPTRREC",
            "LST MSR",
            "LST HCAPSCSCF",
            "ADD HSIFC",
            "ADD DNAPTRREC",
            "RMV HHSSSUB",
            "RMV HSFIC",
            "RMV DNAPTRREC",
            "RMV MSR",
            "NPDB PRINTOUT (STP)",
            "RMV IMS"});
            this.cb_bulk.Location = new System.Drawing.Point(282, 325);
            this.cb_bulk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_bulk.Name = "cb_bulk";
            this.cb_bulk.Size = new System.Drawing.Size(162, 21);
            this.cb_bulk.TabIndex = 1013;
            this.cb_bulk.Visible = false;
            this.cb_bulk.SelectedIndexChanged += new System.EventHandler(this.cb_bulk_SelectedIndexChanged);
            // 
            // lbl_bulk
            // 
            this.lbl_bulk.AutoSize = true;
            this.lbl_bulk.Location = new System.Drawing.Point(279, 308);
            this.lbl_bulk.Name = "lbl_bulk";
            this.lbl_bulk.Size = new System.Drawing.Size(81, 13);
            this.lbl_bulk.TabIndex = 1012;
            this.lbl_bulk.Text = "Bulk Commands";
            this.lbl_bulk.Visible = false;
            // 
            // txt_status
            // 
            this.txt_status.Enabled = false;
            this.txt_status.Location = new System.Drawing.Point(282, 393);
            this.txt_status.Name = "txt_status";
            this.txt_status.ReadOnly = true;
            this.txt_status.Size = new System.Drawing.Size(162, 20);
            this.txt_status.TabIndex = 1015;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(840, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 1017;
            this.label4.Text = "Detailed Output";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // FormManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 453);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_detail_output);
            this.Controls.Add(this.txt_status);
            this.Controls.Add(this.btn_bulk);
            this.Controls.Add(this.cb_bulk);
            this.Controls.Add(this.lbl_bulk);
            this.Controls.Add(this.lbl_manual);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.dataGridViewInput);
            this.Controls.Add(this.dataGridViewOutput);
            this.Controls.Add(this.btn_execute);
            this.Controls.Add(this.txt_soapinput);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btn_rmv_hcapscscf);
            this.Controls.Add(this.btn_add_hhsssub);
            this.Controls.Add(this.btn_add_hcapscscf);
            this.Controls.Add(this.rmv_msr);
            this.Controls.Add(this.btn_add_hsifc);
            this.Controls.Add(this.rmv_dnaptrrec);
            this.Controls.Add(this.btm_add_dnaptrrec);
            this.Controls.Add(this.btn_add_msr);
            this.Controls.Add(this.btn_rmv_hsifc);
            this.Controls.Add(this.btn_rmv_hhsssub);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormManual";
            this.Text = "IMS Provisioning (Manual Mode)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManualProvision_FormClosed);
            this.Load += new System.EventHandler(this.FormManual_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).EndInit();
            this.contextMenuInput.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button rmv_dnaptrrec;
        private System.Windows.Forms.Button btn_rmv_hhsssub;
        private System.Windows.Forms.Button btn_add_hsifc;
        private System.Windows.Forms.Button btm_add_dnaptrrec;
        private System.Windows.Forms.Button btn_add_msr;
        private System.Windows.Forms.Button btn_add_hhsssub;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button rmv_msr;
        private System.Windows.Forms.RichTextBox txt_soapinput;
        private System.Windows.Forms.Button btn_execute;
        private System.Windows.Forms.ToolStripMenuItem backToMainMenuToolStripMenuItem;
        private System.Windows.Forms.Button btn_rmv_hsifc;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btn_rmv_hcapscscf;
        private System.Windows.Forms.Button btn_add_hcapscscf;
        private System.Windows.Forms.CheckBox cb_pilot;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.ComboBox cbServices;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripMenuItem bulkModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sIPUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sIPTrunkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bulkCommandToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem manualSoapToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn Command;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comments;
        private System.Windows.Forms.ToolStripMenuItem copyCellContentToolStripMenuItem;
        private System.Windows.Forms.Button btn_printout;
        private System.Windows.Forms.ComboBox cbPrintout;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dataGridViewInput;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.Label lbl_manual;
        private System.Windows.Forms.Button btn_bulk;
        private System.Windows.Forms.ComboBox cb_bulk;
        private System.Windows.Forms.Label lbl_bulk;
        private System.Windows.Forms.TextBox txt_status;
        private System.Windows.Forms.ToolStripMenuItem stopBulkExecutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eSLUserToolStripMenuItem;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.ComboBox cbRemove;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.ComboBox cbCreate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txt_detail_output;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem sTPToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuInput;
        private System.Windows.Forms.ToolStripMenuItem generateRandomPasswordToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
    }
}