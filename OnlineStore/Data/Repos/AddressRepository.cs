using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AddressRepository(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<Address>> GetAddresses(AddressParams addressParams)
        {
            var addresses = _context.Addresses
              .AsQueryable().AsNoTracking();

            if (!string.IsNullOrEmpty(addressParams.SearchTerm))
            {
                addresses = addresses.Where(u => u.StreetAddress.Contains(addressParams.SearchTerm));
            }

            if (!string.IsNullOrEmpty(addressParams.City.ToString()))
            {
                addresses = addresses.Where(u => u.City.Name == addressParams.City);
            }

            if (!string.IsNullOrEmpty(addressParams.Postcode))
            {
                addresses = addresses.Where(u => u.Postcode == addressParams.Postcode);
            }

            if (!string.IsNullOrEmpty(addressParams.Country.ToString()))
            {
                addresses = addresses.Where(u => u.Country.Name == addressParams.Country);
            }


            var columnsMap = new Dictionary<string, Expression<Func<Address, object>>>()
            {
                ["StreetAddress"] = a => a.StreetAddress,
                ["Country"] = a => a.Country,
                ["City"] = a => a.City,
                ["IsBillingAddress"] = a => a.IsBillingAddress,
                ["IsShippingAddress"] = a => a.IsShippingAddress
            };
            addresses = addresses.ApplyOrdering(addressParams, columnsMap);

            return await PagedList<Address>.CreateAsync(addresses, addressParams.PageNumber, addressParams.PageSize);
        }

        public async Task<Address?> GetAddress(int id)
        {
            return await _context.Addresses
              .SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
