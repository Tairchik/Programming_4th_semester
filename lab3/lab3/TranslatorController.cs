using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    internal class TranslatorController
    {

        public Dictionary<string, string> DisplayNameToFullPath { get; private set; }

        public event EventHandler<string> DirectoryChanged;
        public event EventHandler<string> FileSelected;

        public TranslatorController()
        {
            DisplayNameToFullPath = new Dictionary<string, string>();
        }

        public string[] GetLogicalDrives()
        {
            return Directory.GetLogicalDrives();
        }

        public string[] GetDirectoryEntries(string path)
        {
            DisplayNameToFullPath.Clear();

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException("Каталог не найден: " + path);

            var entries = Directory.GetFileSystemEntries(path);

            foreach (var entry in entries)
            {
                string name = Path.GetFileName(entry);
                if (string.IsNullOrWhiteSpace(name))
                    name = entry;

                if (!DisplayNameToFullPath.ContainsKey(name))
                    DisplayNameToFullPath[name] = entry;
            }

            DirectoryChanged?.Invoke(this, path);

            return DisplayNameToFullPath.Keys.ToArray();
        }

        public void OnItemSelected(string displayName)
        {
            if (TryGetFullPath(displayName, out string fullPath))
            {
                if (IsDirectory(fullPath))
                    DirectoryChanged?.Invoke(this, fullPath);
                else if (IsFile(fullPath))
                    FileSelected?.Invoke(this, fullPath);
            }
        }

        public bool TryGetFullPath(string displayName, out string fullPath)
            => DisplayNameToFullPath.TryGetValue(displayName, out fullPath);

        public bool IsDirectory(string path) => Directory.Exists(path);
        public bool IsFile(string path) => File.Exists(path);
    }
}
