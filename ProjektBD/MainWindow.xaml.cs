using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;


//jakis testowy koment
namespace ProjektBD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int userLevel = -1; // -1 - nie zalogowany

        public MainWindow()
        {
            InitializeComponent();
            RefreshLeftButtonMenu();
        }

        public void RefreshLeftButtonMenu()
        {
            //tu ustawiamy wszystkie przyciski dla konkretnego zalogowanego typa
            switch (userLevel)
            {
                //nie zalogowany
                case -1: break;
                //gosc - tak sobie, raczej nie uzywany
                case 0: break;
                //asystentka
                case 1: break;
                //kierownik
                case 2: break;
                //specjalista
                case 3: break;
                //administrator
                case 4: break;
                //deweloper - posiada dostep do wszystkiego
                case 5: break;
            }
        }

        private void LoginControl1_LoggedInEvent(object sender, LoggedInEventArgs fe)
        {
            labelNotLoggedIn.Visibility = Visibility.Collapsed;
            labelLoggedAs.Visibility = Visibility.Visible;
            labelUserNameSurname.Visibility = Visibility.Visible;
            labelLogOut.Visibility = Visibility.Visible;
            labelUserLevel.Visibility = Visibility.Visible;

            labelUserNameSurname.Content = fe.userName + " " + fe.userSurname;

            switch (fe.userLevel)
            {
                case 0: labelUserLevel.Content = "Gość"; break;
                case 1: labelUserLevel.Content = "Asysten(ka)"; break;
                case 2: labelUserLevel.Content = "Kierownik"; break;
                case 3: labelUserLevel.Content = "Specjalista"; break;
                case 4: labelUserLevel.Content = "Administrator"; break;
                case 5: labelUserLevel.Content = "Deweloper"; break; 
            }
        }
    }
}
