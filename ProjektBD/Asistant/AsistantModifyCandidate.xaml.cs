using System;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using ProjektBD.Database;

namespace ProjektBD.Asistant
{
    /// <summary>
    /// Interaction logic for AsistantModifyCandidate.xaml
    /// </summary>
    public partial class AsistantModifyCandidate : UserControl
    {
        private AsistantSearchCandidate handle;
        private bool displayButtonExists = false;
        private AsistantCandidateData canData;
        private bool modifyButtonExists = false;
        private Candidate canDataBeforModification;
        private Button modifySearch;
        Label resultInfo;

        public AsistantModifyCandidate()
        {
            InitializeComponent();
            handle = new AsistantSearchCandidate();
            mainPanel.Children.Add(handle);
            // Bedzie trzeba pozniej posprzatac po tym evencie ...
            handle.SearchRequested += new AsistantSearchCandidate.SearchDelegate(AddDisplayButton);
        }

        private void buttonDisplayCandidate_Click(object sender, RoutedEventArgs e)
        {
            DisplayCandidate();
        }

        private void AddDisplayButton(int value)
        {
            if (value > 0 && !displayButtonExists)
            {
                displayButtonExists = true;
                Button buttonSearch = new Button();
                buttonSearch.Height = Double.NaN;
                buttonSearch.Width = Double.NaN;
                buttonSearch.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                buttonSearch.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                buttonSearch.Content = "Modfikuj dane kandydata";
                buttonSearch.Margin = new System.Windows.Thickness(0, 20, 0, 0);
                buttonSearch.Click += new RoutedEventHandler(this.buttonDisplayCandidate_Click);
                mainPanel.Children.Add(buttonSearch);
            }
        }

        private void ResultInfo(string result)
        {
            mainPanel.Children.Remove(canData);
            if (modifyButtonExists)
                mainPanel.Children.Remove(modifySearch);
            if (resultInfo != null)
                mainPanel.Children.Remove(resultInfo);
            resultInfo = new Label();
            resultInfo.Content = result;
            mainPanel.Children.Add(resultInfo);
        }


        private void DisplayCandidate()
        {
            canData = new AsistantCandidateData();
            //pobrac ID kandydata, wykonac operacje na bazie, poinformowac o rezultacie
            try
            {
                CandidateAdapter obj = (CandidateAdapter)handle.dataGrid1.SelectedItem;
                canData.ID = obj.GetID();
                MySqlCommand command = DBConnection.Instance.Conn.CreateCommand();
                command.CommandText = "SELECT name, surname, city, pesel, sex, education, skills, experience, courses FROM candidates WHERE id ='" + canData.ID + "'";
                DBConnection.Instance.Conn.Open();
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    canData.Name = Reader.GetString(0);
                    canData.Surname = Reader.GetString(1);
                    canData.City = Reader.GetString(2);
                    canData.Pesel = Reader.GetString(3);
                    canData.Sex = Reader.GetChar(4);
                    canData.Education = Reader.GetString(5);
                    canData.Skills = Reader.GetString(6);
                    canData.Experience = Reader.GetString(7);
                    canData.Courses = Reader.GetString(8);
                    canDataBeforModification = new Candidate(canData);

                }
            }
            catch (MySqlException e)
            {
                ResultInfo(e.ToString());
            }
            catch (NullReferenceException e)
            {
                ResultInfo("Nie wybrano kandydata do modyfikacji.");
            }
            finally
            {
                DBConnection.Instance.Conn.Close();
            }

            mainPanel.Children.Add(canData);
            AddModifyButton();


        }

        private string UpdateCommand()
        {
            string command = "UPDATE candidates SET ";
            if( canData.Name != canDataBeforModification.Name)
                command += "name='" + canData.Name + "', ";
            if( canData.Surname != canDataBeforModification.Surname)
                command += "surname='" + canData.Surname + "', ";
            if( canData.Pesel != canDataBeforModification.Pesel)
                command += "pesel='" + canData.Pesel + "', ";
            if( canData.City != canDataBeforModification.City)
                command += "city='" + canData.City + "', ";
            if( canData.Sex != canDataBeforModification.Sex)
                command += "sex='" + canData.Sex + "', ";
            if( canData.Education != canDataBeforModification.Education)
                command += "education='" + canData.Education + "', ";
            if( canData.Skills != canDataBeforModification.Skills)
                command += "skills='" + canData.Skills + "', ";
            if( canData.Experience != canDataBeforModification.Experience)
                command += "experience='" + canData.Experience + "', ";
            if( canData.Courses != canDataBeforModification.Courses)
                command += "'courses'='" + canData.Courses + "', ";
            if (command[command.Length - 2] == ',')
                command = command.Substring(0, command.Length - 2) + ' ';
            command += "WHERE id=" + canData.ID;

            return command;
        }

        private void UpdateCanData()
        {
            try
            {
                string query = UpdateCommand();
                MySqlCommand addUser = new MySqlCommand(query, DBConnection.Instance.Conn);
                DBConnection.Instance.Conn.Open();
                addUser.ExecuteNonQuery();
                DBConnection.Instance.Conn.Close();
                ResultInfo("Modyfikacja przebiegla pomyslnie");
            }
            catch (MySqlException e)
            {
                ResultInfo(e.ToString());
            }
        }

        private void buttonModifyCandidate_Click(object sender, RoutedEventArgs e)
        {
            UpdateCanData();
        }

        private void AddModifyButton()
        {
            if (!modifyButtonExists)
            {
                modifyButtonExists = true;
                modifySearch = new Button();
                modifySearch.Height = Double.NaN;
                modifySearch.Width = Double.NaN;
                modifySearch.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                modifySearch.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                modifySearch.Content = "Zatwierdz modyfikacje";
                modifySearch.Margin = new System.Windows.Thickness(0, 20, 0, 0);
                modifySearch.Click += new RoutedEventHandler(this.buttonModifyCandidate_Click);
                mainPanel.Children.Add(modifySearch);
            }
        }

    }
    internal class Candidate
    {
        string name;
        string surname;
        string pesel;
        string city;
        char sex;
        string education;
        string skills;
        string experience;
        string courses;

        public string Name { get { return name; } set { name = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public string Pesel { get { return pesel; } set { pesel = value; } }
        public string City { get { return city; } set { city = value; } }
        public char Sex { get { return (char)sex; } set { sex = value; } }
        public string Education { get { return education; } set { education = value; } }
        public string Skills { get { return skills; } set { skills = value; } }
        public string Experience { get { return experience; } set { experience = value; } }
        public string Courses { get { return courses; } set { courses = value; } }

        public Candidate(AsistantCandidateData canData)
        {
            Name = canData.Name;
            Surname = canData.Surname;
            Pesel = canData.Pesel;
            City = canData.City;
            Sex = canData.Sex;
            Education = canData.Education;
            Skills = canData.Skills;
            Experience = canData.Experience;
            Courses = canData.Courses;
        }
    }
}
