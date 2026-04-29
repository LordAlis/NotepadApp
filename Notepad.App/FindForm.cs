namespace Notepad.App
{
    public partial class FindForm : Form
    {
        private readonly TextBox _editor;

        public FindForm(TextBox editor)
        {
            InitializeComponent();
            _editor = editor;
        }

        private void btnFindNext_Click(object? sender, EventArgs e)
        {
            FindNext();
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object? sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;
        }

        private void FindNext()
        {
            string needle = txtSearch.Text;
            if (string.IsNullOrEmpty(needle))
            {
                lblStatus.Text = "Enter a search term.";
                return;
            }

            string haystack = _editor.Text;
            int startIndex = _editor.SelectionStart + _editor.SelectionLength;
            if (startIndex >= haystack.Length) startIndex = 0;

            StringComparison comparison = chkCaseSensitive.Checked
                ? StringComparison.Ordinal
                : StringComparison.OrdinalIgnoreCase;

            int index = haystack.IndexOf(needle, startIndex, comparison);
            if (index < 0 && startIndex > 0)
            {
                index = haystack.IndexOf(needle, 0, comparison);
                if (index >= 0)
                    lblStatus.Text = "Search wrapped to the beginning.";
            }

            if (index < 0)
            {
                lblStatus.Text = $"Cannot find \"{needle}\".";
                return;
            }

            _editor.Focus();
            _editor.Select(index, needle.Length);
            _editor.ScrollToCaret();
            lblStatus.Text = $"Match at position {index}.";
        }
    }
}
