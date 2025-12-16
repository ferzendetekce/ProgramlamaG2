using DevicesControllerApp.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; // Regex için
using System.Globalization; // For numeric parsing/formatting

namespace DevicesControllerApp.Hasta_kayit
{
    public partial class PatientRegistration : UserControl
    {
        // DatabaseManager örneğini çağırıyoruz
        DatabaseManager db = DatabaseManager.Instance;

        public PatientRegistration()
        {
            InitializeComponent();
        }

        // --- FORM YÜKLENİRKEN (LOAD) ---
        private void PatientRegistration_Load(object sender, EventArgs e)
        {
            // Şehirleri ComboBox'a doldur
            comboBox1.DataSource = db.GetAllCitys();
            comboBox1.DisplayMember = "sehir_adi";
            comboBox1.ValueMember = "plaka_kodu";

            // Cinsiyet combobox'unu önce veritabanından alınan izinli değerlerle doldurmaya çalış
            try
            {
                var allowed = db.GetAllowedValues("hasta_bilgileri", "cinsiyet");
                List<KeyValuePair<string, string>> genders;
                if (allowed != null && allowed.Count > 0)
                {
                    genders = allowed.Select(v => new KeyValuePair<string, string>(v, v)).ToList();
                }
                else
                {
                    // Fallback: önceki gösterim/değer eşlemesi
                    genders = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string,string>("Erkek / Man", "Erkek"),
                        new KeyValuePair<string,string>("Kadın / Woman", "Kadın")
                    };
                }

                comboBox2.DisplayMember = "Key"; // gösterilecek metin
                comboBox2.ValueMember = "Value"; // veritabanına gönderilecek değer
                comboBox2.DataSource = genders;
            }
            catch
            {
                // ignore and fallback to default
                comboBox2.DisplayMember = "Key";
                comboBox2.ValueMember = "Value";
                comboBox2.DataSource = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string,string>("Erkek / Man", "Erkek"),
                    new KeyValuePair<string,string>("Kadın / Woman", "Kadın")
                };
            }

