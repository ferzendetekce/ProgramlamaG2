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
            this.tabHastaRaporlari = new System.Windows.Forms.TabPage();
            this.cmbHastaSecimi = new System.Windows.Forms.ComboBox();
            this.tabTerapiRaporlari = new System.Windows.Forms.TabPage();
            this.tabOperatorRaporlari = new System.Windows.Forms.TabPage();
            this.tabSistemRaporlari = new System.Windows.Forms.TabPage();
            this.tabKarsilastirmaliRapor = new System.Windows.Forms.TabPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblTerapiIki = new System.Windows.Forms.Label();
            this.cmbTerapiBir = new System.Windows.Forms.ComboBox();
            this.cmbHastaKarsilastirma = new System.Windows.Forms.ComboBox();
            this.lblTerapiBir = new System.Windows.Forms.Label();
            this.lblHastaKarsilastirma = new System.Windows.Forms.Label();
            this.btnPdfAktar = new System.Windows.Forms.Button();
            this.btnExcelAktar = new System.Windows.Forms.Button();
            this.lblEposta = new System.Windows.Forms.Label();
            this.txtEposta = new System.Windows.Forms.TextBox();
            this.btnEpostaGonder = new System.Windows.Forms.Button();
            this.lblDil = new System.Windows.Forms.Label();
            this.cmbDil = new System.Windows.Forms.ComboBox();
            this.tabloRapor = new System.Windows.Forms.DataGridView();
            this.btnHastalariListele = new System.Windows.Forms.Button();
            this.lblHastaSecimi = new System.Windows.Forms.Label();
            this.btnHastaDetayRaporu = new System.Windows.Forms.Button();
            this.btnHastaTerapiGecmisi = new System.Windows.Forms.Button();
            this.btnTerapiSayilari = new System.Windows.Forms.Button();
            this.btnTerapiSureleri = new System.Windows.Forms.Button();
            this.btnLoadcellGrafikGoster = new System.Windows.Forms.Button();
            this.lblTerapiHastaSeciniz = new System.Windows.Forms.Label();
            this.cmbTerapiHastaSecimi = new System.Windows.Forms.ComboBox();
            this.lblTerapi = new System.Windows.Forms.Label();
            this.cmbTerapiSecimi = new System.Windows.Forms.ComboBox();
            this.btnOperatorTerapiIstatistik = new System.Windows.Forms.Button();
            this.btnOperatorCalismaSaatleri = new System.Windows.Forms.Button();
            this.btnHataLoglari = new System.Windows.Forms.Button();
            this.btnKullanimIstatistikleri = new System.Windows.Forms.Button();
            this.btnCihazPerformans = new System.Windows.Forms.Button();
            this.btnKarsilastirmaliRapor = new System.Windows.Forms.Button();
            this.tabRaporlama.SuspendLayout();
            this.tabHastaRaporlari.SuspendLayout();
            this.tabTerapiRaporlari.SuspendLayout();
            this.tabOperatorRaporlari.SuspendLayout();
            this.tabSistemRaporlari.SuspendLayout();
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
            this.tabRaporlama.Controls.Add(this.tabHastaRaporlari);
            this.tabRaporlama.Controls.Add(this.tabTerapiRaporlari);
            this.tabRaporlama.Controls.Add(this.tabOperatorRaporlari);
            this.tabRaporlama.Controls.Add(this.tabSistemRaporlari);
            this.tabRaporlama.Controls.Add(this.tabKarsilastirmaliRapor);
            this.tabRaporlama.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tabRaporlama.Location = new System.Drawing.Point(322, 50);
            this.tabRaporlama.Name = "tabRaporlama";
            this.tabRaporlama.SelectedIndex = 0;
            this.tabRaporlama.Size = new System.Drawing.Size(624, 90);
            this.tabRaporlama.TabIndex = 5;
            // 
            // tabHastaRaporlari
            // 
            this.tabHastaRaporlari.Controls.Add(this.btnHastaTerapiGecmisi);
            this.tabHastaRaporlari.Controls.Add(this.btnHastaDetayRaporu);
            this.tabHastaRaporlari.Controls.Add(this.lblHastaSecimi);
            this.tabHastaRaporlari.Controls.Add(this.btnHastalariListele);
            this.tabHastaRaporlari.Controls.Add(this.cmbHastaSecimi);
            this.tabHastaRaporlari.Location = new System.Drawing.Point(4, 24);
            this.tabHastaRaporlari.Name = "tabHastaRaporlari";
            this.tabHastaRaporlari.Padding = new System.Windows.Forms.Padding(3);
            this.tabHastaRaporlari.Size = new System.Drawing.Size(616, 62);
            this.tabHastaRaporlari.TabIndex = 0;
            this.tabHastaRaporlari.Text = "Hasta Raporları";
            this.tabHastaRaporlari.UseVisualStyleBackColor = true;
            // 
            // cmbHastaSecimi
            // 
            this.cmbHastaSecimi.FormattingEnabled = true;
            this.cmbHastaSecimi.Location = new System.Drawing.Point(184, 27);
            this.cmbHastaSecimi.Name = "cmbHastaSecimi";
            this.cmbHastaSecimi.Size = new System.Drawing.Size(121, 23);
            this.cmbHastaSecimi.TabIndex = 0;
            this.cmbHastaSecimi.SelectedIndexChanged += new System.EventHandler(this.cmbHastaSecimi_SelectedIndexChanged);
            // 
            // tabTerapiRaporlari
            // 
            this.tabTerapiRaporlari.Controls.Add(this.lblTerapi);
            this.tabTerapiRaporlari.Controls.Add(this.cmbTerapiSecimi);
            this.tabTerapiRaporlari.Controls.Add(this.lblTerapiHastaSeciniz);
            this.tabTerapiRaporlari.Controls.Add(this.cmbTerapiHastaSecimi);
            this.tabTerapiRaporlari.Controls.Add(this.btnLoadcellGrafikGoster);
            this.tabTerapiRaporlari.Controls.Add(this.btnTerapiSureleri);
            this.tabTerapiRaporlari.Controls.Add(this.btnTerapiSayilari);
            this.tabTerapiRaporlari.Location = new System.Drawing.Point(4, 24);
            this.tabTerapiRaporlari.Name = "tabTerapiRaporlari";
            this.tabTerapiRaporlari.Padding = new System.Windows.Forms.Padding(3);
            this.tabTerapiRaporlari.Size = new System.Drawing.Size(616, 62);
            this.tabTerapiRaporlari.TabIndex = 1;
            this.tabTerapiRaporlari.Text = "Terapi Raporları";
            this.tabTerapiRaporlari.UseVisualStyleBackColor = true;
            // 
            // tabOperatorRaporlari
            // 
            this.tabOperatorRaporlari.Controls.Add(this.btnOperatorCalismaSaatleri);
            this.tabOperatorRaporlari.Controls.Add(this.btnOperatorTerapiIstatistik);
            this.tabOperatorRaporlari.Location = new System.Drawing.Point(4, 24);
            this.tabOperatorRaporlari.Name = "tabOperatorRaporlari";
            this.tabOperatorRaporlari.Padding = new System.Windows.Forms.Padding(3);
            this.tabOperatorRaporlari.Size = new System.Drawing.Size(616, 62);
            this.tabOperatorRaporlari.TabIndex = 2;
            this.tabOperatorRaporlari.Text = "Operatör Raporları";
            this.tabOperatorRaporlari.UseVisualStyleBackColor = true;
            // 
            // tabSistemRaporlari
            // 
            this.tabSistemRaporlari.Controls.Add(this.btnCihazPerformans);
            this.tabSistemRaporlari.Controls.Add(this.btnKullanimIstatistikleri);
            this.tabSistemRaporlari.Controls.Add(this.btnHataLoglari);
            this.tabSistemRaporlari.Location = new System.Drawing.Point(4, 24);
            this.tabSistemRaporlari.Name = "tabSistemRaporlari";
            this.tabSistemRaporlari.Padding = new System.Windows.Forms.Padding(3);
            this.tabSistemRaporlari.Size = new System.Drawing.Size(616, 62);
            this.tabSistemRaporlari.TabIndex = 3;
            this.tabSistemRaporlari.Text = "Sistem Raporlari";
            this.tabSistemRaporlari.UseVisualStyleBackColor = true;
            // 
            // tabKarsilastirmaliRapor
            // 
            this.tabKarsilastirmaliRapor.Controls.Add(this.btnKarsilastirmaliRapor);
            this.tabKarsilastirmaliRapor.Controls.Add(this.comboBox1);
            this.tabKarsilastirmaliRapor.Controls.Add(this.lblTerapiIki);
            this.tabKarsilastirmaliRapor.Controls.Add(this.cmbTerapiBir);
            this.tabKarsilastirmaliRapor.Controls.Add(this.cmbHastaKarsilastirma);
            this.tabKarsilastirmaliRapor.Controls.Add(this.lblTerapiBir);
            this.tabKarsilastirmaliRapor.Controls.Add(this.lblHastaKarsilastirma);
            this.tabKarsilastirmaliRapor.Location = new System.Drawing.Point(4, 24);
            this.tabKarsilastirmaliRapor.Name = "tabKarsilastirmaliRapor";
            this.tabKarsilastirmaliRapor.Padding = new System.Windows.Forms.Padding(3);
            this.tabKarsilastirmaliRapor.Size = new System.Drawing.Size(616, 62);
            this.tabKarsilastirmaliRapor.TabIndex = 4;
            this.tabKarsilastirmaliRapor.Text = "Karşılaştırmalı Rapor";
            this.tabKarsilastirmaliRapor.UseVisualStyleBackColor = true;
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
            // cmbTerapiBir
            // 
            this.cmbTerapiBir.FormattingEnabled = true;
            this.cmbTerapiBir.Location = new System.Drawing.Point(180, 28);
            this.cmbTerapiBir.Name = "cmbTerapiBir";
            this.cmbTerapiBir.Size = new System.Drawing.Size(121, 23);
            this.cmbTerapiBir.TabIndex = 3;
            // 
            // cmbHastaKarsilastirma
            // 
            this.cmbHastaKarsilastirma.FormattingEnabled = true;
            this.cmbHastaKarsilastirma.Location = new System.Drawing.Point(37, 28);
            this.cmbHastaKarsilastirma.Name = "cmbHastaKarsilastirma";
            this.cmbHastaKarsilastirma.Size = new System.Drawing.Size(121, 23);
            this.cmbHastaKarsilastirma.TabIndex = 2;
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
            // lblHastaKarsilastirma
            // 
            this.lblHastaKarsilastirma.AutoSize = true;
            this.lblHastaKarsilastirma.Location = new System.Drawing.Point(37, 12);
            this.lblHastaKarsilastirma.Name = "lblHastaKarsilastirma";
            this.lblHastaKarsilastirma.Size = new System.Drawing.Size(78, 15);
            this.lblHastaKarsilastirma.TabIndex = 0;
            this.lblHastaKarsilastirma.Text = "Hasta Seçimi";
            // 
            // btnPdfAktar
            // 
            this.btnPdfAktar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPdfAktar.Location = new System.Drawing.Point(979, 53);
            this.btnPdfAktar.Name = "btnPdfAktar";
            this.btnPdfAktar.Size = new System.Drawing.Size(75, 38);
            this.btnPdfAktar.TabIndex = 7;
            this.btnPdfAktar.Text = "PDF";
            this.btnPdfAktar.UseVisualStyleBackColor = true;
            // 
            // btnExcelAktar
            // 
            this.btnExcelAktar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExcelAktar.Location = new System.Drawing.Point(979, 97);
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
            this.lblEposta.Location = new System.Drawing.Point(1081, 70);
            this.lblEposta.Name = "lblEposta";
            this.lblEposta.Size = new System.Drawing.Size(48, 15);
            this.lblEposta.TabIndex = 9;
            this.lblEposta.Text = "E-Posta";
            // 
            // txtEposta
            // 
            this.txtEposta.Location = new System.Drawing.Point(1132, 67);
            this.txtEposta.Name = "txtEposta";
            this.txtEposta.Size = new System.Drawing.Size(122, 20);
            this.txtEposta.TabIndex = 10;
            // 
            // btnEpostaGonder
            // 
            this.btnEpostaGonder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEpostaGonder.Location = new System.Drawing.Point(1084, 93);
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
            this.tabloRapor.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabloRapor_CellContentClick);
            // 
            // btnHastalariListele
            // 
            this.btnHastalariListele.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHastalariListele.Location = new System.Drawing.Point(19, 12);
            this.btnHastalariListele.Name = "btnHastalariListele";
            this.btnHastalariListele.Size = new System.Drawing.Size(120, 44);
            this.btnHastalariListele.TabIndex = 14;
            this.btnHastalariListele.Text = "Hastaları Listele";
            this.btnHastalariListele.UseVisualStyleBackColor = true;
            this.btnHastalariListele.Click += new System.EventHandler(this.btnHastalariListele_Click);
            // 
            // lblHastaSecimi
            // 
            this.lblHastaSecimi.AutoSize = true;
            this.lblHastaSecimi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHastaSecimi.Location = new System.Drawing.Point(181, 12);
            this.lblHastaSecimi.Name = "lblHastaSecimi";
            this.lblHastaSecimi.Size = new System.Drawing.Size(80, 15);
            this.lblHastaSecimi.TabIndex = 14;
            this.lblHastaSecimi.Text = "Hasta Seçiniz";
            // 
            // btnHastaDetayRaporu
            // 
            this.btnHastaDetayRaporu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHastaDetayRaporu.Location = new System.Drawing.Point(323, 12);
            this.btnHastaDetayRaporu.Name = "btnHastaDetayRaporu";
            this.btnHastaDetayRaporu.Size = new System.Drawing.Size(87, 44);
            this.btnHastaDetayRaporu.TabIndex = 15;
            this.btnHastaDetayRaporu.Text = "Hasta Detay Raporu";
            this.btnHastaDetayRaporu.UseVisualStyleBackColor = true;
            this.btnHastaDetayRaporu.Click += new System.EventHandler(this.btnHastaDetayRaporu_Click);
            // 
            // btnHastaTerapiGecmisi
            // 
            this.btnHastaTerapiGecmisi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHastaTerapiGecmisi.Location = new System.Drawing.Point(426, 12);
            this.btnHastaTerapiGecmisi.Name = "btnHastaTerapiGecmisi";
            this.btnHastaTerapiGecmisi.Size = new System.Drawing.Size(87, 44);
            this.btnHastaTerapiGecmisi.TabIndex = 16;
            this.btnHastaTerapiGecmisi.Text = "Hasta Terapi Geçmişi";
            this.btnHastaTerapiGecmisi.UseVisualStyleBackColor = true;
            this.btnHastaTerapiGecmisi.Click += new System.EventHandler(this.btnHastaTerapiGecmisi_Click);
            // 
            // btnTerapiSayilari
            // 
            this.btnTerapiSayilari.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTerapiSayilari.Location = new System.Drawing.Point(13, 12);
            this.btnTerapiSayilari.Name = "btnTerapiSayilari";
            this.btnTerapiSayilari.Size = new System.Drawing.Size(77, 44);
            this.btnTerapiSayilari.TabIndex = 15;
            this.btnTerapiSayilari.Text = "Terapi Sayıları";
            this.btnTerapiSayilari.UseVisualStyleBackColor = true;
            // 
            // btnTerapiSureleri
            // 
            this.btnTerapiSureleri.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTerapiSureleri.Location = new System.Drawing.Point(103, 12);
            this.btnTerapiSureleri.Name = "btnTerapiSureleri";
            this.btnTerapiSureleri.Size = new System.Drawing.Size(75, 44);
            this.btnTerapiSureleri.TabIndex = 16;
            this.btnTerapiSureleri.Text = "Terapi Süreleri";
            this.btnTerapiSureleri.UseVisualStyleBackColor = true;
            // 
            // btnLoadcellGrafikGoster
            // 
            this.btnLoadcellGrafikGoster.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoadcellGrafikGoster.Location = new System.Drawing.Point(509, 12);
            this.btnLoadcellGrafikGoster.Name = "btnLoadcellGrafikGoster";
            this.btnLoadcellGrafikGoster.Size = new System.Drawing.Size(94, 44);
            this.btnLoadcellGrafikGoster.TabIndex = 17;
            this.btnLoadcellGrafikGoster.Text = "Loadcell Verilerini Gör";
            this.btnLoadcellGrafikGoster.UseVisualStyleBackColor = true;
            // 
            // lblTerapiHastaSeciniz
            // 
            this.lblTerapiHastaSeciniz.AutoSize = true;
            this.lblTerapiHastaSeciniz.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTerapiHastaSeciniz.Location = new System.Drawing.Point(224, 12);
            this.lblTerapiHastaSeciniz.Name = "lblTerapiHastaSeciniz";
            this.lblTerapiHastaSeciniz.Size = new System.Drawing.Size(80, 15);
            this.lblTerapiHastaSeciniz.TabIndex = 19;
            this.lblTerapiHastaSeciniz.Text = "Hasta Seçiniz";
            // 
            // cmbTerapiHastaSecimi
            // 
            this.cmbTerapiHastaSecimi.FormattingEnabled = true;
            this.cmbTerapiHastaSecimi.Location = new System.Drawing.Point(227, 27);
            this.cmbTerapiHastaSecimi.Name = "cmbTerapiHastaSecimi";
            this.cmbTerapiHastaSecimi.Size = new System.Drawing.Size(121, 23);
            this.cmbTerapiHastaSecimi.TabIndex = 18;
            // 
            // lblTerapi
            // 
            this.lblTerapi.AutoSize = true;
            this.lblTerapi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTerapi.Location = new System.Drawing.Point(366, 12);
            this.lblTerapi.Name = "lblTerapi";
            this.lblTerapi.Size = new System.Drawing.Size(83, 15);
            this.lblTerapi.TabIndex = 21;
            this.lblTerapi.Text = "Terapi Seçiniz";
            // 
            // cmbTerapiSecimi
            // 
            this.cmbTerapiSecimi.FormattingEnabled = true;
            this.cmbTerapiSecimi.Location = new System.Drawing.Point(369, 27);
            this.cmbTerapiSecimi.Name = "cmbTerapiSecimi";
            this.cmbTerapiSecimi.Size = new System.Drawing.Size(121, 23);
            this.cmbTerapiSecimi.TabIndex = 20;
            // 
            // btnOperatorTerapiIstatistik
            // 
            this.btnOperatorTerapiIstatistik.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnOperatorTerapiIstatistik.Location = new System.Drawing.Point(19, 12);
            this.btnOperatorTerapiIstatistik.Name = "btnOperatorTerapiIstatistik";
            this.btnOperatorTerapiIstatistik.Size = new System.Drawing.Size(120, 44);
            this.btnOperatorTerapiIstatistik.TabIndex = 15;
            this.btnOperatorTerapiIstatistik.Text = "Terapi İstatistikleri";
            this.btnOperatorTerapiIstatistik.UseVisualStyleBackColor = true;
            // 
            // btnOperatorCalismaSaatleri
            // 
            this.btnOperatorCalismaSaatleri.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnOperatorCalismaSaatleri.Location = new System.Drawing.Point(173, 12);
            this.btnOperatorCalismaSaatleri.Name = "btnOperatorCalismaSaatleri";
            this.btnOperatorCalismaSaatleri.Size = new System.Drawing.Size(120, 44);
            this.btnOperatorCalismaSaatleri.TabIndex = 16;
            this.btnOperatorCalismaSaatleri.Text = "Çalışma Saatleri";
            this.btnOperatorCalismaSaatleri.UseVisualStyleBackColor = true;
            // 
            // btnHataLoglari
            // 
            this.btnHataLoglari.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHataLoglari.Location = new System.Drawing.Point(22, 12);
            this.btnHataLoglari.Name = "btnHataLoglari";
            this.btnHataLoglari.Size = new System.Drawing.Size(120, 44);
            this.btnHataLoglari.TabIndex = 16;
            this.btnHataLoglari.Text = "Hata Logları";
            this.btnHataLoglari.UseVisualStyleBackColor = true;
            // 
            // btnKullanimIstatistikleri
            // 
            this.btnKullanimIstatistikleri.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnKullanimIstatistikleri.Location = new System.Drawing.Point(215, 12);
            this.btnKullanimIstatistikleri.Name = "btnKullanimIstatistikleri";
            this.btnKullanimIstatistikleri.Size = new System.Drawing.Size(146, 44);
            this.btnKullanimIstatistikleri.TabIndex = 17;
            this.btnKullanimIstatistikleri.Text = "Kullanım İstatistikleri";
            this.btnKullanimIstatistikleri.UseVisualStyleBackColor = true;
            // 
            // btnCihazPerformans
            // 
            this.btnCihazPerformans.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCihazPerformans.Location = new System.Drawing.Point(438, 12);
            this.btnCihazPerformans.Name = "btnCihazPerformans";
            this.btnCihazPerformans.Size = new System.Drawing.Size(120, 44);
            this.btnCihazPerformans.TabIndex = 18;
            this.btnCihazPerformans.Text = "Cihaz Performans";
            this.btnCihazPerformans.UseVisualStyleBackColor = true;
            // 
            // btnKarsilastirmaliRapor
            // 
            this.btnKarsilastirmaliRapor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnKarsilastirmaliRapor.Location = new System.Drawing.Point(471, 12);
            this.btnKarsilastirmaliRapor.Name = "btnKarsilastirmaliRapor";
            this.btnKarsilastirmaliRapor.Size = new System.Drawing.Size(120, 44);
            this.btnKarsilastirmaliRapor.TabIndex = 19;
            this.btnKarsilastirmaliRapor.Text = "Raporu Görüntüle";
            this.btnKarsilastirmaliRapor.UseVisualStyleBackColor = true;
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
            this.Controls.Add(this.tabRaporlama);
            this.Controls.Add(this.dtpBitisTarihi);
            this.Controls.Add(this.lblBitisTarihi);
            this.Controls.Add(this.dtpBaslangicTarihi);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.lblBaslangicTarihi);
            this.Name = "Reports";
            this.Size = new System.Drawing.Size(1343, 705);
            this.Load += new System.EventHandler(this.Reports_Load);
            this.tabRaporlama.ResumeLayout(false);
            this.tabHastaRaporlari.ResumeLayout(false);
            this.tabHastaRaporlari.PerformLayout();
            this.tabTerapiRaporlari.ResumeLayout(false);
            this.tabTerapiRaporlari.PerformLayout();
            this.tabOperatorRaporlari.ResumeLayout(false);
            this.tabSistemRaporlari.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabHastaRaporlari;
        private System.Windows.Forms.TabPage tabTerapiRaporlari;
        private System.Windows.Forms.TabPage tabOperatorRaporlari;
        private System.Windows.Forms.TabPage tabSistemRaporlari;
        private System.Windows.Forms.TabPage tabKarsilastirmaliRapor;
        private System.Windows.Forms.ComboBox cmbHastaSecimi;
        private System.Windows.Forms.ComboBox cmbTerapiBir;
        private System.Windows.Forms.ComboBox cmbHastaKarsilastirma;
        private System.Windows.Forms.Label lblTerapiBir;
        private System.Windows.Forms.Label lblHastaKarsilastirma;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblTerapiIki;
        private System.Windows.Forms.Button btnPdfAktar;
        private System.Windows.Forms.Button btnExcelAktar;
        private System.Windows.Forms.Label lblEposta;
        private System.Windows.Forms.TextBox txtEposta;
        private System.Windows.Forms.Button btnEpostaGonder;
        private System.Windows.Forms.Label lblDil;
        private System.Windows.Forms.ComboBox cmbDil;
        private System.Windows.Forms.DataGridView tabloRapor;
        private System.Windows.Forms.Button btnHastalariListele;
        private System.Windows.Forms.Button btnHastaDetayRaporu;
        private System.Windows.Forms.Label lblHastaSecimi;
        private System.Windows.Forms.Button btnHastaTerapiGecmisi;
        private System.Windows.Forms.Button btnLoadcellGrafikGoster;
        private System.Windows.Forms.Button btnTerapiSureleri;
        private System.Windows.Forms.Button btnTerapiSayilari;
        private System.Windows.Forms.Label lblTerapiHastaSeciniz;
        private System.Windows.Forms.ComboBox cmbTerapiHastaSecimi;
        private System.Windows.Forms.Label lblTerapi;
        private System.Windows.Forms.ComboBox cmbTerapiSecimi;
        private System.Windows.Forms.Button btnOperatorCalismaSaatleri;
        private System.Windows.Forms.Button btnOperatorTerapiIstatistik;
        private System.Windows.Forms.Button btnCihazPerformans;
        private System.Windows.Forms.Button btnKullanimIstatistikleri;
        private System.Windows.Forms.Button btnHataLoglari;
        private System.Windows.Forms.Button btnKarsilastirmaliRapor;
    }
}