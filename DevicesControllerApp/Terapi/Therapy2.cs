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

namespace DevicesControllerApp.Terapi
{
    public partial class Therapy : UserControl
    {
        string connectionString = "Server=localhost;Port=5432;Database=database_terapi ;User Id=postgres;Password=1234;";
        public Therapy()
        {
            InitializeComponent();
        }

        void SearchHasta(string searchText)
        {
            using (var con = new NpgsqlConnection(connectionString))
            {
                string query = @"
            SELECT hasta_id, ad, soyad
            FROM hasta_bilgileri
            WHERE true AND (ad ILIKE @search OR soyad ILIKE @search) OR (ad || ' ' || soyad ILIKE @search)
               
        ";
                using (var cmd = new NpgsqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchText + "%");

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    try
                    {
                        da.Fill(dt);
                    } catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    dgvPatients.DataSource = dt;
                }
            }
        }

        private void Therapy_Load(object sender, EventArgs e)
        {
            dgvPatients.SelectionMode= DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.MultiSelect= false;
            dgvPatients.ReadOnly= true; 
            dgvPatients.AllowUserToAddRows= false;

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
            lblAnlikCihazDurumu.Text = "⬤ Aktif";
            lblAnlikCihazDurumu.ForeColor = Color.FromArgb(29,129,123);
        }

        private void btnTerapiDurdur_Click(object sender, EventArgs e)
        {
            if (lblAnlikCihazDurumu.Text == "⬤ Aktif")
            {
                timer.Stop();
                lblAnlikCihazDurumu.Text = "⬤ Ara";
                lblAnlikCihazDurumu.ForeColor = Color.Black;
            }
            else if (lblAnlikCihazDurumu.Text == "⬤ Ara")
            {
                timer.Start();
                lblAnlikCihazDurumu.Text = "⬤ Aktif";
                lblAnlikCihazDurumu.ForeColor = Color.FromArgb(29, 129, 123);
            }
        }

        private void btnBitir_Click(object sender, EventArgs e)
        {
            timer.Stop();
            lblTerapiSuresi.Text ="00:00:00";
            lblAnlikCihazDurumu.Text = "⬤ Pasif";
            lblAnlikCihazDurumu.ForeColor = Color.Red;
        }

        private void btnAcilStop_Click(object sender, EventArgs e)
        {
            timer.Stop();
            lblAnlikCihazDurumu.Text = "⬤ Pasif";
            lblAnlikCihazDurumu.ForeColor = Color.Red;
        }

        private void checkedListBoxKarakter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < checkedListBoxKarakter.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        checkedListBoxKarakter.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void checkedListBoxOrtam_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < checkedListBoxOrtam.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        checkedListBoxOrtam.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void txbArama_TextChanged(object sender, EventArgs e)
        {
            if (txbArama.Text.Trim().Length < 3)
            return;
            SearchHasta(txbArama.Text.Trim());
            
        }

      

       

        private void dgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvPatients.Rows[e.RowIndex];
            int hastaId = Convert.ToInt32(row.Cells["hasta_id"].Value);
            string ad = row.Cells["ad"].Value.ToString();
            string soyad = row.Cells["soyad"].Value.ToString();
            dgvPatients.ClearSelection();
            dgvPatients.Visible = false;
            labelAd.Text = ad;
            labelSoyad.Text = soyad;
        }

        
    }


}
