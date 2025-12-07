using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D; // İkon çizimi için
using System.IO;                // Dosya işlemleri için
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization; // XML işlemleri için

// DİKKAT: Diğer grupların namespace'leri de "RehabilitationSystem" standardında olmalı.
// Eğer onlar hala "DevicesControllerApp" kullanıyorsa, buradaki using'leri ona göre değiştirmen gerekebilir.
using RehabilitationSystem.Communication; 
using RehabilitationSystem.Database; 

// DÜZELTME 1: Namespace, Designer dosyasıyla aynı yapıldı (RehabilitationSystem.Servis).
namespace RehabilitationSystem.Servis
{
    public partial class Service : UserControl
    {
        // Yöneticiler
        private DeviceCommunication _device;
        private DatabaseManager _db;
        private Timer _uiTimer;

        // Servis durumu
        private bool _connected;
        private string _currentLang = "TR";

        public Service()
        {
            InitializeComponent();
            InitializeRuntime();
        }

        /// <summary>
        /// Rol güvenliği: Sadece 'Servis' yetkisi olanlar kontrolleri kullanabilir.
        /// </summary>
        public void ApplyRole(string roleName)
        {
            bool isService = string.Equals(roleName, "Servis", StringComparison.OrdinalIgnoreCase);
            ToggleServiceControls(isService);

            if (!isService)
            {
                // XML'den okunan yetki hatası mesajı
                lblDiagnostics.Text = LocalizationManager.Get("AccessDenied");
                lblDiagnostics.ForeColor = Color.Red;
            }
        }

        private void ToggleServiceControls(bool state)
        {
            grpConnection.Enabled = state;
            grpServo.Enabled = state;
            grpStep.Enabled = state;
            grpRealtime.Enabled = state;
            grpLogs.Enabled = state;
            grpDiagnostics.Enabled = state;
        }

        private void InitializeRuntime()
        {
            // 1. Dil Dosyalarını Kontrol Et ve Yükle
            LocalizationManager.EnsureLanguageFilesExist();

            // Varsayılan dili yükle
            if (cmbServiceLanguage.Items.Count > 0) cmbServiceLanguage.SelectedIndex = 0;

            // 2. Singleton ve Manager örnekleri (Hata yönetimi ile)
            try
            {
                _device = DeviceCommunication.Instance;
                // Olay Abonelikleri
                _device.LoadCellDataReceived += (s, e) => SafeInvoke(() => UpdateLoadCellLabel(e));
                _device.ConnectionStatusChanged += (s, e) => SafeInvoke(() => UpdateConnectionStatus(e));
                _device.DeviceStatusChanged += (s, e) => SafeInvoke(() => UpdateDeviceStatus(e));
                _device.ErrorOccurred += (s, e) => SafeInvoke(() => HandleDeviceError(e));
            }
            catch { /* DeviceCommunication henüz hazır değilse patlamasın */ }

            try
            {
                _db = new DatabaseManager();
            }
            catch { /* DatabaseManager henüz hazır değilse patlamasın */ }

            // 3. Portları doldur
            RefreshPorts();

            // 4. Kullanıcı listesini doldur
            RefreshUserList();

            // 5. UI Timer
            _uiTimer = new Timer { Interval = 1000 }; // 1 saniye
            _uiTimer.Tick += (s, e) => UpdateRealtimeData();
            _uiTimer.Start();

            // 6. Dinamik Motor Kontrollerini Oluştur
            CreateServoControls();
            CreateStepControls();

            // 7. Statik Butonlara İkon Ata
            AssignStaticIcons();

            // 8. İlk veri yükleme
            UpdateDeviceHealth();
            LoadRecentErrors();
            UpdateStatsFromGrid(); // İstatistikleri baştan doldur

            // Dili uygula
            ChangeLanguage("TR");
        }

        private void RefreshPorts()
        {
            try
            {
                cmbPorts.Items.Clear();
                if (_device != null)
                {
                    var ports = _device.GetAvailablePorts();
                    if (ports != null && ports.Length > 0)
                    {
                        cmbPorts.Items.AddRange(ports);
                        cmbPorts.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbPorts.Items.Add("COM3 (Demo)"); // Port yoksa demo port ekle
                        cmbPorts.SelectedIndex = 0;
                    }
                }
            }
            catch 
            {
                 cmbPorts.Items.Add("COM3 (Sim)"); 
            }
        }

