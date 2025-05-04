using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3Client
{
    internal class TranslatorController
    {
        private Client client;
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

            client.SendRequest(path);
            
            var entries = client.GetResponce().Split('|');

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

        public string GetFileText(string path)
        {
            client.SendRequest(path);
            return client.GetResponce();
        }

        public void OnItemSelected(string displayName)
        {
            string fullPath;
            if (DisplayNameToFullPath.TryGetValue(displayName, out fullPath))
            {
                if (Directory.Exists(fullPath))
                    DirectoryChanged?.Invoke(this, fullPath);
                else if (File.Exists(fullPath))
                    FileSelected?.Invoke(this, fullPath);
            }
        }

        public string[] ConnectToServer(string IP)
        {
            client = new Client(IP);
            client.Connect();
            return client.GetResponce().Split(',');
        }

        public void Disconnect()
        {
            client.Close();
        }
    }
}
