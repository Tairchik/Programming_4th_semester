using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_1
{
    /// <summary>
    /// Базовый класс страницы, используемый в виртуальном массиве.
    /// Содержит общие поля: абсолютный номер, битовая карта, данные, флаг модификации, время доступа.
    /// </summary>
    internal abstract class PageBase<T>
    {
        // Номер страницы
        public int AbsolutePageNumber { get; set; } 
        // Флаг изменения страницы
        public byte ModifiedFlag { get; set; }
        // Время изменения
        public DateTime LastAccessTime { get; set; }
        // Битовая карта
        public byte[] BitMap { get; set; }
        // Элементы страницы
        public T[] Data { get; set; }

        protected PageBase(int cellsPerPage, int bitMapSize)
        {
            Data = new T[cellsPerPage];
            BitMap = new byte[bitMapSize];
            ModifiedFlag = 0;
            LastAccessTime = DateTime.MinValue;
        }
    }
    /// <summary>
    /// Абстрактный базовый класс для виртуального массива.
    /// Содержит общие поля и реализацию работы с файлом подкачки и буфером страниц.
    /// Определяет виртуальные методы, отличающиеся в конкретных реализациях.
    /// </summary>
    internal abstract class VirtualArrayBase<T>
        {
            // Общие поля для работы с swap файлом
            protected FileStream fileStream;
            protected readonly string fileName;
            protected readonly byte[] signature = new byte[] { (byte)'V', (byte)'M' };
            protected int bufferPagesCount;
            protected int totalPages;
            protected long totalElements;
            protected int PageBlockSize;
            protected PageBase<T>[] pageBuffer;

            // Конструктор базового класса принимает имя файла, общее число элементов и число страниц в буфере.
            protected VirtualArrayBase(string fileName, long totalElements, int bufferPagesCount = 3)
            {
                if (totalElements <= 10000)
                    throw new ArgumentException("Размер массива должен быть больше 10000 элементов.");
                this.fileName = fileName;
                this.totalElements = totalElements;
                this.bufferPagesCount = Math.Max(bufferPagesCount, 3);
            }

            /// <summary>
            /// Виртуальный метод инициализации файловой структуры.
            /// </summary>
            protected virtual void OpenOrCreateFiles()
            {
                if (!File.Exists(fileName))
                {
                    fileStream = new FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite);
                    fileStream.Write(signature, 0, signature.Length);
                    long fileSize = signature.Length + totalPages * PageBlockSize;
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

            /// <summary>
            /// Абстрактный метод загрузки страницы из файла.
            /// </summary>
            /// <param name="absolutePageNumber">Номер страницы (начиная с 0)</param>
            /// <returns>Загруженная страница</returns>
            protected abstract PageBase<T> LoadPage(int absolutePageNumber);

            /// <summary>
            /// Абстрактный метод записи (выгрузки) страницы в файл.
            /// </summary>
            protected abstract void WritePage(PageBase<T> page);

            /// <summary>
            /// Общая реализация получения индекса страницы из буфера для элемента с заданным индексом.
            /// Если нужная страница отсутствует, выбирается самая старая для замещения.
            /// </summary>
            public virtual int GetPageIndexForElement(long elementIndex)
            {
                if (elementIndex < 0 || elementIndex >= totalElements)
                    return -1;
                int absolutePageNumber = GetAbsolutePageNumber(elementIndex);

                // Поиск страницы в буфере.
                for (int i = 0; i < pageBuffer.Length; i++)
                {
                    if (pageBuffer[i] != null && pageBuffer[i].AbsolutePageNumber == absolutePageNumber)
                    {
                        pageBuffer[i].LastAccessTime = DateTime.Now;
                        return i;
                    }
                }

                // Если не найдена, выбираем самую старую страницу.
                int oldestIndex = 0;
                DateTime oldestTime = pageBuffer[0]?.LastAccessTime ?? DateTime.MaxValue;
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
                // Если выбранная страница модифицирована, выгружаем её.
                if (pageBuffer[oldestIndex] != null && pageBuffer[oldestIndex].ModifiedFlag == 1)
                    WritePage(pageBuffer[oldestIndex]);
                // Загружаем новую страницу.
                pageBuffer[oldestIndex] = LoadPage(absolutePageNumber);
                return oldestIndex;
            }

            /// <summary>
            /// Определяет абсолютный номер страницы, где находится элемент.
            /// Конкретная реализация зависит от типа данных и размера страницы.
            /// </summary>
            protected abstract int GetAbsolutePageNumber(long elementIndex);

            /// <summary>
            /// Общий метод закрытия файлового потока.
            /// Перед закрытием все модифицированные страницы выгружаются.
            /// </summary>
            public virtual void Close()
            {
                if (pageBuffer != null)
                {
                    foreach (var page in pageBuffer)
                    {
                        if (page != null && page.ModifiedFlag == 1)
                            WritePage(page);
                    }
                }
                fileStream?.Close();
            }

            // Можно добавить виртуальные методы для чтения/записи элемента, если их реализация схожа.
            public abstract bool ReadElement(long elementIndex, out T result);
            public abstract bool WriteElement(long elementIndex, T value);
        }
    }

