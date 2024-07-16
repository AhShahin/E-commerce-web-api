using OnlineStore.Dtos;
using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public interface IOrderRepository: IRepository<Order>
    {
        /*Task<PagedList<Order>> GetOrders(OrderParams orderParams);
        Task<Order> GetOrder(int id);
        Task<IEnumerable<object>> GetNumOfOrdersByPaymentMethod();*/
        Task<Order> PlaceOrder(OrderForCreationDto orderForCreationDto, IEnumerable<CartItem> cartItems);

    }
}
