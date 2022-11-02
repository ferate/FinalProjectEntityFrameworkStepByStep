using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result;
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

        public IResult Add(Product product)
        {
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }
        
        public IDataResult<List<Product>> GetAll()
        {
            if(DateTime.Now.Hour==10)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }
        
        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.GetById(p => p.ProductId == id),Messages.ProductShowed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if(System.DateTime.Now.Hour==15)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult< List < ProductDetailDto >>(_productDal.GetProductDetails(),Messages.ProductDetailListed);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
