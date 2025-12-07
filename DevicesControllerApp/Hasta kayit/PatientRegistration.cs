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
            // 1. Verileri al (Textbox isimlerin resimdeki gibi varsayılmıştır)
            string adSoyad = lblAdSoyad.Text.Trim(); // Name: txtAdSoyad olmalı
            string tcNo = lblTc.Text.Trim();         // Name: txtTc olmalı
            //string sehir = cmbSehir.Text;            // Şehir combobox ise

            // 2. HATA DENETİMLERİ (Validations)
            if (string.IsNullOrEmpty(adSoyad) || string.IsNullOrEmpty(tcNo))
            {
                // Dil kontrolü yaparak uyarı veriyoruz (Basit yöntem)
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
    }
}
