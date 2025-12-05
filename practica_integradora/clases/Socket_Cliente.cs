using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace practica_integradora.clases
{
    public class Socket_Cliente
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread hiloRecepcion;

        public event Action<string> MensajeRecibido;

        public void Iniciar(string ip, int puerto)
        {
            client = new TcpClient();
            client.Connect(ip, puerto);

            stream = client.GetStream();

            hiloRecepcion = new Thread(RecibirMensajes);
            hiloRecepcion.IsBackground = true;
            hiloRecepcion.Start();
        }

        private void RecibirMensajes()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    if (bytes <= 0) continue;

                    string mensaje = Encoding.UTF8.GetString(buffer, 0, bytes);

                    MensajeRecibido?.Invoke(mensaje);
                }
            }
            catch { }
        }

        public void Enviar(string texto)
        {
            if (stream == null) return;

            byte[] data = Encoding.UTF8.GetBytes(texto);
            stream.Write(data, 0, data.Length);
        }
    }
}
