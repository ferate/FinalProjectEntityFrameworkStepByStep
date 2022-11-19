// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using Core.Utilities.Result;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

Console.WriteLine(" *** Merhaba Katmanlı Mimari Projesine Hoşgeldiniz ***");

//ProductEklemeSilmeListeleme();

//KategoriEklemeSilmeListeleme();

//EmployeeEklemeSilmeListeleme();

ProductManager productManager = new ProductManager(new EfProductDal(),
                                new CategoryManager(new EfCategoryDal()));

var resultProductDetail = productManager.GetProductDetails();
if (resultProductDetail.Success)
{
    foreach (var detail in resultProductDetail.Data)
    {
        Console.WriteLine("Product Id: {0}, Product Name: {1}, Category Name: {2}, UnitsInStock: {3}",
            detail.ProductId, detail.ProductName, detail.CategoryName, detail.UnitsInStock);
    }

}

Console.WriteLine(resultProductDetail.Message);



static void ProductEklemeSilmeListeleme()
{
    //ProductManager productManager = new ProductManager(new InMemoryProductDal());

    Console.WriteLine(" *** ÜRÜNLER LİSTELENİYOR ***");

    ProductManager productManager = new ProductManager(new EfProductDal(),
                                    new CategoryManager(new EfCategoryDal()));
    var resultProducts = productManager.GetProductDetails();
    foreach (var product in resultProducts.Data)
    {
        Console.WriteLine("{0} - {1} - {2} ", product.ProductId, product.ProductName, product.CategoryName);
    }

    Product yeniUrun = new Product() { CategoryId = 1, ProductName = "Bardak", UnitPrice = 15, UnitsInStock = 15 };

    productManager.Add(yeniUrun);

    Console.WriteLine(" *** ÜRÜN EKLENİP LİSTELENİYOR ***");

    var resultAfterUpdateProducts = productManager.GetProductDetails();

    foreach (var product in resultAfterUpdateProducts.Data)
    {
        Console.WriteLine("{0} - {1} - {2} ", product.ProductId, product.ProductName, product.CategoryName);
    }

    Product silinecekUrun = new Product() { ProductId = 79, CategoryId = 1, ProductName = "Bardak", UnitPrice = 15, UnitsInStock = 15 };

    productManager.Delete(silinecekUrun);

    Console.WriteLine(" *** ÜRÜN SİLİNİP LİSTELENİYOR ***");

    var resultAfterDeleteProducts = productManager.GetProductDetails();

    foreach (var product in resultAfterDeleteProducts.Data)
    {
        Console.WriteLine("{0} - {1} - {2} ", product.ProductId, product.ProductName, product.CategoryName);
    }
}

static void KategoriEklemeSilmeListeleme()
{
    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

    Console.WriteLine(" *** Kategoriler Listeleniyor ***");

    foreach (var item in categoryManager.GetAll())
    {
        Console.WriteLine("Category Id :{0} --- Category Name : {1}", item.CategoryId, item.CategoryName);
    }

    Console.WriteLine(" *** Eklenen Kategoriler İle Listeleniyor ***");
    Category category = new Category() { CategoryName = "Deneme" };
    categoryManager.Add(category);

    foreach (var item in categoryManager.GetAll())
    {
        Console.WriteLine("Category Id :{0} --- Category Name : {1}", item.CategoryId, item.CategoryName);
    }
    Console.WriteLine(" *** Silinen Kategoriler İle Listeleniyor ***");

    Category silinecekCategory = new Category() { CategoryId = 10 };
    categoryManager.Delete(silinecekCategory);

    foreach (var item in categoryManager.GetAll())
    {
        Console.WriteLine("Category Id :{0} --- Category Name : {1}", item.CategoryId, item.CategoryName);
    }
}

static void EmployeeEklemeSilmeListeleme()
{
    EmployeeManager employeeManager = new EmployeeManager(new EfEmployeeDal());

    Console.WriteLine(" *** Çalışanlar Listeleniyor ***");

    foreach (var emp in employeeManager.GetAll())
    {
        Console.WriteLine("Employee ID: {0} , Employee Name : {1}, Employee Surname : {2}", emp.EmployeeID, emp.FirstName, emp.LastName);
    }

    Console.WriteLine(" *** Eklenen Çalışanlar Listeleniyor ***");

    Employee employee = new Employee() { FirstName = "Ferat", LastName = "EFİL", City = "Samsun" };
    employeeManager.Add(employee);

    foreach (var emp in employeeManager.GetAll())
    {
        Console.WriteLine("Employee ID: {0} , Employee Name : {1}, Employee Surname : {2}", emp.EmployeeID, emp.FirstName, emp.LastName);
    }

    Console.WriteLine(" *** Silinen Çalışanlar Listeleniyor ***");

    Employee silinenEmployee = new Employee() { EmployeeID = 10, FirstName = "Ferat", LastName = "EFİL", City = "Samsun" };
    employeeManager.Delete(silinenEmployee);

    foreach (var emp in employeeManager.GetAll())
    {
        Console.WriteLine("Employee ID: {0} , Employee Name : {1}, Employee Surname : {2}", emp.EmployeeID, emp.FirstName, emp.LastName);
    }
}