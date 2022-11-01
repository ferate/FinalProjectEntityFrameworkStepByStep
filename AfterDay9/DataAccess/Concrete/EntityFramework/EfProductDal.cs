using Core.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
      
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from P in context.Products
                             join c in context.Categories
                             on P.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 ProductId = P.ProductId,
                                 ProductName = P.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock = P.UnitsInStock
                             };

                return result.ToList();
            }
        }

    }
}
