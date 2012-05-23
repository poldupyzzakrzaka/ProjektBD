using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using MySql.Data.MySqlClient;
using ProjektBD.Database;

namespace ProjektBD.Asistant
{
    /// <summary>
    /// Interaction logic for AsistantAddDocumentPanel.xaml
    /// </summary>
    public partial class AsistantAddDocumentPanel : UserControl
    {
        private string fileLocation;
        private int ID;
        private string name;
        private string surname;

        public AsistantAddDocumentPanel()
        {
            InitializeComponent();
        }

        public void UpdateData(int id, string name, string surname)
        {
            ID = id;
            this.name = name;
            this.surname = surname;
            labelCan.Content = "Kandydat " + name + " " + surname;
        }

        private string searchFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Text files (*.txt)|*.txt|Microsoft WORD (*.doc;*.docx)|*.doc;*.docx|PDF (*.pdf)|*.pdf|All files (*.*)|*.*";
            dlg.FilterIndex = 3;
            dlg.RestoreDirectory = true;
            dlg.Multiselect = false;

            if (dlg.ShowDialog() == true)
            {
                string fileLocation = dlg.FileName;
                if (File.Exists(fileLocation))
                    return fileLocation;
                return null;
            }
            return null;
        }

        private void fileAdded(string name)
        {
            labelFilename.Content = name;
            labelFilename.Visibility = Visibility.Visible;
        }

        private void buttonAddFile_Click(object sender, RoutedEventArgs e)
        {
            fileLocation = searchFile();
            if (fileLocation != null)
            {
                fileAdded(System.IO.Path.GetFileName(fileLocation));
            }

        }

        private void buttonSendFile_Click(object sender, RoutedEventArgs e)
        {
            ResultInfo("git jest balblablablalbal");
            //string destPath = ConfigurationManager.AppSettings["docPath"];
            //if (System.IO.Directory.Exists(destPath))
            //{
            //    string fname = Path.GetFileName(fileLocation);
            //    string destFile = Path.Combine(destPath, fname);
            //    File.Copy(fileLocation, destFile);
            //  //addToDatabase(ID, destFile);
            //}
        }

        private void addToDatabase(int ID, string destFile)
        {
            try
            {
                string Query = "INSERT INTO can_documents(cid, name, file_location) VALUES" +
                   "(" + ID + ",'" + textBoxDescription.Text + "','" + destFile + "');";
                MySqlCommand addDoc = new MySqlCommand(Query, DBConnection.Instance.Conn);
                DBConnection.Instance.Conn.Open();
                addDoc.ExecuteNonQuery();
                string result = "Dodano plik : " + Path.GetFileName(destFile) + " powiazany z " + name + " " + surname;
                ResultInfo(result);
            }
            catch (MySqlException e)
            {
                ResultInfo(e.ToString());
            }
            finally
            {
                DBConnection.Instance.Conn.Close();
            }
        }

        public void ResultInfo(string result)
        {
            Label resultLabel = new Label();
            resultLabel.Content = result;
            resultLabel.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            resultLabel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            ((StackPanel)this.Parent).Children.Add(resultLabel);
        }

    }
}
