using System;
using System.Collections.Generic;
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
        // ----- NUEVO: VARIABLES PARA HILOS SINCRONIZADOS -----
        private readonly object candado = new object();
        private Queue<string> colaMensajes = new Queue<string>();
        private Thread hiloProcesamiento;
        private bool ejecutar = true;


        public event Action<string> MensajeRecibido;
        // Evento extra para mostrar mensajes técnicos de hilos
        public event Action<string> MensajeDebug;


        public void Iniciar(string ip, int puerto)
        {
            client = new TcpClient();
            client.Connect(ip, puerto);

            stream = client.GetStream();

            // Iniciar hilo de recepción
            hiloRecepcion = new Thread(RecibirMensajes);
            hiloRecepcion.IsBackground = true;
            hiloRecepcion.Start();

            // Iniciar hilo de procesamiento
            hiloProcesamiento = new Thread(ProcesarMensajes);
            hiloProcesamiento.IsBackground = true;
            hiloProcesamiento.Start();
        }

        private void RecibirMensajes()
        {
            try
            {
                while (ejecutar)
                {
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    if (bytes <= 0) continue;

                    string mensaje = Encoding.UTF8.GetString(buffer, 0, bytes);

                    MensajeDebug?.Invoke("[RECEPCIÓN] Hilo quiere acceder a la cola...");

                    lock (candado)
                    {
                        MensajeDebug?.Invoke("[RECEPCIÓN] ENTRA a la sección crítica");
                        colaMensajes.Enqueue(mensaje);
                        MensajeDebug?.Invoke("[RECEPCIÓN] Mensaje encolado.");
                        MensajeDebug?.Invoke("[RECEPCIÓN] SALE de la sección crítica");
                    }
                }
            }
            catch { }
        }

        private void ProcesarMensajes()
        {
            while (ejecutar)
            {
                string mensaje = null;

                MensajeDebug?.Invoke("[PROCESAMIENTO] Hilo quiere acceder a la cola...");

                lock (candado)
                {
                    MensajeDebug?.Invoke("[PROCESAMIENTO] ENTRA a la sección crítica");

                    if (colaMensajes.Count > 0)
                    {
                        mensaje = colaMensajes.Dequeue();
                        MensajeDebug?.Invoke("[PROCESAMIENTO] Mensaje extraído de la cola.");
                    }
                    else
                    {
                        MensajeDebug?.Invoke("[PROCESAMIENTO] Cola vacía.");
                    }

                    MensajeDebug?.Invoke("[PROCESAMIENTO] SALE de la sección crítica");
                }

                if (mensaje != null)
                {
                    MensajeDebug?.Invoke("[PROCESAMIENTO] Procesando mensaje...");
                    MensajeRecibido?.Invoke(mensaje);
                }

                Thread.Sleep(50);
            }
        }

        public void Enviar(string texto)
        {
            if (stream == null) return;

            byte[] data = Encoding.UTF8.GetBytes(texto);
            stream.Write(data, 0, data.Length);
        }

        public void Detener()
        {
            ejecutar = false;
            hiloRecepcion?.Join();
            hiloProcesamiento?.Join();
            client?.Close();
        }
    }

}
