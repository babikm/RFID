using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abstract
{
    public interface IExtruderService
    {
        IEnumerable<Extruder> GetAll();
        Extruder Get(string id);
        void Add(Extruder extruder);
        void Delete(string id);
        void Update(Extruder extruder);
        
    }
}
