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
        public static int GroupID = 0;
        private string number;
        private string course;
       

        public Group(string number, string course)
        {
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
