-- ŞEHİRLER TABLOSU
CREATE TABLE Sehirler_Tablosu (
    Plaka_Kodu SMALLINT PRIMARY KEY,
    Sehir_Adi VARCHAR(50) NOT NULL
);

-- HASTA BİLGİLERİ TABLOSU
CREATE TABLE Hasta_Bilgileri( 
    Hasta_Id SERIAL PRIMARY KEY,
    Ad VARCHAR(30) NOT NULL,
    Soyad VARCHAR(30) NOT NULL,
    TC VARCHAR(11) UNIQUE,
    Pasaport_No VARCHAR(20) UNIQUE,
    Cinsiyet VARCHAR(15),
    Dogum_Tarihi TIMESTAMP NOT NULL,
    E_Mail VARCHAR(100),
    Adresi VARCHAR(100) NOT NULL,
    Memleketi_Plaka_Kodu SMALLINT,
    Dogum_Yeri_Plaka_Kodu SMALLINT,
    Hasta_Telefon_No VARCHAR(20),
    Hasta_Yakini_Adi VARCHAR(30),
    Hasta_Yakini_Soyadi VARCHAR(30),
    Hasta_Yakini_Neyi VARCHAR(20),
    Hasta_Yakini_Telefon_No VARCHAR(20),
    Hastalik_Tanisi VARCHAR(300),
    Kilo_Kg DECIMAL,
    Boy_Cm DECIMAL,
    Ayak_No DECIMAL,
    Kalca_Diz_Mesafesi DECIMAL,
    Diz_Topuk_Mesafesi DECIMAL,
    Kayit_Tarihi TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Guncelleme_Tarihi TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Aktif_Pasif_Durumu VARCHAR(10),

    -- KOŞULLAR
    CONSTRAINT TC_PS CHECK (TC IS NOT NULL OR Pasaport_No IS NOT NULL),
    CONSTRAINT chk_cinsiyet CHECK (Cinsiyet IN ('Kadın', 'Erkek', 'Belirtmek İstemiyorum')),
    CONSTRAINT TEL1_TEL2 CHECK (Hasta_Telefon_No IS NOT NULL OR Hasta_Yakini_Telefon_No IS NOT NULL),
    CONSTRAINT Akt_Pas CHECK (Aktif_Pasif_Durumu IN ('Aktif', 'Pasif')),

    -- YABANCI ANAHTARLAR
    CONSTRAINT fk_memleket FOREIGN KEY (Memleketi_Plaka_Kodu)
        REFERENCES Sehirler_Tablosu (Plaka_Kodu)
        ON UPDATE CASCADE ON DELETE RESTRICT,

    CONSTRAINT fk_dogum_yeri FOREIGN KEY (Dogum_Yeri_Plaka_Kodu)
        REFERENCES Sehirler_Tablosu (Plaka_Kodu)
        ON UPDATE CASCADE ON DELETE RESTRICT
);

-- KULLANICILAR TABLOSU
CREATE TABLE Kullanicilar_Tablosu(
	Kullanici_Id SERIAL PRIMARY KEY,
    Kullanici_Adi_Soyadi VARCHAR(100) NOT NULL,
    Sifre_Hash VARCHAR(255) NOT NULL,
    TC VARCHAR(11),
    Pasaport_No VARCHAR(20),
    Cinsiyet VARCHAR(15),
    Rol VARCHAR(10) NOT NULL,
    E_Posta VARCHAR(320),
    Telefon VARCHAR(15) NOT NULL,
    Kayit_Tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Son_Giris_Tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Aktif_Pasif_Durumu VARCHAR(5) NOT NULL,
    CONSTRAINT chk_cinsiyet_kullanici CHECK (Cinsiyet IN ('Kadın', 'Erkek', 'Belirtmek İstemiyorum')),
    CONSTRAINT chk_aktif_pasif CHECK (Aktif_Pasif_Durumu IN ('Aktif','Pasif'))
);

