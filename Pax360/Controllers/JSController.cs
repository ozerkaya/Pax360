using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Pax360.Extensions;
using Pax360.Interfaces;
using Pax360.Models;

namespace Pax360.Controllers
{

    [Authorize]
    public class JSController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICounterHelper _counterService;
        private readonly IOptions<ExternalDBSettings> _externalConfig;

        public JSController(IHttpContextAccessor httpContextAccessor,
            ICounterHelper counterService,
            IOptions<ExternalDBSettings> externalConfig)
        {
            _httpContextAccessor = httpContextAccessor;
            _counterService = counterService;
            _externalConfig = externalConfig;
        }

        [HttpPost]
        public ActionResult DistrictList([FromBody] string sehir)
        {
            var sehirler = SehirIlce.List.Where(ok => ok.Sehir == sehir).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in sehirler)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Ilce,
                    Value = item.Ilce
                });
            }
            return Json(list);
        }







        [HttpPost]
        public ActionResult SetOrder([FromBody] OrderInputModel dataModel)
        {
            List<OrderInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OrderInputModel>>("ORDERINPUT") ?? new List<OrderInputModel>();

            if (string.IsNullOrWhiteSpace(dataModel.cihazmodeli))
            {
                return BadRequest("Model zorunlu!");
            }
            else if (string.IsNullOrWhiteSpace(dataModel.doviz))
            {
                return BadRequest("Döviz Cinsi zorunlu!");
            }
            else if (dataModel.miktar == 0)
            {
                return BadRequest("Adet zorunlu!");
            }
            else if (dataModel.birimfiyat == 0)
            {
                return BadRequest("Fiyat zorunlu!");
            }
            else if (list.Any(ok => ok.cihazmodeli == dataModel.cihazmodeli))
            {
                return BadRequest(string.Format("{0} Daha Önce Eklenmiş!", dataModel.cihazmodeli));
            }

            decimal fiyat = 0;

            fiyat = dataModel.birimfiyat; /*_counterService.GetPrice("PAX360", dataModel.cihazmodeli);*/

            if (fiyat == 0)
            {
                return BadRequest(dataModel.cihazmodeli + " Fiyatı Bulunamadı!");
            }
            else
            {
                dataModel.birimfiyat = fiyat;
            }

            decimal toplamTutar = fiyat * (decimal)dataModel.miktar;

            dataModel.sira = list.Count + 1;
            dataModel.kdv = dataModel.cihazmodeli.Split('#')[2];
            dataModel.toplamtutar = toplamTutar.ToString();
            dataModel.toplamtutarkdvdahil = (toplamTutar +  (decimal)((toplamTutar/100)*Convert.ToInt32(dataModel.kdv))).ToString().Replace(",",".");
            list.Add(dataModel);
            _httpContextAccessor.HttpContext.Session.SetObject("ORDERINPUT", list);


            return Json(list.OrderByDescending(ok => ok.sira).ToList());
        }

        [HttpPost]
        public ActionResult GetOrders()
        {
            List<OrderInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OrderInputModel>>("ORDERINPUT") ?? new List<OrderInputModel>();
            return Json(list.OrderByDescending(ok => ok.sira).ToList());
        }

        [HttpPost]
        public ActionResult RemoveOrder([FromBody] string sira)
        {
            List<OrderInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OrderInputModel>>("ORDERINPUT") ?? new List<OrderInputModel>();

            if (list != null)
            {
                list.RemoveAll(Ok => Ok.sira == Convert.ToInt32(sira));
                List<OrderInputModel> newList = new List<OrderInputModel>();
                int i = 1;
                foreach (var item in list)
                {
                    OrderInputModel newItem = item;
                    newItem.sira = i;

                    newList.Add(newItem);
                    i++;
                }
                _httpContextAccessor.HttpContext.Session.SetObject("ORDERINPUT", list);
            }

            return Json(list.OrderByDescending(ok => ok.sira).ToList());
        }

        [HttpPost]
        public ActionResult RemoveAllOrder()
        {
            _httpContextAccessor.HttpContext.Session.SetObject("ORDERINPUT", new List<OrderInputModel>());
            List<OrderInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OrderInputModel>>("ORDERINPUT") ?? new List<OrderInputModel>();

            return Json(list.OrderByDescending(ok => ok.sira).ToList());
        }









        [HttpPost]
        public ActionResult SetOffer([FromBody] OfferInputModel dataModel)
        {
            List<OfferInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OfferInputModel>>("OFFERINPUT") ?? new List<OfferInputModel>();

            if (string.IsNullOrWhiteSpace(dataModel.adi))
            {
                return BadRequest("Ürün Modeli zorunlu!");
            }
            else if (dataModel.adet == 0)
            {
                return BadRequest("Adet zorunlu!");
            }
            else if (dataModel.fiyat == 0)
            {
                return BadRequest("Fiyat zorunlu!");
            }

            dataModel.sira = list.Count + 1;
            list.Add(dataModel);
            _httpContextAccessor.HttpContext.Session.SetObject("OFFERINPUT", list);


            return Json(list.OrderByDescending(ok => ok.sira).ToList());
        }

        [HttpPost]
        public ActionResult GetOffers()
        {
            List<OfferInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OfferInputModel>>("OFFERINPUT") ?? new List<OfferInputModel>();
            return Json(list.OrderByDescending(ok => ok.sira).ToList());
        }

        [HttpPost]
        public ActionResult RemoveOffer([FromBody] string sira)
        {
            List<OfferInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OfferInputModel>>("OFFERINPUT") ?? new List<OfferInputModel>();

            if (list != null)
            {
                list.RemoveAll(Ok => Ok.sira == Convert.ToInt32(sira));
                List<OfferInputModel> newList = new List<OfferInputModel>();
                int i = 1;
                foreach (var item in list)
                {
                    OfferInputModel newItem = item;
                    newItem.sira = i;

                    newList.Add(newItem);
                    i++;
                }
                _httpContextAccessor.HttpContext.Session.SetObject("OFFERINPUT", list);
            }

            return Json(list.OrderByDescending(ok => ok.sira).ToList());
        }

        [HttpPost]
        public ActionResult RemoveAllOffer()
        {
            _httpContextAccessor.HttpContext.Session.SetObject("OFFERINPUT", new List<OfferInputModel>());
            List<OfferInputModel> list = _httpContextAccessor.HttpContext.Session.GetObject<List<OfferInputModel>>("OFFERINPUT") ?? new List<OfferInputModel>();

            return Json(list.OrderByDescending(ok => ok.sira).ToList());
        }
    }
}
