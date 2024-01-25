using Blazored.LocalStorage;
using ClienteBlazorWASM.Helpers;
using ClienteBlazorWASM.Models;
using ClienteBlazorWASM.Services.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace ClienteBlazorWASM.Services
{
    public class ServiceAuthentication : IServiceAuthentication
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _stateProviderAuthentication;

        public ServiceAuthentication(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider stateProviderAuthentication)
        {
            _client = client;
            _localStorage = localStorage;
            _stateProviderAuthentication = stateProviderAuthentication;
        }

        public async Task<ResponseAuthentication> Login(UserAuthentication userFromAuthentication)
        {
            var content = JsonConvert.SerializeObject(userFromAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{Initialize.UrlBaseApi}api/users/login", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = (JObject)JsonConvert.DeserializeObject(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                var Token = result["result"]["token"].Value<string>();
                var user = result["result"]["user"]["userName"].Value<string>();

                await _localStorage.SetItemAsync(Initialize.Token_Local, Token);
                await _localStorage.SetItemAsync(Initialize.Data_User_Local, user);

                ((AuthStateProvider)_stateProviderAuthentication).NotifyUserLogin(Token);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Token);
                return new ResponseAuthentication { IsSuccess = true };
            }
            else
            {
                return new ResponseAuthentication { IsSuccess = false };
            }
        }


        public async Task<ResponseRegister> RegisterUser(UserRegister userForRegister)
        {
            var content = JsonConvert.SerializeObject(userForRegister);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{Initialize.UrlBaseApi}api/users/register", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseRegister>(contentTemp);

            if (response.IsSuccessStatusCode)
            {                
                return new ResponseRegister{ correctRegistration = true };
            }
            else
            {
                return result;
            }
        }

        public async Task Exit()
        {
            await _localStorage.RemoveItemAsync(Initialize.Token_Local);
            await _localStorage.RemoveItemAsync(Initialize.Data_User_Local);
            ((AuthStateProvider)_stateProviderAuthentication).NotifyUserLogout();

            _client.DefaultRequestHeaders.Authorization = null;
        }
    } 
}
