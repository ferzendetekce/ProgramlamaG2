namespace DevicesControllerApp.Raporlama
{
    partial class Reports
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
            this.lblBaslangicTarihi = new System.Windows.Forms.Label();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.dtpBaslangicTarihi = new System.Windows.Forms.DateTimePicker();
            this.dtpBitisTarihi = new System.Windows.Forms.DateTimePicker();
            this.lblBitisTarihi = new System.Windows.Forms.Label();
            this.tabRaporlama = new System.Windows.Forms.TabControl();
            this.tabHastaSecimi = new System.Windows.Forms.TabPage();
            this.tabOperatorSecimi = new System.Windows.Forms.TabPage();
            this.tabTerapiDurumu = new System.Windows.Forms.TabPage();
            this.tabLogSeviyesi = new System.Windows.Forms.TabPage();
            this.tabKarsilastirmaliRapor = new System.Windows.Forms.TabPage();
            this.cmbHastaSecimi = new System.Windows.Forms.ComboBox();
            this.cmbOperatorSecimi = new System.Windows.Forms.ComboBox();
            this.cmbTerapiDurumu = new System.Windows.Forms.ComboBox();
            this.cmbLogSeviyesi = new System.Windows.Forms.ComboBox();
            this.lblHastaKarsilastirma = new System.Windows.Forms.Label();
            this.lblTerapiBir = new System.Windows.Forms.Label();
            this.cmbHastaKarsilastirma = new System.Windows.Forms.ComboBox();
            this.cmbTerapiBir = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblTerapiIki = new System.Windows.Forms.Label();
            this.btnRaporGoruntule = new System.Windows.Forms.Button();
            this.btnPdfAktar = new System.Windows.Forms.Button();
            this.btnExcelAktar = new System.Windows.Forms.Button();
            this.lblEposta = new System.Windows.Forms.Label();
            this.txtEposta = new System.Windows.Forms.TextBox();
            this.btnEpostaGonder = new System.Windows.Forms.Button();
            this.lblDil = new System.Windows.Forms.Label();
            this.cmbDil = new System.Windows.Forms.ComboBox();
            this.tabloRapor = new System.Windows.Forms.DataGridView();
            this.tabRaporlama.SuspendLayout();
            this.tabHastaSecimi.SuspendLayout();
            this.tabOperatorSecimi.SuspendLayout();
            this.tabTerapiDurumu.SuspendLayout();
            this.tabLogSeviyesi.SuspendLayout();
            this.tabKarsilastirmaliRapor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabloRapor)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBaslangicTarihi
            // 
            this.lblBaslangicTarihi.AutoSize = true;
            this.lblBaslangicTarihi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBaslangicTarihi.Location = new System.Drawing.Point(61, 72);
            this.lblBaslangicTarihi.Name = "lblBaslangicTarihi";
            this.lblBaslangicTarihi.Size = new System.Drawing.Size(91, 15);
            this.lblBaslangicTarihi.TabIndex = 0;
            this.lblBaslangicTarihi.Text = "Başlangıç Tarihi";
            // 
            // lblBaslik
            // 
            this.lblBaslik.BackColor = System.Drawing.SystemColors.Window;
            this.lblBaslik.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBaslik.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblBaslik.Location = new System.Drawing.Point(0, 0);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(1343, 32);
            this.lblBaslik.TabIndex = 1;
            this.lblBaslik.Text = "RAPORLAMA MODÜLÜ";
            this.lblBaslik.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpBaslangicTarihi
            // 
            this.dtpBaslangicTarihi.CustomFormat = "dd.MM.yyyy";
            this.dtpBaslangicTarihi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBaslangicTarihi.Location = new System.Drawing.Point(64, 91);
            this.dtpBaslangicTarihi.Name = "dtpBaslangicTarihi";
            this.dtpBaslangicTarihi.Size = new System.Drawing.Size(93, 20);
            this.dtpBaslangicTarihi.TabIndex = 2;
            // 
            // dtpBitisTarihi
            // 
            this.dtpBitisTarihi.CustomFormat = "dd.MM.yyyy";
            this.dtpBitisTarihi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBitisTarihi.Location = new System.Drawing.Point(191, 91);
            this.dtpBitisTarihi.Name = "dtpBitisTarihi";
            this.dtpBitisTarihi.Size = new System.Drawing.Size(93, 20);
            this.dtpBitisTarihi.TabIndex = 4;
            // 
            // lblBitisTarihi
            // 
            this.lblBitisTarihi.AutoSize = true;
            this.lblBitisTarihi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBitisTarihi.Location = new System.Drawing.Point(188, 72);
            this.lblBitisTarihi.Name = "lblBitisTarihi";
            this.lblBitisTarihi.Size = new System.Drawing.Size(64, 15);
            this.lblBitisTarihi.TabIndex = 3;
            this.lblBitisTarihi.Text = "Bitiş Tarihi";
            // 
            // tabRaporlama
            // 
            this.tabRaporlama.Controls.Add(this.tabHastaSecimi);
            this.tabRaporlama.Controls.Add(this.tabOperatorSecimi);
            this.tabRaporlama.Controls.Add(this.tabTerapiDurumu);
            this.tabRaporlama.Controls.Add(this.tabLogSeviyesi);
            this.tabRaporlama.Controls.Add(this.tabKarsilastirmaliRapor);
            this.tabRaporlama.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tabRaporlama.Location = new System.Drawing.Point(322, 50);
            this.tabRaporlama.Name = "tabRaporlama";
            this.tabRaporlama.SelectedIndex = 0;
            this.tabRaporlama.Size = new System.Drawing.Size(489, 90);
            this.tabRaporlama.TabIndex = 5;
            // 
            // tabHastaSecimi
            // 
            this.tabHastaSecimi.Controls.Add(this.cmbHastaSecimi);
            this.tabHastaSecimi.Location = new System.Drawing.Point(4, 24);
            this.tabHastaSecimi.Name = "tabHastaSecimi";
            this.tabHastaSecimi.Padding = new System.Windows.Forms.Padding(3);
            this.tabHastaSecimi.Size = new System.Drawing.Size(481, 62);
            this.tabHastaSecimi.TabIndex = 0;
            this.tabHastaSecimi.Text = "Hasta Seçimi";
            this.tabHastaSecimi.UseVisualStyleBackColor = true;
            // 
            // tabOperatorSecimi
            // 
            this.tabOperatorSecimi.Controls.Add(this.cmbOperatorSecimi);
            this.tabOperatorSecimi.Location = new System.Drawing.Point(4, 24);
            this.tabOperatorSecimi.Name = "tabOperatorSecimi";
            this.tabOperatorSecimi.Padding = new System.Windows.Forms.Padding(3);
            this.tabOperatorSecimi.Size = new System.Drawing.Size(482, 62);
            this.tabOperatorSecimi.TabIndex = 1;
            this.tabOperatorSecimi.Text = "Operatör Seçimi";
            this.tabOperatorSecimi.UseVisualStyleBackColor = true;
            // 
            // tabTerapiDurumu
            // 
            this.tabTerapiDurumu.Controls.Add(this.cmbTerapiDurumu);
            this.tabTerapiDurumu.Location = new System.Drawing.Point(4, 24);
            this.tabTerapiDurumu.Name = "tabTerapiDurumu";
            this.tabTerapiDurumu.Padding = new System.Windows.Forms.Padding(3);
            this.tabTerapiDurumu.Size = new System.Drawing.Size(482, 62);
            this.tabTerapiDurumu.TabIndex = 2;
            this.tabTerapiDurumu.Text = "Terapi Durumu";
            this.tabTerapiDurumu.UseVisualStyleBackColor = true;
            // 
            // tabLogSeviyesi
            // 
            this.tabLogSeviyesi.Controls.Add(this.cmbLogSeviyesi);
            this.tabLogSeviyesi.Location = new System.Drawing.Point(4, 24);
            this.tabLogSeviyesi.Name = "tabLogSeviyesi";
            this.tabLogSeviyesi.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogSeviyesi.Size = new System.Drawing.Size(482, 62);
            this.tabLogSeviyesi.TabIndex = 3;
            this.tabLogSeviyesi.Text = "Log Seviyesi";
            this.tabLogSeviyesi.UseVisualStyleBackColor = true;
            // 
            // tabKarsilastirmaliRapor
            // 
            this.tabKarsilastirmaliRapor.Controls.Add(this.comboBox1);
            this.tabKarsilastirmaliRapor.Controls.Add(this.lblTerapiIki);
            this.tabKarsilastirmaliRapor.Controls.Add(this.cmbTerapiBir);
            this.tabKarsilastirmaliRapor.Controls.Add(this.cmbHastaKarsilastirma);
            this.tabKarsilastirmaliRapor.Controls.Add(this.lblTerapiBir);
            this.tabKarsilastirmaliRapor.Controls.Add(this.lblHastaKarsilastirma);
            this.tabKarsilastirmaliRapor.Location = new System.Drawing.Point(4, 24);
            this.tabKarsilastirmaliRapor.Name = "tabKarsilastirmaliRapor";
            this.tabKarsilastirmaliRapor.Padding = new System.Windows.Forms.Padding(3);
            this.tabKarsilastirmaliRapor.Size = new System.Drawing.Size(482, 62);
            this.tabKarsilastirmaliRapor.TabIndex = 4;
            this.tabKarsilastirmaliRapor.Text = "Karşılaştırmalı Rapor";
            this.tabKarsilastirmaliRapor.UseVisualStyleBackColor = true;
            // 
            // cmbHastaSecimi
            // 
            this.cmbHastaSecimi.FormattingEnabled = true;
            this.cmbHastaSecimi.Location = new System.Drawing.Point(5, 20);
            this.cmbHastaSecimi.Name = "cmbHastaSecimi";
            this.cmbHastaSecimi.Size = new System.Drawing.Size(121, 23);
            this.cmbHastaSecimi.TabIndex = 0;
            // 
            // cmbOperatorSecimi
            // 
            this.cmbOperatorSecimi.FormattingEnabled = true;
            this.cmbOperatorSecimi.Location = new System.Drawing.Point(80, 20);
            this.cmbOperatorSecimi.Name = "cmbOperatorSecimi";
            this.cmbOperatorSecimi.Size = new System.Drawing.Size(121, 23);
            this.cmbOperatorSecimi.TabIndex = 1;
            // 
            // cmbTerapiDurumu
            // 
            this.cmbTerapiDurumu.FormattingEnabled = true;
            this.cmbTerapiDurumu.Location = new System.Drawing.Point(175, 20);
            this.cmbTerapiDurumu.Name = "cmbTerapiDurumu";
            this.cmbTerapiDurumu.Size = new System.Drawing.Size(121, 23);
            this.cmbTerapiDurumu.TabIndex = 0;
            // 
            // cmbLogSeviyesi
            // 
            this.cmbLogSeviyesi.FormattingEnabled = true;
            this.cmbLogSeviyesi.Location = new System.Drawing.Point(254, 20);
            this.cmbLogSeviyesi.Name = "cmbLogSeviyesi";
            this.cmbLogSeviyesi.Size = new System.Drawing.Size(121, 23);
            this.cmbLogSeviyesi.TabIndex = 1;
            // 
            // lblHastaKarsilastirma
            // 
            this.lblHastaKarsilastirma.AutoSize = true;
            this.lblHastaKarsilastirma.Location = new System.Drawing.Point(37, 12);
            this.lblHastaKarsilastirma.Name = "lblHastaKarsilastirma";
            this.lblHastaKarsilastirma.Size = new System.Drawing.Size(78, 15);
            this.lblHastaKarsilastirma.TabIndex = 0;
            this.lblHastaKarsilastirma.Text = "Hasta Seçimi";
            // 
            // lblTerapiBir
            // 
            this.lblTerapiBir.AutoSize = true;
            this.lblTerapiBir.Location = new System.Drawing.Point(177, 12);
            this.lblTerapiBir.Name = "lblTerapiBir";
            this.lblTerapiBir.Size = new System.Drawing.Size(51, 15);
            this.lblTerapiBir.TabIndex = 1;
            this.lblTerapiBir.Text = "Terapi 1";
            // 
            // cmbHastaKarsilastirma
            // 
            this.cmbHastaKarsilastirma.FormattingEnabled = true;
            this.cmbHastaKarsilastirma.Location = new System.Drawing.Point(37, 28);
            this.cmbHastaKarsilastirma.Name = "cmbHastaKarsilastirma";
            this.cmbHastaKarsilastirma.Size = new System.Drawing.Size(121, 23);
            this.cmbHastaKarsilastirma.TabIndex = 2;
            // 
            // cmbTerapiBir
            // 
            this.cmbTerapiBir.FormattingEnabled = true;
            this.cmbTerapiBir.Location = new System.Drawing.Point(180, 28);
            this.cmbTerapiBir.Name = "cmbTerapiBir";
            this.cmbTerapiBir.Size = new System.Drawing.Size(121, 23);
            this.cmbTerapiBir.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(323, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 5;
            // 
            // lblTerapiIki
            // 
            this.lblTerapiIki.AutoSize = true;
            this.lblTerapiIki.Location = new System.Drawing.Point(320, 12);
            this.lblTerapiIki.Name = "lblTerapiIki";
            this.lblTerapiIki.Size = new System.Drawing.Size(51, 15);
            this.lblTerapiIki.TabIndex = 4;
            this.lblTerapiIki.Text = "Terapi 2";
            // 
            // btnRaporGoruntule
            // 
            this.btnRaporGoruntule.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRaporGoruntule.Location = new System.Drawing.Point(846, 53);
            this.btnRaporGoruntule.Name = "btnRaporGoruntule";
            this.btnRaporGoruntule.Size = new System.Drawing.Size(87, 82);
            this.btnRaporGoruntule.TabIndex = 6;
            this.btnRaporGoruntule.Text = "Raporu Görüntüle";
            this.btnRaporGoruntule.UseVisualStyleBackColor = true;
            // 
            // btnPdfAktar
            // 
            this.btnPdfAktar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPdfAktar.Location = new System.Drawing.Point(965, 53);
            this.btnPdfAktar.Name = "btnPdfAktar";
            this.btnPdfAktar.Size = new System.Drawing.Size(75, 38);
            this.btnPdfAktar.TabIndex = 7;
            this.btnPdfAktar.Text = "PDF";
            this.btnPdfAktar.UseVisualStyleBackColor = true;
            // 
            // btnExcelAktar
            // 
            this.btnExcelAktar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExcelAktar.Location = new System.Drawing.Point(965, 97);
            this.btnExcelAktar.Name = "btnExcelAktar";
            this.btnExcelAktar.Size = new System.Drawing.Size(75, 38);
            this.btnExcelAktar.TabIndex = 8;
            this.btnExcelAktar.Text = "Excel";
            this.btnExcelAktar.UseVisualStyleBackColor = true;
            // 
            // lblEposta
            // 
            this.lblEposta.AutoSize = true;
            this.lblEposta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEposta.Location = new System.Drawing.Point(1078, 70);
            this.lblEposta.Name = "lblEposta";
            this.lblEposta.Size = new System.Drawing.Size(48, 15);
            this.lblEposta.TabIndex = 9;
            this.lblEposta.Text = "E-Posta";
            // 
            // txtEposta
            // 
            this.txtEposta.Location = new System.Drawing.Point(1129, 67);
            this.txtEposta.Name = "txtEposta";
            this.txtEposta.Size = new System.Drawing.Size(122, 20);
            this.txtEposta.TabIndex = 10;
            // 
            // btnEpostaGonder
            // 
            this.btnEpostaGonder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEpostaGonder.Location = new System.Drawing.Point(1081, 93);
            this.btnEpostaGonder.Name = "btnEpostaGonder";
            this.btnEpostaGonder.Size = new System.Drawing.Size(170, 23);
            this.btnEpostaGonder.TabIndex = 11;
            this.btnEpostaGonder.Text = "E-Posta Gönder";
            this.btnEpostaGonder.UseVisualStyleBackColor = true;
            // 
            // lblDil
            // 
            this.lblDil.AutoSize = true;
            this.lblDil.Location = new System.Drawing.Point(1257, 41);
            this.lblDil.Name = "lblDil";
            this.lblDil.Size = new System.Drawing.Size(19, 13);
            this.lblDil.TabIndex = 12;
            this.lblDil.Text = "Dil";
            // 
            // cmbDil
            // 
            this.cmbDil.FormattingEnabled = true;
            this.cmbDil.Location = new System.Drawing.Point(1282, 38);
            this.cmbDil.Name = "cmbDil";
            this.cmbDil.Size = new System.Drawing.Size(44, 21);
            this.cmbDil.TabIndex = 6;
            // 
            // tabloRapor
            // 
            this.tabloRapor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabloRapor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabloRapor.Location = new System.Drawing.Point(0, 146);
            this.tabloRapor.Name = "tabloRapor";
            this.tabloRapor.Size = new System.Drawing.Size(1343, 559);
            this.tabloRapor.TabIndex = 13;
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabloRapor);
            this.Controls.Add(this.cmbDil);
            this.Controls.Add(this.lblDil);
            this.Controls.Add(this.btnEpostaGonder);
            this.Controls.Add(this.txtEposta);
            this.Controls.Add(this.lblEposta);
            this.Controls.Add(this.btnExcelAktar);
            this.Controls.Add(this.btnPdfAktar);
            this.Controls.Add(this.btnRaporGoruntule);
            this.Controls.Add(this.tabRaporlama);
            this.Controls.Add(this.dtpBitisTarihi);
            this.Controls.Add(this.lblBitisTarihi);
            this.Controls.Add(this.dtpBaslangicTarihi);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.lblBaslangicTarihi);
            this.Name = "Reports";
            this.Size = new System.Drawing.Size(1343, 705);
            this.tabRaporlama.ResumeLayout(false);
            this.tabHastaSecimi.ResumeLayout(false);
            this.tabOperatorSecimi.ResumeLayout(false);
            this.tabTerapiDurumu.ResumeLayout(false);
            this.tabLogSeviyesi.ResumeLayout(false);
            this.tabKarsilastirmaliRapor.ResumeLayout(false);
            this.tabKarsilastirmaliRapor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabloRapor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBaslangicTarihi;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.DateTimePicker dtpBaslangicTarihi;
        private System.Windows.Forms.DateTimePicker dtpBitisTarihi;
        private System.Windows.Forms.Label lblBitisTarihi;
        private System.Windows.Forms.TabControl tabRaporlama;
        private System.Windows.Forms.TabPage tabHastaSecimi;
        private System.Windows.Forms.TabPage tabOperatorSecimi;
        private System.Windows.Forms.TabPage tabTerapiDurumu;
        private System.Windows.Forms.TabPage tabLogSeviyesi;
        private System.Windows.Forms.TabPage tabKarsilastirmaliRapor;
        private System.Windows.Forms.ComboBox cmbHastaSecimi;
        private System.Windows.Forms.ComboBox cmbOperatorSecimi;
        private System.Windows.Forms.ComboBox cmbTerapiDurumu;
        private System.Windows.Forms.ComboBox cmbLogSeviyesi;
        private System.Windows.Forms.ComboBox cmbTerapiBir;
        private System.Windows.Forms.ComboBox cmbHastaKarsilastirma;
        private System.Windows.Forms.Label lblTerapiBir;
        private System.Windows.Forms.Label lblHastaKarsilastirma;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblTerapiIki;
        private System.Windows.Forms.Button btnRaporGoruntule;
        private System.Windows.Forms.Button btnPdfAktar;
        private System.Windows.Forms.Button btnExcelAktar;
        private System.Windows.Forms.Label lblEposta;
        private System.Windows.Forms.TextBox txtEposta;
        private System.Windows.Forms.Button btnEpostaGonder;
        private System.Windows.Forms.Label lblDil;
        private System.Windows.Forms.ComboBox cmbDil;
        private System.Windows.Forms.DataGridView tabloRapor;
    }
}
