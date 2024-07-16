using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Data.UOW;
using OnlineStore.Dtos;
using OnlineStore.Models;
using OnlineStore.Data.Repos;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Helpers;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderForCreationDto orderForCreationDto)
        {
            //replace itemId param by [] UserItemOptions
            var ip = Extensions.getUserIp();

            var userId = string.IsNullOrEmpty(orderForCreationDto.UserId.ToString()) ?
                                ip : orderForCreationDto.UserId.ToString();

            if (userId is null)
            {
                return NotFound();
            }

            var cartItems = await _unitOfWork.CartRepository.GetData(userId);

            if (cartItems is null)
            {
                return NotFound();
            }

            var orderToPlace = await _unitOfWork.OrderRepository.PlaceOrder(orderForCreationDto, cartItems);

            var order = await _unitOfWork.OrderRepository.Add(orderToPlace);

            if (order is null)
            {
                return NotFound();
            }

            if (await _unitOfWork.Complete())
            {
                //await _unitOfWork.CartRepository.RemoveCart(userId);
                return Ok(order);
            }

            throw new Exception("Adding order failed on save");
        }

        // GET: api/Orders
        /*[HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery]OrderParams orderParams)
        {
            var orders = await _orderRepository.GetOrders(orderParams);

            var ordersToReturn = _mapper.Map<IEnumerable<OrderForListDto>>(orders);

            Response.AddPagination(orders.CurrentPage, orders.PageSize,
                orders.TotalCount, orders.TotalPages);

            return Ok(ordersToReturn);
        }*/

        // GET: api/Orders/5
        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderRepository.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderToReturn = _mapper.Map<OrderForDetailsDto>(order);

            return Ok(orderToReturn);
        }*/

        // PUT: api/Orders/5
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] OrderForUpdateDto orderForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != order.Id)
            //{
            //    return BadRequest();
            //}

            var order = await _repository.GetAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            _mapper.Map(orderForUpdateDto, order);

            if (await _unitOfWork.Complete())
                return NoContent();

            throw new Exception($"Updating order {id} failed on save");
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _repository.GetAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            //_repository.Remove(order);
            await _unitOfWork.Complete();

            return Ok(order);
        }*/

        /*[HttpGet("paymentMethod")]
        public async Task<IActionResult> GetNumOfOrdersByPaymentMethod()
        {
            var orders = await _orderRepository.GetNumOfOrdersByPaymentMethod();

            return Ok(orders);
        }*/



    }
}