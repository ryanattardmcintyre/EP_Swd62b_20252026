using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using DataAccess.Context;
using DataAccess.Repositories;
using Presentation.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ShoppingCartDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ShoppingCartDbContext>();
builder.Services.AddControllersWithViews();


//These lines will register the service BooksRepository with a list of 
//known services the CLR can instantiate

/* Why do you go for Scoped/ transient/ Singleton
 * 
 *  Scoped -> it will create a new instance per request per user
 *  Transient -> it will create a new instance per call per request per user
 *  Singleton -> it will create a new instance per application
 *  
 */

//BooksRepository is also referred to as a service

builder.Services.AddScoped(typeof(BooksRepository));
builder.Services.AddScoped(typeof(CategoriesRepository));
//builder.Services.AddTransient(typeof(BooksRepository));
//builder.Services.AddSingleton(typeof(BooksRepository));

var app = builder.Build();

//Property injection:
//BooksController controller = new BooksController(null);
//controller._booksRepository = (BooksRepository)app.Services.GetService(typeof(BooksRepository));

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

app.Run();
