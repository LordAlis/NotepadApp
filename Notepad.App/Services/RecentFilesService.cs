using Notepad.Common.Helpers;
using Notepad.Common.Models;

namespace Notepad.App.Services
{
    public class RecentFilesService
    {
        private const int MaxItems = 5;
        private readonly string _filePath;
        private List<RecentFile> _items;

        public RecentFilesService()
        {
            string appDataDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "NotepadApp");
            Directory.CreateDirectory(appDataDir);
            _filePath = Path.Combine(appDataDir, "recent.json");
            _items = LoadFromDisk();
        }

        public IReadOnlyList<RecentFile> GetAll()
        {
            return _items.AsReadOnly();
        }

        public void Add(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return;

            _items.RemoveAll(f =>
                string.Equals(f.Path, path, StringComparison.OrdinalIgnoreCase));

            _items.Insert(0, new RecentFile(path));

            while (_items.Count > MaxItems)
                _items.RemoveAt(_items.Count - 1);

            SaveToDisk();
        }

        public void Clear()
        {
            _items.Clear();
            SaveToDisk();
        }

        private List<RecentFile> LoadFromDisk()
        {
            if (!File.Exists(_filePath))
                return new List<RecentFile>();

            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonHelper.Deserialize<List<RecentFile>>(json) ?? new List<RecentFile>();
            }
            catch
            {
                return new List<RecentFile>();
            }
        }

        private void SaveToDisk()
        {
            try
            {
                string json = JsonHelper.Serialize(_items);
                File.WriteAllText(_filePath, json);
            }
            catch
            {
                // Best-effort; ignore write failures.
            }
        }
    }
}
