using Core.Utilities.Result;
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

        IDataResult<List<Product>> GetAll();
        IDataResult<Product> GetById(int id);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(Product product);

        IResult TransactionalOperation(Product product);
        IDataResult<List<Product>> GetAllByCategoryId(int categoryId);
    }
}
