using RehabilitationSystem.Communication;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DevicesControllerApp.Veri_izleme
{
    public partial class DataMonitoring : UserControl
    {
        // --- DEĞİŞKENLER ---

        // Arka planda veri üreten/alan sınıfın bir örneği
        private DeviceCommunication device;

        // "Örnek Al" butonunun aktif olup olmadığını tutan bayrak
        private bool freezeMode = false;
        // Ayak grafiklerindeki X ekseninde kaç nokta olacağını belirler
        private int pointCount = 100;
        // Algılanan adım sayısını tutar
        private int stepCount = 0;

        public DataMonitoring()
        {
            InitializeComponent();
        }

        private void DataMonitoring_Load(object sender, EventArgs e)
        {
            // Kodun Visual Studio tasarım ekranında çalışmasını engeller.
       /*     if (!this.DesignMode)
            {
                // Grafiklerin ilk ayarlarını yapar
                SetupCharts();
                // Butonun başlangıç metnini ayarlar
                btnFreeze.Text = "Örnek Al";

                // Cihaz iletişimini başlatır ve veri gelmeye başladığında ne yapılacağını söyler
                device = new DeviceCommunication();
                device.OnNewData += Device_OnNewData; // Veri geldiğinde Device_OnNewData metodunu tetikle
                device.Start(); // Veri akışını başlat
            }
        }*/

        // Ana form kapatılırken çağrılacak olan metot. Arka plan işlemini durdurur
       /* public void StopDeviceCommunication()
        {
            device?.Stop(); // device null değilse Stop() metodunu çağır
        }*/

        // --- VERİ İŞLEME ---

        // Cihazdan yeni bir veri paketi geldiğinde tetikler
      /*  private void Device_OnNewData(object sender, NewDataEventArgs e)
        {
            // Veri arka plandan geldiği için UI'ı güvenli bir şekilde güncellemek gerekir
            if (this.InvokeRequired)
            {
                try
                {
                    // Ana UI thread'ine ProcessData metodunu çalıştırması için bir istek gönderir
                    this.Invoke(new Action(() => ProcessData(e)));
                }
                catch (ObjectDisposedException) {  }
            }
            else
            {
                // Zaten ana thread'de isek, metodu doğrudan çalıştır
                ProcessData(e);
            }
        }*/

        // Gelen veriyi alıp grafiklere işleyen ana metot
      /*  private void ProcessData(NewDataEventArgs e)
        {
            // Sadece bir "adım" algılandığında grafikleri güncelle
            if (e.IsStepDetected)
            {
                // Ayak grafiklerini gelen yeni verilerle güncelle
                UpdateScatter(chartSagTaban, e.SagTabanData);
                UpdateScatter(chartSolTaban, e.SolTabanData);
                UpdateScatter(chartSagTopuk, e.SagTopukData);
                UpdateScatter(chartSolTopuk, e.SolTopukData);

                // Ağırlık grafiğini güncelle.
                chartAgirlik.Series["Ağırlık (kg)"].Points.Clear();
                chartAgirlik.Series["Ağırlık (kg)"].Points.AddY(e.AgirlikDengesi);

                // Adım sayacını bir artır ve ekrandaki etiketi güncelle
                stepCount++;
                lblStepCounter.Text = stepCount.ToString();
            }*/
        }

        // --- GRAFİK AYARLARI ---

        // Tüm grafiklerin ilk kurulumunu başlatan metot
        private void SetupCharts()
        {
            SetupScatterChart(chartSagTaban, "Sağ Ayak Tabanı", Color.CornflowerBlue);
            SetupScatterChart(chartSolTaban, "Sol Ayak Tabanı", Color.MediumSeaGreen);
            SetupScatterChart(chartSagTopuk, "Sağ Ayak Topuğu", Color.Yellow);
            SetupScatterChart(chartSolTopuk, "Sol Ayak Topuğu", Color.MediumPurple);
            SetupBarChart(chartAgirlik, "Ağırlık Dengesi", 0, 100);
        }

        // Ayak grafiklerinin (çizgi grafik) genel ayarlarını yapan metot
        private void SetupScatterChart(Chart chart, string title, Color color)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(title);
            var area = chart.ChartAreas[0];

            // X ekseni ayarları (0'dan 100'e, etiketler 5'er aralıkla)
            area.AxisX.Minimum = 0;
            area.AxisX.Maximum = pointCount;
            area.AxisX.Title = "Index";
            area.AxisX.MajorGrid.LineColor = Color.LightGray;
            area.AxisX.Interval = 10;

            // Y ekseni ayarları (0'dan 100'e, başlık kg).
            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = 100;
            area.AxisY.Title = "LoadCell (kg)";
            area.AxisY.MajorGrid.LineColor = Color.LightGray;

            // Grafikte fare ile zoom yapma özelliğini aktif eder.
            area.CursorX.IsUserEnabled = true;
            area.CursorX.IsUserSelectionEnabled = true;
            area.AxisX.ScaleView.Zoomable = true;
            area.CursorY.IsUserEnabled = true;
            area.CursorY.IsUserSelectionEnabled = true;
            area.AxisY.ScaleView.Zoomable = true;

            // "Canlı" veri serisini oluşturur (renkli çizgiler)
            var live = chart.Series.Add("Canlı");
            live.ChartType = SeriesChartType.Line;
            live.Color = color;
            live.BorderWidth = 3;
            live.MarkerStyle = MarkerStyle.Circle;
            live.MarkerSize = 8;
            live.ToolTip = "Index: #VALX, LoadCell: #VALY kg"; // Fare ile üzerine gelince çıkan bilgi

            // "Örnek" veri serisini oluşturur (kırmızı çizgiler)
            var red = chart.Series.Add("Örnek");
            red.ChartType = SeriesChartType.Line;
            red.Color = Color.Red;
            red.BorderWidth = 3;
            red.MarkerStyle = MarkerStyle.Circle;
            red.MarkerSize = 8;
            red.ToolTip = "Index: #VALX, LoadCell: #VALY kg";
        }

        // Ağırlık Dengesi grafiğinin (sütun grafik) ayarlarını yapan metot
        private void SetupBarChart(Chart chart, string title, double min, double max)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(title);
            var area = chart.ChartAreas[0];

            // Y ekseni ayarları (min/max değerleri dışarıdan alır)
            area.AxisY.Minimum = min;
            area.AxisY.Maximum = max;
            area.AxisY.Interval = 20;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;

            // X eksenini gizler, çünkü tek bir sütun var
            area.AxisX.LabelStyle.Enabled = false;
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisX.LineWidth = 0;

            // "Ağırlık (kg)" serisini oluşturur
            var s = chart.Series.Add("Ağırlık (kg)");
            s.ChartType = SeriesChartType.Column;
            s.Color = Color.Gold;
            s.ToolTip = "Ağırlık: #VALY kg";
        }

        // Ayak grafiklerini yeni veri listesiyle güncelleyen yardımcı metot
        private void UpdateScatter(Chart chart, List<double> data)
        {
            var liveSeries = chart.Series["Canlı"];
            liveSeries.Points.Clear(); // Eski noktaları temizle
            for (int i = 0; i < data.Count; i++)
            {
                liveSeries.Points.AddXY(i, data[i]); // Yeni noktaları ekle
            }
        }

        // --- BUTON OLAYLARI ---

        // "Örnek Al" / "Örneği Temizle" butonuna tıklandığında çalışır
        private void btnFreeze_Click(object sender, EventArgs e)
        {
            freezeMode = !freezeMode; // Durumu tersine çevir (true ise false, false ise true yap).

            if (freezeMode) // Eğer "Örnek Al" moduna geçildiyse
            {
                // O anki canlı (renkli) verileri örnek (kırmızı) serisine kopyala
                CopyBlueToRed(chartSagTaban);
                CopyBlueToRed(chartSolTaban);
                CopyBlueToRed(chartSagTopuk);
                CopyBlueToRed(chartSolTopuk);
                btnFreeze.Text = "Örneği Temizle"; // Butonun metnini değiştir
            }
            else // Eğer "Örneği Temizle" moduna geçildiyse
            {
                // Kırmızı serideki tüm verileri temizle
                ClearRed(chartSagTaban);
                ClearRed(chartSolTaban);
                ClearRed(chartSagTopuk);
                ClearRed(chartSolTopuk);
                btnFreeze.Text = "Örnek Al"; // Butonun metnini eski haline getir
            }
        }

        // "Verileri Dışa Aktar" butonuna tıklandığında çalışır
        private void btnExportData_Click(object sender, EventArgs e)
        {
            // "Farklı Kaydet" penceresini oluşturur ve işi bitince temizler
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                // Pencere ayarları
                sfd.Filter = "CSV Dosyası (*.csv)|*.csv";      // Sadece .csv dosyalarını gösterir
                sfd.Title = "Grafik Verilerini Dışa Aktar";   // Pencere başlığını ayarlar
                sfd.FileName = $"rehabilitasyon_veri_{DateTime.Now:yyyyMMdd_HHmmss}.csv"; // Otomatik dosya adı oluşturur

                // Kullanıcı "Kaydet" butonuna basarsa devam et
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Hata oluşma ihtimaline karşı kodları bu bloğa al
                    try
                    {
                        // CSV içeriğini oluştur
                        var sb = new StringBuilder();
                        sb.AppendLine("Grafik;Index;LoadCell_kg"); // CSV başlık satırını ekle

                        // Her grafiğin verisini CSV metnine ekle.
                        ExportChartDataToCsv(sb, chartSagTaban, "Sag_Taban");
                        ExportChartDataToCsv(sb, chartSolTaban, "Sol_Taban");
                        ExportChartDataToCsv(sb, chartSagTopuk, "Sag_Topuk");
                        ExportChartDataToCsv(sb, chartSolTopuk, "Sol_Topuk");

                        // Dosyaya yaz ve kullanıcıyı bilgilendir
                        File.WriteAllText(sfd.FileName, sb.ToString()); // Oluşturulan metni dosyaya yaz
                        MessageBox.Show("Veriler başarıyla dışa aktarıldı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information); // Başarı mesajı göster.
                    }
                    catch (Exception ex) // Yazma sırasında bir hata olursa...
                    {
                        // Hata mesajını kullanıcıya göster
                        MessageBox.Show("Hata: " + ex.Message, "Dışa Aktarma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- YARDIMCI METOTLAR ---

        // Verilen bir grafiğin verisini CSV metnine ekleyen metot
        private void ExportChartDataToCsv(StringBuilder sb, Chart chart, string chartName)
        {
            // Eğer ekranda bir örnek (kırmızı) gösteriliyorsa onu, yoksa canlı veriyi al
            var seriesToExport = freezeMode ? chart.Series["Örnek"] : chart.Series["Canlı"];
            foreach (var point in seriesToExport.Points)
            {
                sb.AppendLine($"{chartName};{point.XValue};{point.YValues[0]}");
            }
        }

        // Bir grafikteki "Canlı" seriyi "Örnek" serisine kopyalayan metot
        private void CopyBlueToRed(Chart chart)
        {
            var blue = chart.Series["Canlı"];
            var red = chart.Series["Örnek"];
            red.Points.Clear(); // Önceki örneği temizle
            foreach (var p in blue.Points)
                red.Points.AddXY(p.XValue, p.YValues[0]); // Canlıdaki her noktayı örneğe ekle
        }

        // Bir grafikteki "Örnek" serisini temizleyen metot
        private void ClearRed(Chart chart)
        {
            chart.Series["Örnek"].Points.Clear();
        }

        private void chartSagTopuk_Click(object sender, EventArgs e) { }
        private void lblAdimSayisiBaslik_Click(object sender, EventArgs e) { }
    }
}