using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Teknosip.Application.Features.Categories.Commands.CreateCategory;
using Teknosip.Application.Interfaces;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;
using Teknosip.Infrastructure.Services;
using Teknosip.Persistence.Contexts;
using Teknosip.Persistence.Repositories;
using Teknosip.WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TeknosipDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
	// Ýstersen þifre kurallarýný buradan esnetebilirsin (Þimdilik test için kolaylýk saðlar)
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<TeknosipDbContext>() // Veritabaný olarak senin Context'ini kullanacaðýný belirtiyoruz
.AddDefaultTokenProviders(); // Þifre sýfýrlama vs. için token üreteci

//  ÝÞTE EKLENECEK SÝHÝRLÝ SATIR (AddIdentity'den hemen SONRA ekle): 
builder.Services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomUserClaimsPrincipalFactory>();

builder.Services.ConfigureApplicationCookie(options =>
{
	// 1. Güvenlik ve Ýsimlendirme Ayarlarý
	options.Cookie.Name = "Teknosip.AuthTicket"; // Tarayýcýda görünecek özel bilet adýmýz
	options.Cookie.HttpOnly = true; // Çok Kritik! Cookie'yi kötü niyetli JavaScript kodlarýndan korur (XSS saldýrýlarýný engeller).
	options.Cookie.SameSite = SameSiteMode.Strict; // Baþka sitelerden gelen sahte form gönderimlerini (CSRF) engeller.
	options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Canlýda (HTTPS) tam güvenlik saðlar.

	// 2. Süre ve "Beni Hatýrla" Ayarlarý
	options.ExpireTimeSpan = TimeSpan.FromDays(14); // Biletin ömrü 14 gün olsun.
	options.SlidingExpiration = true; // Kullanýcý siteyi kullanmaya devam ettikçe 14 günlük süre her seferinde baþa sarsýn (Kullanýcý aktifken sistemden atýlmasýn).

	// 3. Kapýdan Çevirme (Yönlendirme) Ayarlarý
	options.LoginPath = "/Auth/Login"; // Eðer biletsiz biri gizli bir sayfaya girmeye çalýþýrsa, onu otomatik olarak Login sayfasýna fýrlat.
	options.LogoutPath = "/Auth/Logout"; // Çýkýþ iþlemi yapýlacak adres.
	options.AccessDeniedPath = "/Error/403"; // Bileti var ama YETKÝSÝ YOKSA (Örn: Öðrenci, Þirket sayfasýna girmeye çalýþýrsa) ona "Yetkisiz Eriþim" sayfasýný göster.
});

builder.Services.AddScoped<ICategoryCommandRepository, CategoryCommandRepository>();
builder.Services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
builder.Services.AddScoped<IContactMessageCommandRepository, ContactMessageCommandRepository>();
builder.Services.AddScoped<INewsletterCommandRepository, NewsletterCommandRepository>();
builder.Services.AddScoped<IStudentCommandRepository, StudentCommandRepository>();
builder.Services.AddScoped<IStudentQueryRepository, StudentQueryRepository>();
builder.Services.AddScoped<IAcademicianCommandRepository, AcademicianCommandRepository>();
builder.Services.AddScoped<IAcademicianQueryRepository, AcademicianQueryRepository>();
builder.Services.AddScoped<ICompanyCommandRepository, CompanyCommandRepository>();
builder.Services.AddScoped<ICompanyQueryRepository, CompanyQueryRepository>();
builder.Services.AddScoped<IInstitutionCommandRepository, InstitutionCommandRepository>();
builder.Services.AddScoped<IInstitutionQueryRepository, InstitutionQueryRepository>();
builder.Services.AddScoped<INotificationQueryRepository, NotificationQueryRepository>();
builder.Services.AddScoped<INotificationCommandRepository, NotificationCommandRepository>();
builder.Services.AddScoped<IMessageQueryRepository, MessageQueryRepository>();
builder.Services.AddScoped<IMessageCommandRepository, MessageCommandRepository>();
builder.Services.AddScoped<IAdminQueryRepository, AdminQueryRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IProjectCommandRepository, ProjectCommandRepository>();
builder.Services.AddScoped<IProjectQueryRepository, ProjectQueryRepository>();
builder.Services.AddScoped<IProjectApplicationCommandRepository, ProjectApplicationCommandRepository>();
builder.Services.AddScoped<IProjectApplicationQueryRepository, ProjectApplicationQueryRepository>();


builder.Services.AddScoped<IImageService, ImageService>();



// 1. Dapper için IDbConnection Kaydý (Veritabaný Baðlantýsý)
// Sistem ne zaman bir IDbConnection istese, ona appsettings.json'daki baðlantý cümlesiyle bir SqlConnection verecek.
builder.Services.AddTransient<IDbConnection>(sp =>
	new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

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
	app.UseExceptionHandler("/Error/500");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/{0}");

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
