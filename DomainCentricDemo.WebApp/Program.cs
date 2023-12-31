using DomainCentricDemo.Application.Implementation;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.Infrastrcture;
using DomainCentricDemo.Infrastrcture.Queries;
using DomainCentricDemo.Infrastrcture.Repositories;
using DomainCentricDemo.WebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IAuthorQuery, AuthorQuery>();
builder.Services.AddScoped<IAuthorCommand, AuthorCommand>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddScoped<IBookQuery, BookQuery>();
builder.Services.AddScoped<IBookCommand, BookCommand>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultBookConnection"), opt => {
            opt.MigrationsAssembly("DomainCentricDemo.Infrastrcture");
        }));

//add-migration Init -Project DomainCentricDemo.Infrastrcture -Context BookContext

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
}
else {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
