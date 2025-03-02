using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_1
{
    enum Types
    {
        Int,
        Char,
        Varchar
    }
    internal class VirtualArray
    {
        // Файловый указатель
        private FileStream fileStream;
        // Буфер страниц
        private int bufferPages;
        // Имя файла
        private string fileName;
        // Размерность массива
        private int totalElements;
        // Тип массива int|char|varchar 
        private Types type;
        // длина строки
        private int lenString = 0;

        public VirtualArray(string fileName, int totalElements, Types type, int lenString = 0)
        {
            FileName = fileName;
        }
        public VirtualArray(string fileName, int totalElements, string type, int lenString = 0)
        {
            FileName = fileName;
        }

        

        public int TotalElements
        {
            get { return totalElements; }
            private set
            {
                if (value < 10000)
                {
                    throw new Exception("Размерность массива должна быть не менее 10000");
                }
                else
                {
                    totalElements = value;
                }
            }
        }

        public int BufferPages
        {
            get { return bufferPages; }
            private set 
            {
                if (value < 3)
                {
                    throw new Exception("Размерность страниц буфера должна быть не менее 3");
                }
                else
                {
                    bufferPages = value;
                }
            }
        }

        public string FileName
        {
            get 
            {
                return fileName; 
            }
            private set 
            {
                if (!string.IsNullOrEmpty(value)) 
                {
                    fileName = value;
                }
                else
                {
                    throw new Exception("Поле fileName не может быть пустым");
                }
            }
        }

        public Types StringToTypes(string nameType)
        {
            string lowNameType = nameType.ToLower();
            if (!string.IsNullOrEmpty(nameType))
            {
                throw new Exception("Поле nameType не может быть пустым");
            }
            else if (lowNameType.Equals("int"))
            {
                return Types.Int;
            }
            else if (lowNameType.Equals("char")) 
            {
                return Types.Char;
            }
            else if (lowNameType.Equals("varchar"))
            {
                return Types.Varchar;
            }
            else
            {
                throw new Exception($"Данный тип: {nameType} не поддерживается");
            }
        }
    }
}
