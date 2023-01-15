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

// Bu ba�lant� y�ntemi Business Katman�na Ta��nd��� ��in Kapat�lm��t�r
// Business\DependencyResolvers\Autofac\AutofacBusinessModule.cs olu�turulmu�tur. Ve Autofac paketi y�klenmi�tir.
//builder.Services.AddSingleton<IProductService, ProductManager>();
//builder.Services.AddSingleton<IProductDal, EfProductDal>();

/*     .net 6 da Autofac Eklenmesi / �al��t�r�lmas� ��in Kullan�lan �rnek Kod Par�as� */
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//// Register services directly with Autofac here. Don't
//// call builder.Populate(), that happens in AutofacServiceProviderFactory.
//builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new MyApplicationModule()));
//var app = builder.Build();

//14.g�n dersinde ekledim
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


/*  Autofac Eklentisinin �al��mas� i�in eklenen kod par�as� ba�lang�� */
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
/*  Autofac Eklentisinin �al��mas� i�in eklenen kod par�as� biti� */


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// 14.g�n ders sonu bu k�sm� ba�ka birinin github hesab�ndan alarak ekledim
//****** BA�LANGI�

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


//****** B�T��

// 17.G�n Dersinde ekledim. Frontend taraf�ndan verilere ula��lmas�na izin verilmesi i�in kullan�lan servis
// A�a��da adres ayarlamas�n�n yap�lmas� gereken k�s�m da var. Unutma
builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  HOCAYLA YAZDI�IMIZ BU KISMI �PTAL ETT�M
//// 14.g�n dersinde yazd�k authorization i�in
//IConfiguration configuration = builder.Configuration; 
//var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 17. G�n dersinde ekledim. Frontend taraf�n�n Backend'e ula��m�na izin verilen k�s�m.
app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

//app.UseServiceProviderFactory
app.UseHttpsRedirection();

//14.g�n dersinde ekledik
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
