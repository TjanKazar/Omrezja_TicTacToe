namespace RO_Naloga3_TjanKazar
{
    public partial class startForm : Form
    {
        public startForm()
        {
            InitializeComponent();
        }

        private void startForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void btnServerClick(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(true);
            Visible = false;
            if (!form1.IsDisposed)
            {
                form1.ShowDialog();
            }
            Visible = true;
        }

        private async void btnClientClick(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(false);
            Visible = false;
            if (!form1.IsDisposed)
            {
                form1.ShowDialog();
            }
            Visible = true;
        }
    }
}