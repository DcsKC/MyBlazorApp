using Microsoft.EntityFrameworkCore;
using MyBlazorApp.Components;
using MyBlazorApp.Data;
using MyBlazorApp.Models;
using MyBlazorApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<TimeTrackingService>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!dbContext.Employees.Any())
    {
        dbContext.Employees.Add(new Employee
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Department = "IT"
        });
        dbContext.Employees.Add(new Employee
        {
            Name = "Jane Smith",
            Email = "jane.smith@example.com",
            Department = "HR"
        });
        dbContext.SaveChanges();
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
