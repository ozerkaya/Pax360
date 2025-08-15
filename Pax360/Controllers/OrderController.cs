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
using Microsoft.EntityFrameworkCore;
using Pax360DAL;

namespace Pax360.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMikroHelper _mikroService;
        private readonly IOrderService _orderService;
        private readonly Context db;

        public OrderController(Context _db,
            IHttpContextAccessor httpContextAccessor,
            IMikroHelper mikroService, IOrderService orderService)
        {
            _httpContextAccessor = httpContextAccessor;
            _mikroService = mikroService;
            _orderService = orderService;
            db = _db;
        }

        [HttpGet]
        public async Task<IActionResult> OrderList(OrderListModel dataModel, int page = 1, int DetailID = 0)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Order") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Order").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            OrderListModel model = new OrderListModel();
            model.DetailID = DetailID;
            IQueryable<Orders> query = OrderQuery(dataModel);
            model.OrderList = await query.OrderByDescending(ok => ok.ID).Skip((page - 1) * 50).Take(50).ToListAsync();
            model.PagingMetaData = new StaticPagedList<Orders>(model.OrderList, page, 50, query.Count());

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
                    IQueryable<Orders> query = OrderQuery(dataModel);
                    dataModel.DetailID = 0;
                    model.OrderList = await query.OrderByDescending(ok => ok.ID).Skip((1 - 1) * 50).Take(50).ToListAsync();
                    model.PagingMetaData = new StaticPagedList<Orders>(model.OrderList, 1, 50, query.Count());
                    break;
                case "DurumUpdate":
                    var requiredResult = DurumUpdateRequired(dataModel);

                    if (!string.IsNullOrWhiteSpace(requiredResult))
                    {
                        model.ErrorMessage = requiredResult;
                    }
                    else
                    {
                        var resultUpdate = await _orderService.UpdateOrderStatus(dataModel);

                        if (!string.IsNullOrWhiteSpace(resultUpdate))
                        {
                            model.ErrorMessage = resultUpdate;
                        }
                        else
                        {
                            model.SuccessMessage = "Güncelleme Başarılı.";
                        }
                    }

                    IQueryable<Orders> query2 = OrderQuery(dataModel);
                    model.OrderList = await query2.OrderByDescending(ok => ok.ID).Skip((0) * 50).Take(50).ToListAsync();
                    model.PagingMetaData = new StaticPagedList<Orders>(model.OrderList, 1, 50, query2.Count());


                    var resultCount2 = _mikroService.GetMikroOrderListCount();

                    if (string.IsNullOrWhiteSpace(resultCount2.Item1))
                    {
                        count = resultCount2.Item2;
                    }
                    else
                    {
                        model.ErrorMessage += " " + resultCount2.Item1;
                    }
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
        public async Task<IActionResult> Order(Guid id, int orderID = 0)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Order") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Order").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }

            OrderDetailsModel model = new OrderDetailsModel();

            if (orderID > 0)
            {
                var getResult = await _orderService.GetOrder(orderID);

                if (!string.IsNullOrWhiteSpace(getResult.Item1))
                {
                    model.ErrorMessage = getResult.Item1;
                }
                else
                {
                    model = getResult.Item2;
                    model.IsModify = true;
                    id = model.cari_Guid;
                    model.selectedID = orderID;
                }
            }
            else
            {
                _httpContextAccessor.HttpContext.Session.SetObject("ORDERINPUT", new List<OrderInputModel>());
                List<OrderInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OrderInputModel>>("ORDERINPUT") ?? new List<OrderInputModel>();

                var companyResult = _mikroService.GetMikroCompanyDetails(id);

                if (string.IsNullOrWhiteSpace(companyResult.Item1))
                {
                    model = companyResult.Item2;
                }
                else
                {
                    model.ErrorMessage = companyResult.Item1;
                }
            }

            var mikroProducts = _mikroService.GetMikroProducts();
            if (string.IsNullOrWhiteSpace(mikroProducts.Item1))
            {
                model.MikroProductList = mikroProducts.Item2;
            }
            else
            {
                model.ErrorMessage = "Ürünler Bulunamadı.";
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
                case "Update":
                    string checkUpdate = OrderRequiredCheck(dataModel);
                    if (!string.IsNullOrWhiteSpace(checkUpdate))
                    {
                        errorMessage = checkUpdate;
                    }
                    else
                    {
                        string resultSave = await _orderService.UpdateOrder(dataModel);
                        if (string.IsNullOrWhiteSpace(resultSave))
                        {
                            successMessage = "Güncelleme başarılı.";
                            ModelState.Clear();
                        }
                        else
                        {
                            errorMessage = "Güncelleme başarısız! " + resultSave;

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

            var mikroProducts = _mikroService.GetMikroProducts();
            if (string.IsNullOrWhiteSpace(mikroProducts.Item1))
            {
                model.MikroProductList = mikroProducts.Item2;
            }
            else
            {
                model.ErrorMessage = "Ürünler Bulunamadı.";
            }

            model.selectedID = dataModel.selectedID;
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
            if (dataModel.ID == 0)
            {
                result += "Sipariş Numarası Boş Olamaz!";
            }
            if (string.IsNullOrWhiteSpace(dataModel.SelectedDurum))
            {
                result += "Durum Boş Olamaz!";
            }

            return result;
        }

        private IQueryable<Orders> OrderQuery(OrderListModel dataModel)
        {
            IQueryable<Orders> query = db.Orders;

            if (!string.IsNullOrWhiteSpace(dataModel.SelectedCari))
            {
                Guid cari = Guid.Parse(dataModel.SelectedCari.Split('#')[0]);
                query = query.Where(ok => ok.cari_Guid == cari);
            }

            if (dataModel.DetailID > 0)
            {
                query = db.Orders.Include(ok => ok.OrderItems);
                query = query.Where(ok => ok.ID == dataModel.DetailID);
            }




            return query;
        }
    }
}
