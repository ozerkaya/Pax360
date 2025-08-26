using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Pax360.Interfaces;
using Pax360.Models;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Xml.Serialization;

namespace Pax360.Helpers
{
    public class MikroHelper : IMikroHelper
    {
        private readonly IOptions<ExternalDBSettings> _externalConfig;
        private readonly IOptions<MicroSettings> _mikroConfig;
        private ICryptographyHelper _cryptographyHelper;
        public MikroHelper(IOptions<ExternalDBSettings> externalConfig, IOptions<MicroSettings> mikroConfig, ICryptographyHelper cryptographyHelper)
        {
            _externalConfig = externalConfig;
            _mikroConfig = mikroConfig;
            _cryptographyHelper = cryptographyHelper;
        }

        public Tuple<string, List<SelectListItem>> GetMikroCompanies()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();

            returnList.Add(new SelectListItem
            {
                Text = "Seçiniz",
                Value = "",
            });

            try
            {
                if (_externalConfig.Value.UseExternalDB == "0")
                {
                    returnList.Add(new SelectListItem
                    {
                        Text = "Ozer KAYA AŞ (21584062840)",
                        Value = "f572659c-ffdb-4fb4-bda6-b7d09bcfb171#Ozer KAYA AŞ",
                    });
                    return Tuple.Create(string.Empty, returnList);
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(_externalConfig.Value.MicroConnectionString))
                    {
                        connection.Open();
                        try
                        {
                            string query = @"SELECT [cari_Guid],[cari_unvan1],[cari_vdaire_no] FROM [CARI_HESAPLAR]";
                            SqlCommand command = new SqlCommand(query, connection);
                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {

                                if (reader[0] != null)
                                {
                                    returnList.Add(new SelectListItem
                                    {
                                        Text = string.Format("{0} ({1})", reader["cari_unvan1"]?.ToString(), reader["cari_vdaire_no"]?.ToString()),
                                        Value = string.Format("{0}#{1}", reader["cari_Guid"]?.ToString(), reader["cari_unvan1"]?.ToString()),
                                    });
                                }
                            }

                            return Tuple.Create(string.Empty, returnList);

                        }
                        catch (Exception ex)
                        {
                            return Tuple.Create(ex.Message, returnList);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, returnList);
            }
        }

