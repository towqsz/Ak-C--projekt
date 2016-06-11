using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TSC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
        public ObservableCollection<Person> listaOsob;
        public ViewWindow(ObservableCollection<Note> NotesList)
        {
            InitializeComponent();

            NotesView.Visibility = Visibility.Visible;
            PersonsView.Visibility = Visibility.Hidden;
            this.NotesView.ItemsSource = NotesList;
        }

        public ViewWindow(ObservableCollection<Person> PersonsList)
        {
            InitializeComponent();

            NotesView.Visibility = Visibility.Hidden;
            PersonsView.Visibility = Visibility.Visible;
            this.PersonsView.ItemsSource = PersonsList;
        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
