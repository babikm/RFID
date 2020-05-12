using Abstract;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LocationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Location location)
        {
            _unitOfWork.LocationRepository.Add(new Location
            {
                Id = location.Id,
                Name = location.Name
            });
        }

        public void Delete(string id)
        {
            _unitOfWork.LocationRepository.Delete(id);
        }

        public Location Get(string id)
        {
            var location = _unitOfWork.LocationRepository.Get(id);
            return location;
        }

        public IEnumerable<Location> GetAll()
        {
            var locations = _unitOfWork.LocationRepository.GetAll();
            return locations;
        }

        public void Update(Location location)
        {
            _unitOfWork.LocationRepository.Update(x => x.Id == location.Id, location);
        }
    }
}
