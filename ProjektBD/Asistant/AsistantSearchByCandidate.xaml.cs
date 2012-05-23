using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using ProjektBD.Database;
using Xceed.Wpf.Toolkit;
using System.Windows.Controls.Primitives;

namespace ProjektBD.Asistant
{
    /// <summary>
    /// Interaction logic for AsistantSearchByCandidate.xaml
    /// </summary>
    public partial class AsistantSearchByCandidate : UserControl
    {
        private StackPanel sPanel;
        private WrapPanel wPanel;
        private ListBox canListBox;
        private CheckListBox clb;
        private ListBox lb;
        private string btnText;
        private string oldText;
        private TextBox docDescription;
        //private string btnTag;
        private int mode = 0;
        Dictionary<CanData, List<Documents>> canDocsDict;

        public AsistantSearchByCandidate(int mode)
        {
            //mode = 1 delete documents
            //mode = 2 modify description of document
            InitializeComponent();
            this.mode = mode;
            if (mode == 1)
            {
                btnText = "Usun";
                //btnTag = "del";
            }
            else if (mode == 2)
            {
                btnText = "Modyfikuj opis";
                //btnTag = "mod";
            }
        }

        private List<CanData> SearchForCan()
        {
            string name = wtbName.Text;
            string surname = wtbSurname.Text;
            string pesel = wtbPesel.Text;
            List<CanData> canList = new List<CanData>();
 
            try
            {
                string query = "SELECT id, name, surname FROM candidates WHERE";
                if (name.Length > 0)
                    query += " name LIKE '%" + name + "%' AND";
                if (surname.Length > 0)
                    query += " surname LIKE '%" + surname + "%' AND";
                if (pesel.Length > 0)
                    query += " pesel LIKE '%" + pesel + "%'";
                if (query.EndsWith("AND"))
                    query = query.Substring(0, query.Length - 4);
                if (query.EndsWith("WHERE"))
                    query = query.Substring(0, query.Length - 6);
                MySqlCommand command = DBConnection.Instance.Conn.CreateCommand();
                command.CommandText = query;
                DBConnection.Instance.Conn.Open();
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    CanData can = new CanData();
                    can.ID = Reader.GetInt32(0);
                    can.Name = Reader.GetString(1);
                    can.Surname = Reader.GetString(2);
                    canList.Add(can);
                }
            }
            catch (MySqlException er)
            {
                //ResultInfo(e.ToString());
            }
            finally
            {
                DBConnection.Instance.Conn.Close();
            }

            return canList;
        }

        private Dictionary<CanData, List<Documents>> MakeCanDocsDict(List<CanData> canList)
        {
            Dictionary<CanData, List<Documents>> dictCanDocs = new Dictionary<CanData, List<Documents>>();
            for (int i = 0; i < canList.Count; i++)
            {
                try
                {
                    List<Documents> listDoc = new List<Documents>();
                    MySqlCommand command = DBConnection.Instance.Conn.CreateCommand();
                    command.CommandText = "SELECT id, name, file_location FROM can_documents WHERE cid='" + canList[i].ID + "'";
                    DBConnection.Instance.Conn.Open();
                    MySqlDataReader Reader = command.ExecuteReader();
                    while (Reader.Read())
                    {
                        Documents doc = new Documents();
                        doc.ID = Reader.GetInt32(0);
                        doc.Description = Reader.GetString(1);
                        doc.Path = Reader.GetString(2);
                        listDoc.Add(doc);
                    }
                    if (listDoc.Count > 0)
                    dictCanDocs.Add(canList[i], listDoc);
                }
                catch (MySqlException er)
                {
                    //ResultInfo(e.ToString());
                }
                finally
                {
                    DBConnection.Instance.Conn.Close();
                }
            }
            // Slownik zawiera ID kandydata powiazane z lista dokumentow (id dokumentu, opis, sciezka)
            return dictCanDocs;
        }

