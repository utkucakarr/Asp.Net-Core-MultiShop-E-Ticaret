using MultiShop.SignalRRealTimeApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader() //Her t�rl� HTTP ba�l���n� kabul eder.
               .AllowAnyMethod() //T�m HTTP metodlar�na izin verir (GET, POST, PUT, DELETE, vb.).
               .SetIsOriginAllowed((host) => true) // Herhangi bir kaynaktan gelen isteklere izin verir.
               .AllowCredentials(); //Kimlik bilgilerini (�erezler, oturum bilgilerinin) i�eren isteklere izin verir.
    });
});

builder.Services.AddSignalR();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SignalRHub>("/signalrhub");

app.Run();
