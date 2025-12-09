using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevicesControllerApp.Terapi
{
    public partial class Therapy : UserControl
    {
        

        private void txthasta_Click(object sender, EventArgs e)
        {
               // txthasta.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnEN_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            this.Controls.Clear();
           
        }

        private void btnTR_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr");
            this.Controls.Clear();
            
        }

        private void lblAgirlik_Click(object sender, EventArgs e)
        {

        }

        private void lblTerapiHiz_Click(object sender, EventArgs e)
        {

        }

        private void Therapy_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Therapy
            // 
            this.Name = "Therapy";
            this.ResumeLayout(false);

        }
    }
}
