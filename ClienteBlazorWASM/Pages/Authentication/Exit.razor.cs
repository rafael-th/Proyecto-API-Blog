using ClienteBlazorWASM.Services.IServices;
using Microsoft.AspNetCore.Components;

namespace ClienteBlazorWASM.Pages.Authentication
{
    public partial class Exit
    {
        [Inject]
        public IServiceAuthentication serviceAuthentication { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await serviceAuthentication.Exit();
            navigationManager.NavigateTo("/");
        }
    }
}
