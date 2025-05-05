using System.Net.Sockets;

namespace lab3Client
{
    internal class TranslatorController
    {
        private Client client;
        public Dictionary<string, string> DisplayNameToFullPath { get; private set; }

        public event EventHandler<string> DirectoryChanged;
        public event EventHandler<string> FileSelected;
        public event Action<string> Errors;
        public event Action<string> SocketError;

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
            try
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
            catch (IOException socketEx)
            {
                SocketError?.Invoke(socketEx.Message);
                return DisplayNameToFullPath.Keys.ToArray();
            }
            catch (Exception ex)
            {
                Errors?.Invoke(ex.Message);
                return DisplayNameToFullPath.Keys.ToArray();
            }

        }

        public string GetFileText(string path)
        {
            client.SendRequest(path);
            return client.GetResponce();
        }

        public void OnItemSelected(string displayName)
        {
            try
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
            catch (IOException socketEx)
            {
                SocketError?.Invoke(socketEx.Message);
            }
            catch (Exception ex)
            {
                Errors?.Invoke(ex.Message);
            }
        }

        public string[] ConnectToServer(string IP)
        {
            try
            {
                if (client != null)
                {
                    if (client.Connected)
                    {
                        client.Close();
                    }
                }

                client = new Client(IP);
                client.Connect();

                return client.GetResponce().Split(',');
            }
            catch (SocketException socketEx)
            {
                SocketError?.Invoke(socketEx.Message);
                return new string[] { "" };
            }
            catch (Exception ex)
            {
                Errors?.Invoke(ex.Message);
                return new string[] {""};
            }
        }

        public void Disconnect()
        {
            try
            {
                if (client != null)
                {
                    if (client.Connected)
                    {
                        client.Close();
                    }
                }
            }
            catch (IOException socketEx)
            {
                SocketError?.Invoke(socketEx.Message);
            }
            catch (Exception ex)
            {
                Errors?.Invoke(ex.Message);
            }
        }
    }
}
