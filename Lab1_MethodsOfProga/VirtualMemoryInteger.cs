using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                
                

                int[] intArray = new int[512 / sizeof(int)];

                for (int j = 0; j < 512 / sizeof(int); j++) 
                {
                    byte[] bufferElement = new byte[sizeof(int)];
                    file.Read(bufferElement, 2 + 16 + j * 4 + i * 512, 4);

                    byte[] copyBufferElement = new byte[sizeof(int)];
                    Array.Copy(bufferElement, copyBufferElement, 0);
                    intArray[j] = BitConverter.ToInt32(copyBufferElement, 0);
                }

                byte[] bitMap = new byte[16];
                file.Read(bitMap, 2 + i * 512, 16);
                byte[] copyBitMap = new byte[16];
                Array.Copy(bitMap, copyBitMap, 0);

                bufferPages.Add(new Page(i, 0, DateTime.Now, intArray, copyBitMap));
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
