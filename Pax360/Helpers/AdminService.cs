using Microsoft.EntityFrameworkCore;
using Pax360.Interfaces;
using Pax360.Models;
using Pax360DAL.Models;
using Pax360DAL;
using System.Net;
using System.Text.Json;

namespace Pax360.Helpers
{
    public class AdminService : IAdminService
    {
        private readonly Context _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly int userID;
        private readonly string userRole;
        private readonly string nameSurname;


        public AdminService(Context db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            userID = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("USERID"));
            userRole = _httpContextAccessor.HttpContext.Session.GetString("USERROLE");
            nameSurname = _httpContextAccessor.HttpContext.Session.GetString("NAMESURNAME");
        }

        public UserDetailsModel EditGetUser(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "The user parameter cannot be null.");
            }
            UserDetailsModel model = new UserDetailsModel
            {
                ID = user.ID,
                UserMail = user.Username,
                NameSurname = user.NameSurname,
                UserPass = user.Password,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
                SipSaticiKodu = user.SipSaticiKodu,
                IsEnable = user.IsEnable.ToString(),
                TeamName = user.TeamName,
                TCKN = user.TCKN,
                List = UsersList()
            };

            if (user.Role == "Müşteri")
            {
                model.MikroCompanyID = string.Format("{0}#{1}", user.MikroCompanyID.ToString(), user.MikroCompanyName.ToString());
                model.AuthPersonID = string.Format("{0}#{1}", user.AuthPersonID.ToString(), user.AuthPersonName.ToString());
            }




            return model;
        }
        public List<UserItem> UsersList()
        {
            List<UserItem> list = new List<UserItem>();
            List<Users> users = _db.Users.OrderBy(ok => ok.ID).ToList();

            foreach (var item in users)
            {
                list.Add(new UserItem
                {
                    ID = item.ID,
                    Email = item.Username,
                    NameSurname = item.NameSurname,
                    Role = item.Role,
                    PhoneNumber = item.PhoneNumber,
                    SipSaticiKodu = item.SipSaticiKodu,
                    Company = item.MikroCompanyName,
                    AuthPerson = item.AuthPersonName,
                    IsEnable = item.IsEnable,
                    TeamName = item.TeamName,
                });
            }

            return list;
        }

        public List<string> TeamsList()
        {
            List<string> list = new List<string>();
            List<Teams> teams = _db.Teams.ToList();
            foreach (var item in teams)
            {
                list.Add(item.TeamName);
            }
            return list;
        }

        public UserDetailsModel SearchedUsersList(List<Users> userList)
        {
            UserDetailsModel model = new UserDetailsModel();

            foreach (var item in userList)
            {
                model.List.Add(new UserItem
                {
                    ID = item.ID,
                    Email = item.Username,
                    IsEnable = item.IsEnable,
                    NameSurname = item.NameSurname,
                    PhoneNumber = item.PhoneNumber,
                    SipSaticiKodu = item.SipSaticiKodu,
                    AuthPerson = item.AuthPersonName,
                    Company = item.MikroCompanyName,
                    Role = item.Role,
                    TeamName = item.TeamName,
                });
            }
            return model;
        }
        public List<Users> SearchedUsers(UserDetailsModel dataModel)
        {
            IQueryable<Users> query = _db.Users.Where(ok => ok.ID > 0);

            if (!string.IsNullOrWhiteSpace(dataModel.IsEnable))
            {
                if (dataModel.IsEnable == "true")
                {
                    query = query.Where(ok => ok.IsEnable);
                }
                else
                {
                    query = query.Where(ok => !ok.IsEnable);
                }
            }

            if (!string.IsNullOrWhiteSpace(dataModel.NameSurname))
            {
                query = query.Where(ok => ok.NameSurname.Contains(dataModel.NameSurname));
            }

            if (!string.IsNullOrWhiteSpace(dataModel.Role))
            {
                query = query.Where(ok => ok.Role == dataModel.Role);
            }

            if (!string.IsNullOrWhiteSpace(dataModel.UserMail))
            {
                query = query.Where(ok => ok.Username.Contains(dataModel.UserMail));
            }

            if (!string.IsNullOrWhiteSpace(dataModel.TeamName))
            {

                query = query.Where(ok => ok.TeamName == dataModel.TeamName);
            }

            if (!string.IsNullOrWhiteSpace(dataModel.SearchName))
            {
                query = query.Where(ok => ok.NameSurname.Contains(dataModel.SearchName));
            }

            if (!string.IsNullOrWhiteSpace(dataModel.SearchMail))
            {
                query = query.Where(ok => ok.Username.Contains(dataModel.SearchMail));
            }

            if (dataModel.SearchRole != null && dataModel.SearchRole.Length > 0)
            {
                List<string> rolelist = dataModel.SearchRole.ToList();
                List<int> idList = new List<int>();
                foreach (var item in rolelist)
                {
                    var tempuser = _db.Users.Select(ok => new { ok.ID, ok.Role }).Where(ok => ok.Role.Contains(item)).ToList();

                    foreach (var user in tempuser)
                    {
                        idList.Add(user.ID);
                    }
                }
                query = query.Where(ok => idList.Contains(ok.ID));
            }

            if (dataModel.SearchTeam != null && dataModel.SearchTeam.Length > 0)
            {
                List<string> teamlist = dataModel.SearchTeam.ToList();
                List<int> idList = new List<int>();
                foreach (var item in teamlist)
                {
                    var tempuser = _db.Users.Select(ok => new { ok.ID, ok.TeamName }).Where(ok => ok.TeamName.Contains(item)).ToList();

                    foreach (var user in tempuser)
                    {
                        idList.Add(user.ID);
                    }
                }
                query = query.Where(ok => idList.Contains(ok.ID));
            }

            List<Users> list = query.OrderBy(Ok => Ok.ID).ToList();
            return list;
        }
        public bool RemoveUser(UserDetailsModel dataModel)
        {
            try
            {
                Users user = _db.Users.FirstOrDefault(ok => ok.ID == dataModel.ID);
                if (user != null)
                {
                    Authorizations auth = _db.Authorizations.FirstOrDefault(ok => ok.UserID == dataModel.ID);

                    if (auth != null)
                    {
                        _db.Authorizations.Remove(auth);
                        _db.Entry(auth).State = EntityState.Deleted;
                        _db.SaveChanges();
                    }

                    _db.Users.Remove(user);
                    _db.Entry(user).State = EntityState.Deleted;

                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string SaveUser(UserDetailsModel dataModel, IFormFileCollection images)
        {
            try
            {
                using (var transaction = _db.Database.BeginTransaction())
                {
                    Users user = new Users
                    {
                        IsEnable = dataModel.IsEnable == "true",
                        NameSurname = dataModel.NameSurname,
                        Password = dataModel.UserPass,
                        Role = dataModel.Role,
                        Username = dataModel.UserMail,
                        SipSaticiKodu = dataModel.SipSaticiKodu,
                        TeamName = (dataModel.TeamName != null) ? string.Join("#", dataModel.TeamName) : "",

                        PhoneNumber = dataModel.PhoneNumber,
                        TCKN = dataModel.TCKN,
                    };

                    if (dataModel.Role == "Müşteri")
                    {
                        user.MikroCompanyID = Guid.Parse(dataModel.MikroCompanyID.ToString().Split('#')[0]);
                        user.MikroCompanyName = dataModel.MikroCompanyID.ToString().Split('#')[1];

                        user.AuthPersonID = Convert.ToInt32(dataModel.AuthPersonID.ToString().Split('#')[0]);
                        user.AuthPersonName = dataModel.AuthPersonID.ToString().Split('#')[1];
                    }

                    _db.Users.Add(user);
                    _db.SaveChanges();

                    RoleTypes role = _db.RoleTypes.FirstOrDefault(ok => ok.RoleName == user.Role);
                    if (role == null)
                    {
                        throw new ArgumentException("The user role with the specified ID was not found.");
                    }
                    Authorizations auth = new Authorizations
                    {
                        UserID = user.ID,
                        Module_Users = role.Module_Users,
                        Module_Role = role.Module_Role,
                        Module_Order = role.Module_Order,
                        Module_Offer = role.Module_Offer,
                        Module_Customers = role.Module_Customers,
                    };

                    _db.Authorizations.Add(auth);


                    _db.SaveChanges();
                    transaction.Commit();
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public bool EditSetUser(UserDetailsModel dataModel, IFormFileCollection images)
        {
            try
            {
                Users user = _db.Users.FirstOrDefault(ok => ok.ID == dataModel.ID);
                if (user == null)
                {
                    throw new ArgumentException("The user with the specified ID was not found.");
                }
                user.Username = dataModel.UserMail;
                user.NameSurname = dataModel.NameSurname;
                user.Role = dataModel.Role;
                user.IsEnable = dataModel.IsEnable == "true";
                user.Password = dataModel.UserPass;
                user.TeamName = (dataModel.TeamName != null) ? string.Join("#", dataModel.TeamName) : "";

                if (dataModel.Role == "Müşteri")
                {
                    user.MikroCompanyID = Guid.Parse(dataModel.MikroCompanyID.ToString().Split('#')[0]);
                    user.MikroCompanyName = dataModel.MikroCompanyID.ToString().Split('#')[1];

                    user.AuthPersonID = Convert.ToInt32(dataModel.AuthPersonID.ToString().Split('#')[0]);
                    user.AuthPersonName = dataModel.AuthPersonID.ToString().Split('#')[1];
                }
                else
                {
                    user.SipSaticiKodu = dataModel.SipSaticiKodu;
                }

                user.PhoneNumber = dataModel.PhoneNumber;

                user.TCKN = dataModel.TCKN;

                _db.Users.Attach(user);
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();

                int roleCheck = _db.Authorizations.Count(ok => ok.UserID == user.ID);

                if (roleCheck == 0)
                {
                    RoleTypes role = _db.RoleTypes.FirstOrDefault(ok => ok.RoleName == user.Role);
                    if (role == null)
                    {
                        throw new ArgumentException("The user role with the specified ID was not found.");
                    }
                    Authorizations auth = new Authorizations
                    {
                        UserID = user.ID,
                        Module_Users = role.Module_Users,
                        Module_Role = role.Module_Role,
                        Module_Order = role.Module_Order,
                        Module_Offer = role.Module_Offer,
                        Module_Customers = role.Module_Customers,
                    };

                    _db.Authorizations.Add(auth);
                    _db.SaveChanges();
                }
                _db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
