--sisteme giriş için gerekli bilgiler

CREATE TABLE Sistem_Loglari_Tablosu(

    Log_ID SERIAL PRIMARY KEY, 
    Kullanici_Id INTEGER,           
    Islem_Tipi VARCHAR(50) NOT NULL,
    Islem_Detayi TEXT,
    Zaman_Damgasi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    IP_Adresi VARCHAR(45),
    Hata_Seviyesi VARCHAR(10) NOT NULL
        CHECK (Hata_Seviyesi IN ('Info', 'Warning', 'Error', 'Critical')),

    FOREIGN KEY (Kullanici_Id) REFERENCES Kullanicilar_Tablosu(Kullanici_Id)
);
