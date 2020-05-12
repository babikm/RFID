using Abstract;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ServiceBase
    {
        protected IUnitOfWork _unitOfWork;

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
