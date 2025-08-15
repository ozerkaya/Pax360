using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(500)]
        public string? TeklifStatus { get; set; }
        [MaxLength(500)]
        public string? TeklifSartlari { get; set; }
        [MaxLength(500)]
        public string? MusteriAdi { get; set; }
        [MaxLength(500)]
        public string? cari_kod { get; set; }
        public Guid cari_Guid { get; set; }
        public ICollection<OffersItem> OfferItems { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime TeklifTarihi { get; set; }
    }
}
