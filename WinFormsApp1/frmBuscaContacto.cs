using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class frmBuscaContacto : Form
    {
        public int id = 0;
        public frmBuscaContacto()
        {
            InitializeComponent();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ConectaBaseDatos cbd = new ConectaBaseDatos(@"Data Source=CUSISTEMAS005D\SQLEXPRESS;Initial Catalog=agendame;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            AccederDatos acl = new AccederDatos(cbd);
            dgvDatos.DataSource = acl.Buscador(txtValor.Text);
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex >= 0)
            {
                this.id = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells[0].Value);
                this.Close();
            }
        }
    }
}
