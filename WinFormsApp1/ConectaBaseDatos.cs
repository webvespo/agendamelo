using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    class ConectaBaseDatos
    {
        private String _cadenaConectar;
        private SqlConnection _conectabasedatos;

        //public ConectaBaseDatos(string datosconectar)
        public ConectaBaseDatos(string datosconectar)
        {
            this._conectabasedatos = new SqlConnection();
            this.CadenaConectar = datosconectar;
            this._conectabasedatos.ConnectionString = datosconectar;
        }
        public string CadenaConectar
            {
                get { return this._cadenaConectar; }
                set { this._cadenaConectar = value; }
            }
        public SqlConnection ConectaBaseDatos2
            { 
                get { return this._conectabasedatos; } 
                set { this._conectabasedatos = value; }
            }
        public void Conectar()
        {
            this._conectabasedatos.Open();
        }
        public void Desconectar()
            {
                this._conectabasedatos.Close();
            }
    }
}
