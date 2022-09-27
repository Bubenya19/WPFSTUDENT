using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPFSTUDENT
{
    public class Student : INotifyPropertyChanged
    {
        public static int StudentID = 0;
        private string surname;
        private string name;


        public Student(string number, string course)
        {
            Surname = surname;

            Name = name;

        }

        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Surname"));
            }

        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }




    }
}