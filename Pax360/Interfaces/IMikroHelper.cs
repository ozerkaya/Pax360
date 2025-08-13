using Microsoft.AspNetCore.Mvc.Rendering;
using Pax360.Models;
using Pax360DAL.Models;
using System.Text;
using System.Text.Json;

namespace Pax360.Interfaces
{
    public interface IMikroHelper
    {
        public Tuple<string, List<SelectListItem>> GetMikroCompanies();
        public Tuple<string, OrderDetailsModel> GetMikroCompanyDetails(Guid cari_Guid);
        public Tuple<string, List<OrderListItemModel>> GetMikroOrderList(int page, OrderListModel dataModel);
        public string UpdateMikroOrderDurum(OrderListModel dataModel);
        public Tuple<string, int> GetMikroOrderListCount();
        Task<Tuple<bool, string, string>> SiparisKaydetV2(ReqCreateLeadV2 Req, string cariNo);

    }
}
