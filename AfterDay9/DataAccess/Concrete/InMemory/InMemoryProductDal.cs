using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product>()
            {
                new Product(){ CategoryId=1, ProductName="Bardak", UnitPrice=15, UnitsInStock=15},
                new Product(){ CategoryId=1, ProductName="Kamera", UnitPrice=500, UnitsInStock=3},
                new Product(){ CategoryId=2, ProductName="Telefon", UnitPrice=1500, UnitsInStock=2},
                new Product(){ CategoryId=2, ProductName="Klavye", UnitPrice=150, UnitsInStock=65},
                new Product(){ CategoryId=2, ProductName="Fare", UnitPrice=85, UnitsInStock=1}

            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product productToDelete;
            // LINQ ve Standart Kullanım Gösteriyoruz
            // LINQ ( Language Integrated Query - Dile Bağlı Sorgulama)
            //  => bu işaret lambda işareti

            // LINQ KULLANDIĞIMIZ İÇİN BU YAPIYA GEREK YOK TEK SATIRDA İŞİMİZİ ÇÖZÜYORUZ
            //foreach (var p in _products)
            //{
            //    if (product.ProductId == p.ProductId)
            //    {
            //        productToDelete = p;
            //    }
            //}

            // p için her bir p deki ProductId si benim gönderdiğim product daki ProductId ye eşit olanları getir
            // SingleOrDefault  1 tane arar genelde ID aramalarda kullanılır
            // SingleOrDefault komutu liste içerisinde tek tek dolaşmaya yarar.
            // verdiğimiz p foreach yapısındaki gibi bir değişkendir.
            productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);

            _products.Remove(productToDelete);
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetByCategory(int categoryId)
        {
           //LINQ Komutu
           return _products.Where(p => p.CategoryId==categoryId).ToList();
           //return _products.Where(p => p.CategoryId == categoryId && ve || veya diyerek daha çok şart ekleyebiliriz).ToList();
        }

        public void Update(Product product)
        {
            // Gönderdiğim ürün id sine sahip olan listedeki ürünü bul.
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;            
        }
    }
}
