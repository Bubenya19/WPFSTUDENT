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
    
    public partial class GroupCreatePage : Page
    {

       




        public ObservableCollection<Specialty> Specialitys { get; set; } = new ObservableCollection<Specialty>();
        public Specialty NewSpeciality { get; set; }
        private NpgsqlConnection connection;
        public ObservableCollection<Group> Groups { get; set; } = new ObservableCollection<Group>();
        public Group NewGroup { get; set; }

        public GroupCreatePage()
        {
            InitializeComponent();

            Connect("localhost", "5432", "Denis", "1234", "students");


           

            Binding binding2 = new Binding();
            binding2.Source = Specialitys;
            cmbGSpecId.SetBinding(ComboBox.ItemsSourceProperty, binding2);

            LoadSpec();

            LoadGroup();
            
            DataContext = this;
        }


        private void LoadSpec()
        {
            Specialitys.Clear();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "Select \"code\",\"namespec\", qualification FROM \"speciality\" ORDER by \"code\"";
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Specialitys.Add(new Specialty(result.GetInt32(0), result.GetString(1), result.GetString(2)));
                }
            }
            result.Close();

        }


        private void CreateGroup(object sender, RoutedEventArgs e)
        {



            Groups.Add(NewGroup);

            int SpecId = (cmbGSpecId.SelectedItem as Specialty).code;

            if (cmbGSpecId.SelectedItem == null) return;


            int Number = Convert.ToInt32(textNumberGroup.Text.Trim());
            string Course = textCourseGroup.Text.Trim();
            if (Number == 0 && Course.Length == 0) return;
            


            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO \"group\"(number, course, spec) VALUES(@a, @b, @c)";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Integer, Number);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, Course);
            command.Parameters.AddWithValue("@c", NpgsqlDbType.Integer, SpecId);

            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Группа успешно добавлена!");
                LoadGroup();
            }
            cmbGSpecId.SelectedItem = null;
        }
        private void LoadGroup()
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
