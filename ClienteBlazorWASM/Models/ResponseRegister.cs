namespace ClienteBlazorWASM.Models
{
    public class ResponseRegister
    {
        public bool correctRegistration { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
