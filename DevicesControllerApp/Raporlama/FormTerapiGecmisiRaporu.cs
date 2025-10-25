using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevicesControllerApp.Raporlama
{
    public partial class FormTerapiGecmisiRaporu : Form
    {
        private System.ComponentModel.IContainer components = null;
        private RichTextBox txtRapor;
        private Button btnKapat;
        private Button btnKopyala;
        private Label lblBaslik;
        private PictureBox picIcon;
        private string hastaAdi;
        private string rapor;

        public FormTerapiGecmisiRaporu(string hastaAdi, string rapor)
        {
            this.hastaAdi = hastaAdi;
            this.rapor = rapor;

            InitializeComponent();
            LoadReport();
        }

        /// <summary>
        /// Raporu RichTextBox'a yükler ve formatlar
        /// </summary>
        private void LoadReport()
        {
            try
            {
                // Başlık güncelle
                lblBaslik.Text = $"Terapi Geçmişi - {hastaAdi}";
                this.Text = $"Terapi Geçmişi Raporu - {hastaAdi}";

                // Raporu RichTextBox'a yükle
                txtRapor.Text = rapor;

                // Renklendirme ve formatlama
                FormatReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rapor yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Raporu formatlar ve renklendirmeler uygular
        /// </summary>
        private void FormatReport()
        {
            try
            {
                // Başlıkları kalın ve mavi yap
                string[] basliklar = {
                    "ZAMAN BİLGİLERİ",
                    "TERAPİ PARAMETRELERİ",
                    "DURUM VE OPERATÖR"
                };

                foreach (string baslik in basliklar)
                {
                    int startIndex = 0;
                    while ((startIndex = txtRapor.Text.IndexOf(baslik, startIndex)) >= 0)
                    {
                        txtRapor.Select(startIndex, baslik.Length);
                        txtRapor.SelectionFont = new Font(txtRapor.Font, FontStyle.Bold);
                        txtRapor.SelectionColor = Color.DarkBlue;
                        startIndex += baslik.Length;
                    }
                }

                // Ana başlığı formatla
                int baslikIndex = txtRapor.Text.IndexOf("HASTA TERAPİ GEÇMİŞİ RAPORU");
                if (baslikIndex >= 0)
                {
                    txtRapor.Select(baslikIndex, "HASTA TERAPİ GEÇMİŞİ RAPORU".Length);
                    txtRapor.SelectionFont = new Font(txtRapor.Font.FontFamily, 12, FontStyle.Bold);
                    txtRapor.SelectionColor = Color.Navy;
                }

                // Terapi numaralarını vurgula
                int terapiIndex = 0;
                while ((terapiIndex = txtRapor.Text.IndexOf("TERAPİ #", terapiIndex)) >= 0)
                {
                    txtRapor.Select(terapiIndex, 10);
                    txtRapor.SelectionFont = new Font(txtRapor.Font, FontStyle.Bold);
                    txtRapor.SelectionColor = Color.DarkGreen;
                    terapiIndex += 10;
                }

                // Seçimi kaldır
                txtRapor.Select(0, 0);
            }
            catch
            {
                // Formatlama hatası olursa sessizce devam et
            }
        }

        private void btnKopyala_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtRapor.Text))
                {
                    Clipboard.SetText(txtRapor.Text);
                    MessageBox.Show("Rapor panoya kopyalandı!",
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kopyalanacak rapor bulunamadı.",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kopyalama sırasında hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtRapor = new RichTextBox();
            this.btnKapat = new Button();
            this.btnKopyala = new Button();
            this.lblBaslik = new Label();
            this.picIcon = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();

            // lblBaslik
            this.lblBaslik.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblBaslik.ForeColor = Color.Navy;
            this.lblBaslik.Location = new System.Drawing.Point(60, 15);
            this.lblBaslik.Size = new System.Drawing.Size(530, 25);
            this.lblBaslik.Text = "Terapi Geçmişi Raporu";

            // picIcon
            this.picIcon.Image = System.Drawing.SystemIcons.Information.ToBitmap();
            this.picIcon.Location = new System.Drawing.Point(20, 10);
            this.picIcon.Size = new System.Drawing.Size(32, 32);
            this.picIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            // txtRapor
            this.txtRapor.Font = new Font("Consolas", 10F);
            this.txtRapor.Location = new System.Drawing.Point(20, 50);
            this.txtRapor.ReadOnly = true;
            this.txtRapor.BackColor = Color.WhiteSmoke;
            this.txtRapor.BorderStyle = BorderStyle.FixedSingle;
            this.txtRapor.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.txtRapor.Size = new System.Drawing.Size(580, 530);
            this.txtRapor.TabIndex = 0;

            // btnKopyala
            this.btnKopyala.Text = "📋 Kopyala";
            this.btnKopyala.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnKopyala.BackColor = Color.SteelBlue;
            this.btnKopyala.ForeColor = Color.White;
            this.btnKopyala.FlatStyle = FlatStyle.Flat;
            this.btnKopyala.FlatAppearance.BorderSize = 0;
            this.btnKopyala.Size = new System.Drawing.Size(120, 35);
            this.btnKopyala.Location = new System.Drawing.Point(350, 595);
            this.btnKopyala.Cursor = Cursors.Hand;
            this.btnKopyala.TabIndex = 1;
            this.btnKopyala.Click += new System.EventHandler(this.btnKopyala_Click);

            // Hover efekti
            this.btnKopyala.MouseEnter += (s, e) => btnKopyala.BackColor = Color.DodgerBlue;
            this.btnKopyala.MouseLeave += (s, e) => btnKopyala.BackColor = Color.SteelBlue;

            // btnKapat
            this.btnKapat.Text = "✖ Kapat";
            this.btnKapat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnKapat.BackColor = Color.IndianRed;
            this.btnKapat.ForeColor = Color.White;
            this.btnKapat.FlatStyle = FlatStyle.Flat;
            this.btnKapat.FlatAppearance.BorderSize = 0;
            this.btnKapat.Size = new System.Drawing.Size(120, 35);
            this.btnKapat.Location = new System.Drawing.Point(480, 595);
            this.btnKapat.Cursor = Cursors.Hand;
            this.btnKapat.TabIndex = 2;
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);

            // Hover efekti
            this.btnKapat.MouseEnter += (s, e) => btnKapat.BackColor = Color.Firebrick;
            this.btnKapat.MouseLeave += (s, e) => btnKapat.BackColor = Color.IndianRed;

            // FormTerapiGecmisiRaporu
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(620, 650);
            this.Controls.Add(this.lblBaslik);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.txtRapor);
            this.Controls.Add(this.btnKopyala);
            this.Controls.Add(this.btnKapat);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Terapi Geçmişi Raporu";
            this.BackColor = Color.White;
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
        }
    }
}