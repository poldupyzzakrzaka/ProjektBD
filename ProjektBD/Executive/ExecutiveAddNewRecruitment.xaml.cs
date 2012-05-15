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
using System.Collections.ObjectModel;

namespace ProjektBD.Executive
{
    /// <summary>
    /// Interaction logic for ExecutiveAddNewRecruitment.xaml
    /// </summary>
    public partial class ExecutiveAddNewRecruitment : UserControl
    {
        private Dictionary<int, string> dict_departments;
        public ObservableCollection<BoolStringClass> TheList { get; set; }

        public class BoolStringClass
        {
            public string TheText { get; set; }
            public int TheValue { get; set; }
            public bool TheChecked { get; set; }
            public int TheWidth { get; set; }
        }

        public ExecutiveAddNewRecruitment()
        {
            InitializeComponent();
            dict_departments = new Dictionary<int, string>();
            GetDepartmentList();
            TheList = new ObservableCollection<BoolStringClass>();

            MySqlCommand command = DBConnection.Instance.Conn.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "select * from specialization_type";
            try
            {
                DBConnection.Instance.Conn.Open();
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    int id = Reader.GetInt32(0);
                    string name = Reader.GetString(1);
                    TheList.Add(new BoolStringClass { TheText = name, TheValue = id, TheChecked = false, TheWidth = (int)listBox1.Width - 10 }); 
                }
                this.DataContext = this;
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
            string Query, Query2, Query3;
            try
            {
                int recruitmentId = -1;
                Query = "INSERT INTO recruitments(id,name,decription,department,needed_ppl) VALUES (null,'" + textBoxName.Text + "','" + textBoxDescription.Text + "'," + ComboBoxDepartments.SelectedValue + ",'" + IntegerUpDownHowManyNeeded.Value + "');";
                MySqlCommand addUser = new MySqlCommand(Query, DBConnection.Instance.Conn);
                Query2 = "Select id FROM recruitments WHERE name='" + textBoxName.Text + "' AND department='" + ComboBoxDepartments.SelectedValue + "';";
                MySqlCommand getRecId = new MySqlCommand(Query2, DBConnection.Instance.Conn);

                DBConnection.Instance.Conn.Open();
                addUser.ExecuteNonQuery();

                MySqlDataReader Reader = getRecId.ExecuteReader();
                if (Reader.Read())
                {
                    recruitmentId = Reader.GetInt32(0);
                }
                Reader.Close();

                for (int i = 0; i < TheList.Count; i++)
                {
                    if (TheList.ElementAt<BoolStringClass>(i).TheChecked)
                    {
                        Query3 = "INSERT INTO recruitment_specialization_test_types(rid,spid) VALUES ('" + recruitmentId + "','" + TheList.ElementAt<BoolStringClass>(i).TheValue + "');";
                        MySqlCommand addRecSpecTestTypes = new MySqlCommand(Query3, DBConnection.Instance.Conn);
                        addRecSpecTestTypes.ExecuteNonQuery();
                    }
                }

                DBConnection.Instance.Conn.Close();
            }
            catch (MySqlException ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
    }
}
