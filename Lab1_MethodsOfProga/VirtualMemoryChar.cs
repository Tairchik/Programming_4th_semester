using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_MethodsOfProgram
{
    internal class VirtualMemoryChar : IVirtualMemory<string>
    {
        // Файловый указатель
        private FileStream file;
        // Буфер страниц
        private List<IPage<string>> bufferPages;
        // Длина строк
        private readonly int lengthString;
        // Размер буфера
        private const int BufferSize = 3;
        // Размер файла в байтах
        private readonly long FileByteSize;
        // Количество элементов массива
        private readonly long ArrayLength;

        // Константы
        private readonly int PageByteSize;
        private const int BitMapByteSize = 16;
        private readonly int BlockByteSize;
        public VirtualMemoryChar(string fileName, long totalSize, int lengthString) 
        {
            if (totalSize <= 10000)
            {
                throw new ArgumentException("Размерность массива должна превышать 10000.");
            }

            ArrayLength = totalSize;
            
            if (lengthString <= 0)
            {
                throw new ArgumentException("Длина строки не может быть неположительной.");
            }
            
            this.lengthString = lengthString;

            // Вычисляем константы
            // Выравниваем на границу кратной 512
            PageByteSize = 128 * lengthString + (512 - 128 * lengthString % 512);
            // Итоговый размер страницы
            BlockByteSize = PageByteSize + BitMapByteSize;

            // Вычисляем итоговый размер файла (без учета сигнатуры)
            FileByteSize = totalSize * lengthString + (BlockByteSize - totalSize * lengthString % BlockByteSize);

            string path = $"../../Data/{fileName}.bin";
            if (!File.Exists(path))
            {
                file = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite);
                byte[] signature = new byte[] { (byte)'V', (byte)'M' };
                file.Write(signature, 0, signature.Length);
                file.SetLength(signature.Length + this.FileByteSize);
            }
            else
            {
                file = new FileStream(path, FileMode.Open);
            }

            bufferPages = new List<IPage<string>>();
            for (int i = 0; i < BufferSize; i++)
            {
                bufferPages.Add(LoadFormFile(i));
            }

        }

        public IPage<string> LoadFormFile(long absolutePageNumber)
        {
            if (absolutePageNumber > FileByteSize / BlockByteSize || absolutePageNumber < 0)
            {
                throw new ArgumentOutOfRangeException("Такой страницы не существует.");
            }
            // Выделяем массив для считывания данных из файла
            string[] stringArray = new string[128];

            for (int j = 0; j < 128; j++)
            {
                // Выделяем 4 байта для считывания значений поэлементно из файла  
                byte[] bufferElement = new byte[lengthString];

                // Считываем элементы, где 2 - VM
                file.Seek(2 + j * lengthString + absolutePageNumber * BlockByteSize + BitMapByteSize, SeekOrigin.Begin);
                file.Read(bufferElement, 0, bufferElement.Length);

                // Копируем
                byte[] copyBufferElement = new byte[sizeof(int)];
                Array.Copy(bufferElement, copyBufferElement, 4);

                // Переводим в string и передаем в массив элементов
                stringArray[j] = BitConverter.ToString(copyBufferElement, 0);
            }

            // Считываем битовую карту
            byte[] bitMap = new byte[BitMapByteSize];
            file.Seek(2 + absolutePageNumber * BlockByteSize, SeekOrigin.Begin);
            file.Read(bitMap, 0, BitMapByteSize);
            byte[] copyBitMap = new byte[BitMapByteSize];
            Array.Copy(bitMap, copyBitMap, 0);

            return new PageChar(absolutePageNumber, 0, DateTime.Now, stringArray, copyBitMap);
        }

    }
}
