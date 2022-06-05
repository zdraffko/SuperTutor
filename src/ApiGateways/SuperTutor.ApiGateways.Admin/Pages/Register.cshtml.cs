using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using SuperTutor.ApiGateways.Admin.Options;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace SuperTutor.ApiGateways.Admin.Pages;

public class RegisterModel : PageModel
{
    private static readonly HttpClient httpClient = new();
    private readonly string IdentityApiUrl;
    private readonly IHttpContextAccessor httpContextAccessor;

    public RegisterModel(IOptionsSnapshot<ApiUrlsOptions> apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
    {
        IdentityApiUrl = apiUrlsOptions.Value.Identity;
        this.httpContextAccessor = httpContextAccessor;
    }

    public void OnGet()
    {
    }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    [BindProperty]
    public string FirstName { get; set; }

    [BindProperty]
    public string LastName { get; set; }

    public string Msg { get; set; }

    public async Task<ActionResult> OnPost()
    {
        var cancellationToken = new CancellationTokenSource().Token;

        var adminId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var profilesRequest = new
        {
            Email = Email,
            Password = Password,
            FirstName = FirstName,
            LastName = LastName
        };

        var resposne = await httpClient.PostAsJsonAsync($"{IdentityApiUrl}/users/RegisterAdmin", profilesRequest, cancellationToken: cancellationToken);
        var res = await resposne.Content.ReadFromJsonAsync<RegisterResponse>();
        var principe = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, res.AuthToken),
                    new Claim(ClaimTypes.Name, Email)
            }));

        await httpContextAccessor.HttpContext.SignInAsync("Cookies", principe);

        return RedirectToPage("Index");
    }
}

public class RegisterResponse
{
    [JsonPropertyName("authToken")]
    public string AuthToken { get; set; }
}
