using Microsoft.AspNetCore.Mvc.Rendering;
using Pax360DAL.Models;
using X.PagedList;

namespace Pax360.Models
{
    public class OfferListModel
    {
        public OfferListModel()
        {
            OfferList = new List<Offers>();
        }
        public int ID { get; set; }
        public int DetailID { get; set; }
        public string SelectedDurum { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string TeklifNumarasi { get; set; }
        public string Company { get; set; }
        public DateTime TeklifTarihi { get; set; }
        public string TeklifDurumu { get; set; }
        public IPagedList PagingMetaData { get; set; }
        public List<SelectListItem> MikroCompanyList { get; set; }
        public List<Offers> OfferList { get; set; }
        public int Adet { get; set; }
        public string UrunAdi { get; set; }
        public decimal Fiyat { get; set; }
        public string TeklifSartlari { get; set; }
        public string UrunKodu { get; set; }
        public string SelectedCari { get; set; }
    }

}
