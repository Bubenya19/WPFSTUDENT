using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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

namespace WPFSTUDENT
{
    /// <summary>
    /// Логика взаимодействия для StudentCreatePage.xaml
    /// </summary>
    public partial class StudentCreatePage : Page
    {
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
        public Group NewGroup { get; set; }

        
        private NpgsqlConnection connection;
        
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
        public Student NewStudent { get; set; }

        public StudentCreatePage()
        {
            InitializeComponent();

            Connect("localhost", "5432", "Denis", "1234", "students");




            Binding binding2 = new Binding();
            binding2.Source = Students;
            cmbGStudId.SetBinding(ComboBox.ItemsSourceProperty, binding2);
            
            LoadGroup();
            LoadStud(); 

            DataContext = this;
        }

        private void LoadGroup()
        {
            Groups.Clear();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "Select \"number\",\"course\", spec FROM \"group\" ORDER by \"number\"";
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Groups.Add(new Group(result.GetInt32(0), result.GetString(1), result.GetString(2)));
                }
            }
            result.Close();

        }

        private void CreateStudent(object sender, RoutedEventArgs e)
        {



            Students.Add(NewStudent);

            int GroupStud = (cmbGStudId.SelectedItem as Group).Number1;

            if (cmbGStudId.SelectedItem == null) return;


            int Phone = Convert.ToInt32(textPhone.Text.Trim());
            string Surname = textSurname.Text.Trim();
            if (Phone == 0 && Surname.Length == 0) return;
            
            string Name = textName.Text.Trim();
            if (Name.Length == 0) return;

            string Patronymic = textPatronymic.Text.Trim();
            if (Patronymic.Length == 0) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO \"student\"(phone, surname,  name, patronymic) VALUES(@a, @b, @c, @d, @e)";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Integer, Phone);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, Surname);
            command.Parameters.AddWithValue("@c", NpgsqlDbType.Integer, GroupStud);
            command.Parameters.AddWithValue("@d", NpgsqlDbType.Varchar, Name);
            command.Parameters.AddWithValue("@e", NpgsqlDbType.Varchar, Patronymic);

            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Группа успешно добавлена!");
                LoadGroup();
            }
            cmbGStudId.SelectedItem = null;
        }

        private void LoadStud()
        {
            Groups.Clear();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT number, course, spec FROM \"group\" ORDER BY number";
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Groups.Add(new Group(result.GetInt32(0), result.GetString(1)));
                }
            }
            result.Close();

        }

        private void Connect(string host, string port, string user, string pass, string dbname)
        {
            string cs = string.Format("Host=localhost;Username=postgres;Password=1234;Database=students");
            NpgsqlConnection nc = new NpgsqlConnection(cs);
            connection = new NpgsqlConnection(cs);
            connection.Open();

        }
    }
}
