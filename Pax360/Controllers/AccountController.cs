using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Pax360.Models;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using Pax360DAL;
using Pax360DAL.Models;
using Dapper;

namespace Pax360.Controllers
{
    public class AccountController : Controller
    {
        private Context db;
        private ContextDapper dbDapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AccountController(Context _db, IHttpContextAccessor httpContextAccessor, ContextDapper _dbDapper)
        {
            db = _db;
            _httpContextAccessor = httpContextAccessor;
            dbDapper = _dbDapper;   
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult Login(LoginModel modelPost)
        {
            if (!string.IsNullOrWhiteSpace(modelPost.Username) && !string.IsNullOrWhiteSpace(modelPost.Password))
            {
                Users user = null;
                var query = "SELECT TOP(1) * FROM Users where Username=@Username and Password=@Password";
                var parameters = new DynamicParameters();
                parameters.Add("Username", modelPost.Username, DbType.String);
                parameters.Add("Password", modelPost.Password, DbType.String);

                using (var connection = dbDapper.CreateConnection())
                {
                    var companies = connection.Query<Users>(query, parameters);
                    user = companies.FirstOrDefault();
                }

                if (user != null && !user.IsEnable)
                {
                    modelPost.ErrorMessage = "Kullanýcý Aktif Deðil!";
                    return View(modelPost);
                }
                else if (user != null)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, modelPost.Username));
                    identity.AddClaim(new Claim(ClaimTypes.Name, modelPost.Username));
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    _httpContextAccessor.HttpContext.Session.SetString("LANG_VER", "EN");
                    _httpContextAccessor.HttpContext.Session.SetString("USERID", user.ID.ToString());
                    _httpContextAccessor.HttpContext.Session.SetString("NAMESURNAME", user.NameSurname);
                    _httpContextAccessor.HttpContext.Session.SetString("USERROLE", user.Role ?? "");
                    _httpContextAccessor.HttpContext.Session.SetString("TCKN", user.TCKN ?? "");
                    _httpContextAccessor.HttpContext.Session.SetString("TEAMNAME", user.TeamName ?? "");
                    _httpContextAccessor.HttpContext.Session.SetString("USERNAME", user.Username.ToString());

                    _httpContextAccessor.HttpContext.Session.SetString("SIPSATICIKODU", user.SipSaticiKodu);
                    _httpContextAccessor.HttpContext.Session.SetString("MIKROCOMPANYID", user.MikroCompanyID.ToString());
                    _httpContextAccessor.HttpContext.Session.SetString("MIKROCOMPANYNAME", user.MikroCompanyName ?? string.Empty);

                    SetRoles(user.ID);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    LoginModel model = new LoginModel();
                    var user2 = db.Users.Select(ok => new { ok.Username }).FirstOrDefault(ok => ok.Username == modelPost.Username);

                    if (user2 == null)
                    {
                        model.ErrorMessage = "Kullanýcý Bulunamadý!";
                    }
                    else
                    {
                        model.ErrorMessage = "Þifre Hatalý!";
                    }
                    return View(model);
                }
            }
            else
            {
                LoginModel model = new LoginModel();
                ModelState.AddModelError("Kullanýcý Adý yada Þifre boþ olamaz!", "Kullanýcý Adý yada Þifre boþ olamaz!");
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult Logoff()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public void SetRoles(int id)
        {
            Authorizations auth = db.Authorizations.FirstOrDefault(ok => ok.UserID == id);
            if (auth != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("Module_Users", auth.Module_Users.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("Module_Role", auth.Module_Role.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("Module_Order", auth.Module_Order.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("Module_Offer", auth.Module_Offer.ToString());


            }
        }
    }
}
