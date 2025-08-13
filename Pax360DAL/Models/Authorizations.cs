using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pax360DAL.Models
{
    [Table("Authorizations")]
    public class Authorizations
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public bool Module_Users { get; set; }
        public bool Module_Role { get; set; }
        public bool Module_Order { get; set; }
        public bool Module_Offer { get; set; }

    }
}
