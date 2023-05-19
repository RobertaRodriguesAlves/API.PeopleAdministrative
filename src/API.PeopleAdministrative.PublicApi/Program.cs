using API.PeopleAdministrative.Application;
using API.PeopleAdministrative.Infrastructure;
using API.PeopleAdministrative.Infrastructure.Data.Context;
using API.PeopleAdministrative.PublicApi.Extensions;
using API.PeopleAdministrative.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO.Compression;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KestrelServerOptions>(opt => opt.AddServerHeader = false);
builder.Services.Configure<RouteOptions>(opt => opt.LowercaseUrls = true);
builder.Services.Configure<GzipCompressionProviderOptions>(opt => opt.Level = CompressionLevel.Fastest);
builder.Services.Configure<MvcNewtonsoftJsonOptions>(opt => opt.SerializerSettings.Configure());

//builder.Services.ConfigureAppSettings();
builder.Services.AddInfrastructure();
builder.Services.AddAppServices();
builder.Services.AddPeopleAdministrativeDbContext();

builder.Services.AddCors();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddResponseCompression(opt => opt.Providers.Add<GzipCompressionProvider>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.WebHost.UseDefaultServiceProvider(opt =>
{
    opt.ValidateScopes = builder.Environment.IsDevelopment();
    opt.ValidateOnBuild = true;
});

var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddHealthChecks(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseSwaggerAndUI();
app.UseHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResponseWriter = HealthCheckExtensions.WriteResponse
});
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseRouting();
app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Verifica se há alguma migração pendente e aplica se necessário
await using var scope = app.Services.CreateAsyncScope();
await using var context = scope.ServiceProvider.GetRequiredService<PeopleAdministrativeContext>();

if ((await context.Database.GetPendingMigrationsAsync()).Any())
{
    await context.Database.MigrateAsync();
}

app.Run();
