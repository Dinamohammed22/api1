using api1.Models;
using api1.Repositories.RepoInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    { 
        public IProductRepository ProductRepository { get; }

        public ProductController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

       

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = ProductRepository.GetAll();
            return Ok(products);
        }
        
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Product product = ProductRepository.Get(id);
            return Ok(product);
        }

        [HttpPut]
        public IActionResult updat(int id , Product UpdatedProduct)
        {
            bool IsNotNull = ProductRepository.Update(id , UpdatedProduct);
            if (!IsNotNull)
            {
                return BadRequest();
            }
            ProductRepository.save();
            return CreatedAtAction("Get",new {id = UpdatedProduct.id} , UpdatedProduct);
        }

        [HttpPost]
        public IActionResult Insert(Product UpdatedProduct)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.insert(UpdatedProduct);

                ProductRepository.save();
                return CreatedAtAction("Get", new { id = UpdatedProduct.id }, UpdatedProduct);
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Remove(int id)
        {
            bool IsNotNull = ProductRepository.Delete(id);
            if (!IsNotNull)
            {
                return BadRequest();
            }
            ProductRepository.save();
            return Created();
        }



    }

    
}
