using DevicesControllerApp.Database;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DevicesControllerApp.Raporlama
{
    public partial class Reports : UserControl
    {
        private readonly DatabaseManager _db;

        public Reports()
        {
            InitializeComponent();
            _db = new DatabaseManager();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı testi
            if (_db.TestConnection())
                MessageBox.Show("PostgreSQL bağlantısı başarılı!", "Bağlantı Testi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Bağlantı başarısız!", "Bağlantı Testi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Combobox ve tablo ayarları
            LoadPatients();
            ConfigureGrid();
            ApplyButtonStyles();
            tabRaporlama.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabRaporlama.DrawItem += TabRaporlama_DrawItem;
        }


        private void ConfigureGrid()
        {
            tabloRapor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabloRapor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tabloRapor.ReadOnly = true;
            tabloRapor.MultiSelect = false;
        }
        private void LoadPatients()
        {
            try
            {
                DataTable dt = _db.GetAllPatients();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Sistemde aktif hasta kaydı bulunamadı.",
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                cmbHastaSecimi.DisplayMember = "ad_soyad";
                cmbHastaSecimi.ValueMember = "id";
                cmbHastaSecimi.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hasta listesi yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHastalariListele_Click(object sender, EventArgs e)
        {
            try
            {
                // Tarih kontrolü
                DateTime baslangic = dtpBaslangicTarihi.Value.Date;
                DateTime bitis = dtpBitisTarihi.Value.Date;

                if (baslangic > bitis)
                {
                    MessageBox.Show("Başlangıç tarihi, bitiş tarihinden sonra olamaz!",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kullanıcıya seçenek sun
                DialogResult result = MessageBox.Show(
                    $"Tarih Aralığı: {baslangic:dd.MM.yyyy} - {bitis:dd.MM.yyyy}\n\n" +
                    "Sadece aktif hastaları mı listelemek istiyorsunuz?\n\n" +
                    "EVET: Sadece aktif hastalar\n" +
                    "HAYIR: Tüm hastalar (aktif + pasif)",
                    "Hasta Filtreleme",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                    return;

                bool onlyActive = (result == DialogResult.Yes);

                // Bitiş tarihini günün sonuna ayarla
                DateTime bitisZamani = bitis.AddDays(1).AddSeconds(-1);

                // Tarih aralığına göre verileri getir
                DataTable dt = _db.GetPatientsByDateRange(baslangic, bitisZamani, onlyActive);

                // DataGridView'e aktar
                tabloRapor.DataSource = dt;

                // Bilgi mesajı
                string statusText = onlyActive ? "aktif " : "";
                string dateRangeText = $"{baslangic:dd.MM.yyyy} - {bitis:dd.MM.yyyy}";

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        $"📅 Tarih Aralığı: {dateRangeText}\n\n" +
                        $"Seçilen tarih aralığında kayıtlı {statusText}hasta bulunamadı.",
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        $"📅 Tarih Aralığı: {dateRangeText}\n" +
                        $"📊 Toplam Kayıt: {dt.Rows.Count} {statusText}hasta\n\n" +
                        "Veriler başarıyla listelendi.",
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Kolon genişliklerini optimize et
                OptimizeColumnWidths();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hasta listesi yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// DataGridView kolon genişliklerini içeriğe göre optimize eder
        /// </summary>
        private void OptimizeColumnWidths()
        {
            try
            {
                foreach (DataGridViewColumn column in tabloRapor.Columns)
                {
                    // ID kolonunu dar tut
                    if (column.HeaderText == "ID")
                    {
                        column.Width = 50;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    }
                    // TC/Pasaport No için
                    else if (column.HeaderText == "TC/Pasaport No")
                    {
                        column.Width = 120;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    }
                    // Adres kolonunu geniş tut
                    else if (column.HeaderText == "Adres")
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    // Tarih kolonları için
                    else if (column.HeaderText.Contains("Tarih"))
                    {
                        column.Width = 100;
                        column.DefaultCellStyle.Format = "dd.MM.yyyy";
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    }
                    // Boy, Kilo gibi sayısal değerler
                    else if (column.HeaderText.Contains("(cm)") || column.HeaderText.Contains("(kg)") || column.HeaderText.Contains("No"))
                    {
                        column.Width = 80;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    }
                    // Diğer kolonlar
                    else
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata olsa bile devam et
                System.Diagnostics.Debug.WriteLine($"Kolon genişliği ayarlanırken hata: {ex.Message}");
            }
        }

        // 🔹 Butonlara modern renk ve stil uygular
        // 🔹 Butonlara modern renk ve stil uygular
        private void ApplyButtonStyles()
        {
            Color secondary = ColorTranslator.FromHtml("#2C7BE5"); // Diğer buton rengi
            Color hover = ColorTranslator.FromHtml("#0078D4");     // Hover rengi

            // Alt işlem butonları
            foreach (var btn in new[] { btnPdfAktar, btnExcelAktar, btnEpostaGonder })
            {
                btn.BackColor = secondary;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;

                // Hover efekti
                btn.MouseEnter += (s, e) => ((Button)s).BackColor = hover;
                btn.MouseLeave += (s, e) => ((Button)s).BackColor = secondary;
            }

            // TabPage içindeki butonlar için de aynı stili uygula
            foreach (TabPage tab in tabRaporlama.TabPages)
            {
                foreach (Control ctrl in tab.Controls)
                {
                    if (ctrl is Button btn)
                    {
                        btn.BackColor = ColorTranslator.FromHtml("#2C7BE5");
                        btn.ForeColor = Color.White;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 0;

                        btn.MouseEnter += (s, e) => ((Button)s).BackColor = ColorTranslator.FromHtml("#0078D4");
                        btn.MouseLeave += (s, e) => ((Button)s).BackColor = ColorTranslator.FromHtml("#2C7BE5");
                    }
                }
            }
        }
        private void TabRaporlama_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage currentTab = tabRaporlama.TabPages[e.Index];
            Rectangle tabRect = e.Bounds;
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // Renkler
            Color backColor = isSelected ? ColorTranslator.FromHtml("#005A9E") : ColorTranslator.FromHtml("#E0E0E0");
            Color textColor = isSelected ? Color.White : Color.Black;

            // Sekme arka planını boya
            using (SolidBrush brush = new SolidBrush(backColor))
                e.Graphics.FillRectangle(brush, tabRect);

            // Sekme kenarlığını çerçevele
            e.Graphics.DrawRectangle(Pens.Gray, tabRect);

            // Sekme başlığını ortala ve yazdır
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            using (SolidBrush textBrush = new SolidBrush(textColor))
                e.Graphics.DrawString(currentTab.Text, Font, textBrush, tabRect, sf);
        }
        private void tabRaporlama_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabRaporlama.TabPages)
            {
                tab.BackColor = Color.WhiteSmoke; // pasif sekme içi
            }

            tabRaporlama.SelectedTab.BackColor = ColorTranslator.FromHtml("#F0F8FF"); // aktif sekme içi (açık mavi ton)
        }

        private void btnHastaDetayRaporu_Click(object sender, EventArgs e)
        {
            // Hasta seçimi kontrolü
            if (cmbHastaSecimi.SelectedValue == null)
            {
                MessageBox.Show("Lütfen önce bir hasta seçiniz.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int hastaId = Convert.ToInt32(cmbHastaSecimi.SelectedValue);

                // Hasta detaylarını getir
                DataRow hastaBilgi = _db.GetPatientDetailById(hastaId);

                if (hastaBilgi == null)
                {
                    MessageBox.Show("Hasta bilgileri bulunamadı.",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kullanıcıya seçenek sun
                DialogResult result = MessageBox.Show(
                    "Hasta detayları nasıl görüntülensin?\n\n" +
                    "EVET: Detaylı rapor formatında\n" +
                    "HAYIR: Tablo formatında",
                    "Görüntüleme Seçeneği",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                    return;

                if (result == DialogResult.Yes)
                {
                    // Detaylı rapor formatında göster
                    ShowPatientDetailReport(hastaBilgi);
                }
                else
                {
                    // Tablo formatında göster
                    ShowPatientDetailInGrid(hastaBilgi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hasta detay raporu oluşturulurken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hasta detay raporunu formatlanmış şekilde gösterir
        /// </summary>
        private void ShowPatientDetailReport(DataRow hastaBilgi)
        {
            try
            {
                // Tarih formatı
                string dogumTarihi = hastaBilgi["dogum_tarihi"] != DBNull.Value
                    ? Convert.ToDateTime(hastaBilgi["dogum_tarihi"]).ToString("dd.MM.yyyy")
                    : "Belirtilmemiş";

                string kayitTarihi = hastaBilgi["kayit_tarihi"] != DBNull.Value
                    ? Convert.ToDateTime(hastaBilgi["kayit_tarihi"]).ToString("dd.MM.yyyy HH:mm")
                    : "Belirtilmemiş";

                // Yaş hesaplama
                int yas = 0;
                if (hastaBilgi["dogum_tarihi"] != DBNull.Value)
                {
                    DateTime dogum = Convert.ToDateTime(hastaBilgi["dogum_tarihi"]);
                    yas = DateTime.Now.Year - dogum.Year;
                    if (DateTime.Now < dogum.AddYears(yas)) yas--;
                }

                string hastaAdi = $"{hastaBilgi["ad"]} {hastaBilgi["soyad"]}";

                // Rapor metni oluştur
                string rapor = $@"
╔══════════════════════════════════════════════════════════════════════
║                    HASTA DETAY RAPORU
╚══════════════════════════════════════════════════════════════════════

📋 KİMLİK BİLGİLERİ
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    TC/Pasaport No    : {hastaBilgi["tc_pasaport_no"]}
    Ad                : {hastaBilgi["ad"]}
    Soyad             : {hastaBilgi["soyad"]}
    Doğum Tarihi      : {dogumTarihi}
    Yaş               : {yas}
    Cinsiyet          : {hastaBilgi["cinsiyet"]}

📞 İLETİŞİM BİLGİLERİ
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    Telefon           : {hastaBilgi["telefon"]}
    E-Posta           : {hastaBilgi["eposta"]}
    Adres             : {hastaBilgi["adres"]}

🏥 TIBBİ BİLGİLER
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    Tanı              : {hastaBilgi["tani"]}

📏 FİZİKSEL ÖLÇÜLER
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    Boy               : {hastaBilgi["boy"]} cm
    Kilo              : {hastaBilgi["kilo"]} kg
    Ayak Numarası     : {hastaBilgi["ayak_numarasi"]}
    Kalça-Diz Mesafesi: {hastaBilgi["kalca_diz_mesafesi"]} cm
    Diz-Topuk Mesafesi: {hastaBilgi["diz_topuk_mesafesi"]} cm

ℹ️  SİSTEM BİLGİLERİ
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    Durum             : {(Convert.ToBoolean(hastaBilgi["aktif"]) ? "✓ Aktif" : "✗ Pasif")}
    Kayıt Tarihi      : {kayitTarihi}
    Hasta ID          : {hastaBilgi["id"]}

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Rapor Oluşturulma Zamanı: {DateTime.Now:dd.MM.yyyy HH:mm:ss}
";

                // Formu göster
                FormHastaDetayRaporu form = new FormHastaDetayRaporu(hastaAdi, rapor);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rapor oluşturulurken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hasta detaylarını DataGridView'de gösterir
        /// </summary>
        private void ShowPatientDetailInGrid(DataRow hastaBilgi)
        {
            try
            {
                // Yeni bir DataTable oluştur (key-value formatında)
                DataTable dtDetail = new DataTable();
                dtDetail.Columns.Add("Özellik", typeof(string));
                dtDetail.Columns.Add("Değer", typeof(string));

                // Verileri ekle
                dtDetail.Rows.Add("ID", hastaBilgi["id"].ToString());
                dtDetail.Rows.Add("TC/Pasaport No", hastaBilgi["tc_pasaport_no"].ToString());
                dtDetail.Rows.Add("Ad", hastaBilgi["ad"].ToString());
                dtDetail.Rows.Add("Soyad", hastaBilgi["soyad"].ToString());

                string dogumTarihi = hastaBilgi["dogum_tarihi"] != DBNull.Value
                    ? Convert.ToDateTime(hastaBilgi["dogum_tarihi"]).ToString("dd.MM.yyyy")
                    : "Belirtilmemiş";
                dtDetail.Rows.Add("Doğum Tarihi", dogumTarihi);

                // Yaş hesaplama
                if (hastaBilgi["dogum_tarihi"] != DBNull.Value)
                {
                    DateTime dogum = Convert.ToDateTime(hastaBilgi["dogum_tarihi"]);
                    int yas = DateTime.Now.Year - dogum.Year;
                    if (DateTime.Now < dogum.AddYears(yas)) yas--;
                    dtDetail.Rows.Add("Yaş", yas.ToString());
                }

                dtDetail.Rows.Add("Cinsiyet", hastaBilgi["cinsiyet"].ToString());
                dtDetail.Rows.Add("Telefon", hastaBilgi["telefon"].ToString());
                dtDetail.Rows.Add("E-Posta", hastaBilgi["eposta"].ToString());
                dtDetail.Rows.Add("Adres", hastaBilgi["adres"].ToString());
                dtDetail.Rows.Add("Tanı", hastaBilgi["tani"].ToString());
                dtDetail.Rows.Add("Boy (cm)", hastaBilgi["boy"].ToString());
                dtDetail.Rows.Add("Kilo (kg)", hastaBilgi["kilo"].ToString());
                dtDetail.Rows.Add("Ayak Numarası", hastaBilgi["ayak_numarasi"].ToString());
                dtDetail.Rows.Add("Kalça-Diz Mesafesi (cm)", hastaBilgi["kalca_diz_mesafesi"].ToString());
                dtDetail.Rows.Add("Diz-Topuk Mesafesi (cm)", hastaBilgi["diz_topuk_mesafesi"].ToString());
                dtDetail.Rows.Add("Durum", Convert.ToBoolean(hastaBilgi["aktif"]) ? "Aktif" : "Pasif");

                string kayitTarihi = hastaBilgi["kayit_tarihi"] != DBNull.Value
                    ? Convert.ToDateTime(hastaBilgi["kayit_tarihi"]).ToString("dd.MM.yyyy HH:mm")
                    : "Belirtilmemiş";
                dtDetail.Rows.Add("Kayıt Tarihi", kayitTarihi);

                // DataGridView'e aktar
                tabloRapor.DataSource = dtDetail;

                // Kolon ayarları
                tabloRapor.Columns[0].Width = 200; // Özellik kolonu
                tabloRapor.Columns[0].DefaultCellStyle.Font = new Font(tabloRapor.Font, FontStyle.Bold);
                tabloRapor.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Değer kolonu

                MessageBox.Show($"Hasta detayları tabloda görüntülendi.\n\nHasta: {hastaBilgi["ad"]} {hastaBilgi["soyad"]}",
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tablo formatlanırken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHastaTerapiGecmisi_Click(object sender, EventArgs e)
        {
            // Hasta seçimi kontrolü
            if (cmbHastaSecimi.SelectedValue == null)
            {
                MessageBox.Show("Lütfen önce bir hasta seçiniz.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tarih kontrolü
                DateTime baslangic = dtpBaslangicTarihi.Value.Date;
                DateTime bitis = dtpBitisTarihi.Value.Date;

                if (baslangic > bitis)
                {
                    MessageBox.Show("Başlangıç tarihi, bitiş tarihinden sonra olamaz!",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int hastaId = Convert.ToInt32(cmbHastaSecimi.SelectedValue);

                // Bitiş tarihini günün sonuna ayarla
                DateTime bitisZamani = bitis.AddDays(1).AddSeconds(-1);

                // Önce seçilen tarih aralığında terapi ara
                DataTable terapiGecmisiFiltreli = _db.GetPatientTherapyHistoryByDateRange(hastaId, baslangic, bitisZamani);

                DataTable terapiGecmisi;
                string uyariMesaji = "";

                if (terapiGecmisiFiltreli == null || terapiGecmisiFiltreli.Rows.Count == 0)
                {
                    // Seçilen tarih aralığında terapi yok, tüm terapileri getir
                    terapiGecmisi = _db.GetPatientTherapyHistoryDetailed(hastaId);

                    if (terapiGecmisi == null || terapiGecmisi.Rows.Count == 0)
                    {
                        MessageBox.Show("Bu hasta için hiç terapi kaydı bulunamadı.",
                            "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Hastanın terapi tarih aralığını al
                    var (ilkTerapi, sonTerapi, toplamTerapi) = _db.GetPatientTherapyDateRange(hastaId);

                    if (ilkTerapi.HasValue && sonTerapi.HasValue)
                    {
                        uyariMesaji = $"⚠️ UYARI: Seçilen tarih aralığında ({baslangic:dd.MM.yyyy} - {bitis:dd.MM.yyyy}) terapi kaydı bulunamadı!\n\n" +
                                     $"Bu hastanın terapileri şu tarihler arasında:\n" +
                                     $"📅 İlk Terapi: {ilkTerapi:dd.MM.yyyy}\n" +
                                     $"📅 Son Terapi: {sonTerapi:dd.MM.yyyy}\n" +
                                     $"📊 Toplam Terapi: {toplamTerapi} seans\n\n" +
                                     $"Tüm terapiler listelenecek.";

                        MessageBox.Show(uyariMesaji, "Tarih Aralığı Uyarısı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Seçilen tarih aralığında terapi bulundu
                    terapiGecmisi = terapiGecmisiFiltreli;
                }

                // Hasta adını al
                string hastaAdi = terapiGecmisi.Rows[0]["hasta_adi"].ToString();

                // Kullanıcıya seçenek sun
                string tarihBilgisi = terapiGecmisiFiltreli != null && terapiGecmisiFiltreli.Rows.Count > 0
                    ? $"📅 Tarih Aralığı: {baslangic:dd.MM.yyyy} - {bitis:dd.MM.yyyy}\n📊 Bulunan Terapi: {terapiGecmisi.Rows.Count} seans\n\n"
                    : $"📊 Tüm Terapiler Gösteriliyor: {terapiGecmisi.Rows.Count} seans\n\n";

                DialogResult result = MessageBox.Show(
                    tarihBilgisi +
                    "Terapi geçmişi nasıl görüntülensin?\n\n" +
                    "EVET: Detaylı rapor formatında\n" +
                    "HAYIR: Tablo formatında",
                    "Görüntüleme Seçeneği",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                    return;

                if (result == DialogResult.Yes)
                {
                    // Detaylı rapor formatında göster
                    ShowTherapyHistoryReportWithDateInfo(terapiGecmisi, hastaAdi, baslangic, bitis,
                        terapiGecmisiFiltreli != null && terapiGecmisiFiltreli.Rows.Count > 0);
                }
                else
                {
                    // Tablo formatında göster
                    ShowTherapyHistoryInGrid(terapiGecmisi, hastaAdi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terapi geçmişi raporu oluşturulurken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbHastaSecimi_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Boş
        }

        private void tabloRapor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Boş
        }

        /// <summary>
        /// Terapi geçmişini tarih bilgisi ile formatlanmış şekilde gösterir
        /// </summary>
        private void ShowTherapyHistoryReportWithDateInfo(DataTable terapiGecmisi, string hastaAdi,
            DateTime baslangic, DateTime bitis, bool tarihFiltresiAktif)
        {
            try
            {
                System.Text.StringBuilder rapor = new System.Text.StringBuilder();

                rapor.AppendLine("╔══════════════════════════════════════════════════════════════════════");
                rapor.AppendLine("║              HASTA TERAPİ GEÇMİŞİ RAPORU");
                rapor.AppendLine("╚══════════════════════════════════════════════════════════════════════");
                rapor.AppendLine();
                rapor.AppendLine($"👤 Hasta: {hastaAdi}");

                if (tarihFiltresiAktif)
                {
                    rapor.AppendLine($"📅 Tarih Aralığı: {baslangic:dd.MM.yyyy} - {bitis:dd.MM.yyyy}");
                }
                else
                {
                    rapor.AppendLine($"📅 TÜM TERAPİLER (Tarih filtresi uygulanmadı)");
                }

                rapor.AppendLine($"📊 Toplam Terapi Sayısı: {terapiGecmisi.Rows.Count}");
                rapor.AppendLine();

                int siraNo = 1;
                foreach (DataRow row in terapiGecmisi.Rows)
                {
                    rapor.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                    rapor.AppendLine($"🔹 TERAPİ #{siraNo}");
                    rapor.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

                    // Tarih ve Süre Bilgileri
                    string baslangicZaman = row["baslangic_zamani"] != DBNull.Value
                        ? Convert.ToDateTime(row["baslangic_zamani"]).ToString("dd.MM.yyyy HH:mm")
                        : "Belirtilmemiş";

                    string bitisZaman = row["bitis_zamani"] != DBNull.Value
                        ? Convert.ToDateTime(row["bitis_zamani"]).ToString("dd.MM.yyyy HH:mm")
                        : "Devam ediyor";

                    string sure = row["terapi_suresi"] != DBNull.Value
                        ? row["terapi_suresi"].ToString() + " dk"
                        : "-";

                    rapor.AppendLine("⏱️  ZAMAN BİLGİLERİ");
                    rapor.AppendLine($"   Başlangıç         : {baslangicZaman}");
                    rapor.AppendLine($"   Bitiş             : {bitisZaman}");
                    rapor.AppendLine($"   Süre              : {sure}");
                    rapor.AppendLine();

                    // Terapi Parametreleri
                    rapor.AppendLine("⚙️  TERAPİ PARAMETRELERİ");
                    rapor.AppendLine($"   Hız               : {row["terapi_hizi"]} km/h");
                    rapor.AppendLine($"   Azaltılan Ağırlık : {row["azaltılan_agirlik"]} kg");
                    rapor.AppendLine($"   Destek Barı       : {row["destek_bari_yuksekligi"]} cm");
                    rapor.AppendLine($"   Ayak Numarası     : {row["ayak_numarasi_ayari"]}");
                    rapor.AppendLine();

                    // Durum ve Operatör
                    rapor.AppendLine("ℹ️  DURUM VE OPERATÖR");
                    rapor.AppendLine($"   Durum             : {row["terapi_durumu"]}");
                    rapor.AppendLine($"   Operatör          : {row["operator_adi"]}");

                    if (row["notlar"] != DBNull.Value && !string.IsNullOrEmpty(row["notlar"].ToString()))
                    {
                        rapor.AppendLine($"   Notlar            : {row["notlar"]}");
                    }

                    rapor.AppendLine();
                    siraNo++;
                }

                rapor.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                rapor.AppendLine($"Rapor Oluşturulma Zamanı: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");

                // Formu göster
                FormTerapiGecmisiRaporu form = new FormTerapiGecmisiRaporu(hastaAdi, rapor.ToString());
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rapor oluşturulurken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Terapi geçmişini DataGridView'de gösterir
        /// </summary>
        private void ShowTherapyHistoryInGrid(DataTable terapiGecmisi, string hastaAdi)
        {
            try
            {
                // Görüntüleme için yeni tablo oluştur
                DataTable dtDisplay = new DataTable();
                dtDisplay.Columns.Add("Sıra", typeof(int));
                dtDisplay.Columns.Add("Başlangıç", typeof(string));
                dtDisplay.Columns.Add("Bitiş", typeof(string));
                dtDisplay.Columns.Add("Süre (dk)", typeof(string));
                dtDisplay.Columns.Add("Hız (km/h)", typeof(string));
                dtDisplay.Columns.Add("Azaltılan Ağırlık (kg)", typeof(string));
                dtDisplay.Columns.Add("Destek Barı (cm)", typeof(string));
                dtDisplay.Columns.Add("Ayak No", typeof(string));
                dtDisplay.Columns.Add("Durum", typeof(string));
                dtDisplay.Columns.Add("Operatör", typeof(string));
                dtDisplay.Columns.Add("Notlar", typeof(string));

                int siraNo = 1;
                foreach (DataRow row in terapiGecmisi.Rows)
                {
                    string baslangic = row["baslangic_zamani"] != DBNull.Value
                        ? Convert.ToDateTime(row["baslangic_zamani"]).ToString("dd.MM.yyyy HH:mm")
                        : "-";

                    string bitis = row["bitis_zamani"] != DBNull.Value
                        ? Convert.ToDateTime(row["bitis_zamani"]).ToString("dd.MM.yyyy HH:mm")
                        : "Devam ediyor";

                    string sure = row["terapi_suresi"] != DBNull.Value
                        ? row["terapi_suresi"].ToString()
                        : "-";

                    dtDisplay.Rows.Add(
                        siraNo,
                        baslangic,
                        bitis,
                        sure,
                        row["terapi_hizi"].ToString(),
                        row["azaltılan_agirlik"].ToString(),
                        row["destek_bari_yuksekligi"].ToString(),
                        row["ayak_numarasi_ayari"].ToString(),
                        row["terapi_durumu"].ToString(),
                        row["operator_adi"].ToString(),
                        row["notlar"] != DBNull.Value ? row["notlar"].ToString() : "-"
                    );

                    siraNo++;
                }

                // DataGridView'e aktar
                tabloRapor.DataSource = dtDisplay;

                // Kolon ayarları
                tabloRapor.Columns["Sıra"].Width = 50;
                tabloRapor.Columns["Başlangıç"].Width = 130;
                tabloRapor.Columns["Bitiş"].Width = 130;
                tabloRapor.Columns["Süre (dk)"].Width = 70;
                tabloRapor.Columns["Hız (km/h)"].Width = 80;
                tabloRapor.Columns["Azaltılan Ağırlık (kg)"].Width = 130;
                tabloRapor.Columns["Destek Barı (cm)"].Width = 110;
                tabloRapor.Columns["Ayak No"].Width = 70;
                tabloRapor.Columns["Durum"].Width = 100;
                tabloRapor.Columns["Operatör"].Width = 120;
                tabloRapor.Columns["Notlar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                MessageBox.Show($"Terapi geçmişi tabloda görüntülendi.\n\nHasta: {hastaAdi}\nToplam Terapi: {terapiGecmisi.Rows.Count}",
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tablo formatlanırken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}