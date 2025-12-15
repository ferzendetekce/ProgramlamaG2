using DevicesControllerApp.Ana_ekran_Login;
using DevicesControllerApp.Ayarlar;
using DevicesControllerApp.Hasta_kayit;
using DevicesControllerApp.Kullanici;
using DevicesControllerApp.Raporlama;
using DevicesControllerApp.Servis;
using DevicesControllerApp.Terapi;
using DevicesControllerApp.Veri_izleme;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevicesControllerApp
{
    public partial class MainForm : Form
    {
        private Size originalFormSize;
        private int sideMenuOriginalWidth = 270;
        private bool sideMenuOpened = true;

        public MainForm()
        {
            InitializeComponent();
            //  
            originalFormSize = this.Size;

            // 
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            // 
            ScaleControls(this,
                          Screen.PrimaryScreen.Bounds.Width / (float)originalFormSize.Width,
                          Screen.PrimaryScreen.Bounds.Height / (float)originalFormSize.Height);

            Timer timer = new Timer();
            timer.Interval = 1000; // 1 saniye (1000 milisaniye)
            timer.Tick += Timer_Tick; // Her saniyede bir olay tetiklenecek
            timer.Start(); // Zamanlayıcıyı başlat

        }
        private void ScaleControls(Control parent, float scaleX, float scaleY)
        {
            foreach (Control ctrl in parent.Controls)
            {
                // 
                ctrl.Width = (int)(ctrl.Width * scaleX);
                ctrl.Height = (int)(ctrl.Height * scaleY);

                // 
                ctrl.Left = (int)(ctrl.Left * scaleX);
                ctrl.Top = (int)(ctrl.Top * scaleY);

                // A
                if (ctrl.HasChildren)
                    ScaleControls(ctrl, scaleX, scaleY);
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            splitContainer2.Panel2.Controls.Clear();
            splitContainer2.Panel2.Controls.Add(s);
        }

        private void btnPatient_Click(object sender, EventArgs e)
        {
            PatientRegistration s = new PatientRegistration();
            splitContainer2.Panel2.Controls.Clear();
            splitContainer2.Panel2.Controls.Add(s);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            Reports s = new Reports();
            splitContainer2.Panel2.Controls.Clear();
            splitContainer2.Panel2.Controls.Add(s);
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            Service s = new Service();
            splitContainer2.Panel2.Controls.Clear();
            splitContainer2.Panel2.Controls.Add(s);
        }



        private void btnMonitoring_Click(object sender, EventArgs e)
        {
            DataMonitoring s = new DataMonitoring();
            splitContainer2.Panel2.Controls.Clear();
            splitContainer2.Panel2.Controls.Add(s);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            UserRegistration s = new UserRegistration();
            splitContainer2.Panel2.Controls.Clear();
            splitContainer2.Panel2.Controls.Add(s);
        }

        private void btnTherapy_Click(object sender, EventArgs e)
        {
            Therapy s = new Therapy();
            splitContainer2.Panel2.Controls.Clear();
            splitContainer2.Panel2.Controls.Add(s);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (sideMenuOpened)
            {

                splitContainer1.SplitterDistance = 40;
            }
            else
            {

                splitContainer1.SplitterDistance = sideMenuOriginalWidth;
            }

            sideMenuOpened = !sideMenuOpened;

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Login login = new Login();
            login.StartPosition = FormStartPosition.CenterScreen;


            login.Show();

            //  (MainForm)
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Uyarı mesajı göster
            DialogResult result = MessageBox.Show(
                "Programı kapatmak istiyor musunuz?", // mesaj metni
                "Uyarı",                              // başlık
                MessageBoxButtons.YesNo,              // iki buton (Evet / Hayır)
                MessageBoxIcon.Warning                // uyarı simgesi
            );
            Application.Exit();
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }



        private void labelTime_Click(object sender, EventArgs e)
        {

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Güncel tarih ve saati
            DateTime simdikiZaman = DateTime.Now;

            // Tarih ve saat bilgisini istediğin formatta göster
            labelTime.Text = simdikiZaman.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}