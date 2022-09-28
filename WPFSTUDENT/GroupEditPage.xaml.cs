using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using NpgsqlTypes;

namespace WPFSTUDENT
{
    /// <summary>
    /// Логика взаимодействия для GroupEditPage.xaml
    /// </summary>
    public partial class GroupEditPage : Page
    {
        private NpgsqlConnection connection;
        public ObservableCollection<Group> Groups { get; set; }
        public Group NewGroup { get; set; }
        public GroupEditPage()
        {
            InitializeComponent();


            Groups = new ObservableCollection<Group>();

            Binding binding = new Binding();
            binding.Source = Groups;

            StudList2.SetBinding(ListBox.ItemsSourceProperty, binding);

            LoadGroup1();

            DataContext = this;


        }

        private void UpdateSpecEdit(object sender, RoutedEventArgs e)
        {
            Groups.Add(NewGroup);


            int NumberGroup = Convert.ToInt32(NumberGr.Text.Trim());
            int SpecGroup = Convert.ToInt32(SpecGr.Text.Trim());  
            

            string CourseGroup = CourseGr.Text.Trim();
            if (SpecGroup == 0 && NumberGroup == 0 && CourseGroup.Length == 0) return;
            


            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE \"group\" SET number = @a, course = @b, spec = @c";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Integer, NumberGroup);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, CourseGroup);
            command.Parameters.AddWithValue("@c", NpgsqlDbType.Integer, SpecGroup);

            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Специальность успешно отредактирована!");
                LoadSpec();
            }
        }

        private void LoadSpec()
        {



            NpgsqlCommand command = new NpgsqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT code, namespec, qualification FROM speciality ORDER BY code";
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

        private void DeleteGroup(object sender, RoutedEventArgs e)
        {
            Groups.Add(NewGroup);


            int NumberGroup = Convert.ToInt32(NumberGr.Text.Trim());



            if (NumberGroup == 0) return;



            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM \"group\" WHERE number = @a ";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Integer, NumberGroup);

            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Специальность удалена.");
                LoadGroup1();
            }
        }

        private void LoadGroup1()
        {



            NpgsqlCommand command = new NpgsqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT code, namespec, qualification FROM speciality ORDER BY code";
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



    }
}
