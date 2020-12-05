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
    /// Lógica de interacción para Editorial.xaml
    /// </summary>
    public partial class Editorial : Window
    {
        BibliotecaEntities datos;
        public Editorial()
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
                dtgEditorial.ItemsSource = datos.Editorial.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DtgEditorial_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (dtgEditorial.SelectedItem != null)
            {
                Editorial ed = (Editorial)dtgEditorial.SelectedItem;

                txtId.Text = ed.id.ToString();
                txtNombre.Text = ed.nombre;
                txtDireccion.Text = ed.direccion;
                txtTelefono.Text = ed.telefono;
            }
        }

        private void BtnEliminarDenis_Click(object sender, RoutedEventArgs e)
        {
            if (dtgEditorial.SelectedItem != null)
            {
                Editorial ed = (Editorial)dtgEditorial.SelectedItem;
                datos.Editorial.Remove(ed);
                datos.SaveChanges();
                CargarDatosGrilla();
            }
            else
                MessageBox.Show("Debe seleccionar una Editorial de la grilla para eliminar!");
        }

        private void BtnModificarDenis_Click(object sender, RoutedEventArgs e)
        {
            if (dtgEditorial.SelectedItem != null)
            {
                Editorial ed = (Editorial)dtgEditorial.SelectedItem;

                ed.nombre = txtNombre.Text;
                ed.direccion = txtDireccion.Text;
                ed.telefono = txtTelefono.Text;
                datos.Entry(ed).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();
                CargarDatosGrilla();
            }
            else
                MessageBox.Show("Debe seleccionar una Editorial de la grilla para modificar!");
        }

        private void BtnGuardarDenis_Click(object sender, RoutedEventArgs e)
        {
            Editorial editorial = new Editorial();
            
            editorial.direccion = txtDireccion.Text;
            editorial.telefono = txtTelefono.Text;
            try
            {
                editorial.nombre = txtNombre.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Debe insertar un nombre");
                txtNombre.Text = "";
                txtNombre.Focus();
                return;
                throw;
            }

            datos.Editorial.Add(editorial);
            datos.SaveChanges();
            CargarDatosGrilla();
        }
    }
}
