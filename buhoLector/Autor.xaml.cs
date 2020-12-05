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
    /// Lógica de interacción para Autor.xaml
    /// </summary>
    public partial class Autor : Window
    {
        BibliotecaEntities datos;
        public Autor()
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
                dtgAutor.ItemsSource = datos.Autor.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DtgEditorial_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dtgAutor.SelectedItem != null)
            {
                Autor a = (Autor)dtgAutor.SelectedItem;

                txtId.Text = a.id.ToString();
                txtNroDocumento.Text = a.nroDocumento;
                txtNombre.Text = a.nombre;
                txtDireccion.Text = a.direccion;
                txtTelefono.Text = a.telefono;
            }
        }

        private void BtnEliminarDenis_Click(object sender, RoutedEventArgs e)
        {

            if (dtgAutor.SelectedItem != null)
            {
                Autor a = (Autor)dtgAutor.SelectedItem;
                datos.Autor.Remove(a);
                datos.SaveChanges();
                CargarDatosGrilla();
            }
            else
                MessageBox.Show("Debe seleccionar un Autor de la grilla para eliminar!");
        }

        private void BtnModificarDenis_Click(object sender, RoutedEventArgs e)
        {
            if (dtgAutor.SelectedItem != null)
            {
                Autor a = (Autor)dtgAutor.SelectedItem;

                a.nombre = txtNombre.Text;
                a.nroDocumento = txtNroDocumento.Text;
                a.direccion = txtDireccion.Text;
                a.telefono = txtTelefono.Text;
                datos.Entry(a).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();
                CargarDatosGrilla();
            }
            else
                MessageBox.Show("Debe seleccionar un Autor de la grilla para modificar!");
        }

        private void BtnGuardarDenis_Click(object sender, RoutedEventArgs e)
        {
            Autor autor = new Autor();

            autor.direccion = txtDireccion.Text;
            autor.nroDocumento = txtNroDocumento.Text;
            autor.telefono = txtTelefono.Text;
            try
            {
                autor.nombre = txtNombre.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Debe insertar un nombre");
                txtNombre.Text = "";
                txtNombre.Focus();
                return;
                throw;
            }

            datos.Autor.Add(autor);
            datos.SaveChanges();
            CargarDatosGrilla();
        }
    }
}
