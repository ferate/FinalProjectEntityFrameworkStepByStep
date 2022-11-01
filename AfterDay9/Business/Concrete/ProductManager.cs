using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        // Burada Inject edeceğimiz yapının temel Interfaceini veririz.
        // Burada ne InMemory ne de EntityFramework geçmemeli
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }
        
        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }
        
        public Product GetById(int id)
        {
            return _productDal.GetById(p => p.ProductId == id);
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }
    }
}
