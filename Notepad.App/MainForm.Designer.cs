namespace Notepad.App
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new MenuStrip();
            this.fileMenu = new ToolStripMenuItem();
            this.newMenuItem = new ToolStripMenuItem();
            this.openMenuItem = new ToolStripMenuItem();
            this.recentFilesMenu = new ToolStripMenuItem();
            this.saveMenuItem = new ToolStripMenuItem();
            this.saveAsMenuItem = new ToolStripMenuItem();
            this.fileSeparator = new ToolStripSeparator();
            this.exitMenuItem = new ToolStripMenuItem();
            this.editMenu = new ToolStripMenuItem();
            this.findMenuItem = new ToolStripMenuItem();
            this.fontMenuItem = new ToolStripMenuItem();
            this.wordWrapMenuItem = new ToolStripMenuItem();
            this.helpMenu = new ToolStripMenuItem();
            this.aboutMenuItem = new ToolStripMenuItem();

            this.txtEditor = new TextBox();
            this.statusStrip = new StatusStrip();
            this.lblCharCount = new ToolStripStatusLabel();
            this.lblSeparator = new ToolStripStatusLabel();
            this.lblFilePath = new ToolStripStatusLabel();

            this.openFileDialog = new OpenFileDialog();
            this.saveFileDialog = new SaveFileDialog();
            this.fontDialog = new FontDialog();

            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // menuStrip
            this.menuStrip.Items.AddRange(new ToolStripItem[] {
                this.fileMenu,
                this.editMenu,
                this.helpMenu });
            this.menuStrip.Location = new Point(0, 0);
            this.menuStrip.Size = new Size(800, 24);
            this.menuStrip.Text = "menuStrip";

            // fileMenu
            this.fileMenu.DropDownItems.AddRange(new ToolStripItem[] {
                this.newMenuItem,
                this.openMenuItem,
                this.recentFilesMenu,
                this.saveMenuItem,
                this.saveAsMenuItem,
                this.fileSeparator,
                this.exitMenuItem });
            this.fileMenu.Text = "&File";

            this.newMenuItem.Text = "&New";
            this.newMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            this.newMenuItem.Click += new EventHandler(this.newMenuItem_Click);

            this.openMenuItem.Text = "&Open...";
            this.openMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            this.openMenuItem.Click += new EventHandler(this.openMenuItem_Click);

            this.recentFilesMenu.Text = "Recent &Files";
            this.recentFilesMenu.DropDownOpening += new EventHandler(this.recentFilesMenu_DropDownOpening);

            this.saveMenuItem.Text = "&Save";
            this.saveMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            this.saveMenuItem.Click += new EventHandler(this.saveMenuItem_Click);

            this.saveAsMenuItem.Text = "Save &As...";
            this.saveAsMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            this.saveAsMenuItem.Click += new EventHandler(this.saveAsMenuItem_Click);

            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new EventHandler(this.exitMenuItem_Click);

            // editMenu
            this.editMenu.DropDownItems.AddRange(new ToolStripItem[] {
                this.findMenuItem,
                this.fontMenuItem,
                this.wordWrapMenuItem });
            this.editMenu.Text = "&Edit";

            this.findMenuItem.Text = "&Find...";
            this.findMenuItem.ShortcutKeys = Keys.Control | Keys.F;
            this.findMenuItem.Click += new EventHandler(this.findMenuItem_Click);

            this.fontMenuItem.Text = "F&ont...";
            this.fontMenuItem.Click += new EventHandler(this.fontMenuItem_Click);

            this.wordWrapMenuItem.Text = "&Word Wrap";
            this.wordWrapMenuItem.CheckOnClick = true;
            this.wordWrapMenuItem.Click += new EventHandler(this.wordWrapMenuItem_Click);

            // helpMenu
            this.helpMenu.DropDownItems.AddRange(new ToolStripItem[] {
                this.aboutMenuItem });
            this.helpMenu.Text = "&Help";

            this.aboutMenuItem.Text = "&About";
            this.aboutMenuItem.Click += new EventHandler(this.aboutMenuItem_Click);

            // txtEditor
            this.txtEditor.Dock = DockStyle.Fill;
            this.txtEditor.Multiline = true;
            this.txtEditor.AcceptsTab = true;
            this.txtEditor.AcceptsReturn = true;
            this.txtEditor.ScrollBars = ScrollBars.Both;
            this.txtEditor.WordWrap = true;
            this.txtEditor.Font = new Font("Consolas", 11F);
            this.txtEditor.BorderStyle = BorderStyle.None;
            this.txtEditor.HideSelection = false;
            this.txtEditor.TextChanged += new EventHandler(this.txtEditor_TextChanged);

            // statusStrip
            this.statusStrip.Items.AddRange(new ToolStripItem[] {
                this.lblCharCount,
                this.lblSeparator,
                this.lblFilePath });
            this.statusStrip.Location = new Point(0, 428);
            this.statusStrip.Size = new Size(800, 22);

            this.lblCharCount.Text = "Characters: 0";
            this.lblCharCount.AutoSize = true;

            this.lblSeparator.Text = "  |  ";
            this.lblSeparator.AutoSize = true;

            this.lblFilePath.Text = "(untitled)";
            this.lblFilePath.AutoSize = true;

            // dialogs
            this.openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.saveFileDialog.DefaultExt = "txt";

            // MainForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.txtEditor);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Notepad";
            this.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem newMenuItem;
        private ToolStripMenuItem openMenuItem;
        private ToolStripMenuItem recentFilesMenu;
        private ToolStripMenuItem saveMenuItem;
        private ToolStripMenuItem saveAsMenuItem;
        private ToolStripSeparator fileSeparator;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem editMenu;
        private ToolStripMenuItem findMenuItem;
        private ToolStripMenuItem fontMenuItem;
        private ToolStripMenuItem wordWrapMenuItem;
        private ToolStripMenuItem helpMenu;
        private ToolStripMenuItem aboutMenuItem;
        private TextBox txtEditor;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblCharCount;
        private ToolStripStatusLabel lblSeparator;
        private ToolStripStatusLabel lblFilePath;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private FontDialog fontDialog;
    }
}
