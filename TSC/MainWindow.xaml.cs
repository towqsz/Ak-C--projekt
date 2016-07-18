using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace TSC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Note> NotesList;
        private ObservableCollection<Person> PersonsList;
        private SaveTool saves = new SaveTool();

        public MainWindow()
        {
            InitializeComponent();
            NotesList = new ObservableCollection<Note>();
            PersonsList = new ObservableCollection<Person>();
            try
            {
                readNote();
                readPerson();

            } catch
            {
                createDirectory();
            }
        }

       
        private enum State {MAIN, ADD};
        private enum NoteType {Notatka, Osoba, Data };
        NoteType noteType;
        State state=State.MAIN;
        private string[] Opisy = {
            "Notatka - w tym miejscu możesz dodać lub odczytać dowolną treść, którą chcesz zachować.",
            "Osoba - ten typ notatki przystosowany jest do zapisu informacji o osobach tj. nazwisk, numerów telefonów, maili itp."};
        
        private void confirm(bool i)
        {
            ConfirmationWindow ConfWindow = new ConfirmationWindow();
            if (i==true)
            {
               
                ConfWindow.setConfirmation(0);
                
            } else
                ConfWindow.setConfirmation(-1);

            ConfWindow.Show();
        }

        private void alreadyExists()
        {
            ConfirmationWindow ConfWindow = new ConfirmationWindow();            
            ConfWindow.setConfirmation(1);
            ConfWindow.Show();
        }

        private void createDirectory()
        {
            string pathFolder = @"saves";
            string noteFile = saves.pathNote;
            string personFile = saves.pathPerson;
            System.IO.Directory.CreateDirectory(pathFolder);

            if (!System.IO.File.Exists(noteFile))
            {
                System.IO.FileStream fs = System.IO.File.Create(noteFile);
               
            }

            if (!System.IO.File.Exists(personFile))
            {
                System.IO.FileStream fs = System.IO.File.Create(personFile);

            }
        }

        private void readPerson()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer reader =
                       new System.Xml.Serialization.XmlSerializer(typeof(ObservableCollection<Person>));
                System.IO.StreamReader file = new System.IO.StreamReader(saves.pathPerson);
                PersonsList = (ObservableCollection<Person>)reader.Deserialize(file);
            }
            catch { }
        }

        private void SetChoice(int index)
        {
            if (index == 0)
                noteType = NoteType.Notatka;
            else if (index == 1)
                noteType = NoteType.Osoba;
            else
                noteType = NoteType.Data;

        }

        private void addNote(NoteType type)
        {
            Person person=new Person();
            Note note = new Note();
            if (type == NoteType.Notatka)
            {

                note.Title = PhoneText.Text;
                note.NotesText = textBox.Text;
                if (!note.compare(NotesList))
                {
                    NotesList.Add(note);
                    confirm(true);
                }
                else
                    alreadyExists();
            } 
            
            else if (type == NoteType.Osoba)
            {
                person.FirstName = FirstNameText.Text;
                person.LastName = LastNameText.Text;
                try
                {
                    person.Phone = int.Parse(this.PhoneText.Text);
                }
                catch
                {
                    confirm(false);
                    return;
                }
                
                person.NotesText = textBox.Text;
                if (!person.compare(PersonsList))
                {
                    PersonsList.Add(person);
                    confirm(true);
                }
                else
                    alreadyExists();
                
                
                
            }
        }

        private void readNote()
        {

            try
            {
                System.Xml.Serialization.XmlSerializer reader =
                        new System.Xml.Serialization.XmlSerializer(typeof(ObservableCollection<Note>));
                System.IO.StreamReader file = new System.IO.StreamReader(saves.pathNote);
                NotesList = (ObservableCollection<Note>)reader.Deserialize(file);
            }
            catch { }

        }

        


        private void ClrBoxes()
        {
            comboBox.SelectedIndex = -1;
            textBox.Text = "";
            FirstNameText.Text = "";
            LastNameText.Text = "";
            PhoneText.Text = "";
        }


        private void SetStateAdd()
        {
            state = State.ADD;
            SetChoice(comboBox.SelectedIndex);
            ClrBoxes();
            opt.Content = "Wstecz";
            textBox.IsEnabled = true;
            comboBox.Visibility = Visibility.Hidden;
            PhoneText.Visibility = Visibility.Visible;
            PhoneLabel.Visibility = Visibility.Visible;

            if (noteType == NoteType.Notatka)
            {
                PhoneLabel.Content = "Tytuł:";
                
            }
            else
            {
                PhoneLabel.Content = "Telefon:";
                FirstNameText.Visibility = Visibility.Visible;
                LastNameText.Visibility = Visibility.Visible;
                FirstNameLabel.Visibility = Visibility.Visible;
                LastNameLabel.Visibility = Visibility.Visible;
            }
        }


        private void SetStateMain()
        {
            state = State.MAIN;
            comboBox.Visibility = Visibility.Visible;
            textBox.IsEnabled = false;
            textBox.Text = "";
            opt.Content = "Zobacz";
            FirstNameText.Visibility = Visibility.Hidden;
            LastNameText.Visibility = Visibility.Hidden;
            PhoneText.Visibility = Visibility.Hidden;
            FirstNameLabel.Visibility = Visibility.Hidden;
            LastNameLabel.Visibility = Visibility.Hidden;
            PhoneLabel.Visibility = Visibility.Hidden;
            
            readNote();
            readPerson();
        }


        private void ChangeState(State st)
        {
            if (st == State.ADD)
            {
                SetStateAdd();

            }

            else if (st == State.MAIN)
            {
               SetStateMain(); 

            }

           
        }


        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    textBox.Text = Opisy[comboBox.SelectedIndex];
                    break;
                case 1:
                    textBox.Text = Opisy[comboBox.SelectedIndex];
                    break;
                

            }
                
        }


        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (state == State.MAIN && comboBox.SelectedIndex!=-1)
            {
                ChangeState(State.ADD); 

            }

            else if (state == State.ADD)
            {
                addNote(noteType);
                try
                {
                    this.saves.write(NotesList);
                    this.saves.write(PersonsList);
                }
                catch
                {
                    confirm(false);
                }
            }
                
        }

        private void opt_Click(object sender, RoutedEventArgs e)
        {
            if (state != State.MAIN)
            {

                ChangeState(State.MAIN);

            }
            else if (comboBox.SelectedIndex == 0)
            {
                
                ViewWindow win = new ViewWindow(NotesList);
                
                win.Show();
            }
            else 
            {

                ViewWindow win = new ViewWindow(PersonsList);

                win.Show();
            }

        }

        
    }
}
