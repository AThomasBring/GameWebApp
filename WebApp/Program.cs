using WebAPI.GameLogic;

var builder = WebApplication.CreateBuilder(args);
int boardSize = 4;
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddRouting();
builder.Services.AddSingleton(sp => new Game(boardSize));
builder.Services.AddScoped<GameLogic>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Game}/{action=Index}/{id?}");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
