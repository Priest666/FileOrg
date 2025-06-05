using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileOrg
{
    internal class FileSorter
    {
        private readonly string _sourcePath;
        private readonly Dictionary<string, string> _extensionMap;

        public FileSorter(string sourcePath)
        {
            _sourcePath = sourcePath;
            _extensionMap = LoadConfig();
        }

        public void SortFiles()
        {
            string[] files = Directory.GetFiles(_sourcePath);

            foreach (string filePath in files)
            {
                string extension = Path.GetExtension(filePath);
                string category = _extensionMap.TryGetValue(extension, out var cat) ? cat : "Others";

                string targetFolder = Path.Combine(_sourcePath, category);
                Directory.CreateDirectory(targetFolder);

                string fileName = Path.GetFileName(filePath);
                string destinationPath = Path.Combine(targetFolder, fileName);
                destinationPath = GetUniqueFilePath(destinationPath);

                try
                {
                    File.Move(filePath, destinationPath);
                    string msg = $"Moved {fileName} to {category}";
                    Console.WriteLine(msg);
                }
                catch (Exception ex)
                {
                    string error = $"Error during transfer of file {fileName}: {ex.Message}";
                    Console.WriteLine(error);
                }
            }
        }

        private Dictionary<string, string> LoadConfig()
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");        

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException("Missing file config.json");
            }

            string json = File.ReadAllText(configPath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json)!;
        }

        private string GetUniqueFilePath(string path)
        {
            int count = 1;
            string directory = Path.GetDirectoryName(path)!;
            string baseName = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);

            while (File.Exists(path))
            {
                string tmpFileName = $"{baseName} ({count++}){extension}";
                path = Path.Combine(directory, tmpFileName);
            }

            return path;
        }  
    }
}