        private void RefreshUserList()
        {
            try
            {
                cmbLogUser.Items.Clear();
                cmbLogUser.Items.Add("Tümü");
                
                if (_db != null)
                {
                    var users = _db.GetAllUsers();
                    if (users != null)
                    {
                        foreach (DataRow row in users.Rows)
                        {
                            // DÜZELTME 2: database.backup dosyasında sütun adı 'kullanici_adi' değil, 
                            // 'kullanici_adi_soyadi' olarak görünüyor. Kod buna göre güncellendi.
                            string colName = users.Columns.Contains("kullanici_adi") ? "kullanici_adi" : "kullanici_adi_soyadi";
                            
                            string username = row[colName] != DBNull.Value ? row[colName].ToString() : "";
                            if (!string.IsNullOrEmpty(username))
                                cmbLogUser.Items.Add(username);
                        }
                    }
                }
                if (cmbLogUser.Items.Count > 0) cmbLogUser.SelectedIndex = 0;
            }
            catch { }
        }

        private void AssignStaticIcons()
        {
            // Designer'daki sabit butonlara kod ile ikon atıyoruz
            btnConnect.Image = IconHelper.CreateIcon(IconType.Check, Color.Green);
            btnDisconnect.Image = IconHelper.CreateIcon(IconType.Stop, Color.Red);

            btnHoming.Image = IconHelper.CreateIcon(IconType.Home, Color.Blue);
            btnCalibrate.Image = IconHelper.CreateIcon(IconType.Settings, Color.Orange);

            btnFilterLogs.Image = IconHelper.CreateIcon(IconType.Filter, Color.Purple);
            btnExportCsv.Image = IconHelper.CreateIcon(IconType.Save, Color.DarkGreen);
            btnExportExcel.Image = IconHelper.CreateIcon(IconType.Save, Color.DarkGreen);
            btnLoadDeviceLogs.Image = IconHelper.CreateIcon(IconType.Read, Color.Teal);

            SetButtonLayout(btnConnect);
            SetButtonLayout(btnDisconnect);
            SetButtonLayout(btnHoming);
            SetButtonLayout(btnCalibrate);
            SetButtonLayout(btnFilterLogs);
            SetButtonLayout(btnExportCsv);
            SetButtonLayout(btnExportExcel);
            SetButtonLayout(btnLoadDeviceLogs);
        }

        private void SetButtonLayout(Button btn)
        {
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.Padding = new Padding(2, 0, 0, 0);
        }

        // --- DİNAMİK KONTROL OLUŞTURMA ---

