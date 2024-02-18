using S9.Core.Configuration;
using S9.Core.Data.Storage.MsSqlStorage;
using webManagerCMS.Core.Components;
using webManagerCMS.Data.Storage.MsSqlStorage.Access;
using webManagerCMS.Data.Storage;
using webManagerCMS.Data.Tenants;
using webManagerCMS.Core.Tenants;
using webManagerCMS.Data.Models;
using webManagerCMS.Core.Middlewares;
using webManagerCMS.Core.Services.ComponentService;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

// The following is here because the middleware TenantMiddleware need access to HTTP context
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSingleton<ITenantAccess, HttpContextTenantAccess>();// current tenant access

// Application settings
var applicationSettings = new ApplicationSettings();
var applicationSettingsConfigurationSection = builder.Configuration.GetSection("ApplicationSettings");
applicationSettingsConfigurationSection.Bind(applicationSettings);
builder.Services.AddOptions<ApplicationSettings>().Bind(applicationSettingsConfigurationSection);

// Dynamic Components
builder.Services.AddSingleton<IComponentService>(_ =>
{
	var service = new ComponentService(applicationSettings);
	service.LoadDynamicComponents();
	return service;
});


// Data storage access
var dataStorageConfigSettings = new DataStorageConfigSettings();
builder.Configuration.GetSection("DataStorageSettings").Bind(dataStorageConfigSettings);

ConfigureMsSqlDataStorage(builder, dataStorageConfigSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseMiddleware<TenantMiddleware>();

app.MapRazorComponents<App>();

app.Run();


void ConfigureMsSqlDataStorage(WebApplicationBuilder builder, DataStorageConfigSettings dataStorageConfigSettings)
{
	MsSqlDataStorageSettings createMsSqlSettings(string msSqlConnectionStringName)
	{
		var msSqlSettings = new MsSqlDataStorageSettings()
		{
			ConnectionString = builder.Configuration.GetConnectionString(msSqlConnectionStringName)
		};

		if (dataStorageConfigSettings.DbCommandTimeoutSeconds > 0)
			msSqlSettings.CommandTimeout = dataStorageConfigSettings.DbCommandTimeoutSeconds;

		if (!string.IsNullOrWhiteSpace(dataStorageConfigSettings.AlternativeLogFilePath))
			msSqlSettings.AlternativeLogFilePath = dataStorageConfigSettings.AlternativeLogFilePath;

		return msSqlSettings;
	}

	builder.Services.AddSingleton<IDataStorageAccess>(services =>
		new MsSqlDataStorageAccess(
			createMsSqlSettings("MsSqlConnectionString"),
			createMsSqlSettings("MsSqlConnectionStringLog"),
			services.GetService<ITenantAccess>()
	)) ;

	// for logging (see DataStorageLoggerProvider)
	builder.Services.AddSingleton(services => services.GetService<IDataStorageAccess>().LogDataStorage);
}