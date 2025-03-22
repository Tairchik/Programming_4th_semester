using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_MethodsOfProgram
{
    internal class VirtualMemoryString : IVirtualMemory<string>
    {
        // Файловый указатель
        private FileStream file;
        // Буфер страниц
        private List<IPage<int>> bufferPages;
        // Длина строк
        private readonly int lengthString;
        // Размер буфера
        private const int BufferSize = 3;
        // Размер файла в байтах
        private readonly long FileByteSize;
        // Количество элементов массива
        private readonly long ArrayLength;

        // Константы
        private const int PageByteSize = 512;
        private const int BitMapByteSize = 16;
        private readonly int BlockByteSize;
        private readonly long PageCount;

        // Строка из файла 2:
        private string file2Str = "";
        private FileStream file2;
        private readonly string path2;
        public VirtualMemoryString(string fileName, long totalSize, int lengthString)
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

            // Итоговый размер страницы (блок)
            BlockByteSize = PageByteSize + BitMapByteSize;

            // Общее количество страниц в файле
            PageCount = (long)Math.Ceiling((decimal)ArrayLength / 128);

            // Вычисляем итоговый размер файла (без учета сигнатуры)
            FileByteSize = PageCount * BlockByteSize;

            string path = $"../../Data/{fileName}.bin";
            path2 = $"../../Data/{fileName}.txt";
            if (!File.Exists(path))
            {
                try
                {
                    file = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite);
                    byte[] signature = new byte[] { (byte)'V', (byte)'M' };
                    file.Write(signature, 0, signature.Length);
                    file.SetLength(signature.Length + FileByteSize);
                    file2 = new FileStream(path2, FileMode.CreateNew, FileAccess.ReadWrite);
                    file2.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }   
            }
            else
            {
                file = new FileStream(path, FileMode.Open);
                byte[] signature = new byte[] { (byte)'V', (byte)'M' };
                file.SetLength(FileByteSize + signature.Length);
                file2 = new FileStream(path2, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                file2.Close();
            }

            file2Str = File.ReadAllText(path2);

            bufferPages = new List<IPage<int>>();
            for (int i = 0; i < BufferSize; i++)
            {
                bufferPages.Add(LoadFormFile(i));
            }

        }

        private IPage<int> LoadFormFile(long absolutePageNumber)
        {
            if (absolutePageNumber > PageCount || absolutePageNumber < 0)
            {
                throw new ArgumentOutOfRangeException("Страницы не существует.");
            }
            // Выделяем массив для считывания данных из файла
            int[] intArray = new int[128];

            for (int j = 0; j < 128; j++)
            {
                // Выделяем 4 байта для считывания значений поэлементно из файла  
                byte[] bufferElement = new byte[sizeof(int)];

                // Считываем элементы, где 2 - VM
                file.Seek(2 + j * 4 + absolutePageNumber * BlockByteSize + BitMapByteSize, SeekOrigin.Begin);
                file.Read(bufferElement, 0, bufferElement.Length);

                // Копируем
                byte[] copyBufferElement = new byte[sizeof(int)];
                Array.Copy(bufferElement, copyBufferElement, sizeof(int));

                // Переводим в int и передаем в массив элементов
                intArray[j] = BitConverter.ToInt32(copyBufferElement, 0);
            }

            // Считываем битовую карту
            byte[] bitMap = new byte[BitMapByteSize];
            file.Seek(2 + absolutePageNumber * BlockByteSize, SeekOrigin.Begin);
            file.Read(bitMap, 0, BitMapByteSize);
            byte[] copyBitMap = new byte[BitMapByteSize];
            Array.Copy(bitMap, copyBitMap, BitMapByteSize);

            return new PageInt(absolutePageNumber, 0, DateTime.Now, intArray, copyBitMap);
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
                        byte[] elementInBytes = BitConverter.GetBytes(bufferPages[page].Values[i]);
                        Array.Copy(elementInBytes, 0, valuesInBytes, i * sizeof(int), elementInBytes.Length);
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
            int bit = (int)indexElementInPage % 8;

            for (int page = 0; page < bufferPages.Count; page++)
            {
                if (bufferPages[page].AbsoluteNumber == absolutePageNumber)
                {   
                    if (file2Str.Length == 0)
                    {
                        throw new ArgumentException("Элемент не задан.");
                    }
                    int strId = GetStringIndex(index);
                    absolutePageNumber = GetPageNumber(index);

                    for (int j = 0; j < bufferPages.Count; j++)
                    {
                        if (bufferPages[j].AbsoluteNumber == absolutePageNumber)
                        {
                            if (file2Str.Length == 0)
                            {
                                throw new ArgumentException("Элемент не задан.");
                            }
                            return file2Str.Substring(strId, bufferPages[j].Values[indexElementInPage]);
                        }
                    }
                   
                }
            }
            throw new Exception("Элемент не найден в буфере.");
        }


        private int GetStringIndex(long index)
        {
            long absolutePageNumber = GetPageNumber(index);

            long countIndex = 0;
            int countPage = 0;
            int result = 0;
            long temp = 0;
            while (true) 
            {
                temp = GetPageNumber(countPage);
                foreach (var page in bufferPages)
                {
                    if (temp == page.AbsoluteNumber)
                    {
                        for (int i = 0; i < 128; i++) 
                        {
                            if (countIndex == index)
                            {
                                return result;
                            }
                            result += page.Values[i];
                            countIndex++;
                        }
                    }
                }
                countPage += 128;
            }
        }


        // Метод записи заданного значения в элемент массива с указанным индексом
        public bool SetElementByIndex(int index, string value)
        {
            if (index < 0 || index > ArrayLength)
            {
                throw new ArgumentOutOfRangeException("Адресуемый элемент выходит за пределы массива.");
            }

            if (value.Length > lengthString)
            {
                value = value.Substring(0, lengthString);
            }
            // Вычисляет номер (индекс) страницы в буфере страниц, на которой находится требуемый элемент
            long absolutePageNumber = GetPageNumber(index);

            // Вычисляет страничный адрес элемента массива с заданным индексом
            long indexElementInPage = index % 128;

            int byteIndex = (int)indexElementInPage / 8;
            int bitIndex = (int)indexElementInPage % 8;
            for (int page = 0; page < bufferPages.Count; page++)
            {
                if (bufferPages[page].AbsoluteNumber == absolutePageNumber)
                {
                    // Вставляем элемент в файл со строками

                    int id = GetStringIndex(index);
                    absolutePageNumber = GetPageNumber(index);

                    for (int j = 0; j < bufferPages.Count; j++)
                    {
                        
                        if (bufferPages[j].AbsoluteNumber == absolutePageNumber)
                        {
                            if (file2Str.Length == 0)
                            {
                                file2Str = value;
                            }
                            else
                            {
                                file2Str = file2Str.Substring(0, id) + value + file2Str.Substring(bufferPages[j].Values[indexElementInPage] + id);
                            }
                            bufferPages[j].Values[indexElementInPage] = value.Length;
                            bufferPages[j].BitMap[byteIndex] |= (byte)(1 << bitIndex);
                            bufferPages[j].Status = 1;
                            bufferPages[j].ModTime = DateTime.Now;
                            return true;
                        }
                    }     
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
                    for (int i = 0; i < PageByteSize / sizeof(int); i++)
                    {
                        byte[] elementInBytes = BitConverter.GetBytes(page.Values[i]);
                        Array.Copy(elementInBytes, 0, valuesInBytes, i * sizeof(int), elementInBytes.Length);
                    }
                    file.Seek(2 + page.AbsoluteNumber * BlockByteSize + BitMapByteSize, SeekOrigin.Begin);
                    file.Write(valuesInBytes, 0, valuesInBytes.Length);
                }
            }

            File.WriteAllText(path2, file2Str);

        }
        public void Close()
        {
            file.Close();
        }
    }
}
