using HappyBrowser.Properties;
using System.Windows.Forms;

namespace HappyBrowser.Controls
{
    public class CtlAddressBox : Panel
    {

        public event EventHandler? OnAccessWebUrl;
        public event EventHandler? OnWebCollectClicked;

        private const int CLEAN_BUTTON_WIDTH = 16;
        private const int SEARCH_BUTTON_WIDTH = 28;
        private const int FAV_BUTTON_WIDTH = 28;

        private TextBox txtAddress;
        private Button btnClear;
        private Button btnSearch;
        private Button btnFav;

        public CtlAddressBox()
        {
            txtAddress = new();
            txtAddress.MaxLength = 10000;
            txtAddress.AutoSize = false;
            txtAddress.BorderStyle=BorderStyle.None;
            txtAddress.Size = new Size(this.Width-CLEAN_BUTTON_WIDTH*2, this.Height);
            txtAddress.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtAddress.Location = new Point(0,0);
            txtAddress.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            txtAddress.KeyDown += (object? sender, KeyEventArgs e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    OnAccessWebUrl?.Invoke(txtAddress, e);
                }
            };
            this.Controls.Add(txtAddress);

            btnClear = new();
            btnClear.Margin=new Padding(0);
            btnClear.FlatAppearance.BorderSize=0;
            btnClear.FlatStyle=FlatStyle.Flat;
            btnClear.Size = new Size(CLEAN_BUTTON_WIDTH, this.Height);
            btnClear.Image = Resources.search_clean_16;
            btnClear.Location = new Point(txtAddress.Right, 0);
            btnClear.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            btnClear.Click += (object? sender, EventArgs e) => {
                txtAddress.Clear();
                txtAddress.Focus();
            };
            this.Controls.Add(btnClear);

            btnSearch = new ();
            btnSearch.Margin=new Padding(0);
            btnSearch.FlatAppearance.BorderSize=0;
            btnSearch.FlatStyle=FlatStyle.Flat;
            btnSearch.Size = new Size(SEARCH_BUTTON_WIDTH, this.Height);
            btnSearch.Image = Resources.search_find_16;
            btnSearch.Location = new Point(btnClear.Right, 0);
            btnSearch.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            btnSearch.Click += (object? sender, EventArgs e) => {
                if (string.IsNullOrEmpty(txtAddress.Text)) return;
                OnAccessWebUrl?.Invoke(txtAddress, e);
            };
            this.Controls.Add(btnSearch);

            btnFav = new();
            btnFav.Margin=new Padding(0);
            btnFav.FlatAppearance.BorderSize=0;
            btnFav.FlatStyle=FlatStyle.Flat;
            btnFav.Size = new Size(FAV_BUTTON_WIDTH, this.Height);
            btnFav.Image = Resources.fav_16;
            btnFav.Location = new Point(btnSearch.Right, 0);
            btnFav.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            btnFav.Click += (object? sender, EventArgs e) => {
                if (string.IsNullOrEmpty(txtAddress.Text)) return;
                OnWebCollectClicked?.Invoke(txtAddress, e);
            };
            this.Controls.Add(btnFav);

            BorderStyle=BorderStyle.None;
            Margin=new Padding(0);
            SizeChanged += (sender, args) =>
            {
                txtAddress.Size = new Size(this.Width-CLEAN_BUTTON_WIDTH-SEARCH_BUTTON_WIDTH-FAV_BUTTON_WIDTH, this.Height);
                
                btnClear.Size = new Size(CLEAN_BUTTON_WIDTH, this.Height);
                btnClear.Location = new Point(txtAddress.Right, 0);

                btnSearch.Size = new Size(SEARCH_BUTTON_WIDTH, this.Height);
                btnSearch.Location = new Point(btnClear.Right, 0);

                btnFav.Size = new Size(FAV_BUTTON_WIDTH, this.Height);
                btnFav.Location = new Point(btnSearch.Right, 0);
            };
        }

        public override string Text { 
            get => txtAddress.Text; 
            set => txtAddress.Text=value; 
        }

        public void SelectAll()
        {
            this.txtAddress.Focus();
            this.txtAddress.SelectAll();
        }
    }
}
