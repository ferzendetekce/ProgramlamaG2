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
	Terapi_Notlari TEXT	,

	--Buradaki FOREIGN KEY'ler 1 ve 2'nci tablolardan alınmıştır.
	CONSTRAINT Fk_Hasta_Id
	FOREIGN KEY (Hasta_Id) REFERENCES Hasta_Bilgileri(Hasta_Id),
	CONSTRAINT Fk_Operator_Id
	FOREIGN KEY (Operator_Id) REFERENCES Kullanicilar_Tablosu(Operator_Id)
);

CREATE TABLE Loadcell_Verileri_Tablosu 
(
	Loadcell_Id serial PRIMARY KEY
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
	Olcum_Index_Numarasi int NOT NULL DEFAULT 0

	Hastanin_Kullandigi_Taraf VARCHAR(100) CHECK (Hastanin_Kullandigi_Taraf 
	IN ('Sol','Sağ','İki Taraf Birden'))

	--Buradaki FOREIGN KEY 3. tablodan alınmıştır.
	CONSTRAINT Fk_Terapi_Id
	FOREIGN KEY (Terapi_Id) REFERENCES Terapiler_Tablosu(Terapi_Id) ON CASCADE DELETE
);

