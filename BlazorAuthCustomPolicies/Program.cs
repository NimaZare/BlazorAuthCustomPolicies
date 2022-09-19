using BlazorAuthCustomPolicies.Areas.Identity;
using BlazorAuthCustomPolicies.Data;
using BlazorAuthCustomPolicies.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();
// NEW MY CODES
builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>(); // step 2
builder.Services.AddSingleton<IAuthorizationHandler, CertifiedMinimumHandler>(); // step 3
builder.Services.AddSingleton<IAuthorizationHandler, AdminClaimHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Administrator")); // step 1
    options.AddPolicy("AdminPolicy", policy => policy.Requirements.Add(new AdminClaimRequirement(true)));
    // single requirement policy
    options.AddPolicy("AtLast18", policy => policy.Requirements.Add(new MinimumAgeRequirement(18))); // step 1
    // multiple requirement policy
    options.AddPolicy("IsExpertCertifiedAnd18", policy => policy.Requirements.Add(new CertifiedMinimumRequirement(true, 5))); // step 3
});  // step 1

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
