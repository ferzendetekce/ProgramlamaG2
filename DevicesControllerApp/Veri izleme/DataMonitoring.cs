using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Npgsql;

namespace DevicesControllerApp.Veri_izleme
{
    public partial class DataMonitoring : UserControl
    {
        // --- Sabit seri anahtarları (kodu kırmamak için DEĞİŞMEZ) ---
        private const string SERIES_LIVE = "Canlı";
        private const string SERIES_SAMPLE = "Örnek";

        private int pointCount = 100;
        private DataTable dtSimulationData;
        private int simIndex = 0;

        private string currentLang = "TR";

        private const string ConnectionString =
            "Host=localhost;Port=5432;Database=veri_izleme;Username=postgres;Password=1234";

        private enum SampleState { None, Recording, Stopped }
        private SampleState sampleState = SampleState.None;

        public DataMonitoring()
        {
            InitializeComponent();
        }

        private void DataMonitoring_Load_1(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                cmbDataDil.Items.Clear();
                cmbDataDil.Items.Add("Türkçe");
                cmbDataDil.Items.Add("English");
                cmbDataDil.Items.Add("العربية");
                cmbDataDil.SelectedIndex = 0;

                SetupCharts();

                dgvRehList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvRehList.MultiSelect = false;
                dgvRehList.ReadOnly = true;
                dgvRehList.AllowUserToAddRows = false;

                ChangeLanguage("TR");
                LoadRehabilitationList();
            }
        }

