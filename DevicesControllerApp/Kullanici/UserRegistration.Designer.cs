// 1. Gerekli kütüphaneleri ve User modelimizi ekliyoruz
using System;
using System.Collections.Generic; // Listeler için
using System.Data;
using System.Linq; // .ToList() için
using System.Windows.Forms;
using DevicesControllerApp.Models; // <-- Adım 1'de oluşturduğumuz User.cs için
using System.IO;
using System.Drawing;

// 2. Namespace'i kontrol edin (Kullanici klasörüne eklediyseniz böyle olmalı)
namespace DevicesControllerApp.Kullanici
{
    // 3. Sınıf adı
    public partial class UserRegistration : UserControl
    {
        private List<User> _kullaniciListesi;
        // 4. Constructor (Yapıcı Metot) - Bu satır zaten mevcuttur
        public UserRegistration()
        {
            InitializeComponent();
        }

        // 5. Az önce oluşturduğunuz 'Load' olayı
        private void UserRegistration_Load(object sender, EventArgs e)
        {
            // Bu kontrol ilk yüklendiğinde yapılması gerekenler
            // (DesignMode kontrolü, tasarım ekranında kodun çalışmasını engeller)
            if (!this.DesignMode)
            {
                RolleriDoldur();
                KullanicilariListele(); // Sahte verilerle listeyi doldur
                FormuTemizle(); // Formun boş başlamasını sağla
            }
        }

        // 6. YENİ YARDIMCI METOT: Rolleri Doldur
        private void RolleriDoldur()
        {
            // Models/User.cs'teki UserRole enum'ındaki (Admin, Operatör, Servis)
            // tüm değerleri alıp cmbRol'e kaynak olarak atıyoruz.
            cmbRol.DataSource = Enum.GetValues(typeof(UserRole));
            cmbRol.SelectedIndex = -1; // Başlangıçta seçili olmasın
        }

        // 7. YENİ YARDIMCI METOT: Formu Temizle
        private void FormuTemizle()
        {
            // Sağ paneldeki tüm giriş alanlarını temizler
            // (İsimlerin Adım 3'teki gibi olduğundan emin olun)
            txtKullaniciAdi.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtSifre.Text = "";
            txtTcPasaport.Text = "";
            txtEposta.Text = "";
            txtTelefon.Text = "";
            cmbRol.SelectedIndex = -1;
            picFotograf.Image = null;
            picFotograf.ImageLocation = null; 

            // Yeni kullanıcı ekleneceği için şifre alanı aktif olmalı
            txtSifre.Enabled = true;

            // Listede seçili olan satırı kaldır
            dgvKullanicilar.ClearSelection();
        }
        // YARDIMCI METOT 1: Image'ı -> byte[] dizisine çevirir
        
        // 8. YENİ YARDIMCI METOT: Kullanıcıları Listele (SAHTE VERİ İLE)
        private void KullanicilariListele()
        {
            // 2. YENİ: Liste boşsa (yani program ilk kez çalışıyorsa)
            // sahte verilerle doldur.
            if (_kullaniciListesi == null)
            {
                _kullaniciListesi = new List<User>(); // Listeyi oluştur
                _kullaniciListesi.Add(new User
                {
                    Id = 1,
                    Username = "admin",
                    FirstName = "Ali",
                    LastName = "Veli",
                    Role = UserRole.Admin,
                    Email = "admin@site.com",
                    IsActive = true
                    // TODO (Kişi 2): PasswordHash eklenecek
                });
                _kullaniciListesi.Add(new User
                {
                    Id = 2,
                    Username = "operator1",
                    FirstName = "Ayşe",
                    LastName = "Yılmaz",
                    Role = UserRole.Operatör,
                    Email = "ayse@site.com",
                    IsActive = true
                    // TODO (Kişi 2): PasswordHash eklenecek
                });
            }

            // 3. GÜNCELLEME: DataGridView'in veri kaynağını bu yeni listeye bağla.
            // .ToList() ekleyerek listenin bir kopyasını veriyoruz, bu
            // DataGridView'in güncellemelerde yenilenmesini garantiler.
            // Sadece 'IsActive' true olanları (silinmemiş olanları) göster
            dgvKullanicilar.DataSource = _kullaniciListesi.Where(k => k.IsActive == true).ToList();

            // ... Sütun gizleme kodları (BUNLAR AYNI KALIR) ...
            dgvKullanicilar.Columns["Id"].HeaderText = "ID";
            // ...
            dgvKullanicilar.Columns["IsActive"].Visible = false;
        }
      

        // ... (Mevcut kodlarınız: constructor, Load olayı, RolleriDoldur, FormuTemizle, KullanicilariListele...)