        private void canListBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (mode == 1)
            {
                if (!wPanel.Children.Contains(clb))
                    wPanel.Children.Add(clb);
                CanData can = (CanData)canListBox.SelectedItem;
                clb.SelectedItems = null;
                clb.ItemsSource = canDocsDict[can];
                clb.DisplayMemberPath = "Filename";
            }
            else if (mode == 2)
            {
                if (!wPanel.Children.Contains(lb))
                    wPanel.Children.Add(lb);
                CanData can = (CanData)canListBox.SelectedItem;
                lb.SelectedItem = null;
                lb.ItemsSource = canDocsDict[can];
                lb.DisplayMemberPath = "Filename";
            }
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (sPanel != null && sPanel.Children.Count > 0)
                sPanel.Children.Clear();
            List<CanData> canList = SearchForCan();
            canDocsDict = MakeCanDocsDict(canList);
            sPanel = new StackPanel();
            sPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            mainPanel.Children.Add(sPanel);
            {
                // wyswietlic wiecej kadydatow wybrac jednego wyswietlic jego dokumenty
                wPanel = new WrapPanel();
                canListBox = new ListBox();
                canListBox.ItemsSource = canDocsDict.Keys;
                canListBox.DisplayMemberPath = "Can";
                canListBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                canListBox.Width = double.NaN;
                canListBox.Height = double.NaN;
                canListBox.Margin = new Thickness(20, 20, 0, 0);
                canListBox.SelectionChanged += new SelectionChangedEventHandler(this.canListBox_SelectionChanged);
                if (mode == 1)
                    canListBox.SelectionChanged += new SelectionChangedEventHandler(this.clb_SelectedItemChanged);
                else if (mode == 2)
                    canListBox.SelectionChanged += new SelectionChangedEventHandler(this.lb_SelectionChanged);
                wPanel.Children.Add(canListBox);

                if (mode == 1)
                {
                    clb = new CheckListBox();
                    clb.Margin = new Thickness(20, 20, 0, 0);
                    clb.Width = Double.NaN;
                    clb.Height = Double.NaN;
                    clb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    clb.SelectedItemChanged += new Xceed.Wpf.Toolkit.Primitives.SelectedItemChangedEventHandler(this.clb_SelectedItemChanged);
                }
                else if (mode == 2)
                {
                    lb = new ListBox();
                    lb.Margin = new Thickness(20, 20, 0, 0);
                    lb.Width = Double.NaN;
                    lb.Height = Double.NaN;
                    lb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    lb.SelectionChanged += new SelectionChangedEventHandler(lb_SelectionChanged);
                    lb.SelectionMode = SelectionMode.Single;
                }
                sPanel.Children.Add(wPanel);
                CreateButton(btnText);
            }

        }

        private void CreateButton(string text)//, string tag)
        {
            Button btn = new Button();
            btn.Content = text;
            btn.Height = Double.NaN;
            btn.Width = Double.NaN;
            btn.Margin = new System.Windows.Thickness(20, 20, 0, 0);
            if (mode == 1)
                btn.Click += new RoutedEventHandler(this.buttonDelDoc_Click);
            else if (mode == 2)
            {
                btn.Click += new RoutedEventHandler(this.buttonModDoc_Click);
            }
            btn.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            btn.IsEnabled = false;
            //btn.Tag = tag;
            btn.Tag = 1;
            sPanel.Children.Add(btn);
        }

        private void clb_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            if (clb.SelectedItems != null && clb.SelectedItems.Count > 0)
                foreach (var item in sPanel.Children)
                {
                    if (item is Button && ((int)(((Button)item).Tag) == 1))
                    {
                        ((Button)item).IsEnabled = true;
                        break;

                    }
                }
            else
            {
                foreach (var item in sPanel.Children)
                {
                    if (item is Button && ((int)(((Button)item).Tag) == 1))
                    {
                        ((Button)item).IsEnabled = false;
                        break;

                    }
                }
            }
        }

        private void lb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (lb.SelectedItem != null && lb.SelectedItems.Count > 0)
                foreach (var item in sPanel.Children)
                {
                    if (item is Button && ((int)(((Button)item).Tag) == 1))
                    {
                        ((Button)item).IsEnabled = true;
                        break;

                    }
                }
            else
            {
                foreach (var item in sPanel.Children)
                {
                    if (item is Button && ((int)(((Button)item).Tag) == 1))
                    {
                        ((Button)item).IsEnabled = false;
                        break;

                    }
                }
            }
        }

        private void buttonDelDoc_Click(object sender, RoutedEventArgs args)
        {
            int size = clb.SelectedItems.Count;
            int[] arr = new int[size];
            int i = 0;
            foreach (Documents elem in clb.SelectedItems)
            {
                arr[i] = elem.ID;
                i++;
            }
            try
            {
                string query = "DELETE FROM can_documents WHERE id ";
                if (size > 1)
                {
                    query += "IN(";
                    foreach (int elem in arr)
                        query += elem.ToString() + ",";
                    query = query.Substring(0, query.Length - 1) + ")";
                }
                else
                    query += "=" + arr[0].ToString();

                MySqlCommand delDoc = new MySqlCommand(query, DBConnection.Instance.Conn);
                DBConnection.Instance.Conn.Open();
                delDoc.ExecuteNonQuery();
            }
            catch (MySqlException er)
            {
                ResultInfo(er.ToString());
            }
            finally
            {
                DBConnection.Instance.Conn.Close();
                sPanel.Children.Clear();
            }
            ResultInfo("Usunieto pomyslnie.");
        }

        private void btnMod_Click(object sender, RoutedEventArgs args)
        {
            string result = "Zmodyfikowano";
            int id = ((Documents)lb.SelectedItem).ID;
            string query = "UPDATE can_documents SET ";
            if (oldText != docDescription.Text)
            {
                query += "name='" + docDescription.Text + "' WHERE id=" + id;
                try
                {
                    MySqlCommand delDoc = new MySqlCommand(query, DBConnection.Instance.Conn);
                    DBConnection.Instance.Conn.Open();
                    delDoc.ExecuteNonQuery();
                }
                catch (MySqlException er)
                {
                    result = er.ToString();
                }
                finally
                {
                    DBConnection.Instance.Conn.Close();
                    sPanel.Children.Clear();
                }
                ResultInfo(result);
            }
        }

        private void buttonModDoc_Click(object sender, RoutedEventArgs args)
        {
            sPanel.Children.Remove(this);
            Label lab = new Label();
            lab.Content = "Opis :";
            docDescription = new TextBox();
            docDescription.Text = ((Documents)lb.SelectedItem).Description;
            docDescription.TextChanged +=new TextChangedEventHandler(docDescription_TextChanged);
            Button btnMod = new Button();
            btnMod.Tag = 2;
            btnMod.Content = "Zatwierdz modyfikacje";
            btnMod.IsEnabled = false;
            btnMod.Click += new RoutedEventHandler(this.btnMod_Click);
            sPanel.Children.Add(lab);
            sPanel.Children.Add(docDescription);
            sPanel.Children.Add(btnMod);
            oldText = docDescription.Text;
        }

        private void  docDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (docDescription.Text != oldText)
            {
                foreach (var item in sPanel.Children)
                {
                    if (item is Button && ((int)(((Button)item).Tag) == 2))
                    {
                        ((Button)item).IsEnabled = true;
                        break;

                    }
                }
            }
            else
            {
                foreach (var item in sPanel.Children)
                {
                    if (item is Button && ((int)(((Button)item).Tag) == 2))
                    {
                        ((Button)item).IsEnabled = false;
                        break;

                    }
                }
            }
        }

        private void ResultInfo(string result)
        {
            Label labOK = new Label();
            labOK.HorizontalAlignment = HorizontalAlignment.Left;
            labOK.VerticalAlignment = VerticalAlignment.Top;
            labOK.VerticalContentAlignment = VerticalAlignment.Top;
            labOK.HorizontalContentAlignment = HorizontalAlignment.Center;
            labOK.Content = result;
            labOK.Margin = new Thickness(20, 20, 0, 0);
            labOK.Width = 400;
            labOK.Height = 30;
            labOK.Foreground = new SolidColorBrush(Colors.Red);
            sPanel.Children.Add(labOK);
        }
    }

    internal class CanData
    {
        private int id;
        private string name;
        private string surname;

        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public string Can { get { return name + " " + surname; } }
        public CanData() { }
    }

    internal class Documents
    {
        private int id;
        private string description;
        private string path;

        public int ID { get { return id; } set { id = value; } }
        public string Description { get { return description; } set { description = value; } }
        public string Path { get { return path; } set { path = value; } }
        public string Filename { get { return System.IO.Path.GetFileName(path); } }
        public Documents() {}

    }
}
