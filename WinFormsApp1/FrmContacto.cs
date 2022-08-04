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
    public partial class FrmContacto : Form
    {
        private const string V = @"Data Source=CUSISTEMAS005D\SQLEXPRESS;Initial Catalog=agendame;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        public string operacion = "";
        public FrmContacto()
        {
            InitializeComponent();
        }
        public void ManejaBotones(int op)
        {
            panelDatos.Enabled = false;
            btnBuscar.Enabled = false;
            btnCambiar.Enabled = false;
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
            btnInsertar.Enabled = false;

            if (op == 1)
            {
                btnBuscar.Enabled = true;
                btnInsertar.Enabled = true;
            }
            if (op == 2)
            {
                panelDatos.Enabled = true;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            if (op == 3)
            {
                btnEliminar.Enabled=true;
                btnCambiar.Enabled=true;
                btnCancelar.Enabled = true;
            }
        }
        public void LimpiarTexto()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtCiudad.Clear();
            txtCodPost.Clear();
        }

        private void FrmContacto_Load(object sender, EventArgs e)
        {
            this.ManejaBotones(1);
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            this.operacion = "insertar";
            this.ManejaBotones(2);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.ManejaBotones(1);
            // blanquear campos de texto
            this.LimpiarTexto();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {            
                Contacto contacto = new Contacto();
                contacto.Nombre = txtNombre.Text;
                contacto.Email = txtEmail.Text;
                contacto.Telefono = txtTelefono.Text;
                contacto.Direccion = txtDireccion.Text;
                contacto.Ciudad = txtCiudad.Text;
                contacto.Codpost = txtCodPost.Text;
                String strConectar = V;
                ConectaBaseDatos conectabasedatos = new ConectaBaseDatos(strConectar);
                AccederDatos acl = new AccederDatos(conectabasedatos);
                if (this.operacion == "insertar")
                {
                    //agregar a la base de datos
                    acl.Insertar(contacto);
                    MessageBox.Show("Agendado con ID: " + contacto.Id.ToString());
                    this.ManejaBotones(1);
                    // blanquear campos de texto
                    this.LimpiarTexto();
                try
                    {
                        conectabasedatos.Conectar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    //opciones
                    contacto.Id = Convert.ToInt32(txtId.Text);
                    acl.Modificar(contacto);
                    MessageBox.Show("Modificado");
                    this.ManejaBotones(1);
                    // blanquear campos de texto
                    this.LimpiarTexto();
                }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmBuscaContacto b = new frmBuscaContacto();
            b.ShowDialog();
            if(b.id !=0)
            {
                //traer datos 
                String strConectar = V;
                ConectaBaseDatos conectabasedatos = new ConectaBaseDatos(strConectar);
                AccederDatos acl = new AccederDatos(conectabasedatos);
                Contacto contacto = acl.modeloContacto(b.id);
                txtId.Text = contacto.Id.ToString();
                txtNombre.Text = contacto.Nombre.ToString();
                txtEmail.Text = contacto.Email.ToString();
                txtTelefono.Text = contacto.Telefono.ToString();
                txtDireccion.Text = contacto.Direccion.ToString();
                txtCiudad.Text = contacto.Ciudad.ToString();
                txtCodPost.Text = contacto.Codpost.ToString();
                this.ManejaBotones(3);
            }
            b.Dispose();
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
 
            this.operacion = "modificar";
            this.ManejaBotones(2);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("¿Desea eliminar el contacto?","Aviso",MessageBoxButtons.YesNo);
            if (d.ToString() == "Yes")
            {
                String strConectar = V;
                ConectaBaseDatos conectabasedatos = new ConectaBaseDatos(strConectar);
                AccederDatos acl = new AccederDatos(conectabasedatos);
                acl.Eliminar(Convert.ToInt32(txtId.Text));
                this.ManejaBotones(1);
                // blanquear campos de texto
                this.LimpiarTexto();
            }
        }
        ErrorProvider errorP = new ErrorProvider();
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (ValidarDatosIngresados.validarVacio(txtNombre))
                errorP.SetError(txtNombre, "No puede quedar vacio");
            else
                errorP.Clear();

        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (!ValidarDatosIngresados.validarEmail(txtEmail.Text))
                errorP.SetError(txtEmail, "Email no válido");
            else
                errorP.Clear();
        }
 
        private void txtCodPost_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool valida = ValidarDatosIngresados.soloNumeros(e);
            if (!valida)
                errorP.SetError(txtCodPost, "Solo ingregar números");
            else
                errorP.Clear();

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            bool valida = ValidarDatosIngresados.soloNumeros(e);
            if (!valida)
                errorP.SetError(txtTelefono, "Solo ingregar números");
            else
                errorP.Clear();

            
        }
    }
}
