using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
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


        // Константы
        private const int PageByteSize = 512;
        private const int BitMapByteSize = PageByteSize / sizeof(int) / 8;
        private const int TotalByteSize = PageByteSize + BitMapByteSize;


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
                bufferPages.Add(LoadFormFile(i));
            }
        }

       


        public Page LoadFormFile(long absolutePageNumber)
        {
            // Выделяем массив для считывания данных из файла
            int[] intArray = new int[PageByteSize / sizeof(int)];

            for (int j = 0; j < PageByteSize / sizeof(int); j++)
            {
                if (2 + BitMapByteSize + j * 4 + absolutePageNumber * PageByteSize + 4 > totalSize)
                {
                    return null;
                }
                // Выделяем 4 байта для считывания значений поэлементно из файла  
                byte[] bufferElement = new byte[sizeof(int)];

                // Считываем элементы, где 2 - VM, 16 - битовая карта
                file.Seek(2 + BitMapByteSize + j * 4 + absolutePageNumber * PageByteSize, SeekOrigin.Begin);
                file.Read(bufferElement, 0, bufferElement.Length);

                // Копируем в другой массив
                byte[] copyBufferElement = new byte[sizeof(int)];
                Array.Copy(bufferElement, copyBufferElement, 4);

                // Переводим в int и передаем в массив 
                intArray[j] = BitConverter.ToInt32(copyBufferElement, 0);
            }

            if (2 + absolutePageNumber * PageByteSize + 16 <= totalSize)
            {
                byte[] bitMap = new byte[BitMapByteSize];
                file.Seek(2 + absolutePageNumber * PageByteSize, SeekOrigin.Begin);
                file.Read(bitMap, 0, BitMapByteSize);
                byte[] copyBitMap = new byte[BitMapByteSize];
                Array.Copy(bitMap, copyBitMap, 0);

                return new Page(absolutePageNumber, 0, DateTime.Now, intArray, copyBitMap);
            }
            return null;
        }


        // Метод определения номера (индекса) страницы в буфере страниц,
        // где находится элемент массива с заданным индексом
        public long GetPageNumber(long index)
        {
            // Абсолютный номер страницы, определяется как индекс деленный нацело на длину одной страницы (128)
            long absolutePageNumber = index / (PageByteSize / (sizeof(int)));

            // Проверка на наличие страницы в памяти
            DateTime time = DateTime.Now;
            foreach (var page in bufferPages)
            {
                if (page.AbsoluteNumber == absolutePageNumber)
                {
                    return page.AbsoluteNumber;
                }
                else
                {
                    if (DateTime.Compare(time, page.modTime) > 0)
                    {
                        time = page.modTime;
                    }
                }
            }

            for (int page = 0; page < bufferPages.Count; page++)
            {
                if (Equals(bufferPages[page].modTime, time) && bufferPages[page].status == 1)
                {
                    // Выгружаем битовую карту
                    file.Seek(2 + bufferPages[page].AbsoluteNumber * TotalByteSize, SeekOrigin.Begin);
                    file.Write(bufferPages[page].bitMap, 0, BitMapByteSize);

                    // Выгружаем элементы страницы
                    byte[] valuesInBytes = new byte[PageByteSize];
                    for (int i = 0; i < PageByteSize / sizeof(int); i++)
                    {
                        byte[] elementInBytes = BitConverter.GetBytes(bufferPages[page].values[i]);
                        Array.Copy(elementInBytes, 0, valuesInBytes, i * sizeof(int), elementInBytes.Length);
                    }
                    file.Seek(2 + bufferPages[page].AbsoluteNumber * TotalByteSize + BitMapByteSize, SeekOrigin.Begin);
                    file.Write(valuesInBytes, 0, valuesInBytes.Length);

                    // Загружаем в буфер
                    bufferPages[page] = LoadFormFile(index);
                    return bufferPages[page].AbsoluteNumber;
                }
                else if (Equals(bufferPages[page].modTime, time) && bufferPages[page].status == 0)
                {
                    // Загружаем в буфер
                    bufferPages[page] = LoadFormFile(absolutePageNumber);
                    return bufferPages[page].AbsoluteNumber;
                }
            }

            throw new Exception("Страница не найдена!");
        }

        // Метод чтения значения элемента массива с заданным индексом в указанную переменную
        public int GetElementByIndex(long index) 
        {
            if (index >= totalSize) 
            {
                throw new ArgumentOutOfRangeException("index");
            }
            // Вычисляет номер (индекс) страницы в буфере страниц, на которой находится требуемый элемент
            long absolutePageNumber = GetPageNumber(index);

            // Вычисляет страничный адрес элемента массива с заданным индексом
            long indexElementInPage = index % 128;
            
            foreach (var page in bufferPages)
            {
                if (page.AbsoluteNumber == absolutePageNumber)
                {
                    return page.values[indexElementInPage];
                }
            }
            throw new Exception("Элемент не найден в буфере.");
        }


        // Метод записи заданного значения в элемент массива с указанным индексом
        public bool SetElementByIndex(int index, int value)
        {
            if (index >= totalSize)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            // Вычисляет номер (индекс) страницы в буфере страниц, на которой находится требуемый элемент
            long absolutePageNumber = GetPageNumber(index);

            // Вычисляет страничный адрес элемента массива с заданным индексом
            long indexElementInPage = index % 128;

            int byteIndex = (int) indexElementInPage / 8;
            int bitIndex = (int) indexElementInPage % 8;
            foreach (var page in bufferPages)
            {
                if (page.AbsoluteNumber == absolutePageNumber)
                {
                    page.values[indexElementInPage] = value;
                    page.bitMap[byteIndex] |= (byte)(1 << bitIndex);
                    page.status = 1;
                    page.modTime = DateTime.Now;
                    return true;
                }
            }
            return false;
        }

        // Выгрузка в файл 
        public void DumpBuffer()
        {
            foreach (var page in bufferPages)
            {
                if (page.status == 1)
                {
                    // Выгружаем битовую карту
                    file.Seek(2 + page.AbsoluteNumber * TotalByteSize, SeekOrigin.Begin);
                    file.Write(page.bitMap, 0, BitMapByteSize);
                    // Выгружаем элементы страницы
                    byte[] valuesInBytes = new byte[PageByteSize];
                    for (int i = 0; i < PageByteSize / sizeof(int); i++)
                    {
                        byte[] elementInBytes = BitConverter.GetBytes(page.values[i]);
                        Array.Copy(elementInBytes, 0, valuesInBytes, i * sizeof(int), elementInBytes.Length);
                    }
                    file.Seek(2 + page.AbsoluteNumber * TotalByteSize + BitMapByteSize, SeekOrigin.Begin);
                    file.Write(valuesInBytes, 0, valuesInBytes.Length);
                }
            }
        }
    }
}
