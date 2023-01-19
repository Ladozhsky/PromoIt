using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PromoItBlazorServer.Data;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuth0WebAppAuthentication(options => {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
        options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
        options.OpenIdConnectEvents = new OpenIdConnectEvents
        {
            OnTokenValidated = (context) =>
            {
                var token = context.SecurityToken.RawHeader + "." +
                    context.SecurityToken.RawPayload + "." + context.SecurityToken.RawSignature;
                var claims = new List<Claim>
                    {
                        new Claim("jwt_token", token)
                    };
                var appIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                context.Principal.AddIdentity(appIdentity);
                context.Response.Cookies.Append("auth_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return Task.CompletedTask;
            },
            OnTicketReceived = notification =>
            {
                Console.WriteLine("Authentication ticket received.");
                return Task.FromResult(true);
            },

            OnAuthorizationCodeReceived = notification =>
            {
                Console.WriteLine("Authorization code received");
                return Task.FromResult(true);
            },
            OnUserInformationReceived = notification =>
            {
                Console.WriteLine("Token validation received");
                return Task.FromResult(true);
            }
        };

    }).WithAccessToken(options =>
    {
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.UseRefreshTokens = true;
    });

builder.Services.AddCors();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "Admin"));
    options.AddPolicy("NPO Representative and Admin", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "NPO Representative", "Admin"));
    options.AddPolicy("Business representative and Admin", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "Business representative", "Admin"));
    options.AddPolicy("Social Activist and Admin", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "Social Activist", "Admin"));
    options.AddPolicy("Social Activist", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "Social Activist"));
    options.AddPolicy("NPO Representative", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "NPO Representative"));
    options.AddPolicy("Business representative", policy => policy.RequireClaim("https://promoit.co.il/claims/role", "Business representative"));
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(builder => builder.AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
