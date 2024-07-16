using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.UOW;
using OnlineStore.Dtos;
using OnlineStore.Helpers;
using OnlineStore.Helpers.ErrorHandeling;
using OnlineStore.Helpers.OrderInvoice;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public class OrderRepository: Repository<Order>, IOrderRepository
    {
        private readonly IMapper _mapper;

        public OrderRepository(DataContext context, IMapper mapper): base(context) {
            _mapper = mapper;
        }

        /*public async Task<IEnumerable<object>> GetNumOfOrdersByPaymentMethod()
        {
            return await Context.Orders
                                .GroupBy(t => t.PaymentMethod)
                                .Select(g => new { Name = g.Key, Count = g.Count() })
                                .ToListAsync();
        }*/

        /*public async Task<Order> GetOrder(int id)
        {
            return await Context.Orders
              .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
              .SingleOrDefaultAsync(o => o.Id == id);
        }*/


        public async Task<Order> PlaceOrder(OrderForCreationDto orderForCreationDto, IEnumerable<CartItem> cartItems)
        {
            IList<OrderProduct> orderProducts = new List<OrderProduct>();
            var totalAmount = 0m;
            var orderToPlace = new Order();
            
            orderToPlace = _mapper.Map(orderForCreationDto, orderToPlace);
            orderToPlace.Addresses = await Context.Addresses.Where(a => a.Id == orderForCreationDto.BillingAddressId ||
                                            a.Id == orderForCreationDto.ShippingAddressId).ToListAsync();
            orderToPlace.OrderStatusId = 2;
            orderToPlace.Number = Guid.NewGuid().ToString().Substring(0, 8);
            orderToPlace.CreatedOn = DateTime.Now;

            if (orderToPlace.UserId is null)
            {
                orderToPlace.GuestId = Extensions.getUserIp();
            }
            else
            {
                orderToPlace.UserId = orderForCreationDto.UserId;
            }

            foreach (var cartItem in cartItems)
            {
                var productOptionsSize = await Context.ProductOptions_Sizes.SingleOrDefaultAsync(ps => ps.ProductOptionsId == cartItem.SelectedOptionId
                                                    && ps.SizeId == cartItem.SizeId);
                var isQtyAvailable = cartItem.Quantity <= productOptionsSize!.Quantity; 
                
                if (isQtyAvailable)
                {
                    productOptionsSize!.Quantity -= cartItem.Quantity;
                    Context.Entry(productOptionsSize).State = EntityState.Modified;
                }
                else
                {
                    throw new CustomException("Quantity not available",
                    new { cartItemId = cartItem.Id });
                    //throw new Exception($"Quantity of product option: {cartItem.SelectedOptionId} and size: {cartItem.SizeId} not available");
                }

                var orderProduct = new OrderProduct
                {
                    Name = cartItem.Name,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Price,
                    SizeId = cartItem.SizeId,
                    ProductOptionsId = cartItem.SelectedOptionId
                };

                totalAmount += cartItem.Price * cartItem.Quantity;
                orderProducts.Add(orderProduct);
            }

            orderToPlace.FinalePrice = totalAmount;
            orderToPlace.OrderProducts = orderProducts;

            orderToPlace.Invoice = new OrderInvoice(orderToPlace).GeneratePdf();

            return orderToPlace;
        }
    }
}
