using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Pax360.Interfaces;

namespace Pax360.Models
{
    public class OfferDetailsModel : IMessage
    {
        public int ID { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string UrunAdi { get; set; }
        public string UrunKodu { get; set; }
        public string TeklifStatus { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public string TeklifSartlari { get; set; }
        public List<SelectListItem> MikroCompanyList { get; set; }
        public string SelectedCari { get; set; }
        public string MusteriAdi { get; set; }
        public string cari_kod { get; set; }
        public Guid cari_Guid { get; set; }
        public int selectedID { get; set; }
        public bool IsModify { get; set; }
    }
}
