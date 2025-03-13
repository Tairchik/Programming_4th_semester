using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab1_MethodsOfProgram
{
    internal class VirtualMemoryInteger
    {
        // Файловый указатель
        private FileStream file;
        // Буфер страниц
        private List<Page> bufferPages;
        // Размер буфера
        private const int BufferSize = 3;
        // Размер массива
        private readonly long totalSize;

        public VirtualMemoryInteger(string fileName, long totalSize)
        {
            if (totalSize <= 10000) 
            {
                throw new ArgumentException("Размерность массива должна превышать 10000.");
            }
            else 
            {
                this.totalSize = totalSize;
            }

            string path = $"../../Data/{fileName}.bin";
            if (!File.Exists(path))
            {
                file = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite);
                byte[] signature = new byte[] { (byte)'V', (byte)'M' };
                file.Write(signature, 0, signature.Length);
                file.SetLength(signature.Length + totalSize);   
            }
            else
            {
                file = new FileStream(path, FileMode.Open);
            }

            bufferPages = new List<Page>();
            for (int i = 0; i < BufferSize; i++) 
            {
                // выделяем массив для считывания данных из файла
                byte[] buffer = new byte[512];
                file.Read(buffer, 2 + i * 512, 2 + (i + 1) * 512);


                bufferPages.Add(new Page(i, 0, DateTime.Now));
            }
            file.Close();
        }


        // Метод определения номера (индекса) страницы в буфере страниц,
        // где находится элемент массива с заданным индексом
        public void IdentifyIndex(long index) 
        {
            // Абсолютный номер страницы, определяется как индекс деленный нацело на длину одной страницы (128)
            long absolutePageNumber = index / (512 / (sizeof(int)));

            // Проверка на наличие страницы в памяти
            bool fl = false;
            foreach (var page in bufferPages) 
            {
                if (page.AbsoluteNumber == absolutePageNumber)
                {
                    fl = true; 
                }
            }

            if (fl == false)
            {
                
                foreach (var page in bufferPages)
                {

                }
            }
        }

    }
}
