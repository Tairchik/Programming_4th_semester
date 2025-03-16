using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_MethodsOfProgram
{
    internal class VirtualMemoryChar : IVirtualMemory<string, string>
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
        private readonly long PageCount;
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

            // Итоговый размер страницы (блок)
            BlockByteSize = PageByteSize + BitMapByteSize;

            // Общее количество страниц в файле
            PageCount = (long)Math.Ceiling((decimal)ArrayLength / 128);

            // Вычисляем итоговый размер файла (без учета сигнатуры)
            FileByteSize = PageCount * BlockByteSize;

            string path = $"../../Data/{fileName}.bin";
            if (!File.Exists(path))
            {
                file = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite);
                byte[] signature = new byte[] { (byte)'V', (byte)'M' };
                file.Write(signature, 0, signature.Length);
                file.SetLength(signature.Length + FileByteSize);
            }
            else
            {
                file = new FileStream(path, FileMode.Open);
                byte[] signature = new byte[] { (byte)'V', (byte)'M' };
                file.SetLength(FileByteSize + signature.Length);
            }

            bufferPages = new List<IPage<string>>();
            for (int i = 0; i < BufferSize; i++)
            {
                bufferPages.Add(LoadFormFile(i));
            }

        }

        public IPage<string> LoadFormFile(long absolutePageNumber)
        {
            if (absolutePageNumber > PageCount || absolutePageNumber < 0)
            {
                throw new ArgumentOutOfRangeException("Страницы не существует.");
            }
            // Выделяем массив для считывания данных из файла
            string[] stringArray = new string[128];

            for (int j = 0; j < 128; j++)
            {
                // Выделяем lengthString байта для считывания значений поэлементно из файла  
                byte[] bufferElement = new byte[lengthString];

                // Считываем элементы, где 2 - VM
                file.Seek(2 + j * lengthString + absolutePageNumber * BlockByteSize + BitMapByteSize, SeekOrigin.Begin);
                file.Read(bufferElement, 0, bufferElement.Length);

                // Копируем
                byte[] copyBufferElement = new byte[lengthString];
                Array.Copy(bufferElement, copyBufferElement, lengthString);

                // Переводим в string и передаем в массив элементов
                stringArray[j] = Encoding.UTF8.GetString(copyBufferElement).TrimEnd('\0');
            }

            // Считываем битовую карту
            byte[] bitMap = new byte[BitMapByteSize];
            file.Seek(2 + absolutePageNumber * BlockByteSize, SeekOrigin.Begin);
            file.Read(bitMap, 0, BitMapByteSize);
            byte[] copyBitMap = new byte[BitMapByteSize];
            Array.Copy(bitMap, copyBitMap, 0);

            return new PageChar(absolutePageNumber, 0, DateTime.Now, stringArray, copyBitMap);
        }

        // Метод определения номера (индекса) страницы в буфере страниц,
        // где находится элемент массива с заданным индексом
        public long GetPageNumber(long index)
        {
            if (index < 0 || index > ArrayLength)
            {
                throw new ArgumentOutOfRangeException("Адресуемый элемент выходит за пределы массива.");
            }
            // Абсолютный номер страницы, определяется как номер элемента деленный нацело на длину одной страницы (128)
            long absolutePageNumber = index / 128;

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
                    if (DateTime.Compare(time, page.ModTime) > 0)
                    {
                        time = page.ModTime;
                    }
                }
            }

            for (int page = 0; page < bufferPages.Count; page++)
            {
                if (Equals(bufferPages[page].ModTime, time) && bufferPages[page].Status == 1)
                {
                    // Выгружаем битовую карту
                    file.Seek(2 + bufferPages[page].AbsoluteNumber * BlockByteSize, SeekOrigin.Begin);
                    file.Write(bufferPages[page].BitMap, 0, BitMapByteSize);

                    // Выгружаем элементы страницы
                    byte[] valuesInBytes = new byte[PageByteSize];
                    for (int i = 0; i < 128; i++)
                    {
                        byte[] elementInBytes = Encoding.UTF8.GetBytes(bufferPages[page].Values[i]);                     
                        Array.Copy(elementInBytes, 0, valuesInBytes, i * lengthString, elementInBytes.Length);
                    }
                    file.Seek(2 + bufferPages[page].AbsoluteNumber * BlockByteSize + BitMapByteSize, SeekOrigin.Begin);
                    file.Write(valuesInBytes, 0, valuesInBytes.Length);

                    // Загружаем в буфер
                    bufferPages[page] = LoadFormFile(index);
                    return bufferPages[page].AbsoluteNumber;
                }
                else if (Equals(bufferPages[page].ModTime, time) && bufferPages[page].Status == 0)
                {
                    // Загружаем в буфер
                    bufferPages[page] = LoadFormFile(absolutePageNumber);
                    return bufferPages[page].AbsoluteNumber;
                }
            }

            throw new Exception("Страница не найдена!");
        }

        // Метод чтения значения элемента массива с заданным индексом в указанную переменную
        public string GetElementByIndex(long index)
        {
            if (index < 0 || index > ArrayLength)
            {
                throw new ArgumentOutOfRangeException("Адресуемый элемент выходит за пределы массива.");
            }

            // Вычисляет номер (индекс) страницы в буфере страниц, на которой находится требуемый элемент
            long absolutePageNumber = GetPageNumber(index);

            // Вычисляет номер элемента в странице
            long indexElementInPage = index % 128;

            foreach (var page in bufferPages)
            {
                if (page.AbsoluteNumber == absolutePageNumber)
                {
                    return page.Values[indexElementInPage];
                }
            }
            throw new Exception("Элемент не найден в буфере.");
        }


        // Метод записи заданного значения в элемент массива с указанным индексом
        public bool SetElementByIndex(int index, string value)
        {
            if (index < 0 || index > ArrayLength)
            {
                throw new ArgumentOutOfRangeException("Адресуемый элемент выходит за пределы массива.");
            }
            // Вычисляет номер (индекс) страницы в буфере страниц, на которой находится требуемый элемент
            long absolutePageNumber = GetPageNumber(index);

            // Вычисляет номер элемента в странице
            long indexElementInPage = index % 128;

            int byteIndex = (int)indexElementInPage / 8;
            int bitIndex = (int)indexElementInPage % 8;
            foreach (var page in bufferPages)
            {
                if (page.AbsoluteNumber == absolutePageNumber)
                {
                    if (lengthString > value.Length)
                    {
                        page.Values[indexElementInPage] = value.PadRight(lengthString);
                    }
                    else
                    {
                        page.Values[indexElementInPage] = value.Substring(0, lengthString);
                    }
                    
                    page.BitMap[byteIndex] |= (byte)(1 << bitIndex);
                    page.Status = 1;
                    page.ModTime = DateTime.Now;
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
                if (page.Status == 1)
                {
                    // Выгружаем битовую карту
                    file.Seek(2 + page.AbsoluteNumber * BlockByteSize, SeekOrigin.Begin);
                    file.Write(page.BitMap, 0, BitMapByteSize);
                    // Выгружаем элементы страницы
                    byte[] valuesInBytes = new byte[PageByteSize];
                    for (int i = 0; i < 128; i++)
                    {
                        byte[] elementInBytes = Encoding.UTF8.GetBytes(page.Values[i]);
                        Array.Copy(elementInBytes, 0, valuesInBytes, i * lengthString, elementInBytes.Length);
                    }
                    file.Seek(2 + page.AbsoluteNumber * BlockByteSize + BitMapByteSize, SeekOrigin.Begin);
                    file.Write(valuesInBytes, 0, valuesInBytes.Length);
                }
            }
        }
    }
}
