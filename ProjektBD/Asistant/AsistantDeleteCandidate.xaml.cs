using System;
using System.Windows;
using System.Windows.Controls;
using ProjektBD.Database;
using MySql.Data.MySqlClient;

namespace ProjektBD.Asistant
{
    /// <summary>
    /// Interaction logic for AsistantDeleteCandidate.xaml
    /// </summary>
    public partial class AsistantDeleteCandidate : UserControl
    {
        private AsistantSearchCandidate handle;
        private bool delButtonExists = false;
        private Label resultInfo;

        public AsistantDeleteCandidate()
        {
            InitializeComponent();
            handle = new AsistantSearchCandidate();
            mainPanel.Children.Add(handle);
            // Bedzie trzeba pozniej posprzatac po tym evencie ...
            handle.SearchRequested += new AsistantSearchCandidate.SearchDelegate(AddDeleteButton);
        }

        private void AddDeleteButton(int value)
        {
            if (value > 0 && !delButtonExists)
            {
                delButtonExists = true;
                Button buttonSearch = new Button();
                buttonSearch.Height = Double.NaN;
                buttonSearch.Width = Double.NaN;
                buttonSearch.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                buttonSearch.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                buttonSearch.Content = "Delete Candidate";
                buttonSearch.Margin = new System.Windows.Thickness(0, 20, 0, 0); 
                buttonSearch.Click += new RoutedEventHandler(this.buttonDeleteCandidate_Click);
                mainPanel.Children.Add(buttonSearch);
            }
        }

        private void buttonDeleteCandidate_Click(object sender, RoutedEventArgs e)
        {
            DeleteCandidate();
        }

        private void DeleteCandidate()
        {
            //pobrac ID kandydata, wykonac operacje na bazie, poinformowac o rezultacie
            MySqlCommand command = DBConnection.Instance.Conn.CreateCommand();
            try
            {
                CandidateAdapter obj = (CandidateAdapter)handle.dataGrid1.SelectedItem;
                int id = obj.GetID();
                string query = "DELETE FROM candidates WHERE id ='" + id + "'";
                MySqlCommand deleteUser = new MySqlCommand(query, DBConnection.Instance.Conn);
                DBConnection.Instance.Conn.Open();
                deleteUser.ExecuteNonQuery();
                DBConnection.Instance.Conn.Close();
                ResultInfo("Usunieto pomyslnie.");
            }
            catch (MySqlException e)
            {
                ResultInfo(e.ToString());
            }
            catch (NullReferenceException e)
            {
                ResultInfo("Nie wybrano kandydata do usuniecia.");
            }
        }

        private void Cleaining()
        {

        }

        private void ResultInfo(string result)
        {
            if (resultInfo != null)
                mainPanel.Children.Remove(resultInfo);
            resultInfo = new Label();
            resultInfo.Content = result;
            mainPanel.Children.Add(resultInfo);

        }

    }
}
