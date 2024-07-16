using Microsoft.AspNetCore.Mvc;
using OnlineStore.Dtos;
using OnlineStore.Helpers.Validator;
using OnlineStore.Models;
using System.Threading.Tasks;
using System.Net;
using OnlineStore.Data.UOW;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using OnlineStore.Helpers;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNetCore.Routing;
namespace OnlineStore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        [HttpGet(Name = "cart")]
        public async Task<IActionResult> GetCartItems()
        {
            var userIp = Extensions.getUserIp();
            var cartItems = await _unitOfWork.CartRepository.GetData(userIp);


            if (cartItems != null && cartItems.Count() > 0)
            {
               _ = await _unitOfWork.ProductRepository.AddProductOptionStockBySize(cartItems);
            }

            return Ok(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItem(CartItem cartItem)
        {
            var userIp = Extensions.getUserIp();
            await _unitOfWork.CartRepository.SetData(userIp, cartItem);

            return Ok(cartItem);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCartItemQty(CartItemForUpdateDto cartItem)
        {
            var userIp = Extensions.getUserIp();

            await _unitOfWork.CartRepository.UpdateItemQty(userIp, cartItem.Id,
                cartItem.changes.Quantity);

            return Ok(cartItem);

        }

        [Route("{itemId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem(string itemId)
        {
            var userIp = Extensions.getUserIp();

            await _unitOfWork.CartRepository.RemoveItem(userIp, itemId);

            return Ok(itemId);

        }
    }
}
