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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace practica_integradora
{
    /// <summary>
    /// Lógica de interacción para V_DG_clientes_registrados.xaml
    /// </summary>
    public partial class V_DG_clientes_registrados : Window
    {
        public V_DG_clientes_registrados()
        {
            InitializeComponent();
            mostrarCliente();
        }

        public async void mostrarCliente()
        {
            DGclientes.ItemsSource = await ObtenerClientesAsync();
        }

        public async Task<List<ClienteDTO>> ObtenerClientesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5120");

                HttpResponseMessage response = await client.GetAsync("api/cliente");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error al obtener clientes");
                    return new List<ClienteDTO>();
                }

                string json = await response.Content.ReadAsStringAsync();
                List<ClienteDTO> lista = JsonConvert.DeserializeObject<List<ClienteDTO>>(json);

                return lista;
            }
        }


    }
}