-- ŞEHİRLER VERİLERİ
INSERT INTO Sehirler_Tablosu (Plaka_Kodu, Sehir_Adi) VALUES
(1, 'Adana'),
(2, 'Adıyaman'),
(3, 'Afyonkarahisar'),
(4, 'Ağrı'),
(5, 'Amasya'),
(6, 'Ankara'),
(7, 'Antalya'),
(8, 'Artvin'),
(9, 'Aydın'),
(10, 'Balıkesir'),
(11, 'Bilecik'),
(12, 'Bingöl'),
(13, 'Bitlis'),
(14, 'Bolu'),
(15, 'Burdur'),
(16, 'Bursa'),
(17, 'Çanakkale'),
(18, 'Çankırı'),
(19, 'Çorum'),
(20, 'Denizli'),
(21, 'Diyarbakır'),
(22, 'Edirne'),
(23, 'Elazığ'),
(24, 'Erzincan'),
(25, 'Erzurum'),
(26, 'Eskişehir'),
(27, 'Gaziantep'),
(28, 'Giresun'),
(29, 'Gümüşhane'),
(30, 'Hakkari'),
(31, 'Hatay'),
(32, 'Isparta'),
(33, 'Mersin'),
(34, 'İstanbul'),
(35, 'İzmir'),
(36, 'Kars'),
(37, 'Kastamonu'),
(38, 'Kayseri'),
(39, 'Kırklareli'),
(40, 'Kırşehir'),
(41, 'Kocaeli'),
(42, 'Konya'),
(43, 'Kütahya'),
(44, 'Malatya'),
(45, 'Manisa'),
(46, 'Kahramanmaraş'),
(47, 'Mardin'),
(48, 'Muğla'),
(49, 'Muş'),
(50, 'Nevşehir'),
(51, 'Niğde'),
(52, 'Ordu'),
(53, 'Rize'),
(54, 'Sakarya'),
(55, 'Samsun'),
(56, 'Siirt'),
(57, 'Sinop'),
(58, 'Sivas'),
(59, 'Tekirdağ'),
(60, 'Tokat'),
(61, 'Trabzon'),
(62, 'Tunceli'),
(63, 'Şanlıurfa'),
(64, 'Uşak'),
(65, 'Van'),
(66, 'Yozgat'),
(67, 'Zonguldak'),
(68, 'Aksaray'),
(69, 'Bayburt'),
(70, 'Karaman'),
(71, 'Kırıkkale'),
(72, 'Batman'),
(73, 'Şırnak'),
(74, 'Bartın'),
(75, 'Ardahan'),
(76, 'Iğdır'),
(77, 'Yalova'),
(78, 'Karabük'),
(79, 'Kilis'),
(80, 'Osmaniye'),
(81, 'Düzce');
	
