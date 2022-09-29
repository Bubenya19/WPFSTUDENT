using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPFSTUDENT
{
    public class Group
    {
        
        private int number;
        private string course;


        public Group(int number, string course)
        {
            Number1 = number;

            Course = course;

        }

        public int Number1
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Number1"));
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