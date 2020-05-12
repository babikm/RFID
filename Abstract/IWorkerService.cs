using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstract
{
    public interface IWorkerService
    {
        IEnumerable<Worker> GetAll();
        Worker Get(string id);
        void Add(Worker person);
        void Delete(string id);
        void Update(Worker person);
    }
}
