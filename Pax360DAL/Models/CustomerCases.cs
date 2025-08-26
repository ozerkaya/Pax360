using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pax360DAL.Models
{
    [Table("CustomerCases")]
    public class CustomerCases
    {
        public int ID { get; set; }
        public string CaseCompany { get; set; }
        public Customers Customer { get; set; }
        public int CustomerID { get; set; }
    }
}
