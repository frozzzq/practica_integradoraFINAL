using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace practica_integradora
{
    /// <summary>
    /// Lógica de interacción para P_registrar_membresia.xaml
    /// </summary>
    public partial class P_registrar_membresia : Page
    {
        public P_registrar_membresia()
        {
            InitializeComponent();
        }

        public class Membresia
        {
            public int id_membresia { get; set; }
            public string nombre { get; set; }
            public decimal costo { get; set; }
            public int duracion_dias { get; set; }
        }

        public async Task RegistrarMembresiaAsync()
        {
            var membresia = new
            {
                nombre = TBnombre_membresia.Text,
                costo = decimal.Parse(TBcosto.Text),
                duracion_dias = int.Parse(TBduracion_dias.Text)
            };

            string json = JsonConvert.SerializeObject(membresia);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5120");
                HttpResponseMessage response = await client.PostAsync("api/membresias", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Membresía registrada exitosamente");
                }
                else
                {
                    MessageBox.Show("Error al registrar membresía");
                }
            }
        }

        public async Task EliminarMembresiaAsync(int idMembresia)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5120");

                HttpResponseMessage response = await client.DeleteAsync($"api/membresias/{idMembresia}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Membresía eliminada correctamente");
                }
                else
                {
                    MessageBox.Show("Error al eliminar membresía");
                }
            }
        }

        private async void btnEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void btnRegistrarMembresia_Click(object sender, RoutedEventArgs e)
        {
            await RegistrarMembresiaAsync();
        }

        private async void btnEliminarMembresia_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(TBid_eliminar.Text);
            await EliminarMembresiaAsync(id);
        }
    }
}
