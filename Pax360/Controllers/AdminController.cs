using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pax360DAL.Models;
using Pax360DAL;
using System.Net;
using System.Text.Json;
using Pax360.Interfaces;
using Pax360.Models;
using Pax360.Helpers;
using Pax360.Attributes;
using System.Data;

namespace Pax360.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private Context db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminService _adminService;
        private readonly IMikroHelper _mikroService;
        private int userID;
        private string userRole;
        private string nameSurname;
        private string tckn;
        private string takim;
        private string organizations;

        public AdminController(Context _db,
            IHttpContextAccessor httpContextAccessor,
            IAdminService adminService,
            IMikroHelper mikroService)
        {
            db = _db;
            _httpContextAccessor = httpContextAccessor;
            _adminService = adminService;
            _mikroService = mikroService;

            userID = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("USERID"));
            userRole = _httpContextAccessor.HttpContext.Session.GetString("USERROLE");
            nameSurname = _httpContextAccessor.HttpContext.Session.GetString("NAMESURNAME");
            tckn = _httpContextAccessor.HttpContext.Session.GetString("TCKN");
            takim = _httpContextAccessor.HttpContext.Session.GetString("TEAMNAME");
            organizations = _httpContextAccessor.HttpContext.Session.GetString("ORGANIZATIONS");

            if (string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.Session.GetString("USERID")) || Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("USERID").ToString()) == 0)
            {
                _httpContextAccessor.HttpContext.Response.Redirect("/Account/Logoff");
            }
        }
        [HttpGet]
        public IActionResult Users(int id = 0)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Users") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Users").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }
            if (id < 0)
            {
                return BadRequest("Bilinmeyen Kullanıcı ID.");
            }
            UserHelper userHelper = new UserHelper(db, _httpContextAccessor);
            UserDetailsModel model = new UserDetailsModel();
            if (id != 0)
            {
                Users user = db.Users.FirstOrDefault(ok => ok.ID == id);
                if (user == null)
                {
                    return NotFound("Kullanıcı Bulunamadı.");
                }
                model = _adminService.EditGetUser(user);
            }
            else
            {
                model.List = _adminService.UsersList();
            }

            model.TeamList = _adminService.TeamsList();
            model.RoleList = userHelper.RoleList();
            model.RoleTypeList = userHelper.RoleTypeList();
            model.AuthPersonList = userHelper.AuthPersons();

            var mikroCompanies = _mikroService.GetMikroCompanies();
            if (string.IsNullOrWhiteSpace(mikroCompanies.Item1))
            {
                model.MikroCompanyList = mikroCompanies.Item2;
            }
            else
            {
                model.ErrorMessage = "Kurumlar Bulunamadı.";
            }

            return View(model);
        }

        [HttpPost]
        [ReadableBodyStream]
        public IActionResult Users(UserDetailsModel dataModel, string operation, IFormFileCollection images)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Users") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Users").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }
            UserHelper userHelper = new UserHelper(db, _httpContextAccessor);
            UserDetailsModel model = new UserDetailsModel();

            switch (operation)
            {
                case "Save":
                    string checkSave = UserRequiredCheck(dataModel, true);
                    if (!string.IsNullOrWhiteSpace(checkSave))
                    {
                        model.ErrorMessage = checkSave;
                    }
                    else
                    {
                        string resultSave = _adminService.SaveUser(dataModel, images);
                        if (string.IsNullOrWhiteSpace(resultSave))
                        {
                            model.SuccessMessage = "Kaydetme başarılı.";
                            ModelState.Clear();
                        }
                        else
                        {
                            model.ErrorMessage = "Kaydeme başarısız! " + resultSave;
                        }
                    }
                    break;
                case "Update":
                    string checkUpdate = UserRequiredCheck(dataModel, false);
                    if (!string.IsNullOrWhiteSpace(checkUpdate))
                    {
                        model.ErrorMessage = checkUpdate;
                        model.ID = dataModel.ID;

                    }
                    else
                    {
                        if (_adminService.EditSetUser(dataModel, images))
                        {
                            model.SuccessMessage = "Güncelleme başarılı.";
                            ModelState.Clear();
                        }
                        else
                        {
                            model.ErrorMessage = "Güncelleme başarısız!";
                        }
                    }
                    break;
                case "Remove":

                    if (_adminService.RemoveUser(dataModel))
                    {
                        model.SuccessMessage = "Silme başarılı.";
                        ModelState.Clear();
                    }
                    else
                    {
                        model.ErrorMessage = "Silme başarısız!";
                        model.ID = dataModel.ID;
                    }

                    break;
                case "Search":
                    List<Users> list = _adminService.SearchedUsers(dataModel);
                    model = _adminService.SearchedUsersList(list);
                    model.TeamList = _adminService.TeamsList();
                    model.RoleList = userHelper.RoleList();
                    model.RoleTypeList = userHelper.RoleTypeList();
                    model.AuthPersonList = userHelper.AuthPersons();

                    var mikroCompanies = _mikroService.GetMikroCompanies();
                    if (string.IsNullOrWhiteSpace(mikroCompanies.Item1))
                    {
                        model.MikroCompanyList = mikroCompanies.Item2;
                    }
                    else
                    {
                        model.ErrorMessage = "Kullanıcı Bulunamadı.";
                    }
                    return View(model);
                default:
                    break;
            }

            model.List = _adminService.UsersList();
            model.TeamList = _adminService.TeamsList();
            model.RoleList = userHelper.RoleList();
            model.RoleTypeList = userHelper.RoleTypeList();
            model.AuthPersonList = userHelper.AuthPersons();

            var mikroCompanies2 = _mikroService.GetMikroCompanies();
            if (string.IsNullOrWhiteSpace(mikroCompanies2.Item1))
            {
                model.MikroCompanyList = mikroCompanies2.Item2;
            }
            else
            {
                model.ErrorMessage = "Kullanıcı Bulunamadı.";
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Roles(int id = 0, int role = 0, int newRole = 0)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            if ((_httpContextAccessor.HttpContext.Session.GetString("Module_Role") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Role").ToString() == "False") &&
                (_httpContextAccessor.HttpContext.Session.GetString("Module_Role_Users") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Role_Users").ToString() == "False"))
            {
                return RedirectToAction("Logoff", "Account");
            }
            if (id < 0 || role < 0 || newRole < 0)
            {
                return BadRequest("Bilinmeyen parametre.");
            }
            UserHelper userHelper = new UserHelper(db, _httpContextAccessor);
            RoleDetailsModel model = new RoleDetailsModel();

            if (id != 0 || role != 0)
            {
                if (role != 0)
                {
                    RoleTypes editrole = db.RoleTypes.FirstOrDefault(ok => ok.ID == role);
                    model = EditGetRole(editrole);
                    model.Role = role.ToString();

                    model.ID = role;
                    model.AuthID = 0;
                }
                else if (id != 0)
                {
                    Authorizations edituser = db.Authorizations.FirstOrDefault(ok => ok.UserID == id);
                    model = EditGetRoleUser(edituser);
                    model.User = id.ToString();

                    model.ID = 0;
                    model.AuthID = id;
                }
                ModelState.Clear();
            }
            else
            {
                model.List = RolesList();
            }

            model.NewRole = newRole;
            model.UserList = userHelper.UserRelationUser();
            model.RoleList = userHelper.RoleListWithID();
            return View(model);
        }

        [HttpPost]
        [ReadableBodyStream]
        public IActionResult Roles(RoleDetailsModel dataModel, string operation)
        {
            if ((_httpContextAccessor.HttpContext.Session.GetString("Module_Role") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Role").ToString() == "False") &&
                 (_httpContextAccessor.HttpContext.Session.GetString("Module_Role_Users") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Role_Users").ToString() == "False"))
            {
                return RedirectToAction("Logoff", "Account");
            }

            UserHelper userHelper = new UserHelper(db, _httpContextAccessor);
            RoleDetailsModel model = new RoleDetailsModel();

            switch (operation)
            {
                case "Save":
                    string checkSave = RoleRequiredCheck(dataModel);
                    if (!string.IsNullOrWhiteSpace(checkSave))
                    {
                        model.ErrorMessage = checkSave;
                    }
                    else
                    {
                        if (SaveRole(dataModel))
                        {
                            model.SuccessMessage = "Kaydetme başarılı.";
                            ModelState.Clear();
                        }
                        else
                        {
                            model.ErrorMessage = "Kaydetme başarısız!";
                        }
                    }
                    break;
                case "Remove":

                    if (RemoveRole(dataModel))
                    {
                        model.SuccessMessage = "Silme başarılı.";
                        ModelState.Clear();
                    }
                    else
                    {
                        model.ErrorMessage = "Silme başarısız!";
                        model.ID = dataModel.ID;
                    }

                    break;
                case "Update":
                    if (dataModel.ID != 0)
                    {
                        if (EditSetRole(dataModel))
                        {
                            model.SuccessMessage = "Güncelleme başarılı.";
                            ModelState.Clear();
                        }
                        else
                        {
                            model.ErrorMessage = "Güncelleme başarısız!";
                        }
                    }
                    else if (dataModel.AuthID != 0)
                    {
                        if (EditSetAuth(dataModel))
                        {
                            model.SuccessMessage = "Güncelleme başarılı.";
                            ModelState.Clear();
                        }
                        else
                        {
                            model.ErrorMessage = "Güncelleme başarısız!";
                        }
                    }
                    else
                    {
                        model.ErrorMessage = "Güncelleme başarısız!";
                    }
                    break;
                default:
                    break;
            }

            model.List = RolesList();
            model.UserList = userHelper.UserRelationUser();
            model.RoleList = userHelper.RoleListWithID();
            return View(model);
        }
        private string UserRequiredCheck(UserDetailsModel dataModel, bool save)
        {
            string result = "";

            if (string.IsNullOrWhiteSpace(dataModel.NameSurname))
            {
                result += "Ad Soyad Zorunlu. ";
            }
            else if (db.Users.Count(ok => ok.NameSurname == dataModel.NameSurname) > 0 && save)
            {
                result += dataModel.NameSurname + " Kullanılamaz. ";
            }
            else if (db.Users.Count(ok => ok.NameSurname == dataModel.NameSurname) > 0 && !save)
            {

                var user = db.Users.FirstOrDefault(ok => ok.NameSurname == dataModel.NameSurname);
                if (user != null && user.ID != dataModel.ID)
                {
                    result += dataModel.NameSurname + " Kullanılamaz. ";
                }
            }
            if (string.IsNullOrWhiteSpace(dataModel.UserMail))
            {
                result += "E-Posta Zorunlu. ";
            }
            else if (db.Users.Count(ok => ok.Username == dataModel.UserMail) > 0 && save)
            {
                result += dataModel.UserMail + " Kullanılamaz. ";
            }
            else if (db.Users.Count(ok => ok.Username == dataModel.UserMail) > 0 && !save)
            {

                var user = db.Users.FirstOrDefault(ok => ok.Username == dataModel.UserMail);
                if (user != null && user.ID != dataModel.ID)
                {
                    result += dataModel.UserMail + " Kullanılamaz. ";
                }
            }

            if (string.IsNullOrWhiteSpace(dataModel.UserPass))
            {
                result += "Şifre Zorunlu. ";
            }

            if (string.IsNullOrWhiteSpace(dataModel.Role))
            {
                result += "Rol Zorunlu. ";
            }
            else if (dataModel.Role == "Müşteri")
            {
                if (string.IsNullOrWhiteSpace(dataModel.AuthPersonID))
                {
                    result += "Müşteri Temsilcisi Zorunlu. ";
                }
                if (string.IsNullOrWhiteSpace(dataModel.MikroCompanyID))
                {
                    result += "Firma Zorunlu. ";
                }
            }
            return result;
        }
        private RoleDetailsModel EditGetRole(RoleTypes role)
        {
            if (role != null)
            {
                RoleDetailsModel model = new RoleDetailsModel
                {
                    ID = role.ID,
                    AuthID = 0,
                    Module_Users = role.Module_Users,
                    Module_Role = role.Module_Role,
                    Module_Order = role.Module_Order,
                    Module_Offer = role.Module_Offer,
                    Module_Customers = role.Module_Customers,

                    List = RolesList()
                };
                return model;
            }
            else
            {
                RoleDetailsModel model = new RoleDetailsModel
                {
                    List = RolesList()
                };
                return model;
            }
        }

        private RoleDetailsModel EditGetRoleUser(Authorizations role)
        {
            if (role != null)
            {
                RoleDetailsModel model = new RoleDetailsModel
                {
                    AuthID = role.ID,
                    ID = 0,
                    Module_Users = role.Module_Users,
                    Module_Role = role.Module_Role,
                    Module_Order = role.Module_Order,
                    Module_Offer = role.Module_Offer,
                    Module_Customers = role.Module_Customers,

                    List = RolesList()
                };
                return model;
            }
            else
            {
                RoleDetailsModel model = new RoleDetailsModel
                {
                    List = RolesList()
                };
                return model;
            }
        }

        private List<RoleItem> RolesList()
        {
            List<RoleItem> list = new List<RoleItem>();
            List<RoleTypes> roles = db.RoleTypes.OrderByDescending(ok => ok.ID).ToList();

            foreach (var item in roles)
            {
                list.Add(new RoleItem
                {
                    ID = item.ID,
                    RoleName = item.RoleName,
                    Module_Users = item.Module_Users,
                    Module_Role = item.Module_Role,
                    Module_Order = item.Module_Order,
                    Module_Offer = item.Module_Offer,
                    Module_Customers = item.Module_Customers,
                });
            }
            return list;
        }
        private string RoleRequiredCheck(RoleDetailsModel dataModel)
        {
            string result = "";

            if (string.IsNullOrWhiteSpace(dataModel.RoleName))
            {
                result += "Role Name Required. ";
            }

            return result;
        }
        private bool SaveRole(RoleDetailsModel dataModel)
        {
            try
            {
                RoleTypes role = new RoleTypes
                {
                    RoleName = dataModel.RoleName,
                    Module_Role = dataModel.Module_Role,
                    Module_Users = dataModel.Module_Users,
                    Module_Order = dataModel.Module_Order,
                    Module_Offer = dataModel.Module_Offer,
                    Module_Customers = dataModel.Module_Customers,
                };
                db.RoleTypes.Add(role);

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool RemoveRole(RoleDetailsModel dataModel)
        {
            try
            {
                RoleTypes role = db.RoleTypes.FirstOrDefault(ok => ok.ID == dataModel.ID);
                if (role != null)
                {
                    db.RoleTypes.Remove(role);
                    db.Entry(role).State = EntityState.Deleted;

                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool EditSetRole(RoleDetailsModel dataModel)
        {
            try
            {
                RoleTypes role = db.RoleTypes.FirstOrDefault(ok => ok.ID == dataModel.ID);
                if (role == null)
                {
                    throw new ArgumentException("The user role with the specified ID was not found");
                }
                role.Module_Users = dataModel.Module_Users;
                role.Module_Role = dataModel.Module_Role;
                role.Module_Order = dataModel.Module_Order;
                role.Module_Offer = dataModel.Module_Offer;
                role.Module_Customers = dataModel.Module_Customers;

                db.RoleTypes.Attach(role);
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();

                var userList = db.Users.Select(ok => new { ok.ID, ok.Role }).Where(ok => ok.Role == role.RoleName).ToList();

                foreach (var item in userList)
                {
                    Authorizations auth = db.Authorizations.FirstOrDefault(ok => ok.UserID == item.ID);
                    if (auth != null)
                    {
                        auth.Module_Users = dataModel.Module_Users;
                        auth.Module_Role = dataModel.Module_Role;
                        auth.Module_Order = dataModel.Module_Order;
                        auth.Module_Offer = dataModel.Module_Offer;
                        auth.Module_Customers = dataModel.Module_Customers;

                        db.Authorizations.Attach(auth);
                        db.Entry(auth).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool EditSetAuth(RoleDetailsModel dataModel)
        {
            try
            {
                Authorizations role = db.Authorizations.FirstOrDefault(ok => ok.UserID == dataModel.AuthID);
                if (role == null)
                {
                    throw new ArgumentException("The user role with the specified ID was not found.");
                }
                role.Module_Role = dataModel.Module_Role;
                role.Module_Users = dataModel.Module_Users;
                role.Module_Order = dataModel.Module_Order;
                role.Module_Offer = dataModel.Module_Offer;
                role.Module_Customers = dataModel.Module_Customers;

                db.Authorizations.Attach(role);
                db.Entry(role).State = EntityState.Modified;

                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        [ReadableBodyStream]
        public IActionResult SaveTeam([FromBody] string teamName)
        {
            if (!ModelState.IsValid)
            {

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                    //Add Logger
                }
            }
            Teams team = new Teams
            {
                TeamName = teamName
            };
            db.Teams.Add(team);

            db.SaveChanges();
            return Json(true);

        }
    }
}
