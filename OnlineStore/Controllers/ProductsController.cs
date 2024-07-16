using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.UOW;
using OnlineStore.Dtos;
using OnlineStore.Models;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Helpers;
using OnlineStore.Helpers.Validator;
using FluentValidation;
using static OnlineStore.Helpers.Enums;
using Microsoft.AspNetCore.Authorization;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [Route("{gender:alpha}")]
        [HttpGet]
        public async Task<IActionResult> GetProducts(Gender gender, [FromQuery] ProductParams Params)
        {
            
            var items = await _unitOfWork.ProductRepository.GetProducts(gender, Params);

            if (items.Count == 0)
            {
                return NotFound(new { error = "Items not found" });
            }

            // getting claims from accessToken

            var itemsToReturn = _mapper.Map<IEnumerable<ProductForListDto>>(items);

            Response.AddPagination(items.CurrentPage, items.PageSize,
                items.TotalCount, items.TotalPages);

            return Ok(itemsToReturn);
        }

        // GET: api/Items/5
        [HttpGet]
        [Route("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProduct(int id)
        {
            //var claims = User.Claims.ToList();

            var item = await _unitOfWork.ProductRepository.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] ProductForCreationDto productForCreationDto)
        {

            var validator = new ProductCreationValidator();
            FluentValidation.Results.ValidationResult result = validator.Validate(productForCreationDto);

            if (!result.IsValid)
            {
                return Validate(result);
            }

            var product = await _unitOfWork.ProductRepository.Add(_mapper.Map(productForCreationDto, new Product()));

            if (product == null)
            {
                return NotFound();
            }

            if (await _unitOfWork.Complete())
                return Ok(product);

            throw new Exception("Adding product failed on save");
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] ProductForUpdateDto productForUpdateDto)
        {
            var validator = new UpdateProductValidator();
            FluentValidation.Results.ValidationResult result = validator.Validate(productForUpdateDto);

            if (!result.IsValid)
            {
                return Validate(result);
            }

            var product = await _unitOfWork.ProductRepository.GetAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            _mapper.Map(productForUpdateDto, product);

            if (await _unitOfWork.Complete())
                return Ok("Product was successfully updated");

            throw new Exception($"Updating user {id} failed on save");
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            await _unitOfWork.UserRepository.Delete(id);

            if (await _unitOfWork.Complete())
                return Ok("User was successfully deleted!");

            throw new Exception($"Deleting user {id} failed on delete");
        }

        [HttpGet]
        [Route("{searchTerm:alpha}/filter")]
        public async Task<IActionResult> GetProductNames(string searchTerm)
        {
            var itemNamesList = await _unitOfWork.ProductRepository.GetProductTitles(searchTerm);

            if (itemNamesList == null)
            {
                return NotFound();
            }

            return Ok(itemNamesList);
        }

        [Route("{gender:alpha}/Popular")]
        [HttpGet]
        public async Task<IActionResult> GetPopularProducts(Gender gender)
        {
            var items = await _unitOfWork.ProductRepository.GetPopularProducts(gender);

            if(items != null)
                return Ok(items);

            return NotFound();
        }
        [HttpGet("qty")]
        public async Task<IActionResult> GetItemsWithLowQty([FromQuery] ProductParams itemParams)
        {
            var items = await _unitOfWork.ProductRepository.GetProductsWithLowQty(itemParams);

            Response.AddPagination(items.CurrentPage, items.PageSize,
                items.TotalCount, items.TotalPages);

            return Ok(items);
        }

        [HttpGet("ProductsSoldInFirstQuarter")]
        public async Task<IActionResult> ProductsSoldInFirstQuarter()
        {
            var res = await _unitOfWork.ProductRepository.ProductsSoldInFirstQuarter();
            return Ok(res);
        }


        [HttpGet("orderLessThanThree")]
        public async Task<IActionResult> ProductsSoldLessThanThreeTimes()
        {
            var res = await _unitOfWork.ProductRepository.ProductsSoldLessThanThreeTimes();
            return Ok(res);
        }

        [HttpGet("ProdTotalSoldProdOpt")]
        public async Task<IActionResult> ProductsWithTotalSoldProdOpt()
        {
            var res = await _unitOfWork.ProductRepository.ProductsWithTotalSoldProdOpt();
            return Ok(res);
        }
    }
}