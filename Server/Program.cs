using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Identity.Web;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web.UI;
using Microsoft.OpenApi.Models;
using Graph = Microsoft.Graph;
using YoghurtBank.Shared.Services;
using YoghurtBank.Shared.Infrastructure;
using YoghurtBank.Shared.Model;



/*
// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy
    options.FallbackPolicy = options.DefaultPolicy;
});


*/



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var initialScopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ');

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "YoghurtBank.Server", Version = "v1" });
    c.UseInlineDefinitionsForEnums();
});



var configuration = LoadConfiguration();
var connectionString = configuration.GetConnectionString("Yoghurtbase:connectionString");


//database connection
builder.Services.AddDbContextFactory<YoghurtContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddScoped<IYoghurtContext, YoghurtContext>();
builder.Services.AddScoped<ICollaborationRequestRepository, CollaborationRequestRepository>();
builder.Services.AddScoped<IIdeaRepository, IdeaRepository>();
builder.Services.AddScoped<CollaborationRequestController, CollaborationRequestController>();
builder.Services.AddScoped<IdeaController, IdeaController>();
builder.Services.AddScoped<UserController, UserController>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} 
else 
{
    app.UseWebAssemblyDebugging();
}



app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "YoghurtBank V1"));

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseBlazorFrameworkFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");



await app.SeedAsync();

static IConfiguration LoadConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddUserSecrets<Program>();

    return builder.Build();
}

app.Run();
