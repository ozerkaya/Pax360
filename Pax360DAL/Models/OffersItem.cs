using Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pax360DAL.Models
{
    [Table("OffersItem")]
    public class OffersItem    
    {
        public int ID { get; set; }
        public string UrunAdi { get; set; }
        public string UrunKodu { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public Offers Offer { get; set; }
        public int OfferID { get; set; }
    }
}
