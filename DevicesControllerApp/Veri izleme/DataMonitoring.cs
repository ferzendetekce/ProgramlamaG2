using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using RehabilitationSystem.Communication;

namespace DevicesControllerApp.Veri_izleme
{
    public partial class DataMonitoring : UserControl
    {
        // --- DEĞİŞKENLER ---
        private DeviceCommunication device;
        private bool freezeMode = false;
        private int pointCount = 100;
        private int stepCount = 0;

        // Dil Seçimi Değişkenleri
        private ComboBox cmbLanguage;
        private string currentLanguage = "TR"; // Varsayılan

        public DataMonitoring()
        {
            InitializeComponent();
            InitializeLanguageComboBox();
        }

        // Dil Kutusunu Oluştur
        private void InitializeLanguageComboBox()
        {
            cmbLanguage = new ComboBox();
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.Items.Add("Türkçe");
            cmbLanguage.Items.Add("English");
            cmbLanguage.SelectedIndex = 0; // Türkçe Başla

            // Konumunu ayarla
            cmbLanguage.Location = new Point(1120, 15);
            cmbLanguage.Size = new Size(100, 25);

            cmbLanguage.SelectedIndexChanged += CmbLanguage_SelectedIndexChanged;

            this.Controls.Add(cmbLanguage);
            cmbLanguage.BringToFront();
        }

        private void DataMonitoring_Load_1(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                SetupCharts();
                ApplyLanguage(); // Dili uygula

                device = new DeviceCommunication();
                device.OnNewData += Device_OnNewData;
                device.Start();
            }
        }

