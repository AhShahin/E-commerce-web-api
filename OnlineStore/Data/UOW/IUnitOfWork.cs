using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Repos;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.UOW
{
    public interface IUnitOfWork//<out TContext> where TContext : DbContext, new()
    {
        IUserRepository UserRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        IBrandRepository BrandRepository { get; }
        IAddressRepository AddressRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        ICartRepository CartRepository { get; }

        //TContext Context { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        Task<bool> Complete();
        int Save();
        bool HasChanges();
    }
}
