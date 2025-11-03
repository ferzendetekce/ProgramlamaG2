using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RehabilitationSystem.Communication;
using DevicesControllerApp.Database;

namespace DevicesControllerApp.Servis
{
    public partial class Service : UserControl
    {
        // Cihaz haberleşme ve veritabanı yöneticisi
        private DeviceCommunication _device;
        private DatabaseManager _db;

        // Zamanlayıcı (canlı veri güncelleme)
        private Timer _uiTimer;

        // Basit durum
        private bool _connected;
        private string _lang = "TR"; // Servis'e özel dil anahtarı

        public Service()
        {
            InitializeComponent();
            InitializeRuntime();
        }

        /// <summary>
        /// Rol bazlı erişim: Sadece 'Servis' rolüne tüm kontroller açılır.
        /// Diğer rollerde kontroller pasif hale getirilir.
        /// </summary>
        /// <param name="roleName">Aktif kullanıcının rol adı</param>
        public void ApplyRole(string roleName)
        {
            bool isService = string.Equals(roleName, "Servis", StringComparison.OrdinalIgnoreCase);
            SetEnabledForService(isService);
            if (!isService)
            {
                lblDiagnostics.Text = "Erişim kısıtlı: Sadece Servis rolü";
            }
        }

        // Türkçe: Tüm servis kontrol gruplarını tek noktadan aç/kapat
        private void SetEnabledForService(bool enabled)
        {
            grpConnection.Enabled = enabled;
            grpServo.Enabled = enabled;
            grpStep.Enabled = enabled;
            grpRealtime.Enabled = enabled;
            grpLogs.Enabled = enabled;
            grpDiagnostics.Enabled = enabled;
        }

        /// <summary>
        /// Çalışma zamanı başlatma: portları yükle, panelleri hazırla, timer başlat
        /// </summary>
        private void InitializeRuntime()
        {
            // Sınıflar
            _device = DeviceCommunication.Instance;
            _db = new DatabaseManager();

            // Event abonelikleri (cihazdan veri geldiğinde UI güncelleyeceğiz)
            _device.LoadCellDataReceived += Device_LoadCellDataReceived;
            _device.ConnectionStatusChanged += Device_ConnectionStatusChanged;
            _device.DeviceStatusChanged += Device_DeviceStatusChanged;
            _device.ErrorOccurred += Device_ErrorOccurred;

            // Port listesini yükle
            try
            {
                var ports = _device.GetAvailablePorts() ?? new string[0];
                cmbPorts.Items.Clear();
                cmbPorts.Items.AddRange(ports);
                if (cmbPorts.Items.Count > 0) cmbPorts.SelectedIndex = 0;
            }
            catch { }

            // Servo ve Step motor kontrollerini oluştur
            BuildServoControls();
            BuildStepControls();

            // UI Timer
            _uiTimer = new Timer();
            _uiTimer.Interval = 500; // 2 Hz - canlı metin güncelleme için yeterli
            _uiTimer.Tick += UiTimer_Tick;
            _uiTimer.Start();

            // Varsayılan dil
            try
            {
                cmbServiceLanguage.SelectedIndex = 0; // TR
                ApplyLanguage(_lang);
            }
            catch { }
        }

        // 7 adet servo için satır bazlı kontrol elemanları oluşturulur
        private void BuildServoControls()
        {
            flowServo.Controls.Clear();
            for (int i = 0; i < 7; i++)
            {
                var panel = new Panel { Width = 360, Height = 30, Margin = new Padding(4) };
                var lbl = new Label { Text = $"Servo {i + 1}", AutoSize = true, Left = 4, Top = 7 };
                var num = new NumericUpDown { Minimum = -10000, Maximum = 10000, Left = 80, Top = 4, Width = 90 }; // konum/pozisyon
                var btn = new Button { Text = "Git", Left = 180, Top = 3, Width = 50 };
                var btnTest = new Button { Text = "Test", Left = 235, Top = 3, Width = 50 };
                var btnRead = new Button { Text = "Oku", Left = 290, Top = 3, Width = 50 };

                int motorIndex = i;
                btn.Click += (s, e) =>
                {
                    // Hedef pozisyona gönder
                    _device.SetServoMotorPosition(motorIndex, (int)num.Value);
                };
                btnTest.Click += (s, e) =>
                {
                    // Basit ileri-geri test hareketi
                    _device.SetServoMotorPosition(motorIndex, 100);
                    _device.SetServoMotorPosition(motorIndex, -100);
                    _device.SetServoMotorPosition(motorIndex, 0);
                };
                btnRead.Click += (s, e) =>
                {
                    var pos = _device.GetServoMotorPosition(motorIndex);
                    num.Value = Math.Max(num.Minimum, Math.Min(num.Maximum, pos));
                };

                panel.Controls.Add(lbl);
                panel.Controls.Add(num);
                panel.Controls.Add(btn);
                panel.Controls.Add(btnTest);
                panel.Controls.Add(btnRead);
                flowServo.Controls.Add(panel);
            }
        }