            // Gri alanı (DataGridView2) doldur
            VerileriYenile();
        }

        // --- LİSTEYİ YENİLEME METODU ---
        private void VerileriYenile()
        {
            // Veritabanından verileri çek ve Grid'e bağla
            dataGridView2.DataSource = db.GetAllPatients();
        }

        // --- DİL DEĞİŞTİRME SEÇENEĞİ ---
        private void cmbDil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDil.SelectedItem != null)
            {
                if (cmbDil.SelectedItem.ToString() == "English")
                    DiliDegistir("en");
                else
                    DiliDegistir("tr");
            }
        }

        private void DiliDegistir(string dil)
        {
            if (dil == "en")
            {
                lblAdSoyad.Text = "Name Surname:";
                lblTc.Text = "ID Number:";
                this.Text = "Patient Registration";
                this.labele1.Text = "Language:";
                lblSehir.Text = "City:";
                btnKaydet.Text = "SAVE PATIENT";
                btnSil.Text = "DELETE PATIENT";
                label1.Text = "PATIENTS";
                button3.Text = "SEARCH PATIENT";
                button1.Text = "UPDATE PATIENT";
                label3.Text = "Address";
                label5.Text = "Weight";
                label6.Text = "Height";
                label4.Text = "Shoe Size";
                label7.Text = "Hip Knee Distance";
                label8.Text = "Knee Heel Distance";
                label2.Text = "Gender:";
                lblyakinad.Text = "Name";
                label13.Text = "Surname";
                label14.Text = "Degree of Kinship";
                label15.Text = "Phone Number:(+90)";
                label16.Text = "Disease Diagnosis";
                label11.Text = "PATIENT CLOSE INFORMATION";
                label9.Text = "Phone Number:(+90)";
                label10.Text = "Date of Birth";
                label12.Text = "PERSONAL INFORMATION";

            }
            else
            {
                lblAdSoyad.Text = "Ad Soyad:";
                lblTc.Text = "TC Kimlik No:";
                this.Text = "Hasta Kayıt";
                this.labele1.Text = "Dil Seçimi:";
                lblSehir.Text = "Şehir:";
                btnKaydet.Text = "HASTA KAYDET";
                btnSil.Text = "HASTA SİL";
                label1.Text = "HASTALAR";
                button3.Text = "HASTAYI ARA";
                button1.Text = "HASTAYI GÜNCELLE";
                label3.Text = "Adres";
                label5.Text = "Kilo";
                label6.Text = "Boy";
                label4.Text = "Ayak No";
                label7.Text = "Kalça Diz Mesafesi";
                label8.Text = "Diz Topuk Mesafesi";
                label2.Text = "Cinsiyet:";
                lblyakinad.Text = "Ad";
                label13.Text = "Soyad";
                label14.Text = "Yakınlık Derecesi";
                label15.Text = "Telefon No:(+90)";
                label16.Text = "Hastalık Tanısı";
                label11.Text = "HASTA YAKIN BİLGİLERİ";
                label9.Text = "Telefon No:(+90)";
                label10.Text = "Doğum Tarihi";
                label12.Text = "KİŞİSEL BİLGİLER";
            }
        }

        // --- KAYDET BUTONU ---
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            
            
            // 1. Validasyonlar (Hata Denetimi)
            //textbox blank
            string tel = textBox8.Text;
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text)
                 || string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text) || string.IsNullOrEmpty(textBox10.Text) || string.IsNullOrEmpty(textBox11.Text) || string.IsNullOrEmpty(textBox12.Text))
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Please fill all fields!" : "Lütfen tüm alanları doldurunuz!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //---------------------------
            //adsoyad kontrol
            string adSoyad = textBox1.Text.Trim();

            foreach (char c in adSoyad)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    string uyari = (btnKaydet.Text == "SAVE PATIENT")
                        ? "Name and surname must contain only letters!"
                        : "Ad Soyad sadece harf içermelidir!";

                    MessageBox.Show(uyari, "Hata/Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    textBox1.Focus();
                    return;
                }
            }
            //--------------------------------------------------
            //tc
             if (textBox2.Text.Length != 11)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "ID must be 11 digits!" : "TC Kimlik No 11 haneli olmalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
                
            }
            foreach (char c in textBox2.Text)
            {
                if (!char.IsDigit(c))
                {
                    string uyari = (btnKaydet.Text == "SAVE PATIENT")
                        ? "ID must contain only digits!"
                        : "TC Kimlik No sadece rakamlardan oluşmalıdır!";

                    MessageBox.Show(uyari, "Hata/Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    textBox2.Focus();
                    return;
                }
            }
            //-----

            //TELEFON

            if (tel.Length != 10)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT")
                    ? "Phone number must be 10 digits after +90!"
                    : "+90'dan sonra telefon numarası 10 haneli olmalıdır!";

                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox8.Focus();
                return;
            }
            for (int i = 1; i < tel.Length; i++)
            {
                if (!char.IsDigit(tel[i]))
                {
                    string uyari = (btnKaydet.Text == "SAVE PATIENT")
                        ? "Phone number cannot contain letters!"
                        : "Telefon numarası harf içeremez!";

                    MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox8.Focus();
                    return;
                }
            }
            //----------------------
            //mail
            string email = txtmail.Text.Trim();

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(email, pattern))
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT")
                    ? "Please enter a valid email address!"
                    : "Geçerli bir e-posta adresi giriniz!";

                MessageBox.Show(uyari, "Hata/Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtmail.Focus();
                return;
            }
            //--
            //Hastalık tanısı combo
            if (comboBox3.SelectedIndex == -1)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT")
                    ? "Please select a disease!"
                    : "Lütfen bir hastalık seçiniz!";

                MessageBox.Show(uyari, "Hata/Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                comboBox3.Select();
                return;
            }

            //------
            //Hasta yakin combo
            if (comboBox4.SelectedIndex == -1)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT")
                    ? "Please select a relative!"
                    : "Lütfen bir yakın seçiniz!";

                MessageBox.Show(uyari, "Hata/Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                comboBox4.Select();
                return;
            }

            // Sayısal alan kontrolleri
            if (!CheckNumeric(textBox4, "Weight/Kilo") || !CheckNumeric(textBox5, "Height/Boy") ||
                !CheckNumeric(textBox7, "Shoe Size/Ayak No") || !CheckNumeric(textBox11, "Hip Knee/Kalça Diz") ||
                !CheckNumeric(textBox6, "Knee Heel/Diz Topuk"))
            {
                return;
            }

            // Ad ve Soyadı Ayırma
            string tamAd = textBox1.Text.Trim();
            string ad = tamAd;
            string soyad = "";
            if (tamAd.Contains(" "))
            {
                int sonBosluk = tamAd.LastIndexOf(' ');
                ad = tamAd.Substring(0, sonBosluk);
                soyad = tamAd.Substring(sonBosluk + 1);
            }

            // Değerleri Parse Etme
            decimal.TryParse(textBox4.Text, out decimal kilo);
            decimal.TryParse(textBox5.Text, out decimal boy);
            decimal.TryParse(textBox7.Text, out decimal ayak);
            decimal.TryParse(textBox11.Text, out decimal kalcaDiz);
            decimal.TryParse(textBox6.Text, out decimal dizTopuk);

            int sehirPlaka = 0;
            if (comboBox1.SelectedValue != null)
                int.TryParse(comboBox1.SelectedValue.ToString(), out sehirPlaka);

            // Normalize gender value to match database check constraint (use SelectedValue now)
            string genderValue = comboBox2.SelectedValue != null ? comboBox2.SelectedValue.ToString() : comboBox2.Text ?? string.Empty;

            // Veritabanına Kaydetme
            bool sonuc = db.AddPatient(
                textBox2.Text.Trim(), // TC
                ad, soyad,
                dateTimePicker1.Value, // Doğum Tarihi
                txtmail.Text.Trim(), // Mail
                textBox3.Text.Trim(), // Adres
                textBox8.Text.Trim(), // Telefon
                genderValue, // Cinsiyet (normalize edilmiş)
                textBox9.Text.Trim(), // Yakın Ad
                textBox10.Text.Trim(), // Yakın Soyad
                comboBox4.Text, // Yakınlık
                textBox12.Text.Trim(), // Yakın Tel
                comboBox3.Text, // Tanı
                boy, kilo, ayak, kalcaDiz, dizTopuk,
                sehirPlaka
            );

            if (sonuc)
            {
                string mesaj = (btnKaydet.Text == "SAVE PATIENT") ? "Patient Saved." : "Hasta başarıyla kaydedildi.";
                MessageBox.Show(mesaj, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                VerileriYenile(); // Listeyi güncelle
            }
        }

        // --- SİL BUTONU (Mevcut kodunuzda button1_Click Sil butonu olarak atanmıştı) ---
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Lütfen silinecek kişinin TC Numarasını giriniz.");
                return;
            }

            if (MessageBox.Show("Silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (db.DeletePatientByTC(textBox2.Text))
                {
                    MessageBox.Show("Silme işlemi başarılı");
                    VerileriYenile(); // Listeyi Güncelle
                }
                else
                {
                    MessageBox.Show("Silme işlemi başarısız");
                }
            }
        }

        // Handler for btnSil (designer wired). Performs same deletion logic.
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Lütfen silinecek kişinin TC Numarasını giriniz.");
                return;
            }

            if (MessageBox.Show("Silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (db.DeletePatientByTC(textBox2.Text.Trim()))
                {
                    string mesaj = (btnKaydet.Text == "SAVE PATIENT") ? "Patient Deleted." : "Silme işlemi başarılı";
                    MessageBox.Show(mesaj, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    VerileriYenile(); // Listeyi Güncelle
                }
                else
                {
                    string hata = (btnKaydet.Text == "SAVE PATIENT") ? "Delete failed." : "Silme işlemi başarısız";
                    MessageBox.Show(hata, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- GÜNCELLE BUTONU (button1_Click_1 Güncelle butonu olarak atanmıştı) ---
        private void button1_Click_1(object sender, EventArgs e)
        {
            // Ad ve Soyadı Ayırma
            string tamAd = textBox1.Text.Trim();
            string ad = tamAd;
            string soyad = "";
            if (tamAd.Contains(" "))
            {
                int sonBosluk = tamAd.LastIndexOf(' ');
                ad = tamAd.Substring(0, sonBosluk);
                soyad = tamAd.Substring(sonBosluk + 1);
            }

            decimal.TryParse(textBox4.Text, out decimal kilo);
            decimal.TryParse(textBox5.Text, out decimal boy);
            decimal.TryParse(textBox7.Text, out decimal ayak);
            decimal.TryParse(textBox11.Text, out decimal kalcaDiz);
            decimal.TryParse(textBox6.Text, out decimal dizTopuk);

            // Yeni: hasta yakın ve doğum tarihi bilgilerini de gönder
            bool sonuc = db.UpdatePatientByTC(
                 textBox2.Text.Trim(), // Referans TC
                 ad, soyad,
                 dateTimePicker1.Value,
                 comboBox2.SelectedValue != null ? comboBox2.SelectedValue.ToString() : comboBox2.Text, // gender
                 txtmail.Text, textBox3.Text, textBox8.Text,
                 boy, kilo, ayak, kalcaDiz, dizTopuk,
                 textBox9.Text.Trim(), textBox10.Text.Trim(), comboBox4.Text, textBox12.Text.Trim(),
                 comboBox3.Text // diagnosis
            );

            if (sonuc)
            {
                MessageBox.Show("Güncelleme işlemi başarılı");
                VerileriYenile();
            }
            else
            {
                MessageBox.Show("Güncelleme işlemi başarısız");
            }
        }

        // --- LİSTEDEN SEÇİNCE KUTULARI DOLDURMA ---
        // Tasarım ekranında CellClick eventine bunu bağlamalısınız!
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                string ad = row.Cells["ad"].Value != DBNull.Value ? row.Cells["ad"].Value.ToString() : "";
                string soyad = row.Cells["soyad"].Value != DBNull.Value ? row.Cells["soyad"].Value.ToString() : "";
                textBox1.Text = ad + " " + soyad;

                textBox2.Text = row.Cells["tc"].Value?.ToString();
                txtmail.Text = row.Cells["e_mail"].Value?.ToString();
                textBox3.Text = row.Cells["adresi"].Value?.ToString();
                textBox8.Text = row.Cells["hasta_telefon_no"].Value?.ToString();

                // Numeric fields: try parse as decimal and format as int if whole, otherwise as double with up to 2 decimals
                Func<object, string> formatNumeric = (obj) =>
                {
                    if (obj == null || obj == DBNull.Value) return string.Empty;
                    if (obj is decimal decVal)
                    {
                        if (decimal.Truncate(decVal) == decVal)
                            return ((long)decVal).ToString(CultureInfo.CurrentCulture);
                        return decimal.Round(decVal, 2).ToString("0.##", CultureInfo.CurrentCulture);
                    }
                    if (obj is double dbl)
                    {
                        if (Math.Truncate(dbl) == dbl)
                            return ((long)dbl).ToString(CultureInfo.CurrentCulture);
                        return dbl.ToString("0.##", CultureInfo.CurrentCulture);
                    }
                    // fallback parse
                    if (decimal.TryParse(obj.ToString(), NumberStyles.Number, CultureInfo.CurrentCulture, out decimal parsed))
                    {
                        if (decimal.Truncate(parsed) == parsed)
                            return ((long)parsed).ToString(CultureInfo.CurrentCulture);
                        return decimal.Round(parsed, 2).ToString("0.##", CultureInfo.CurrentCulture);
                    }
                    return obj.ToString();
                };

                textBox4.Text = formatNumeric(row.Cells["kilo_kg"].Value);
                textBox5.Text = formatNumeric(row.Cells["boy_cm"].Value);
                textBox7.Text = formatNumeric(row.Cells["ayak_no"].Value);
                textBox11.Text = formatNumeric(row.Cells["kalca_diz_mesafesi"].Value);
                textBox6.Text = formatNumeric(row.Cells["diz_topuk_mesafesi"].Value);

                // Set birth date if available
                var dogumObj = row.Cells["dogum_tarihi"].Value;
                if (dogumObj != null && dogumObj != DBNull.Value)
                {
                    DateTime dt;
                    if (dogumObj is DateTime)
                        dt = (DateTime)dogumObj;
                    else if (!DateTime.TryParse(dogumObj.ToString(), out dt))
                        dt = dateTimePicker1.Value; // keep existing if parse fails

                    dateTimePicker1.Value = dt;
                }

                // Set gender combobox based on DB value if present
                var genderDbVal = row.Cells["cinsiyet"].Value?.ToString();
                if (!string.IsNullOrEmpty(genderDbVal))
                {
                    try { comboBox2.SelectedValue = genderDbVal; } catch { /* ignore if value not found */ }
                }

                // Fill patient relative info from DB
                var yakinAd = row.Cells["hasta_yakini_adi"].Value?.ToString() ?? string.Empty;
                var yakinSoyad = row.Cells["hasta_yakini_soyadi"].Value?.ToString() ?? string.Empty;
                var yakinNeyi = row.Cells["hasta_yakini_neyi"].Value?.ToString() ?? string.Empty;
                var yakinTel = row.Cells["hasta_yakini_telefon_no"].Value?.ToString() ?? string.Empty;

                textBox9.Text = yakinAd;
                textBox10.Text = yakinSoyad;
                textBox12.Text = yakinTel;

                // Try to select the relation in comboBox4; if not present add it and select
                if (!string.IsNullOrEmpty(yakinNeyi))
                {
                    int foundIndex = -1;
                    for (int i = 0; i < comboBox4.Items.Count; i++)
                    {
                        var itemText = comboBox4.Items[i]?.ToString();
                        if (!string.IsNullOrEmpty(itemText) && string.Equals(itemText.Trim(), yakinNeyi.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            foundIndex = i; break;
                        }
                    }
                    if (foundIndex >= 0)
                    {
                        comboBox4.SelectedIndex = foundIndex;
                    }
                    else
                    {
                        // add new display item equal to the DB value and select it
                        comboBox4.Items.Add(yakinNeyi);
                        comboBox4.SelectedItem = yakinNeyi;
                    }
                }
            }
        }

        // Yardımcı Metod: Sayısal Kontrol
        private bool CheckNumeric(TextBox box, string fieldName)
        {
            string val = box.Text.Trim();
            if (string.IsNullOrEmpty(val)) return true; // empty allowed (handled elsewhere)

            // Accept integers and decimals according to current culture
            if (!decimal.TryParse(val, NumberStyles.Number, CultureInfo.CurrentCulture, out _))
            {
                MessageBox.Show($"{fieldName} sadece sayısal bir değer olmalıdır (ör. 170 veya 170.5).", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                box.Focus();
                return false;
            }
            return true;
        }

        // --- GEREKSİZ BOŞ EVENTLER (Tasarım hatası vermemesi için silmedim) ---
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { MessageBox.Show(" Plaka no:" + comboBox1.SelectedValue + " Şehir:" + comboBox1.Text); }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) { } // Bunu CellClick ile değiştireceğiz
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void textBox8_TextChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e) { }
        private void textBox3_TextChanged_1(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void textBox7_TextChanged(object sender, EventArgs e) { }
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void textBox11_TextChanged(object sender, EventArgs e) { }
        private void groupBox2_Enter(object sender, EventArgs e) { }
        private void lblyakinad_Click(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e)
        {
            // Hastayı ara butonuna basıldığında: textBox1 içeriği ad soyad, textBox2 ise TC olabilir.
            string term = string.Empty;
            if (!string.IsNullOrEmpty(textBox2.Text))
                term = textBox2.Text.Trim();
            else if (!string.IsNullOrEmpty(textBox1.Text))
                term = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(term))
            {
                MessageBox.Show("Aramak için isim veya TC giriniz.");
                return;
            }

            // DataTable ile sorgu yap ve Grid'i güncelle
            var dt = db.SearchPatients(term);
            dataGridView2.DataSource = dt;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
    }
}