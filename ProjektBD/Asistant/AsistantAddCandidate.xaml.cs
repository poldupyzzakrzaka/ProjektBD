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
        private AsistantCandidateData canData;
        public AsistantAddCandidate()
        {
            InitializeComponent();
            dict_departments = new Dictionary<int, string>(); 
            canData = new AsistantCandidateData();
            GetDepartmentList();
            gridCanData.Children.Add(canData);
        }

        private void GetDepartmentList()
        {
            MySqlCommand command = DBConnection.Instance.Conn.CreateCommand();
            MySqlDataReader Reader;
            //pobiera id i nazwy dzialow do ktorych aktualnie trwa rekrutacja
            command.CommandText = "SELECT r.id, d.department FROM departments As d, recruitments AS r WHERE d.id= r.department";
            DBConnection.Instance.Conn.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                int key = Reader.GetInt32(0);
                string value = Reader.GetString(1);
                dict_departments.Add(key, value);
            }
            //wypelnia comboboxa nazwami dzialow
            canData.GetComboBoxDepart().ItemsSource = dict_departments;
            canData.GetComboBoxDepart().SelectedValuePath = "Key";
            canData.GetComboBoxDepart().DisplayMemberPath = "Value";
            DBConnection.Instance.Conn.Close();
        }

        private string CreateInsertCommand()
        {
            int recruitmentID = (int)canData.GetComboBoxDepart().SelectedValue;
            return "INSERT INTO candidates(rid, name, surname, city, pesel, sex, education, skills, experience, courses) VALUES" +
                   "(" + recruitmentID + ",'" + canData.Name + "','" + canData.Surname + "','" + canData.City + "','" +
                   canData.Pesel + "','" + canData.Sex + "','" + canData.Education + "','" + canData.Skills + "','" +
                   canData.Experience + "','" + canData.Courses + "');";
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
            catch (MySqlException e)
            {
                ResultInfo(e.ToString());
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
