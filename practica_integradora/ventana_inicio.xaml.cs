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
    /// Lógica de interacción para ventana_inicio.xaml
    /// </summary>
    public partial class ventana_inicio : Window
    {
        public ventana_inicio()
        {
            InitializeComponent();
        }

        private void Bclientes_Checked(object sender, RoutedEventArgs e)
        {
            if (Bclientes.IsChecked == true)
            {
                registrar_cliente.Navigate(new P_registrar_cliente());
            }
        }
    }
}
