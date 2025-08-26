using Microsoft.EntityFrameworkCore;
using Pax360.Interfaces;
using Pax360.Models;
using Pax360DAL.Models;
using Pax360DAL;
using System.Net;
using System.Text.Json;
using Pax360.Extensions;
using System;

namespace Pax360.Helpers
{
    public class OfferService : IOfferService
    {
        private readonly Context db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMikroHelper _mikroService;
        private readonly int userID;
        private readonly string nameSurname;


        public OfferService(Context _db, IHttpContextAccessor httpContextAccessor, IMikroHelper mikroService)
        {
            db = _db;
            _httpContextAccessor = httpContextAccessor;
            _mikroService = mikroService;

            userID = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("USERID"));
            nameSurname = _httpContextAccessor.HttpContext.Session.GetString("NAMESURNAME");
        }

        public async Task<string> SaveOffer(OfferDetailsModel dataModel)
        {
            try
            {
                var offer = new Offers
                {
                    TeklifSartlari = dataModel.TeklifSartlari,
                    TeklifStatus = dataModel.TeklifStatus,
                    cari_kod = dataModel.cari_kod,
                    cari_Guid = dataModel.cari_Guid,
                    MusteriAdi = dataModel.MusteriAdi,
                    TeklifTarihi = DateTime.Now,
                    UserID = userID,
                    UserName = nameSurname,
                    OfferItems = new List<OffersItem>()
                };

                List<OfferInputModel> listOffer = _httpContextAccessor.HttpContext.Session.GetObject<List<OfferInputModel>>("OFFERINPUT") ?? new List<OfferInputModel>();

                foreach (var offerItem in listOffer)
                {
                    offer.OfferItems.Add(new OffersItem
                    {
                        Adet = offerItem.adet,
                        UrunAdi = offerItem.adi,
                        Fiyat = offerItem.fiyat,
                        UrunKodu = ""
                    });
                }

                db.Offers.Add(offer);
                await db.SaveChangesAsync();

                _httpContextAccessor.HttpContext.Session.SetObject("OFFERINPUT", new List<OfferInputModel>());
                List<OfferInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OfferInputModel>>("OFFERINPUT") ?? new List<OfferInputModel>();

                return string.Empty;

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public async Task<Tuple<string, OfferDetailsModel>> GetOffer(int orderID)
        {
            try
            {
                var offer = await db.Offers.Include(ok => ok.OfferItems).FirstOrDefaultAsync(ok => ok.ID == orderID);

                if (offer != null)
                {
                    OfferDetailsModel model = new OfferDetailsModel()
                    {
                        TeklifSartlari = offer.TeklifSartlari,
                        TeklifStatus = offer.TeklifStatus,
                        cari_kod = offer.cari_kod,
                        cari_Guid = offer.cari_Guid,
                        MusteriAdi = offer.MusteriAdi,
                    };

                    List<OfferInputModel> kalemList = new List<OfferInputModel>();
                    int i = 1;
                    foreach (var item in offer.OfferItems)
                    {
                        kalemList.Add(new OfferInputModel
                        {
                            sira = i,
                            adet = item.Adet,
                            adi = item.UrunAdi,
                            fiyat = item.Fiyat,
                        });

                        i++;
                    }

                    _httpContextAccessor.HttpContext.Session.SetObject("OFFERINPUT", kalemList);

                    return Tuple.Create(string.Empty, model);
                }
                else
                {
                    return Tuple.Create("Sipariş Bulunamadı!", new OfferDetailsModel());
                }

            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, new OfferDetailsModel());
            }
        }

        public async Task<string> UpdateOffer(OfferDetailsModel dataModel)
        {
            try
            {
                List<OfferDetailsModel> kalemler = _httpContextAccessor.HttpContext.Session.GetObject<List<OfferDetailsModel>>("OFFERINPUT") ?? new List<OfferDetailsModel>();
                decimal total = 0;

                var offer = db.Offers.FirstOrDefault(ok => ok.ID == dataModel.selectedID);

                if (offer == null)
                {
                    return "Teklif Bulunamadı! ID:" + dataModel.selectedID;
                }

                offer.TeklifSartlari = dataModel.TeklifSartlari;
                offer.TeklifStatus = dataModel.TeklifStatus;
                offer.cari_kod = dataModel.cari_kod;
                offer.cari_Guid = dataModel.cari_Guid;
                offer.MusteriAdi = dataModel.MusteriAdi;

                offer.OfferItems = new List<OffersItem>();

                foreach (var item in kalemler)
                {
                    offer.OfferItems.Add(new OffersItem
                    {
                        Adet = item.Adet,
                        UrunAdi = item.UrunAdi,
                        Fiyat = item.Fiyat,
                    });
                }

                db.Offers.Attach(offer);
                db.Entry(offer).State = EntityState.Modified;
                db.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
    }
}
