namespace Pax360.Models
{
    public class CreateLeadSiparisKaydetModel
    {
        public MikroSiparis Mikro { get; set; }
    }

    public class MikroSiparis
    {
        public string FirmaKodu { get; set; }
        public int CalismaYili { get; set; }
        public string ApiKey { get; set; }
        public string KullaniciKodu { get; set; }
        public string Sifre { get; set; }
        public int FirmaNo { get; set; }
        public int SubeNo { get; set; }
        public List<Evrak> evraklar { get; set; }
    }

    public class Evrak
    {
        public List<EvrakAciklama> evrak_aciklamalari { get; set; }
        public List<Satir> satirlar { get; set; }
    }

    public class EvrakAciklama
    {
        public string aciklama { get; set; }
    }

    public class Satir
    {
        public string sip_Guid { get; set; }
        public string sip_tarih { get; set; }
        public string sip_tip { get; set; }
        public string sip_cins { get; set; }
        public string sip_evrakno_seri { get; set; }
        public string sip_musteri_kod { get; set; }
        public string sip_stok_kod { get; set; }
        public string sip_belgeno { get; set; }
        public decimal sip_b_fiyat { get; set; }
        public int sip_miktar { get; set; }
        public int sip_birim_pntr { get; set; }
        public decimal sip_tutar { get; set; }
        public int sip_vergi_pntr { get; set; }
        public int sip_depono { get; set; }
        public bool sip_vergisiz_fl { get; set; }
        public string sip_stok_sormerk { get; set; }
        public string seriler { get; set; }
        public int sip_harekettipi { get; set; }
        public string sip_special1 { get; set; }
        public string sip_special3 { get; set; }
        public string sip_eticaret_kanal_kodu { get; set; }
        public string sip_HareketGrupKodu1 { get; set; }
        public string sip_HareketGrupKodu2 { get; set; }
        public string sip_satici_kod { get; set; }
        public string sip_paket_kod { get; set; }
        public string sip_Exp_Imp_Kodu { get; set; }
        public List<UserTablo> user_tablo { get; set; }
        public List<RenkBeden> renk_beden { get; set; }
        public string sip_ExternalProgramId { get; set; }
        public int sip_doviz_cinsi { get; set; }
        public int sip_satis_fiyat_doviz_kuru { get; set; }
    }

    public class UserTablo
    {
        public string aciklama { get; set; }
    }

    public class RenkBeden
    {
        public int renk_no { get; set; }
        public int beden_no { get; set; }
        public int miktar { get; set; }
    }
}
