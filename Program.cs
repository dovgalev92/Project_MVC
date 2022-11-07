using Microsoft.EntityFrameworkCore;
using Project_MVC.Context_DataBase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContextDb>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("�� ������� ������������ � ���� ������ ���� ��� �� �������")));

builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
