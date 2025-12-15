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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using System.Text.RegularExpressions;// For Regex

namespace DevicesControllerApp.Hasta_kayit
{
    public partial class PatientRegistration : UserControl
    {
        Database.DatabaseManager db = Database.DatabaseManager.Instance;
        public PatientRegistration()
        {
            InitializeComponent();
            db.OpenConnection();
           dataGridView1.DataSource= db.GetAllCitys();
            comboBox1.DataSource = db.GetAllCitys();
            comboBox1.DisplayMember = "sehir_adi";
            comboBox1.ValueMember = "plaka_kodu";
        }
        private void cmbDil_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçilen dile göre metodu çağır
            if (cmbDil.SelectedItem.ToString() == "English")
            {
                DiliDegistir("en");
            }
            else
            {
                DiliDegistir("tr");
            }
        }

        private void DiliDegistir(string dil)
        {
            if (dil == "en")
            {
                // İngilizce Metinler
                lblAdSoyad.Text = "Name Surname:";
                lblTc.Text = "ID Number:";
                // btnKaydet.Text = "Save";
                this.Text = "Patient Registration"; // Form Başlığı
                this.labele1.Text
                 = "Language:";
                lblSehir.Text = "City:";
                btnKaydet.Text = "SAVE PATIENT";
                btnSil.Text = "DELETE PATIENT";
                label1.Text = "PATIENTS";
                button3.Text = "SEARCH PATIENT";
                button1.Text = " UPDATE PATIENT";
                label3.Text = "Address";
                label5.Text = "Weight";
                label6.Text = "Height";
                label4.Text = "Shoe Size";
                label7.Text = "Hip Knee Distance";
                label8.Text = "Knee Heel Distance";
                label2.Text = "Gender:";
                lblyakinad.Text = "Name";
                label13.Text = "Surname";
                label14.Text= "Degree of Kinship";
                label15.Text = "Phone Number:(+90)";
                label16.Text = "Disease Diagnosis";
                label11.Text = "Patient Close Information";
                label9.Text = "Phone Number:(+90)";
                label10.Text = "Date of Birth";
            }
            else
            {
                // Türkçe Metinler (Varsayılan)
                lblAdSoyad.Text = "Ad Soyad:";
                lblTc.Text = "TC Kimlik No:";
                //  btnKaydet.Text = "Kaydet";
                this.Text = "Hasta Kayıt"; // Form Başlığı
                this.labele1.Text
                 = "Dil Seçimi:";
                lblSehir.Text = "Şehir:";
                btnKaydet.Text = "HASTA KAYDET";
                btnSil.Text = "HASTA SİL";
                label1.Text = "HASTALAR";
                button3.Text = "HASTAYI ARA";
                button1.Text = " HASTAYI GÜNCELLE";
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


        private void PatientRegistration_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(" Plaka no:" + comboBox1.SelectedValue.ToString() + "Şehir:"+ comboBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            
            
            if(db.HastaSil(textBox2.Text)==false)
                MessageBox.Show("Silme işlemi başarısız");
            else
                MessageBox.Show("Silme işlemi başarılı");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
             
           
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string adSoyad = textBox1.Text.Trim(); 
            string tcNo = textBox2.Text.Trim();
            string kiloStr = textBox4.Text.Trim();
            string boyStr = textBox5.Text.Trim();
            string ayaknoStr = textBox7.Text.Trim();
            string kalcadizStr = textBox11.Text.Trim();
            string diztopukStr = textBox6.Text.Trim();
            string Ad = textBox9.Text.Trim();
            string Soyad = textBox10.Text.Trim();

            string email = txtmail.Text.Trim();



            // 2. HATA DENETİMLERİ (Validations)

            if (string.IsNullOrEmpty(adSoyad) || string.IsNullOrEmpty(tcNo))
            {
                
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Please fill all fields!" : "Lütfen tüm alanları doldurunuz!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tcNo.Length != 11)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "ID must be 11 digits!" : "TC Kimlik No 11 haneli olmalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           

            
            if (!string.IsNullOrEmpty(kiloStr) && !kiloStr.All(char.IsDigit))
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Weight must contain only digits!" : "Kilo bilgisi sadece rakamlardan oluşmalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return;
            }

           
            if (string.IsNullOrEmpty(kiloStr) || int.Parse(kiloStr) <= 0)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Weight information must not be left blank!" : "Kilo bilgisi Boş Kalmamalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return;
            }
            if (textBox8.Text.Length > 10 || textBox8.Text.Length<10)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Phone Number must be 10 digits!" : "Telefon No 10 haneli olmalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(boyStr) && !boyStr.All(char.IsDigit))
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Height must contain only digits!" : "Boy bilgisi sadece rakamlardan oluşmalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox5.Focus();
                return;
            }


            if (string.IsNullOrEmpty(boyStr) || int.Parse(boyStr) <= 0)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Height information must not be left blank!" : "Boy bilgisi Boş Kalmamalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox5.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(ayaknoStr) && !ayaknoStr.All(char.IsDigit))
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Shoe Size must contain only digits!" : "Ayak Numarası bilgisi sadece rakamlardan oluşmalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox7.Focus();
                return;
            }


            if (string.IsNullOrEmpty(ayaknoStr) || int.Parse(ayaknoStr) <= 0)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Shoe Size information must not be left blank!" : "Ayak Numarası bilgisi Boş Kalmamalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox7.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(kalcadizStr) && !kalcadizStr.All(char.IsDigit))
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Hip Knee Distance must contain only digits!" : "Kalça Diz Mesafesi bilgisi sadece rakamlardan oluşmalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox11.Focus();
                return;
            }


            if (string.IsNullOrEmpty(kalcadizStr) || int.Parse(kalcadizStr) <= 0)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Hip Knee Distance information must not be left blank!" : "Kalça Diz Mesafesi  bilgisi Boş Kalmamalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox11.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(diztopukStr) && !diztopukStr.All(char.IsDigit))
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Knee Heel Distance Distance must contain only digits!" : "Diz Topuk Mesafesi bilgisi sadece rakamlardan oluşmalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox6.Focus();
                return;
            }


            if (string.IsNullOrEmpty(diztopukStr) || int.Parse(diztopukStr) <= 0)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Knee Heel Distance information must not be left blank!" : "Diz Topuk Mesafesi  bilgisi Boş Kalmamalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox6.Focus();
                return;
            }
            
            if (string.IsNullOrEmpty(Ad) || string.IsNullOrEmpty(Soyad))
            {

                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Please make sure you enter the patient's close name and surname.!" : "Lütfen hasta yakın ad soyad girdiğinizden emin olunuz!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBox12.Text.Length > 10 || textBox12.Text.Length < 10)
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Phone Number must be 10 digits!" : "Telefon No 10 haneli olmalıdır!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox2.SelectedIndex == -1)
            {
                
                string uyari = (btnKaydet.Text == "SAVE PATIENT")
                               ? "Please select a gender!"
                               : "Lütfen bir cinsiyet seçiniz!";

                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox2.Focus(); 
                return; 

            }
            if (comboBox4.SelectedIndex == -1)
            {

                string uyari = (btnKaydet.Text == "SAVE PATIENT")
                               ? "Please select a kinship!"
                               : "Lütfen bir yakın seçiniz!";

                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox4.Focus(); 
                return; 
            }
            //---- E-Posta Kontrolleri ----//
            if (string.IsNullOrEmpty(email))
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT") ? "Please enter an email address!" : "Lütfen bir e-posta adresi giriniz!";
                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmail.Focus();
                return;
            }

            
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(email, emailPattern))
            {
                string uyari = (btnKaydet.Text == "SAVE PATIENT")
                               ? "Please enter a valid email address (e.g., example@domain.com)!"
                               : "Lütfen geçerli bir e-posta adresi giriniz (örn: ornek@alanadi.com)!";

                MessageBox.Show(uyari, "Hata/Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmail.Focus();
                return;
            }
            //---------------------

            // 3. VERİTABANI KAYDI (DatabaseManager kullanımı)
            // try
            {
           //     DatabaseManager db = new DatabaseManager();
               
             // Fonksiyonu DatabaseManager.cs içinde oluşturman gerekecek
              
                // bool sonuc = db.HastaEkle(adSoyad, tcNo, sehir);

               // if (sonuc)
                {
              //      string mesaj = (btnKaydet.Text == "SAVE PATIENT") ? "Patient Saved." : "Hasta başarıyla kaydedildi.";
                 //   MessageBox.Show(mesaj, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Kutuları temizle
                  // lblAdSoyad.Text = "";
                    //lblTc.Text = "";
                }
            }
           // catch (Exception ex)
            {
            //    MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
           
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void lblyakinad_Click(object sender, EventArgs e)
        {

        }
    }
}
