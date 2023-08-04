using HappyBrowser.Controls.CtlEventArgs;
using HappyBrowser.Properties;
using System.Windows.Forms;

namespace HappyBrowser.Controls
{
    public class CtlSearchBox : Panel
    {

        public event EventHandler<HeaderSearchChangedAgrs>? OnGoSearch;

        private const int BUTTON_WIDTH = 16;

        private TextBox txtSearch;
        private Button btnClear;
        private Button btnSearch;

        public CtlSearchBox()
        {
            txtSearch = new();
            txtSearch.MaxLength = 500;
            txtSearch.AutoSize = false;
            txtSearch.BorderStyle=BorderStyle.None;
            txtSearch.Size = new Size(this.Width-BUTTON_WIDTH*2, this.Height);
            txtSearch.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtSearch.Location = new Point(0,0);
            txtSearch.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            txtSearch.KeyDown += (object? sender, KeyEventArgs e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    OnGoSearch?.Invoke(txtSearch,new(txtSearch.Text));
                }
            };
            this.Controls.Add(txtSearch);

            btnClear = new Button();
            btnClear.Margin=new Padding(0);
            btnClear.FlatAppearance.BorderSize=0;
            btnClear.FlatStyle=FlatStyle.Flat;
            btnClear.Size = new Size(BUTTON_WIDTH, this.Height);
            btnClear.Image = Resources.search_clean_16;
            btnClear.Location = new Point(txtSearch.Right, 0);
            btnClear.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            btnClear.Click += (object? sender, EventArgs e) => {
                txtSearch.Clear();
                txtSearch.Focus();
            };
            this.Controls.Add(btnClear);

            btnSearch = new Button();
            btnSearch.Margin=new Padding(0);
            btnSearch.FlatAppearance.BorderSize=0;
            btnSearch.FlatStyle=FlatStyle.Flat;
            btnSearch.Size = new Size(BUTTON_WIDTH,this.Height);
            btnSearch.Image = Resources.search_find_16;
            btnSearch.Location = new Point(btnClear.Right, 0);
            btnSearch.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            btnSearch.Click += (object? sender, EventArgs e) => {
                if (string.IsNullOrEmpty(txtSearch.Text)) return;
                OnGoSearch?.Invoke(txtSearch, new(txtSearch.Text));
            };
            this.Controls.Add(btnSearch);

            BorderStyle=BorderStyle.None;
            Margin=new Padding(0);
            SizeChanged += (sender, args) =>
            {
                txtSearch.Size = new Size(this.Width-BUTTON_WIDTH*2, this.Height);
                btnClear.Size = new Size(BUTTON_WIDTH, this.Height);
                btnSearch.Size = new Size(BUTTON_WIDTH, this.Height);
                btnClear.Location = new Point(txtSearch.Right, 0);
                btnSearch.Location = new Point(btnClear.Right, 0);
            };
        }

        public override string Text
        {
            get => btnSearch.Text;
            set => btnSearch.Text=value;
        }

    }
}
