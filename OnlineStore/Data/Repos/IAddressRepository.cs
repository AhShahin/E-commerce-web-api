using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repos
{
    public interface IAddressRepository
    {
        Task<PagedList<Address>> GetAddresses(AddressParams addressParams);
        Task<Address?> GetAddress(int id);

    }
}
