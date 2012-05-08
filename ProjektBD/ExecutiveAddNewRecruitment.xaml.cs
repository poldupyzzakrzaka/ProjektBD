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
    /// Interaction logic for ExecutiveAddNewRecruitment.xaml
    /// </summary>
    public partial class ExecutiveAddNewRecruitment : UserControl
    {
        public ExecutiveAddNewRecruitment()
        {
            InitializeComponent();

            string MyConString = "SERVER=localhost;" +
                "DATABASE=projektBD;" +
                "UID=root;" +
                "PASSWORD=;";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "select * from specialization_type";
            connection.Open();
            Reader = command.ExecuteReader();
            int licznik = 0;
            while (Reader.Read())
            {
                if (licznik % 3 == 0)
                {
                    GridLengthConverter myGridLengthConverter = new GridLengthConverter();
                    GridLength gl1 = (GridLength)myGridLengthConverter.ConvertFromString("30*");
                    Grid1.RowDefinitions.Add(new RowDefinition() { Height = gl1 });
                }

                int id = Reader.GetInt32(0);
                string name = Reader.GetString(1);
                CheckBox nowy = new CheckBox() { Content = name, Name = name + id.ToString() };
                Grid.SetColumn(nowy, licznik%3);
                Grid.SetRow(nowy, licznik/3);
                licznik++;
                Grid1.Children.Add(nowy);
            }
            connection.Close();
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //MySqlConnection connection = new MySqlConnection(MyConString);
            //MySqlCommand command = connection.CreateCommand();
            //MySqlDataw
            //command.CommandText = "select * from specialization_type";
            //connection.Open();

        }
    }
}
