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

namespace Pax360.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMikroHelper _mikroService;
        private readonly IOrderService _orderService;

        public OrderController(IHttpContextAccessor httpContextAccessor, IMikroHelper mikroService, IOrderService orderService)
        {
            _httpContextAccessor = httpContextAccessor;
            _mikroService = mikroService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> OrderList(OrderListModel dataModel, int page = 1)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Order") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Order").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }
            int count = 0;
            OrderListModel model = new OrderListModel();

            var result = _mikroService.GetMikroOrderList(page, dataModel);

            if (string.IsNullOrWhiteSpace(result.Item1))
            {
                model.OrderList = result.Item2;
            }
            else
            {
                model.ErrorMessage = result.Item1;
            }

            var resultCount = _mikroService.GetMikroOrderListCount();

            if (string.IsNullOrWhiteSpace(resultCount.Item1))
            {
                count = resultCount.Item2;
            }

            model.PagingMetaData = new StaticPagedList<OrderListItemModel>(model.OrderList, page, 50, count);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> OrderList(OrderListModel dataModel, string operation)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Order") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Order").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            OrderListModel model = new OrderListModel();
            int count = 0;
            switch (operation)
            {
                case "Search":
                    var result = _mikroService.GetMikroOrderList(1, dataModel);

                    if (string.IsNullOrWhiteSpace(result.Item1))
                    {
                        model.OrderList = result.Item2;
                    }
                    else
                    {
                        model.ErrorMessage = result.Item1;
                    }

                    var resultCount = _mikroService.GetMikroOrderListCount();

                    if (string.IsNullOrWhiteSpace(resultCount.Item1))
                    {
                        count = resultCount.Item2;
                    }
                    else
                    {
                        model.ErrorMessage += " " + resultCount.Item1;
                    }
                    break;

                case "DurumUpdate":
                    var requiredResult = DurumUpdateRequired(dataModel);

                    if (!string.IsNullOrWhiteSpace(requiredResult))
                    {
                        model.ErrorMessage = requiredResult;
                    }
                    else
                    {
                        var resultUpdate = _mikroService.UpdateMikroOrderDurum(dataModel);

                        if (!string.IsNullOrWhiteSpace(resultUpdate))
                        {
                            model.ErrorMessage = resultUpdate;
                        }
                    }

                    var result2 = _mikroService.GetMikroOrderList(1, dataModel);

                    if (string.IsNullOrWhiteSpace(result2.Item1))
                    {
                        model.OrderList = result2.Item2;
                    }
                    else
                    {
                        model.ErrorMessage += " " + result2.Item1;
                    }

                    var resultCount2 = _mikroService.GetMikroOrderListCount();

                    if (string.IsNullOrWhiteSpace(resultCount2.Item1))
                    {
                        count = resultCount2.Item2;
                    }
                    else
                    {
                        model.ErrorMessage += " " + result2.Item1;
                    }
                    break;
            }

            model.PagingMetaData = new StaticPagedList<OrderListItemModel>(model.OrderList, 1, 50, count);
            return View(model);
        }

        [HttpGet]
        public IActionResult Order(Guid id)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Order") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Order").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            OrderDetailsModel model = new OrderDetailsModel();
            var companyResult = _mikroService.GetMikroCompanyDetails(id);

            if (string.IsNullOrWhiteSpace(companyResult.Item1))
            {
                model = companyResult.Item2;
            }
            else
            {
                model.ErrorMessage = companyResult.Item1;
            }

            model.cari_Guid = id;
            model.CityList = CityBinder.SehirBind();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Order(OrderDetailsModel dataModel, string operation)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Order") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Order").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }
            string errorMessage = null;
            string successMessage = null;
            OrderDetailsModel model = new OrderDetailsModel();

            switch (operation)
            {
                case "Save":
                    string checkSave = OrderRequiredCheck(dataModel);
                    if (!string.IsNullOrWhiteSpace(checkSave))
                    {
                        errorMessage = checkSave;
                    }
                    else
                    {
                        string resultSave = await _orderService.SaveOrder(dataModel);
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
                model = companyResult.Item2;
            }
            else
            {
                model.ErrorMessage = companyResult.Item1;
            }

            model.ErrorMessage = errorMessage;
            model.SuccessMessage = successMessage;
            model.cari_Guid = dataModel.cari_Guid;
            model.CityList = CityBinder.SehirBind();
            return View(model);
        }

        [HttpGet]
        public IActionResult SelectCompany()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Order") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Order").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            OrderDetailsModel model = new OrderDetailsModel();
            var mikroCompanies = _mikroService.GetMikroCompanies();
            if (string.IsNullOrWhiteSpace(mikroCompanies.Item1))
            {
                model.MikroCompanyList = mikroCompanies.Item2;
            }
            else
            {
                model.ErrorMessage = "Kurumlar Bulunamadı.";
            }
            model.CityList = CityBinder.SehirBind();
            return View(model);
        }

        [HttpPost]
        public IActionResult SelectCompany(OrderDetailsModel dataModel)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Order") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Order").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            OrderDetailsModel model = new OrderDetailsModel();

            if (!string.IsNullOrWhiteSpace(dataModel.SelectedCari))
            {
                return Redirect("/Order/Order?id=" + dataModel.SelectedCari.Split('#')[0]);
            }
            else
            {
                model.ErrorMessage = "Kurum seçmediniz!";
            }
            return View(model);
        }

        private string OrderRequiredCheck(OrderDetailsModel dataModel)
        {
            string result = "";

            if (string.IsNullOrWhiteSpace(dataModel.TeslimatAdresi))
            {
                result += "Teslimat Adresi Zorunlu. ";
            }

            if (string.IsNullOrWhiteSpace(dataModel.TeslimatIl))
            {
                result += "Teslimat İl Zorunlu. ";
            }

            if (string.IsNullOrWhiteSpace(dataModel.TeslimatIlce))
            {
                result += "Teslimat İlçe Zorunlu. ";
            }

            if (string.IsNullOrWhiteSpace(dataModel.VadeTarihi))
            {
                result += "Vade Tarihi Zorunlu. ";
            }

            if (string.IsNullOrWhiteSpace(dataModel.SahaFirmasi))
            {
                result += "Saha Firması Zorunlu. ";
            }

            if (string.IsNullOrWhiteSpace(dataModel.BankaOrtami))
            {
                result += "Banka Ortamı Zorunlu. ";
            }

            if (string.IsNullOrWhiteSpace(dataModel.CihazModu))
            {
                result += "Cihaz Modu Zorunlu. ";
            }

            if (string.IsNullOrWhiteSpace(dataModel.Entegrasyon))
            {
                result += "Entegrasyon Zorunlu. ";
            }

            if (string.IsNullOrWhiteSpace(dataModel.YuklenecekBanka))
            {
                result += "Bankacılık Uygulaması Zorunlu. ";
            }

            if (string.IsNullOrWhiteSpace(dataModel.YuklenecekUygulama))
            {
                result += "Satış Uygulaması Zorunlu. ";
            }

            List<OrderInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OrderInputModel>>("ORDERINPUT") ?? new List<OrderInputModel>();

            if (list.Count == 0)
            {
                result += "En Az 1 Kalem Zorunlu. ";
            }

            return result;
        }

        private string DurumUpdateRequired(OrderListModel dataModel)
        {
            string result = string.Empty;
            if (string.IsNullOrWhiteSpace(dataModel.ID))
            {
                result += "Sipariş Numarası Boş Olamaz!,";
            }
            if (string.IsNullOrWhiteSpace(dataModel.SelectedDurum))
            {
                result += "Durum Boş Olamaz!,";
            }

            return result;
        }
    }
}
