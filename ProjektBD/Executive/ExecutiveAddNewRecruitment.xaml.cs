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
using ProjektBD.Database;

namespace ProjektBD.Executive
{
    /// <summary>
    /// Interaction logic for ExecutiveAddNewRecruitment.xaml
    /// </summary>
    public partial class ExecutiveAddNewRecruitment : UserControl
    {
        private Dictionary<int, string> dict_departments;

        public ExecutiveAddNewRecruitment()
        {
            InitializeComponent();
            dict_departments = new Dictionary<int, string>();
            GetDepartmentList();


            MySqlCommand command = DBConnection.Instance.Conn.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "select * from specialization_type";
            try
            {
                DBConnection.Instance.Conn.Open();
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
                    CheckBox nowy = new CheckBox() { Content = name, Name = "CheckBoxSpec" + id.ToString() };
                    Grid.SetColumn(nowy, licznik % 3);
                    Grid.SetRow(nowy, licznik / 3);
                    licznik++;
                    Grid1.Children.Add(nowy);
                }
                DBConnection.Instance.Conn.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

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
            ComboBoxDepartments.SelectedIndex = 0;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Query = "INSERT INTO recruitments(id,name,decription,department,needed_ppl) VALUES (null,'" + textBoxName.Text + "','" + textBoxDescription.Text + "','" + ComboBoxDepartments.SelectedIndex+1 + "','" + IntegerUpDownHowManyNeeded.Value + "');";
                MySqlCommand addUser = new MySqlCommand(Query, DBConnection.Instance.Conn);
                DBConnection.Instance.Conn.Open();
                addUser.ExecuteNonQuery();
                DBConnection.Instance.Conn.Close();
            }
            catch (MySqlException ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
    }
}