        public Tuple<string, List<SelectListItem>> GetMikroProducts()
        {
            List<SelectListItem> returnList = new List<SelectListItem>();

            returnList.Add(new SelectListItem
            {
                Text = "Seçiniz",
                Value = "",
            });

            try
            {
                if (_externalConfig.Value.UseExternalDB == "0")
                {
                    returnList.Add(new SelectListItem
                    {
                        Text = "A920 MAX",
                        Value = "A920-3AW-RD5-25EU#A920#5",
                    });
                    return Tuple.Create(string.Empty, returnList);
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(_externalConfig.Value.MicroConnectionString))
                    {
                        connection.Open();
                        try
                        {
                            string query = @"SELECT [sto_kod],[sto_isim],[sto_toptan_vergi] FROM [STOKLAR] where [sto_sezon_kodu]='PAX360'";
                            SqlCommand command = new SqlCommand(query, connection);
                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {

                                if (reader[0] != null)
                                {
                                    string vergi = "20";

                                    if (reader["sto_isim"]?.ToString() == "6")
                                    {
                                        vergi = "20";
                                    }
                                    else if (reader["sto_isim"]?.ToString() == "5")
                                    {
                                        vergi = "10";
                                    }

                                    returnList.Add(new SelectListItem
                                    {
                                        Text = reader["sto_kod"]?.ToString() + " - " + reader["sto_isim"]?.ToString(),
                                        Value = string.Format("{0}#{1}#{2}", reader["sto_kod"]?.ToString(), reader["sto_isim"]?.ToString(), vergi),
                                    });
                                }
                            }

                            return Tuple.Create(string.Empty, returnList);

                        }
                        catch (Exception ex)
                        {
                            return Tuple.Create(ex.Message, returnList);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, returnList);
            }
        }

        public Tuple<string, OrderDetailsModel> GetMikroCompanyDetails(Guid cari_Guid)
        {
            OrderDetailsModel model = new OrderDetailsModel();

            try
            {
                if (cari_Guid == Guid.Empty)
                {
                    return Tuple.Create("Firma Bulunamadı!", model);
                }
                else if (_externalConfig.Value.UseExternalDB == "0")
                {
                    return Tuple.Create(string.Empty, new OrderDetailsModel
                    {
                        AdSoyad = "Özer KAYA",
                        VKNTCKN = "21500011222",
                        Telefon = "05535580817",
                        Eposta = "maqromedia@gmail.com",
                        FaturaAdresi = "Arifbey mah alaçam sok no:6",
                        Il = "Sakarya",
                        Ilce = "Arifiye",
                        MusteriAdi = "Özer KAYA AŞ",
                        SiparisNumarasi = Guid.NewGuid().ToString(),
                        cari_kod = "12345",
                        cari_Guid = cari_Guid,
                    });
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(_externalConfig.Value.MicroConnectionString))
                    {
                        connection.Open();
                        try
                        {
                            string query = @"WITH RankedOrders AS (
    SELECT 
           CARI_HESAPLAR.[cari_Guid],
           CARI_HESAPLAR.[cari_unvan1],
           CARI_HESAPLAR.[cari_vdaire_no],
           CARI_HESAPLAR.[cari_EMail],
           CARI_HESAPLAR.[cari_CepTel],
           CARI_HESAPLAR.[cari_kod],
           CARI_HESAPLAR.[cari_odemeplan_no],
           CARI_HESAP_ADRESLERI.[adr_cadde],
           CARI_HESAP_ADRESLERI.[adr_mahalle],
           CARI_HESAP_ADRESLERI.[adr_sokak],
           CARI_HESAP_ADRESLERI.[adr_Semt],
           CARI_HESAP_ADRESLERI.[adr_Apt_No],
           CARI_HESAP_ADRESLERI.[adr_Daire_No],
           CARI_HESAP_ADRESLERI.[adr_posta_kodu],
           CARI_HESAP_ADRESLERI.[adr_ilce],
           CARI_HESAP_ADRESLERI.[adr_il],
           CARI_HESAP_ADRESLERI.[adr_ulke],
           CARI_HESAP_ADRESLERI.[adr_tel_no1],
           CARI_HESAP_ADRESLERI.[adr_tel_no2],           
           CARI_HESAP_ADRESLERI.[adr_adres_no],
		   CARI_HESAP_YETKILILERI.[mye_isim],
		   CARI_HESAP_YETKILILERI.[mye_soyisim],
           ROW_NUMBER() OVER (
               PARTITION BY CARI_HESAPLAR.[cari_Guid] 
               ORDER BY 
                   CASE 
				       WHEN  [CARI_HESAP_ADRESLERI].adr_adres_no = 1 THEN 1         
					   WHEN  [CARI_HESAP_ADRESLERI].adr_adres_no = 2 THEN 2
                       ELSE 3
                   END
           ) AS RowNum
    FROM CARI_HESAPLAR
    INNER JOIN CARI_HESAP_ADRESLERI ON CARI_HESAP_ADRESLERI.adr_cari_kod = CARI_HESAPLAR.cari_kod
	LEFT JOIN CARI_HESAP_YETKILILERI ON CARI_HESAP_YETKILILERI.mye_cari_kod = CARI_HESAPLAR.cari_kod
    WHERE cari_Guid =@CARIGUID
      
)
SELECT *
FROM RankedOrders
WHERE RowNum = 1;";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@CARIGUID", cari_Guid);
                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                if (reader[0] != null)
                                {
                                    model.AdSoyad = reader["mye_isim"]?.ToString() + " " + reader["mye_soyisim"]?.ToString();
                                    model.VKNTCKN = reader["cari_vdaire_no"]?.ToString();
                                    model.Telefon = reader["cari_CepTel"]?.ToString();
                                    model.Eposta = reader["cari_EMail"]?.ToString();
                                    model.FaturaAdresi = reader["adr_cadde"]?.ToString() + " " +
                                                         reader["adr_sokak"]?.ToString() + " ";
                                    model.Il = reader["adr_il"]?.ToString();
                                    model.Ilce = reader["adr_ilce"]?.ToString();
                                    model.MusteriAdi = reader["cari_unvan1"]?.ToString();
                                    model.SiparisNumarasi = Guid.NewGuid().ToString();
                                    model.cari_kod = reader["cari_kod"]?.ToString();
                                    model.cari_Guid = cari_Guid;
                                    model.VadeTarihi = Convert.ToInt32(reader["cari_odemeplan_no"]?.ToString());
                                }
                            }

                            return Tuple.Create(string.Empty, model);

                        }
                        catch (Exception ex)
                        {
                            return Tuple.Create(ex.Message, model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, model);
            }
        }

