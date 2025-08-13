using Microsoft.EntityFrameworkCore;
using Pax360.Interfaces;
using Pax360.Models;
using Pax360DAL.Models;
using Pax360DAL;
using System.Net;
using System.Text.Json;
using Pax360.Extensions;

namespace Pax360.Helpers
{
    public class OrderService : IOrderService
    {
        private readonly Context _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMikroHelper _mikroService;
        private readonly int userID;
        private readonly string userRole;
        private readonly string nameSurname;


        public OrderService(Context db, IHttpContextAccessor httpContextAccessor, IMikroHelper mikroService)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _mikroService = mikroService;
        }

        public async Task<string> SaveOrder(OrderDetailsModel dataModel)
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
                foreach (var item in kalemler)
                {
                    request.Items.Add(new CreateLeadV2Item
                    {
                        PaymentAmount = Convert.ToDecimal(item.birimfiyattl * item.miktar),
                        LeadQuantity = item.miktar,
                        ProductCode = item.cihazmodeli,
                        HareketTipi = 0,
                        sip_eticaret_kanal_kodu = "PAX360SATIS",
                        vergi_orani = 10,
                    });

                    total += Convert.ToDecimal(item.birimfiyattl * item.miktar);
                }

                request.PaymentAmount = total;

                var result = await _mikroService.SiparisKaydetV2(request, dataModel.cari_kod);

                if (result.Item1)
                {
                    _httpContextAccessor.HttpContext.Session.SetObject("ORDERINPUT", new List<OrderInputModel>());
                    List<OrderInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OrderInputModel>>("ORDERINPUT") ?? new List<OrderInputModel>();

                    return string.Empty;
                }
                else
                {
                    return result.Item2;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
