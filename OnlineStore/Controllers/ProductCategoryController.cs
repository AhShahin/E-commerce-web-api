using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.UOW;
using OnlineStore.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductCategories()
        {
            var categories = await _unitOfWork.ProductCategoryRepository.getCategories();
            var categoriesToReturn = _mapper.Map<IEnumerable<ProductCategoryForListDto>>(categories);
            return Ok(categoriesToReturn);
        }
    }
}
