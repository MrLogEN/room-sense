using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace RoomSense.Api.Lib.Authentication;

public class ApiKeyFilter : IAsyncAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(
                ApiAuthConstants.ApiKeyHeaderName,
                out var extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("API Key is missing.");
            return;
        }

        var apiKey = _configuration.GetValue<string>(ApiAuthConstants.SectionName);

        if (apiKey is null)
        {
            throw new ArgumentNullException(nameof(apiKey), "API key was not found");
        }
        
        if (!apiKey.Equals(extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("API Key is invalid.");
            return;
        }
    }
}