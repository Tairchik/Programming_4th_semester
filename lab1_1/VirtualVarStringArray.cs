using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_1
{
    /// <summary>
    /// Для произвольной длины строк используется два файла: swap-файл и файл записей.
    /// Swap-файл хранит адреса записей (int) в файле записей.
    /// </summary>
    internal class VirtualVarStringArray : VirtualArrayBase<int>
    {
        // Поля, характерные для варианта с произвольной длиной.
        private FileStream recordFileStream;
        private readonly string recordFileName;
        private readonly int maxStringSize;
        // В swap файле каждая страница содержит 128 адресов (int) и битовую карту (16 байт).
        private const int ElementsPerPage = 128;
        private readonly int bitMapSize = 16;
        private int PageBlockSize => bitMapSize + ElementsPerPage * sizeof(int);

        // Общее число страниц рассчитывается по количеству элементов (адресов).
        // Здесь totalPages уже инициализировано в базовом классе через totalElements.
        public VirtualVarStringArray(string swapFileName, string recordFileName, long totalElements, int maxStringSize, int bufferPagesCount = 3)
            : base(swapFileName, totalElements, bufferPagesCount)
        {
            if (maxStringSize <= 0)
                throw new ArgumentException("Максимальный размер строки должен быть больше 0.");
            this.recordFileName = recordFileName;
            this.maxStringSize = maxStringSize;
            // totalPages рассчитывается: каждая страница содержит 128 элементов (адресов).
            totalPages = (int)Math.Ceiling(totalElements / (double)ElementsPerPage);
            OpenOrCreateFiles();
            OpenOrCreateRecordFile();
            pageBuffer = new PageBase<int>[bufferPagesCount];
            for (int i = 0; i < bufferPagesCount; i++)
            {
                pageBuffer[i] = i < totalPages ? LoadPage(i) : null;
            }
        }

        protected override void OpenOrCreateFiles()
        {
            // Открываем или создаём swap-файл.
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
                    throw new InvalidDataException("Неверная сигнатура swap-файла.");
            }
        }

        private void OpenOrCreateRecordFile()
        {
            if (!File.Exists(recordFileName))
            {
                recordFileStream = new FileStream(recordFileName, FileMode.CreateNew, FileAccess.ReadWrite);
            }
            else
            {
                recordFileStream = new FileStream(recordFileName, FileMode.Open, FileAccess.ReadWrite);
            }
        }

        protected override PageBase<int> LoadPage(int absolutePageNumber)
        {
            long offset = signature.Length + absolutePageNumber * PageBlockSize;
            byte[] pageBlock = new byte[PageBlockSize];
            fileStream.Seek(offset, SeekOrigin.Begin);
            int bytesRead = fileStream.Read(pageBlock, 0, PageBlockSize);
            if (bytesRead != PageBlockSize)
                throw new IOException("Ошибка чтения swap-страницы.");
            var page = new IntPage(ElementsPerPage, bitMapSize)
            {
                AbsolutePageNumber = (int)absolutePageNumber,
                LastAccessTime = DateTime.Now,
                ModifiedFlag = 0
            };
            Array.Copy(pageBlock, 0, page.BitMap, 0, bitMapSize);
            int dataOffset = bitMapSize;
            for (int i = 0; i < ElementsPerPage; i++)
            {
                byte[] addrBytes = new byte[4];
                Array.Copy(pageBlock, dataOffset + i * 4, addrBytes, 0, 4);
                int address = BitConverter.ToInt32(addrBytes, 0);
                page.Data[i] = address;
            }
            return page;
        }

        protected override void WritePage(PageBase<int> page)
        {
            long offset = signature.Length + page.AbsolutePageNumber * PageBlockSize;
            byte[] pageBlock = new byte[PageBlockSize];
            Array.Copy(page.BitMap, 0, pageBlock, 0, bitMapSize);
            int dataOffset = bitMapSize;
            for (int i = 0; i < ElementsPerPage; i++)
            {
                byte[] addrBytes = BitConverter.GetBytes(page.Data[i]);
                Array.Copy(addrBytes, 0, pageBlock, dataOffset + i * 4, 4);
            }
            fileStream.Seek(offset, SeekOrigin.Begin);
            fileStream.Write(pageBlock, 0, PageBlockSize);
            fileStream.Flush();
            page.ModifiedFlag = 0;
        }

        protected override int GetAbsolutePageNumber(long elementIndex)
        {
            return (int)(elementIndex / ElementsPerPage);
        }

        // Чтение элемента: сначала читается адрес записи из swap-файла, затем по адресу извлекается строка из record-файла.
        public override bool ReadElement(long elementIndex, out int dummy)
        {
            // Здесь базовый метод для получения адреса – возвращаем его через out-параметр.
            dummy = 0;
            int pageIndex = GetPageIndexForElement(elementIndex);
            if (pageIndex == -1)
                return false;
            int offsetInPage = (int)(elementIndex % ElementsPerPage);
            int address = pageBuffer[pageIndex].Data[offsetInPage];
            if (address == 0)
                return false;
            recordFileStream.Seek(address, SeekOrigin.Begin);
            byte[] lengthBytes = new byte[4];
            int bytesRead = recordFileStream.Read(lengthBytes, 0, 4);
            if (bytesRead != 4)
                return false;
            int strLength = BitConverter.ToInt32(lengthBytes, 0);
            byte[] strBytes = new byte[strLength];
            bytesRead = recordFileStream.Read(strBytes, 0, strLength);
            if (bytesRead != strLength)
                return false;
            // Для удобства можно вывести строку в консоль или вернуть через отдельный метод.
            string result = Encoding.ASCII.GetString(strBytes);
            Console.WriteLine(result);
            return true;
        }

        // Запись элемента: строка записывается в record-файл (с префиксом длины), затем адрес записи сохраняется в swap-файле.
        public override bool WriteElement(long elementIndex, int dummy)
        {
            // В данной реализации тип T для swap-файла – int (адрес записи).
            // Для демонстрации метод оставлен пустым, а запись строк реализуется через отдельный метод.
            throw new NotImplementedException("Используйте метод WriteStringElement для записи строки.");
        }

        /// <summary>
        /// Метод для записи строки в виртуальный массив переменной длины.
        /// </summary>
        public bool WriteStringElement(long elementIndex, string value)
        {
            if (elementIndex < 0 || elementIndex >= totalElements)
                return false;
            // Если строка длиннее maxStringSize, усекаем её.
            if (value.Length > maxStringSize)
                value = value.Substring(0, maxStringSize);
            byte[] strBytes = Encoding.ASCII.GetBytes(value);
            int strLength = strBytes.Length;
            recordFileStream.Seek(0, SeekOrigin.End);
            int address = (int)recordFileStream.Position;
            recordFileStream.Write(BitConverter.GetBytes(strLength), 0, 4);
            recordFileStream.Write(strBytes, 0, strLength);
            recordFileStream.Flush();
            int pageIndex = GetPageIndexForElement(elementIndex);
            if (pageIndex == -1)
                return false;
            int offsetInPage = (int)(elementIndex % ElementsPerPage);
            pageBuffer[pageIndex].Data[offsetInPage] = address;
            int byteIndex = offsetInPage / 8;
            int bitIndex = offsetInPage % 8;
            pageBuffer[pageIndex].BitMap[byteIndex] |= (byte)(1 << bitIndex);
            pageBuffer[pageIndex].ModifiedFlag = 1;
            pageBuffer[pageIndex].LastAccessTime = DateTime.Now;
            return true;
        }
    }
}