        // Dil Değişimi
        private void CmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentLanguage = (cmbLanguage.SelectedIndex == 0) ? "TR" : "EN";
            ApplyLanguage();
        }

        // --- DİL AYARLARI ---
        private void ApplyLanguage()
        {
            bool isTR = currentLanguage == "TR";

            // 1. Etiket ve Buton Metinleri
            lblVeriBaslik.Text = isTR ? "Veri İzleme" : "Data Monitoring";
            btnExportData.Text = isTR ? "Dışarı Aktar" : "Export Data";
            lblAdimSayisiBaslik.Text = isTR ? "Adım Sayısı" : "Step Count";

            // 2. Grafik Başlıkları
            UpdateChartTitle(chartSagTaban, isTR ? "Sağ Ayak Tabanı" : "Right Foot Sole");
            UpdateChartTitle(chartSolTaban, isTR ? "Sol Ayak Tabanı" : "Left Foot Sole");
            UpdateChartTitle(chartSagTopuk, isTR ? "Sağ Ayak Topuğu" : "Right Foot Heel");
            UpdateChartTitle(chartSolTopuk, isTR ? "Sol Ayak Topuğu" : "Left Foot Heel");
            UpdateChartTitle(chartAgirlik, isTR ? "Ağırlık Dengesi" : "Weight Balance");

            // 3. Eksen Başlıkları ve Tooltip (İpucu) Yazıları
            UpdateAxisAndTooltips(chartSagTaban, isTR);
            UpdateAxisAndTooltips(chartSolTaban, isTR);
            UpdateAxisAndTooltips(chartSagTopuk, isTR);
            UpdateAxisAndTooltips(chartSolTopuk, isTR);

            // 4. Grafik Seri İsimleri (Canlı / Örnek -> Live / Sample)
            string liveText = isTR ? "Canlı" : "Live";
            string sampleText = isTR ? "Örnek" : "Sample";

            UpdateSeriesLegend(chartSagTaban, liveText, sampleText);
            UpdateSeriesLegend(chartSolTaban, liveText, sampleText);
            UpdateSeriesLegend(chartSagTopuk, liveText, sampleText);
            UpdateSeriesLegend(chartSolTopuk, liveText, sampleText);

            // Ağırlık Grafiği Seri Adı
            if (chartAgirlik.Series.Count > 0)
            {
                // Bar grafiğinde LegendText kullanıyoruz
                chartAgirlik.Series[0].LegendText = isTR ? "Ağırlık (kg)" : "Weight (kg)";
                chartAgirlik.Series[0].ToolTip = isTR ? "Ağırlık: #VALY kg" : "Weight: #VALY kg";
            }

            // 5. Freeze Butonu
            UpdateFreezeButtonText();
        }

        // Yardımcı Metot: Seri İsimlerini (Legend) Güncelle
        private void UpdateSeriesLegend(Chart chart, string liveText, string sampleText)
        {
            // Kod içinde serileri "Canlı" ve "Örnek" adıyla çağırmaya devam ediyoruz,
            // ama kullanıcıya görünen metni (LegendText) değiştiriyoruz.
            if (chart.Series["Canlı"] != null) chart.Series["Canlı"].LegendText = liveText;
            if (chart.Series["Örnek"] != null) chart.Series["Örnek"].LegendText = sampleText;
        }

        // Yardımcı Metot: Eksen ve Tooltip Güncelle
        private void UpdateAxisAndTooltips(Chart chart, bool isTR)
        {
            if (chart.ChartAreas.Count > 0)
            {
                chart.ChartAreas[0].AxisX.Title = "Index";
                chart.ChartAreas[0].AxisY.Title = isTR ? "Yük (kg)" : "Load (kg)";

                // Fare ile üzerine gelince çıkan yazı
                string tooltipFormat = isTR ? "Index: #VALX, Yük: #VALY kg" : "Index: #VALX, Load: #VALY kg";

                if (chart.Series["Canlı"] != null) chart.Series["Canlı"].ToolTip = tooltipFormat;
                if (chart.Series["Örnek"] != null) chart.Series["Örnek"].ToolTip = tooltipFormat;
            }
        }

        // Yardımcı Metot: Buton Metni
        private void UpdateFreezeButtonText()
        {
            bool isTR = currentLanguage == "TR";

            if (freezeMode)
                btnFreeze.Text = isTR ? "Örneği Temizle" : "Clear Sample";
            else
                btnFreeze.Text = isTR ? "Örnek Al" : "Take Sample";
        }

        private void UpdateChartTitle(Chart chart, string newTitle)
        {
            if (chart.Titles.Count > 0)
                chart.Titles[0].Text = newTitle;
            else
                chart.Titles.Add(newTitle);
        }

        public void StopDeviceCommunication()
        {
            device?.Stop();
        }

        // --- VERİ İŞLEME ---
        private void Device_OnNewData(object sender, NewDataEventArgs e)
        {
            if (this.InvokeRequired)
            {
                try { this.Invoke(new Action(() => ProcessData(e))); }
                catch (ObjectDisposedException) { }
            }
            else
            {
                ProcessData(e);
            }
        }

        private void ProcessData(NewDataEventArgs e)
        {
            if (e.IsStepDetected)
            {
                UpdateScatter(chartSagTaban, e.SagTabanData);
                UpdateScatter(chartSolTaban, e.SolTabanData);
                UpdateScatter(chartSagTopuk, e.SagTopukData);
                UpdateScatter(chartSolTopuk, e.SolTopukData);

                // Ağırlık grafiğini güncelle (index 0 kullanıyoruz, isim değişse de index sabittir)
                if (chartAgirlik.Series.Count > 0)
                {
                    chartAgirlik.Series[0].Points.Clear();
                    chartAgirlik.Series[0].Points.AddY(e.AgirlikDengesi);
                }

                stepCount++;
                lblStepCounter.Text = stepCount.ToString();
            }
        }

        // --- GRAFİK KURULUM ---
        private void SetupCharts()
        {
            SetupScatterChart(chartSagTaban, "Sağ Ayak Tabanı", Color.CornflowerBlue);
            SetupScatterChart(chartSolTaban, "Sol Ayak Tabanı", Color.MediumSeaGreen);
            SetupScatterChart(chartSagTopuk, "Sağ Ayak Topuğu", Color.Yellow);
            SetupScatterChart(chartSolTopuk, "Sol Ayak Topuğu", Color.MediumPurple);
            SetupBarChart(chartAgirlik, "Ağırlık Dengesi", 0, 100);
        }

        private void SetupScatterChart(Chart chart, string title, Color color)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(title);
            var area = chart.ChartAreas[0];

            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = pointCount;
            area.AxisX.Title = "Index";
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.Interval = 10;

            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = 100;
            area.AxisY.Title = "Yük (kg)";
            area.AxisY.MajorGrid.LineColor = Color.LightGray;

            area.CursorX.IsUserEnabled = true;
            area.CursorX.IsUserSelectionEnabled = true;
            area.AxisX.ScaleView.Zoomable = true;
            area.CursorY.IsUserEnabled = true;
            area.CursorY.IsUserSelectionEnabled = true;
            area.AxisY.ScaleView.Zoomable = true;

            // İÇ İSİMLER "Canlı" ve "Örnek" OLARAK KALIYOR (Kod hatası olmaması için)
            // Görünen isimleri ApplyLanguage metodu düzeltecek.
            var live = chart.Series.Add("Canlı");
            live.ChartType = SeriesChartType.Line;
            live.Color = color;
            live.BorderWidth = 3;
            live.MarkerStyle = MarkerStyle.Circle;
            live.MarkerSize = 8;

            var red = chart.Series.Add("Örnek");
            red.ChartType = SeriesChartType.Line;
            red.Color = Color.Red;
            red.BorderWidth = 3;
            red.MarkerStyle = MarkerStyle.Circle;
            red.MarkerSize = 8;
        }

        private void SetupBarChart(Chart chart, string title, double min, double max)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(title);
            var area = chart.ChartAreas[0];

            area.AxisY.Minimum = min;
            area.AxisY.Maximum = max;
            area.AxisY.Interval = 20;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.LabelStyle.Enabled = false;
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisX.LineWidth = 0;

            var s = chart.Series.Add("Ağırlık (kg)");
            s.ChartType = SeriesChartType.Column;
            s.Color = Color.Gold;
        }

        private void UpdateScatter(Chart chart, List<double> data)
        {
            // Veri eklerken iç ismi kullanıyoruz
            var liveSeries = chart.Series["Canlı"];
            liveSeries.Points.Clear();
            for (int i = 0; i < data.Count; i++)
            {
                liveSeries.Points.AddXY(i, data[i]);
            }
        }

        // --- BUTON VE ETKİLEŞİM ---
        private void btnFreeze_Click(object sender, EventArgs e)
        {
            freezeMode = !freezeMode;
            if (freezeMode)
            {
                CopyBlueToRed(chartSagTaban);
                CopyBlueToRed(chartSolTaban);
                CopyBlueToRed(chartSagTopuk);
                CopyBlueToRed(chartSolTopuk);
            }
            else
            {
                ClearRed(chartSagTaban);
                ClearRed(chartSolTaban);
                ClearRed(chartSagTopuk);
                ClearRed(chartSolTopuk);
            }
            UpdateFreezeButtonText();
        }

        private void btnExportData_Click(object sender, EventArgs e)
        {
            bool isTR = currentLanguage == "TR";

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = isTR ? "CSV Dosyası (*.csv)|*.csv" : "CSV File (*.csv)|*.csv";
                sfd.Title = isTR ? "Grafik Verilerini Dışa Aktar" : "Export Chart Data";
                sfd.FileName = $"rehab_data_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var sb = new StringBuilder();
                        sb.AppendLine("Chart;Index;Value_kg");

                        ExportChartDataToCsv(sb, chartSagTaban, "Right_Sole");
                        ExportChartDataToCsv(sb, chartSolTaban, "Left_Sole");
                        ExportChartDataToCsv(sb, chartSagTopuk, "Right_Heel");
                        ExportChartDataToCsv(sb, chartSolTopuk, "Left_Heel");

                        File.WriteAllText(sfd.FileName, sb.ToString());

                        string msg = isTR ? "Veriler başarıyla dışa aktarıldı!" : "Data exported successfully!";
                        string title = isTR ? "Başarılı" : "Success";
                        MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        string msg = isTR ? "Hata: " : "Error: ";
                        string title = isTR ? "Dışa Aktarma Hatası" : "Export Error";
                        MessageBox.Show(msg + ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- YARDIMCI METOTLAR ---
        private void ExportChartDataToCsv(StringBuilder sb, Chart chart, string chartName)
        {
            var seriesToExport = freezeMode ? chart.Series["Örnek"] : chart.Series["Canlı"];
            foreach (var point in seriesToExport.Points)
            {
                sb.AppendLine($"{chartName};{point.XValue};{point.YValues[0]}");
            }
        }

        private void CopyBlueToRed(Chart chart)
        {
            var blue = chart.Series["Canlı"];
            var red = chart.Series["Örnek"];
            red.Points.Clear();
            foreach (var p in blue.Points)
                red.Points.AddXY(p.XValue, p.YValues[0]);
        }

        private void ClearRed(Chart chart)
        {
            chart.Series["Örnek"].Points.Clear();
        }

        private void DataMonitoring_Click(object sender, EventArgs e) { }
        private void chartSagTopuk_Click(object sender, EventArgs e) { }
        private void lblAdimSayisiBaslik_Click(object sender, EventArgs e) { }
        private void lblStepCounter_Click(object sender, EventArgs e) { }
    }
}