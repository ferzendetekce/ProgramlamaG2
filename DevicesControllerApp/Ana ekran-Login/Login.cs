using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace DevicesControllerApp.Ana_ekran_Login
{
    public partial class Login : Form

    {
        public static string LoggedUser = "";
        public static string LoggedRole = "";

        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox2.PasswordChar = '*';
            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            this.Load += Login_Load;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RememberMe)
            {
                textBox1.Text = Properties.Settings.Default.SavedUsername;
                checkBox2.Checked = true;
                textBox1.ForeColor = Color.Black;
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var db = DevicesControllerApp.Database.DatabaseManager.Instance;

            if (!db.OpenConnection())
            {
                MessageBox.Show("Veritabanına bağlanılamadı",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {

                bool isValid = db.ValidateUserLogin(username, password);

                if (!isValid)
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                DataRow userRow = db.GetUserByUsername(username);

                if (userRow == null)
                {
                    MessageBox.Show("Kullanıcı bilgileri alınamadı",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LoggedUser = userRow["kullanici_adi_soyadi"].ToString();
                LoggedRole = userRow["rol"].ToString();

                MessageBox.Show("Giriş başarılı",
                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);




                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            finally
            {
                db.CloseConnection();
            }

        }





        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }



        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                textBox2.PasswordChar = '\0';
            }
            else
            {

                textBox2.PasswordChar = '*';
            }
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {

                Properties.Settings.Default.SavedUsername = textBox1.Text.Trim();
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.Save();
            }
            else
            {

                Properties.Settings.Default.SavedUsername = "";
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.Save();
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {
            resetpassword rp = new resetpassword();
            rp.StartPosition = FormStartPosition.CenterScreen;
            rp.ShowDialog();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Kullanıcı adı")  // 
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "şifre") // 
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}