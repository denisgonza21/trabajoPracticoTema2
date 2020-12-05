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
    /// Lógica de interacción para Libro.xaml
    /// </summary>
    public partial class Libro : Window
    {
        BibliotecaEntities datos;
        public Libro()
        {
            InitializeComponent();
            datos = new BibliotecaEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatosGrilla();

            cboEditorial.ItemsSource = datos.Editorial.ToList();
            cboEditorial.DisplayMemberPath = "nombre";
            cboEditorial.SelectedValuePath = "id";

            cboAutor.ItemsSource = datos.Autor.ToList();
            cboAutor.DisplayMemberPath = "nombre";
            cboAutor.SelectedValuePath = "id";
        }

        private void CargarDatosGrilla()
        {
            try
            {
                dtgLibro.ItemsSource = datos.Libro.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DtgEditorial_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dtgLibro.SelectedItem != null)
            {
                Libro l = (Libro)dtgLibro.SelectedItem;


                txtId.Text = l.id.ToString();
                txtNombre.Text = l.nombre;
                cboEditorial.SelectedItem = l.editorial;
                cboAutor.SelectedItem = l.autor;
                txtejemplares.Text = Convert.ToString(l.cantidad_ejemplares);
                txtdisponible.Text = Convert.ToString(l.cantidad_disponible);
            }
        }

        private void BtnEliminarDenis_Click(object sender, RoutedEventArgs e)
        {
            if (dtgLibro.SelectedItem != null)
            {
                Libro l = (Libro)dtgLibro.SelectedItem;
                datos.Libro.Remove(l);
                datos.SaveChanges();
                CargarDatosGrilla();
            }
            else
                MessageBox.Show("Debe seleccionar un Libro de la grilla para eliminar!");
        }

        private void BtnModificarDenis_Click(object sender, RoutedEventArgs e)
        {
            if (dtgLibro.SelectedItem != null)
            {
                Libro l = (Libro)dtgLibro.SelectedItem;

                l.nombre = txtNombre.Text;
                l.editorial = Convert.ToInt16((Editorial)cboEditorial.SelectedItem);
                l.autor = Convert.ToInt16((Autor)cboAutor.SelectedItem);
                l.cantidad_ejemplares = Convert.ToInt16(txtejemplares.Text);
                l.cantidad_disponible = Convert.ToInt16(txtdisponible.Text);
                datos.Entry(l).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();
                CargarDatosGrilla();
            }
            else
                MessageBox.Show("Debe seleccionar un Libro de la grilla para modificar!");
        }

        private void BtnGuardarDenis_Click(object sender, RoutedEventArgs e)
        {
            Libro libro = new Libro();

            libro.editorial = Convert.ToInt16((Editorial)cboEditorial.SelectedItem);
            libro.autor = Convert.ToInt16((Autor)cboAutor.SelectedItem);
            libro.cantidad_disponible = Convert.ToInt16(txtdisponible.Text);
            libro.cantidad_ejemplares = Convert.ToInt16(txtejemplares.Text);
            try
            {
                libro.nombre = txtNombre.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Debe insertar un nombre");
                txtNombre.Text = "";
                txtNombre.Focus();
                return;
                throw;
            }

            datos.Libro.Add(libro);
            datos.SaveChanges();
            CargarDatosGrilla();
        }
    }
}
