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
using System.Net.Http;
using Newtonsoft.Json;
using System.Runtime.Remoting.Channels;

namespace practica_integradora
{
    /// <summary>
    /// Lógica de interacción para registrar_cliente.xaml
    /// </summary>
    public partial class registrar_cliente : Window
    {
        public registrar_cliente()
        {
            InitializeComponent();
        }

         private async void btnRegistrarCliente_Click(object sender, RoutedEventArgs e)
        {
            await registrarClienteAsync();
        }

        public async Task registrarClienteAsync()
        {
            var cliente = new
            {
                nombre = TBnombre.Text,
                apellidos = TBapellidos.Text,
                correo = TBcorreo.Text,
                telefono = TBtelefono.Text,
                fecha_registro = DateTime.Now
            };

            string json = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5120");
                HttpResponseMessage response = await client.PostAsync("api/cliente", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("cliente registrado exitosamente");

                }
                else
                {
                    MessageBox.Show("error al registrar");
                }
                
            }

        }

       
    }
}
