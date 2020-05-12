using DAL.Models;
using DAL.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IMongoDatabase _database;
        public UnitOfWork()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var dbName = "RfidProjectDb";
            var connectionString
                = ConfigurationManager.GetSection("ConnectionString");
            _database = client.GetDatabase(dbName);
        }

        private IRepository<Reader> _readerRepository;
        public IRepository<Reader> ReaderRepository
            => _readerRepository ?? (_readerRepository = new Repository<Reader>(_database, "Readers"));

        private IRepository<Product> _productRepository;
        public IRepository<Product> ProductRepository
            => _productRepository ?? (_productRepository = new Repository<Product>(_database, "Products"));

        private IRepository<Worker> _workerRepository;
        public IRepository<Worker> WorkerRepository
            => _workerRepository ?? (_workerRepository = new Repository<Worker>(_database, "Workers"));

        private IRepository<User> _userRepository;
        public IRepository<User> UserRepository
            => _userRepository ?? (_userRepository = new Repository<User>(_database, "Users"));

        private IRepository<Extruder> _extruderRepository;
        public IRepository<Extruder> ExtruderRepository 
            => _extruderRepository ?? (_extruderRepository = new Repository<Extruder>(_database, "Extruder"));

        private IRepository<Location> _locationRepository;
        public IRepository<Location> LocationRepository
            => _locationRepository ?? (_locationRepository = new Repository<Location>(_database, "Location"));

    }
}
