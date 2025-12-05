using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica_integradora.clases
{
    internal class lista_usuarios
    {
        public static List<usuario> Usuarios = new List<usuario>
        {
            new usuario("frozz", "1234"),
            new usuario("admin", "admin"),
            new usuario("cliente", "pass"),
        };

        public static bool Validar(string nombre, string contrasena)
        {
            foreach (var u in Usuarios)
            {
                if (u.Nombre == nombre && u.Contraseña == contrasena)
                    return true;
            }

            return false;
        }
    }
}
