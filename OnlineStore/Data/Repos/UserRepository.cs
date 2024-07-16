using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Dtos;
using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Data.Repos
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper ) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = Context.Users
              .AsQueryable().AsNoTracking();

            if (!string.IsNullOrEmpty(userParams.SearchTerm))
            {
                users = users.Where(u => u.FirstName.Contains(userParams.SearchTerm) || u.LastName.Contains(userParams.SearchTerm));
            }

            if (!string.IsNullOrEmpty(userParams.Gender))
            {
                users = users.Where(u => u.Gender.ToString() == userParams.Gender);
            }

            if (!string.IsNullOrEmpty(userParams.Type))
            {
                users = users.Where(u => u.Type == userParams.Type);
            }

            if (userParams.MinAge != 0 || userParams.MaxAge != 0)
            {
                var minDob = DateTime.Today.AddYears(userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(userParams.MinAge);

                users = users.Where(u => u.DoB >= minDob && u.DoB <= maxDob);
            }

            var columnsMap = new Dictionary<string, Expression<Func<User, object>>>()
            {
                ["FirstName"] = u => u.FirstName,
                ["LastName"] = u => u.LastName,
                ["Type"] = u => u.Type,
                ["Gender"] = u => u.Gender,
            };
            users = users.ApplyOrdering(userParams, columnsMap);

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<UserForDetailedDto?> GetUser(int id)
        {
            return await Context.Users
                    .Where(u => u.Id == id)
                    .ProjectTo<UserForDetailedDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
        }
        
        public async Task<IEnumerable<object>> GetNumOfUsersByGender()
        {
            var usersBYGender = await (from u in Context.Users
                                group u by u.Gender into g
                                    select new
                                {
                                    key = g.Key,
                                    count = g.Count(),
                                }).ToListAsync();

            return  usersBYGender.Select(p => new { p.count, name = Enum.GetName(typeof(Gender), p.key) });
        }

        public async Task<IEnumerable<object>> UsersNeverOrdered()
        {
            var res = Context.Users.Where(u => !Context.Orders.Any(o => u.Id == o.UserId)).Select(u => new { firstname =u.FirstName });

            return await res.ToListAsync();
        }

        public async Task<User?> UsersMostOrdered()
        {
            var res = await Context.Users.Where(u => u.Id == Context.Orders.
                                                            GroupBy(o => o.UserId).
                                                            OrderByDescending(o => o.Count()).
                                                            Select(g => g.Key)
                                                            .First()!.Value)
                                                            .SingleOrDefaultAsync();

            return res;
        }   
    }
}
