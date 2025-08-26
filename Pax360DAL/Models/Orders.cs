using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pax360DAL.Models
{
    [Table("Orders")]
    public class Orders
    {
        public int ID { get; set; }
        [MaxLength(500)]
        public string? MusteriAdi { get; set; }
        [MaxLength(500)]
        public string? TicariUnvan { get; set; }
        [MaxLength(500)]
        public string? VKNTCKN { get; set; }
        [MaxLength(500)]
        public string? SiparisTipi { get; set; }
        [MaxLength(500)]
        public string? FaturaAdresi { get; set; }
        [MaxLength(500)]
        public string? Il { get; set; }
        [MaxLength(500)]
        public string? Ilce { get; set; }
        [MaxLength(500)]
        public string? SaticiPlasiyer { get; set; }
        [MaxLength(500)]
        public string? TeslimatAdresi { get; set; }
        [MaxLength(500)]
        public string? TeslimatIl { get; set; }
        [MaxLength(500)]
        public string? TeslimatIlce { get; set; }
        [MaxLength(500)]
        public string? SiparisNumarasi { get; set; }
        public Guid cari_Guid { get; set; }
        [MaxLength(500)]
        public string? cari_kod { get; set; }
        [MaxLength(500)]
        public string? AdSoyad { get; set; }
        [MaxLength(500)]
        public string? Eposta { get; set; }
        [MaxLength(500)]
        public string? Telefon { get; set; }
        [MaxLength(500)]
        public int VadeTarihi { get; set; }
        [MaxLength(500)]
        public string? TeslimTuru { get; set; }
        [MaxLength(500)]
        public string? SahaFirmasi { get; set; }
        [MaxLength(500)]
        public string? BankaOrtami { get; set; }
        [MaxLength(500)]
        public string? CihazModu { get; set; }
        [MaxLength(500)]
        public string? Entegrasyon { get; set; }
        [MaxLength(500)]
        public string? YuklenecekBanka { get; set; }
        [MaxLength(500)]
        public string? Not { get; set; }
        public DateTime SiparisTarihi { get; set; }
        [MaxLength(500)]
        public string? SiparisDurumu { get; set; }
        public ICollection<OrdersItem> OrderItems { get; set; }

        public int UserID { get; set; }
        public string UserName { get; set; }


        public Guid SiparisMusterisi_cari_Guid { get; set; }
        [MaxLength(500)]
        public string SiparisMusterisi { get; set; }

    }
}
