using Microsoft.AspNetCore.Authentication;

namespace ProductsService.Auth;
public class ApiKeyOptions : AuthenticationSchemeOptions
{
    public string ApiKey { get; set; } = string.Empty;
}