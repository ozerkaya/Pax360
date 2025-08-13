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
    public class OfferService : IOfferService
    {
        private readonly Context _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMikroHelper _mikroService;
        private readonly int userID;
        private readonly string userRole;
        private readonly string nameSurname;


        public OfferService(Context db, IHttpContextAccessor httpContextAccessor, IMikroHelper mikroService)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _mikroService = mikroService;
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

                _db.Offers.Add(offer);
                await _db.SaveChangesAsync();

                _httpContextAccessor.HttpContext.Session.SetObject("OFFERINPUT", new List<OfferInputModel>());
                List<OfferInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OfferInputModel>>("OFFERINPUT") ?? new List<OfferInputModel>();

                return string.Empty;

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
