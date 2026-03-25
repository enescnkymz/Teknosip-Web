using Microsoft.EntityFrameworkCore;
using Teknosip.Application.Features.Categories.Commands.CreateCategory;
using Teknosip.Application.Repositories;
using Teknosip.Persistence.Contexts;
using Teknosip.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TeknosipDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICategoryCommandRepository, CategoryCommandRepository>();
builder.Services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();

// 2. MEDIATR KAYDI (Postacýyý iþe alýyoruz)
// Diyoruz ki: "Application katmanýna git, oradaki bütün Handler'larý (iþçileri) bul ve sisteme kaydet."
builder.Services.AddMediatR(configuration =>
{
	// Application katmanýndaki herhangi bir sýnýfý referans göstermemiz yeterli, o tüm katmaný tarar.
	configuration.RegisterServicesFromAssembly(typeof(CreateCategoryCommand).Assembly);
});

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
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