CREATE TABLE Terapiler_Tablosu
(
	Terapi_Id serial PRIMARY KEY,
	Hasta_Id int NOT NULL,
	Operator_Id int NOT NULL,
	Loadcell_Cihaz_Model_Adi varchar(100) NOT NULL,

	Hastanin_Kacinci_Terapisi int NOT NULL,
	Terapi_Baslangic_Zamani timestamp NOT NULL,
	Terapi_Bitis_Zamani timestamp,
	
	Terapi_Suresi decimal(6,2) NOT NULL DEFAULT 0,
	Terapi_Makine_Hizi decimal(6,2) NOT NULL DEFAULT 0,
 
	Hastanin_Guncel_Kalp_Atis_Hizi int,
	Hastanin_Ortalama_Kalp_Atis_Hizi int,
	Hastanin_Toplam_Adim_Sayisi int,

	--Hastanın bacaklarının eklem açı değerleri
	Hastanin_Ortalama_Sag_Diz_Acisi decimal(4,1), 
	Hastanin_Ortalama_Sol_Diz_Acisi decimal(4,1),
	Hastanin_Ortalama_Sag_Ayak_Bilegi_Acisi decimal(4,1),
	Hastanin_Ortalama_Sol_Ayak_Bilegi_Acisi decimal(4,1),
	Hastanin_Ortalama_Sag_Kalca_Acisi decimal(4,1),
	Hastanin_Ortalama_Sol_Kalca_Acisi decimal(4,1) ,

	--Hastanın bacak eklemlerinin 0-10 arası ağrı değerleri
	Sag_Diz_Agri_Seviyesi decimal(3,1) 
	CHECK (Sag_Diz_Agri_Seviyesi>=0 AND Sag_Diz_Agri_Seviyesi<=10),
	Sol_Diz_Agri_Seviyesi decimal(3,1) 
	CHECK (Sol_Diz_Agri_Seviyesi>=0 AND Sol_Diz_Agri_Seviyesi<=10),
	Sag_Ayak_Bilegi_Agri_Seviyesi decimal(3,1) 
	CHECK (Sag_Ayak_Bilegi_Agri_Seviyesi>=0 AND Sag_Ayak_Bilegi_Agri_Seviyesi<=10),
	Sol_Ayak_Bilegi_Agri_Seviyesi decimal(3,1) 
	CHECK (Sol_Ayak_Bilegi_Agri_Seviyesi>=0 AND Sol_Ayak_Bilegi_Agri_Seviyesi<=10),
	Sag_Kalca_Agri_Seviyesi decimal(3,1) 
	CHECK (Sag_Kalca_Agri_Seviyesi>=0 AND Sag_Kalca_Agri_Seviyesi<=10),
	Sol_Kalca_Agri_Seviyesi decimal(3,1) 
	CHECK (Sol_Kalca_Agri_Seviyesi>=0 AND Sol_Kalca_Agri_Seviyesi<=10),
	
	Ortalama_Azaltilan_Agirlik decimal(6,2) NOT NULL DEFAULT 0,
	Destek_Bari_Yuksekligi decimal(6,2)NOT NULL DEFAULT 0,
	Ayak_Numarasi_Ayari decimal(3,1),
	
	Terapi_Durumu VARCHAR(50) CHECK (Terapi_Durumu 
	IN ('Yarıda Kesildi','Devam Ediyor','Tamamlandı')),

	Hasta_Tepkileri TEXT,
	Terapi_Notlari TEXT,

	--Buradaki FOREIGN KEY'ler 1 ve 2'nci tablolardan alınmıştır.
	CONSTRAINT Fk_Hasta_Id
	FOREIGN KEY (Hasta_Id) REFERENCES Hasta_Bilgileri(Hasta_Id),
	CONSTRAINT Fk_Operator_Id
	FOREIGN KEY (Operator_Id) REFERENCES Kullanicilar_Tablosu(Kullanici_Id)
);

