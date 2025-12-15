using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms; // MessageBox için gerekli
using Npgsql; // Npgsql kütüphanesi
using System.Linq; // LINQ için eklendi

namespace DevicesControllerApp.Database
{
    internal class DatabaseManager
    {
        private static DatabaseManager instance = null;

        internal static DatabaseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseManager();
                }
                return instance;
            }
        }

        private string connectionString;
        private NpgsqlConnection conn;

        private DatabaseManager()
        {
            // ŞİFRENİZİ BURAYA YAZIN. Database ismini backup dosyasına göre 'database' olarak güncelledim.
            // Eğer veritabanı adınız 'lokomat' ise onu değiştirmeyin.
            connectionString = "Server=localhost;Port=5432;Database=SonDB;User Id=postgres;Password=1234;";
        }

        public bool OpenConnection()
        {
            if (conn == null) conn = new NpgsqlConnection(connectionString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı Hatası: " + ex.Message);
                return false;
            }
        }

        public void CloseConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }

        // ŞEHİRLERİ GETİR
        public DataTable GetAllCitys()
        {
            DataTable dt = new DataTable();
            try
            {
                if (OpenConnection())
                {
                    // Backup dosyanızdaki tablo adı: sehirler_tablosu
                    string query = "SELECT plaka_kodu, sehir_adi FROM sehirler_tablosu ORDER BY sehir_adi";
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Şehirler getirilemedi: " + ex.Message); }
            return dt;
        }

        // HASTALARI LİSTELE (Sadece Aktif Olanlar)
        public DataTable GetAllPatients()
        {
            DataTable dt = new DataTable();
            try
            {
                if (OpenConnection())
                {
                    string query = "SELECT * FROM hasta_bilgileri WHERE aktif_pasif_durumu = 'Aktif' ORDER BY hasta_id DESC";
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, conn))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Hastalar getirilemedi: " + ex.Message); }
            return dt;
        }

        // HASTA EKLE
        public bool AddPatient(string tcNo, string firstName, string lastName, DateTime birthDate,
            string gender, string address, string phone, string email, string diagnosis,
            decimal height, decimal weight, decimal shoeSize, decimal hipKneeDistance, decimal kneeHeelDistance,
            int sehirPlaka, string yakinAd, string yakinSoyad, string yakinDerece, string yakinTel)
        {
            try
            {
                if (!OpenConnection()) return false;

                // Local helper to attempt insert with a specific gender value
                bool TryInsert(string genderValue)
                {
                    string query = @"INSERT INTO hasta_bilgileri 
                    (ad, soyad, tc, dogum_tarihi, cinsiyet, adresi, hasta_telefon_no, e_mail, hastalik_tanisi,
                     boy_cm, kilo_kg, ayak_no, kalca_diz_mesafesi, diz_topuk_mesafesi, 
                     memleketi_plaka_kodu, hasta_yakini_adi, hasta_yakini_soyadi, hasta_yakini_neyi, hasta_yakini_telefon_no, aktif_pasif_durumu)
                    VALUES 
                    (@ad, @soyad, @tc, @dogum, @cinsiyet, @adres, @tel, @email, @tani,
                     @boy, @kilo, @ayak, @kalcaDiz, @dizTopuk,
                     @plaka, @yakinAd, @yakinSoyad, @yakinDerece, @yakinTel, 'Aktif')";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ad", firstName);
                        cmd.Parameters.AddWithValue("@soyad", lastName);
                        cmd.Parameters.AddWithValue("@tc", tcNo);
                        cmd.Parameters.AddWithValue("@dogum", birthDate);
                        cmd.Parameters.AddWithValue("@cinsiyet", genderValue ?? string.Empty);
                        cmd.Parameters.AddWithValue("@adres", address);
                        cmd.Parameters.AddWithValue("@tel", phone);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@tani", diagnosis);
                        cmd.Parameters.AddWithValue("@boy", height);
                        cmd.Parameters.AddWithValue("@kilo", weight);
                        cmd.Parameters.AddWithValue("@ayak", shoeSize);
                        cmd.Parameters.AddWithValue("@kalcaDiz", hipKneeDistance);
                        cmd.Parameters.AddWithValue("@dizTopuk", kneeHeelDistance);
                        cmd.Parameters.AddWithValue("@plaka", sehirPlaka);
                        cmd.Parameters.AddWithValue("@yakinAd", yakinAd);
                        cmd.Parameters.AddWithValue("@yakinSoyad", yakinSoyad);
                        cmd.Parameters.AddWithValue("@yakinDerece", yakinDerece);
                        cmd.Parameters.AddWithValue("@yakinTel", yakinTel);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }

                // First try with provided gender
                try
                {
                    if (TryInsert(gender)) return true;
                }
                catch (Npgsql.PostgresException pex)
                {
                    // If check constraint for gender failed, we'll try mapping to an alternate value and retry
                    if (string.Equals(pex.ConstraintName, "chk_cinsiyet", StringComparison.OrdinalIgnoreCase))
                    {
                        string alt = GetAlternateGender(gender);
                        if (!string.IsNullOrEmpty(alt) && !string.Equals(alt, gender, StringComparison.Ordinal))
                        {
                            try
                            {
                                if (TryInsert(alt)) return true;
                            }
                            catch (Exception) { /* fall through to show error below */ }
                        }
                    }
                    // For any other PostgresException, show the message below
                    MessageBox.Show("Ekleme Hatası: " + pex.Message);
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme Hatası: " + ex.Message);
                return false;
            }
            return false;
        }

        // Map between UI gender representations and common DB representations
        private string GetAlternateGender(string gender)
        {
            if (string.IsNullOrWhiteSpace(gender)) return gender;
            string g = gender.Trim();
            // If form shows "Erkek / Man" or "Kadın / Woman" earlier code trimmed to part before'/'
            if (g.Contains("/")) g = g.Split('/')[0].Trim();
            string lower = g.ToLowerInvariant();

            // Try to read allowed values from DB check constraint for column 'cinsiyet' in table 'hasta_bilgileri'
            var allowed = GetCheckConstraintAllowedValues("hasta_bilgileri", "cinsiyet");
            if (allowed != null && allowed.Count > 0)
            {
                // Normalize allowed values
                var allowedLower = allowed.Select(x => x.ToLowerInvariant()).ToList();

                // If exact match to an allowed value, return that allowed value (preserve original allowed casing)
                for (int i = 0; i < allowedLower.Count; i++)
                {
                    if (allowedLower[i] == lower || lower.Contains(allowedLower[i]) || allowedLower[i].Contains(lower))
                        return allowed[i];
                }

                // Heuristic mappings: try Turkish/English/short codes
                if (lower.StartsWith("erk"))
                {
                    // prefer allowed value that starts with 'erk' or equals 'm'
                    for (int i = 0; i < allowedLower.Count; i++)
                        if (allowedLower[i].StartsWith("erk") || allowedLower[i] == "m") return allowed[i];
                }
                if (lower.StartsWith("kad") || lower.StartsWith("kadın"))
                {
                    for (int i = 0; i < allowedLower.Count; i++)
                        if (allowedLower[i].StartsWith("kad") || allowedLower[i] == "f") return allowed[i];
                }
                if (lower == "m" || lower == "male")
                {
                    for (int i = 0; i < allowedLower.Count; i++)
                        if (allowedLower[i] == "m" || allowedLower[i].StartsWith("erk") || allowedLower[i].Contains("male")) return allowed[i];
                }
                if (lower == "f" || lower == "female" || lower == "woman")
                {
                    for (int i = 0; i < allowedLower.Count; i++)
                        if (allowedLower[i] == "f" || allowedLower[i].StartsWith("kad") || allowedLower[i].Contains("female") || allowedLower[i].Contains("woman")) return allowed[i];
                }

                // As a last resort return the first allowed value
                return allowed.First();
            }

            // Fallback if we couldn't read constraint: prior heuristics
            // Common mappings: Turkish <-> short codes
            if (lower.StartsWith("erk")) // "erkek"
                return "M"; // try short code first
            if (lower.StartsWith("kad") || lower.StartsWith("kadın")) // "kadın"
                return "F";
            if (lower == "m")
                return "Erkek";
            if (lower == "f")
                return "Kadın";

            // Fallback: also support English words
            if (lower.StartsWith("man") || lower == "male") return "M";
            if (lower.StartsWith("wom") || lower == "female") return "F";

            // If nothing matched, return original
            return gender;
        }

        // Read check constraint definition(s) for given table and column and extract allowed string values
        private List<string> GetCheckConstraintAllowedValues(string tableName, string columnName)
        {
            try
            {
                if (!OpenConnection()) return null;

                string sql = @"SELECT pg_get_constraintdef(pc.oid) AS def
FROM pg_constraint pc
JOIN pg_class c ON pc.conrelid = c.oid
JOIN pg_namespace n ON c.relnamespace = n.oid
WHERE c.relname = @table AND pc.contype = 'c' AND pg_get_constraintdef(pc.oid) ILIKE '%' || @col || '%';";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@table", tableName);
                    cmd.Parameters.AddWithValue("@col", columnName);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<string>();
                        while (reader.Read())
                        {
                            var def = reader[0]?.ToString();
                            if (string.IsNullOrEmpty(def)) continue;
                            // Extract single-quoted literals from definition
                            var matches = System.Text.RegularExpressions.Regex.Matches(def, "'([^']*)'");
                            foreach (System.Text.RegularExpressions.Match m in matches)
                            {
                                string val = m.Groups[1].Value;
                                if (!results.Contains(val)) results.Add(val);
                            }
                        }
                        return results.Distinct().ToList();
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        // Public wrapper to get allowed values for a check-constrained column (e.g., cinsiyet)
        public List<string> GetAllowedValues(string tableName, string columnName)
        {
            try
            {
                return GetCheckConstraintAllowedValues(tableName, columnName);
            }
            catch
            {
                return null;
            }
        }

        // HASTA GÜNCELLE
        public bool UpdatePatientByTC(string refTcNo, string firstName, string lastName,
            string email, string address, string phone,
            decimal height, decimal weight, decimal shoeSize, decimal hipKneeDistance, decimal kneeHeelDistance)
        {
            try
            {
                if (OpenConnection())
                {
                    string query = @"UPDATE hasta_bilgileri SET 
                        ad=@ad, soyad=@soyad, e_mail=@email, adresi=@adres, hasta_telefon_no=@tel,
                        boy_cm=@boy, kilo_kg=@kilo, ayak_no=@ayak, kalca_diz_mesafesi=@kalcaDiz, diz_topuk_mesafesi=@dizTopuk,
                        guncelleme_tarihi=NOW()
                        WHERE tc=@refTc";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ad", firstName);
                        cmd.Parameters.AddWithValue("@soyad", lastName);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@adres", address);
                        cmd.Parameters.AddWithValue("@tel", phone);
                        cmd.Parameters.AddWithValue("@boy", height);
                        cmd.Parameters.AddWithValue("@kilo", weight);
                        cmd.Parameters.AddWithValue("@ayak", shoeSize);
                        cmd.Parameters.AddWithValue("@kalcaDiz", hipKneeDistance);
                        cmd.Parameters.AddWithValue("@dizTopuk", kneeHeelDistance);
                        cmd.Parameters.AddWithValue("@refTc", refTcNo);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme Hatası: " + ex.Message);
                return false;
            }
            return false;
        }

        // HASTA SİL (Pasife Çek)
        public bool DeletePatientByTC(string tcNo)
        {
            try
            {
                if (OpenConnection())
                {
                    string query = "UPDATE hasta_bilgileri SET aktif_pasif_durumu='Pasif' WHERE tc=@tc";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tc", tcNo);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme Hatası: " + ex.Message);
                return false;
            }
            return false;
        }

        internal bool AddPatient(string v1, string ad, string soyad, DateTime value, string v2, string v3, string v4, string text1, string v5, string v6, string text2, string v7, string text3, decimal boy, decimal kilo, decimal ayak, decimal kalcaDiz, decimal dizTopuk, int sehirPlaka)
        {
            // Parametre sıralaması PatientRegistration'daki çağrı ile uyumlu olacak şekilde gelmektedir.
            // Burada gelen parametreleri public AddPatient metodunun beklediği sıraya yeniden eşleyip delege ediyoruz.
            return AddPatient(
                tcNo: v1,
                firstName: ad,
                lastName: soyad,
                birthDate: value,
                gender: text1,
                address: v3,
                phone: v4,
                email: v2,
                diagnosis: text3,
                height: boy,
                weight: kilo,
                shoeSize: ayak,
                hipKneeDistance: kalcaDiz,
                kneeHeelDistance: dizTopuk,
                sehirPlaka: sehirPlaka,
                yakinAd: v5,
                yakinSoyad: v6,
                yakinDerece: text2,
                yakinTel: v7
            );
        }

        // HASTALARI ARA (TC veya ad soyad ile)
        public DataTable SearchPatients(string term)
        {
            DataTable dt = new DataTable();
            try
            {
                if (!OpenConnection()) return dt;

                string query = @"SELECT * FROM hasta_bilgileri 
WHERE aktif_pasif_durumu = 'Aktif' AND (
    tc = @term OR
    (ad || ' ' || soyad) ILIKE '%' || @termLike || '%' OR
    ad ILIKE '%' || @termLike || '%' OR
    soyad ILIKE '%' || @termLike || '%'
)
ORDER BY hasta_id DESC";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@term", term ?? string.Empty);
                    cmd.Parameters.AddWithValue("@termLike", term ?? string.Empty);
                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama Hatası: " + ex.Message);
            }
            return dt;
        }
    }
}