using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

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

/*  Autofac Eklentisinin Çalýþmasý için eklenen kod parçasý baþlangýç */
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
/*  Autofac Eklentisinin Çalýþmasý için eklenen kod parçasý bitiþ */



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
