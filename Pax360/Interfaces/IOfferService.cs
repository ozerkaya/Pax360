using Pax360.Models;
using Pax360DAL.Models;

namespace Pax360.Interfaces
{
    public interface IOfferService
    {
        public Task<string> SaveOffer(OfferDetailsModel dataModel);

    }
}
