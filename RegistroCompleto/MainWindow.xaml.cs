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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RegistroCompleto.UI.RegistrarPersona;
using RegistroCompleto.UI.InscribirPersona;
using RegistroCompleto.UI.ConsultasPersonas;
using RegistroCompleto.UI.ConsultasInscripciones;

namespace RegistroCompleto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegistrarPersonaButton_Click(object sender, RoutedEventArgs e)
        {
            rPersona r = new rPersona();
            r.Show();
        }

        private void InscribirPersonaButton_Click(object sender, RoutedEventArgs e)
        {
            iPersona i = new iPersona();
            i.Show();
        }

        private void ConsultarPersonasButton_Click(object sender, RoutedEventArgs e)
        {
            cPersona cp = new cPersona();
            cp.Show();
        }

        private void ConsultarInscripcionesButton_Click(object sender, RoutedEventArgs e)
        {
            cInscripciones ci = new cInscripciones();
            ci.Show();
        }
    }
}