        // 10 adet step motor için kontrol
        private void BuildStepControls()
        {
            flowStep.Controls.Clear();
            for (int i = 0; i < 10; i++)
            {
                var panel = new Panel { Width = 360, Height = 30, Margin = new Padding(4) };
                var lbl = new Label { Text = $"Step {i + 1}", AutoSize = true, Left = 4, Top = 7 };
                var num = new NumericUpDown { Minimum = -200000, Maximum = 200000, Left = 80, Top = 4, Width = 90 }; // adım
                var btn = new Button { Text = "Git", Left = 180, Top = 3, Width = 50 };
                var btnTest = new Button { Text = "Test", Left = 235, Top = 3, Width = 50 };
                var btnRead = new Button { Text = "Oku", Left = 290, Top = 3, Width = 50 };

                int motorIndex = i;
                btn.Click += (s, e) =>
                {
                    _device.SetStepMotorPosition(motorIndex, (int)num.Value);
                };
                btnTest.Click += (s, e) =>
                {
                    _device.SetStepMotorPosition(motorIndex, 1000);
                    _device.SetStepMotorPosition(motorIndex, -1000);
                    _device.SetStepMotorPosition(motorIndex, 0);
                };
                btnRead.Click += (s, e) =>
                {
                    var pos = _device.GetStepMotorPosition(motorIndex);
                    num.Value = Math.Max(num.Minimum, Math.Min(num.Maximum, pos));
                };

                panel.Controls.Add(lbl);
                panel.Controls.Add(num);
                panel.Controls.Add(btn);
                panel.Controls.Add(btnTest);
                panel.Controls.Add(btnRead);
                flowStep.Controls.Add(panel);
            }
        }

        private void UiTimer_Tick(object sender, EventArgs e)
        {
            // Basit canlı durum metinleri periyodik yenileme
            lblStatus.Text = _connected ? "Durum: Bağlı" : "Durum: Bağlı Değil";
        }

