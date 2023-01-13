using ETradeBackend.Application.Validators.Product;
using ETradeBackend.Infrastructure.Filters;
using ETradeBackend.Persistance;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

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
