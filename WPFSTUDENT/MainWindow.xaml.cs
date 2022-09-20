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
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<string> Codes { get; set; }
        
        public ObservableCollection<string> Numbers { get; set; }
        public Employee NewEmployee { get; set; }

        


        public MainWindow()
        {
            InitializeComponent();

            Employees = new ObservableCollection<Employee>();
            Codes = new ObservableCollection<string>();

            Connect("localhost", "5432", "Denis", "1234", "students");

            Binding binding = new Binding();
            binding.Source = Codes;
            binding.Source = Numbers;


            DataContext = this;
        }
        private void Connect(string host, string port, string user, string pass, string dbname)
        {
            string cs = string.Format("Host=localhost;Username=postgres;Password=1234;Database=students");
            NpgsqlConnection nc = new NpgsqlConnection(cs);
            connection = new NpgsqlConnection(cs);
            connection.Open();





        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EnableControl(true);


            Employees.Add(NewEmployee);
            



            string CodeSpec = textCodeSpec.Text.Trim();
            if (CodeSpec.Length == 0) return;
            string NameSpec = textNameSpec.Text.Trim();
            if (NameSpec.Length == 0) return;
            string qualification = textQualSpec.Text.Trim();
            if (qualification.Length == 0) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO speciality(code, namespec, qualification) VALUES(@a, @b, @c)";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, CodeSpec);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, NameSpec);
            command.Parameters.AddWithValue("@c", NpgsqlDbType.Varchar, qualification);
            
            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Специальность успешно добавлена!");
                LoadSpec();
            }

        }
        private void LoadSpec()
        {
            Employees.Clear();

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT code(speciality), namespec(speciality), speciality(group)";
            var result = command.ExecuteReader();
            if (result == null) return;
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Employees.Add(new Employee(result.GetString(0), result.GetString(1), result.GetInt32(2)));
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
        

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EnableControl(true);

            Employees.Add(NewEmployee);


            string NumberGroup = textNumberGroup.Text.Trim();
            if (NumberGroup.Length == 0) return;
            string CourseGroup = textCourse.Text.Trim();
            if (CourseGroup.Length == 0) return;
            
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO group(number, course) VALUES(@a, @b)";
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
            Employees.Clear();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT number FROM group ORDER BY number";
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Numbers.Add(result.GetString(0));
                }
            }
            result.Close();
        }
       

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            EnableControl(true);

            

            Employees.Add(NewEmployee);

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