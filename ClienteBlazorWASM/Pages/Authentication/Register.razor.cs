using ClienteBlazorWASM.Models;
using ClienteBlazorWASM.Services.IServices;
using Microsoft.AspNetCore.Components;

namespace ClienteBlazorWASM.Pages.Authentication
{
    public partial class Register
    {
        private UserRegister UserForRegister = new UserRegister();
        public bool isProcessing { get; set; } = false;
        public bool ShowErrorRegister {  get; set; }
        public IEnumerable<string> Mistakes { get; set; }

        [Inject]
        public IServiceAuthentication serviceAuthentication { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task registerUser()
        {
            ShowErrorRegister = false;
            isProcessing = true;
            var result = await serviceAuthentication.RegisterUser(UserForRegister);
            if (result.correctRegistration)
            {
                isProcessing = false;
                navigationManager.NavigateTo("/login");
            }
            else
            {
                isProcessing = false;
                Mistakes = result.Errors;
                ShowErrorRegister = true;
            }
        }
    }
}
