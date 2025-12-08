using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Npgsql;
using System.Security.Cryptography;

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
                    instance = new DatabaseManager();
                return instance;
            }
        }

        private string connectionString;

        private DatabaseManager()
        {
            connectionString = "Server=localhost;Port=5432;Database=prgd2;User Id=postgres;Password=1234;";
        }

        NpgsqlConnection conn;

        public bool OpenConnection()
        {
            conn = new NpgsqlConnection(connectionString);
            try
            {
                conn.Open();
                return conn.State == ConnectionState.Open;
            }
            catch
            {
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    return true;
                }
            }
            catch { }
            return false;
        }

        // --------------------- USER LOGIN ----------------------------

        public bool ValidateUserLogin(string username, string password)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    return false;

                string query = @"SELECT sifre_hash 
                                 FROM kullanicilar_tablosu 
                                 WHERE kullanici_adi_soyadi = @u 
                                 LIMIT 1";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    object result = cmd.ExecuteScalar();

                    if (result == null)
                        return false;

                    string hashedPassword = result.ToString();
                    return VerifyPassword(password, hashedPassword);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "ValidateUserLogin");
                return false;
            }
        }

        public DataRow GetUserByUsername(string username)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    return null;

                string query = @"SELECT * 
                                 FROM kullanicilar_tablosu 
                                 WHERE kullanici_adi_soyadi = @u 
                                 LIMIT 1";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);

                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count == 0)
                            return null;

                        return dt.Rows[0];
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "GetUserByUsername");
                return null;
            }
        }

        // --------------------- PASSWORD SECURITY ----------------------------

        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            string hashOfInput = HashPassword(password);
            return hashOfInput == hashedPassword;
        }

        // --------------------- LOGGING ----------------------------

        private void LogError(Exception ex, string methodName)
        {
            Console.WriteLine($"[ERROR] {methodName}: {ex.Message}");
        }
        // --------------------- RESET PASSWORD ----------------------------

        public bool ResetPassword(string username, string tc, string newPassword)
        {
            try
            {
                if (conn == null || conn.State != ConnectionState.Open)
                    conn.Open();

                
                string newHash = HashPassword(newPassword);

                string query = @"UPDATE kullanicilar_tablosu
                         SET sifre_hash = @h
                         WHERE LOWER(TRIM(kullanici_adi_soyadi)) = LOWER(TRIM(@u))
                         AND TRIM(tc) = TRIM(@tc)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@h", newHash);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@tc", tc);

                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "ResetPassword");
                return false;
            }
        }


        // ------------------ OTHER METHODS (EMPTY FOR NOW) -------------------

        public DataTable GetAllCitys() { return null; }
        public bool HastaSil(long tc) { return false; }
        public bool TestConnection() { return false; }

        // Other functions remain unchanged for now...
    }

    // LoadCell Data Model
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
