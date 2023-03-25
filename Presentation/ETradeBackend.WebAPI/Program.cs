using System.Security.Claims;
using ETradeBackend.Application;
using ETradeBackend.Application.Validators.Product;
using ETradeBackend.Infrastructure;
using ETradeBackend.Infrastructure.Enums;
using ETradeBackend.Infrastructure.Filters;
using ETradeBackend.Infrastructure.Services.Storage.LocalStorage;
using ETradeBackend.Persistance;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ETradeBackend.SignalR;
using ETradeBackend.WebAPI.Configuraitons.ColumnWriters;
using ETradeBackend.WebAPI.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();

builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage(StorageType.Local);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"),
        "logs",
        needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            {"message", new RenderedMessageColumnWriter()},
            {"message_template", new MessageTemplateColumnWriter()},
            {"level", new LevelColumnWriter()},
            {"time_stamp" , new TimestampColumnWriter()},
            {"exception" , new ExceptionColumnWriter()},
            {"log_event", new LogEventSerializedColumnWriter()},
            {"user_name", new UsernameColumnWriter()}
        }
        ).Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(log);

// .Net her istek att�g�m�zda IAsyncActionFilter arabirimi ile iste�in aras�na girebilmemizi ve belirli i�lemler yapabilmemize olanak sa�lar. Biz a�a��da ValidationFilter ad�nda 
// IAsyncActionFilter aray�z�n� implemente eden bir s�n�f olu�turduk ve bu s�n�f� asa��daki gibi ekledik. 2 Alt sat�rda da AddFluentValidation diyerek bizim uygulamam�z validasyon 
// olarak fluent validasyon kullanacak dedik. RegisterValidatorsFromAssemblyContaining diyerek program�n assembly seviyesindeki b�t�n FluentValidation kullanan s�n�flar�n� taray�p sisteme dahil edecek.
// Sonu� olarak biz AbstractValidator<> s�n�f�ndan t�retilmi� her bir validation nesnemizi sisteme otomatik tan�tm�� olaca��z ve 1. Sat�rdaki ValidationFilter ile valide olmayan alanlar� se�ip
// d�n�� olarak errorlar� d�nece�iz.
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
        options.TokenValidationParameters = new()
        {
            ValidateAudience =
            true, //olu�turulacak token de�erini kimlerin/hangi originlerin/sitelerin kullanacag�n� belirledi�imiz de�erdir. �rn www.ugurcimen.com
            ValidateIssuer = true, //olu�turulan tokeni kimin da��tt���n� bildirdi�imiz aland�r. �rn www.myapi.com
            ValidateLifetime = true, //olu�turulan tokenin s�resini kontrol edecek olan alandir.
            ValidateIssuerSigningKey =
            true, //�retilecek token de�erinin uygulamam�za ait oldu�unu bildiren bir de�er oldu�unu ifade eden securityKey verisinin do�rulamas�d�r.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.Name //JWT �zerinde Name claimine kar��l�k gelen de�eri User.Identity.Name propertysinden elde edebiliriz.
        }).AddJwtBearer("Manav", options => options.TokenValidationParameters = new()
        {
            ValidateAudience =
                true, //olu�turulacak token de�erini kimlerin/hangi originlerin/sitelerin kullanacag�n� belirledi�imiz de�erdir. �rn www.ugurcimen.com
            ValidateIssuer = true, //olu�turulan tokeni kimin da��tt���n� bildirdi�imiz aland�r. �rn www.myapi.com
            ValidateLifetime = true, //olu�turulan tokenin s�resini kontrol edecek olan alandir.
            ValidateIssuerSigningKey =
                true, //�retilecek token de�erinin uygulamam�za ait oldu�unu bildiren bir de�er oldu�unu ifade eden securityKey verisinin do�rulamas�d�r.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            NameClaimType = ClaimTypes.Name
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});

app.MapControllers();
app.MapHubs();


app.Run();
