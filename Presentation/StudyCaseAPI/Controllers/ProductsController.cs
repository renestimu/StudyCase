using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyCase.Application.Repositories;
using StudyCase.Domain.Entities;
using System.Globalization;

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
        [HttpGet("test")]
        public async void Get()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new(){Name="Product 1",Description="ürün1",Price=100,CreateTime=DateTime.UtcNow,Stock=10},
                new(){Name="Product 2",Description="ürün1",Price=200,CreateTime=DateTime.UtcNow,Stock=20},
                new(){Name="Product 3",Description="ürün1",Price=300,CreateTime=DateTime.UtcNow,Stock=30},
            });
            await _productWriteRepository.SaveAsync();
        }



        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var data = _productReadRepository.GetAll();
            return Ok(data);
        }
        [HttpGet("getbyname")]
        public IActionResult GetByName(string name)
        {

            var result = _productReadRepository.GetWhere(x => x.Name.Contains(name));
            if (result == null)
                return BadRequest();
            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(Product product)
        {
            var result = await _productWriteRepository.AddAsync(product);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

       
        [HttpPost("addrandom")]
        public async Task<IActionResult> AddRandom()
        {
            List<Product> result;

            //burayı böyle yapmak istemezdim ama hemen için biraz 
            using (TextReader fileReader = new StreamReader("MOCK_DATA.csv"))
            {
                using (var csv = new CsvReader(fileReader, CultureInfo.InvariantCulture))
                {
                    try
                    {
                        csv.Context.RegisterClassMap<ProductMap>();
                        result = csv.GetRecords<Product>().ToList();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);

                    }

                }
            }
            if (result.Count > 0)
            {
                if (result.Count > 11)
                {
                    Random random = new Random();
                    int num = random.Next(1, result.Count - 11);
                    if (num + 10 < result.Count)
                    {
                        List<Product> randomTenProduct = result.Skip(num).Take(10).Select(s=>new Product
                        {
                            Name = s.Name,
                            Description = s.Description,
                            CreateTime = s.CreateTime.ToUniversalTime(),
                            Stock = s.Stock,
                            Price = s.Price,
                        }).ToList();
                        try
                        {
                            var result2 = await _productWriteRepository.AddRangeAsync(randomTenProduct);
                            await _productWriteRepository.SaveAsync();
                            if (result2)
                            {
                                return Ok(result2);
                            }
                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.Message);

                        }


                    }
                }
            }


            return BadRequest();

        }

    }
}
