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
using Microsoft.Win32;
using System.IO;
using System.ComponentModel;
using System.Configuration;
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
        public AsistantAddDocumentPanel()
        {
            InitializeComponent();
        }

        public void UpdateData(int id, string name, string surname)
        {
            ID = id;
            labelCan.Content = "Kandydat " + "  " + name + surname;
        }

        private string searchFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Text files (*.txt)|*.txt|Microsoft WORD (*.doc;*.docx)|*.doc;*.docx|PDF (*.pdf)|*.pdf|All files (*.*)|*.*";
            dlg.FilterIndex = 2;
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
            string destPath = ConfigurationManager.AppSettings["docPath"];
            if (System.IO.Directory.Exists(destPath))
            {
                string fname = Path.GetFileName(fileLocation);
                string destFile = Path.Combine(destPath, fname);
                File.Copy(fileLocation, destFile);
                addToDatabase(ID, destFile);
            }
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
            }
            catch (MySqlException e)
            {
                //ResultInfo(e.ToString());
            }
            finally
            {
                DBConnection.Instance.Conn.Close();
            }
        }

    }
}
