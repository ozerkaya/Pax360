using Microsoft.EntityFrameworkCore;
using Pax360.Interfaces;
using Pax360.Models;
using Pax360DAL.Models;
using Pax360DAL;
using System.Net;
using System.Text.Json;
using Pax360.Extensions;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pax360.Helpers
{
    public class CustomerService : ICustomerService
    {
        private readonly Context db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMikroHelper _mikroService;
        private readonly int userID;
        private readonly string nameSurname;


        public CustomerService(Context _db, IHttpContextAccessor httpContextAccessor, IMikroHelper mikroService)
        {
            db = _db;
            _httpContextAccessor = httpContextAccessor;
            _mikroService = mikroService;

            userID = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("USERID"));
            nameSurname = _httpContextAccessor.HttpContext.Session.GetString("NAMESURNAME");
        }

        public IQueryable<Customers> CustomerQuery(CustomerListModel dataModel)
        {
            IQueryable<Customers> query = db.Customers;


            return query;
        }

        public Customers GetCustomer(Guid cari_Guid)
        {
            var customer = db.Customers.Include(ok => ok.MusteriBankalari).Include(ok => ok.KasaFirmasi).Include(ok => ok.Dokumanlar).FirstOrDefault(ok => ok.Cari_Guid_Mikro == cari_Guid);

            if (customer == null)
            {
                return new Customers();
            }
            else
            {
                return customer;
            }

        }

        public async Task<string> UpdateCustomer(CustomerListModel dataModel)
        {
            try
            {
                if (dataModel.ID == 0)
                {
                    var customer = new Customers
                    {
                        MusteriSegmenti = dataModel.Customer.MusteriSegmenti,
                        AccountManager = dataModel.Customer.AccountManager,
                        AccountManagerID = dataModel.Customer.AccountManagerID,
                        SonAktiviteNumarasi = dataModel.Customer.SonAktiviteNumarasi,
                        SonAktiviteOzeti = dataModel.Customer.SonAktiviteOzeti,
                        SonAktiviteTarihi = dataModel.Customer.SonAktiviteTarihi,
                        SonAktiviteTipi = dataModel.Customer.SonAktiviteTipi,
                        Cari_Guid_Mikro = dataModel.Cari_Guid,
                        KasaSayisi = dataModel.Customer.KasaSayisi,
                        MagazaSayisi = dataModel.Customer.MagazaSayisi,
                        MusteriSektoru = dataModel.Customer.MusteriSektoru,
                        SatisKanali = dataModel.Customer.SatisKanali,
                        SahaFirmasi = dataModel.Customer.SahaFirmasi,
                        Dokumanlar = new List<CustomerDocuments>(),
                        KasaFirmasi = new List<CustomerCases>(),
                        MusteriBankalari = new List<CustomerBanks>(),
                    };

                    if (dataModel.MusteriBankalari is not null)
                    {
                        foreach (var item in dataModel.MusteriBankalari.ToList())
                        {
                            customer.MusteriBankalari.Add(new CustomerBanks
                            {
                                BankName = item
                            });
                        }
                    }

                    if (dataModel.KasaFirmasi is not null)
                    {
                        foreach (var item in dataModel.KasaFirmasi.ToList())
                        {
                            customer.KasaFirmasi.Add(new CustomerCases
                            {
                                CaseCompany = item
                            });
                        }
                    }


                    db.Customers.Add(customer);
                    await db.SaveChangesAsync();
                    return string.Empty;
                }
                else
                {
                    var customer = await db.Customers.FirstOrDefaultAsync(ok => ok.ID == dataModel.ID);

                    if (customer != null)
                    {
                        customer.MusteriSegmenti = dataModel.Customer.MusteriSegmenti;
                        customer.AccountManager = dataModel.Customer.AccountManager;
                        customer.AccountManagerID = dataModel.Customer.AccountManagerID;
                        customer.SonAktiviteNumarasi = dataModel.Customer.SonAktiviteNumarasi;
                        customer.SonAktiviteOzeti = dataModel.Customer.SonAktiviteOzeti;
                        customer.SonAktiviteTarihi = dataModel.Customer.SonAktiviteTarihi;
                        customer.SonAktiviteTipi = dataModel.Customer.SonAktiviteTipi;
                        customer.Cari_Guid_Mikro = dataModel.Cari_Guid;
                        customer.KasaSayisi = dataModel.Customer.KasaSayisi;
                        customer.MagazaSayisi = dataModel.Customer.MagazaSayisi;
                        customer.MusteriSektoru = dataModel.Customer.MusteriSektoru;
                        customer.SatisKanali = dataModel.Customer.SatisKanali;
                        customer.SahaFirmasi = dataModel.Customer.SahaFirmasi;
                                               

                        if (dataModel.MusteriBankalari is not null)
                        {
                            foreach (var item in customer.MusteriBankalari.ToList())
                            {
                                db.CustomerBanks.Remove(item);
                                db.Entry(item).State = EntityState.Deleted;
                            }

                            foreach (var item in dataModel.MusteriBankalari.ToList())
                            {
                                customer.MusteriBankalari.Add(new CustomerBanks
                                {
                                    BankName = item
                                });
                            }
                        }

                        if (dataModel.KasaFirmasi is not null)
                        {
                            foreach (var item in customer.KasaFirmasi.ToList())
                            {
                                db.CustomerCases.Remove(item);
                                db.Entry(item).State = EntityState.Deleted;
                            }

                            foreach (var item in dataModel.KasaFirmasi.ToList())
                            {
                                customer.KasaFirmasi.Add(new CustomerCases
                                {
                                    CaseCompany = item
                                });
                            }
                        }

                        db.Customers.Attach(customer);
                        db.Entry(customer).State = EntityState.Modified;
                        db.SaveChanges();

                        return string.Empty;
                    }
                    else
                    {
                        return "Müşteri Bulunamadı!";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
