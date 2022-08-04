using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace WinFormsApp1
{
    internal class AccederDatos
    {
        private ConectaBaseDatos objConectaBaseDatos;
        public AccederDatos(ConectaBaseDatos conectabasedatos)
        {
            objConectaBaseDatos = conectabasedatos;
        }
        public void Insertar(Contacto contacto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = objConectaBaseDatos.ConectaBaseDatos2;
            cmd.CommandText = "insert into contacto(nombre, email, telefono, direccion, ciudad, codpost) values (@nombre, @email, @telefono, @direccion, @ciudad, @codpost); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@nombre", contacto.Nombre);
            cmd.Parameters.AddWithValue("@email", contacto.Email);
            cmd.Parameters.AddWithValue("@telefono", contacto.Telefono);
            cmd.Parameters.AddWithValue("@direccion", contacto.Direccion);
            cmd.Parameters.AddWithValue("@ciudad", contacto.Ciudad);
            cmd.Parameters.AddWithValue("@codpost", contacto.Codpost);
            objConectaBaseDatos.Conectar();
            contacto.Id = Convert.ToInt32(cmd.ExecuteScalar());
            objConectaBaseDatos.Desconectar();
        }
        public void Modificar(Contacto contacto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = objConectaBaseDatos.ConectaBaseDatos2;
            cmd.CommandText = "update contacto set nombre=@nombre, email=@email, telefono=@telefono, direccion=@direccion, ciudad=@ciudad, codpost=@codpost where id=@id;";
            cmd.Parameters.AddWithValue("@nombre", contacto.Nombre);
            cmd.Parameters.AddWithValue("@email", contacto.Email);
            cmd.Parameters.AddWithValue("@telefono", contacto.Telefono);
            cmd.Parameters.AddWithValue("@direccion", contacto.Direccion);
            cmd.Parameters.AddWithValue("@ciudad", contacto.Ciudad);
            cmd.Parameters.AddWithValue("@codpost", contacto.Codpost);
            cmd.Parameters.AddWithValue("@id", contacto.Id);
            objConectaBaseDatos.Conectar();
            cmd.ExecuteNonQuery();
            objConectaBaseDatos.Desconectar();
        }
        public void Eliminar(int valor)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = objConectaBaseDatos.ConectaBaseDatos2;
            cmd.CommandText = "delete from contacto where id=@id";
            cmd.Parameters.AddWithValue("@id", valor);
            objConectaBaseDatos.Conectar();
            cmd.ExecuteNonQuery();
            objConectaBaseDatos.Desconectar();
        }
        public DataTable Buscador(string valores)
        {
            DataTable tabla = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from contacto where nombre like '%"+ valores +"%'", objConectaBaseDatos.CadenaConectar);
            da.Fill(tabla);
            return tabla;
        }
        public Contacto modeloContacto(int codigo)
        {
            Contacto modelo = new Contacto();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = objConectaBaseDatos.ConectaBaseDatos2;
            cmd.CommandText = "Select * from contacto where id =" + codigo.ToString();
            objConectaBaseDatos.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.Id = Convert.ToInt32(registro["id"]);
                modelo.Nombre = Convert.ToString(registro["nombre"]);
                modelo.Email = Convert.ToString(registro["email"]);
                modelo.Telefono = Convert.ToString(registro["telefono"]);
                modelo.Direccion = Convert.ToString(registro["direccion"]);
                modelo.Ciudad = Convert.ToString(registro["ciudad"]);
                modelo.Codpost = Convert.ToString(registro["codpost"]);
            }
            return modelo;
        }
    }
}
