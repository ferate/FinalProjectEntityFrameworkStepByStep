// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

Console.WriteLine(" *** Merhaba Katmanlı Mimari Projesine Hoşgeldiniz ***");

//ProductManager productManager = new ProductManager(new InMemoryProductDal());



//Product yeniUrun = new Product() { CategoryID = 1, ProductName = "Bardak", UnitPrice = 15, UnitsInStock = 15 };

//productManager.Add(yeniUrun);

//Product silinecekUrun  = new Product() { ProductID = 78,CategoryId = 1, ProductName = "Bardak", UnitPrice = 15, UnitsInStock = 15 };

//productManager.Delete(silinecekUrun);

ProductManager productManager = new ProductManager(new EfProductDal());

foreach (var product in productManager.GetAll(p=>p.CategoryId==2))
{
    Console.WriteLine("{0} - {1} - {2} ",product.ProductId,product.ProductName,product.CategoryId);
}