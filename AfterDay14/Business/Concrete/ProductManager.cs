using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcers.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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
        // Başka Entitylere ait hiç bir dalı burada enjekte edemeyiz.
        // Yani her Entity ancak ve ancak kendi Dal'ını enjekte edebilir.
        // Başka entityler için service'leri enjekte ederiz.
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            // Bu kod refactor edilerek Core\CrossCuttingConcers\Validation\ValidationTool'a taşınmış ve yapılandırılmıştır.
            //var context = new ValidationContext<Product>(product);
            //ProductValidator productValidator = new ProductValidator();
            //var result = productValidator.Validate(context);
            //if(!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}

            // Yukarıdaki kodu ilgili yere taşıdıktan sonra Validation için bu satırı yazıyoruz.
            //ValidationTool.Validate(new ProductValidator(), product);
            // Yukarıdaki kod Core katmanına eklediğimiz
            // Aspect\Autofac\Validation\ValidationAspect kodunda sonra kaldırılmıştır.Refactor edilmiştir.

            IResult result = BusinessRules.Run(CheckIfProductCountCategoryCorrect(product.CategoryId),
                              CheckIfProductNameExists(product.ProductName),CheckIfCategoryLimitExceded());
            
            if(result!=null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

            /* Business Rules - İş Kuralları Servisi ekleyip Refactor ettiğimiz için gerek kalmadı
            if (CheckIfProductCountCategoryCorrect(product.CategoryId).Success)
            {
                if(CheckIfProductNameExists(product.ProductName).Success)
                {
                    _productDal.Add(product);
                    return new SuccessResult(Messages.ProductAdded);
                }                
            }
            return new ErrorResult();
            */
            
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }
        
        public IDataResult<List<Product>> GetAll()
        {
            if(DateTime.Now.Hour==15)
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

        private IResult CheckIfProductCountCategoryCorrect(int categoryId)
        {
            if(_productDal.GetAll(p=>p.CategoryId==categoryId).Count>10)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            if(_productDal.GetAll(p=>p.ProductName==productName).Count>0)
            {
                return new ErrorResult();                
            }
            return new SuccessResult();
        }


        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if(result.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExcede);
            }
            return new SuccessResult();
        }

    }
}
