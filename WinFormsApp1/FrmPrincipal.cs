namespace WinFormsApp1
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }



        private void btnContacto_Click_1(object sender, EventArgs e)
        {
            FrmContacto f = new FrmContacto();
            f.ShowDialog();
            f.Dispose();
        }
    }
}