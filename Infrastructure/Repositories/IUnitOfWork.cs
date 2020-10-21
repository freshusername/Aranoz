using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<ApplicationUser> Users { get; }

        IBaseRepository<Hotel> Hotels { get; }



        int Save();
    }
}
