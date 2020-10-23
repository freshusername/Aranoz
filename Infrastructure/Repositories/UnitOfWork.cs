using Infrastructure.EF;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

        private IBaseRepository<ApplicationUser> _usersRepo;
        private IBaseRepository<Hotel> _hotelsRepo;

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }

        #region repositories
        public IBaseRepository<ApplicationUser> Users
        {
            get
            {
                if (_usersRepo == null) { _usersRepo = new BaseRepository<ApplicationUser>(_context); }
                return _usersRepo;
            }
        }

        public IBaseRepository<Hotel> Hotels
        {
            get
            {
                if (_hotelsRepo == null) { _hotelsRepo = new BaseRepository<Hotel>(_context); }
                return _hotelsRepo;
            }
        }
        #endregion

        private bool isDisposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                _context.Dispose();
            }
            isDisposed = true;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
