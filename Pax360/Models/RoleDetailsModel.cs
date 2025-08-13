using Microsoft.AspNetCore.Mvc.Rendering;
using Pax360.Interfaces;
using System.Text.Json.Serialization;

namespace Pax360.Models
{
    public class RoleDetailsModel : IMessage
    {
        public RoleDetailsModel()
        {
            List = new List<RoleItem>();

            RoleList = new List<SelectListItem>();
            UserList = new List<SelectListItem>();
        }
        [JsonRequired]
        public int ID { get; set; }
        [JsonRequired]
        public int AuthID { get; set; }
        public string RoleName { get; set; }
        [JsonRequired]
        public int NewRole { get; set; }
        [JsonRequired]
        public bool Module_Users { get; set; }
        [JsonRequired]
        public bool Module_Role { get; set; }
        [JsonRequired]
        public bool Module_Order { get; set; }





        public List<RoleItem> List { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public List<SelectListItem> RoleList { get; set; }
        public List<SelectListItem> UserList { get; set; }
        public string Role { get; set; }
        public string User { get; set; }
    }

    public class RoleItem
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public bool Module_Users { get; set; }
        public bool Module_Role { get; set; }
        public bool Module_Order { get; set; }

    }
}