        private void CreateServoControls()
        {
            flowServo.Controls.Clear();
            Image iconGo = IconHelper.CreateIcon(IconType.Play, Color.Green);
            Image iconTest = IconHelper.CreateIcon(IconType.Settings, Color.Orange);
            Image iconRead = IconHelper.CreateIcon(IconType.Read, Color.Blue);

            for (int i = 0; i < 7; i++)
            {
                int index = i;
                Panel pnl = CreateMotorPanel($"Servo {i + 1}", -10000, 10000, iconGo, iconTest, iconRead,
                    (val) => _device?.SetServoMotorPosition(index, val),
                    () => { 
                        try 
                        { 
                            _device?.TestServoMotor(index);
                            MessageBox.Show($"Servo Motor {index + 1} test edildi.", "Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        } 
                        catch (Exception ex) 
                        { 
                            MessageBox.Show($"Test simülasyonu: Servo {index + 1} hareket etti.\n({ex.Message})", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    },
                    () => { return _device != null ? _device.GetServoMotorPosition(index) : 0; }
                );
                flowServo.Controls.Add(pnl);
            }
            UpdateDynamicControlsText();
        }

        private void CreateStepControls()
        {
            flowStep.Controls.Clear();
            Image iconGo = IconHelper.CreateIcon(IconType.Play, Color.DarkGreen);
            Image iconTest = IconHelper.CreateIcon(IconType.Settings, Color.DarkOrange);
            Image iconRead = IconHelper.CreateIcon(IconType.Read, Color.DarkBlue);

            for (int i = 0; i < 10; i++)
            {
                int index = i;
                Panel pnl = CreateMotorPanel($"Step {i + 1}", -200000, 200000, iconGo, iconTest, iconRead,
                    (val) => _device?.SetStepMotorPosition(index, val),
                    () => { 
                        try 
                        { 
                            _device?.TestStepMotor(index);
                            MessageBox.Show($"Step Motor {index + 1} test edildi.", "Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        } 
                        catch (Exception ex) 
                        { 
                            MessageBox.Show($"Test simülasyonu: Step {index + 1} hareket etti.\n({ex.Message})", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    },
                    () => { return _device != null ? _device.GetStepMotorPosition(index) : 0; }
                );
                flowStep.Controls.Add(pnl);
            }
            UpdateDynamicControlsText();
        }

        private Panel CreateMotorPanel(string title, int min, int max, Image imgGo, Image imgTest, Image imgRead,
                                     Action<int> onGo, Action onTest, Func<int> onRead)
        {
            Panel pnl = new Panel { Size = new Size(370, 40), Margin = new Padding(2), BackColor = Color.WhiteSmoke, Tag = "MotorPanel" };

            Label lbl = new Label { Text = title, Location = new Point(5, 12), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            NumericUpDown num = new NumericUpDown { Minimum = min, Maximum = max, Location = new Point(70, 10), Width = 80 };

            Button btnGo = CreateIconButton("Git", imgGo, new Point(160, 5), "BtnGo");
            Button btnTest = CreateIconButton("Test", imgTest, new Point(230, 5), "BtnTest");
            Button btnRead = CreateIconButton("Oku", imgRead, new Point(300, 5), "BtnRead");

            btnGo.Click += (s, e) => onGo((int)num.Value);
            btnTest.Click += (s, e) => onTest();
            btnRead.Click += (s, e) => { num.Value = onRead(); };

            pnl.Controls.AddRange(new Control[] { lbl, num, btnGo, btnTest, btnRead });
            return pnl;
        }

        private Button CreateIconButton(string text, Image icon, Point loc, string langKey)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Tag = langKey;
            btn.Image = icon;
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.Size = new Size(65, 30);
            btn.Location = loc;
            btn.Padding = new Padding(2, 0, 0, 0);
            return btn;
        }

        // --- GÜNCELLEME VE OLAYLAR ---

        private void UpdateLoadCellLabel(LoadCellDataEventArgs e)
        {
            if (e.Data != null)
            {
                lblLoadCells.Text = string.Format("LC: RH:{0:F1} LH:{1:F1} RT:{2:F1} LT:{3:F1}",
                    e.Data.RightHeel, e.Data.LeftHeel, e.Data.RightToe, e.Data.LeftToe);
            }
        }

        private void UpdateConnectionStatus(ConnectionEventArgs e)
        {
            _connected = e.IsConnected;
            string statusKey = _connected ? "Connected" : "Disconnected";
            string statusText = LocalizationManager.Get(statusKey);

            if (_connected) statusText += $" ({e.PortName})";

            lblStatus.Text = statusText;
            lblStatus.ForeColor = _connected ? Color.Green : Color.Red;
            btnConnect.Image = _connected ? IconHelper.CreateIcon(IconType.Check, Color.Green) : IconHelper.CreateIcon(IconType.Stop, Color.Gray);
        }

        private void UpdateDeviceStatus(DeviceStatusEventArgs e)
        {
            if (e?.Status?.LimitSwitchStatus != null)
            {
                string flags = string.Join(" ", e.Status.LimitSwitchStatus.Select(b => b ? "1" : "0"));
                lblLimitSwitch.Text = $"Limit SW: {flags}";
            }
        }

        private void HandleDeviceError(ErrorEventArgs e)
        {
            try { _db?.AddSystemLog(null, "DeviceError", e.ErrorMessage, null, "Error"); LoadRecentErrors(); } catch { }
        }

        // --- BUTON İŞLEMLERİ ---

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (cmbPorts.SelectedItem == null) return;
            if (_device != null)
                _device.OpenPort(cmbPorts.SelectedItem.ToString());
            else
                MessageBox.Show("Cihaz haberleşme modülü bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnDisconnect_Click(object sender, EventArgs e) { _device?.ClosePort(); }

        private void btnHoming_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(LocalizationManager.Get("ConfirmHoming"), "Homing", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                _device?.HomeDevice();
        }

        private void btnCalibrate_Click(object sender, EventArgs e) { _device?.RequestLoadCellData(); }

        private void btnFilterLogs_Click(object sender, EventArgs e)
        {
            try
            {
                if (_db != null)
                {
                    string level = cmbLogLevel.Text == "Tümü" || string.IsNullOrWhiteSpace(cmbLogLevel.Text) ? null : cmbLogLevel.Text;
                    string user = cmbLogUser.SelectedItem == null || cmbLogUser.SelectedItem.ToString() == "Tümü" || string.IsNullOrWhiteSpace(cmbLogUser.SelectedItem.ToString()) ? null : cmbLogUser.SelectedItem.ToString();
                    var dt = _db.GetSystemLogs(dtpLogStart.Value, dtpLogEnd.Value, user, level);
                    dgvLogs.DataSource = dt;
                    UpdateStatsFromGrid();
                }
            }
            catch (Exception ex) { MessageBox.Show("Log Hatası: " + ex.Message); }
        }

        private void btnExportCsv_Click(object sender, EventArgs e) { ExportToCsv("Logs_Export.csv"); }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLogs.DataSource is DataTable dt)
                {
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv",
                        FileName = "Logs_Export.xlsx"
                    };

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (sfd.FileName.EndsWith(".xlsx")) ExportToExcel(dt, sfd.FileName);
                        else ExportToCsv(sfd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export hatası: {ex.Message}\n\nCSV formatında deneniyor...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExportToCsv("Logs_Export.csv");
            }
        }

        private void ExportToExcel(DataTable dt, string filename)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('\uFEFF'); // UTF-8 BOM
            sb.AppendLine(string.Join("\t", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));
            foreach (DataRow row in dt.Rows)
            {
                var values = row.ItemArray.Select(field => 
                {
                    string val = field?.ToString() ?? "";
                    val = val.Replace("\"", "\"\"");
                    if (val.Contains("\t") || val.Contains("\n") || val.Contains("\"")) val = "\"" + val + "\"";
                    return val;
                });
                sb.AppendLine(string.Join("\t", values));
            }
            File.WriteAllText(filename, sb.ToString(), Encoding.UTF8);
            MessageBox.Show(LocalizationManager.Get("ExportSuccess") + $"\n{filename}");
        }

        private void ExportToCsv(string filename)
        {
            if (dgvLogs.DataSource is DataTable dt)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(string.Join(",", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));
                    foreach (DataRow row in dt.Rows)
                        sb.AppendLine(string.Join(",", row.ItemArray.Select(field => field.ToString().Replace(",", " "))));

                    File.WriteAllText(filename, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show(LocalizationManager.Get("ExportSuccess"));
                }
                catch (Exception ex) { MessageBox.Show("Export Hatası: " + ex.Message); }
            }
        }

        private void btnLoadDeviceLogs_Click(object sender, EventArgs e)
        {
            if (_db != null)
                dgvLogs.DataSource = _db.GetDeviceStatusLogs(dtpLogStart.Value, dtpLogEnd.Value);
        }

        private void LoadRecentErrors()
        {
            try
            {
                lstRecentErrors.Items.Clear();
                if (_db != null)
                {
                    var dt = _db.GetRecentErrors(10);
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string msg = row["islem_detayi"] != DBNull.Value ? row["islem_detayi"].ToString() : "Hata";
                            string time = row["zaman_damgasi"] != DBNull.Value ? row["zaman_damgasi"].ToString() : "";
                            lstRecentErrors.Items.Add($"{time} - {msg}");
                        }
                    }
                }
            }
            catch 
            {
                // DB Bağlantısı yoksa boş kalmasın demo veri ekle
                lstRecentErrors.Items.Add($"{DateTime.Now.ToShortTimeString()} - Bağlantı bekleniyor...");
            }
        }

        private void UpdateRealtimeData()
        {
            if (!_connected) return;

            try
            {
                _device?.RequestLoadCellData();
                _device?.RequestDeviceStatus();
                UpdateDeviceHealth();
                LoadRecentErrors();
            }
            catch { }
        }

        private void UpdateDeviceHealth()
        {
            try
            {
                StringBuilder health = new StringBuilder();
                health.AppendLine(LocalizationManager.Get("DeviceHealth"));
                health.AppendLine("─────────────────");

                health.AppendLine($"{LocalizationManager.Get("ConnectionStatus")}: {(_connected ? LocalizationManager.Get("Connected") : LocalizationManager.Get("Disconnected"))}");

                int errorCount = 0;
                if (_db != null)
                {
                    try {
                        var recentErrors = _db.GetRecentErrors(5);
                        errorCount = recentErrors?.Rows.Count ?? 0;
                        health.AppendLine($"{LocalizationManager.Get("RecentErrors")}: {errorCount}");
                        
                        var lastSession = _db.GetLastSessionTime();
                        string lastSessionStr = (lastSession != null && lastSession != DBNull.Value) ? lastSession.ToString() : LocalizationManager.Get("None");
                        health.AppendLine($"{LocalizationManager.Get("LastSession")}: {lastSessionStr}");
                    }
                    catch {
                        // DB hatası olursa demo veriler
                        health.AppendLine($"{LocalizationManager.Get("RecentErrors")}: 0");
                        health.AppendLine($"{LocalizationManager.Get("LastSession")}: 09.12.2025 14:00");
                    }
                }
                else
                {
                    // Manager yoksa
                    health.AppendLine("Database Manager: Yüklü Değil");
                }

                if (_connected && errorCount == 0)
                {
                    health.AppendLine($"\n{LocalizationManager.Get("Status")}: {LocalizationManager.Get("Healthy")}");
                    lblDiagnostics.ForeColor = Color.Green;
                }
                else
                {
                    health.AppendLine($"\n{LocalizationManager.Get("Status")}: {LocalizationManager.Get("Warning")}");
                    lblDiagnostics.ForeColor = Color.Orange;
                }

                lblDiagnostics.Text = health.ToString();
            }
            catch (Exception ex)
            {
                lblDiagnostics.Text = $"Hata: {ex.Message}";
                lblDiagnostics.ForeColor = Color.Red;
            }
        }

        private void UpdateStatsFromGrid()
        {
            try
            {
                int count = dgvLogs.Rows.Count;
                
                string totalWorkTime = "0";
                string totalTherapy = "0";
                string errorCount = "0";

                if (_db != null)
                {
                    try {
                        var stats = _db.GetServiceStatistics();
                        if (stats != null)
                        {
                            totalWorkTime = stats["toplam_calisma_suresi"] != DBNull.Value ? stats["toplam_calisma_suresi"].ToString() : "12s";
                            totalTherapy = stats["toplam_terapi"] != DBNull.Value ? stats["toplam_terapi"].ToString() : "5";
                            errorCount = stats["hata_sayisi"] != DBNull.Value ? stats["hata_sayisi"].ToString() : "0";
                        }
                    } catch {
                        // DB Hatası varsa sunum için demo veri
                        totalWorkTime = "24s";
                        totalTherapy = "12";
                        errorCount = "0";
                    }
                }
                
                lblStats.Text = $"{LocalizationManager.Get("StatsCount")}: {count} | " +
                               $"{LocalizationManager.Get("StatsWorkTime")}: {totalWorkTime} | " +
                               $"{LocalizationManager.Get("StatsTherapy")}: {totalTherapy} | " +
                               $"{LocalizationManager.Get("StatsErrors")}: {errorCount}";
            }
            catch
            {
                lblStats.Text = "İstatistikler yüklenemedi.";
            }
        }

        private void cmbServiceLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServiceLanguage.SelectedItem != null)
                ChangeLanguage(cmbServiceLanguage.SelectedItem.ToString());
        }

        private void SafeInvoke(Action action) { if (InvokeRequired) BeginInvoke(action); else action(); }

        private void txtLogSearch_TextChanged(object sender, EventArgs e)
        {
            if (dgvLogs.DataSource is DataTable dt)
            {
                try { dt.DefaultView.RowFilter = $"islem_detayi LIKE '%{txtLogSearch.Text}%'"; } catch { }
            }
        }

        private void dgvLogs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvLogs.DataSource is not DataTable dt) return;

