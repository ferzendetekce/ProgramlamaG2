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
    public partial class Login : Form
    {
        public string username;
        public int status;
        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox2.PasswordChar = '*';
            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            username = "Ferzende";
            status = 1; // 1-admin 2-user
            this.Close();   
        }

 

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Kullanıcı adı")  // Eğer kutuda varsayılan metin varsa, sil ve yazmaya hazırla
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "şifre") // Eğer kutuda varsayılan metin varsa, sil ve yazmaya hazırla
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }
    }
}
