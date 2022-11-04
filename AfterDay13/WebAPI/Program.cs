using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

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

/*  Autofac Eklentisinin �al��mas� i�in eklenen kod par�as� ba�lang�� */
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
/*  Autofac Eklentisinin �al��mas� i�in eklenen kod par�as� biti� */



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

//app.UseServiceProviderFactory
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
