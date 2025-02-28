using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_1
{
    internal class VirtualMemoryArray
    {
        private const int PageSize = 512;
        private const string Signature = "VM";
        private string filePath;
        private long arraySize;
        private FileStream fileStream;
        private List<Page> buffer;
        private int bufferSize = 3;

        struct Page
        {
            public int PageNumber;
            public int Status;
            public DateTime LastAccess;
            public byte[] Bitmap;
            public int[] Data;
        }

        public VirtualMemoryArray(string filename, long size)
        {
            filePath = filename;
            arraySize = size;
            buffer = new List<Page>(bufferSize);

            if (!File.Exists(filePath))
            {
                InitializeFile();
            }
            fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            LoadPagesToBuffer();
        }

        private void InitializeFile()
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            using (var writer = new BinaryWriter(fs))
            {
                writer.Write(Signature.ToCharArray());
                long numPages = (arraySize * sizeof(int) + PageSize - 1) / PageSize;
                byte[] emptyPage = new byte[PageSize];
                for (long i = 0; i < numPages; i++)
                {
                    writer.Write(emptyPage);
                }
            }
        }

        private void LoadPagesToBuffer()
        {
            for (int i = 0; i < bufferSize; i++)
            {
                buffer.Add(LoadPage(i));
            }
        }

        private Page LoadPage(int pageNumber)
        {
            fileStream.Seek(2 + pageNumber * PageSize, SeekOrigin.Begin);
            byte[] pageData = new byte[PageSize];
            fileStream.Read(pageData, 0, PageSize);

            Page page = new Page
            {
                PageNumber = pageNumber,
                Status = 0,
                LastAccess = DateTime.Now,
                Bitmap = new byte[4],
                Data = new int[(PageSize - 4) / sizeof(int)]
            };

            Buffer.BlockCopy(pageData, 0, page.Bitmap, 0, 4);
            Buffer.BlockCopy(pageData, 4, page.Data, 0, PageSize - 4);

            return page;
        }

        private int GetPageIndex(long index)
        {
            int pageNumber = (int)(index * sizeof(int) / PageSize);
            foreach (var page in buffer)
            {
                if (page.PageNumber == pageNumber)
                    return buffer.IndexOf(page);
            }

            return LoadPageIntoBuffer(pageNumber);
        }

        private int LoadPageIntoBuffer(int pageNumber)
        {
            if (buffer.Count >= bufferSize)
            {
                int oldestPageIndex = 0;
                for (int i = 1; i < buffer.Count; i++)
                {
                    if (buffer[i].LastAccess < buffer[oldestPageIndex].LastAccess)
                    {
                        oldestPageIndex = i;
                    }
                }

                if (buffer[oldestPageIndex].Status == 1)
                {
                    SavePageToFile(buffer[oldestPageIndex]);
                }
                buffer[oldestPageIndex] = LoadPage(pageNumber);
                return oldestPageIndex;
            }
            else
            {
                buffer.Add(LoadPage(pageNumber));
                return buffer.Count - 1;
            }
        }

        private void SavePageToFile(Page page)
        {
            fileStream.Seek(2 + page.PageNumber * PageSize, SeekOrigin.Begin);
            byte[] pageData = new byte[PageSize];
            Buffer.BlockCopy(page.Bitmap, 0, pageData, 0, 4);
            Buffer.BlockCopy(page.Data, 0, pageData, 4, PageSize - 4);
            fileStream.Write(pageData, 0, PageSize);
        }

        public int Read(long index)
        {
            if (index < 0 || index >= arraySize) throw new IndexOutOfRangeException();
            int pageIndex = GetPageIndex(index);
            int offset = (int)(index % ((PageSize - 4) / sizeof(int)));
            return buffer[pageIndex].Data[offset];
        }

        public void Write(long index, int value)
        {
            if (index < 0 || index >= arraySize) throw new IndexOutOfRangeException();
            int pageIndex = GetPageIndex(index);
            int offset = (int)(index % ((PageSize - 4) / sizeof(int)));
            Page temp = buffer[pageIndex];
            temp.Data[offset] = value;
            temp.Status = 1;
            temp.LastAccess = DateTime.Now;
            buffer[pageIndex] = temp;
        }

        public void Close()
        {
            foreach (var page in buffer)
            {
                if (page.Status == 1)
                {
                    SavePageToFile(page);
                }
            }
            fileStream.Close();
        }
    }
}
