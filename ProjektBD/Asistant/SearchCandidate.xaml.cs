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

namespace ProjektBD.Asistant
{
    /// <summary>
    /// Interaction logic for SearchCandidate.xaml
    /// </summary>
    public partial class SearchCandidate : UserControl
    {
        public SearchCandidate()
        {
            InitializeComponent();
        }

        private void Search(string name, string surname, string city, string sex, string pesel)
        {
            Candidates can = new Candidates();
            can.SearchCommand(name, surname, city, sex, pesel);
            can.CreateList();
            dataGrid1.ItemsSource = can.GetList();
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            Search(textBoxName.Text, textBoxSurname.Text, textBoxCity.Text, textBoxSex.Text, textBoxPesel.Text);
        }
    }
}
