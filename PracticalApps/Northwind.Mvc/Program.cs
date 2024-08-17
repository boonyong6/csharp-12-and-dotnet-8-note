#region Import namespaces

using Microsoft.AspNetCore.Identity; // To use IdentityUser.
using Microsoft.EntityFrameworkCore; // To use UseSqlite/UseSqlServer method.
using Northwind.Mvc.Data; // To use ApplicationDbContext.
using Northwind.EntityModels; // To use AddNorthwindContext method.
using System.Net.Http.Headers; // To use MediaTypeWithQualityHeaderValue.

#endregion

#region Configure the host web server including services

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Enable role management. (The order matters!!!)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddNorthwindContext();

builder.Services.AddOutputCache(options =>
{
    // The default is one minute.
    options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(20);
    options.AddPolicy("views", p => p.SetVaryByQuery("alertStyle"));
});

builder.Services.AddHttpClient(name: "Northwind.WebApi", 
    configureClient: options =>
    {
        options.BaseAddress = new Uri("https://localhost:5151/");
        options.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue(
                mediaType: "application/json", quality: 1.0));
    });

builder.Services.AddHttpClient(name: "Northwind.MinimalApi",
    configureClient: options =>
    {
        options.BaseAddress = new Uri("http://localhost:5152/");
        options.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json", 1.0));
    });

#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    // The default relative URL path: /ApplyDatabaseMigrations, see https://github.com/dotnet/aspnetcore/blob/main/src/Middleware/Diagnostics.EntityFrameworkCore/src/MigrationsEndPointOptions.cs#L18
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseOutputCache();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    // .CacheOutput(policyName: "views");
app.MapRazorPages(); // ASP.NET Core Identity uses a Razor Class Library that uses Razor Pages.

app.MapGet("/notcached", () => DateTime.Now.ToString());
app.MapGet("/cached", () => DateTime.Now.ToString()).CacheOutput();

#endregion

#region Start the host web server listening for HTTP requests.

app.Run(); // This is a blocking call.

#endregion
