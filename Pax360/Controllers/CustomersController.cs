using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pax360.Interfaces;
using Pax360.Models;
using Pax360DAL.Models;
using Pax360DAL;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using Pax360.Extensions;
using Pax360.Helpers;
using Microsoft.Extensions.Internal;

namespace Pax360.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMikroHelper _mikroService;
        private readonly ICustomerService _customerService;
        private readonly Context db;

        public CustomersController(Context _db,
            IHttpContextAccessor httpContextAccessor,
            IMikroHelper mikroService, ICustomerService customerService)
        {
            _httpContextAccessor = httpContextAccessor;
            _mikroService = mikroService;
            _customerService = customerService;
            db = _db;
        }

        [HttpGet]
        public async Task<IActionResult> CustomerList(CustomerListModel dataModel, Guid cariGuid, int page = 1)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Customers") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Customers").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            CustomerListModel model = new CustomerListModel();
            var companies = _mikroService.GetMikroCompaniesWithPax360(dataModel, (page - 1) * 50, 50);
            var counts = _mikroService.GetMikroCompaniesWithPax360Count();

            if (!string.IsNullOrWhiteSpace(companies.Item1))
            {
                model.ErrorMessage = "Kurumlar Bulunamadı.";
            }
            else if (!string.IsNullOrWhiteSpace(counts.Item1))
            {
                model.ErrorMessage = "Kurum Toplamı Bulunamadı.";
            }
            else
            {
                model.CustomerItemList = companies.Item2;
                model.PagingMetaData = new StaticPagedList<CustomerListItemModel>(model.CustomerItemList, page, 50, counts.Item2);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerList(CustomerListModel dataModel, string operation)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Customers") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Customers").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            CustomerListModel model = new CustomerListModel();
            int count = 0;
            switch (operation)
            {
                case "Search":
                    var companies = _mikroService.GetMikroCompaniesWithPax360(dataModel, 0, 50);
                    var counts = _mikroService.GetMikroCompaniesWithPax360Count();

                    if (!string.IsNullOrWhiteSpace(companies.Item1))
                    {
                        model.ErrorMessage = "Kurumlar Bulunamadı.";
                    }
                    else if (!string.IsNullOrWhiteSpace(counts.Item1))
                    {
                        model.ErrorMessage = "Kurum Toplamı Bulunamadı.";
                    }
                    else
                    {
                        model.CustomerItemList = companies.Item2;
                        model.PagingMetaData = new StaticPagedList<CustomerListItemModel>(model.CustomerItemList, 1, 50, counts.Item2);
                    }
                    break;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Customer(Guid cariGuid)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Order") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Order").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            CustomerListModel model = new CustomerListModel();
            UserHelper userHelper = new UserHelper(db, _httpContextAccessor);

            if (cariGuid != default(Guid))
            {
                var companyDetails = _mikroService.GetMikroCompanyDetails(cariGuid);

                if (!string.IsNullOrWhiteSpace(companyDetails.Item1))
                {
                    model.ErrorMessage = companyDetails.Item1;
                }
                else
                {
                    var companyDetailDB = _customerService.GetCustomer(cariGuid);
                    model.MusteriAdi = companyDetails.Item2.MusteriAdi;

                    model.KontakKisiAdSoyad = companyDetails.Item2.AdSoyad;
                    model.KontakKisiAdTelefon = companyDetails.Item2.Telefon;
                    model.KontakKisiAdEmail = companyDetails.Item2.Eposta;
                    model.Cari_Guid = cariGuid;
                    model.Customer = companyDetailDB;
                    model.ID = companyDetailDB.ID;

                    model.MusteriBankalari = companyDetailDB.MusteriBankalari.Select(ok => ok.BankName).ToArray();
                    model.KasaFirmasi = companyDetailDB.KasaFirmasi.Select(ok => ok.CaseCompany).ToArray();

                    model.AuthPersonList = userHelper.AuthPersonsWithID();
                }
            }
            else
            {
                model.ErrorMessage = "Müşteri Seçilmedi!";
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Customer(CustomerListModel dataModel, string operation)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Order") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Order").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }
            string errorMessage = null;
            string successMessage = null;
            CustomerListModel model = new CustomerListModel();
            UserHelper userHelper = new UserHelper(db, _httpContextAccessor);

            switch (operation)
            {
                case "Update":
                    string resultSave = await _customerService.UpdateCustomer(dataModel);
                    if (string.IsNullOrWhiteSpace(resultSave))
                    {
                        successMessage = "Güncelleme başarılı.";
                        ModelState.Clear();
                    }
                    else
                    {
                        errorMessage = "Güncelleme başarısız! " + resultSave;

                    }
                    break;
            }

            if (dataModel.Cari_Guid != default(Guid))
            {
                var companyDetails = _mikroService.GetMikroCompanyDetails(dataModel.Cari_Guid);

                if (!string.IsNullOrWhiteSpace(companyDetails.Item1))
                {
                    model.ErrorMessage = companyDetails.Item1;
                }
                else
                {
                    var companyDetailDB = _customerService.GetCustomer(dataModel.Cari_Guid);
                    model.MusteriAdi = companyDetails.Item2.MusteriAdi;

                    model.KontakKisiAdSoyad = companyDetails.Item2.AdSoyad;
                    model.KontakKisiAdTelefon = companyDetails.Item2.Telefon;
                    model.KontakKisiAdEmail = companyDetails.Item2.Eposta;
                    model.Cari_Guid = dataModel.Cari_Guid;
                    model.Customer = companyDetailDB;
                    model.ID = companyDetailDB.ID;

                    if (companyDetailDB.MusteriBankalari is not null)
                    {
                        model.MusteriBankalari = companyDetailDB.MusteriBankalari.Select(ok => ok.BankName).ToArray();
                    }

                    if (companyDetailDB.KasaFirmasi is not null)
                    {
                        model.KasaFirmasi = companyDetailDB.KasaFirmasi.Select(ok => ok.CaseCompany).ToArray();
                    }

                    model.AuthPersonList = userHelper.AuthPersonsWithID();
                }
            }
            else
            {
                model.ErrorMessage = "Müşteri Seçilmedi!";
            }

            model.SuccessMessage = successMessage;
            model.ErrorMessage = errorMessage;
            return View(model);
        }
    }
}
