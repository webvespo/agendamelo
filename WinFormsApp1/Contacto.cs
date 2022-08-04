using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class Contacto
    {
        public Contacto()
        {
            this.Id = 0;
            this.nombre = "";
            this.Email = "";
            this.telefono = "";
            this.direccion = "";
            this.ciudad = "";
            this.codpost = "";

        }
        public Contacto(int id, string nombre, string email, string telefono, string direccion, string ciudad, string codpost)
        {
            Id = id;
            Nombre = nombre;
            Email = email;
            Telefono = telefono;
            Direccion = direccion;
            Ciudad = ciudad;
            Codpost = codpost;
        }

        private int id;
        public int Id { get; set; }
        private string nombre;
        public string Nombre { get; set; }
        private string email;
        public string Email { get; set; }
        private string telefono;
        public string Telefono { get; set; }
        private string direccion;
        public string Direccion { get; set; }
        private string ciudad;
        public string Ciudad { get; set; }
        private string codpost;
        public string Codpost { get; set; }
    }
}
