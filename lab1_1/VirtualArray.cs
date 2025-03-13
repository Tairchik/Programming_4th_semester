using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        // Длина строки
        private int lenString;
        // Сигнатура файла VM, занимающая два байта
        private readonly byte[] signature = new byte[] { (byte)'V', (byte)'M' };
        // Общее число страниц массива
        private int totalPages;
        // Размер страницы
        private int pageSize = 512;
        // Список страниц
        private PageBase<int>[] Pages;
        // Тип данных
        

        public VirtualArray(string fileName, int totalElements, Types type, int lenString = 0)
        {
            FileName = fileName;
            TotalElements = totalElements;
            this.type = type;
            LenString = lenString;
            totalPages = (totalElements * ElementSize + 511) / pageSize;
            CreateOpenFile();

        }

        public Type ReturnType()
        {
            if (type == Types.Int)
            {
                return typeof(int);
            }
            else if (type == Types.Char)
            {
                return typeof(string);
            }
            else
            {
                return typeof(string);
            }
        }
        public VirtualArray(string fileName, int totalElements, string type, int lenString = 0)
        {
            FileName = fileName;
            TotalElements = totalElements;
            this.type = StringToTypes(type);
            LenString = lenString;
            totalPages = (totalElements * ElementSize + 511) / pageSize;
            CreateOpenFile();
        }
        private void CreateOpenFile()
        {
            if (!File.Exists(fileName))
            {
                fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite);

                fileStream.Write(signature, 0, signature.Length);
                long fileSize = signature.Length + totalPages * pageSize;
                fileStream.SetLength(fileSize);
            }
            else
            {
                fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                byte[] sig = new byte[2];
                fileStream.Read(sig, 0, 2);
                if (!sig.SequenceEqual(signature))
                    throw new InvalidDataException("Неверная сигнатура файла.");
            }
        }

        private void LoadPages()
        {
            for (int i = 0; i < bufferPages; i++) 
            {

            }
        }

        public int ElementSize
        {
            get 
            {
                if (type == Types.Int)
                {
                    return sizeof(int);
                }
                else if (type == Types.Char)
                {
                    return sizeof(char);
                }
                else if (type == Types.Varchar) 
                {
                    return sizeof(char);
                }
                else
                {
                    throw new Exception("Не определен тип");
                }
            }
        }


        public int LenString
        {
            get
            {
                return lenString;
            }
            private set
            {
                if (value >= 0)
                {
                    lenString = value;
                }
                else
                {
                    throw new Exception("Поле lenString не может быть меньше нуля");
                }
            }
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
