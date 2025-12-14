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
        string placeholderUsername = "Kullancı Adı ";
        string placeholderTC = "TC kimlik no ";
        string placeholderPassword = " Yeni şifre ";

        public resetpassword()
        {
            InitializeComponent();
            txtUsername.Text = placeholderUsername;
            txtUsername.ForeColor = Color.Gray;

            txtTC.Text = placeholderTC;
            txtTC.ForeColor = Color.Gray;

            txtNewPassword.Text = placeholderPassword;
            txtNewPassword.ForeColor = Color.Gray;
            txtNewPassword.UseSystemPasswordChar = false;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // يغلق Form1
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked)
            {
                txtNewPassword.UseSystemPasswordChar = false; // إظهار كلمة السر
            }
            else
            {
                // نخفي فقط إذا المستخدم كتب كلمة فعلية وليس Placeholder
                if (txtNewPassword.ForeColor != Color.Gray)
                    txtNewPassword.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.ForeColor == Color.Gray)
            {
                txtUsername.Clear();
                txtUsername.ForeColor = Color.Black;
            }

        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.Text = placeholderUsername;
                txtUsername.ForeColor = Color.Gray;
            }
        }

        private void txtTC_Enter(object sender, EventArgs e)
        {
            if (txtTC.ForeColor == Color.Gray)
            {
                txtTC.Clear();
                txtTC.ForeColor = Color.Black;
            }
        }

        private void txtTC_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTC.Text))
            {
                txtTC.Text = placeholderTC;
                txtTC.ForeColor = Color.Gray;
            }
        }

        private void txtNewPassword_Enter(object sender, EventArgs e)
        {
            if (txtNewPassword.ForeColor == Color.Gray)
            {
                txtNewPassword.Clear();
                txtNewPassword.ForeColor = Color.Black;
                txtNewPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtNewPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                txtNewPassword.UseSystemPasswordChar = false;
                txtNewPassword.Text = placeholderPassword;
                txtNewPassword.ForeColor = Color.Gray;
            }
        }
        private void txtUsername_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.Text = "Kullancı Adı ";
                txtUsername.ForeColor = Color.Gray;
            }
        }

        private void resetpassword_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null; // يمنع التركيز على أي TextBox

            txtNewPassword.UseSystemPasswordChar = false;
            txtNewPassword.Text = placeholderPassword;
            txtNewPassword.ForeColor = Color.Gray;
        }
    }

}
    