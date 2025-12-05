using practica_integradora.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
using practica_integradora.clases;

namespace practica_integradora
{
    /// <summary>
    /// Lógica de interacción para P_configuracion_sockets_quejas_.xaml
    /// </summary>
    public partial class P_configuracion_sockets_quejas_ : Page
    {
        Socket_Cliente socket;
        public P_configuracion_sockets_quejas_()
        {
            InitializeComponent();

            socket = new Socket_Cliente();
            socket.MensajeRecibido += MostrarMensaje;
            socket.Iniciar("192.168.1.83", 5000); //hola
        }

        private void MostrarMensaje(string mensaje)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                listaMensajes.Items.Add(mensaje);
            });
        }



        

        private async void enviar_queja_Click(object sender, RoutedEventArgs e)
        {
            var mensaje = new MensajeQueja
            {
               Usuario = usu.Text,
               Contenido = queja.Text,
               Fecha = DateTime.Now,
            };
            
            socket.Enviar(mensaje.Formatear());
            
            MessageBox.Show("mensaje enviado");
        }
        public async Task EnviarMensajeSocket(string texto)
        {
            try
            {
                TcpClient client = new TcpClient("192.168.1.94", 5000);

                byte[] data = Encoding.UTF8.GetBytes(texto);
                NetworkStream stream = client.GetStream();

                await stream.WriteAsync(data, 0, data.Length);
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error enviando mensaje: " + ex.Message);
            }
        }

    }
}
