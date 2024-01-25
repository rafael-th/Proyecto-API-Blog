using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClienteBlazorWASM.Pages
{
    public partial class RedirectToAccess
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationProviderState { get; set; }
        bool noAuthorized { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var AuthorizationState = await AuthenticationProviderState;

            if (AuthorizationState.User == null)
            {
                var returnUrl = navigationManager.ToBaseRelativePath(navigationManager.Uri);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    navigationManager.NavigateTo("Access", true);
                }
                else
                {
                    navigationManager.NavigateTo($"Access?returnUrl={returnUrl}",true);
                }
            }
            else
            {
                noAuthorized = true;
            }
        }
    }
}
