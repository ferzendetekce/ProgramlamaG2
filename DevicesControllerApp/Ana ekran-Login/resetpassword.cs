using DevicesControllerApp.Database;
using System;
using System.Drawing;
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

        //RESET
        private void btnReset_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string tc = txtTC.Text.Trim();
            string newPass = txtNewPassword.Text.Trim();

            //
            if (
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(tc) ||
                string.IsNullOrWhiteSpace(newPass) ||
                username == placeholderUsername ||
                tc == placeholderTC ||
                newPass == placeholderPassword
            )
            {
                MessageBox.Show("Lütfen tüm alanları doğru şekilde doldurun!");
                return;
            }

            var db = DatabaseManager.Instance;

            if (!db.OpenConnection())
            {
                MessageBox.Show("Veritabanı bağlantı hatası!");
                return;
            }

            bool ok = false;

            try
            {
                ok = db.ResetPassword(username, tc, newPass);
            }
            finally
            {
                db.CloseConnection();
            }

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

        //EVENTS REQUIRED BY DESIGNER

        private void txtUsername_TextChanged(object sender, EventArgs e) { }

        private void txtTC_TextChanged(object sender, EventArgs e) { }

        private void txtNewPassword_TextChanged(object sender, EventArgs e) { }

        private void resetpassword_Load(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        //SHOW / HIDE PASSWORD
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked)
            {
                txtNewPassword.UseSystemPasswordChar = false;
            }
            else
            {
                if (txtNewPassword.ForeColor != Color.Gray)
                    txtNewPassword.UseSystemPasswordChar = true;
            }
        }

        //CLOSE
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //PLACEHOLDERS
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

        private void txtUsername_Leave_1(object sender, EventArgs e)
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

        private void resetpassword_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;

            txtNewPassword.UseSystemPasswordChar = false;
            txtNewPassword.Text = placeholderPassword;
            txtNewPassword.ForeColor = Color.Gray;
        }
    }
}