        // 1. "Yeni Kullanıcı" butonuna tıklandığında (Az önce oluşturduğunuz metot)
        private void btnYeniKullanici_Click(object sender, EventArgs e)
        {
            // Formu temizleyen metodu çağırmamız yeterli
            FormuTemizle();
        }

        // 2. DataGridView'de seçim değiştiğinde (Az önce oluşturduğunuz metot)
        private void dgvKullanicilar_SelectionChanged(object sender, EventArgs e)
        {
            // Eğer listede seçili bir satır varsa (ve satır sayısı 0'dan fazlaysa)
            if (dgvKullanicilar.SelectedRows.Count > 0)
            {
                // Seçili satırdaki 'User' nesnesini al
                // Not: Listenin veri kaynağı (DataSource) 'User' nesnelerinden oluştuğu için
                // DataBoundItem'ı doğrudan User modelimize çevirebiliriz (casting).
                User seciliKullanici = (User)dgvKullanicilar.SelectedRows[0].DataBoundItem;

                // Bu kullanıcıyı forma doldurmak için yeni metodumuzu çağırıyoruz
                FormuDoldur(seciliKullanici);
            }
        }

        // 3. YENİ EKLENEN YARDIMCI METOT: FormuDoldur
        private void FormuDoldur(User user)
        {
            // FormuTemizle'nin tam tersi
            txtKullaniciAdi.Text = user.Username;
            txtAd.Text = user.FirstName;
            txtSoyad.Text = user.LastName;

            // Sahte verilerde bu alanlar boş (null) olabilir, 
            // .ToString() kullanmak null hatasını engeller (veya ?? "" kullanabiliriz)
            txtTcPasaport.Text = user.TcPassportNo;
            txtEposta.Text = user.Email;
            txtTelefon.Text = user.Phone;

            cmbRol.SelectedItem = user.Role;

            // YENİ EKLENDİ: byte[] dizisini Image'a çevir ve PictureBox'ta göster
            picFotograf.Image = ByteArrayToImage(user.Photograph);
            picFotograf.SizeMode = PictureBoxSizeMode.StretchImage; // Kutuya sığdır

            // MEVCUT BİR KULLANICIYI GÜNCELLEDİĞİMİZ İÇİN:
            // Güvenlik amacıyla Şifre alanı kilitli olmalı
            txtSifre.Text = "********"; // Gerçek şifreyi asla gösterme
            txtSifre.Enabled = false; // Şifre sadece 'Şifre Sıfırla' (Kişi 2) ile değişmeli
        }

