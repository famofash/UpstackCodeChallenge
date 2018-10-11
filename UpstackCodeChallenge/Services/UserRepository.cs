using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpstackCodeChallenge.Contracts;
using UpstackCodeChallenge.Data;

namespace UpstackCodeChallenge.Services
{
    public class UserRepository :GenericRepository<User>, IUserRepository
    {
        public UserRepository(UpstackDbContext DbContext)
        {
            _dbContext = DbContext;
        }
        
    }
}
