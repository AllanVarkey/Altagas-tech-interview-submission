using Microsoft.EntityFrameworkCore;
using TMS.Automapper;
using TMS.Components;
using TMS.Data.DatabaseContext;
using TMS.Data.Datastore;
using TMS.Data.Interfaces;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(config => config.AddProfile<TMSAutoMapperProfile>(), typeof(TMSAutoMapperProfile).Assembly);
builder.Services.AddScoped<ITripsRepository, TripsDatastore>();
builder.Services.AddScoped<ICityRepository, CityDatastore>();
builder.Services.AddScoped<IRailCarEventRecordRepository, RailCarEventRecordDatastore>();
// add the dbcontext. the connection string in the appsettings.json is a docker instance of mssql2022. the database is called TMS
builder.Services.AddDbContext<TMSDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options.Title = "TMS";
    options.OpenApiRoutePattern = "/openapi/v1.json";
});
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(TMS.Client._Imports).Assembly);

app.Run();
