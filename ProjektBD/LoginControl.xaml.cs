﻿using System;
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

namespace ProjektBD
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>

    public partial class LoginControl : UserControl
    {
        int userId;
        int userLevel;
        String userName;
        String userSurname;

        public delegate void LoggedInEventHandler(object sender, LoggedInEventArgs fe);
        public event LoggedInEventHandler LoggedInEvent;

        public LoginControl()
        {
            InitializeComponent();
        }

        public void Zaloguj(String name, String password)
        {
            if (name.Length > 0 && password.Length > 0)
            {
                string MyConString = "SERVER=localhost;" +
                "DATABASE=projektBD;" +
                "UID=root;" +
                "PASSWORD=;";
                MySqlConnection connection = new MySqlConnection(MyConString);
                MySqlCommand command = connection.CreateCommand();
                MySqlDataReader Reader;
                command.CommandText = "select p.level, u.uid, u.name, u.surname from users u, privilages p where p.uid = u.uid and u.login = \"" + textBoxLogin.Text + "\" and u.password = \"" + textBoxPassword.Password + "\"";
                connection.Open();
                Reader = command.ExecuteReader();
                if(Reader.Read())
                {
                    userId = Reader.GetInt32(0);
                    userLevel = Reader.GetInt32(1);
                    userName = Reader.GetString(2);
                    userSurname = Reader.GetString(3);
                }
                connection.Close();

                Visibility = Visibility.Collapsed;

                LoggedInEventArgs LoggedInArgs = new LoggedInEventArgs(userId, userLevel, userName, userSurname);
                LoggedInEvent(this, LoggedInArgs);
            }
            else
            {
                //nie podane haslo/nick
            }
        }

        private void buttonLoginConfirm_Click(object sender, RoutedEventArgs e)
        {
            Zaloguj(textBoxLogin.Text, textBoxPassword.Password);
        }
    }

    public class LoggedInEventArgs : EventArgs
    {
        public LoggedInEventArgs(int Id, int Level, String Name, String Surname)
        {
            userId = Id;
            userLevel = Level;
            userName = Name;
            userSurname = Surname;
        }
        public int userId;
        public int userLevel;
        public String userName;
        public String userSurname;
    }
}
