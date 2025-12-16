using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevicesControllerApp.Terapi
{
    public partial class Therapy : UserControl
    {
        public Therapy()
        {
            InitializeComponent();
        }

        private void Therapy_Load(object sender, EventArgs e)
        {
            

        }

        private void txbArama_Enter(object sender, EventArgs e)
        {
            if (txbArama.Text == "🔎︎ Hasta Ara")
            {
                txbArama.Text = "";
                txbArama.ForeColor = Color.Black;
            }
        }

        private void txbArama_Leave(object sender, EventArgs e)
        {
            if (txbArama.Text == "")
            {
                txbArama.Text = "🔎︎ Hasta Ara";
                txbArama.ForeColor = Color.Gray;
            }
        }

        private void trackBarAgirlik_Scroll(object sender, EventArgs e)
        {
            //lblAgirlik.Text = trackBarAgirlik.Value.ToString() + " kg";
            lblAnlikAgirlik.Text = trackBarAgirlik.Value.ToString() + " kg";
        }

        private void trackBarDestekBar_Scroll(object sender, EventArgs e)
        {
           // lblBarYukseklik.Text = trackBarDestekBar.Value.ToString() + " cm";
            lblAnlikDestekBar.Text = trackBarDestekBar.Value.ToString() + " cm";
        }

        private void trackBarAyakNumarasi_Scroll(object sender, EventArgs e)
        {
           // lblAyakNumarasi.Text = trackBarAyakNumarasi.Value.ToString();
            lblAnlikAyak.Text = trackBarAyakNumarasi.Value.ToString();
        }

        private void trackBarHiz_Scroll(object sender, EventArgs e)
        {
            //lblHiz.Text = trackBarHiz.Value.ToString() + " km/h";
            lblAnlikBilgiHiz.Text = trackBarHiz.Value.ToString() + " km/h";
        }
        int seconds = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            seconds++;
            int mins = seconds / 60;
            int hrs = mins / 60;
            lblTerapiSuresi.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", hrs, mins % 60, seconds % 60);

        }

        private void btnTerapiBasla_Click(object sender, EventArgs e)
        {
            timer.Start();
        }
    }


}
