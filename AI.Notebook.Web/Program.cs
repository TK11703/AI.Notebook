using AI.Notebook.Web;
using AI.Notebook.Web.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.ConfigureClients(builder.Configuration); //custom service extension method with configurations

builder.Services.AddLogging();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(builder.Configuration.GetValue<int>("SiteSettings:SessionTimeout"));
	options.Cookie.Name = builder.Configuration.GetValue<string>("SiteSettings:SessionCookieName");
	options.Cookie.HttpOnly = builder.Configuration.GetValue<bool>("SiteSettings:HttpOnly");
	options.Cookie.IsEssential = true;
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
	// This lambda determines whether user consent for non-essential cookies is needed for a given request.
	options.CheckConsentNeeded = context => true;
	options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
	// Handling SameSite cookie according to https://docs.microsoft.com/en-us/aspnet/core/security/samesite
	//options.HandleSameSiteCookieCompatibility();
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

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

app.UseSession();

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapFileHandlerEndpoints();

app.Run();