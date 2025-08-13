namespace Pax360.Models
{
    public class SiparisKaydetResult
    {
        public List<ResultSiparisKaydet> result { get; set; }
    }

    public class ResultSiparisKaydet
    {
        public int StatusCode { get; set; }
        public DataSiparis Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }
    }

    public class ListSiparis
    {
        public string cariHarGuid { get; set; }
    }

    public class DataSiparis
    {
        public List<ListSiparis> list { get; set; }
    }
}
