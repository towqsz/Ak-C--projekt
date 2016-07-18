using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TSC
{
    public class Note : ICompareNote<Note>
    {
        public string Title { get; set; }
        public string NotesText { get; set; }

        public bool compare(ObservableCollection<Note> NotesList)
        {
            foreach (Note note in NotesList)
                if (this.Title == note.Title)
                    return true;       
                
            return false;
        }

    }
}