        // btnKaydet_Click metodunun DOĞRU HALİ
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // --- 1. Doğrulama (Validation) ---
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
            {
                MessageBox.Show("Kullanıcı adı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Kaydetmeyi durdur
            }
            if (cmbRol.SelectedItem == null)
            {
                MessageBox.Show("Bir rol seçmelisiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Kaydetmeyi durdur
            }

            // --- TODO: Kişi 2'nin Görevleri ---
            // (Şifre kontrolleri buraya gelecek)
            // ------------------------------------


            // --- 2. Ekleme mi Güncelleme mi? ---
            if (dgvKullanicilar.SelectedRows.Count > 0)
            {
                // GÜNCELLEME İŞLEMİ
                User guncellenecekUser = (User)dgvKullanicilar.SelectedRows[0].DataBoundItem;

                // Formdaki verileri bu nesneye aktar
                guncellenecekUser.Username = txtKullaniciAdi.Text;
                guncellenecekUser.FirstName = txtAd.Text;
                guncellenecekUser.LastName = txtSoyad.Text;
                guncellenecekUser.TcPassportNo = txtTcPasaport.Text;
                guncellenecekUser.Email = txtEposta.Text;
                guncellenecekUser.Phone = txtTelefon.Text;
                guncellenecekUser.Role = (UserRole)cmbRol.SelectedItem;

                // Adım 8'de eklediğimiz fotoğraf kısmı
                guncellenecekUser.Photograph = ImageToByteArray(picFotograf.Image);
            }
            else
            {
                // YENİ EKLEME İŞLEMİ
                User newUser = new User();

                // Yeni bir ID ata
                newUser.Id = _kullaniciListesi.Count > 0 ? _kullaniciListesi.Max(k => k.Id) + 1 : 1;

                // Formdaki verileri bu nesneye aktar
                newUser.Username = txtKullaniciAdi.Text;
                newUser.FirstName = txtAd.Text;
                newUser.LastName = txtSoyad.Text;
                newUser.TcPassportNo = txtTcPasaport.Text;
                newUser.Email = txtEposta.Text;
                newUser.Phone = txtTelefon.Text;
                newUser.Role = (UserRole)cmbRol.SelectedItem;
                newUser.IsActive = true;
                newUser.PasswordHash = "sahte_hash_12345"; // Kişi 2 burayı düzeltecek

                // Adım 8'de eklediğimiz fotoğraf kısmı
                newUser.Photograph = ImageToByteArray(picFotograf.Image);

                // Sahte listemize yeni kullanıcıyı ekle
                _kullaniciListesi.Add(newUser);

            } // <-- BU 'else' BLOĞUNU KAPATAN PARANTEZ (SİZDE EKSİK VEYA YANLIŞ YERDEYDİ)

            // --- 3. Bitiriş ---
            MessageBox.Show("Kullanıcı başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            KullanicilariListele(); // Listeyi yenile 
            FormuTemizle();        // Formu temizle
        }
        // <-- btnKaydet_Click METODU BURADA BİTER.

        private void btnSil_Click(object sender, EventArgs e)
        {
            // 1. Listeden bir kullanıcı seçili mi?
            if (dgvKullanicilar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek için bir kullanıcı seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Emin misiniz? (Onay)
            User seciliKullanici = (User)dgvKullanicilar.SelectedRows[0].DataBoundItem;

            DialogResult result = MessageBox.Show(
                $"'{seciliKullanici.Username}' adlı kullanıcıyı silmek istediğinizden emin misiniz?\nBu işlem geri alınamaz.",
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // 3. Soft Delete İşlemi
                // Kullanıcıyı listeden silmiyoruz, sadece 'IsActive' bayrağını false yapıyoruz.
                seciliKullanici.IsActive = false;

                // 4. Listeyi ve Formu Yenile
                KullanicilariListele(); // Liste yenilendiğinde, IsActive=false olanlar kaybolacak
                FormuTemizle();

                MessageBox.Show("Kullanıcı başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // 'Hayır' derse hiçbir şey yapma
        }

       
private void btnFotografSec_Click(object sender, EventArgs e)
    {
        // Dosya seçme penceresi oluştur
        OpenFileDialog openFile = new OpenFileDialog();

        // Sadece resim dosyalarını filtrele
        openFile.Filter = "Resim Dosyaları (*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif";

        // Pencereyi aç ve kullanıcı 'OK' (Tamam) tuşuna basarsa
        if (openFile.ShowDialog() == DialogResult.OK)
        {
            // Seçilen resmi 'picFotograf' PictureBox'ına yükle
            // ImageLocation, resmi dosya yolundan yükler. Bu, bellek için daha verimlidir.
            picFotograf.ImageLocation = openFile.FileName;

            // Resmi 'Stretch' moduna al ki kutuya sığsın (isteğe bağlı ama önerilir)
            picFotograf.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
        private byte[] ImageToByteArray(Image image)
        {
            if (image == null)
                return null;

            // MemoryStream kullanarak resmi bellekte bir akışa dönüştür
            using (MemoryStream ms = new MemoryStream())
            {
                // Resmi 'Bmp' (Bitmap) formatında streame kaydet
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                // Akışın byte dizisini geri döndür
                return ms.ToArray();
            }
        }

        // --- YENİ EKLENECEK METOT 2 (Gelecekteki Hatayı Önleyecek) ---
        // YARDIMCI METOT 2: byte[] dizisini -> Image'a çevirir
        private Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            // MemoryStream kullanarak byte dizisini bellekte bir akışa dönüştür
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                // Stream'den Image nesnesi oluştur
                Image image = Image.FromStream(ms);
                return image;
            }
        }

        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            // 1. Arama kutusundaki metni al (küçük harfe çevirerek)
            string aramaMetni = txtArama.Text.ToLower();

            // 2. Ana listemizde (_kullaniciListesi) filtreleme yap
            // Not: Burası Database Grubu'nun kodu geldiğinde değişecek.
            // Şimdilik sahte liste (LINQ) üzerinde filtreleme yapıyoruz.
            List<User> filtrelenmisListe = _kullaniciListesi
                .Where(kullanici =>
                    kullanici.IsActive == true && // Sadece aktif olanları ara
                    (
                        kullanici.Username.ToLower().Contains(aramaMetni) ||
                        kullanici.FirstName.ToLower().Contains(aramaMetni) ||
                        kullanici.LastName.ToLower().Contains(aramaMetni) ||
                        kullanici.Email.ToLower().Contains(aramaMetni)
                    )
                ).ToList();

            // 3. DataGridView'in veri kaynağını bu YENİ FİLTRELENMİŞ LİSTE ile güncelle
            dgvKullanicilar.DataSource = filtrelenmisListe;
        }
    }
}
