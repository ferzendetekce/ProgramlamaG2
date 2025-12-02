--
-- PostgreSQL database dump
--

\restrict 8KyBommsxXst8YJZtambP8irekG3dfS1oc96Bp2j3yHhuiEamiZ50AE5zzgmez9

-- Dumped from database version 18.0
-- Dumped by pg_dump version 18.0

-- Started on 2025-11-25 21:45:18

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
-- TOC entry 220 (class 1259 OID 16776)
-- Name: ayarlar_tablosu; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ayarlar_tablosu (
    ayar_anahtari integer NOT NULL,
    ayar_degeri text NOT NULL,
    aciklama text,
    guncelleme_tarihi timestamp with time zone DEFAULT now(),
    guncelleyen_kullanici_id integer
);


ALTER TABLE public.ayarlar_tablosu OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16775)
-- Name: ayarlar_tablosu_ayar_anahtari_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.ayarlar_tablosu_ayar_anahtari_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.ayarlar_tablosu_ayar_anahtari_seq OWNER TO postgres;

--
-- TOC entry 5014 (class 0 OID 0)
-- Dependencies: 219
-- Name: ayarlar_tablosu_ayar_anahtari_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.ayarlar_tablosu_ayar_anahtari_seq OWNED BY public.ayarlar_tablosu.ayar_anahtari;


--
-- TOC entry 4856 (class 2604 OID 16779)
-- Name: ayarlar_tablosu ayar_anahtari; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ayarlar_tablosu ALTER COLUMN ayar_anahtari SET DEFAULT nextval('public.ayarlar_tablosu_ayar_anahtari_seq'::regclass);


--
-- TOC entry 5008 (class 0 OID 16776)
-- Dependencies: 220
-- Data for Name: ayarlar_tablosu; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.ayarlar_tablosu (ayar_anahtari, ayar_degeri, aciklama, guncelleme_tarihi, guncelleyen_kullanici_id) FROM stdin;
\.


--
-- TOC entry 5015 (class 0 OID 0)
-- Dependencies: 219
-- Name: ayarlar_tablosu_ayar_anahtari_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.ayarlar_tablosu_ayar_anahtari_seq', 1, false);


--
-- TOC entry 4859 (class 2606 OID 16786)
-- Name: ayarlar_tablosu ayarlar_tablosu_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ayarlar_tablosu
    ADD CONSTRAINT ayarlar_tablosu_pkey PRIMARY KEY (ayar_anahtari);


-- Completed on 2025-11-25 21:45:18

--
-- PostgreSQL database dump complete
--

\unrestrict 8KyBommsxXst8YJZtambP8irekG3dfS1oc96Bp2j3yHhuiEamiZ50AE5zzgmez9

