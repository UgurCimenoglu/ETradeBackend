using ETradeBackend.Application.Validators.Product;
using ETradeBackend.Infrastructure.Filters;
using ETradeBackend.Persistance;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
