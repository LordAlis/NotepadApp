using System.Diagnostics;
using Notepad.App.Services;
using Notepad.Common.Exceptions;
using Notepad.Common.Interfaces;
using Notepad.Common.Models;
using Notepad.Common.Services;

namespace Notepad.App
{
    public partial class MainForm : Form
    {
        private readonly IFileService _fileService;
        private readonly ISettingsService _settingsService;
        private readonly RecentFilesService _recentFilesService;

        private EditorSettings _settings;
        private EditorState _state;
        private string? _currentFilePath;
        private bool _isDirty;
        private FindForm? _findForm;

        public MainForm()
        {
            InitializeComponent();

            _fileService = new TextFileService();
            _settingsService = new SettingsService();
            _recentFilesService = new RecentFilesService();
            _settings = new EditorSettings();

            if (_fileService is FileServiceBase baseService)
                baseService.LogMessage += OnLogMessage;

            _settings = _settingsService.Load();
            ApplySettings();
            UpdateTitle();
            UpdateCharCount();
        }

        private void ApplySettings()
        {
            try
            {
                txtEditor.Font = new Font(_settings.FontFamily, _settings.FontSize);
            }
            catch
            {
                txtEditor.Font = new Font("Consolas", _settings.FontSize);
            }
            txtEditor.WordWrap = _settings.WordWrap;
            wordWrapMenuItem.Checked = _settings.WordWrap;
        }

        private void newMenuItem_Click(object? sender, EventArgs e)
        {
            if (!ConfirmDiscardIfDirty()) return;
            txtEditor.Clear();
            _currentFilePath = null;
            _isDirty = false;
            _state = EditorState.Idle;
            UpdateTitle();
            UpdateFilePathLabel();
        }

        private async void openMenuItem_Click(object? sender, EventArgs e)
        {
            if (!ConfirmDiscardIfDirty()) return;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                await LoadFileAsync(openFileDialog.FileName);
            }
        }

        private async void saveMenuItem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                saveAsMenuItem_Click(sender, e);
                return;
            }
            await SaveFileAsync(_currentFilePath);
        }

        private async void saveAsMenuItem_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentFilePath))
                saveFileDialog.FileName = Path.GetFileName(_currentFilePath);

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                await SaveFileAsync(saveFileDialog.FileName);
            }
        }

        private void exitMenuItem_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void findMenuItem_Click(object? sender, EventArgs e)
        {
            if (_findForm == null || _findForm.IsDisposed)
            {
                _findForm = new FindForm(txtEditor);
            }

            if (!_findForm.Visible)
                _findForm.Show(this);
            else
                _findForm.BringToFront();
        }

        private void fontMenuItem_Click(object? sender, EventArgs e)
        {
            fontDialog.Font = txtEditor.Font;
            if (fontDialog.ShowDialog(this) == DialogResult.OK)
            {
                txtEditor.Font = fontDialog.Font;
                _settings.FontFamily = fontDialog.Font.FontFamily.Name;
                _settings.FontSize = (int)Math.Round(fontDialog.Font.Size);
                _settingsService.Save(_settings);
            }
        }

        private void wordWrapMenuItem_Click(object? sender, EventArgs e)
        {
            _settings.WordWrap = wordWrapMenuItem.Checked;
            txtEditor.WordWrap = _settings.WordWrap;
            _settingsService.Save(_settings);
        }

        private void aboutMenuItem_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                "Notepad\nA C# OOP course project demonstrating File I/O and JSON serialization.\nBuilt with .NET 8 and Windows Forms.",
                "About",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void recentFilesMenu_DropDownOpening(object? sender, EventArgs e)
        {
            recentFilesMenu.DropDownItems.Clear();

            var recents = _recentFilesService.GetAll();
            if (recents.Count == 0)
            {
                var empty = new ToolStripMenuItem("(empty)") { Enabled = false };
                recentFilesMenu.DropDownItems.Add(empty);
                return;
            }

            int index = 1;
            foreach (var recent in recents)
            {
                string path = recent.Path;
                var item = new ToolStripMenuItem($"{index++}. {path}");
                item.Click += async (s, args) =>
                {
                    if (!ConfirmDiscardIfDirty()) return;
                    await LoadFileAsync(path);
                };
                recentFilesMenu.DropDownItems.Add(item);
            }

            recentFilesMenu.DropDownItems.Add(new ToolStripSeparator());
            var clearItem = new ToolStripMenuItem("Clear list");
            clearItem.Click += (s, args) => _recentFilesService.Clear();
            recentFilesMenu.DropDownItems.Add(clearItem);
        }

        private void txtEditor_TextChanged(object? sender, EventArgs e)
        {
            if (_state == EditorState.Loading) return;

            if (!_isDirty)
            {
                _isDirty = true;
                _state = EditorState.Modified;
                UpdateTitle();
            }
            UpdateCharCount();
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!ConfirmDiscardIfDirty())
            {
                e.Cancel = true;
                return;
            }

            _settingsService.Save(_settings);

            if (_fileService is FileServiceBase baseService)
            {
                baseService.LogMessage -= OnLogMessage;
                baseService.Dispose();
            }
        }

        private async Task LoadFileAsync(string path)
        {
            try
            {
                _state = EditorState.Loading;
                string content = await _fileService.LoadAsync(path);
                txtEditor.Text = content;
                _currentFilePath = path;
                _isDirty = false;
                _state = EditorState.Idle;
                _recentFilesService.Add(path);
                UpdateTitle();
                UpdateFilePathLabel();
                UpdateCharCount();
            }
            catch (FileServiceException ex)
            {
                _state = EditorState.Idle;
                MessageBox.Show(this, ex.Message, "Open failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task SaveFileAsync(string path)
        {
            try
            {
                _state = EditorState.Saving;
                await _fileService.SaveAsync(path, txtEditor.Text);
                _currentFilePath = path;
                _isDirty = false;
                _state = EditorState.Idle;
                _recentFilesService.Add(path);
                UpdateTitle();
                UpdateFilePathLabel();
            }
            catch (FileServiceException ex)
            {
                _state = EditorState.Idle;
                MessageBox.Show(this, ex.Message, "Save failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ConfirmDiscardIfDirty()
        {
            if (!_isDirty) return true;

            var result = MessageBox.Show(this,
                "You have unsaved changes. Save them now?",
                "Save changes?",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Cancel) return false;

            if (result == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(_currentFilePath))
                {
                    if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                        return false;
                    SaveFileAsync(saveFileDialog.FileName).GetAwaiter().GetResult();
                }
                else
                {
                    SaveFileAsync(_currentFilePath).GetAwaiter().GetResult();
                }
                return !_isDirty;
            }

            return true;
        }

        private void UpdateTitle()
        {
            string name = string.IsNullOrEmpty(_currentFilePath)
                ? "Untitled"
                : Path.GetFileName(_currentFilePath);
            string dirtyMark = _isDirty ? "*" : string.Empty;
            this.Text = $"{dirtyMark}{name} - Notepad";
        }

        private void UpdateFilePathLabel()
        {
            lblFilePath.Text = string.IsNullOrEmpty(_currentFilePath)
                ? "(untitled)"
                : _currentFilePath;
        }

        private void UpdateCharCount()
        {
            lblCharCount.Text = $"Characters: {txtEditor.TextLength}";
        }

        private void OnLogMessage(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
