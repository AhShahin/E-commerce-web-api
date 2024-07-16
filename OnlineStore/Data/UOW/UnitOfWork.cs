using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using OnlineStore.Data.Repos;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.UOW
{
    public class UnitOfWork: IUnitOfWork
    {
        private IDbContextTransaction _objTran;
        private readonly DataContext _context;
        private readonly IDistributedCache? _cache;
        private readonly IMapper _mapper;
        private bool disposed = false;

        public UnitOfWork(DataContext context, IDistributedCache? cache, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public IUserRepository _UserRepository;
        public IOrderRepository _OrderRepository;
        public IProductCategoryRepository _ProductCategoryRepository;
        public IProductRepository _ProductRepository;
        public IBrandRepository _BrandRepository;
        public IAddressRepository _AddressRepository;
        public ICartRepository _CartRepository;

        public IUserRepository UserRepository { 
            get => _UserRepository ??= new UserRepository(_context, _mapper);
            
        }

        public IOrderRepository OrderRepository
        {
            get => _OrderRepository??= new OrderRepository(_context, _mapper); 
        }

        public IProductCategoryRepository ProductCategoryRepository
        {
            get => _ProductCategoryRepository ??=  new ProductCategoryRepository(_context);        
        }

        public IProductRepository ProductRepository
        {
            get => _ProductRepository ??= new ProductRepository(_context, _mapper);           
        }

        public IAddressRepository AddressRepository
        {
            get=> _AddressRepository??= new AddressRepository(_context, _mapper);
        }

        public IBrandRepository BrandRepository {
            get => _BrandRepository ??= new BrandsRepository(_context);
        }

        public ICartRepository CartRepository
        {
            get => _CartRepository ??= new CartRepository(_cache);
        }

        public async Task<bool> Complete()
        {
            var res = await _context.SaveChangesAsync() > 0;
            return res;
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
        public void CreateTransaction()
        {
            _objTran = _context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _objTran.Commit();
        }
        public void Rollback()
        {
            _objTran.Rollback();
            _objTran.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
    }
}
