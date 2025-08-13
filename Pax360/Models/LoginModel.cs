using Pax360.Interfaces;

namespace Pax360.Models
{
    public class LoginModel : IMessage
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Hatirla { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
