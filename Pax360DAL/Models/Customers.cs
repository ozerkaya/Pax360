using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pax360DAL.Models
{
    [Table("Customers")]
    public class Customers
    {
        public int ID { get; set; }
        public Guid Cari_Guid_Mikro { get; set; }

        [MaxLength(500)]
        public string? MusteriSegmenti { get; set; }
        [MaxLength(500)]
        public string? MusteriSektoru { get; set; }
        public int MagazaSayisi { get; set; }
        public int KasaSayisi { get; set; }
        [MaxLength(500)]
        public string? AccountManager { get; set; }
        public int AccountManagerID { get; set; }
        [MaxLength(500)]
        public string? SatisKanali { get; set; }

        [MaxLength(500)]
        public string? SonAktiviteNumarasi { get; set; }
        public DateTime SonAktiviteTarihi { get; set; }
        [MaxLength(500)]
        public string? SonAktiviteTipi { get; set; }
        [MaxLength(500)]
        public string? SonAktiviteOzeti { get; set; }
        [MaxLength(500)]
        public string? SahaFirmasi { get; set; }
        public ICollection<CustomerBanks> MusteriBankalari { get; set; }
        public ICollection<CustomerCases> KasaFirmasi { get; set; }
        public ICollection<CustomerDocuments> Dokumanlar { get; set; }
    }
}
