using System;
using System.Collections;
using System.Collections.Generic;
using DAL.Models;

namespace Abstract
{
    public interface IUserService
    {
        bool IsUserExist(string name);
        void VerificationEmail(string email, string activationCode);
        IEnumerable<User> GetAll();
        User Get(string id);
        void Add(User user);
        void Delete(string id);
        void Update(User product);
        string Hash(string value);

    }

}
