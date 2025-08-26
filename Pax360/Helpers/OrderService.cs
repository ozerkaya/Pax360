using Microsoft.EntityFrameworkCore;
using Pax360.Interfaces;
using Pax360.Models;
using Pax360DAL.Models;
using Pax360DAL;
using System.Net;
using System.Text.Json;
using Pax360.Extensions;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pax360.Helpers
{
    public class OrderService : IOrderService
    {
        private readonly Context db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMikroHelper _mikroService;
        private readonly int userID;
        private readonly string nameSurname;


        public OrderService(Context _db, IHttpContextAccessor httpContextAccessor, IMikroHelper mikroService)
        {
            db = _db;
            _httpContextAccessor = httpContextAccessor;
            _mikroService = mikroService;

            userID = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("USERID"));
            nameSurname = _httpContextAccessor.HttpContext.Session.GetString("NAMESURNAME");
        }

        public async Task<string> SaveOrder(OrderDetailsModel dataModel)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var request = new ReqCreateLeadV2
                    {
                        FiscalID = dataModel.SiparisNumarasi.ToString(),
                        FirstName = dataModel.AdSoyad,
                        LastName = dataModel.AdSoyad,

                        ShippingAddress = (dataModel.DifferentCargoAddress) ? dataModel.TeslimatAdresi : dataModel.FaturaAdresi,
                        ShippingCityCode = (dataModel.DifferentCargoAddress) ? StaticHelpers.CityList(dataModel.TeslimatIl) : StaticHelpers.CityList(dataModel.Il),
                        ShippingDistrict = (dataModel.DifferentCargoAddress) ? dataModel.TeslimatIlce : dataModel.Ilce,
                        ShippingCounty = "Türkiye",
                        ShippingPostCode = 0,

                        BillingAddress = dataModel.FaturaAdresi,
                        BillingCityCode = StaticHelpers.CityList(dataModel.Il),
                        BillingDistrict = dataModel.Ilce,
                        BillingCounty = "Türkiye",
                        BillingPostCode = 0,

                        Phone = dataModel.Telefon,
                        CellularPhone = dataModel.Telefon,
                        Email = dataModel.Eposta,
                        Tckn = dataModel.VKNTCKN,
                        Vkn = dataModel.VKNTCKN,
                        TaxOfficeName = string.Empty,
                        CompanyName = dataModel.TicariUnvan,
                        CompanyKnownAs = dataModel.TicariUnvan,
                        sip_satici_kod = _httpContextAccessor.HttpContext.Session.GetString("SIPSATICIKODU"),
                        sip_evrakno_seri = "P"
                    };
                    List<OrderInputModel> kalemler = _httpContextAccessor.HttpContext.Session.GetObject<List<OrderInputModel>>("ORDERINPUT") ?? new List<OrderInputModel>();
                    decimal total = 0;

                    var order = new Orders
                    {
                        AdSoyad = dataModel.AdSoyad ?? "",
                        FaturaAdresi = dataModel.FaturaAdresi,
                        MusteriAdi = dataModel.MusteriAdi,
                        SahaFirmasi = dataModel.SahaFirmasi,
                        BankaOrtami = dataModel.BankaOrtami,
                        cari_Guid = dataModel.cari_Guid,
                        cari_kod = dataModel.cari_kod,
                        CihazModu = dataModel.CihazModu,
                        Entegrasyon = dataModel.Entegrasyon,
                        Eposta = dataModel.Eposta,
                        Il = dataModel.Il,
                        Ilce = dataModel.Ilce,
                        SaticiPlasiyer = dataModel.SaticiPlasiyer,
                        Not = dataModel.Not,
                        SiparisNumarasi = dataModel.SiparisNumarasi,
                        SiparisDurumu = "Sipariş Oluşturuldu",
                        Telefon = dataModel.Telefon,
                        SiparisMusterisi = string.Empty,
                        SiparisMusterisi_cari_Guid = default(Guid),
                        TeslimatAdresi = (dataModel.DifferentCargoAddress) ? dataModel.TeslimatAdresi : dataModel.FaturaAdresi,
                        TeslimatIl = (dataModel.DifferentCargoAddress) ? dataModel.TeslimatIl : dataModel.Il,
                        TeslimatIlce = (dataModel.DifferentCargoAddress) ? dataModel.TeslimatIlce : dataModel.Ilce,
                        TeslimTuru = dataModel.TeslimTuru,

                        TicariUnvan = dataModel.TicariUnvan,
                        VadeTarihi = dataModel.VadeTarihi,
                        YuklenecekBanka = string.Join("#", dataModel.YuklenecekBanka),
                        VKNTCKN = dataModel.VKNTCKN,
                        SiparisTipi = dataModel.SiparisTipi,
                        SiparisTarihi = DateTime.Now,
                        UserID = userID,
                        UserName = nameSurname,
                        OrderItems = new List<OrdersItem>()
                    };

                    if (!string.IsNullOrWhiteSpace(dataModel.SiparisMusterisi))
                    {
                        order.SiparisMusterisi = dataModel.SiparisMusterisi.Split('#')[1];
                        order.SiparisMusterisi_cari_Guid = Guid.Parse(dataModel.SiparisMusterisi.Split('#')[0]);
                    }

                    foreach (var item in kalemler)
                    {
                        request.Items.Add(new CreateLeadV2Item
                        {
                            PaymentAmount = (item.doviz == "USD") ? Convert.ToDecimal(item.birimfiyat * item.miktar) : Convert.ToDecimal(item.birimfiyattl * item.miktar),
                            LeadQuantity = item.miktar,
                            ProductCode = item.cihazmodeli.Split('#')[0],
                            HareketTipi = 0,
                            sip_eticaret_kanal_kodu = "PAX360SATIS",
                            vergi_orani = Convert.ToInt32(item.kdv),
                            sip_doviz_cinsi = (item.doviz == "USD") ? 1 : 0,
                        });

                        total += (item.doviz == "USD") ? Convert.ToDecimal(item.birimfiyat * item.miktar) : Convert.ToDecimal(item.birimfiyattl * item.miktar);

                        order.OrderItems.Add(new OrdersItem
                        {
                            BirimFiyat = item.birimfiyat,
                            BirimFiyatTL = item.birimfiyattl,
                            CihazModeli = item.cihazmodeli,
                            Kdv = item.kdv,
                            Miktar = item.miktar,
                            ToplamTutar = total,
                            DovizCinsi = item.doviz,

                        });
                    }

                    db.Orders.Add(order);
                    db.SaveChanges();

                    request.PaymentAmount = total;
                    var result = await _mikroService.SiparisKaydetV2(request, dataModel.cari_kod);

                    if (result.Item1)
                    {
                        _httpContextAccessor.HttpContext.Session.SetObject("ORDERINPUT", new List<OrderInputModel>());
                        List<OrderInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OrderInputModel>>("ORDERINPUT") ?? new List<OrderInputModel>();
                        transaction.Commit();
                        return string.Empty;
                    }
                    else
                    {
                        transaction.Rollback();
                        return result.Item2;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.ToString();
                }
            }
        }

        public async Task<string> UpdateOrderStatus(OrderListModel dataModel)
        {
            try
            {
                Orders order = db.Orders.FirstOrDefault(ok => ok.ID == dataModel.ID);
                order.SiparisDurumu = dataModel.SelectedDurum;

                db.Orders.Attach(order);
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public async Task<Tuple<string, OrderDetailsModel>> GetOrder(int orderID)
        {
            try
            {

                var order = await db.Orders.Include(ok => ok.OrderItems).FirstOrDefaultAsync(ok => ok.ID == orderID);

                if (order != null)
                {

                    OrderDetailsModel model = new OrderDetailsModel()
                    {
                        AdSoyad = order.AdSoyad ?? "",
                        FaturaAdresi = order.FaturaAdresi,
                        MusteriAdi = order.MusteriAdi,
                        TeslimatAdresi = order.TeslimatAdresi,
                        SahaFirmasi = order.SahaFirmasi,
                        BankaOrtami = order.BankaOrtami,
                        cari_Guid = order.cari_Guid,
                        cari_kod = order.cari_kod,
                        CihazModu = order.CihazModu,
                        Entegrasyon = order.Entegrasyon,
                        Eposta = order.Eposta,
                        Il = order.Il,
                        Ilce = order.Ilce,
                        SaticiPlasiyer = order.SaticiPlasiyer,
                        Not = order.Not,
                        SiparisNumarasi = order.SiparisNumarasi,
                        Telefon = order.Telefon,
                        TeslimatIl = order.TeslimatIl,
                        TeslimatIlce = order.TeslimatIlce,
                        TeslimTuru = order.TeslimTuru,
                        TicariUnvan = order.TicariUnvan,
                        VadeTarihi = order.VadeTarihi,
                        YuklenecekBanka = order.YuklenecekBanka.Split('#'),
                        VKNTCKN = order.VKNTCKN,
                        SiparisTipi = order.SiparisTipi,
                        SiparisMusterisi = string.Format("{0}#{1}", order.SiparisMusterisi_cari_Guid, order.SiparisMusterisi),
                    };

                    List<OrderInputModel> kalemList = new List<OrderInputModel>();
                    int i = 1;
                    foreach (var item in order.OrderItems)
                    {
                        kalemList.Add(new OrderInputModel
                        {
                            sira = i,
                            birimfiyat = item.BirimFiyat,
                            birimfiyattl = item.BirimFiyatTL,
                            cihazmodeli = item.CihazModeli,
                            kdv = item.Kdv,
                            miktar = item.Miktar,
                            toplamtutar = item.ToplamTutar.ToString(),
                        });

                        i++;
                    }

                    _httpContextAccessor.HttpContext.Session.SetObject("ORDERINPUT", kalemList);

                    return Tuple.Create(string.Empty, model);
                }
                else
                {
                    return Tuple.Create("Sipariş Bulunamadı!", new OrderDetailsModel());
                }

            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, new OrderDetailsModel());
            }
        }

        public async Task<string> UpdateOrder(OrderDetailsModel dataModel)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var request = new ReqCreateLeadV2
                    {
                        FiscalID = dataModel.SiparisNumarasi.ToString(),
                        FirstName = dataModel.AdSoyad,
                        LastName = dataModel.AdSoyad,

                        ShippingAddress = dataModel.TeslimatAdresi,
                        ShippingCityCode = StaticHelpers.CityList(dataModel.TeslimatIl),
                        ShippingDistrict = dataModel.TeslimatIlce,
                        ShippingCounty = "Türkiye",
                        ShippingPostCode = 0,

                        BillingAddress = dataModel.FaturaAdresi,
                        BillingCityCode = StaticHelpers.CityList(dataModel.Il),
                        BillingDistrict = dataModel.Ilce,
                        BillingCounty = "Türkiye",
                        BillingPostCode = 0,

                        Phone = dataModel.Telefon,
                        CellularPhone = dataModel.Telefon,
                        Email = dataModel.Eposta,
                        Tckn = dataModel.VKNTCKN,
                        Vkn = dataModel.VKNTCKN,
                        TaxOfficeName = string.Empty,
                        CompanyName = dataModel.TicariUnvan,
                        CompanyKnownAs = dataModel.TicariUnvan,
                        sip_satici_kod = _httpContextAccessor.HttpContext.Session.GetString("SIPSATICIKODU"),
                    };
                    List<OrderInputModel> kalemler = _httpContextAccessor.HttpContext.Session.GetObject<List<OrderInputModel>>("ORDERINPUT") ?? new List<OrderInputModel>();
                    decimal total = 0;

                    var order = db.Orders.FirstOrDefault(ok => ok.ID == dataModel.selectedID);

                    if (order == null)
                    {
                        transaction.Rollback();
                        return "Sipariş Buluanmadı! ID:" + dataModel.selectedID;
                    }

                    order.AdSoyad = dataModel.AdSoyad ?? "";
                    order.FaturaAdresi = dataModel.FaturaAdresi;
                    order.MusteriAdi = dataModel.MusteriAdi;
                    order.TeslimatAdresi = dataModel.TeslimatAdresi;
                    order.SahaFirmasi = dataModel.SahaFirmasi;
                    order.BankaOrtami = dataModel.BankaOrtami;
                    order.cari_Guid = dataModel.cari_Guid;
                    order.cari_kod = dataModel.cari_kod;
                    order.CihazModu = dataModel.CihazModu;
                    order.Entegrasyon = dataModel.Entegrasyon;
                    order.Eposta = dataModel.Eposta;
                    order.Il = dataModel.Il;
                    order.Ilce = dataModel.Ilce;
                    order.SaticiPlasiyer = dataModel.SaticiPlasiyer;
                    order.Not = dataModel.Not;
                    order.SiparisNumarasi = dataModel.SiparisNumarasi;
                    order.Telefon = dataModel.Telefon;
                    order.TeslimatIl = dataModel.TeslimatIl;
                    order.TeslimatIlce = dataModel.TeslimatIlce;
                    order.TeslimTuru = dataModel.TeslimTuru;
                    order.TicariUnvan = dataModel.TicariUnvan;
                    order.VadeTarihi = dataModel.VadeTarihi;
                    order.YuklenecekBanka = string.Join("#", order.YuklenecekBanka);
                    order.VKNTCKN = dataModel.VKNTCKN;
                    order.SiparisTipi = dataModel.SiparisTipi;
                    order.SiparisTarihi = DateTime.Now;
                    order.UserID = userID;
                    order.UserName = nameSurname;

                    order.SiparisMusterisi = dataModel.SiparisMusterisi.Split('#')[1];
                    order.SiparisMusterisi_cari_Guid = Guid.Parse(dataModel.SiparisMusterisi.Split('#')[0]);

                    order.OrderItems = new List<OrdersItem>();

                    foreach (var item in kalemler)
                    {
                        request.Items.Add(new CreateLeadV2Item
                        {
                            PaymentAmount = Convert.ToDecimal(item.birimfiyattl * item.miktar),
                            LeadQuantity = item.miktar,
                            ProductCode = item.cihazmodeli.Split('#')[0],
                            HareketTipi = 0,
                            sip_eticaret_kanal_kodu = "PAX360SATIS",
                            vergi_orani = 10,
                            sip_doviz_cinsi = (item.doviz == "USD") ? 1 : 0
                        });

                        total += Convert.ToDecimal(item.birimfiyattl * item.miktar);

                        order.OrderItems.Add(new OrdersItem
                        {
                            BirimFiyat = item.birimfiyat,
                            BirimFiyatTL = item.birimfiyattl,
                            CihazModeli = item.cihazmodeli,
                            Kdv = item.kdv,
                            Miktar = item.miktar,
                            ToplamTutar = total,
                            DovizCinsi = item.doviz
                        });
                    }

                    db.Orders.Attach(order);
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();

                    request.PaymentAmount = total;
                    var result = await _mikroService.SiparisDuzeltV2(request, dataModel.cari_kod, dataModel.SiparisNumarasi);

                    if (result.Item1)
                    {
                        _httpContextAccessor.HttpContext.Session.SetObject("ORDERINPUT", new List<OrderInputModel>());
                        List<OrderInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OrderInputModel>>("ORDERINPUT") ?? new List<OrderInputModel>();
                        transaction.Commit();
                        return string.Empty;
                    }
                    else
                    {
                        transaction.Rollback();
                        return result.Item2;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ex.ToString();
                }
            }
        }
    }
}
