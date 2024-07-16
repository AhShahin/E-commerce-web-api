using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>?> GetData(string key);
        Task<CartItem> SetData(string key, CartItem value);
        Task<bool> UpdateItemQty(string key, string itemId, int qty);
        Task RemoveItem(string key, string itemId);
        Task RemoveCart(string key);
    }
}
