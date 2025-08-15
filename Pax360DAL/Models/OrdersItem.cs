using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pax360DAL.Models
{
    public class OrdersItem
    {
        public int ID { get; set; }
        public string CihazModeli { get; set; }
        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal BirimFiyatTL { get; set; }
        public string Kdv { get; set; }
        public string Iskonto { get; set; }
        public decimal ToplamTutar { get; set; }
        public Orders Order { get; set; }
        public int OrderID { get; set; }
    }
}
