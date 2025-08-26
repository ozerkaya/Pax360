using Pax360.Models;
using Pax360DAL.Models;

namespace Pax360.Interfaces
{
    public interface ICustomerService
    {
        IQueryable<Customers> CustomerQuery(CustomerListModel dataModel);
        Customers GetCustomer(Guid cari_Guid);
        Task<string> UpdateCustomer(CustomerListModel dataModel);
    }
}
