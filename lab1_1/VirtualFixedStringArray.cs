using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace lab1_1
{
    /// <summary>
    /// Страница для фиксированного строкового массива.
    /// Здесь Data – массив строк фиксированной длины.
    /// </summary>
    internal class FixedStringPage : PageBase<string>
    {
        public FixedStringPage(int cellsPerPage, int bitMapSize) : base(cellsPerPage, bitMapSize) { }
    }
    /// <summary>
    /// Виртуальный массив строк фиксированной длины.
    /// На каждой странице содержится 128 элементов, размер данных = 128 * stringLength, плюс битовая карта.
    /// Размер страницы выравнивается до кратного 512.
    /// </summary>
    internal class VirtualFixedStringArray : VirtualArrayBase<string>
    {

        private const int ElementsPerPage = 128;
        private readonly int stringLength;
        private readonly int bitMapSize = 16;  // для 128 элементов
        private int rawDataSize;      // = 128 * stringLength
        private int rawPageSize;      // = bitMapSize + rawDataSize
        private new int PageBlockSize;    // выравненный до 512 байт

        public VirtualFixedStringArray(string fileName, long totalElements, int stringLength, int bufferPagesCount = 3)
            : base(fileName, totalElements, bufferPagesCount)
        {
            if (stringLength <= 0)
                throw new ArgumentException("Длина строки должна быть больше 0.");
            this.stringLength = stringLength;
            rawDataSize = ElementsPerPage * stringLength;
            rawPageSize = bitMapSize + rawDataSize;
            PageBlockSize = ((rawPageSize + 511) / 512) * 512;
            totalPages = (int)Math.Ceiling((totalElements * (double)stringLength) / rawDataSize);
            OpenOrCreateFiles();
            pageBuffer = new PageBase<string>[bufferPagesCount];
            for (int i = 0; i < bufferPagesCount; i++)
            {
                pageBuffer[i] = i < totalPages ? LoadPage(i) : null;
            }
        }



        protected override PageBase<string> LoadPage(int absolutePageNumber)
        {
            long offset = signature.Length + absolutePageNumber * PageBlockSize;
            byte[] pageBlock = new byte[PageBlockSize];
            fileStream.Seek(offset, SeekOrigin.Begin);
            int bytesRead = fileStream.Read(pageBlock, 0, PageBlockSize);
            if (bytesRead != PageBlockSize)
                throw new IOException("Ошибка чтения страницы.");
            var page = new FixedStringPage(ElementsPerPage, bitMapSize)
            {
                AbsolutePageNumber = (int)absolutePageNumber,
                LastAccessTime = DateTime.Now,
                ModifiedFlag = 0
            };
            // Копируем битовую карту.
            Array.Copy(pageBlock, 0, page.BitMap, 0, bitMapSize);
            int dataOffset = bitMapSize;
            for (int i = 0; i < ElementsPerPage; i++)
            {
                byte[] strBytes = new byte[stringLength];
                Array.Copy(pageBlock, dataOffset + i * stringLength, strBytes, 0, stringLength);
                page.Data[i] = Encoding.ASCII.GetString(strBytes).TrimEnd('\0');
            }
            return page;
        }

        protected override void WritePage(PageBase<string> page)
        {
            long offset = signature.Length + page.AbsolutePageNumber * PageBlockSize;
            byte[] pageBlock = new byte[PageBlockSize];
            Array.Copy(page.BitMap, 0, pageBlock, 0, bitMapSize);
            int dataOffset = bitMapSize;
            for (int i = 0; i < ElementsPerPage; i++)
            {
                string s = page.Data[i] ?? "";
                string fixedStr = s.Length > stringLength ? s.Substring(0, stringLength)
                                                          : s.PadRight(stringLength, '\0');
                byte[] strBytes = Encoding.ASCII.GetBytes(fixedStr);
                Array.Copy(strBytes, 0, pageBlock, dataOffset + i * stringLength, stringLength);
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

        public override bool ReadElement(long elementIndex, out string result)
        {
            result = string.Empty;
            int pageIndex = GetPageIndexForElement(elementIndex);
            if (pageIndex == -1)
                return false;
            int offset = (int)(elementIndex % ElementsPerPage);
            result = pageBuffer[pageIndex].Data[offset];
            return true;
        }

        public override bool WriteElement(long elementIndex, string value)
        {
            int pageIndex = GetPageIndexForElement(elementIndex);
            if (pageIndex == -1)
                return false;
            int offset = (int)(elementIndex % ElementsPerPage);
            // Приводим строку к фиксированной длине.
            string fixedStr = value.Length > stringLength ? value.Substring(0, stringLength)
                                                          : value.PadRight(stringLength, '\0');
            pageBuffer[pageIndex].Data[offset] = fixedStr;
            int byteIndex = offset / 8;
            int bitIndex = offset % 8;
            pageBuffer[pageIndex].BitMap[byteIndex] |= (byte)(1 << bitIndex);
            pageBuffer[pageIndex].ModifiedFlag = 1;
            pageBuffer[pageIndex].LastAccessTime = DateTime.Now;
            return true;
        }
    }
}
