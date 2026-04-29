namespace Notepad.App
{
    partial class FindForm
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
            this.lblSearch = new Label();
            this.txtSearch = new TextBox();
            this.chkCaseSensitive = new CheckBox();
            this.btnFindNext = new Button();
            this.btnClose = new Button();
            this.lblStatus = new Label();
            this.SuspendLayout();

            // lblSearch
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new Point(12, 15);
            this.lblSearch.Text = "Find what:";

            // txtSearch
            this.txtSearch.Location = new Point(85, 12);
            this.txtSearch.Size = new Size(250, 23);
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);

            // chkCaseSensitive
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Location = new Point(85, 45);
            this.chkCaseSensitive.Text = "Match &case";

            // btnFindNext
            this.btnFindNext.Location = new Point(85, 75);
            this.btnFindNext.Size = new Size(120, 28);
            this.btnFindNext.Text = "Find &Next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new EventHandler(this.btnFindNext_Click);

            // btnClose
            this.btnClose.Location = new Point(215, 75);
            this.btnClose.Size = new Size(120, 28);
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new Point(12, 115);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Text = string.Empty;

            // FindForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AcceptButton = this.btnFindNext;
            this.CancelButton = this.btnClose;
            this.ClientSize = new Size(360, 145);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnFindNext);
            this.Controls.Add(this.chkCaseSensitive);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Find";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblSearch;
        private TextBox txtSearch;
        private CheckBox chkCaseSensitive;
        private Button btnFindNext;
        private Button btnClose;
        private Label lblStatus;
    }
}
