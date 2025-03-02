using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_1
{
    
    /// <summary>
    /// Страница для виртуального массива int.
    /// Здесь данные – массив int.
    /// </summary>
    internal class IntPage : PageBase<int>
    {
        public IntPage(int cellsPerPage, int bitMapSize) : base(cellsPerPage, bitMapSize) { }
    }
    /// <summary>
    /// Виртуальный массив целых чисел. 
    /// Страница содержит битовую карту (16 байт) и 512 байт данных, что соответствует 128 элементам.
    /// Общий размер страницы = 16 + 512 = 528 байт.
    /// </summary>
    internal class VirtualIntArray : VirtualArrayBase<int>
    {
        private const int ElementSize = sizeof(int);  // размер int (4 байта)
        private const int DataPageSize = 512; // байт данных на странице
        private const int CellsPerPage = DataPageSize / ElementSize; // 128 элементов
        private readonly int bitMapSize = (int)Math.Ceiling(CellsPerPage / 8.0); // 16 байт
        private int PageBlockSize => bitMapSize + DataPageSize;  // 528 байт

        public VirtualIntArray(string fileName, long totalElements, int bufferPagesCount = 3)
            : base(fileName, totalElements, bufferPagesCount)
        {
            // Вычисляем общее число страниц по данным: каждая страница содержит DataPageSize байт.
            totalPages = (int)Math.Ceiling((totalElements * (double)ElementSize) / DataPageSize);
            // Открываем или создаем файл подкачки.
            OpenOrCreateFiles();
            pageBuffer = new PageBase<int>[bufferPagesCount];
            for (int i = 0; i < bufferPagesCount; i++)
            {
                pageBuffer[i] = i < totalPages ? LoadPage(i) : null;
            }
        }


        protected override PageBase<int> LoadPage(int absolutePageNumber)
        {
            long offset = signature.Length + absolutePageNumber * PageBlockSize;
            byte[] pageBlock = new byte[PageBlockSize];
            fileStream.Seek(offset, SeekOrigin.Begin);
            int bytesRead = fileStream.Read(pageBlock, 0, PageBlockSize);
            if (bytesRead != PageBlockSize)
                throw new IOException("Ошибка чтения страницы.");
            var page = new IntPage(CellsPerPage, bitMapSize)
            {
                AbsolutePageNumber = (int)absolutePageNumber,
                LastAccessTime = DateTime.Now,
                ModifiedFlag = 0
            };
            // Копируем битовую карту.
            Array.Copy(pageBlock, 0, page.BitMap, 0, bitMapSize);
            // Считываем данные.
            int dataOffset = bitMapSize;
            for (int i = 0; i < CellsPerPage; i++)
            {
                byte[] b = new byte[ElementSize];
                Array.Copy(pageBlock, dataOffset + i * ElementSize, b, 0, ElementSize);
                page.Data[i] = BitConverter.ToInt32(b, 0);
            }
            return page;
        }

        protected override void WritePage(PageBase<int> page)
        {
            long offset = signature.Length + page.AbsolutePageNumber * PageBlockSize;
            byte[] pageBlock = new byte[PageBlockSize];
            Array.Copy(page.BitMap, 0, pageBlock, 0, bitMapSize);
            int dataOffset = bitMapSize;
            for (int i = 0; i < CellsPerPage; i++)
            {
                byte[] b = BitConverter.GetBytes(page.Data[i]);
                Array.Copy(b, 0, pageBlock, dataOffset + i * ElementSize, ElementSize);
            }
            fileStream.Seek(offset, SeekOrigin.Begin);
            fileStream.Write(pageBlock, 0, PageBlockSize);
            fileStream.Flush();
            page.ModifiedFlag = 0;
        }

        protected override int GetAbsolutePageNumber(long elementIndex)
        {
            return (int)(elementIndex / CellsPerPage);
        }

        public override bool ReadElement(long elementIndex, out int result)
        {
            result = 0;
            int pageIndex = GetPageIndexForElement(elementIndex);
            if (pageIndex == -1)
                return false;
            int offset = (int)(elementIndex % CellsPerPage);
            result = pageBuffer[pageIndex].Data[offset];
            return true;
        }

        public override bool WriteElement(long elementIndex, int value)
        {
            int pageIndex = GetPageIndexForElement(elementIndex);
            if (pageIndex == -1)
                return false;
            int offset = (int)(elementIndex % CellsPerPage);
            pageBuffer[pageIndex].Data[offset] = value;
            // Обновляем битовую карту.
            int byteIndex = offset / 8;
            int bitIndex = offset % 8;
            pageBuffer[pageIndex].BitMap[byteIndex] |= (byte)(1 << bitIndex);
            pageBuffer[pageIndex].ModifiedFlag = 1;
            pageBuffer[pageIndex].LastAccessTime = DateTime.Now;
            return true;
        }
    }
}
