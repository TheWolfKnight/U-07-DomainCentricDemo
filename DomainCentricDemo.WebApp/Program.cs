using DomainCentricDemo.Application.Implementation;
using DomainCentricDemo.Application.Interface;
using DomainCentricDemo.Infrastrcture;
using DomainCentricDemo.Infrastrcture.Queries;
using DomainCentricDemo.Infrastrcture.Repositories;
using DomainCentricDemo.WebApp.Data;
using DomainCentricDemo.WebApp.Policies;
using DomainCentricDemo.WebApp.Policies.Requirement;
using DomainCentricDemo.WebApp.Policies.Handler;
using DomainCentricDemo.WebApp.Policies.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// add-migration Init -Project DomainCentricDemo.WebApp -Context ApplicationDbContext
// update-database -Project DomainCentricDemo.WebApp -Context ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.Lockout.AllowedForNewUsers = true;
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IAuthorQuery, AuthorQuery>();
builder.Services.AddScoped<IAuthorCommand, AuthorCommand>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddScoped<IBookQuery, BookQuery>();
builder.Services.AddScoped<IBookCommand, BookCommand>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


builder.Services.AddAuthorization(config => {
    config.AddPolicy(Policy.IS_ADMIN_ONLY.GetName(), policyBuilder => {
        policyBuilder.RequireClaim(ClaimsType.Admin);
    });
    config.AddPolicy(Policy.IS_SOLE_AUTHOR_OR_ADMIN.GetName(), policyBuilder => {
        policyBuilder.Requirements.Add(new IsSoleAuthorOrAdminRequirement());
    });
});

builder.Services.AddScoped<IAuthorizationHandler, IsSoleAuthorOrAdminHandler>();

builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultBookConnection"), opt => {
            opt.MigrationsAssembly("DomainCentricDemo.Infrastrcture");
        }
    )
);

// add-migration Init -Project DomainCentricDemo.Infrastrcture -Context BookContext
// update-database -Project DomainCentricDemo.Infrastrcture -Context BookContext

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
