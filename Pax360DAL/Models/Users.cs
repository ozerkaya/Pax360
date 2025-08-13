using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pax360DAL.Models
{
    [Table("Users")]
    public class Users
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NameSurname { get; set; }
        public string Role { get; set; }
        public bool IsEnable { get; set; }
        public string TeamName { get; set; }
        public string PhoneNumber { get; set; }
        public string TCKN { get; set; }

        public Guid MikroCompanyID { get; set; }
        public string? MikroCompanyName { get; set; }

        public int AuthPersonID { get; set; }
        public string? AuthPersonName { get; set; }
        public string SipSaticiKodu { get; set; }
    }
}
