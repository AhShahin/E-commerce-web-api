using Microsoft.AspNetCore.Mvc;
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
    public interface IUserRepository : IRepository<User> 
    {
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<UserForDetailedDto?> GetUser(int id);
        Task<IEnumerable<object>> GetNumOfUsersByGender();
        Task<IEnumerable<object>> UsersNeverOrdered();
        Task<User?> UsersMostOrdered();

        //Task<UserForListDto> CreateUser(UserForCreationDto user);
    }
}
