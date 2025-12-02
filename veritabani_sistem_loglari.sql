CREATE TABLE public.kullanicilar_tablosu (
    kullanici_id bigint NOT NULL,
    kullanici_adi_soyadi character varying(100) NOT NULL,
    sifre_hash character varying(255) NOT NULL,
    tc character varying(11),
    pasaport_no character varying(20),
    cinsiyet character varying(15),
    rol character varying(10) NOT NULL,
    e_posta character varying(320),
    telefon character varying(15) NOT NULL,
    kayit_tarihi timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    son_giris_tarihi timestamp without time zone NOT NULL,
    aktif_pasif_durumu character varying(5) NOT NULL
);



