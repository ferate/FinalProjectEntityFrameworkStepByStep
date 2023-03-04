using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Bu baðlantý yöntemi Business Katmanýna Taþýndýðý Ýçin Kapatýlmýþtýr
// Business\DependencyResolvers\Autofac\AutofacBusinessModule.cs oluþturulmuþtur. Ve Autofac paketi yüklenmiþtir.
//builder.Services.AddSingleton<IProductService, ProductManager>();
//builder.Services.AddSingleton<IProductDal, EfProductDal>();

/*     .net 6 da Autofac Eklenmesi / Çalýþtýrýlmasý Ýçin Kullanýlan Örnek Kod Parçasý */
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//// Register services directly with Autofac here. Don't
//// call builder.Populate(), that happens in AutofacServiceProviderFactory.
//builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new MyApplicationModule()));
//var app = builder.Build();

//14.gün dersinde ekledim
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


/*  Autofac Eklentisinin Çalýþmasý için eklenen kod parçasý baþlangýç */
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
/*  Autofac Eklentisinin Çalýþmasý için eklenen kod parçasý bitiþ */


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// 14.gün ders sonu bu kýsmý baþka birinin github hesabýndan alarak ekledim
//****** BAÞLANGIÇ

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)

        };

    });

builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });


//****** BÝTÝÞ

// 17.Gün Dersinde ekledim. Frontend tarafýndan verilere ulaþýlmasýna izin verilmesi için kullanýlan servis
// Aþaðýda adres ayarlamasýnýn yapýlmasý gereken kýsým da var. Unutma
builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  HOCAYLA YAZDIÐIMIZ BU KISMI ÝPTAL ETTÝM
//// 14.gün dersinde yazdýk authorization için
//IConfiguration configuration = builder.Configuration; 
//var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 17. Gün dersinde ekledim. Frontend tarafýnýn Backend'e ulaþýmýna izin verilen kýsým.
app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

//app.UseServiceProviderFactory
app.UseHttpsRedirection();

//14.gün dersinde ekledik
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
