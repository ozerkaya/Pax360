using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Pax360.Interfaces;

namespace Pax360.Models
{
    public class OrderDetailsModel : IMessage
    {
        public OrderDetailsModel()
        {
            CityList = new List<SelectListItem>();
            MikroCompanyList = new List<SelectListItem>();
            MikroProductList = new List<SelectListItem>();
        }
        public int ID { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string MusteriAdi { get; set; }
        public string TicariUnvan { get; set; }
        public string VKNTCKN { get; set; }
        public string FaturaAdresi { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string SaticiPlasiyer { get; set; }
        public string TeslimatAdresi { get; set; }
        public string TeslimatIl { get; set; }
        public string TeslimatIlce { get; set; }
        public string SiparisNumarasi { get; set; }
        public Guid cari_Guid { get; set; }
        public string SelectedCari { get; set; }
        public string cari_kod { get; set; }
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }
        public string Telefon { get; set; }
        public string VadeTarihi { get; set; }
        public string TeslimTuru { get; set; }
        public string SahaFirmasi { get; set; }
        public List<SelectListItem> CityList { get; set; }
        public List<SelectListItem> MikroCompanyList { get; set; }
        public List<SelectListItem> MikroProductList { get; set; }
        public string CihazModeli { get; set; }
        public int Adet { get; set; }
        public string BankaOrtami { get; set; }
        public string CihazModu { get; set; }
        public string Entegrasyon { get; set; }
        public string YuklenecekBanka { get; set; }
        public string YuklenecekUygulama { get; set; }
        public string Not { get; set; }
        public decimal Fiyat { get; set; }
        public bool IsModify { get; set; }
        public int selectedID { get; set; }

    }
}
