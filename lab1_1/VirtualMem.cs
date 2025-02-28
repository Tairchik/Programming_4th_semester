using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_1
{
    namespace VirtualMemoryManager
    {
        /// <summary>
        /// Структура (класс) страницы, находящейся в памяти.
        /// </summary>
        public class MemoryPage
        {
            public long AbsolutePageNumber { get; set; }     // Порядковый номер страницы в файле.
            public byte ModifiedFlag { get; set; }           // 0 – не модифицирована, 1 – изменена.
            public DateTime LastAccessTime { get; set; }       // Время последнего обращения.
            public byte[] BitMap { get; set; }                // Битовая карта страницы.
            public int[] Data { get; set; }                   // Массив значений (целых чисел).

            public MemoryPage(int cellsPerPage, int bitMapSize)
            {
                Data = new int[cellsPerPage];
                BitMap = new byte[bitMapSize];
                ModifiedFlag = 0;
                LastAccessTime = DateTime.MinValue;
            }
        }

        /// <summary>
        /// Класс для управления виртуальным массивом целых чисел.
        /// </summary>
        public class VirtualIntArray
        {
            private FileStream fileStream;           // Файловый поток.
            private readonly string fileName;        // Имя файла подкачки.
            private MemoryPage[] pageBuffer;         // Буфер страниц (не менее 3).
            private readonly int bufferPagesCount;   // Количество страниц в буфере.
            private readonly byte[] signature = new byte[] { (byte)'V', (byte)'M' };  // Сигнатура файла.

            // Новые константы:
            private const int DataPageSize = 512;    // На каждой странице ровно 512 байт отведено под данные.
            private const int ElementSize = sizeof(Int32); // Размер одного элемента (4 байта).
            private const int cellsPerPage = DataPageSize / ElementSize; // 512/4 = 128 элементов.
            private readonly int bitMapSize = (int)Math.Ceiling(cellsPerPage / 8.0); // Для 128 ячеек: 16 байт.

            // Фактический размер блока страницы в файле: битовая карта + данные.
            private int PageBlockSize => bitMapSize + DataPageSize;

            private readonly long totalElements;     // Общее число элементов массива.
            private readonly long totalPages;        // Общее число страниц (вычисляется по данным).

            /// <summary>
            /// Конструктор инициализации.
            /// Параметры:
            ///   fileName – имя файла подкачки;
            ///   totalElements – размерность массива (должно быть >10000);
            ///   arrayType – для целого типа (в данном случае всегда "int");
            ///   stringLength – длина строки (по умолчанию 0, не используется для int).
            /// Буфер страниц формируется из не менее чем 3 страниц.
            /// </summary>
            public VirtualIntArray(string fileName, long totalElements, string arrayType, int stringLength = 0, int bufferPagesCount = 3)
            {
                if(arrayType.ToLower() != "int")
                throw new ArgumentException("Поддерживается только тип int.");

                if (totalElements <= 10000)
                    throw new ArgumentException("Размер массива должен быть больше 10000 элементов.");

                this.fileName = fileName;
                this.totalElements = totalElements;
                this.bufferPagesCount = Math.Max(bufferPagesCount, 3);

                // Вычисляем общее число страниц по данным: каждая страница содержит ровно DataPageSize байт.
                long totalDataBytes = totalElements * ElementSize;
                totalPages = (long)Math.Ceiling(totalDataBytes / (double)DataPageSize);

                // Инициализируем файл подкачки.
                OpenOrCreateSwapFile();

                // Инициализируем буфер страниц и загружаем первые bufferPagesCount страниц.
                pageBuffer = new MemoryPage[this.bufferPagesCount];
                for (int i = 0; i < this.bufferPagesCount; i++)
                {
                    if (i < totalPages)
                        pageBuffer[i] = LoadPage(i);
                    else
                        pageBuffer[i] = null;
                }
            }

            /// <summary>
            /// Метод открытия или создания файла подкачки.
            /// Если файла не существует, создаётся новый, записывается сигнатура и файл заполняется нулями.
            /// Если существует – файл открывается в режиме чтения/записи.
            /// </summary>
            private void OpenOrCreateSwapFile()
            {
                if (!File.Exists(fileName))
                {
                    // Создаём новый файл.
                    fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite);

                    // Записываем сигнатуру.
                    fileStream.Write(signature, 0, signature.Length);

                    // Вычисляем размер файла: сигнатура + (количество страниц * PageBlockSize).
                    long fileSize = signature.Length + totalPages * PageBlockSize;
                    fileStream.SetLength(fileSize);
                    // Можно также заполнить содержимое нулями (хотя SetLength гарантирует нужный размер).
                }
                else
                {
                    // Открываем существующий файл.
                    fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);

                    // Проверяем сигнатуру.
                    byte[] fileSig = new byte[2];
                    fileStream.Read(fileSig, 0, 2);
                    if (!fileSig.SequenceEqual(signature))
                    {
                        throw new InvalidDataException("Неверная сигнатура файла подкачки.");
                    }
                }
            }

            /// <summary>
            /// Метод загрузки страницы из файла подкачки.
            /// Считывает битовую карту и данные страницы.
            /// </summary>
            /// <param name="absolutePageNumber">Абсолютный номер страницы в файле (начиная с 0)</param>
            /// <returns>Объект MemoryPage, загруженный из файла</returns>
            private MemoryPage LoadPage(long absolutePageNumber)
            {
                // Вычисляем смещение страницы: после сигнатуры, каждая страница имеет размер PageSize.
                long offset = signature.Length + absolutePageNumber * PageBlockSize;
                byte[] pageBlock = new byte[PageBlockSize];

                fileStream.Seek(offset, SeekOrigin.Begin);
                int bytesRead = fileStream.Read(pageBlock, 0, PageBlockSize);
                if (bytesRead != PageBlockSize)
                    throw new IOException("Ошибка чтения страницы из файла подкачки.");

                // Создаём объект страницы.
                MemoryPage page = new MemoryPage(cellsPerPage, bitMapSize)
                {
                    AbsolutePageNumber = absolutePageNumber,
                    LastAccessTime = DateTime.Now,
                    ModifiedFlag = 0
                };

                // Копируем битовую карту.
                Array.Copy(pageBlock, 0, page.BitMap, 0, bitMapSize);
                    
                // Считываем данные: начиная с байта, следующего за битовой картой.
                int dataOffset = bitMapSize;
                for (int i = 0; i < cellsPerPage; i++)
                {
                    byte[] elementBytes = new byte[ElementSize];
                    Array.Copy(pageBlock, dataOffset + i * ElementSize, elementBytes, 0, ElementSize);
                    page.Data[i] = BitConverter.ToInt32(elementBytes, 0);
                }
                return page;
            }

            /// <summary>
            /// Метод определения индекса страницы в буфере, в которой находится элемент с заданным индексом.
            /// Если страница не загружена, выбирается самая старая страница для замещения.
            /// Если выбранная страница модифицирована, она выгружается в файл.
            /// После загрузки обновляются атрибуты страницы.
            /// Возвращается индекс страницы в буфере или -1 в случае ошибки.
            /// </summary>
            /// <param name="elementIndex">Индекс элемента в виртуальном массиве</param>
            /// <returns>Индекс страницы в буфере или -1, если произошла ошибка</returns>
            public int GetPageIndexForElement(long elementIndex)
            {
                if (elementIndex < 0 || elementIndex >= totalElements)
                    return -1;

                // Вычисляем абсолютный номер страницы, где находится элемент.
                long absolutePageNumber = elementIndex / cellsPerPage;

                // Ищем страницу в буфере.
                for (int i = 0; i < pageBuffer.Length; i++)
                {
                    if (pageBuffer[i] != null && pageBuffer[i].AbsolutePageNumber == absolutePageNumber)
                    {
                        // Обновляем время последнего обращения и сбрасываем флаг модификации.
                        pageBuffer[i].LastAccessTime = DateTime.Now;
                        pageBuffer[i].ModifiedFlag = 0;
                        return i;
                    }
                }

                // Если страница не найдена в буфере, выбираем самую старую страницу для замещения.
                int oldestIndex = 0;
                DateTime oldestTime;

                if (pageBuffer[0] != null) 
                {
                    oldestTime = pageBuffer[0].LastAccessTime;
                }
                else
                {
                    oldestTime = DateTime.MinValue;
                }

                for (int i = 1; i < pageBuffer.Length; i++)
                {
                    if (pageBuffer[i] == null)
                    {
                        oldestIndex = i;
                        break;
                    }
                    if (pageBuffer[i].LastAccessTime < oldestTime)
                    {
                        oldestTime = pageBuffer[i].LastAccessTime;
                        oldestIndex = i;
                    }
                }

                // Если выбранная страница модифицирована, выгружаем её в файл.
                if (pageBuffer[oldestIndex] != null && pageBuffer[oldestIndex].ModifiedFlag == 1)
                {
                    WritePage(pageBuffer[oldestIndex]);
                }

                // Загружаем новую страницу из файла.
                pageBuffer[oldestIndex] = LoadPage(absolutePageNumber);
                return oldestIndex;
            }

            /// <summary>
            /// Метод чтения значения элемента массива по заданному индексу.
            /// Вычисляет страницу, затем позицию элемента внутри страницы.
            /// </summary>
            /// <param name="elementIndex">Индекс элемента в виртуальном массиве</param>
            /// <param name="result">Переменная для записи прочитанного значения</param>
            /// <returns>True, если операция успешна, иначе false</returns>
            public bool ReadElement(long elementIndex, out int result)
            {
                result = 0;
                int pageIndex = GetPageIndexForElement(elementIndex);
                if (pageIndex == -1)
                    return false;

                // Вычисляем адрес элемента внутри страницы.
                int offsetInPage = (int)(elementIndex % cellsPerPage);
                result = pageBuffer[pageIndex].Data[offsetInPage];
                return true;
            }

            /// <summary>
            /// Метод записи заданного значения в элемент массива по указанному индексу.
            /// Вычисляет страницу, позицию элемента, записывает значение и обновляет атрибуты страницы.
            /// </summary>
            /// <param name="elementIndex">Индекс элемента в виртуальном массиве</param>
            /// <param name="value">Записываемое значение</param>
            /// <returns>True, если операция успешна, иначе false</returns>
            public bool WriteElement(long elementIndex, int value)
            {
                int pageIndex = GetPageIndexForElement(elementIndex);
                if (pageIndex == -1)
                    return false;

                int offsetInPage = (int)(elementIndex % cellsPerPage);
                pageBuffer[pageIndex].Data[offsetInPage] = value;

                // Обновляем битовую карту: ставим флаг, что элемент записан.
                int byteIndex = offsetInPage / 8;
                int bitIndex = offsetInPage % 8;
                pageBuffer[pageIndex].BitMap[byteIndex] |= (byte)(1 << bitIndex);

                // Обновляем атрибуты страницы.
                pageBuffer[pageIndex].ModifiedFlag = 1;
                pageBuffer[pageIndex].LastAccessTime = DateTime.Now;
                return true;
            }

            /// <summary>
            /// Метод записи (выгрузки) страницы в файл подкачки.
            /// Записывает в файл обновлённую страницу по её абсолютному номеру.
            /// </summary>
            /// <param name="page">Страница, которую необходимо выгрузить в файл</param>
            private void WritePage(MemoryPage page)
            {
                // Вычисляем смещение страницы в файле.
                long offset = signature.Length + page.AbsolutePageNumber * PageBlockSize;
                byte[] pageBlock = new byte[PageBlockSize];

                // Копируем битовую карту.
                Array.Copy(page.BitMap, 0, pageBlock, 0, bitMapSize);

                // Копируем данные.
                int dataOffset = bitMapSize;
                for (int i = 0; i < cellsPerPage; i++)
                {
                    byte[] elementBytes = BitConverter.GetBytes(page.Data[i]);
                    Array.Copy(elementBytes, 0, pageBlock, dataOffset + i * ElementSize, ElementSize);
                }

                // Записываем в файл.
                fileStream.Seek(offset, SeekOrigin.Begin);
                fileStream.Write(pageBlock, 0, PageBlockSize);
                fileStream.Flush();

                // Сбрасываем флаг модификации.
                page.ModifiedFlag = 0;
            }

            /// <summary>
            /// Закрывает файловый поток.
            /// </summary>
            public void Close()
            {
                // Перед закрытием можно выгрузить все модифицированные страницы.
                foreach (var page in pageBuffer)
                {
                    if (page != null && page.ModifiedFlag == 1)
                    {
                        WritePage(page);
                    }
                }
                fileStream?.Close();
            }
        }
    } 
}