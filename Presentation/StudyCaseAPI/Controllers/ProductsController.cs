using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyCase.Application.Repositories;
using StudyCase.Domain.Entities;

namespace StudyCaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var data =  _productReadRepository.GetAll();
            return Ok(data);
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string id)
        {

            var result = await _productReadRepository.GetByIdAsync(id);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(Product product)
        {
            var result =await _productWriteRepository.AddAsync(product);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

    }
}