        public Tuple<string, List<OrderListItemModel>> GetMikroOrderList(int page, OrderListModel dataModel)
        {
            List<OrderListItemModel> returnList = new List<OrderListItemModel>();

            try
            {
                if (_externalConfig.Value.UseExternalDB == "0")
                {
                    returnList.Add(new OrderListItemModel
                    {
                        Company = "Ozer Kaya AŞ",
                        SiparisDurumu = "Teslim Edildi",
                        SiparisNumarasi = Guid.NewGuid().ToString(),
                        SiparisTarihi = DateTime.Now,
                    });
                    return Tuple.Create(string.Empty, returnList);
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(_externalConfig.Value.MicroConnectionString))
                    {
                        connection.Open();
                        try
                        {
                            string query = @"SELECT 
[sip_belgeno]
,CARI_HESAPLAR.cari_unvan1
,[sip_tarih]
,[sip_special1] 
FROM [SIPARISLER]
INNER JOIN CARI_HESAPLAR ON CARI_HESAPLAR.cari_kod = SIPARISLER.sip_musteri_kod
WHERE  sip_eticaret_kanal_kodu = 'PAX360SATIS'";

                            if (!string.IsNullOrWhiteSpace(dataModel.SiparisNumarasi))
                            {
                                query += " AND sip_belgeno = @SIPARISNUMARASI";
                            }

                            query += @"
ORDER BY [sip_tarih] desc
OFFSET @SKIP ROWS
FETCH NEXT 50 ROWS ONLY";

                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@SKIP", (page - 1) * 50);
                            if (!string.IsNullOrWhiteSpace(dataModel.SiparisNumarasi))
                            {
                                command.Parameters.AddWithValue("@SIPARISNUMARASI", dataModel.SiparisNumarasi);
                            }
                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {

                                if (reader[0] != null)
                                {
                                    returnList.Add(new OrderListItemModel
                                    {
                                        Company = reader["cari_unvan1"]?.ToString() ?? "",
                                        SiparisDurumu = reader["sip_special1"]?.ToString() ?? "",
                                        SiparisNumarasi = reader["sip_belgeno"]?.ToString() ?? "",
                                        SiparisTarihi = reader.GetDateTime(reader.GetOrdinal("sip_tarih")),
                                    });
                                }
                            }

                            return Tuple.Create(string.Empty, returnList);

                        }
                        catch (Exception ex)
                        {
                            return Tuple.Create(ex.Message, returnList);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, returnList);
            }
        }

        public Tuple<string, int> GetMikroOrderListCount()
        {
            int count = 0;

            try
            {
                if (_externalConfig.Value.UseExternalDB == "0")
                {
                    return Tuple.Create(string.Empty, 1);
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(_externalConfig.Value.MicroConnectionString))
                    {
                        connection.Open();
                        try
                        {
                            string query = @"SELECT 
COUNT([sip_belgeno]) 
FROM [SIPARISLER]
WHERE  sip_eticaret_kanal_kodu = 'PAX360SATIS'";

                            SqlCommand command = new SqlCommand(query, connection);
                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {

                                if (reader[0] != null)
                                {
                                    count = Convert.ToInt32(reader[0].ToString());
                                }
                            }

                            return Tuple.Create(string.Empty, count);

                        }
                        catch (Exception ex)
                        {
                            return Tuple.Create(ex.Message, count);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, count);
            }
        }

        public async Task<Tuple<bool, string, string>> SiparisKaydetV2(ReqCreateLeadV2 Req, string cariNo)
        {
            var root = new CreateLeadSiparisKaydetModel
            {
                Mikro = new MikroSiparis
                {
                    FirmaKodu = _mikroConfig.Value.FirmaKodu,
                    CalismaYili = Convert.ToInt32(_mikroConfig.Value.CalismaYili),
                    ApiKey = _mikroConfig.Value.ApiKey,
                    KullaniciKodu = _mikroConfig.Value.KullaniciKodu,
                    Sifre = _cryptographyHelper.CreateMD5(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd"), _mikroConfig.Value.Sifre)),
                    FirmaNo = Convert.ToInt32(_mikroConfig.Value.FirmaNo),
                    SubeNo = Convert.ToInt32(_mikroConfig.Value.SubeNo),

                    evraklar = new List<Evrak>
                    {
                        new Evrak
                        {
                            evrak_aciklamalari = new List<EvrakAciklama>
                            {
                                new EvrakAciklama { aciklama = "Interpay Online Satış (Evrak Açıklama)" },
                                new EvrakAciklama { aciklama = "" },
                                new EvrakAciklama { aciklama = "" },
                                new EvrakAciklama { aciklama = "" },
                                new EvrakAciklama { aciklama = "" },
                                new EvrakAciklama { aciklama = Req.FiscalID },

                            },
                            satirlar = new List<Satir>(),
                        }
                    }
                }
            };

            foreach (var item in Req.Items)
            {
                int vergiPntr = 5;

                if (item.vergi_orani == 20)
                {
                    vergiPntr = 6;
                }
                else if (item.vergi_orani == 10)
                {
                    vergiPntr = 5;
                }
                else if (item.vergi_orani == 1)
                {
                    vergiPntr = 5;
                }

                int depoNo = 1;
                if (!string.IsNullOrWhiteSpace(item.serialNumbers))
                {
                    if (Req.Banka == "QNB")
                    {
                        depoNo = 9;
                    }
                    else if (Req.Banka == "Denizbank")
                    {
                        depoNo = 10;
                    }
                    else/* if (Req.Bank == "Ziraat Bankası")*/
                    {
                        depoNo = 7;
                    }
                }

                root.Mikro.evraklar.FirstOrDefault().satirlar.Add(new Satir
                {
                    sip_tarih = DateTime.Now.ToString("dd.MM.yyyy"),
                    sip_tip = "0",
                    sip_cins = "0",
                    sip_doviz_cinsi = item.sip_doviz_cinsi,
                    sip_satis_fiyat_doviz_kuru = item.sip_doviz_cinsi,
                    sip_evrakno_seri = "T",
                    sip_musteri_kod = cariNo,
                    sip_stok_kod = item.ProductCode,
                    sip_belgeno = Req.FiscalID,
                    sip_b_fiyat = item.PaymentAmount / item.LeadQuantity,
                    sip_miktar = item.LeadQuantity,
                    sip_birim_pntr = item.LeadQuantity,
                    sip_tutar = Req.PaymentAmount,
                    sip_vergi_pntr = vergiPntr,
                    sip_depono = depoNo,
                    sip_vergisiz_fl = false,
                    sip_stok_sormerk = string.IsNullOrWhiteSpace(Req.QRCode) ? "" : "INT-PAYSER GELİRLERİ",
                    seriler = item.serialNumbers,
                    sip_harekettipi = item.HareketTipi,
                    sip_special1 = Req.Banka,
                    sip_special3 = Req.QRCode,
                    sip_eticaret_kanal_kodu = item.sip_eticaret_kanal_kodu,
                    sip_HareketGrupKodu1 = string.Format("{0} {1}", Req.FirstName, Req.LastName),
                    sip_HareketGrupKodu2 = Req.CellularPhone,
                    sip_satici_kod = Req.sip_satici_kod,
                    sip_ExternalProgramId = string.Format("{0},{1},", item.CampaignType ?? "", item.HurdaSeriNo ?? ""),
                    user_tablo = new List<UserTablo>
                                    {
                                        new UserTablo { aciklama = "Pax 360 Satış" }
                                    },
                });
            }

            var json = JsonSerializer.Serialize(root);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync("http://localhost:8094/api/APIMethods/SiparisKaydetV2", data);

            string result = response.Content.ReadAsStringAsync().Result;
            var resultModel = JsonSerializer.Deserialize<SiparisKaydetResult>(result);

            ServisObjeleriniLogla(root, "SiparisKaydetV2");
            ServisObjeleriniLogla(resultModel, "SiparisKaydetV2");

            if (resultModel != null && resultModel.result != null && resultModel.result.FirstOrDefault() != null)
            {
                if (!resultModel.result.FirstOrDefault().IsError)
                {
                    return Tuple.Create(true, string.Empty, string.Empty);
                    // mikro güncellemesi sonrası hata oluştuğu için kapatıldı cariharguid dönmüyor return Tuple.Create(true, string.Empty, resultModel.result.FirstOrDefault().Data.list.FirstOrDefault().cariHarGuid.ToString());
                }
                else
                {
                    return Tuple.Create(false, resultModel.result.FirstOrDefault().ErrorMessage + " " + _cryptographyHelper.CreateMD5(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd"), _mikroConfig.Value.Sifre)), string.Empty);
                }
            }
            else
            {
                return Tuple.Create(false, "Result is null", string.Empty);
            }

        }

        public async Task<Tuple<bool, string, string>> SiparisDuzeltV2(ReqCreateLeadV2 Req, string cariNo, string sip_Guid)
        {
            var root = new CreateLeadSiparisKaydetModel
            {
                Mikro = new MikroSiparis
                {
                    FirmaKodu = _mikroConfig.Value.FirmaKodu,
                    CalismaYili = Convert.ToInt32(_mikroConfig.Value.CalismaYili),
                    ApiKey = _mikroConfig.Value.ApiKey,
                    KullaniciKodu = _mikroConfig.Value.KullaniciKodu,
                    Sifre = _cryptographyHelper.CreateMD5(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd"), _mikroConfig.Value.Sifre)),
                    FirmaNo = Convert.ToInt32(_mikroConfig.Value.FirmaNo),
                    SubeNo = Convert.ToInt32(_mikroConfig.Value.SubeNo),
                    evraklar = new List<Evrak>
                    {
                        new Evrak
                        {
                            evrak_aciklamalari = new List<EvrakAciklama>
                            {
                                new EvrakAciklama { aciklama = "Interpay Online Satış (Evrak Açıklama)" },
                                new EvrakAciklama { aciklama = "" },
                                new EvrakAciklama { aciklama = "" },
                                new EvrakAciklama { aciklama = "" },
                                new EvrakAciklama { aciklama = "" },
                                new EvrakAciklama { aciklama = Req.FiscalID },

                            },
                            satirlar = new List<Satir>(),
                        }
                    }
                }
            };

            foreach (var item in Req.Items)
            {
                int vergiPntr = 5;

                if (item.vergi_orani == 20)
                {
                    vergiPntr = 6;
                }
                else if (item.vergi_orani == 10)
                {
                    vergiPntr = 5;
                }
                else if (item.vergi_orani == 1)
                {
                    vergiPntr = 5;
                }

                int depoNo = 1;
                if (!string.IsNullOrWhiteSpace(item.serialNumbers))
                {
                    if (Req.Banka == "QNB")
                    {
                        depoNo = 9;
                    }
                    else if (Req.Banka == "Denizbank")
                    {
                        depoNo = 10;
                    }
                    else/* if (Req.Bank == "Ziraat Bankası")*/
                    {
                        depoNo = 7;
                    }
                }

                string guid = string.Empty;
                var guidResult = GetMikroSipGuid(sip_Guid);

                if (string.IsNullOrWhiteSpace(guidResult.Item1))
                {
                    guid = guidResult.Item2;
                }
                else
                {
                    return Tuple.Create(false, guidResult.Item1, string.Empty);
                }

                root.Mikro.evraklar.FirstOrDefault().satirlar.Add(new Satir
                {
                    sip_Guid = guid,
                    sip_tarih = DateTime.Now.ToString("dd.MM.yyyy"),
                    sip_tip = "0",
                    sip_cins = "0",
                    sip_doviz_cinsi = item.sip_doviz_cinsi,
                    sip_satis_fiyat_doviz_kuru = item.sip_doviz_cinsi,
                    sip_evrakno_seri = "T",
                    sip_musteri_kod = cariNo,
                    sip_stok_kod = item.ProductCode,
                    sip_belgeno = Req.FiscalID,
                    sip_b_fiyat = item.PaymentAmount / item.LeadQuantity,
                    sip_miktar = item.LeadQuantity,
                    sip_birim_pntr = item.LeadQuantity,
                    sip_tutar = Req.PaymentAmount,
                    sip_vergi_pntr = vergiPntr,
                    sip_depono = depoNo,
                    sip_vergisiz_fl = false,
                    sip_stok_sormerk = string.IsNullOrWhiteSpace(Req.QRCode) ? "" : "INT-PAYSER GELİRLERİ",
                    seriler = item.serialNumbers,
                    sip_harekettipi = item.HareketTipi,
                    sip_special1 = Req.Banka,
                    sip_special3 = Req.QRCode,
                    sip_eticaret_kanal_kodu = item.sip_eticaret_kanal_kodu,
                    sip_HareketGrupKodu1 = string.Format("{0} {1}", Req.FirstName, Req.LastName),
                    sip_HareketGrupKodu2 = Req.CellularPhone,
                    sip_satici_kod = Req.sip_satici_kod, /////// ONLİNE ÖDEMESİ ALINMIŞ SİPARİŞLERDE GÖRİLECEK HAVAEDE GÖNDEİRLMEYECEK. WL DE YAZACAK
                    sip_ExternalProgramId = string.Format("{0},{1},", item.CampaignType ?? "", item.HurdaSeriNo ?? ""),
                    user_tablo = new List<UserTablo>
                                    {
                                        new UserTablo { aciklama = "Pax 360 Satış" }
                                    },
                });
            }

            var json = JsonSerializer.Serialize(root);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync("http://localhost:8094/api/APIMethods/SiparisDuzeltV2", data);

            string result = response.Content.ReadAsStringAsync().Result;
            var resultModel = JsonSerializer.Deserialize<SiparisKaydetResult>(result);

            ServisObjeleriniLogla(root, "SiparisDuzeltV2");
            ServisObjeleriniLogla(resultModel, "SiparisDuzeltV2");

            if (resultModel != null && resultModel.result != null && resultModel.result.FirstOrDefault() != null)
            {
                if (!resultModel.result.FirstOrDefault().IsError)
                {
                    return Tuple.Create(true, string.Empty, string.Empty);
                    // mikro güncellemesi sonrası hata oluştuğu için kapatıldı cariharguid dönmüyor return Tuple.Create(true, string.Empty, resultModel.result.FirstOrDefault().Data.list.FirstOrDefault().cariHarGuid.ToString());
                }
                else
                {
                    return Tuple.Create(false, resultModel.result.FirstOrDefault().ErrorMessage + " " + _cryptographyHelper.CreateMD5(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd"), _mikroConfig.Value.Sifre)), string.Empty);
                }
            }
            else
            {
                return Tuple.Create(false, "Result is null", string.Empty);
            }

        }
        public string UpdateMikroOrderDurum(OrderListModel dataModel)
        {
            using (SqlConnection connection = new SqlConnection(_externalConfig.Value.MicroConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command2 = connection.CreateCommand();
                    command2.CommandText = "UPDATE [SIPARISLER] SET sip_special1 = @DURUM WHERE [sip_belgeno] = @ID";
                    command2.Parameters.AddWithValue("@ID", dataModel.ID);
                    command2.Parameters.AddWithValue("@DURUM", dataModel.SelectedDurum);
                    command2.ExecuteNonQuery();
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public Tuple<string, string> GetMikroSipGuid(string sip_Belge_no)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_externalConfig.Value.MicroConnectionString))
                {
                    connection.Open();
                    try
                    {
                        string query = @"SELECT TOP (1) [sip_Guid] FROM [SIPARISLER] where sip_belgeno=@SIPBELGENO";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@SIPBELGENO", sip_Belge_no);
                        SqlDataReader reader = command.ExecuteReader();
                        string sip_guid = string.Empty;
                        while (reader.Read())
                        {

                            if (reader[0] != null)
                            {
                                sip_guid = reader["sip_Guid"]?.ToString();
                            }
                            else
                            {
                                return Tuple.Create("Sipariş Belge No Bulunamadı", string.Empty);
                            }
                        }

                        return Tuple.Create(string.Empty, sip_guid);

                    }
                    catch (Exception ex)
                    {
                        return Tuple.Create(ex.Message, string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, string.Empty);
            }
        }

        private static void ServisObjeleriniLogla(object Obj, string fileName)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                XmlSerializer ser = new XmlSerializer(Obj.GetType());
                ser.Serialize(stream, Obj);
                stream.Position = 0;

                StringBuilder str = new StringBuilder();
                str.AppendLine("-----------------------------------------------------------------------------");
                str.AppendLine(string.Format("{0}:{1}", "Zaman", DateTime.Now.ToString()));
                str.AppendLine(Encoding.UTF8.GetString(stream.ToArray()));
                str.AppendLine("-----------------------------------------------------------------------------");
                DateTime date = DateTime.Now;
                string path = string.Format("{0}\\{1}\\{2}\\{3}\\{4}", "wwwroot", "LogFilesMikro", date.Year, date.Month, date.Day);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream fs = File.Open(path + string.Format("{0}{1}{2}", "\\", fileName, ".txt"), FileMode.Append, FileAccess.Write, FileShare.None))
                using (StreamWriter sw = new StreamWriter(fs)) sw.WriteLine(str);

            }
            catch
            {
            }
        }

        public Tuple<string, List<CustomerListItemModel>> GetMikroCompaniesWithPax360(CustomerListModel dataModel, int skip, int take)
        {
            List<CustomerListItemModel> returnList = new List<CustomerListItemModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_externalConfig.Value.MicroConnectionString))
                {
                    connection.Open();
                    try
                    {
                        string query = @"SELECT [cari_Guid],[cari_unvan1],[cari_vdaire_no] FROM [CARI_HESAPLAR] 
                        WHERE [cari_unvan1]!=''";

                        if (!string.IsNullOrWhiteSpace(dataModel.MusteriAdi))
                        {
                            query += " AND [cari_unvan1] like @CARIUNVAN";
                        }

                        query += @" ORDER BY [cari_Guid]
                        OFFSET @SKIP ROWS
                        FETCH NEXT @TAKE ROWS ONLY;";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@SKIP", skip);
                        command.Parameters.AddWithValue("@TAKE", take);

                        if (!string.IsNullOrWhiteSpace(dataModel.MusteriAdi))
                        {
                            command.Parameters.AddWithValue("@CARIUNVAN", "%" + dataModel.MusteriAdi + "%");
                        }

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader[0] != null)
                            {
                                returnList.Add(new CustomerListItemModel
                                {
                                    MusteriAdi = reader["cari_unvan1"]?.ToString(),
                                    Cari_Guid = Guid.Parse(reader["cari_Guid"]?.ToString()),
                                });
                            }
                        }

                        return Tuple.Create(string.Empty, returnList);

                    }
                    catch (Exception ex)
                    {
                        return Tuple.Create(ex.Message, returnList);
                    }
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, returnList);
            }
        }

        public Tuple<string, int> GetMikroCompaniesWithPax360Count()
        {
            int count = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_externalConfig.Value.MicroConnectionString))
                {
                    connection.Open();
                    try
                    {
                        string query = @"SELECT COUNT([cari_Guid]) FROM [CARI_HESAPLAR]";

                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader[0] != null)
                            {
                                count = Convert.ToInt32(reader[0].ToString());
                            }
                        }

                        return Tuple.Create(string.Empty, count);

                    }
                    catch (Exception ex)
                    {
                        return Tuple.Create(ex.Message, count);
                    }
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, count);
            }
        }
    }
}