CREATE TABLE Loadcell_Verileri_Tablosu 
(
	Loadcell_Id serial PRIMARY KEY,
	Terapi_Id int NOT NULL,

	Loadcell_Karti_Sicakligi decimal(5,2) NOT NULL DEFAULT 0,
	Zaman_Damgasi timestamp NOT NULL,
	--Zaman_Damgasi: Ölçümün kesin olarak hangi tarihte ve saatte alındığını gösterir.
	Ornekleme_Frekansi int NOT NULL DEFAULT 0, 
	--"Ornekleme Frekansi": LoadCell sensörünün veriyi saniyede kaç kez ölçtüğünü gösterir.	
	--Örneğin 100 Hz ise sensör her saniye 100 ölçüm alır.
	
	Sag_Topuk_Basinc_Degeri decimal(6,2) NOT NULL DEFAULT 0,
	Sol_Topuk_Basinc_Degeri decimal(6,2) NOT NULL DEFAULT 0,
	Sag_On_Ayak_Basinc_Degeri decimal(6,2) NOT NULL DEFAULT 0,
	Sol_On_Ayak_Basinc_Degeri decimal(6,2) NOT NULL DEFAULT 0,
	Sag_Ayak_Toplam_Basinc_Degeri decimal(6,2) NOT NULL DEFAULT 0,
	Sol_Ayak_Toplam_Basinc_Degeri decimal(6,2) NOT NULL DEFAULT 0,
	Sag_Ayak_Merkez_Basinc_Koordinati decimal(6,2) NOT NULL DEFAULT 0,
	Sol_Ayak_Merkez_Basinc_Koordinati decimal(6,2) NOT NULL DEFAULT 0,

	--Buradaki veriler 0-10 arası değerler alabilir.
	--Örnegin: "Hastanın sağ ayağı 10 üzerinden 3 aktivite seviyesi gösteriyor."
	Sag_Ayak_Taban_Basinc_Aktivitesi decimal(3,1)
	CHECK (Sag_Ayak_Taban_Basinc_Aktivitesi >= 0 AND Sag_Ayak_Taban_Basinc_Aktivitesi <= 10),
	Sol_Ayak_Taban_Basinc_Aktivitesi decimal(3,1)
	CHECK (Sol_Ayak_Taban_Basinc_Aktivitesi >= 0 AND Sol_Ayak_Taban_Basinc_Aktivitesi <= 10),

	Azaltilan_Agirlik_Degeeri decimal(6,2) NOT NULL DEFAULT 0,
	Agirlik_Dengeleme_Degeri decimal(6,2) NOT NULL DEFAULT 0,
	Olcum_Index_Numarasi int NOT NULL DEFAULT 0,

	Hastanin_Kullandigi_Taraf VARCHAR(100) CHECK (Hastanin_Kullandigi_Taraf 
	IN ('Sol','Sağ','İki Taraf Birden')),

	--Buradaki FOREIGN KEY 3. tablodan alınmıştır.
	CONSTRAINT Fk_Terapi_Id
	FOREIGN KEY (Terapi_Id) REFERENCES Terapiler_Tablosu(Terapi_Id) ON DELETE CASCADE
);

CREATE TABLE Ayarlar_Tablosu(

--burada ayar anahtarı yeni eklenicek ayarların ismi 
--ayar değeri ise yeni eklenicek olan ayarlardaki değerler için kullanılır

    Ayar_Anahtari VARCHAR(50) NOT NULL,
    Ayar_Degeri VARCHAR(50) NOT NULL,
    Aciklama VARCHAR(100),
    Hareket_Hizi NUMERIC(5,2),
    Destek_Seviyesi INTEGER,
    Yuruyus_Mod VARCHAR(20),
    Kuvvet_Geribildirim BOOLEAN DEFAULT FALSE,
    Tema VARCHAR(20) DEFAULT 'acik',
    Dil VARCHAR(10) DEFAULT 'tr',
    Sesli_Geribildirim BOOLEAN DEFAULT TRUE,
    Uyari_Sesi_Seviyesi INTEGER DEFAULT 70,
    Son_Bakim_Tarihi DATE,
    Guncelleme_Tarihi TIMESTAMP DEFAULT NOW(),
    Guncelleyen_kullanici_id INTEGER,
    
    PRIMARY KEY (Ayar_Anahtari),
    FOREIGN KEY (Guncelleyen_kullanici_id) REFERENCES Kullanicilar_Tablosu(Kullanici_Id)
);

CREATE TABLE Cihaz_Durum_Loglari(
    Log_Id BIGSERIAL PRIMARY KEY,
    Servo_Motor_Durum BOOLEAN[],
    Step_Motor_Durum BOOLEAN[],
    Limit_Switch_Durumlari BOOLEAN,
    Zaman_Damgasi TIMESTAMP DEFAULT NOW() NOT NULL,
    Hata_Kodlari INTEGER[]
);

CREATE TABLE Sistem_Loglari_Tablosu(
    Log_ID SERIAL PRIMARY KEY,  -- logun benzersiz ID'si
    Kullanici_Id BIGINT,           
    Islem_Tipi VARCHAR(50) NOT NULL,
    Islem_Detayi TEXT,
    Zaman_Damgasi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    IP_Adresi VARCHAR(45),
    Hata_Seviyesi VARCHAR(10) NOT NULL
        CHECK (Hata_Seviyesi IN ('Info', 'Warning', 'Error', 'Critical')),

    FOREIGN KEY (Kullanici_Id) REFERENCES Kullanicilar_Tablosu(Kullanici_Id)
);
