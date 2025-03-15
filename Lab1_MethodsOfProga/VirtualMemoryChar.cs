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
        private List<IPage<int>> bufferPages;
        // Длина строк
        private readonly int lengthString;
        // Размер буфера
        private const int BufferSize = 3;
        // Размер массива с выравниваем
        private readonly long totalSize;
        // Размер массива без выравнивая, т.е. количество элементов массива
        private readonly long PersonTotalSize;



        // Константы
        private readonly int PageByteSize;
        private const int BitMapByteSize = 16;
        private readonly int TotalByteSize;
        public VirtualMemoryChar(string fileName, long totalSize, int lengthString) 
        {
            if (totalSize <= 10000)
            {
                throw new ArgumentException("Размерность массива должна превышать 10000.");
            }

            PersonTotalSize = totalSize;
            
            if (lengthString <= 0)
            {
                throw new ArgumentException("Длина строки не может быть неположительной.");
            }
            
            this.lengthString = lengthString;

            // Вычисляем константы
            // Выравниваем на границу кратной 512
            PageByteSize = 128 * lengthString + (512 - 128 * lengthString % 512);
            // Итоговый размер страницы
            TotalByteSize = PageByteSize + BitMapByteSize;

            // Вычисляем итоговый размер файла (без учета сигнатуры)
            this.totalSize = totalSize * lengthString + (TotalByteSize - totalSize % TotalByteSize);

            string path = $"../../Data/{fileName}.bin";
            if (!File.Exists(path))
            {
                file = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite);
                byte[] signature = new byte[] { (byte)'V', (byte)'M' };
                file.Write(signature, 0, signature.Length);
                file.SetLength(signature.Length + this.totalSize);
            }
            else
            {
                file = new FileStream(path, FileMode.Open);
            }

            bufferPages = new List<IPage<int>>();
            for (int i = 0; i < BufferSize; i++)
            {
                //bufferPages.Add(LoadFormFile(i));
            }

        }
    }
}
