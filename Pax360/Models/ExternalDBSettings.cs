namespace Pax360.Models
{
    public class ExternalDBSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string MicroConnectionString { get; set; }
        public string CounterConnectionString { get; set; }
        public string UseExternalDB { get; set; }
    }
}
