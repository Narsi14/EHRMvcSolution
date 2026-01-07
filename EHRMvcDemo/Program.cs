using EHRMvcDemo.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC controllers + views and Razor Pages support
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<EHRDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EHRConnection"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Default route changed to start the app at DoctorSummary/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DoctorSummary}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
