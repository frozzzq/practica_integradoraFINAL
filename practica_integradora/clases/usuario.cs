using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica_integradora.clases
{
    internal class usuario
    {
        public string Nombre { get; set; }
        public string Contraseña { get; set; }

        public usuario(string nombre, string contraseña)
        {
            Nombre = nombre;
            Contraseña = contraseña;
        }


    }



 



}
