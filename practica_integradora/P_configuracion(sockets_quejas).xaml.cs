using practica_integradora.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace practica_integradora
{
    /// <summary>
    /// Lógica de interacción para P_configuracion_sockets_quejas_.xaml
    /// </summary>
    public partial class P_configuracion_sockets_quejas_ : Page
    {
        Socket_Cliente socket;
        private readonly object candadoMensajes = new object();
        public P_configuracion_sockets_quejas_()
        {
            InitializeComponent();

            socket = new Socket_Cliente();
            socket.MensajeRecibido += MostrarMensaje;
            socket.MensajeDebug += MostrarDebug;
            socket.Iniciar("192.168.1.83", 5000); //hola
    
        }


        private void MostrarMensaje(string mensaje)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                listaMensajes.Items.Add(mensaje);
            });
        }

        private void MostrarDebug(string mensaje)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                listaHilos.Items.Add(mensaje);
            });
        }

        private void EnviarMensaje(MensajeBase mensaje)
        {
            string texto = mensaje.Formatear();
            socket.Enviar(texto);
        }


        private void enviar_mensaje_Click(object sender, RoutedEventArgs e)
        {
            if (tipoMensaje.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un tipo de mensaje.");
                return;
            }

            string tipo = (tipoMensaje.SelectedItem as ComboBoxItem).Content.ToString();

            MensajeBase mensaje;

            switch (tipo)
            {
                case "Queja":
                    mensaje = new MensajeQueja();
                    break;

                case "Sugerencia":
                    mensaje = new MensajeSugerencia();
                    break;

                case "General":
                    mensaje = new MensajeGeneral();
                    break;

                default:
                    MessageBox.Show("Tipo inválido.");
                    return;
            }

            mensaje.Usuario = usu.Text;
            mensaje.Contenido = queja.Text;
            mensaje.Fecha = DateTime.Now;

            EnviarMensaje(mensaje);
            MessageBox.Show("Mensaje enviado.");
        }

        private void enviar_sugerencia_Click(object sender, RoutedEventArgs e)
        {
            MensajeBase mensaje = new MensajeSugerencia()
            {
                Usuario = usu.Text,
                Contenido = queja.Text,
                Fecha = DateTime.Now
            };

            EnviarMensaje(mensaje);
            MessageBox.Show("mensaje enviado");

        }

        private void enviar_general_Click(object sender, RoutedEventArgs e)
        {
            MensajeBase mensaje = new MensajeGeneral()
            {
                Usuario = usu.Text,
                Contenido = queja.Text,
                Fecha = DateTime.Now
            };
            EnviarMensaje(mensaje);
            MessageBox.Show("mensaje enviado");

        }
 

        private void listaMensajes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
