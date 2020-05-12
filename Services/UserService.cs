using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abstract;
using DAL.Models;
using DAL.UnitOfWork;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(string id)
        {
            _unitOfWork.UserRepository.Delete(id);
        }

        public User Get(string id)
        {
            return _unitOfWork.UserRepository.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public bool IsUserExist(string name)
        {
            var user = _unitOfWork.UserRepository.GetAll();

            if(user.Any(x => x.EmailId == name || x.UserName == name))
            {
                return true;
            }

            return false;
        }

        public void Update(User product)
        {
            _unitOfWork.UserRepository.Update(x => x.Id == product.Id, product);
        }

        public string Hash(string value)
        {
            return Convert.ToBase64String(System.Security.Cryptography
                .SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        public void Add(User user)
        {
            _unitOfWork.UserRepository.Add(user);
        }

        public void VerificationEmail(string email, string activationCode)
        {
                        
        }
    }
}
