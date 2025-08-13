using System;

namespace Pax360.Models
{
    public class OrderInputModel
    {
        public int sira { get; set; }
        public string cihazmodeli { get; set; }
        public int miktar { get; set; }
        public decimal birimfiyat { get; set; }
        public decimal birimfiyattl { get; set; }
        public string kdv { get; set; }
        public string iskonto { get; set; }
        public string toplamtutar { get; set; }
    }
}
