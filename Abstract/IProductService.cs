using DAL.Models;
using System.Collections.Generic;

namespace Abstract
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product Get(string id);
        void Add(Product product);
        void Delete(string id);
        void Update(Product product);
        bool IsTagIdExist(string tagId);
        bool Check(Product product, string tagId);
    }
}
