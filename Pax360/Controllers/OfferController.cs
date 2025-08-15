using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pax360.Attributes;
using Pax360.Extensions;
using Pax360.Helpers;
using Pax360.Interfaces;
using Pax360.Models;
using Pax360DAL.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static NuGet.Packaging.PackagingConstants;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Pax360DAL;

namespace Pax360.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMikroHelper _mikroService;
        private readonly IOfferService _offerService;
        private readonly Context db;

        public OfferController(Context _db,
            IHttpContextAccessor httpContextAccessor,
            IMikroHelper mikroService,
            IOfferService offerService)
        {
            _httpContextAccessor = httpContextAccessor;
            _mikroService = mikroService;
            _offerService = offerService;
            db = _db;
        }

        [HttpGet]
        public async Task<IActionResult> OfferList(OfferListModel dataModel, int page = 1, int DetailID = 0)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Offer") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Offer").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            OfferListModel model = new OfferListModel();
            model.DetailID = DetailID;
            IQueryable<Offers> query = OfferQuery(dataModel);
            model.OfferList = await query.OrderByDescending(ok => ok.ID).Skip((page - 1) * 50).Take(50).ToListAsync();
            model.PagingMetaData = new StaticPagedList<Offers>(model.OfferList, page, 50, query.Count());

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
        public async Task<IActionResult> OfferList(OfferListModel dataModel, string operation)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Offer") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Offer").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            OfferListModel model = new OfferListModel();
            int count = 0;
            switch (operation)
            {
                case "Search":
                    IQueryable<Offers> query = OfferQuery(dataModel);
                    dataModel.DetailID = 0;
                    model.OfferList = await query.OrderByDescending(ok => ok.ID).Skip((1 - 1) * 50).Take(50).ToListAsync();
                    model.PagingMetaData = new StaticPagedList<Offers>(model.OfferList, 1, 50, query.Count());
                    break;
            }

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

        [HttpGet]
        public IActionResult Offer(Guid id)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Offer") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Offer").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            OfferDetailsModel model = new OfferDetailsModel();

            var companyResult = _mikroService.GetMikroCompanyDetails(id);

            if (string.IsNullOrWhiteSpace(companyResult.Item1))
            {
                model.MusteriAdi = companyResult.Item2.MusteriAdi;
                model.cari_kod = companyResult.Item2.cari_kod;
                model.cari_Guid = id;
            }
            else
            {
                model.ErrorMessage = companyResult.Item1;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Offer(OfferDetailsModel dataModel, string operation)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Offer") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Offer").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }
            string errorMessage = null;
            string successMessage = null;
            OfferDetailsModel model = new OfferDetailsModel();

            switch (operation)
            {
                case "Save":
                    string checkSave = OfferRequiredCheck(dataModel);
                    if (!string.IsNullOrWhiteSpace(checkSave))
                    {
                        errorMessage = checkSave;
                    }
                    else
                    {
                        string resultSave = await _offerService.SaveOffer(dataModel);
                        if (string.IsNullOrWhiteSpace(resultSave))
                        {
                            successMessage = "Kaydetme başarılı.";
                            ModelState.Clear();
                        }
                        else
                        {
                            errorMessage = "Kaydeme başarısız! " + resultSave;
                        }
                    }
                    break;
            }

            var companyResult = _mikroService.GetMikroCompanyDetails(dataModel.cari_Guid);

            if (string.IsNullOrWhiteSpace(companyResult.Item1))
            {
                model.MusteriAdi = companyResult.Item2.MusteriAdi;
                model.cari_kod = companyResult.Item2.cari_kod;
                model.cari_Guid = dataModel.cari_Guid;
            }
            else
            {
                errorMessage += " " + companyResult.Item1;
            }
            model.ErrorMessage = errorMessage;
            model.SuccessMessage = successMessage;
            return View(model);
        }

        [HttpGet]
        public IActionResult SelectCompany()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Offer") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Offer").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            OfferDetailsModel model = new OfferDetailsModel();
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
        public IActionResult SelectCompany(OfferDetailsModel dataModel)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Offer") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Offer").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            OfferDetailsModel model = new OfferDetailsModel();

            if (!string.IsNullOrWhiteSpace(dataModel.SelectedCari))
            {
                return Redirect("/Offer/Offer?id=" + dataModel.SelectedCari.Split('#')[0]);
            }
            else
            {
                model.ErrorMessage = "Kurum seçmediniz!";
            }
            return View(model);
        }

        private string OfferRequiredCheck(OfferDetailsModel dataModel)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(dataModel.TeklifStatus))
            {
                result += "Teklif Statüsü Boş Olamaz! ";
            }
            if (string.IsNullOrWhiteSpace(dataModel.TeklifSartlari))
            {
                result += "Teklif Şartları Boş Olamaz! ";
            }
            if (string.IsNullOrWhiteSpace(dataModel.MusteriAdi))
            {
                result += "Müşteri Adı Boş Olamaz! ";
            }
            if (string.IsNullOrWhiteSpace(dataModel.cari_kod))
            {
                result += "Cari Kod Boş Olamaz! ";
            }

            List<OfferInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OfferInputModel>>("OFFERINPUT") ?? new List<OfferInputModel>();

            if (list.Count == 0)
            {
                result += "En Az 1 Kalem Zorunlu. ";
            }

            return result;
        }

        private IQueryable<Offers> OfferQuery(OfferListModel dataModel)
        {
            IQueryable<Offers> query = db.Offers;

            if (!string.IsNullOrWhiteSpace(dataModel.SelectedCari))
            {
                Guid cari = Guid.Parse(dataModel.SelectedCari.Split('#')[0]);
                query = query.Where(ok => ok.cari_Guid == cari);
            }

            if (dataModel.DetailID > 0)
            {
                query = db.Offers.Include(ok => ok.OfferItems);
                query = query.Where(ok => ok.ID == dataModel.DetailID);
            }

            if (!string.IsNullOrWhiteSpace(dataModel.TeklifSartlari))
            {
                query = query.Where(ok => ok.TeklifSartlari == dataModel.TeklifSartlari);
            }

            if (!string.IsNullOrWhiteSpace(dataModel.TeklifDurumu))
            {
                query = query.Where(ok => ok.TeklifStatus == dataModel.TeklifDurumu);
            }

            if (!string.IsNullOrWhiteSpace(dataModel.TeklifDurumu))
            {
                query = query.Where(ok => ok.TeklifStatus == dataModel.TeklifDurumu);
            }



            return query;
        }
    }
}
