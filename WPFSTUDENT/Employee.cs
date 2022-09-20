using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPFSTUDENT
{
    public class Employee : INotifyPropertyChanged
    {
        public static int EmployeeID = 0;
        private string code;
        private string namespec;
        private string qualification;

        private string number;
        private string course;
        private int speciality;

        public Employee(string number, string course, int speciality)
        {

            Speciality = speciality;


            ID = ++EmployeeID;

            Number1 = number;
            Course = course;
        }
        

        public string Number1
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Number1"));
            }

        }
        public int Speciality
        {
            get => speciality;
            set
            {
                speciality = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Speciality"));
            }

        }

        public string Course
        {
            get => course;
            set
            {
                course = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Course"));
            }

        }



        public int ID { get; set; }

        public string Code
        {
            get => code;
            set
            {
                code = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Code"));
            }
        }

        public string Namespec
        {
            get => namespec;
            set
            {
                namespec = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Namespec"));
            }
        }

        public string Qualification
        {
            get => qualification;
            set
            {
                qualification = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Qualification"));
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
