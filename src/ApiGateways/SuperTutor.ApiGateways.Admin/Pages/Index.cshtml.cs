using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SuperTutor.ApiGateways.Admin.Pages;
public class IndexModel : PageModel
{
    private static readonly HttpClient httpClient = new();

    public void OnGet()
    {

    }
}
