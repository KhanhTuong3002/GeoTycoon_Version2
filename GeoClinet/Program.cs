using BusinessObject.Entites;
using DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<GeoTycoonDbcontext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GeoTycoonDbcontext>();
builder.Services.AddScoped<DbContext,GeoTycoonDbcontext>();
builder.Services.AddScoped<ProfileRepo>();
builder.Services.AddControllersWithViews();

string storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Storage", "Images");
if (!Directory.Exists(storagePath))
{
    Directory.CreateDirectory(storagePath);
}
PhysicalFileProvider fileProvider = new(storagePath);
builder.Services.AddSingleton(fileProvider);

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
});

// Authorize role
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Teacher", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Teacher", "Administrator");
    });
    options.AddPolicy("Student", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Student", "Pending");
    });
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Administrator");
    });
});
var app = builder.Build();


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = new PathString("/Images"),
    ServeUnknownFileTypes = false
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
//this is test code for send mail .....
Console.WriteLine("Sending test email");
//await RegisterModel.SendEmailAsync("Tuongvkce161108@fpt.edu.vn", "test", "test");

//default admin account
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var configuration = app.Configuration;
    string email = configuration["Credentials:Email"];
    string password = configuration["Credentials:Password"];

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
        };
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Administrator");
            var profileRepo = services.GetRequiredService<ProfileRepo>();
            var profile = profileRepo.GetprofileFrommUser(user);
        }
    }
}

//add more pending users
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var configuration = app.Configuration;
    string email = "mydieu@123.com";
    string password = "chetconmenoroi";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
        };
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Pending");
            var profileRepo = services.GetRequiredService<ProfileRepo>();
            var profile = profileRepo.GetprofileFrommUser(user);
        }
    }
}
app.Run();
