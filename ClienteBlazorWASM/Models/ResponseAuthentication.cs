namespace ClienteBlazorWASM.Models
{
    public class ResponseAuthentication
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public User User { get; set; }
    }
}
