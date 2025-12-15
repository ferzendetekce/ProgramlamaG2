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
                label11.Text = "Patient Close Information";
                label9.Text = "Phone Number:(+90)";
                label10.Text = "Date of Birth";
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
                label11.Text = "Hasta Yakın Bilgileri";
                label9.Text = "Telefon No:(+90)";
                label10.Text = "Doğum Tarihi";
            }
        }

        // --- KAYDET BUTONU ---
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // 1. Validasyonlar (Hata Denetimi)
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Please fill all fields!" : "Lütfen tüm alanları doldurunuz!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox2.Text.Length != 11)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "ID must be 11 digits!" : "TC Kimlik No 11 haneli olmalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            bool sonuc = db.UpdatePatientByTC(
                 textBox2.Text.Trim(), // Referans TC
                 ad, soyad, txtmail.Text, textBox3.Text, textBox8.Text,
                 boy, kilo, ayak, kalcaDiz, dizTopuk
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

                textBox4.Text = row.Cells["kilo_kg"].Value?.ToString();
                textBox5.Text = row.Cells["boy_cm"].Value?.ToString();
                textBox7.Text = row.Cells["ayak_no"].Value?.ToString();
                textBox11.Text = row.Cells["kalca_diz_mesafesi"].Value?.ToString();
                textBox6.Text = row.Cells["diz_topuk_mesafesi"].Value?.ToString();
                // Set gender combobox based on DB value if present
                var genderDbVal = row.Cells["cinsiyet"].Value?.ToString();
                if (!string.IsNullOrEmpty(genderDbVal))
                {
                    try { comboBox2.SelectedValue = genderDbVal; } catch { /* ignore if value not found */ }
                }
            }
        }

        // Yardımcı Metod: Sayısal Kontrol
        private bool CheckNumeric(TextBox box, string fieldName)
        {
            string val = box.Text.Trim();
            if (!string.IsNullOrEmpty(val) && !val.All(char.IsDigit))
            {
                MessageBox.Show($"{fieldName} sadece rakam içermelidir!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}