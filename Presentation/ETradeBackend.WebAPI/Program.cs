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
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage(StorageType.Local);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

// .Net her istek attýgýmýzda IAsyncActionFilter arabirimi ile isteðin arasýna girebilmemizi ve belirli iþlemler yapabilmemize olanak saðlar. Biz aþaðýda ValidationFilter adýnda 
// IAsyncActionFilter arayüzünü implemente eden bir sýnýf oluþturduk ve bu sýnýfý asaðýdaki gibi ekledik. 2 Alt satýrda da AddFluentValidation diyerek bizim uygulamamýz validasyon 
// olarak fluent validasyon kullanacak dedik. RegisterValidatorsFromAssemblyContaining diyerek programýn assembly seviyesindeki bütün FluentValidation kullanan sýnýflarýný tarayýp sisteme dahil edecek.
// Sonuç olarak biz AbstractValidator<> sýnýfýndan türetilmiþ her bir validation nesnemizi sisteme otomatik tanýtmýþ olacaðýz ve 1. Satýrdaki ValidationFilter ile valide olmayan alanlarý seçip
// dönüþ olarak errorlarý döneceðiz.
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options => options.TokenValidationParameters = new()
    {
        ValidateAudience =
            true, //oluþturulacak token deðerini kimlerin/hangi originlerin/sitelerin kullanacagýný belirlediðimiz deðerdir. Örn www.ugurcimen.com
        ValidateIssuer = true, //oluþturulan tokeni kimin daðýttýðýný bildirdiðimiz alandýr. Örn www.myapi.com
        ValidateLifetime = true, //oluþturulan tokenin süresini kontrol edecek olan alandir.
        ValidateIssuerSigningKey =
            true, //Üretilecek token deðerinin uygulamamýza ait olduðunu bildiren bir deðer olduðunu ifade eden securityKey verisinin doðrulamasýdýr.

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
    }).AddJwtBearer("Manav", options => options.TokenValidationParameters = new()
    {
        ValidateAudience =
            true, //oluþturulacak token deðerini kimlerin/hangi originlerin/sitelerin kullanacagýný belirlediðimiz deðerdir. Örn www.ugurcimen.com
        ValidateIssuer = true, //oluþturulan tokeni kimin daðýttýðýný bildirdiðimiz alandýr. Örn www.myapi.com
        ValidateLifetime = true, //oluþturulan tokenin süresini kontrol edecek olan alandir.
        ValidateIssuerSigningKey =
            true, //Üretilecek token deðerinin uygulamamýza ait olduðunu bildiren bir deðer olduðunu ifade eden securityKey verisinin doðrulamasýdýr.

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("azsd"))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
