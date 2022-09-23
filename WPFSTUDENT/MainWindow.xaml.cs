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
            

            Connect("10.14.206.27", "5432", "Denis", "1234", "students");

            Binding binding = new Binding();
            binding.Source = Specialitys;
           
            Binding binding1 = new Binding();
            binding1.Source = Groups;
            
            SpecList.SetBinding(ListBox.ItemsSourceProperty, binding);
            GroupList.SetBinding(ListBox.ItemsSourceProperty, binding1); 
            LoadSpec();
            
            DataContext = this;
        }
      
        private void Connect(string host, string port, string user, string pass, string dbname)
        {
            string cs = string.Format("Host=10.14.206.27;Username=student;Password=1234;Database=denis200");
            NpgsqlConnection nc = new NpgsqlConnection(cs);
            connection = new NpgsqlConnection(cs);
            connection.Open();

        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EnableControl(true);


            Specialitys.Add(NewSpeciality);
            



            string CodeSpec = textCodeSpec.Text.Trim();
            if (CodeSpec.Length == 0) return;
            string NameSpec = textNameSpec.Text.Trim();
            if (NameSpec.Length == 0) return;
            string qualifiation = textQualSpec.Text.Trim();
            if (qualifiation.Length == 0) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO speciality(code, namespec, qualifiation) VALUES(@a, @b, @c)";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, CodeSpec);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, NameSpec);
            command.Parameters.AddWithValue("@c", NpgsqlDbType.Varchar, qualifiation);
            
            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Специальность успешно добавлена!");
                LoadSpec();
            }

        }
        private void LoadSpec()
        {
            Specialitys.Clear();

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT code, namespec, qualifiation FROM speciality";
            var result = command.ExecuteReader();
            if (result == null) return;
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Specialitys.Add(new Specialty(result.GetString(0), result.GetString(1), result.GetString(2)));
                }
            }
            result.Close();
        }

       


        private void EnableControl(bool isEnable)
        {
            foreach (UIElement element in NewEmployeePanel.Children)
            {
                if (element.GetType() == typeof(TextBox))
                {
                    element.IsEnabled = isEnable;
                }
            }
        }

        private ListBox GetSpecList()
        {
            return SpecList;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            EnableControl(true);
            Groups.Add(NewGroups);


            string NumberGroup = textNumberGroup.Text.Trim();
            if (NumberGroup.Length == 0) return;
           
            string CourseGroup = textCourse.Text.Trim();
            if (CourseGroup.Length == 0) return;


            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO group(number, course), speciality(idspec) VALUES(@a, @b)";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, NumberGroup);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, CourseGroup);
            
            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Группа успешно добавлена!");
                LoadGroup();
            }
        }

        private void LoadGroup()
        {
            Groups.Clear();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT number, course FROM group ORDER BY number";
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Groups.Add(new Group(result.GetString(0), result.GetString(1)));
                }
            }
            result.Close();
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            EnableControl(true);



            Students.Add(NewStudent);

            string SurName = Surname.Text.Trim();
            if (SurName.Length == 0) return;
            string StName = StudName.Text.Trim();
            if (StName.Length == 0) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO \"Position\"(\"Position\") VALUES(@a, @b)";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, SurName);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, StName);

            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Студент успешно добавлен!");

            }
        }



    }
}