        private void Device_LoadCellDataReceived(object sender, LoadCellDataEventArgs e)
        {
            // UI thread güvenli güncelleme
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateLoadCellLabel(e)));
            }
            else
            {
                UpdateLoadCellLabel(e);
            }
        }

        private void UpdateLoadCellLabel(LoadCellDataEventArgs e)
        {
            // LoadCell değerlerini kısaca yaz
            var d = e.Data;
            if (d != null)
            {
                lblLoadCells.Text = $"LoadCell: RH={d.RightHeel:F1} LH={d.LeftHeel:F1} RT={d.RightToe:F1} LT={d.LeftToe:F1} WB={d.WeightBalance:F1}";
            }
        }

        private void Device_ConnectionStatusChanged(object sender, ConnectionEventArgs e)
        {
            _connected = e.IsConnected;
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    lblStatus.Text = _connected ? $"Durum: Bağlı ({e.PortName})" : "Durum: Bağlı Değil";
                }));
            }
            else
            {
                lblStatus.Text = _connected ? $"Durum: Bağlı ({e.PortName})" : "Durum: Bağlı Değil";
            }
        }

        private void Device_DeviceStatusChanged(object sender, DeviceStatusEventArgs e)
        {
            // Limit switch özetini yaz
            if (e?.Status?.LimitSwitchStatus != null)
            {
                var flags = string.Join(" ", e.Status.LimitSwitchStatus.Select(b => b ? "1" : "0"));
                if (InvokeRequired)
                    BeginInvoke(new Action(() => lblLimitSwitch.Text = $"Limit Switch: {flags}"));
                else
                    lblLimitSwitch.Text = $"Limit Switch: {flags}";
            }

            // Diagnostik kısa özet
            if (e?.Status != null)
            {
                var s = e.Status;
                string text = $"Hazır={(s.IsReady ? "Evet" : "Hayır")} \nÇalışıyor={(s.IsRunning ? "Evet" : "Hayır")} \nAcil Stop={(s.IsEmergencyStopped ? "Evet" : "Hayır")} \nHız={s.CurrentSpeed:F1}";
                if (InvokeRequired)
                    BeginInvoke(new Action(() => lblDiagnostics.Text = text));
                else
                    lblDiagnostics.Text = text;
            }
        }

        private void Device_ErrorOccurred(object sender, ErrorEventArgs e)
        {
            //  Hata bilgisini sistem loguna eklemek (DatabaseManager tarafı henüz stub olabilir)
            try
            {
                _db.AddSystemLog(null, "DeviceError", e.ErrorMessage, null, e.Level.ToString());
            }
            catch { }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (cmbPorts.SelectedItem == null) return;
            var port = cmbPorts.SelectedItem.ToString();
            // Varsayılan parametrelerle bağlanmayı dene
            var ok = _device.OpenPort(port);
            if (!ok)
            {
                MessageBox.Show("Bağlantı başarısız.", "Cihaz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            _device.ClosePort();
        }

        private void btnHoming_Click(object sender, EventArgs e)
        {
            // Cihazı referans noktasına döndür
            var ok = _device.HomeDevice();
            if (!ok)
            {
                MessageBox.Show("Homing başarısız.", "Cihaz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            // Basit kalibrasyon örneği (LoadCell okuma tetikleme)
            _device.RequestLoadCellData();
            MessageBox.Show("Kalibrasyon komutu gönderildi.", "Cihaz", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnFilterLogs_Click(object sender, EventArgs e)
        {
            // Sistem loglarını filtrele ve göster (DatabaseManager metodları henüz stub olabilir)
            DateTime? start = dtpLogStart.Value;
            DateTime? end = dtpLogEnd.Value;
            int? userId = null;
            string level = string.IsNullOrWhiteSpace(cmbLogLevel.Text) ? null : cmbLogLevel.Text;

            try
            {
                var dt = _db.GetSystemLogs(start, end, userId, level);
                dgvLogs.DataSource = dt;
                ApplySearchFilter();
                LoadRecentErrors();
                UpdateStatsFromGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loglar yüklenirken hata: " + ex.Message);
            }
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            // Basit CSV export
            if (dgvLogs.DataSource is DataTable dt)
            {
                using (var sfd = new SaveFileDialog { Filter = "CSV (*.csv)|*.csv", FileName = "logs.csv" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            // Başlıklar
                            var cols = dt.Columns.Cast<DataColumn>().Select(c => EscapeCsv(c.ColumnName));
                            sb.AppendLine(string.Join(",", cols));
                            foreach (DataRow row in dt.Rows)
                            {
                                var vals = dt.Columns.Cast<DataColumn>().Select(c => EscapeCsv(Convert.ToString(row[c])));
                                sb.AppendLine(string.Join(",", vals));
                            }
                            System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                            MessageBox.Show("CSV kayıt edildi.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("CSV export hatası: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Basit XLSX yerine CSV yedek; gerçek Excel kütüphanesi entegre edilince güncellenebilir
            btnExportCsv_Click(sender, e);
        }

        private string EscapeCsv(string input)
        {
            if (input == null) return "";
            if (input.Contains(",") || input.Contains("\"") || input.Contains("\n"))
            {
                return "\"" + input.Replace("\"", "\"\"") + "\"";
            }
            return input;
        }

        private void txtLogSearch_TextChanged(object sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        // DataGridView'e bağlı DataTable üzerinde metin araması uygular
        private void ApplySearchFilter()
        {
            try
            {
                if (dgvLogs.DataSource is DataTable dt)
                {
                    string text = txtLogSearch.Text?.Trim();
                    if (string.IsNullOrEmpty(text))
                    {
                        dt.DefaultView.RowFilter = string.Empty;
                        return;
                    }

                    var filters = new List<string>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        string colName = col.ColumnName.Replace("'", "''");
                        string val = text.Replace("'", "''");
                        filters.Add($"Convert([{colName}], 'System.String') LIKE '%{val}%'");
                    }
                    dt.DefaultView.RowFilter = string.Join(" OR ", filters);
                }
            }
            catch { }
        }

        private void dgvLogs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvLogs.Rows[e.RowIndex];
            var sb = new StringBuilder();
            foreach (DataGridViewColumn col in dgvLogs.Columns)
            {
                var val = row.Cells[col.Index].Value;
                sb.AppendLine($"{col.HeaderText}: {Convert.ToString(val)}");
            }
            MessageBox.Show(sb.ToString(), "Log Detayı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLoadDeviceLogs_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = _db.GetDeviceStatusLogs(dtpLogStart.Value, dtpLogEnd.Value);
                dgvLogs.DataSource = dt;
                ApplySearchFilter();
                UpdateStatsFromGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cihaz logları yüklenirken hata: " + ex.Message);
            }
        }

        private void LoadRecentErrors()
        {
            try
            {
                var dt = _db.GetRecentErrors(10);
                lstRecentErrors.Items.Clear();
                if (dt != null)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string ts = r.Table.Columns.Contains("Zaman") ? Convert.ToString(r["Zaman"]) : "";
                        string msg = r.Table.Columns.Contains("Mesaj") ? Convert.ToString(r["Mesaj"]) : r[0].ToString();
                        lstRecentErrors.Items.Add($"{ts} {msg}".Trim());
                    }
                }
            }
            catch { }
        }

        // Servis içi basit istatistikler (grid verisinden) hesaplanır
        private void UpdateStatsFromGrid()
        {
            try
            {
                if (!(dgvLogs.DataSource is DataTable dt)) { lblStats.Text = Translate("İstatistikler için veri yok", "No data for statistics"); return; }
                int total = dt.DefaultView.Count;
                int errors = 0, critical = 0;
                foreach (DataRowView rv in dt.DefaultView)
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        var v = Convert.ToString(rv[c.ColumnName]);
                        if (string.IsNullOrEmpty(v)) continue;
                        var vv = v.ToLowerInvariant();
                        if (vv.Contains("critical")) { critical++; break; }
                        if (vv.Contains("error")) { errors++; break; }
                    }
                }
                lblStats.Text = Translate($"Toplam Kayıt: {total} | Hata: {errors} | Kritik: {critical}",
                                          $"Total: {total} | Error: {errors} | Critical: {critical}");
            }
            catch
            {
                lblStats.Text = Translate("İstatistik hesaplanamadı","Statistics could not be computed");
            }
        }

        private void cmbServiceLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lang = cmbServiceLanguage.SelectedItem?.ToString() == "EN" ? "EN" : "TR";
            ApplyLanguage(_lang);
        }

        // Servis’e özel basit dil uygulaması
        private void ApplyLanguage(string lang)
        {
            bool isTR = lang != "EN";
            grpConnection.Text = Translate("Cihaz Bağlantı","Device Connection");
            btnConnect.Text = Translate("Bağlan","Connect");
            btnDisconnect.Text = Translate("Kes","Disconnect");
            grpServo.Text = Translate("Servo Motorlar (7)","Servo Motors (7)");
            grpStep.Text = Translate("Step Motorlar (10)","Step Motors (10)");
            grpRealtime.Text = Translate("Canlı Veriler / Hızlı İşlemler","Realtime / Quick Actions");
            btnCalibrate.Text = Translate("Kalibrasyon","Calibrate");
            btnHoming.Text = Translate("Homing","Homing");
            grpLogs.Text = Translate("Log İnceleme","Logs");
            btnFilterLogs.Text = Translate("Filtrele","Filter");
            btnExportCsv.Text = "CSV";
            btnExportExcel.Text = "Excel";
            lblLogSearch.Text = Translate("Ara (metin)","Search (text)");
            btnLoadDeviceLogs.Text = Translate("Cihaz Durum Loglarını Yükle","Load Device Status Logs");
            grpDiagnostics.Text = Translate("Diagnostik","Diagnostics");
            lblLang.Text = Translate("Dil:","Lang:");

            // Durum metinleri
            lblStatus.Text = _connected ? Translate("Durum: Bağlı","Status: Connected") : Translate("Durum: Bağlı Değil","Status: Disconnected");
            if (lblDiagnostics.Text == "Cihaz sağlık özeti hazırlanıyor" || lblDiagnostics.Text == "Preparing device health summary")
                lblDiagnostics.Text = Translate("Cihaz sağlık özeti hazırlanıyor","Preparing device health summary");
            if (lblStats.Text == "İstatistikler hazırlanıyor" || lblStats.Text == "Preparing statistics")
                lblStats.Text = Translate("İstatistikler hazırlanıyor","Preparing statistics");
        }

        private string Translate(string tr, string en)
        {
            return _lang == "EN" ? en : tr;
        }
    }
}
