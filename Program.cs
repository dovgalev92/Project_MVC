using Microsoft.EntityFrameworkCore;
using Project_MVC.Context_DataBase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookies")
    .AddCookie(option =>
    {
        option.LoginPath = "/Access/LogInUser";
    });
builder.Services.AddAuthorization();

builder.Services.AddDbContext<ContextDb>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Не удалось подключиться к базе данных либо она не создана")));

builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ListUser}/{action=List_User}/{id?}");

app.Run();
