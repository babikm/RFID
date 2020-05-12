using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Reader> ReaderRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<Worker> WorkerRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Extruder> ExtruderRepository { get; }
        IRepository<Location> LocationRepository { get; }


    }
}
