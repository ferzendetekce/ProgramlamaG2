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

namespace DevicesControllerApp.Ana_ekran_Login
{
    public partial class resetpassword : Form
    {
        public resetpassword()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string tc = txtTC.Text.Trim();
            string newPass = txtNewPassword.Text.Trim();

            if (username == "" || tc == "" || newPass == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

            if (!DatabaseManager.Instance.OpenConnection())
            {
                MessageBox.Show("Veritabanı bağlantı hatası!");
                return;
            }

            bool ok = DatabaseManager.Instance.ResetPassword(username, tc, newPass);

            DatabaseManager.Instance.CloseConnection();

            if (ok)
            {
                MessageBox.Show("Şifre başarıyla değiştirildi!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı bulunamadı! Bilgileri kontrol edin.");
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void resetpassword_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
