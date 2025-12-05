using practica_integradora.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace practica_integradora
{
    /// <summary>
    /// Lógica de interacción para V_inicio_sesion.xaml
    /// </summary>
    public partial class V_inicio_sesion : Window
    {
        public V_inicio_sesion()
        {
            InitializeComponent();
        }

        private void enviar_queja_Click(object sender, RoutedEventArgs e)
        {
            string usuario = usu.Text;
            string contrase = contra.Password;

            bool valido = lista_usuarios.Validar(usuario, contrase);

            if (valido)
            {
                MessageBox.Show("inicio de sesion exitosa!");

                ventana_inicio RC = new ventana_inicio();
                RC.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("usuario o contrasena incorrecta");
            }
        }
    }
}
