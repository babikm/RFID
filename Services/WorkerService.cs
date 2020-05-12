using Abstract;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorkerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Worker worker)
        {
            _unitOfWork.WorkerRepository.Add(worker);
        }

        public void Delete(string id)
        {
            _unitOfWork.WorkerRepository.Delete(id);
        }

        public Worker Get(string id)
        {
            return _unitOfWork.WorkerRepository.Get(id);
        }

        public IEnumerable<Worker> GetAll()
        {
            return _unitOfWork.WorkerRepository.GetAll();
        }

        public void Update(Worker worker)
        {
            _unitOfWork.WorkerRepository.Update(x => x.Id == worker.Id, worker);
        }
    }
}
