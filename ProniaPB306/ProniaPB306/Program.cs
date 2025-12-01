using Microsoft.EntityFrameworkCore;
using ProniaPB306.DAL;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});



//builder.Services.AddScoped<IEmailService,TestService>();

//builder.Services.AddSingleton<EmailService>();

//builder.Services.AddTransient<EmailService>();

var app = builder.Build();


app.UseStaticFiles();
app.MapControllerRoute(
    "area",
    "{area:exists}/{controller=home}/{action=index}/{id?}"
    );

app.MapControllerRoute(
    "default",
    "{controller=home}/{action=index}/{id?}"
    );



app.Run();
