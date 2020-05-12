using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abstract
{
    public interface ILocationService
    {
        IEnumerable<Location> GetAll();
        Location Get(string id);
        void Add(Location location);
        void Delete(string id);
        void Update(Location location);
    }
}
