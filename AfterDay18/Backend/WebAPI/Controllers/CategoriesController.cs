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
    public class CategoriesController : ControllerBase
    {
        //Loosely Coupled
        ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        
        [HttpGet("getall")]        
        public IActionResult GetAll()
        {            
            var result = _categoryService.GetAll();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);             
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _categoryService.GetById(id);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /*
        [HttpPost("add")]
        
        public IActionResult Add(Category category)
        { 
            var result = _categoryService.Add(category);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
           
        }

        [HttpPost("transactiontest")]
        public IActionResult TransactionTest(Category category)
        {
           
            var result = _categoryService.TransactionalOperation(category);
            
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
          

        }
        */

    }
}
