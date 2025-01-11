using Football.Data.Database;
using Football.Data.Repository;
using Football.Logic.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MongoDb") ?? throw new ArgumentNullException("MongoDb connection string is missing");

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton(sp => new MongoDbContext(connectionString, "database"));
builder.Services.AddSingleton<ILeagueRepository, LeagueRepository>();
builder.Services.AddSingleton<IMatchRepository, MatchRepository>();
builder.Services.AddSingleton<IPlayerRepository, PlayerRepository>();
builder.Services.AddSingleton<IPlayerStatRepository, PlayerStatRepository>();

builder.Services.AddSingleton<IPlayerService, PlayerService>();
builder.Services.AddSingleton<IMatchService, MatchService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
