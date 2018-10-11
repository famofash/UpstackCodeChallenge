using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpstackCodeChallenge.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
       int Save();
    }
}
