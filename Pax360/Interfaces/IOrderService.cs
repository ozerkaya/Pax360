using Pax360.Models;
using Pax360DAL.Models;

namespace Pax360.Interfaces
{
    public interface IOrderService
    {
        public Task<string> SaveOrder(OrderDetailsModel dataModel);

    }
}
