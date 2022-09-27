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
    /// Логика взаимодействия для SpecDeletePage.xaml
    /// </summary>
    public partial class SpecDeletePage : Page
    {
        private NpgsqlConnection connection;
        public ObservableCollection<Specialty> Specialitys { get; set; }
        public Specialty NewSpeciality { get; set; }
        
        public SpecDeletePage()
        {
            InitializeComponent();
            Connect("10.14.206.27", "5432", "Denis", "1234", "denis200");

            Specialitys = new ObservableCollection<Specialty>();

            Binding binding = new Binding();
            binding.Source = Specialitys;

            SpecList2.SetBinding(ListBox.ItemsSourceProperty, binding);

            LoadSpec();

            DataContext = this;
        }


        private void DelSpecDelete(object sender, RoutedEventArgs e)
        {
            Specialitys.Add(NewSpeciality);


            int CodeSpec = Convert.ToInt32(textDeleteCode.Text.Trim());

            
            if (CodeSpec == 0) return;
            


            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE speciality SET code = @a, namespec = @b, qualification = @c";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Integer, CodeSpec);
          
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
                    Specialitys.Add(new Specialty(result.GetInt32(0), result.GetString(1), result.GetString(2)));
                }
            }
            result.Close();

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
