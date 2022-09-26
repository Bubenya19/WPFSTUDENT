using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Main_Page.xaml
    /// </summary>
    public partial class Main_Page : Page
    {
        public Main_Page()
        {
            InitializeComponent();
        }

        private void FirstPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.main_page);
        }

        private void StCr(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.stud_create);
        }

        private void StDel(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.stud_edit);
        }


        private void StEd(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.stud_delete);
        }

       

    }
}
