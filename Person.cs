using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aflevering
{
    public class Person : INotifyPropertyChanged
    {
        // OberservableCollection which contains the persons loaded in from the textfile
        private static ObservableCollection<Person> persons = new ObservableCollection<Person>();
        private int id;
        private String name;
        private int age;
        private int score;

        //data is the content from the selected textfile
        //data is splitted and parsed into the person constructor
        public Person(string data)
        {
            try
            {

                var L = data.Split(';');
                Id = int.Parse(L[0]);
                Name = L[1];
                Age = int.Parse(L[2]);
                Score = int.Parse(L[3]);
            }
            catch (Exception e) { };
        }

        public string Name
        {
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
            get { return name; }
        }

        public int Id
        {
            set
            {
                id = value;
                NotifyPropertyChanged(nameof(id));
            }
            get
            {
                return id;
            }
        }

        public int Age
        {
            set
            {
                age = value;
                NotifyPropertyChanged(nameof(Age));
            }
            get
            {
                return age;
            }
        }

        public int Score
        {
            set
            {
                score = value;
                NotifyPropertyChanged(nameof(Score));
            }
            get
            {
                return score;
            }
        }


        public override string ToString()
        {
            return Name +" " + age;      
        }

        //Load txt file
        public static List<Person> ReadCSVFile(string path)
        {
            List<Person> list = new List<Person>();
            using (var file = new StreamReader(path))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var p = new Person(line);
                    list.Add(p);
                    Console.WriteLine(p);
                }
            }
            return list;
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        //Notifies the property owner when the property is changed
        //Essential for working with binding
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            
        }

    }
}

