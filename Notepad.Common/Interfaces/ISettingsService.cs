using Notepad.Common.Models;

namespace Notepad.Common.Interfaces
{
    public interface ISettingsService
    {
        EditorSettings Load();
        void Save(EditorSettings settings);
    }
}
