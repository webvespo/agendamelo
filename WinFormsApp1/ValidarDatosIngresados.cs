using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class ValidarDatosIngresados
    {
        public static bool soloNumeros(KeyPressEventArgs e)
        {
                if(char.IsNumber(e.KeyChar))
                {
                    e.Handled = false;
                    return true;
                }
                else if(char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                    return true;
                }
                else
                {
                    e.Handled=true;
                    return false;
                }
        }
        public static bool validarEmail(string pCorreo)
        {
            return pCorreo != null && Regex.IsMatch(pCorreo,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        public  static bool validarVacio(TextBox pTxt)
        {
            if(pTxt.Text == string.Empty)
            {
                pTxt.Focus();
                return true;            
            }
            else
            {
                return false;
            }
        }
    }
}
