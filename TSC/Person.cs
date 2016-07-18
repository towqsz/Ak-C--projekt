using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TSC
{
    public class Person : Note, ICompareNote<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }

        public bool compare(ObservableCollection<Person> PersonList)
        {
            foreach (Person person in PersonList)
                if (this.FirstName == person.FirstName && this.LastName == person.LastName)
                    return true;

              
            return false;
        }

    }
}
