using Mono.Service.Models;
using Microsoft.EntityFrameworkCore;
using Mono.MVC.Models.Automapper;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Mono.MVC.Models.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
   builder.RegisterModule(new AutoFacConfig());
   
});
builder.Services.AddAutoMapper(typeof(AutoMappingProfiles).Assembly);
builder.Services.AddDbContext<VehicleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"), b => b.MigrationsAssembly("Mono.MVC")));


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

