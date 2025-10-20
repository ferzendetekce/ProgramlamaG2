CREATE TABLE Hasta_Bilgileri( 
	Hasta_Id SERIAL PRIMARY KEY,
	Ad VARCHAR(30) NOT NULL,
	Soyad VARCHAR(30) NOT NULL,
	TC VARCHAR(11) UNIQUE,
	Pasaport_No VARCHAR(20) UNIQUE,
	Cinsiyet VARCHAR(15),
	Dogum_Tarihi TIMESTAMP NOT NULL,
	E_Mail VARCHAR(20),
	Adresi VARCHAR(100) NOT NULL,
	Memleketi VARCHAR(20),
	Dogum_Yeri VARCHAR(30),
	Hasta_Telefon_No VARCHAR(20) NOT NULL,
	Hasta_Yakini_Adi VARCHAR(30),
	Hasta_Yakini_Soyadi VARCHAR(30),
	Hasta_Yakini_Neyi VARCHAR(20),
	Hasta_Yakini_Telefon_No int,
	Hastalik_Tanisi VARCHAR(100),
	Kilo_Kg decimal,
	Boy_Cm decimal,
	Ayak_No decimal,
	Kalca_Diz_Mesafesi decimal,
	Diz_Topuk_Mesafesi decimal,
	Kayit_Tarihi TIMESTAMP NOT NULL,
	Guncelleme_Tarihi TIMESTAMP NOT NULL,
	Aktif_Pasif_Durumu VARCHAR(10) NOT NULL,
	CONSTRAINT TC_PS CHECK(TC IS NOT NULL OR Pasaport_No IS NOT NULL)
);


CREATE TABLE Kullanicilar_Tablosu(
	Kullanici_Adi_Soyadi VARCHAR(100) NOT NULL,
	Sifre_Hash VARCHAR(255) NOT NULL,
	TC VARCHAR(11) NOT NULL,
	Pasaport_No VARCHAR(20) NOT NULL,
	Cinsiyet VARCHAR(15),
	Rol VARCHAR(10) NOT NULL,
	E_Posta VARCHAR(320),
	Telefon VARCHAR(15) NOT NULL,
	Kayit_Tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	Son_Giris_Tarihi TIMESTAMP NOT NULL,
	Aktif_Pasif_Durumu VARCHAR(5) NOT NULL
	
);





