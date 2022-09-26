﻿using System;
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
    
    public partial class SpecEditPage : Page
    {
        private NpgsqlConnection connection;
        public ObservableCollection<Specialty> Specialitys { get; set; }
        public Specialty NewSpeciality { get; set; }
        public SpecEditPage()
        {
            InitializeComponent();
            Connect("localhost", "5432", "Denis", "1234", "students");

            Specialitys = new ObservableCollection<Specialty>();

            Binding binding = new Binding();
            binding.Source = Specialitys;

            SpecList1.SetBinding(ListBox.ItemsSourceProperty, binding);

            LoadSpec();

            DataContext = this;
        }


        private void UpdateSpecEdit(object sender, RoutedEventArgs e)
        {
            Specialitys.Add(NewSpeciality);


            string CodeSpec = textEditCode.Text.Trim();
            if (CodeSpec.Length == 0) return;

            string NameSpec = textEditName.Text.Trim();
            if (NameSpec.Length == 0) return;
            string qualification = textEditQual.Text.Trim();
            if (qualification.Length == 0) return;


            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE speciality(code, namespec, qualification) VALUES(@a, @b, @c)";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, CodeSpec);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, NameSpec);
            command.Parameters.AddWithValue("@c", NpgsqlDbType.Varchar, qualification);

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
                    Specialitys.Add(new Specialty(result.GetString(0), result.GetString(1), result.GetString(2)));
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
