using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        //List<Product> GetAll(Expression<Func<Product, bool>> filter = null);

        List<Product> GetAll();
        Product GetById(int id);
        List<ProductDetailDto> GetProductDetails();
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
