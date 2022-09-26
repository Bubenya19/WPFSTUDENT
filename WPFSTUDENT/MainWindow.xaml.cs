using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using NpgsqlTypes;

namespace WPFSTUDENT
{

    public partial class MainWindow : Window
    {

        private NpgsqlConnection connection;
        
        public ObservableCollection<Specialty> Specialitys { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        public Student NewStudent { get; set; }
        public Specialty NewSpeciality {get; set;}
        public Group NewGroups { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            Specialitys = new ObservableCollection<Specialty>();
            Groups = new ObservableCollection<Group>();
            Students = new ObservableCollection<Student>();
            Connect("10.14.206.27", "5432", "Denis", "1234", "students");

            Binding binding = new Binding();
            binding.Source = Specialitys;
           
            Binding binding1 = new Binding();
            binding1.Source = Groups;
            
           
            DataContext = this;

            MainFrame.Navigate(PageControl.stud_create);

        }
      
        private void Connect(string host, string port, string user, string pass, string dbname)
        {
            string cs = string.Format("Host=10.14.206.27;Username=student;Password=1234;Database=denis200");
            NpgsqlConnection nc = new NpgsqlConnection(cs);
            connection = new NpgsqlConnection(cs);
            connection.Open();

        }





    }
}