        private void cmbDataDil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDataDil.SelectedIndex == 1) ChangeLanguage("EN");
            else if (cmbDataDil.SelectedIndex == 2) ChangeLanguage("AR");
            else ChangeLanguage("TR");
        }

        private void ChangeLanguage(string lang)
        {
            currentLang = lang;

            if (lang == "EN")
            {
                UpdateChartTitle(chartSagTaban, "Right Sole");
                UpdateChartTitle(chartSolTaban, "Left Sole");
                UpdateChartTitle(chartSagTopuk, "Right Heel");
                UpdateChartTitle(chartSolTopuk, "Left Heel");
                UpdateChartTitle(chartAgirlik, "Weight Balance");

                btnExportData.Text = "Export";

                if (dgvRehList.Columns.Count >= 3)
                {
                    dgvRehList.Columns[0].HeaderText = "ID";
                    dgvRehList.Columns[1].HeaderText = "Patient ID";
                    dgvRehList.Columns[2].HeaderText = "Date/Time";
                }

                if (chartAgirlik.Series.Count > 0) chartAgirlik.Series[0].Name = "Balanced Weight";
            }
            else if (lang == "AR")
            {
                UpdateChartTitle(chartSagTaban, "باطن القدم اليمنى");
                UpdateChartTitle(chartSolTaban, "باطن القدم اليسرى");
                UpdateChartTitle(chartSagTopuk, "كعب القدم اليمنى");
                UpdateChartTitle(chartSolTopuk, "كعب القدم اليسرى");
                UpdateChartTitle(chartAgirlik, "توازن الوزن");

                btnExportData.Text = "تصدير";

                if (dgvRehList.Columns.Count >= 3)
                {
                    dgvRehList.Columns[0].HeaderText = "المعرّف";
                    dgvRehList.Columns[1].HeaderText = "رقم المريض";
                    dgvRehList.Columns[2].HeaderText = "التاريخ/الوقت";
                }

                if (chartAgirlik.Series.Count > 0) chartAgirlik.Series[0].Name = "الوزن المتوازن";
            }
            else
            {
                UpdateChartTitle(chartSagTaban, "Sağ Ayak Tabanı");
                UpdateChartTitle(chartSolTaban, "Sol Ayak Tabanı");
                UpdateChartTitle(chartSagTopuk, "Sağ Ayak Topuğu");
                UpdateChartTitle(chartSolTopuk, "Sol Ayak Topuğu");
                UpdateChartTitle(chartAgirlik, "Ağırlık Dengesi");

                btnExportData.Text = "Dışarı Aktar";

                if (dgvRehList.Columns.Count >= 3)
                {
                    dgvRehList.Columns[0].HeaderText = "ID";
                    dgvRehList.Columns[1].HeaderText = "Hasta No";
                    dgvRehList.Columns[2].HeaderText = "Tarih/Saat";
                }

                if (chartAgirlik.Series.Count > 0) chartAgirlik.Series[0].Name = "Dengelenmiş Ağırlık";
            }

            SetHeaderLabels();
            UpdateSampleButtonText();

            // >>> Canlı / Örnek yazıları da dil değişince güncellensin
            ApplySeriesDisplayNames();
        }

        private void UpdateSampleButtonText()
        {
            if (currentLang == "EN")
            {
                switch (sampleState)
                {
                    case SampleState.None: btnFreeze.Text = "Take Sample"; break;
                    case SampleState.Recording: btnFreeze.Text = "Stop"; break;
                    case SampleState.Stopped: btnFreeze.Text = "Delete Sample"; break;
                    default: btnFreeze.Text = "Take Sample"; break;
                }
            }
            else if (currentLang == "AR")
            {
                switch (sampleState)
                {
                    case SampleState.None: btnFreeze.Text = "تسجيل عينة"; break;
                    case SampleState.Recording: btnFreeze.Text = "إيقاف"; break;
                    case SampleState.Stopped: btnFreeze.Text = "حذف العينة"; break;
                    default: btnFreeze.Text = "تسجيل عينة"; break;
                }
            }
            else
            {
                switch (sampleState)
                {
                    case SampleState.None: btnFreeze.Text = "Örnek Al"; break;
                    case SampleState.Recording: btnFreeze.Text = "Durdur"; break;
                    case SampleState.Stopped: btnFreeze.Text = "Örnek Sil"; break;
                    default: btnFreeze.Text = "Örnek Al"; break;
                }
            }
        }

        private void UpdateChartTitle(Chart chart, string newTitle)
        {
            if (chart.Titles.Count > 0) chart.Titles[0].Text = newTitle;
            else chart.Titles.Add(newTitle);
        }

        // -------------------- LISTE --------------------
        private void LoadRehabilitationList()
        {
            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();

                    string sql =
                        "SELECT terapi_id, hasta_id, terapi_baslangic_zamani " +
                        "FROM public.terapiler_tablosu " +
                        "ORDER BY terapi_id DESC";

                    using (var da = new NpgsqlDataAdapter(sql, conn))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvRehList.DataSource = dt;

                        ChangeLanguage(currentLang);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(BuildDbErrorMessage(ex), "Database");
            }
        }

        private void dgvRehList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvRehList.SelectedRows.Count == 0) return;

            int secilenId = Convert.ToInt32(dgvRehList.SelectedRows[0].Cells[0].Value);
            LoadSimulationDataFromDB(secilenId);

            if (dtSimulationData == null || dtSimulationData.Rows.Count == 0)
            {
                MessageBox.Show(BuildNoDataMessage(secilenId), "Info");
                return;
            }

            sampleState = SampleState.None;
            UpdateSampleButtonText();

            ClearAllCharts();
            simIndex = 0;

            timerSim.Stop();
            timerSim.Start();
        }

        private void LoadSimulationDataFromDB(int terapiId)
        {
            dtSimulationData = new DataTable();

            try
            {
                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();

                    string sql = @"
                        SELECT *
                        FROM public.loadcell_verileri_tablosu
                        WHERE terapi_id = @id
                        ORDER BY loadcell_id ASC";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", terapiId);

                        using (var da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dtSimulationData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(BuildLoadcellErrorMessage(ex), "Database");
            }
        }

        // -------------------- TIMER --------------------
        private void timerSim_Tick(object sender, EventArgs e)
        {
            if (dtSimulationData == null || dtSimulationData.Rows.Count == 0)
            {
                timerSim.Stop();
                return;
            }

            if (simIndex >= dtSimulationData.Rows.Count)
            {
                timerSim.Stop();
                return;
            }

            try
            {
                DataRow row = dtSimulationData.Rows[simIndex];

                double sagTopuk = Clamp01To100(GetDouble(row, "sag_topuk_basinc_degeri"));
                double solTopuk = Clamp01To100(GetDouble(row, "sol_topuk_basinc_degeri"));
                double sagTaban = Clamp01To100(GetDouble(row, "sag_on_ayak_basinc_degeri"));
                double solTaban = Clamp01To100(GetDouble(row, "sol_on_ayak_basinc_degeri"));

                DateTime ts = GetDateTime(row, "zaman_damgasi", DateTime.Now);

                AddPointToChartLive(chartSagTopuk, ts, sagTopuk);
                AddPointToChartLive(chartSolTopuk, ts, solTopuk);
                AddPointToChartLive(chartSagTaban, ts, sagTaban);
                AddPointToChartLive(chartSolTaban, ts, solTaban);

                if (sampleState == SampleState.Recording)
                {
                    AddPointToChartSample(chartSagTopuk, ts, sagTopuk);
                    AddPointToChartSample(chartSolTopuk, ts, solTopuk);
                    AddPointToChartSample(chartSagTaban, ts, sagTaban);
                    AddPointToChartSample(chartSolTaban, ts, solTaban);
                }

                double agirlikDengeleme = GetDouble(row, "agirlik_dengeleme_degeri");
                double azaltilanAgirlik = GetDouble(row, "azaltilan_agirlik_degeri");
                double dengelenmisAgirlik = Clamp01To100(agirlikDengeleme - azaltilanAgirlik);

                if (chartAgirlik.Series.Count > 0)
                {
                    chartAgirlik.Series[0].Points.Clear();
                    chartAgirlik.Series[0].Points.AddY(dengelenmisAgirlik);
                }

                simIndex++;
            }
            catch (Exception ex)
            {
                timerSim.Stop();
                MessageBox.Show(BuildRuntimeErrorMessage(ex), "Runtime");
            }
        }

        // -------------------- SAMPLE BUTTON --------------------
        private void btnFreeze_Click(object sender, EventArgs e)
        {
            switch (sampleState)
            {
                case SampleState.None:
                    ClearSampleCharts();
                    sampleState = SampleState.Recording;
                    break;

                case SampleState.Recording:
                    sampleState = SampleState.Stopped;
                    break;

                case SampleState.Stopped:
                    ClearSampleCharts();
                    sampleState = SampleState.None;
                    break;
            }

            UpdateSampleButtonText();
        }

        // -------------------- EXPORT --------------------
        private void btnExportData_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File (*.csv)|*.csv";
                sfd.FileName = $"rehab_data_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var sb = new System.Text.StringBuilder();
                        sb.AppendLine("Chart;Time;Value");

                        ExportChartDataToCsv(sb, chartSagTaban, "Right_Sole");
                        ExportChartDataToCsv(sb, chartSolTaban, "Left_Sole");
                        ExportChartDataToCsv(sb, chartSagTopuk, "Right_Heel");
                        ExportChartDataToCsv(sb, chartSolTopuk, "Left_Heel");

                        File.WriteAllText(sfd.FileName, sb.ToString());
                        MessageBox.Show(BuildExportOkMessage(), "Export");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(BuildExportErrorMessage(ex), "Export");
                    }
                }
            }
        }

        private void ExportChartDataToCsv(System.Text.StringBuilder sb, Chart chart, string chartName)
        {
            bool hasSample = (sampleState == SampleState.Recording || sampleState == SampleState.Stopped);
            var series = hasSample ? chart.Series[SERIES_SAMPLE] : chart.Series[SERIES_LIVE];

            foreach (var p in series.Points)
            {
                DateTime t = DateTime.FromOADate(p.XValue);
                sb.AppendLine($"{chartName};{t:HH:mm:ss};{p.YValues[0]}");
            }
        }

        // -------------------- CHART SETUP --------------------
        private void SetupCharts()
        {
            SetupScatterChart(chartSagTaban, "Sağ Ayak Tabanı", Color.CornflowerBlue);
            SetupScatterChart(chartSolTaban, "Sol Ayak Tabanı", Color.MediumSeaGreen);
            SetupScatterChart(chartSagTopuk, "Sağ Ayak Topuğu", Color.Yellow);
            SetupScatterChart(chartSolTopuk, "Sol Ayak Topuğu", Color.MediumPurple);

            SetupBarChart(chartAgirlik, "Ağırlık Dengesi", 0, 100);

            // İlk kurulumdan sonra legend yazılarını da ayarla
            ApplySeriesDisplayNames();
        }

        private void SetupScatterChart(Chart chart, string title, Color color)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(title);

            EnsureLegend(chart);

            var area = chart.ChartAreas[0];
            area.AxisX.LabelStyle.Format = "HH:mm:ss";
            area.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            area.AxisX.MajorGrid.LineColor = Color.LightGray;

            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = 100;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;

            var live = chart.Series.Add(SERIES_LIVE);
            live.ChartType = SeriesChartType.Line;
            live.Color = color;
            live.BorderWidth = 3;
            live.XValueType = ChartValueType.DateTime;
            live.IsVisibleInLegend = true;

            var sample = chart.Series.Add(SERIES_SAMPLE);
            sample.ChartType = SeriesChartType.Line;
            sample.Color = Color.Red;
            sample.BorderWidth = 3;
            sample.XValueType = ChartValueType.DateTime;
            sample.IsVisibleInLegend = true;
        }

        private void SetupBarChart(Chart chart, string title, double min, double max)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(title);

            EnsureLegend(chart);

            var area = chart.ChartAreas[0];
            area.AxisY.Minimum = min;
            area.AxisY.Maximum = max;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;

            area.AxisX.LabelStyle.Enabled = false;
            area.AxisX.MajorTickMark.Enabled = false;

            var s = chart.Series.Add("Dengelenmiş Ağırlık");
            s.ChartType = SeriesChartType.Column;
            s.Color = Color.Gold;
            s.IsVisibleInLegend = false; // tek sütun, legend kalabalık yapmasın
        }

        private void EnsureLegend(Chart chart)
        {
            if (chart.Legends.Count == 0)
                chart.Legends.Add(new Legend("Legend1"));

            chart.Legends[0].Enabled = true;
        }

        // >>> DİL DEĞİŞİNCE: legend’de görünen “Canlı/Örnek” yazıları değişsin
        private void ApplySeriesDisplayNames()
        {
            string liveText = GetLiveSeriesText();
            string sampleText = GetSampleSeriesText();

            foreach (var ch in new[] { chartSagTaban, chartSolTaban, chartSagTopuk, chartSolTopuk })
            {
                if (ch == null) continue;
                EnsureLegend(ch);

                if (ch.Series.IndexOf(SERIES_LIVE) >= 0)
                    ch.Series[SERIES_LIVE].LegendText = liveText;

                if (ch.Series.IndexOf(SERIES_SAMPLE) >= 0)
                    ch.Series[SERIES_SAMPLE].LegendText = sampleText;
            }
        }

        private string GetLiveSeriesText()
        {
            if (currentLang == "EN") return "Live";
            if (currentLang == "AR") return "مباشر";
            return "Canlı";
        }

        private string GetSampleSeriesText()
        {
            if (currentLang == "EN") return "Sample";
            if (currentLang == "AR") return "عينة";
            return "Örnek";
        }

        // -------------------- POINT ADD --------------------
        private void AddPointToChartLive(Chart chart, DateTime xTime, double yValue)
        {
            var series = chart.Series[SERIES_LIVE];
            series.XValueType = ChartValueType.DateTime;

            series.Points.AddXY(xTime, yValue);

            if (series.Points.Count > pointCount) series.Points.RemoveAt(0);
            chart.ChartAreas[0].RecalculateAxesScale();
        }

        private void AddPointToChartSample(Chart chart, DateTime xTime, double yValue)
        {
            var series = chart.Series[SERIES_SAMPLE];
            series.XValueType = ChartValueType.DateTime;

            series.Points.AddXY(xTime, yValue);

            if (series.Points.Count > pointCount) series.Points.RemoveAt(0);
            chart.ChartAreas[0].RecalculateAxesScale();
        }

        // -------------------- CLEAR --------------------
        private void ClearAllCharts()
        {
            foreach (var ch in new[] { chartSagTaban, chartSolTaban, chartSagTopuk, chartSolTopuk })
            {
                ch.Series[SERIES_LIVE].Points.Clear();
                ch.Series[SERIES_SAMPLE].Points.Clear();
            }

            if (chartAgirlik.Series.Count > 0) chartAgirlik.Series[0].Points.Clear();
        }

        private void ClearSampleCharts()
        {
            foreach (var ch in new[] { chartSagTaban, chartSolTaban, chartSagTopuk, chartSolTopuk })
                ch.Series[SERIES_SAMPLE].Points.Clear();
        }

        // -------------------- SAFE READ --------------------
        private double Clamp01To100(double v)
        {
            if (v < 0) return 0;
            if (v > 100) return 100;
            return v;
        }

        private double GetDouble(DataRow row, string col)
        {
            if (!row.Table.Columns.Contains(col)) return 0;
            return row[col] != DBNull.Value ? Convert.ToDouble(row[col]) : 0;
        }

        private DateTime GetDateTime(DataRow row, string col, DateTime fallback)
        {
            if (!row.Table.Columns.Contains(col)) return fallback;
            return row[col] != DBNull.Value ? Convert.ToDateTime(row[col]) : fallback;
        }

        // -------------------- LABELS --------------------
        private Label FindLabelRecursive(Control root, string labelName)
        {
            foreach (Control c in root.Controls)
            {
                var lbl = c as Label;
                if (lbl != null && string.Equals(lbl.Name, labelName, StringComparison.Ordinal))
                    return lbl;

                var found = FindLabelRecursive(c, labelName);
                if (found != null) return found;
            }
            return null;
        }

        private void SetHeaderLabels()
        {
            Label lblData = FindLabelRecursive(this, "lblDataBaslık");
            if (lblData == null) lblData = FindLabelRecursive(this, "lblDataBaslik");

            Label lblVeri = FindLabelRecursive(this, "lblVeriBaslik");
            if (lblVeri == null) lblVeri = FindLabelRecursive(this, "lblVeriBaslık");

            if (currentLang == "EN")
            {
                if (lblData != null) lblData.Text = "Past Rehabilitation Records";
                if (lblVeri != null) lblVeri.Text = "Data Monitoring";
            }
            else if (currentLang == "AR")
            {
                if (lblData != null) lblData.Text = "سجلات التأهيل السابقة";
                if (lblVeri != null) lblVeri.Text = "مراقبة البيانات";
            }
            else
            {
                if (lblData != null) lblData.Text = "Geçmiş Rehabilitasyon Kayıtları";
                if (lblVeri != null) lblVeri.Text = "Veri İzleme";
            }
        }

        // -------------------- MESSAGES --------------------
        private string BuildDbErrorMessage(Exception ex)
        {
            if (currentLang == "EN")
                return "Cannot connect to the database. Check if PostgreSQL is running and the connection string is correct.\n\nDetails: " + ex.Message;

            if (currentLang == "AR")
                return "تعذّر الاتصال بقاعدة البيانات. تأكد من تشغيل PostgreSQL وصحة بيانات الاتصال.\n\nالتفاصيل: " + ex.Message;

            return "Veritabanına bağlanılamadı. PostgreSQL çalışıyor mu ve bağlantı bilgileri doğru mu kontrol et.\n\nDetay: " + ex.Message;
        }

        private string BuildLoadcellErrorMessage(Exception ex)
        {
            if (currentLang == "EN")
                return "Loadcell data could not be loaded. Check table/column names and permissions.\n\nDetails: " + ex.Message;

            if (currentLang == "AR")
                return "تعذّر تحميل بيانات الحساسات (Loadcell). تحقق من أسماء الجداول/الأعمدة والصلاحيات.\n\nالتفاصيل: " + ex.Message;

            return "Loadcell verileri yüklenemedi. Tablo/sütun adlarını ve yetkileri kontrol et.\n\nDetay: " + ex.Message;
        }

        private string BuildNoDataMessage(int terapiId)
        {
            if (currentLang == "EN")
                return $"No loadcell data found for Therapy ID = {terapiId}. Check loadcell_verileri_tablosu.";

            if (currentLang == "AR")
                return $"لا توجد بيانات Loadcell للمعرّف العلاجي = {terapiId}. تحقق من جدول loadcell_verileri_tablosu.";

            return $"Terapi ID = {terapiId} için loadcell verisi bulunamadı. loadcell_verileri_tablosu tablosunu kontrol et.";
        }

        private string BuildRuntimeErrorMessage(Exception ex)
        {
            if (currentLang == "EN")
                return "An error occurred while processing the data stream.\n\nDetails: " + ex.Message;

            if (currentLang == "AR")
                return "حدث خطأ أثناء معالجة تدفق البيانات.\n\nالتفاصيل: " + ex.Message;

            return "Veri akışı işlenirken hata oluştu.\n\nDetay: " + ex.Message;
        }

        private string BuildExportOkMessage()
        {
            if (currentLang == "EN") return "Export completed.";
            if (currentLang == "AR") return "تم التصدير بنجاح.";
            return "Dışarı aktarma tamamlandı.";
        }

        private string BuildExportErrorMessage(Exception ex)
        {
            if (currentLang == "EN")
                return "Export failed.\n\nDetails: " + ex.Message;

            if (currentLang == "AR")
                return "فشل التصدير.\n\nالتفاصيل: " + ex.Message;

            return "Dışarı aktarma başarısız.\n\nDetay: " + ex.Message;
        }

        private void DataMonitoring_Click(object sender, EventArgs e) { }
        private void chartSagTopuk_Click(object sender, EventArgs e) { }
    }
}
