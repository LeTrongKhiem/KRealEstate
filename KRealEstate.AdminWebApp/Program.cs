using KRealEstate.APIIntegration.ProductClient;
using KRealEstate.APIIntegration.UserClient;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//IMvcBuilder builders = builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
builder.Services.AddControllersWithViews();//.AddRazorRuntimeCompilation();
builder.Services.AddMvc();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //use session header bearer
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Login/Index";
    option.AccessDeniedPath = "/User/Forbidden";
}
    );
#if DEBUG
if (environment == Environments.Development)
{
    builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
}
#endif
builder.Services.AddTransient<IUserApiClient, UserApiClient>();
builder.Services.AddTransient<IAddressApiClient, AddressApiClient>();
builder.Services.AddTransient<IProductApiClient, ProductApiClient>();
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
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
