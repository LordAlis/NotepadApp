namespace Notepad.Common.Models
{
    public class EditorSettings
    {
        private string _fontFamily = "Consolas";
        private int _fontSize = 11;
        private bool _wordWrap = true;

        public string FontFamily
        {
            get => _fontFamily;
            set => _fontFamily = string.IsNullOrWhiteSpace(value) ? "Consolas" : value;
        }

        public int FontSize
        {
            get => _fontSize;
            set => _fontSize = Math.Clamp(value, 8, 72);
        }

        public bool WordWrap
        {
            get => _wordWrap;
            set => _wordWrap = value;
        }
    }
}
