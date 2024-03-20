using ApplicationsAudit.Core;
using ApplicationsAudit.Domain.Managers;
using Common.Infrastructure;
using Infrastructure.EFCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(o =>
{
});
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyMethod()
           .AllowAnyHeader();
}));
builder.Services.AddServerSideBlazor();
builder.Services.AddDashboard();
builder.Services.AddManagers();
builder.Services.AddEmailService();
builder.Services.AddRadzenComponents();
builder.Services.AddControllers();
builder.Services.AddMvcCore();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<DialogService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
    });

builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddIdentity<Manager, ManagerRole>(o =>
{
    o.Lockout.AllowedForNewUsers = false;
})
.AddEntityFrameworkStores<DataContext>();

builder.Services.AddRoles();

var app = builder.Build();

app.MapRazorPages();
app.MapControllers();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
