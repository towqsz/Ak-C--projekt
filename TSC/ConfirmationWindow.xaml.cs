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

namespace TSC
{
    /// <summary>
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        public ConfirmationWindow()
        {
            InitializeComponent();
        }

        public void setConfirmation(int i)
        {
            if (i == 0)
            {
                label.Content = labels[0];
                label.Background = Brushes.Green;
            }
            else
            {
                label.Content = labels[1];
                label.Background = Brushes.Red;
            }
            
        }

        private string[] labels = {
            "Dodawanie notatki zakończone sukcesem!",
            "Niesty wystąpił błąd! Sprawdź dane i spróbuj ponownie.",
             };

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
