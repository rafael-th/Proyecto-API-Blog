using ClienteBlazorWASM.Models;
using ClienteBlazorWASM.Services.IServices;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace ClienteBlazorWASM.Pages.Authentication
{
    public partial class Login
    {
        private UserAuthentication userAuthentication = new UserAuthentication();
        public bool isProcessing { get; set; } = false;
        public bool ShowErrorAuthentication { get; set; }
        public string Mistakes { get; set; }

        public string UrlReturn { get; set; }

        [Inject]
        public IServiceAuthentication serviceAuthentication { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task LoginUser()
        {
            ShowErrorAuthentication = false;
            isProcessing = true;
            var result = await serviceAuthentication.Login(userAuthentication);
            if (result.IsSuccess)
            {
                isProcessing = false;
                var urlAbsolute = new Uri(navigationManager.Uri);
                var parametersQuery = HttpUtility.ParseQueryString(urlAbsolute.Query);
                UrlReturn = parametersQuery["returnUrl"];
                if (string.IsNullOrEmpty(UrlReturn))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + UrlReturn);
                }
                
            }
            else
            {
                isProcessing = false;
                ShowErrorAuthentication = true;
                Mistakes = "Usuario y/o password son incorrectos";
                navigationManager.NavigateTo("/login");
            }
        }
    }
}
