using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

services.AddControllersWithViews();

// var authenticationOptions = configuration
//     .GetSection(KeycloakAuthenticationOptions.Section)
//     .Get<KeycloakAuthenticationOptions>();

// services.AddKeycloakAuthentication(authenticationOptions);
services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie("Cookies")
  .AddOpenIdConnect(options =>
  {
      options.Authority = "http://localhost:8080/auth/realms/Test";
      options.MetadataAddress = "http://localhost:8080/realms/Test/.well-known/openid-configuration";
      options.ClientId = "test-client";
      options.ClientSecret = "Eq41jAVOnZeUeZGDbXE5uuxdvbxStz6x";
      options.Scope.Add("openid");
      options.Scope.Add("profile");
      options.ResponseType = OpenIdConnectResponseType.Code;
      //   options.SaveTokens = true;
      //   options.GetClaimsFromUserInfoEndpoint = true;
      //   options.TokenValidationParameters = new TokenValidationParameters
      //   {
      //       NameClaimType = "name",
      //       RoleClaimType = "role"
      //   };
      // 開発のためHttpを許可する
      options.RequireHttpsMetadata = false;
  });
services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
