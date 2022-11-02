using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loosely Coupled
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //[HttpGet]  Bunu alias vererek yeniden düzenledik
        [HttpGet("getall")]
        // public List<Product> Get() Bunu IActionResult'a dönüştürdük
        public IActionResult GetAll()
        {
            // İlk başta bu kısmı yazdık daha sonra bunu genele taşıyacağız
            // dependency injection ile
            //IProductService productService = new ProductManager(new EfProductDal());
            var result = _productService.GetAll();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);             
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //[HttpPost] Alias verilerek yeniden düzenlendi
        [HttpPost("add")]
        //public IActionResult Post(Product product) //aliasla uyumlu olması açısından isim Add olarak düzenlendi
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
