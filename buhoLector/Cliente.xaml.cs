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

namespace buhoLector
{
    /// <summary>
    /// Lógica de interacción para Cliente.xaml
    /// </summary>
    public partial class Cliente : Window
    {
        BibliotecaEntities datos;
        public Cliente()
        {
            InitializeComponent();
            datos = new BibliotecaEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatosGrilla();
        }

        private void CargarDatosGrilla()
        {
            try
            {
                dtgCliente.ItemsSource = datos.Cliente.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DtgEditorial_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dtgCliente.SelectedItem != null)
            {
                Cliente c = (Cliente)dtgCliente.SelectedItem;

                txtId.Text = c.id.ToString();
                txtNroDocumento.Text = c.nroDocumento;
                txtNombre.Text = c.nombre;
                txtDireccion.Text = c.direccion;
                txtTelefono.Text = c.telefono;
            }
        }

        private void BtnEliminarDenis_Click(object sender, RoutedEventArgs e)
        {
            if (dtgCliente.SelectedItem != null)
            {
                Cliente c = (Cliente)dtgCliente.SelectedItem;
                datos.Cliente.Remove(c);
                datos.SaveChanges();
                CargarDatosGrilla();
            }
            else
                MessageBox.Show("Debe seleccionar un Cliente de la grilla para eliminar!");
        }

        private void BtnModificarDenis_Click(object sender, RoutedEventArgs e)
        {
            if (dtgCliente.SelectedItem != null)
            {
                Cliente c = (Cliente)dtgCliente.SelectedItem;

                c.nombre = txtNombre.Text;
                c.nroDocumento = txtNroDocumento.Text;
                c.direccion = txtDireccion.Text;
                c.telefono = txtTelefono.Text;
                datos.Entry(c).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();
                CargarDatosGrilla();
            }
            else
                MessageBox.Show("Debe seleccionar un Cliente de la grilla para modificar!");
        }

        private void BtnGuardarDenis_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();

            cliente.direccion = txtDireccion.Text;
            cliente.nroDocumento = txtNroDocumento.Text;
            cliente.telefono = txtTelefono.Text;
            try
            {
                cliente.nombre = txtNombre.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Debe insertar un nombre");
                txtNombre.Text = "";
                txtNombre.Focus();
                return;
                throw;
            }

            datos.Cliente.Add(cliente);
            datos.SaveChanges();
            CargarDatosGrilla();
        }

       
    }
}
