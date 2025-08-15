using Pax360.Models;
using Pax360DAL.Models;

namespace Pax360.Interfaces
{
    public interface IOrderService
    {
        public Task<string> SaveOrder(OrderDetailsModel dataModel);
        public Task<string> UpdateOrderStatus(OrderListModel dataModel);
        public Task<Tuple<string, OrderDetailsModel>> GetOrder(int orderID);
        public Task<string> UpdateOrder(OrderDetailsModel dataModel);
    }
}
