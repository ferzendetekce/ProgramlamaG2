--
-- PostgreSQL database dump
--

-- Dumped from database version 17.5
-- Dumped by pg_dump version 17.5

-- Started on 2025-11-25 22:20:37

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 218 (class 1259 OID 18100)
-- Name: kullanicilar_tablosu; Type: TABLE; Schema: public; Owner: postgres
--

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


ALTER TABLE public.kullanicilar_tablosu OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 18099)
-- Name: kullanicilar_tablosu_kullanici_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.kullanicilar_tablosu_kullanici_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.kullanicilar_tablosu_kullanici_id_seq OWNER TO postgres;

--
-- TOC entry 4911 (class 0 OID 0)
-- Dependencies: 217
-- Name: kullanicilar_tablosu_kullanici_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.kullanicilar_tablosu_kullanici_id_seq OWNED BY public.kullanicilar_tablosu.kullanici_id;


--
-- TOC entry 220 (class 1259 OID 18110)
-- Name: sistem_loglari_tablosu; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sistem_loglari_tablosu (
    log_id integer NOT NULL,
    kullanici_id bigint,
    islem_tipi character varying(50) NOT NULL,
    islem_detayi text,
    zaman_damgasi timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    ip_adresi character varying(45),
    hata_seviyesi character varying(10) NOT NULL,
    CONSTRAINT sistem_loglari_tablosu_hata_seviyesi_check CHECK (((hata_seviyesi)::text = ANY ((ARRAY['Info'::character varying, 'Warning'::character varying, 'Error'::character varying, 'Critical'::character varying])::text[])))
);


ALTER TABLE public.sistem_loglari_tablosu OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 18109)
-- Name: sistem_loglari_tablosu_log_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.sistem_loglari_tablosu_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.sistem_loglari_tablosu_log_id_seq OWNER TO postgres;

--
-- TOC entry 4912 (class 0 OID 0)
-- Dependencies: 219
-- Name: sistem_loglari_tablosu_log_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.sistem_loglari_tablosu_log_id_seq OWNED BY public.sistem_loglari_tablosu.log_id;


--
-- TOC entry 4747 (class 2604 OID 18103)
-- Name: kullanicilar_tablosu kullanici_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kullanicilar_tablosu ALTER COLUMN kullanici_id SET DEFAULT nextval('public.kullanicilar_tablosu_kullanici_id_seq'::regclass);


--
-- TOC entry 4749 (class 2604 OID 18113)
-- Name: sistem_loglari_tablosu log_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sistem_loglari_tablosu ALTER COLUMN log_id SET DEFAULT nextval('public.sistem_loglari_tablosu_log_id_seq'::regclass);


--
-- TOC entry 4903 (class 0 OID 18100)
-- Dependencies: 218
-- Data for Name: kullanicilar_tablosu; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.kullanicilar_tablosu (kullanici_id, kullanici_adi_soyadi, sifre_hash, tc, pasaport_no, cinsiyet, rol, e_posta, telefon, kayit_tarihi, son_giris_tarihi, aktif_pasif_durumu) FROM stdin;
\.


--
-- TOC entry 4905 (class 0 OID 18110)
-- Dependencies: 220
-- Data for Name: sistem_loglari_tablosu; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.sistem_loglari_tablosu (log_id, kullanici_id, islem_tipi, islem_detayi, zaman_damgasi, ip_adresi, hata_seviyesi) FROM stdin;
\.


--
-- TOC entry 4913 (class 0 OID 0)
-- Dependencies: 217
-- Name: kullanicilar_tablosu_kullanici_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.kullanicilar_tablosu_kullanici_id_seq', 1, false);


--
-- TOC entry 4914 (class 0 OID 0)
-- Dependencies: 219
-- Name: sistem_loglari_tablosu_log_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.sistem_loglari_tablosu_log_id_seq', 1, false);


--
-- TOC entry 4753 (class 2606 OID 18108)
-- Name: kullanicilar_tablosu kullanicilar_tablosu_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kullanicilar_tablosu
    ADD CONSTRAINT kullanicilar_tablosu_pkey PRIMARY KEY (kullanici_id);


--
-- TOC entry 4755 (class 2606 OID 18119)
-- Name: sistem_loglari_tablosu sistem_loglari_tablosu_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sistem_loglari_tablosu
    ADD CONSTRAINT sistem_loglari_tablosu_pkey PRIMARY KEY (log_id);


--
-- TOC entry 4756 (class 2606 OID 18120)
-- Name: sistem_loglari_tablosu sistem_loglari_tablosu_kullanici_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sistem_loglari_tablosu
    ADD CONSTRAINT sistem_loglari_tablosu_kullanici_id_fkey FOREIGN KEY (kullanici_id) REFERENCES public.kullanicilar_tablosu(kullanici_id);


-- Completed on 2025-11-25 22:20:37

--
-- PostgreSQL database dump complete
--

