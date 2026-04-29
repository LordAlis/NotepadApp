using Notepad.Common.Helpers;
using Notepad.Common.Interfaces;
using Notepad.Common.Models;

namespace Notepad.App.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly string _filePath;

        public SettingsService()
        {
            string appDataDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "NotepadApp");
            Directory.CreateDirectory(appDataDir);
            _filePath = Path.Combine(appDataDir, "settings.json");
        }

        public EditorSettings Load()
        {
            if (!File.Exists(_filePath))
                return new EditorSettings();

            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonHelper.Deserialize<EditorSettings>(json) ?? new EditorSettings();
            }
            catch
            {
                return new EditorSettings();
            }
        }

        public void Save(EditorSettings settings)
        {
            try
            {
                string json = JsonHelper.Serialize(settings);
                File.WriteAllText(_filePath, json);
            }
            catch
            {
                // Settings persistence is best-effort; ignore write failures.
            }
        }
    }
}
