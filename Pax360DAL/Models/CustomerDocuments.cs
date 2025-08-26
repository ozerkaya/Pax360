using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pax360DAL.Models
{
    [Table("CustomerDocuments")]
    public class CustomerDocuments
    {
        public int ID { get; set; }
        public string DocumentName { get; set; }
        public Customers Customer { get; set; }
        public int CustomerID { get; set; }
    }
}
