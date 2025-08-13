using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pax360DAL.Models
{
    [Table("Teams")]
    public class Teams
    {
        public int ID { get; set; }
        public string TeamName { get; set; }
    }
}
