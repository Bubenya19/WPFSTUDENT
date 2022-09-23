using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace WPFSTUDENT
{
    public class Specialty
    {
        public static int SpecialityID = 0;
        private string code;
        private string namespec;
        private string qualification; 



        public Specialty(string code, string namespec, string qualification)
        {
            Codes = code;

            Namespec = namespec;
            Qualification = qualification;
        }

        public string Codes
        {
            get => code;
            set
            {
                code = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Codes"));
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
