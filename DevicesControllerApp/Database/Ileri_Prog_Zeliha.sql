CREATE TABLE Ayarlar_tablosu(

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
    FOREIGN KEY (Guncelleyen_kullanici_id) REFERENCES Kullanıcılar_Tablosu(Kullanıcı_Id)
);

CREATE TABLE Cihaz_Durum_Loglari(
    Log_Id BIGSERIAL PRIMARY KEY,
    Servo_Motor_Durum BOOLEAN[],
    Step_Motor_Durum BOOLEAN[],
    Limit_Switch_Durumlari BOOLEAN,
    Zaman_Damgasi TIMESTAMP DEFAULT NOW() NOT NULL,
    Hata_Kodlari INTEGER[]
);
