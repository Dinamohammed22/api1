using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api1.Models;
using api1.Repositories.RepoInterface;
using api1.Repositories;
using api1.DTO;
using Microsoft.AspNetCore.Authorization;

namespace api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository CategoryRepository;
        private readonly IProductRepository ProductRepository;

        public CategoryController(ICategoryRepository CategoryRepository , IProductRepository ProductRepository)
        {
            this.CategoryRepository = CategoryRepository;
            this.ProductRepository = ProductRepository;
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPost("{CategoryId}")]
        public IActionResult GetProductsByCatId(int CategoryId)
        {
            List<string> products = ProductRepository.GetProductNamessByCatId(CategoryId);
            string CategoryName = CategoryRepository.GetName(CategoryId);

            CategoryProductsDTO categoryProductsDTO = new CategoryProductsDTO()
            {
                CategoryName = CategoryName,
                CategoryId = CategoryId,
                ProductNames = products
                
            };
            return Ok(categoryProductsDTO);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult GetCategories()
        {
            List<Category> categories = CategoryRepository.GetAll();
            return Ok(categories);
        }



    }
}
