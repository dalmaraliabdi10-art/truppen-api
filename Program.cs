using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TruppenApi.Data;
using TruppenApi.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container. varför man använder AddJsonOptions för att konvertera enum till string i JSON-responsen istället för nummer.
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFileStorageService, FileStorageService>(); // Registrerar FileStorageService som implementation av IFileStorageService i DI-containern.

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>(); // Registrerar PlayerRepository som implementation av IPlayerRepository i DI-containern. 
// Detta gör att när IPlayerRepository efterfrågas, kommer PlayerRepository att användas.
builder.Services.AddScoped<IPlayerService, PlayerService>(); // Registrerar PlayerService som implementation av IPlayerService i DI-containern.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build(); // Configure the HTTP request pipeline. Varför man använder UseSwagger och UseSwaggerUI
// det är för att generera dokumentation och testgränssnitt för API:et.

// Skapar databasfilen vid uppstart om den inte redan finns
// Detta är användbart för utveckling och testning. 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    DbSeeder.Seed(db);
}
// Om applikationen körs i utvecklingsmiljö, aktivera Swagger för att generera dokumentation och testgränssnitt för API:et.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(); // Aktiverar statiska filer så att bilder som laddas upp kan nås via webbläsaren.

// Aktivera HTTPS-omdirigering för att säkerställa att alla HTTP-förfrågningar omdirigeras till HTTPS.
// Tog bort app.UseHttpsRedirection(); eftersom det kan orsaka problem vid lokal utveckling om man inte har ett giltigt SSL-certifikat. Samt vid mobile 
// ska anslutas över HTTP, så det är bättre att inte tvinga HTTPS i utvecklingsmiljö.
app.MapControllers();

app.Run();