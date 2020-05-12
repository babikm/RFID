using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstract
{
    public interface IReaderService
    {
        void Add(Reader reader);
        string GetReaderVersion();
        Reader Get(string id);
        Reader GetByTagId(string tagId);
        IEnumerable<Reader> GetAll();
        void Delete(string id);
        void Update(Reader reader);
        string GetVersionNum();
        void showStatus(int Code);
        byte[] convertSNR(string str, int keyN);
        string GetTagId();
        bool IsReaderExist(string versionNum);
    }
}
