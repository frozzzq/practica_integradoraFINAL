using Newtonsoft.Json;
using practica_integradora.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace practica_integradora
{
    /// <summary>
    /// Lógica de interacción para P_registrar_cliente.xaml
    /// </summary>
    public partial class P_registrar_cliente : Page
    {
        public P_registrar_cliente()
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

        public async Task borrarClienteAsync(int idCliente)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5120");

                HttpResponseMessage response = await client.DeleteAsync($"api/cliente/{idCliente}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cliente eliminado correctamente");
                }
                else
                {
                    MessageBox.Show("Error al eliminar cliente");
                }
            }
        }
        


        private async void btnEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            int Id = int.Parse(TBid.Text);
            await borrarClienteAsync(Id);
        }

        private void btnMostrarClientes_Click(object sender, RoutedEventArgs e)
        {
            V_DG_clientes_registrados ventanaClientes = new V_DG_clientes_registrados();
            ventanaClientes.Show();
        }
    }
}
