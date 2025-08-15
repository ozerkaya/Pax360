using Microsoft.AspNetCore.Mvc.Rendering;
using Pax360DAL.Models;
using X.PagedList;

namespace Pax360.Models
{
    public class OrderListModel
    {
        public OrderListModel()
        {
            OrderList = new List<Orders>();
            MikroCompanyList = new List<SelectListItem>();
        }
        public int ID { get; set; }
        public string SelectedDurum { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string SiparisNumarasi { get; set; }
        public string Company { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public string SiparisDurumu { get; set; }
        public IPagedList PagingMetaData { get; set; }
        public List<SelectListItem> MikroCompanyList { get; set; }        
        public List<Orders> OrderList { get; set; }
        public int DetailID { get; set; }
        public string SelectedCari { get; set; }
    }

    public class OrderListItemModel
    {
        public string SiparisNumarasi { get; set; }
        public string Company { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public string SiparisDurumu { get; set; }
    }
}
