using Microsoft.AspNetCore.Mvc.Rendering;
using Pax360DAL.Models;
using Pax360DAL;

namespace Pax360.Helpers
{
    public class UserHelper
    {
        private Context db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHelper(Context _db, IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            _httpContextAccessor = httpContextAccessor;

            if (string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Session.GetString("USERID")) || Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("USERID").ToString()) == 0)
            {
                _httpContextAccessor.HttpContext.Response.Redirect("/Account/Logoff");
            }
        }

        public List<SelectListItem> RoleList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var roles = db.RoleTypes.ToList();

            list.Add(new SelectListItem
            {
                Text = "Seçiniz",
                Value = ""
            });


            foreach (var item in roles)
            {
                list.Add(new SelectListItem
                {
                    Text = item.RoleName,
                    Value = item.RoleName,
                });
            }


            return list;
        }
        public List<RoleTypes> RoleTypeList()
        {
            List<RoleTypes> list = new List<RoleTypes>();

            var roles = db.RoleTypes.ToList();

            foreach (var item in roles)
            {
                list.Add(item);
            }

            return list;
        }

        public List<SelectListItem> RoleListWithID()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var roles = db.RoleTypes.ToList();

            list.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });


            foreach (var item in roles)
            {
                list.Add(new SelectListItem
                {
                    Text = item.RoleName,
                    Value = item.ID.ToString(),
                });
            }


            return list;
        }



        public List<SelectListItem> UserRelationUser()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            int userid = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("USERID"));
            var screenUser = db.Users.FirstOrDefault(ok => ok.ID == userid);

            IQueryable<Users> query = db.Users.Where(ok => ok.ID > 0);

            var users = query.ToList();

            list.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });


            foreach (var item in users)
            {
                list.Add(new SelectListItem
                {
                    Text = item.NameSurname,
                    Value = item.ID.ToString()
                });
            }
            if (screenUser != null &&
                !string.IsNullOrEmpty(screenUser.NameSurname))
            {
                list.Add(new SelectListItem
                {
                    Text = screenUser.NameSurname,
                    Value = screenUser.ID.ToString()
                });
            }
            return list;
        }

        public List<SelectListItem> AuthPersons()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            IQueryable<Users> query = db.Users.Where(ok => ok.Role == "Müşteri Temsilcisi");

            var users = query.ToList();

            list.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });


            foreach (var item in users)
            {
                list.Add(new SelectListItem
                {
                    Text = item.NameSurname,
                    Value = string.Format("{0}#{1}", item.ID.ToString(), item.NameSurname),
                });
            }

            return list;
        }

        public List<SelectListItem> GetAllUsers()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            var users = db.Users.ToList();

            list.Add(new SelectListItem
            {
                Text = "Select",
                Value = ""
            });


            foreach (var item in users)
            {
                list.Add(new SelectListItem
                {
                    Text = item.NameSurname,
                    Value = item.ID.ToString()
                });
            }

            return list;
        }

    }
}
