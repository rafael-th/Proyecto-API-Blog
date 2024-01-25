using System.Security.Claims;
using System.Text.Json;

namespace ClienteBlazorWASM.Helpers
{
    public class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseInBase64WithoutMargin(payload);

            //pasa el formato JSON (clave:valor) a un objeto o clase de .NET (mapear)
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
                                    //kvp = keyValuePair
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private static byte[] ParseInBase64WithoutMargin(string base64)
        {
            switch (base64.Length %4)
            {
                case 2: base64 += "==";break;
                case 3: base64 += "=";  break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
