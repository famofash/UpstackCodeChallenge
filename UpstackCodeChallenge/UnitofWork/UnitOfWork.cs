using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpstackCodeChallenge.Contracts;
using UpstackCodeChallenge.Data;
using UpstackCodeChallenge.Services;

namespace UpstackCodeChallenge.UnitofWork
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly UpstackDbContext _context;
        public IUserRepository UserRepository { get; private set; }
        public UnitOfWork(UpstackDbContext context)
        {
            _context = context;

            UserRepository = new UserRepository(_context);

        }
        public UnitOfWork() : this(new UpstackDbContext())
        {
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