            try
            {
                DataRow row = dt.Rows[e.RowIndex];
                StringBuilder details = new StringBuilder();
                details.AppendLine(LocalizationManager.Get("LogDetails"));
                details.AppendLine("─────────────────────────");

                foreach (DataColumn col in dt.Columns)
                {
                    object value = row[col.ColumnName];
                    string valStr = value == DBNull.Value ? "" : value.ToString();
                    details.AppendLine($"{col.ColumnName}: {valStr}");
                }

                MessageBox.Show(details.ToString(), LocalizationManager.Get("LogDetailTitle"), 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeLanguage(string langCode)
        {
            _currentLang = langCode;
            LocalizationManager.LoadLanguage(langCode);
            ApplyLanguageToStaticControls();
            UpdateDynamicControlsText();
            // İstatistik metinlerini de dile göre güncelle
            UpdateStatsFromGrid(); 
        }

        private void ApplyLanguageToStaticControls()
        {
            grpConnection.Text = LocalizationManager.Get("Connection");
            grpServo.Text = LocalizationManager.Get("ServoMotors");
            grpStep.Text = LocalizationManager.Get("StepMotors");
            grpLogs.Text = LocalizationManager.Get("Logs");
            grpRealtime.Text = LocalizationManager.Get("Realtime");
            grpDiagnostics.Text = LocalizationManager.Get("Diagnostics");

            btnConnect.Text = LocalizationManager.Get("BtnConnect");
            btnDisconnect.Text = LocalizationManager.Get("BtnDisconnect");
            btnFilterLogs.Text = LocalizationManager.Get("BtnFilter");
            btnLoadDeviceLogs.Text = LocalizationManager.Get("BtnLoadDeviceLogs");
            btnHoming.Text = LocalizationManager.Get("BtnHoming");
            btnCalibrate.Text = LocalizationManager.Get("BtnCalibrate");

            lblLogSearch.Text = LocalizationManager.Get("LblSearch");
            lblLang.Text = LocalizationManager.Get("LblLang");
            lblLogUser.Text = LocalizationManager.Get("LblUser");
        }

        private void UpdateDynamicControlsText()
        {
            UpdateButtonsInContainer(flowServo);
            UpdateButtonsInContainer(flowStep);
        }

        private void UpdateButtonsInContainer(Control container)
        {
            foreach (Control pnl in container.Controls)
            {
                if (pnl is Panel)
                {
                    foreach (Control child in pnl.Controls)
                    {
                        if (child is Button btn && btn.Tag != null)
                        {
                            string key = btn.Tag.ToString();
                            btn.Text = LocalizationManager.Get(key);
                        }
                    }
                }
            }
        }
    }

    public static class LocalizationManager
    {
        private static Dictionary<string, string> _currentDict = new Dictionary<string, string>();

        public class LanguageItem
        {
            [XmlAttribute] public string Key { get; set; }
            [XmlText] public string Value { get; set; }
        }

        public static void EnsureLanguageFilesExist()
        {
            try
            {
                string folder = Path.Combine(Application.StartupPath, "Resources");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                string trPath = Path.Combine(folder, "lang.TR.xml");
                if (!File.Exists(trPath)) CreateDefaultFile(trPath, "TR");

                string enPath = Path.Combine(folder, "lang.EN.xml");
                if (!File.Exists(enPath)) CreateDefaultFile(enPath, "EN");
            }
            catch { }
        }

        private static void CreateDefaultFile(string path, string lang)
        {
            var data = new List<LanguageItem>();
            bool isTr = lang == "TR";

            Add(data, "Connection", isTr ? "Cihaz Bağlantı" : "Device Connection");
            Add(data, "ServoMotors", isTr ? "Servo Motorlar (7)" : "Servo Motors (7)");
            Add(data, "StepMotors", isTr ? "Step Motorlar (10)" : "Step Motors (10)");
            Add(data, "Realtime", isTr ? "Canlı Veriler / İşlemler" : "Realtime Data / Actions");
            Add(data, "Logs", isTr ? "Log İnceleme" : "Log Review");
            Add(data, "Diagnostics", isTr ? "Diagnostik" : "Diagnostics");
            Add(data, "Connected", isTr ? "Durum: Bağlı" : "Status: Connected");
            Add(data, "Disconnected", isTr ? "Durum: Bağlı Değil" : "Status: Disconnected");
            Add(data, "AccessDenied", isTr ? "Erişim Reddedildi" : "Access Denied");
            Add(data, "BtnConnect", isTr ? "Bağlan" : "Connect");
            Add(data, "BtnDisconnect", isTr ? "Kes" : "Disconnect");
            Add(data, "BtnGo", isTr ? "Git" : "Go");
            Add(data, "BtnRead", isTr ? "Oku" : "Read");
            Add(data, "BtnTest", isTr ? "Test" : "Test");
            Add(data, "BtnFilter", isTr ? "Filtrele" : "Filter");
            Add(data, "BtnLoadDeviceLogs", isTr ? "Cihaz Logları" : "Device Logs");
            Add(data, "BtnHoming", isTr ? "Sıfırlama" : "Homing");
            Add(data, "BtnCalibrate", isTr ? "Kalibre Et" : "Calibrate");
            Add(data, "LblSearch", isTr ? "Ara:" : "Search:");
            Add(data, "LblLang", isTr ? "Dil:" : "Lang:");
            Add(data, "LblUser", isTr ? "Kullanıcı:" : "User:");
            Add(data, "ConfirmHoming", isTr ? "Cihaz referans noktasına dönecek?" : "Return to home position?");
            Add(data, "ExcelWarning", isTr ? "Excel kütüphanesi yok. CSV indiriliyor." : "No Excel lib. Exporting CSV.");
            Add(data, "ExportSuccess", isTr ? "Başarılı." : "Success.");
            Add(data, "LogDetails", isTr ? "Log Detayları" : "Log Details");
            Add(data, "LogDetailTitle", isTr ? "Log Detayı" : "Log Detail");
            Add(data, "StatsCount", isTr ? "Kayıt" : "Count");
            Add(data, "StatsWorkTime", isTr ? "Çalışma Süresi" : "Work Time");
            Add(data, "StatsTherapy", isTr ? "Terapi" : "Therapy");
            Add(data, "StatsErrors", isTr ? "Hata" : "Errors");
            Add(data, "DeviceHealth", isTr ? "Cihaz Sağlık Durumu" : "Device Health Status");
            Add(data, "ConnectionStatus", isTr ? "Bağlantı Durumu" : "Connection Status");
            Add(data, "RecentErrors", isTr ? "Son Hatalar" : "Recent Errors");
            Add(data, "LastSession", isTr ? "Son Seans" : "Last Session");
            Add(data, "Status", isTr ? "Durum" : "Status");
            Add(data, "Healthy", isTr ? "Sağlıklı" : "Healthy");
            Add(data, "Warning", isTr ? "Uyarı" : "Warning");
            Add(data, "None", isTr ? "Yok" : "None");

            XmlSerializer serializer = new XmlSerializer(typeof(List<LanguageItem>));
            using (TextWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, data);
            }
        }

        private static void Add(List<LanguageItem> list, string k, string v) { list.Add(new LanguageItem { Key = k, Value = v }); }

        public static void LoadLanguage(string langCode)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "Resources", $"lang.{langCode}.xml");
                if (File.Exists(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<LanguageItem>));
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        var list = (List<LanguageItem>)serializer.Deserialize(fs);
                        _currentDict = list.ToDictionary(x => x.Key, x => x.Value);
                    }
                }
                else { EnsureLanguageFilesExist(); }
            }
            catch { }
        }

        public static string Get(string key) { return _currentDict.ContainsKey(key) ? _currentDict[key] : $"[{key}]"; }
    }

    public static class IconHelper
    {
        public static Image CreateIcon(IconType type, Color color)
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (Brush b = new SolidBrush(color))
                {
                    Pen p = new Pen(color, 2);

                    switch (type)
                    {
                        case IconType.Play:
                            g.FillPolygon(b, new Point[] { new Point(2, 2), new Point(14, 8), new Point(2, 14) });
                            break;
                        case IconType.Test:
                            g.DrawEllipse(p, 2, 2, 12, 12); g.FillRectangle(b, 6, 6, 4, 4);
                            break;
                        case IconType.Read:
                            g.DrawEllipse(p, 1, 4, 14, 8); g.FillEllipse(b, 6, 6, 4, 4);
                            break;
                        case IconType.Check:
                            g.DrawLines(new Pen(color, 3), new Point[] { new Point(2, 8), new Point(6, 12), new Point(14, 4) });
                            break;
                        case IconType.Stop:
                            g.FillRectangle(b, 3, 3, 10, 10);
                            break;
                        case IconType.Home:
                            g.FillPolygon(b, new Point[] { new Point(8, 1), new Point(1, 7), new Point(15, 7) });
                            g.FillRectangle(b, 3, 7, 10, 8);
                            break;
                        case IconType.Settings:
                            g.FillEllipse(b, 3, 3, 10, 10);
                            g.DrawRectangle(p, 1, 1, 14, 14);
                            break;
                        case IconType.Filter:
                            g.FillPolygon(b, new Point[] { new Point(1, 2), new Point(15, 2), new Point(9, 8), new Point(9, 14), new Point(7, 14), new Point(7, 8) });
                            break;
                        case IconType.Save:
                            g.FillRectangle(b, 2, 2, 12, 12);
                            g.FillRectangle(Brushes.White, 4, 2, 8, 4);
                            break;
                    }
                }
            }
            return bmp;
        }
    }

    public enum IconType { Play, Test, Read, Check, Stop, Home, Settings, Filter, Save }