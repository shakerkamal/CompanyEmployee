using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken) =>

            await FindByCondition(x => x.RefreshToken.ToLower().Equals(refreshToken.ToLower()), false)
                .SingleOrDefaultAsync();
    }
}
