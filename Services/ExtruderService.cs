using Abstract;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ExtruderService : IExtruderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExtruderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Extruder extruder)
        {
            _unitOfWork.ExtruderRepository.Add(new Extruder
            {
                Id = extruder.Id,
                Name = extruder.Name
            });
        }

        public void Delete(string id)
        {
            _unitOfWork.ExtruderRepository.Delete(id);
        }

        public Extruder Get(string id)
        {
            return _unitOfWork.ExtruderRepository.Get(id);
        }

        public IEnumerable<Extruder> GetAll()
        {
            return _unitOfWork.ExtruderRepository.GetAll();
        }

        public void Update(Extruder extruder)
        {
            _unitOfWork.ExtruderRepository.Update(x => x.Id == extruder.Id, extruder);

        }
    }
}
