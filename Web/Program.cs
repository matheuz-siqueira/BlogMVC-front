using System.Net.Http.Headers;
using Web.Services;
using Web.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IAccountService, AccountService>("BlogAPI",c => 
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:Blog"]); 
});

builder.Services.AddHttpClient<IAuthenticateService, AuthenticateService>("BlogAPI", c => 
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:Blog"]); 
    c.DefaultRequestHeaders.Accept.Clear(); 
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
