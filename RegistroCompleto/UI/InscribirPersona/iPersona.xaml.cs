using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RegistroCompleto.Entidades;
using RegistroCompleto.BLL;

namespace RegistroCompleto.UI.InscribirPersona
{
    /// <summary>
    /// Interaction logic for iPersona.xaml
    /// </summary>
    public partial class iPersona : Window
    {
        public iPersona()
        {
            InitializeComponent();
            FechaDatePicker.SelectedDate = DateTime.Now;
            BalanceTextBox.Text = "0";
            MontoTextBox.Text = "0";
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Inscripciones inscripcion = new Inscripciones();
            bool paso = false;

            if (!validar())
                return;

            inscripcion = llenaClase();

            if (Convert.ToInt32(InscripcionIdComboBox.SelectedIndex) == 0)
            {
                paso = InscripcionesBLL.Guardar(inscripcion);
            }
            else
            {
                if (!existeEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar una Inscripcion que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (inscripcion.Balance <= 0)
                    return;

                if(inscripcion.Monto<=inscripcion.Balance)
                    paso = InscripcionesBLL.Modificar(inscripcion);
                else
                {
                    MessageBox.Show("Esta sobre pagando");
                }
            }

            if (paso)
            {
                limpiarInscripcion();
                obtenerInscripciones(Convert.ToInt32(PersonaIdTextBox.Text));
                MessageBox.Show("Guardado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Personas persona = new Personas();
            int.TryParse(PersonaIdTextBox.Text, out id);

            limpiarTodo();

            persona = PersonasBLL.Buscar(id);

            if (persona != null)
            {
                MessageBox.Show("Persona Encontrada");
                llenaCampoPersona(persona);
                obtenerInscripciones(id);
            }
            else
                MessageBox.Show("Persona no Encontrada");
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(InscripcionIdComboBox.SelectedValue.ToString(), out id);

            limpiarInscripcion();

            if (InscripcionesBLL.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("No se puede eliminar una Inscripcion que no existe");
        }

        private void limpiarTodo()
        {
            reiniciarInscripciones();
            FechaDatePicker.SelectedDate = DateTime.Now;
            PersonaIdTextBox.Text = string.Empty;
            NombreTextBox.Text = string.Empty;
            ComentariosTextBox.Text = string.Empty;
            BalanceTextBox.Text = "0";
            MontoTextBox.Text = "0";
        }

        private void limpiarInscripcion()
        {
            reiniciarInscripciones();
            FechaDatePicker.SelectedDate = DateTime.Now;
            ComentariosTextBox.Text = string.Empty;
            BalanceTextBox.Text = "0";
            MontoTextBox.Text = "0";
        }

        private void llenaCampoPersona(Personas persona)
        {
            PersonaIdTextBox.Text = Convert.ToString(persona.PersonaId);
            NombreTextBox.Text = persona.Nombre;
        }

        private void llenaCampoInscripcion(Inscripciones inscripcion)
        {
            FechaDatePicker.SelectedDate = inscripcion.Fecha;
            ComentariosTextBox.Text = inscripcion.Comentarios;
            BalanceTextBox.Text = Convert.ToString(inscripcion.Balance);
            MontoTextBox.Text = "0";
        }

        private Inscripciones llenaClase()
        {
            Inscripciones inscripcion = new Inscripciones();
            inscripcion.InscripcionId = Convert.ToInt32(InscripcionIdComboBox.SelectedValue.ToString());
            inscripcion.Fecha = (DateTime) FechaDatePicker.SelectedDate;
            inscripcion.PersonaId = Convert.ToInt32(PersonaIdTextBox.Text);
            inscripcion.Comentarios = ComentariosTextBox.Text;
            inscripcion.Balance = Convert.ToInt32(BalanceTextBox.Text);
            inscripcion.Monto = Convert.ToInt32(MontoTextBox.Text);

            return inscripcion;
        }

        private bool existeEnLaBaseDeDatos()
        {
            Inscripciones inscripcion = InscripcionesBLL.Buscar(Convert.ToInt32(InscripcionIdComboBox.SelectedValue.ToString()));

            return (inscripcion.PersonaId == Convert.ToInt32(PersonaIdTextBox.Text));
        }

        private void obtenerInscripciones(int id)
        {
            reiniciarInscripciones();
            List<Inscripciones> lista = InscripcionesBLL.GetList(p => p.PersonaId == id);
            foreach(Inscripciones inscripcion in lista)
            {
                InscripcionIdComboBox.Items.Add(inscripcion.InscripcionId);
            }
        }

        private void reiniciarInscripciones()
        {
            InscripcionIdComboBox.Items.Clear();
            InscripcionIdComboBox.Items.Add("0");
            InscripcionIdComboBox.SelectedIndex = 0;
        }

        private bool validar()
        {//En los campos numericos no se verificar los punto ya que no utilizar valores decimales
            bool paso = true;

            int inscripcionId;

            //InscripcionId
            int.TryParse(InscripcionIdComboBox.SelectedValue.ToString(), out inscripcionId);
                
            //PersonaId
            if (string.IsNullOrWhiteSpace(PersonaIdTextBox.Text))
                paso = false;
            else
            {
                for (int i = 0; i < PersonaIdTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(PersonaIdTextBox.Text[i]) || Convert.ToInt32(PersonaIdTextBox.Text[i]) < 0)
                        paso = false;
                }
            }

            //Monto
            if(inscripcionId > 0)
            {
                if (string.IsNullOrWhiteSpace(MontoTextBox.Text))
                    paso = false;
                else
                {
                    for (int i = 0; i < MontoTextBox.Text.Length; i++)
                    {
                        if (!Char.IsDigit(MontoTextBox.Text[i]) || Convert.ToInt32(MontoTextBox.Text[i]) < 0)
                            paso = false;
                    }
                }
            }

            if (paso == false)
                MessageBox.Show("Datos invalidos");

            return paso;
        }

        private void InscripcionIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InscripcionIdComboBox.SelectedIndex != 0)
            {
                if(InscripcionIdComboBox.Items.Count!= 0)
                {
                    Inscripciones inscripcion = InscripcionesBLL.Buscar(Convert.ToInt32(InscripcionIdComboBox.SelectedValue.ToString()));
                    llenaCampoInscripcion(inscripcion);
                    MontoTextBox.IsEnabled = true;
                }
            }
            else
            {
                MontoTextBox.IsEnabled = false;
                MontoTextBox.Text = "0";
                BalanceTextBox.Text = "0";
            }
        }
    }
}
