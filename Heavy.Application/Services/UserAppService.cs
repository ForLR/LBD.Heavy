using AutoMapper;
using Heavy.Application.Interfaces;
using Heavy.Application.ViewModels.Users;
using Heavy.Domain.Core.Bus;
using Heavy.Identity.Commands;
using Heavy.Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heavy.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly UserManager<User> _user;
        private readonly IMediatorHandler _mediator;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _cache;

        public UserAppService(UserManager<User> user, IMediatorHandler mediator, IMapper mapper, IMemoryCache memoryCache, IDistributedCache cache)
        {
            this._user = user;
            this._mediator = mediator;
            this._mapper = mapper;
            this._memoryCache = memoryCache;
            this._cache = cache;
        }

        public async Task<List<UserViewModel>> AllViewModel()
        {
            var distributedCacheEntryOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(10));
            _cache.SetString("RedisTest", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), distributedCacheEntryOptions);
            var redisData=_cache.GetString("RedisTest");
            if (!_memoryCache.TryGetValue("GetAllUser", out List<UserViewModel> result))
            {
                var data = await _user.Users.ToListAsync();
                result = _mapper.Map<List<UserViewModel>>(data);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));//绝对过期时间
                _memoryCache.Set("GetAllUser", result, cacheEntryOptions);
            }
            return result;
        }

        public void DeleteAsync(string id)
        {
            var deleteCommand = _mapper.Map<DeleteUserCommand>(id);
            _mediator.SendCommand(deleteCommand);
         
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<User> GetById(string id)
        {
            var data = await _user.FindByIdAsync(id);
            return data;
        }

        public void Register(UserViewModel user)
        {
            var registerCommand = _mapper.Map<RegisterUserCommand>(user);
            _mediator.SendCommand(registerCommand);
        }

        public void Update(UserViewModel user)
        {
            var updateCommand = _mapper.Map<UpdateUserCommand>(user);
            _mediator.SendCommand(updateCommand);
        }
    }
}
