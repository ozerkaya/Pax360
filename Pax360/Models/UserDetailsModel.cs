using Microsoft.AspNetCore.Mvc.Rendering;
using Pax360.Interfaces;
using Pax360DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pax360.Models
{
    public class UserDetailsModel : IMessage
    {
        public UserDetailsModel()
        {
            List = new List<UserItem>();
            RoleList = new List<SelectListItem>();
            RoleTypeList = new List<RoleTypes>();
            MikroCompanyList = new List<SelectListItem>();
            AuthPersonList = new List<SelectListItem>();
        }
        [JsonRequired]
        public int ID { get; set; }
        [Required]
        public string NameSurname { get; set; }
        [Required]
        public string UserMail { get; set; }
        [Required]
        public string UserPass { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string IsEnable { get; set; }
        public string SipSaticiKodu { get; set; }
        public string TeamName { get; set; }
        public string PhoneNumber { get; set; }
        public string TCKN { get; set; }
        public string MikroCompanyID { get; set; }
        public string AuthPersonID { get; set; }
        public string MikroCompanyName { get; set; }
        public string[] Organizations { get; set; }
        public List<SelectListItem> RoleList { get; set; }
        public List<SelectListItem> MikroCompanyList { get; set; }
        public List<SelectListItem> AuthPersonList { get; set; }
        public List<RoleTypes> RoleTypeList { get; set; }
        public List<UserItem> List { get; set; }
        public List<string> TeamList { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string[] SearchRole { get; set; }
        public string[] SearchTeam { get; set; }
        public string SearchName { get; set; }
        public string SearchMail { get; set; }
        public string Image { get; set; }

    }

    public class UserItem
    {
        public int ID { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsEnable { get; set; }
        public string TeamName { get; set; }
        public string PhoneNumber { get; set; }
        public string Company { get; set; }
        public string AuthPerson { get; set; }
        public string SipSaticiKodu { get; set; }
    }
}
