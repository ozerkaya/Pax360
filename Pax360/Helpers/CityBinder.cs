using Microsoft.AspNetCore.Mvc.Rendering;
using Pax360.Models;

namespace Pax360.Helpers
{
    public static class CityBinder
    {
        public static List<SelectListItem> SehirBind(bool IsIstanbulDivide = false, bool IsAnaStok = false)
        {
            var sehirler = SehirIlce.List.GroupBy(ok => ok.Sehir).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "Seçiniz",
                Value = ""
            });

            if (IsAnaStok)
            {
                list.Add(new SelectListItem
                {
                    Text = "ANA STOK",
                    Value = "ANA STOK"
                });

                list.Add(new SelectListItem
                {
                    Text = "ÖDEAL TEMSİLCİ STOK",
                    Value = "ÖDEAL TEMSİLCİ STOK"
                });
            }

            foreach (var item in sehirler)
            {
                if (IsIstanbulDivide && item.Key == "İSTANBUL")
                {
                    list.Add(new SelectListItem
                    {
                        Text = "İSTANBUL (ANADOLU)",
                        Value = "İSTANBUL (ANADOLU)",
                    });

                    list.Add(new SelectListItem
                    {
                        Text = "İSTANBUL (AVRUPA)",
                        Value = "İSTANBUL (AVRUPA)",
                    });
                }
                else
                {
                    list.Add(new SelectListItem
                    {
                        Text = item.Key,
                        Value = item.Key
                    });
                }


            }
            return list;
        }

        public static List<SelectListItem> PropayBasicSehirBind(bool istanbul = true)
        {

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "Seçiniz",
                Value = ""
            });


            list.Add(new SelectListItem
            {
                Text = "ADANA",
                Value = "ADANA"
            });

            list.Add(new SelectListItem
            {
                Text = "ANKARA",
                Value = "ANKARA"
            });

            list.Add(new SelectListItem
            {
                Text = "ANTALYA",
                Value = "ANTALYA"
            });

            list.Add(new SelectListItem
            {
                Text = "BURSA",
                Value = "BURSA"
            });

            list.Add(new SelectListItem
            {
                Text = "ESKİŞEHİR",
                Value = "ESKİŞEHİR"
            });

            if (istanbul)
            {
                list.Add(new SelectListItem
                {
                    Text = "İSTANBUL",
                    Value = "İSTANBUL"
                });
            }

            list.Add(new SelectListItem
            {
                Text = "İSTANBUL (ANADOLU)",
                Value = "İSTANBUL (ANADOLU)"
            });

            list.Add(new SelectListItem
            {
                Text = "İSTANBUL (AVRUPA)",
                Value = "İSTANBUL (AVRUPA)"
            });

            list.Add(new SelectListItem
            {
                Text = "İZMİR",
                Value = "İZMİR"
            });

            list.Add(new SelectListItem
            {
                Text = "KOCAELİ",
                Value = "KOCAELİ"
            });

            list.Add(new SelectListItem
            {
                Text = "SAMSUN",
                Value = "SAMSUN"
            });

            list.Add(new SelectListItem
            {
                Text = "TEKİRDAĞ",
                Value = "TEKİRDAĞ"
            });

            list.Add(new SelectListItem
            {
                Text = "MERSİN",
                Value = "MERSİN"
            });

            list.Add(new SelectListItem
            {
                Text = "DENİZLİ",
                Value = "DENİZLİ"
            });

            list.Add(new SelectListItem
            {
                Text = "DENİZLİ",
                Value = "DENİZLİ"
            });

            list.Add(new SelectListItem
            {
                Text = "KONYA",
                Value = "KONYA"
            });

            //list.Add(new SelectListItem
            //{
            //    Text = "SAKARYA",
            //    Value = "SAKARYA"
            //});

            list.Add(new SelectListItem
            {
                Text = "ÖDEAL TEMSİLCİ STOK",
                Value = "ÖDEAL TEMSİLCİ STOK"
            });



            return list;
        }
    }
}
