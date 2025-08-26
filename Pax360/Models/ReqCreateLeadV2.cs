namespace Pax360.Models
{
    public class ReqCreateLeadV2
    {
        public ReqCreateLeadV2()
        {
            Items = new List<CreateLeadV2Item>();
        }
        public string usrPwd { get; set; }
        public string FiscalID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingDistrict { get; set; }
        public string ShippingCounty { get; set; }
        public int ShippingPostCode { get; set; }
        public int ShippingCityCode { get; set; }
        public string BillingAddress { get; set; }
        public string BillingDistrict { get; set; }
        public string BillingCounty { get; set; }
        public int BillingPostCode { get; set; }
        public int BillingCityCode { get; set; }
        public string Phone { get; set; }
        public string CellularPhone { get; set; }
        public string Email { get; set; }
        public string Vkn { get; set; }
        public string Tckn { get; set; }
        public string TaxOfficeName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyKnownAs { get; set; }
        public DateTime PermitExpirationDate { get; set; }
        public string PaymentType { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Comments { get; set; }
        public string TerminalId { get; set; }
        public string MerchantId { get; set; }
        public string Adsl_Number { get; set; }
        public string NaceKodu { get; set; }
        public string Banka { get; set; }
        public string QRCode { get; set; }
        public string sip_satici_kod { get; set; }
        public string sip_evrakno_seri { get; set; }
        public List<CreateLeadV2Item> Items { get; set; }
    }

    public class CreateLeadV2Item
    {
        public int LeadQuantity { get; set; }
        public string ProductCode { get; set; }
        public decimal PaymentAmount { get; set; }
        public string sip_eticaret_kanal_kodu { get; set; }
        public string serialNumbers { get; set; }
        public int HareketTipi { get; set; }
        public int vergi_orani { get; set; }
        public int sip_doviz_cinsi { get; set; }
        public string CampaignType { get; set; }
        public string HurdaSeriNo { get; set; }
    }
}
