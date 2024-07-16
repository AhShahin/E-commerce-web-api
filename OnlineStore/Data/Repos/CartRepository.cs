using Microsoft.Extensions.Caching.Distributed;
using OnlineStore.Models;
using RedisDemo.Extensions;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache? _cache;

        public CartRepository(IDistributedCache? cache)
        {
            _cache = cache;
        }
        
        public async Task<IEnumerable<CartItem>?> GetData(string key)  {
            
            return await _cache!.GetRecordAsync(key);
        }

        public async Task<CartItem> SetData(string key, CartItem cartItem)
        {
            IList<CartItem>? userCart = await GetData(key) as IList<CartItem>;
            if (userCart != null)
            {
                var existingCartItem = userCart.FirstOrDefault(c => c.Id == cartItem.Id);
                if(existingCartItem is not null)
                {
                    existingCartItem.Quantity++;
                    cartItem.Quantity = existingCartItem.Quantity;
                }
                else
                {
                    userCart.Add(cartItem);
                }
                
                await _cache!.SetRecordAsync(key, userCart);
            }
            else
            {
                List<CartItem> newCart = new()
                {
                    cartItem
                };

                await _cache!.SetRecordAsync(key, newCart);
            }

            return cartItem;

        }

        public async Task<bool> UpdateItemQty(string key, string itemId , int qty)
        {
            IList<CartItem>? userCart = await GetData(key) as IList<CartItem>;
            if (userCart == null)
            {
                return false;
            }

            var existingCartItem = userCart.FirstOrDefault(c => c.Id == itemId);
            if (existingCartItem is not null)
            {
                existingCartItem.Quantity = qty;
            }
            else
            {
                return false;
            }

            await _cache!.SetRecordAsync(key, userCart);
            return true;
        }


        public async Task RemoveItem(string key, string itemId)
        {
            IList<CartItem>? userCart = await GetData(key) as IList<CartItem>;
            if (userCart is null)
            {
                throw new Exception("Cart not found");
            }

            var ItemToRemove = userCart.FirstOrDefault(c => c.Id == itemId);
            if (ItemToRemove is null)
            {
                throw new Exception("Cart item not found");
            }
            
            userCart.Remove(ItemToRemove);

            if(userCart.Count > 0)
            {
                await _cache!.SetRecordAsync(key, userCart);
            }else
            {
                await _cache!.DeleteRecordAsync(key);
            }
        }

        public async Task RemoveCart(string key)
        {
            if(!string.IsNullOrEmpty(key))
            {
                await _cache!.DeleteRecordAsync(key);
            }
        }

    }
}
