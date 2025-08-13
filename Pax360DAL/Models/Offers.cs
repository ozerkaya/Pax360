using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pax360DAL.Models
{
    [Table("Offers")]
    public class Offers
    {
        public int ID { get; set; }
        public string TeklifStatus { get; set; }
        public string TeklifSartlari { get; set; }
        public string MusteriAdi { get; set; }
        public string cari_kod { get; set; }
        public Guid cari_Guid { get; set; }
        public ICollection<OffersItem> OfferItems { get; set; }
    }
}
