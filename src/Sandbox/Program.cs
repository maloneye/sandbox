using BlazorSpinner;
using BuinessLogic;
using Syncfusion.Blazor;
using Newtonsoft.Json;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

string settings;
using (StreamReader reader = new(@"private/private.json"))
{ 
    settings = reader.ReadToEnd();
}

WebsiteSettings webSettings = JsonConvert.DeserializeObject<WebsiteSettings>(settings)!;
var database = new MySQLDatabase();
database.Initalise(webSettings);

// Add services to the container.
builder.Services.AddSyncfusionBlazor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<SpinnerService>();
builder.Services.AddSingleton<IDatabase>(database);

var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(webSettings.SfKey);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
