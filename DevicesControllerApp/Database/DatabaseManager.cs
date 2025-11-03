using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevicesControllerApp.Database
{

    internal class DatabaseManager
    {

        private readonly string _connectionString;

        public DatabaseManager()
        {
            // PostgreSQL bağlantı bilgileri
            _connectionString = "Host=localhost;Port=5432;Database=raporlama_db;Username=postgres;Password=";
        }

        public bool TestConnection()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    return conn.State == ConnectionState.Open;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bağlantı hatası: {ex.Message}");
                return false;
            }
        }

        public DataTable ExecuteQuery(string query, params NpgsqlParameter[] parameters)
        {
            var table = new DataTable();

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sorgu hatası: {ex.Message}");
            }

            return table;
        }

        public int ExecuteNonQuery(string query, params NpgsqlParameter[] parameters)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veritabanı işlem hatası: {ex.Message}");
                return -1;
            }
        }

        // ====================BAŞLANGIÇ==============================
        // RAPORLAMA MODÜLÜ EKİBİ TARAFINDAN GELİŞTİRİLEN KISIMLAR
        // ---------------------------------------------------------------
        // Grup: 3 - RAPORLAMA
        // Açıklama: Bu bölümde raporlama modülü ile ilgili veritabanı
        // erişim fonksiyonları (Hasta Raporları, Terapi Geçmişi, 
        // Operatör Performansı vb.) geliştirilecektir.
        // ================================================================

        public DataTable GetAllPatients()
        {
            DataTable dt = new DataTable();
            string query = "SELECT id, ad || ' ' || soyad AS ad_soyad FROM hastalar WHERE aktif = TRUE ORDER BY ad;";

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "GetAllPatients");
                MessageBox.Show("Hasta verileri çekilirken hata oluştu: " + ex.Message,
                    "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        /// <summary>
        /// Seçilen hastanın detaylı bilgilerini getirir
        /// </summary>
        /// <param name="patientId">Hasta ID</param>
        /// <returns>Hasta detaylarını içeren DataRow</returns>
        public DataRow GetPatientDetailById(int patientId)
        {
            string query = @"
                SELECT 
                    id,
                    tc_pasaport_no,
                    ad,
                    soyad,
                    dogum_tarihi,
                    cinsiyet,
                    adres,
                    telefon,
                    eposta,
                    tani,
                    boy,
                    kilo,
                    ayak_numarasi,
                    kalca_diz_mesafesi,
                    diz_topuk_mesafesi,
                    aktif,
                    kayit_tarihi
                FROM hastalar
                WHERE id = @patientId;
            ";

            try
            {
                var param = new NpgsqlParameter("@patientId", patientId);
                DataTable dt = ExecuteQuery(query, param);

                if (dt.Rows.Count > 0)
                    return dt.Rows[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                LogError(ex, "GetPatientDetailById");
                MessageBox.Show("Hasta detayları alınırken hata oluştu: " + ex.Message,
                    "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Tüm hastaların detaylı bilgilerini getirir
        /// </summary>
        /// <summary>
        /// Tüm hastaların detaylı bilgilerini getirir (aktif/pasif filtresi ile)
        /// </summary>
        /// <param name="activeOnly">True ise sadece aktif hastalar getirilir</param>
        public DataTable GetAllPatientsDetailed(bool activeOnly = false)
        {
            DataTable dt = new DataTable();

            string whereClause = activeOnly ? "WHERE aktif = TRUE" : "";

            string query = $@"
                SELECT 
                    id AS ""ID"",
                    tc_pasaport_no AS ""TC/Pasaport No"",
                    ad AS ""Ad"",
                    soyad AS ""Soyad"",
                    dogum_tarihi AS ""Doğum Tarihi"",
                    cinsiyet AS ""Cinsiyet"",
                    adres AS ""Adres"",
                    telefon AS ""Telefon"",
                    eposta AS ""E-Posta"",
                    tani AS ""Tanı"",
                    boy AS ""Boy (cm)"",
                    kilo AS ""Kilo (kg)"",
                    ayak_numarasi AS ""Ayak No"",
                    kalca_diz_mesafesi AS ""Kalça-Diz (cm)"",
                    diz_topuk_mesafesi AS ""Diz-Topuk (cm)"",
                    CASE 
                        WHEN aktif = TRUE THEN 'Aktif'
                        ELSE 'Pasif'
                    END AS ""Durum"",
                    kayit_tarihi AS ""Kayıt Tarihi""
                FROM hastalar
                {whereClause}
                ORDER BY kayit_tarihi DESC;
            ";

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "GetAllPatientsDetailed");
                MessageBox.Show("Hasta detayları çekilirken hata oluştu: " + ex.Message,
                    "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        /// <summary>
        /// Seçilen hastanın tüm terapi geçmişini getirir
        /// </summary>
        /// <param name="patientId">Hasta ID</param>
        /// <returns>Terapi geçmişi DataTable</returns>
        public DataTable GetPatientTherapyHistoryDetailed(int patientId)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    t.id,
                    t.hasta_id,
                    t.operator_id,
                    t.baslangic_zamani,
                    t.bitis_zamani,
                    t.terapi_suresi,
                    t.terapi_hizi,
                    t.""azaltılan_agirlik"",
                    t.destek_bari_yuksekligi,
                    t.ayak_numarasi_ayari,
                    t.terapi_durumu,
                    t.notlar,
                    h.ad || ' ' || h.soyad AS hasta_adi,
                    k.ad || ' ' || k.soyad AS operator_adi
                FROM terapiler t
                LEFT JOIN hastalar h ON t.hasta_id = h.id
                LEFT JOIN kullanicilar k ON t.operator_id = k.id
                WHERE t.hasta_id = @patientId
                ORDER BY t.baslangic_zamani DESC;
            ";

            try
            {
                var param = new NpgsqlParameter("@patientId", patientId);
                dt = ExecuteQuery(query, param);
            }
            catch (Exception ex)
            {
                LogError(ex, "GetPatientTherapyHistoryDetailed");
                MessageBox.Show("Terapi geçmişi alınırken hata oluştu: " + ex.Message,
                    "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        /// <summary>
        /// Tarih aralığına ve aktiflik durumuna göre hastaları getirir
        /// </summary>
        /// <param name="startDate">Başlangıç tarihi (kayıt_tarihi)</param>
        /// <param name="endDate">Bitiş tarihi (kayıt_tarihi)</param>
        /// <param name="activeOnly">Sadece aktif hastalar mı?</param>
        /// <returns>Filtrelenmiş hasta listesi</returns>
        public DataTable GetPatientsByDateRange(DateTime startDate, DateTime endDate, bool activeOnly = false)
        {
            DataTable dt = new DataTable();

            string activeFilter = activeOnly ? "AND aktif = TRUE" : "";

            string query = $@"
                SELECT 
                    id AS ""ID"",
                    tc_pasaport_no AS ""TC/Pasaport No"",
                    ad AS ""Ad"",
                    soyad AS ""Soyad"",
                    dogum_tarihi AS ""Doğum Tarihi"",
                    cinsiyet AS ""Cinsiyet"",
                    adres AS ""Adres"",
                    telefon AS ""Telefon"",
                    eposta AS ""E-Posta"",
                    tani AS ""Tanı"",
                    boy AS ""Boy (cm)"",
                    kilo AS ""Kilo (kg)"",
                    ayak_numarasi AS ""Ayak No"",
                    kalca_diz_mesafesi AS ""Kalça-Diz (cm)"",
                    diz_topuk_mesafesi AS ""Diz-Topuk (cm)"",
                    CASE 
                        WHEN aktif = TRUE THEN 'Aktif'
                        ELSE 'Pasif'
                    END AS ""Durum"",
                    kayit_tarihi AS ""Kayıt Tarihi""
                FROM hastalar
                WHERE kayit_tarihi >= @startDate 
                  AND kayit_tarihi <= @endDate
                  {activeFilter}
                ORDER BY kayit_tarihi DESC;
            ";

            try
            {
                var parameters = new NpgsqlParameter[]
                {
            new NpgsqlParameter("@startDate", startDate),
            new NpgsqlParameter("@endDate", endDate)
                };

                using (var conn = new NpgsqlConnection(_connectionString))
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "GetPatientsByDateRange");
                MessageBox.Show("Hasta verileri çekilirken hata oluştu: " + ex.Message,
                    "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        /// <summary>
        /// Seçilen hastanın tarih aralığına göre terapi geçmişini getirir
        /// </summary>
        /// <param name="patientId">Hasta ID</param>
        /// <param name="startDate">Başlangıç tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        /// <returns>Terapi geçmişi DataTable</returns>
        public DataTable GetPatientTherapyHistoryByDateRange(int patientId, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    t.id,
                    t.hasta_id,
                    t.operator_id,
                    t.baslangic_zamani,
                    t.bitis_zamani,
                    t.terapi_suresi,
                    t.terapi_hizi,
                    t.""azaltılan_agirlik"" AS azaltılan_agirlik,
                    t.destek_bari_yuksekligi,
                    t.ayak_numarasi_ayari,
                    t.terapi_durumu,
                    t.notlar,
                    h.ad || ' ' || h.soyad AS hasta_adi,
                    k.ad || ' ' || k.soyad AS operator_adi
                FROM terapiler t
                LEFT JOIN hastalar h ON t.hasta_id = h.id
                LEFT JOIN kullanicilar k ON t.operator_id = k.id
                WHERE t.hasta_id = @patientId
                  AND t.baslangic_zamani >= @startDate
                  AND t.baslangic_zamani <= @endDate
                ORDER BY t.baslangic_zamani DESC;
            ";

            try
            {
                var parameters = new NpgsqlParameter[]
                {
            new NpgsqlParameter("@patientId", patientId),
            new NpgsqlParameter("@startDate", startDate),
            new NpgsqlParameter("@endDate", endDate)
                };

                dt = ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                LogError(ex, "GetPatientTherapyHistoryByDateRange");
                MessageBox.Show("Terapi geçmişi alınırken hata oluştu: " + ex.Message,
                    "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        /// <summary>
        /// Hastanın ilk ve son terapi tarihlerini getirir
        /// </summary>
        /// <param name="patientId">Hasta ID</param>
        /// <returns>İlk ve son terapi tarihleri</returns>
        public (DateTime? ilkTerapi, DateTime? sonTerapi, int toplamTerapi) GetPatientTherapyDateRange(int patientId)
        {
            string query = @"
        SELECT 
            MIN(baslangic_zamani) AS ilk_terapi,
            MAX(baslangic_zamani) AS son_terapi,
            COUNT(*) AS toplam_terapi
        FROM terapiler
        WHERE hasta_id = @patientId;
    ";

            try
            {
                var param = new NpgsqlParameter("@patientId", patientId);
                DataTable dt = ExecuteQuery(query, param);

                if (dt.Rows.Count > 0 && dt.Rows[0]["ilk_terapi"] != DBNull.Value)
                {
                    DateTime ilk = Convert.ToDateTime(dt.Rows[0]["ilk_terapi"]);
                    DateTime son = Convert.ToDateTime(dt.Rows[0]["son_terapi"]);
                    int toplam = Convert.ToInt32(dt.Rows[0]["toplam_terapi"]);

                    return (ilk, son, toplam);
                }

                return (null, null, 0);
            }
            catch (Exception ex)
            {
                LogError(ex, "GetPatientTherapyDateRange");
                return (null, null, 0);
            }
        }

        // =======================SON==================================
        // RAPORLAMA MODÜLÜ EKİBİ TARAFINDAN GELİŞTİRİLEN KISIMLAR
        // =======================SON==================================

        public bool OpenConnection()
        {
            string connectionString = "Server=...;port=...;Database=...;user Id=...;password=..."; 
            //burayı kendi database'nize uygun olarak doldurunuz

            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            
            return false;
        }

        public bool CloseConnection()
        {
            //connection.Close();
            return false;
        }

        public bool AddPatient(string tcNo, string firstName, string lastName, DateTime birthDate,
            string gender, string address, string phone, string email, string diagnosis,
            int height, int weight, int shoeSize, int hipKneeDistance, int kneeHeelDistance)
        {
            return false;
        }

        public bool UpdatePatient(int patientId, string tcNo, string firstName, string lastName,
            DateTime birthDate, string gender, string address, string phone, string email,
            string diagnosis, int height, int weight, int shoeSize, int hipKneeDistance,
            int kneeHeelDistance)
        {
            return false;
        }

        public bool DeletePatient(int patientId)
        {
            return false;
        }

        public DataTable SearchPatient(string searchTerm)
        {
            return null;
        }



        public DataRow GetPatientDetails(int patientId)
        {
            return null;
        }

        public DataRow GetPatientByTcNo(string tcNo)
        {
            return null;
        }

        public bool ValidateUserLogin(string username, string password)
        {
            return false;
        }

        public DataRow GetUserByUsername(string username)
        {
            return null;
        }

        public bool AddUser(string username, string password, string tcNo, string firstName,
            string lastName, string role, string email, string phone)
        {
            return false;
        }

        public bool UpdateUser(int userId, string username, string tcNo, string firstName,
            string lastName, string role, string email, string phone)
        {
            return false;
        }

        public bool DeleteUser(int userId)
        {
            return false;
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            return false;
        }

        public string GeneratePasswordResetToken(string email)
        {
            return null;
        }

        public bool ResetPasswordWithToken(string token, string newPassword)
        {
            return false;
        }

        public DataTable GetAllUsers()
        {
            return null;
        }

        public bool UpdateLastLogin(int userId)
        {
            return false;
        }

        public int StartTherapy(int patientId, int operatorId, double speed, double weightReduction,
            double supportBarHeight, int shoeSize)
        {
            return 0;
        }

        public bool EndTherapy(int therapyId, string notes)
        {
            return false;
        }

        public bool UpdateTherapy(int therapyId, double speed, double weightReduction,
            double supportBarHeight)
        {
            return false;
        }

        public DataTable SearchTherapy(DateTime? startDate, DateTime? endDate, int? patientId,
            int? operatorId)
        {
            return null;
        }

        public DataRow GetTherapyDetails(int therapyId)
        {
            return null;
        }

        public DataTable GetPatientTherapyHistory(int patientId)
        {
            return null;
        }

        public bool UpdateTherapyStatus(int therapyId, string status)
        {
            return false;
        }

        public bool SaveLoadCellData(int therapyId, DateTime timestamp, double rightHeel,
            double leftHeel, double rightToe, double leftToe, double weightBalance, int index)
        {
            return false;
        }

        public bool SaveLoadCellDataBulk(int therapyId, List<LoadCellData> dataList)
        {
            return false;
        }

        public DataTable GetLoadCellData(int therapyId)
        {
            return null;
        }

        public DataTable GetLoadCellDataByTimeRange(int therapyId, DateTime startTime,
            DateTime endTime)
        {
            return null;
        }

        public bool AddSystemLog(int? userId, string operationType, string operationDetail,
            string ipAddress, string errorLevel)
        {
            return false;
        }

        public bool AddDeviceStatusLog(string servoMotorStatus, string stepMotorStatus,
            string limitSwitchStatus, string errorCodes)
        {
            return false;
        }

        public DataTable GetSystemLogs(DateTime? startDate, DateTime? endDate, int? userId,
            string errorLevel)
        {
            return null;
        }

        public DataTable GetDeviceStatusLogs(DateTime? startDate, DateTime? endDate)
        {
            return null;
        }

        public DataTable GetRecentErrors(int count)
        {
            return null;
        }

        public string GetSetting(string settingKey)
        {
            return null;
        }

        public bool SaveSetting(string settingKey, string settingValue, string description,
            int updatedByUserId)
        {
            return false;
        }

        public DataTable GetAllSettings()
        {
            return null;
        }

        public bool DeleteSetting(string settingKey)
        {
            return false;
        }

        public int GetTherapyCountByDateRange(DateTime startDate, DateTime endDate)
        {
            return 0;
        }

        public DataTable GetTherapyStatisticsByDateRange(DateTime startDate, DateTime endDate)
        {
            return null;
        }

        public DataTable GetPatientTherapyReport(int patientId)
        {
            return null;
        }

        public DataTable GetOperatorPerformanceReport(DateTime startDate, DateTime endDate)
        {
            return null;
        }

        public DataTable GetOperatorTherapyCount(int operatorId, DateTime startDate,
            DateTime endDate)
        {
            return null;
        }

        public DataTable GetAverageTherapyDuration(DateTime startDate, DateTime endDate)
        {
            return null;
        }

        public DataTable GetPatientProgressReport(int patientId)
        {
            return null;
        }

        public DataTable GetMostActivePatients(int topCount, DateTime startDate, DateTime endDate)
        {
            return null;
        }

        public DataTable GetDeviceUsageStatistics(DateTime startDate, DateTime endDate)
        {
            return null;
        }

        private string HashPassword(string password)
        {
            return null;
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return false;
        }

        private void LogError(Exception ex, string methodName)
        {
            // Hata loglama
        }
    }
    // LoadCell verisi için model class
    public class LoadCellData
    {
        public DateTime Timestamp { get; set; }
        public double RightHeel { get; set; }
        public double LeftHeel { get; set; }
        public double RightToe { get; set; }
        public double LeftToe { get; set; }
        public double WeightBalance { get; set; }
        public int Index { get; set; }
    }
}


