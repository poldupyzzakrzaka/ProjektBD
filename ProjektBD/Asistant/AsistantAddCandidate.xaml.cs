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
using ProjektBD.Database;
using MySql.Data.MySqlClient;

namespace ProjektBD.Asistant
{
    /// <summary>
    /// Interaction logic for AsistantAddCandidate.xaml
    /// </summary>
    public partial class AsistantAddCandidate : UserControl
    {
        private Dictionary<int, string> dict_departments;

        public AsistantAddCandidate()
        {
            InitializeComponent();
            dict_departments = new Dictionary<int, string>(); 
            GetDepartmentList();
        }

        private string Name { get { return textBoxName.Text; } }
        private string Surname { get { return textBoxSurname.Text; } }
        private string Pesel { get { return textBoxPesel.Text; } }
        private string City { get { return textBoxCity.Text; } }
        private string Sex { get { return textBoxSex.Text; } }
        private string Education { get { return textBoxEducation.Text; } }
        private string Skills { get { return textBoxSkills.Text; } }
        private string Experience { get { return textBoxExperience.Text; } }
        private string Courses { get { return textBoxCourses.Text; } }

        private void GetDepartmentList()
        {
            MySqlCommand command = DBConnection.Instance.Conn.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "SELECT id, department FROM departments";
            DBConnection.Instance.Conn.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                int key = Reader.GetInt32(0);
                string value = Reader.GetString(1);
                dict_departments.Add(key, value);
            }
            ComboBoxDepartments.ItemsSource = dict_departments;
            ComboBoxDepartments.SelectedValuePath = "Key";
            ComboBoxDepartments.DisplayMemberPath = "Value";
            DBConnection.Instance.Conn.Close();
        }

        private string CreateInsertCommand()
        {
            int departmentID =(int) ComboBoxDepartments.SelectedValue;
            return "INSERT INTO candidates(did, name, surname, city, pesel, sex, education, skills, experience, courses) VALUES" +
                   "(" +departmentID+ ",'" +Name+ "','" +Surname+ "','" +City+ "','" +Pesel+ "','" +Sex+ "','" +Education+ "','" +Skills+ "','" +Experience+ "','" +Courses+ "');";
        }

        private void InsertData()
        {
            try
            {
                string Query = CreateInsertCommand();
                MySqlCommand addUser = new MySqlCommand(Query, DBConnection.Instance.Conn);
                DBConnection.Instance.Conn.Open();
                addUser.ExecuteNonQuery();
                DBConnection.Instance.Conn.Close();
                ResultInfo("Dodano pomyslnie");
            }
            catch (Exception e)
            {
                ResultInfo("Wystapil blad");
            }
        }

        private void ResultInfo(string info)
        {
            if (stackPanelInfo.Children.Count > 0)
                stackPanelInfo.Children.Clear();
            Label labOK = new Label();
            labOK.HorizontalAlignment = HorizontalAlignment.Center;
            labOK.VerticalAlignment = VerticalAlignment.Top;
            labOK.VerticalContentAlignment = VerticalAlignment.Top;
            labOK.HorizontalContentAlignment = HorizontalAlignment.Center;
            labOK.Content = info;
            labOK.Margin = new Thickness(0, 5, 0, 0);
            labOK.Width = 400;
            labOK.Height = 30;
            labOK.Foreground = new SolidColorBrush(Colors.Red);
            stackPanelInfo.Children.Add(labOK);
        }

        private void buttonAddCandidate_Click(object sender, RoutedEventArgs e)
        {
            InsertData();
        }

    }

}
