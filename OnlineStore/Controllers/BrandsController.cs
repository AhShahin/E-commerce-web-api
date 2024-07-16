using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.UOW;
using OnlineStore.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandsController(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET: api/<BrandsController>
        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _unitOfWork.BrandRepository.GetBrands();
            var brandToReturn = _mapper.Map<IEnumerable<BrandForListDto>>(brands);

            if (brands == null)
            {
                return NotFound();
            }

            return Ok(brandToReturn);
        }

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BrandsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BrandsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
