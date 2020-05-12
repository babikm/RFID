using Abstract;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public class ReaderService : IReaderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReaderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Reader Get(string id)
        {
            return _unitOfWork.ReaderRepository.Get(id);
        }

        public IEnumerable<Reader> GetAll()
        {
            return _unitOfWork.ReaderRepository.GetAll();
        }

        public void Add(Reader reader)
        {
            var versionNumber = GetReaderVersion();
            if (!IsReaderExist(versionNumber))
            {
                reader.ReaderNumber = versionNumber;
                _unitOfWork.ReaderRepository.Add(new Reader
                {
                    Id = reader.Id,
                    Location = reader.Location,
                    Description = reader.Description,
                    ReaderNumber = versionNumber
                });
            }

        }
        public void Delete(string id)
        {
            _unitOfWork.ReaderRepository.Delete(id);
        }

        public void Update(Reader reader)
        {
            _unitOfWork.ReaderRepository.Update(x => x.Id == reader.Id, reader);
        }

        public string GetReaderVersion()
        {
            var versionNumber = GetVersionNum();
            if (versionNumber != null)
                return versionNumber;
            return null;
        }

        public void showStatus(int Code)
        {
            string msg = "";
            switch (Code)
            {
                case 0x00:
                    msg = "Powiodło się";
                    break;
                case 0x01:
                    msg = "Coś poszło nie tak!\nPrzyłóż kartę do czytnika!";
                    break;
                case 0x02:
                    msg = "Błąd sumy kontrolnej";
                    break;
                case 0x03:
                    msg = "Nie wybrano portu COM";
                    break;
                case 0x04:
                    msg = "Minął limit czasu odpowiedzi";
                    break;
                case 0x05:
                    msg = "Sprawdź błąd sekwencji";
                    break;
                case 0x07:
                    msg = "Sprawdź sumę kontrolną";
                    break;
                case 0x0A:
                    msg = "Parametr znajduje się poza zakresem";
                    break;
                case 0x80:
                    msg = "OK :) ";
                    break;
                case 0x81:
                    msg = "Błąd!";
                    break;
                case 0x82:
                    msg = "Błąd przekroczenia limitu czasu odpowiedzi czytnika";
                    break;
                case 0x83:
                    msg = "Przyłóż kartę do czytnika!";
                    break;
                case 0x84:
                    msg = "Błąd danych";
                    break;
                case 0x85:
                    msg = "Czytnik otrzymał nieznane polecenie";
                    break;
                case 0x87:
                    msg = "Błąd";
                    break;
                case 0x89:
                    msg = "Parametr polecenia lub format błędu polecenia";
                    break;
                case 0x8A:
                    msg = "Niektóre błędy pojawiają się w procesie inicjalizacji karty ";
                    break;
                case 0x8B:
                    msg = "Get the wrong snr during anticollison loop.....";
                    break;
                case 0x8C:
                    msg = "Błąd autoryzacji";
                    break;
                case 0x8F:
                    msg = "Czytnik otrzymał nieznane polecenie";
                    break;
                case 0x90:
                    msg = "Karta nie obsługuje tego polecenia";
                    break;
                case 0x91:
                    msg = "Błędne polecenie";
                    break;
                case 0x92:
                    msg = "Nie współpracuje z tą opcją";
                    break;
                case 0x93:
                    msg = "Blok nie istnieje";
                    break;
                case 0x94:
                    msg = "Obiekt został zablokowany";
                    break;
                case 0x95:
                    msg = "Blokada zakończona niepowodzeniem";
                    break;
                case 0x96:
                    msg = "Operacja zakończona niepowodzeniem";
                    break;
            }
            Console.WriteLine(msg + "\r\n");

        }

        public string GetVersionNum()
        {
            string result = "";
            byte[] byteArry = new byte[12];
            int nRet = Reader.GetVersionNum(byteArry);
            if (nRet != 0)
            {
                showStatus(byteArry[0]);
            }
            else
            {
                result = BitConverter.ToString(byteArry);
            }

            return result;
        }

        public byte[] convertSNR(string str, int keyN)
        {
            string regex = "[^a-fA-F0-9]";
            string tmpJudge = Regex.Replace(str, regex, "");

            if (tmpJudge.Length != 12) return null;

            string[] tmpResult = Regex.Split(str, regex);
            byte[] result = new byte[keyN];
            int i = 0;
            foreach (string tmp in tmpResult)
            {
                result[i] = Convert.ToByte(tmp, 16);
                i++;
            }
            return result;
        }

        //Odczyt
        public string GetTagId()
        {
            byte mode = (byte)0x01;
            byte block = Convert.ToByte("00", 16);
            byte numOfBlock = Convert.ToByte("01", 16);

            byte[] key = new byte[6];

            key = convertSNR("FF FF FF FF FF FF", 6);
            if (key == null)
            {
                return null;
            }

            byte[] buffer = new byte[16 * numOfBlock];
            int nRet = Reader.MF_Read(mode, block, numOfBlock, key, buffer);
            int x = 0;
            int y = 4;
            string result = "";

            for (int i = 0; i < y; i++)
            {
                if (buffer[x + i] < 0)
                    buffer[x + i] = Convert.ToByte(Convert.ToInt32(buffer[x + i]) + 256);
            }

            for (int i = 0; i < y; i++)
            {
                result += buffer[x + i].ToString("X2");
            }

            if (result == "00000000")
            {
                return null;
            }
            return result;
        }

        public Reader GetByTagId(string tagId)
        {
            return _unitOfWork.ReaderRepository.GetByTagId(tagId);
        }

        public bool IsReaderExist(string versionNum)
        {
            var all = GetAll()
                .FirstOrDefault(x => x.ReaderNumber == versionNum);
            if (all != null)
            {
                return true;
            }
            return false;
        }

    }
}

