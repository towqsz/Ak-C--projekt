using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace TSC
{
    class SaveTool
    {
        public string pathNote  { get;  }
        public string pathPerson { get;  }

        public SaveTool()
        {
            pathNote = @"/saves/notes.xml";
            pathPerson = @"/saves/people.xml";
        }

        public void write(ObservableCollection<Note> NotesList)
        {
           System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(ObservableCollection<Note>));

            System.IO.StreamWriter file = new System.IO.StreamWriter(pathNote);
            writer.Serialize(file, NotesList);
            file.Close();
        }

        public void write(ObservableCollection<Person> PersonsList)
        {

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(ObservableCollection<Person>));

            System.IO.StreamWriter file = new System.IO.StreamWriter(pathPerson);
            writer.Serialize(file, PersonsList);
            file.Close();
        }
    }
}
