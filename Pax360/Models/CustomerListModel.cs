using Microsoft.AspNetCore.Mvc.Rendering;
using Pax360DAL.Models;
using X.PagedList;

namespace Pax360.Models
{
    public class CustomerListModel
    {
        public CustomerListModel()
        {
            CustomerList = new List<Customers>();
            MikroCompanyList = new List<SelectListItem>();
            CustomerItemList = new List<CustomerListItemModel>();
            AuthPersonList = new List<SelectListItem>();
        }
        public int ID { get; set; }
        public string SelectedDurum { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public IPagedList PagingMetaData { get; set; }
        public List<SelectListItem> MikroCompanyList { get; set; }
        public List<Customers> CustomerList { get; set; }
        public List<CustomerListItemModel> CustomerItemList { get; set; }
        public List<SelectListItem> AuthPersonList { get; set; }
        public string MusteriAdi { get; set; }
        public Customers Customer { get; set; }
        public string KontakKisiAdSoyad { get; set; }
        public string KontakKisiAdTelefon { get; set; }
        public string KontakKisiAdEmail { get; set; }
        public Guid Cari_Guid { get; set; }


        public string[] KasaFirmasi { get; set; }
        public string[] MusteriBankalari { get; set; }
    }

    public class CustomerListItemModel
    {
        public int ID { get; set; }
        public string MusteriAdi { get; set; }
        public Guid Cari_Guid { get; set; }
    }
}
