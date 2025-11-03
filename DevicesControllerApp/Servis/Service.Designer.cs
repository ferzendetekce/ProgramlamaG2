namespace DevicesControllerApp.Servis
{
    partial class Service
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbPorts = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpServo = new System.Windows.Forms.GroupBox();
            this.flowServo = new System.Windows.Forms.FlowLayoutPanel();
            this.grpStep = new System.Windows.Forms.GroupBox();
            this.flowStep = new System.Windows.Forms.FlowLayoutPanel();
            this.grpRealtime = new System.Windows.Forms.GroupBox();
            this.lblLimitSwitch = new System.Windows.Forms.Label();
            this.lblLoadCells = new System.Windows.Forms.Label();
            this.btnHoming = new System.Windows.Forms.Button();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.grpLogs = new System.Windows.Forms.GroupBox();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnFilterLogs = new System.Windows.Forms.Button();
            this.cmbLogLevel = new System.Windows.Forms.ComboBox();
            this.dtpLogEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpLogStart = new System.Windows.Forms.DateTimePicker();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.lblLogSearch = new System.Windows.Forms.Label();
            this.txtLogSearch = new System.Windows.Forms.TextBox();
            this.btnLoadDeviceLogs = new System.Windows.Forms.Button();
            this.grpDiagnostics = new System.Windows.Forms.GroupBox();
            this.lblDiagnostics = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.cmbServiceLanguage = new System.Windows.Forms.ComboBox();
            this.lblLang = new System.Windows.Forms.Label();
            this.grpConnection.SuspendLayout();
            this.grpServo.SuspendLayout();
            this.grpStep.SuspendLayout();
            this.grpRealtime.SuspendLayout();
            this.grpLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.grpDiagnostics.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this.btnDisconnect);
            this.grpConnection.Controls.Add(this.btnConnect);
            this.grpConnection.Controls.Add(this.cmbPorts);
            this.grpConnection.Controls.Add(this.lblStatus);
            this.grpConnection.Location = new System.Drawing.Point(12, 12);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(400, 100);
            this.grpConnection.TabIndex = 0;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Cihaz Bağlantı";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(300, 58);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 3;
            this.btnDisconnect.Text = "Kes";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(219, 58);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Bağlan";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cmbPorts
            // 
            this.cmbPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPorts.FormattingEnabled = true;
            this.cmbPorts.Location = new System.Drawing.Point(17, 58);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(196, 21);
            this.cmbPorts.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(14, 28);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(120, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Durum: Bağlı Değil";
            // 
            // grpServo
            // 
            this.grpServo.Controls.Add(this.flowServo);
            this.grpServo.Location = new System.Drawing.Point(12, 118);
            this.grpServo.Name = "grpServo";
            this.grpServo.Size = new System.Drawing.Size(400, 250);
            this.grpServo.TabIndex = 1;
            this.grpServo.TabStop = false;
            this.grpServo.Text = "Servo Motorlar (7)";
            // 
            // flowServo
            // 
            this.flowServo.AutoScroll = true;
            this.flowServo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowServo.Location = new System.Drawing.Point(3, 16);
            this.flowServo.Name = "flowServo";
            this.flowServo.Size = new System.Drawing.Size(394, 231);
            this.flowServo.TabIndex = 0;
            // 
            // grpStep
            // 
            this.grpStep.Controls.Add(this.flowStep);
            this.grpStep.Location = new System.Drawing.Point(12, 374);
            this.grpStep.Name = "grpStep";
            this.grpStep.Size = new System.Drawing.Size(400, 250);
            this.grpStep.TabIndex = 2;
            this.grpStep.TabStop = false;
            this.grpStep.Text = "Step Motorlar (10)";
            // 
            // flowStep
            // 
            this.flowStep.AutoScroll = true;
            this.flowStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowStep.Location = new System.Drawing.Point(3, 16);
            this.flowStep.Name = "flowStep";
            this.flowStep.Size = new System.Drawing.Size(394, 231);
            this.flowStep.TabIndex = 0;
            // 
            // grpRealtime
            // 
            this.grpRealtime.Controls.Add(this.lblLimitSwitch);
            this.grpRealtime.Controls.Add(this.lblLoadCells);
            this.grpRealtime.Controls.Add(this.btnHoming);
            this.grpRealtime.Controls.Add(this.btnCalibrate);
            this.grpRealtime.Location = new System.Drawing.Point(418, 12);
            this.grpRealtime.Name = "grpRealtime";
            this.grpRealtime.Size = new System.Drawing.Size(450, 200);
            this.grpRealtime.TabIndex = 3;
            this.grpRealtime.TabStop = false;
            this.grpRealtime.Text = "Canlı Veriler / Hızlı İşlemler";
            // 
            // lblLimitSwitch
            // 
            this.lblLimitSwitch.AutoSize = true;
            this.lblLimitSwitch.Location = new System.Drawing.Point(16, 80);
            this.lblLimitSwitch.Name = "lblLimitSwitch";
            this.lblLimitSwitch.Size = new System.Drawing.Size(126, 13);
            this.lblLimitSwitch.TabIndex = 3;
            this.lblLimitSwitch.Text = "Limit Switch: - - - - - - -";
            // 
            // lblLoadCells
            // 
            this.lblLoadCells.AutoSize = true;
            this.lblLoadCells.Location = new System.Drawing.Point(16, 50);
            this.lblLoadCells.Name = "lblLoadCells";
            this.lblLoadCells.Size = new System.Drawing.Size(215, 13);
            this.lblLoadCells.TabIndex = 2;
            this.lblLoadCells.Text = "LoadCell: RH=0 LH=0 RT=0 LT=0 WB=0";
            // 
            // btnHoming
            // 
            this.btnHoming.Location = new System.Drawing.Point(340, 24);
            this.btnHoming.Name = "btnHoming";
            this.btnHoming.Size = new System.Drawing.Size(88, 23);
            this.btnHoming.TabIndex = 1;
            this.btnHoming.Text = "Homing";
            this.btnHoming.UseVisualStyleBackColor = true;
            this.btnHoming.Click += new System.EventHandler(this.btnHoming_Click);
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Location = new System.Drawing.Point(246, 24);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(88, 23);
            this.btnCalibrate.TabIndex = 0;
            this.btnCalibrate.Text = "Kalibrasyon";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // grpLogs
            // 
            this.grpLogs.Controls.Add(this.btnLoadDeviceLogs);
            this.grpLogs.Controls.Add(this.txtLogSearch);
            this.grpLogs.Controls.Add(this.lblLogSearch);
            this.grpLogs.Controls.Add(this.btnExportExcel);
            this.grpLogs.Controls.Add(this.btnExportCsv);
            this.grpLogs.Controls.Add(this.btnFilterLogs);
            this.grpLogs.Controls.Add(this.cmbLogLevel);
            this.grpLogs.Controls.Add(this.dtpLogEnd);
            this.grpLogs.Controls.Add(this.dtpLogStart);
            this.grpLogs.Controls.Add(this.dgvLogs);
            this.grpLogs.Location = new System.Drawing.Point(418, 218);
            this.grpLogs.Name = "grpLogs";
            this.grpLogs.Size = new System.Drawing.Size(850, 406);
            this.grpLogs.TabIndex = 4;
            this.grpLogs.TabStop = false;
            this.grpLogs.Text = "Log İnceleme";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(758, 19);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 23);
            this.btnExportExcel.TabIndex = 6;
            this.btnExportExcel.Text = "Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Location = new System.Drawing.Point(677, 19);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(75, 23);
            this.btnExportCsv.TabIndex = 5;
            this.btnExportCsv.Text = "CSV";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // btnFilterLogs
            // 
            this.btnFilterLogs.Location = new System.Drawing.Point(596, 19);
            this.btnFilterLogs.Name = "btnFilterLogs";
            this.btnFilterLogs.Size = new System.Drawing.Size(75, 23);
            this.btnFilterLogs.TabIndex = 4;
            this.btnFilterLogs.Text = "Filtrele";
            this.btnFilterLogs.UseVisualStyleBackColor = true;
            this.btnFilterLogs.Click += new System.EventHandler(this.btnFilterLogs_Click);
            // 
            // cmbLogLevel
            // 
            this.cmbLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogLevel.FormattingEnabled = true;
            this.cmbLogLevel.Items.AddRange(new object[] {
            "",
            "Info",
            "Warning",
            "Error",
            "Critical"});
            this.cmbLogLevel.Location = new System.Drawing.Point(469, 20);
            this.cmbLogLevel.Name = "cmbLogLevel";
            this.cmbLogLevel.Size = new System.Drawing.Size(121, 21);
            this.cmbLogLevel.TabIndex = 3;
            // 
            // dtpLogEnd
            // 
            this.dtpLogEnd.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpLogEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLogEnd.Location = new System.Drawing.Point(248, 20);
            this.dtpLogEnd.Name = "dtpLogEnd";
            this.dtpLogEnd.Size = new System.Drawing.Size(215, 20);
            this.dtpLogEnd.TabIndex = 2;
            // 
            // dtpLogStart
            // 
            this.dtpLogStart.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpLogStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLogStart.Location = new System.Drawing.Point(17, 20);
            this.dtpLogStart.Name = "dtpLogStart";
            this.dtpLogStart.Size = new System.Drawing.Size(215, 20);
            this.dtpLogStart.TabIndex = 1;
            // 
            // dgvLogs
            // 
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLogs.Location = new System.Drawing.Point(17, 56);
            this.dgvLogs.MultiSelect = false;
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.Size = new System.Drawing.Size(816, 334);
            this.dgvLogs.TabIndex = 0;
            this.dgvLogs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLogs_CellDoubleClick);
            // 
            // lblLogSearch
            // 
            this.lblLogSearch.AutoSize = true;
            this.lblLogSearch.Location = new System.Drawing.Point(17, 41);
            this.lblLogSearch.Name = "lblLogSearch";
            this.lblLogSearch.Size = new System.Drawing.Size(58, 13);
            this.lblLogSearch.TabIndex = 7;
            this.lblLogSearch.Text = "Ara (metin)";
            // 
            // txtLogSearch
            // 
            this.txtLogSearch.Location = new System.Drawing.Point(81, 38);
            this.txtLogSearch.Name = "txtLogSearch";
            this.txtLogSearch.Size = new System.Drawing.Size(381, 20);
            this.txtLogSearch.TabIndex = 8;
            this.txtLogSearch.TextChanged += new System.EventHandler(this.txtLogSearch_TextChanged);
            // 
            // btnLoadDeviceLogs
            // 
            this.btnLoadDeviceLogs.Location = new System.Drawing.Point(596, 44);
            this.btnLoadDeviceLogs.Name = "btnLoadDeviceLogs";
            this.btnLoadDeviceLogs.Size = new System.Drawing.Size(237, 23);
            this.btnLoadDeviceLogs.TabIndex = 9;
            this.btnLoadDeviceLogs.Text = "Cihaz Durum Loglarını Yükle";
            this.btnLoadDeviceLogs.UseVisualStyleBackColor = true;
            this.btnLoadDeviceLogs.Click += new System.EventHandler(this.btnLoadDeviceLogs_Click);
            // 
            // grpDiagnostics
            // 
            this.grpDiagnostics.Controls.Add(this.lblLang);
            this.grpDiagnostics.Controls.Add(this.cmbServiceLanguage);
            this.grpDiagnostics.Controls.Add(this.lblStats);
            this.grpDiagnostics.Controls.Add(this.lblDiagnostics);
            this.grpDiagnostics.Controls.Add(this.lstRecentErrors);
            this.grpDiagnostics.Location = new System.Drawing.Point(874, 12);
            this.grpDiagnostics.Name = "grpDiagnostics";
            this.grpDiagnostics.Size = new System.Drawing.Size(394, 200);
            this.grpDiagnostics.TabIndex = 5;
            this.grpDiagnostics.TabStop = false;
            this.grpDiagnostics.Text = "Diagnostik";
            // 
            // lblDiagnostics
            // 
            this.lblDiagnostics.AutoSize = true;
            this.lblDiagnostics.Location = new System.Drawing.Point(16, 28);
            this.lblDiagnostics.Name = "lblDiagnostics";
            this.lblDiagnostics.Size = new System.Drawing.Size(148, 13);
            this.lblDiagnostics.TabIndex = 0;
            this.lblDiagnostics.Text = "Cihaz sağlık özeti hazırlanıyor";
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.Location = new System.Drawing.Point(16, 46);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(108, 13);
            this.lblStats.TabIndex = 2;
            this.lblStats.Text = "İstatistikler hazırlanıyor";
            // 
            // cmbServiceLanguage
            // 
            this.cmbServiceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceLanguage.FormattingEnabled = true;
            this.cmbServiceLanguage.Items.AddRange(new object[] {
            "TR",
            "EN"});
            this.cmbServiceLanguage.Location = new System.Drawing.Point(305, 19);
            this.cmbServiceLanguage.Name = "cmbServiceLanguage";
            this.cmbServiceLanguage.Size = new System.Drawing.Size(69, 21);
            this.cmbServiceLanguage.TabIndex = 3;
            this.cmbServiceLanguage.SelectedIndexChanged += new System.EventHandler(this.cmbServiceLanguage_SelectedIndexChanged);
            // 
            // lblLang
            // 
            this.lblLang.AutoSize = true;
            this.lblLang.Location = new System.Drawing.Point(260, 22);
            this.lblLang.Name = "lblLang";
            this.lblLang.Size = new System.Drawing.Size(31, 13);
            this.lblLang.TabIndex = 4;
            this.lblLang.Text = "Dil:";
            // 
            // lstRecentErrors
            // 
            this.lstRecentErrors.FormattingEnabled = true;
            this.lstRecentErrors.Location = new System.Drawing.Point(19, 55);
            this.lstRecentErrors.Name = "lstRecentErrors";
            this.lstRecentErrors.Size = new System.Drawing.Size(355, 121);
            this.lstRecentErrors.TabIndex = 1;
            // 
            // Servis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDiagnostics);
            this.Controls.Add(this.grpLogs);
            this.Controls.Add(this.grpRealtime);
            this.Controls.Add(this.grpStep);
            this.Controls.Add(this.grpServo);
            this.Controls.Add(this.grpConnection);
            this.Name = "Servis";
            this.Size = new System.Drawing.Size(1280, 697);
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpServo.ResumeLayout(false);
            this.grpStep.ResumeLayout(false);
            this.grpRealtime.ResumeLayout(false);
            this.grpRealtime.PerformLayout();
            this.grpLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.grpDiagnostics.ResumeLayout(false);
            this.grpDiagnostics.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmbPorts;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grpServo;
        private System.Windows.Forms.FlowLayoutPanel flowServo;
        private System.Windows.Forms.GroupBox grpStep;
        private System.Windows.Forms.FlowLayoutPanel flowStep;
        private System.Windows.Forms.GroupBox grpRealtime;
        private System.Windows.Forms.Label lblLimitSwitch;
        private System.Windows.Forms.Label lblLoadCells;
        private System.Windows.Forms.Button btnHoming;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.GroupBox grpLogs;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.DateTimePicker dtpLogStart;
        private System.Windows.Forms.DateTimePicker dtpLogEnd;
        private System.Windows.Forms.ComboBox cmbLogLevel;
        private System.Windows.Forms.Button btnFilterLogs;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.GroupBox grpDiagnostics;
        private System.Windows.Forms.Label lblDiagnostics;
        private System.Windows.Forms.Label lblLogSearch;
        private System.Windows.Forms.TextBox txtLogSearch;
        private System.Windows.Forms.Button btnLoadDeviceLogs;
        private System.Windows.Forms.ListBox lstRecentErrors;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.ComboBox cmbServiceLanguage;
        private System.Windows.Forms.Label lblLang;
    }
}
