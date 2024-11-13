using Exam.Front.Getaways;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var configurationBuilder = new ConfigurationBuilder();
#if DEBUG
configurationBuilder.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: false);
#else
// For release configuration, it will automatically load appsettings.json
configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
#endif
IConfiguration configuration = configurationBuilder.Build();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IResponseGetaway, ResponseGetaway>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(new System.IO.DirectoryInfo(app.Environment.ContentRootPath).Parent + configuration["StaticFiles:Assets"])),
    RequestPath = "/assets"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(new System.IO.DirectoryInfo(app.Environment.ContentRootPath).Parent + configuration["StaticFiles:Vendors"])),
    RequestPath = "/vendors"
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
