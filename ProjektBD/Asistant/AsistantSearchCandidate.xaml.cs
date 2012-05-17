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
    /// Interaction logic for AsistantSearchCandidate.xaml
    /// </summary>
    ///
    public partial class AsistantSearchCandidate : UserControl
    {
        public delegate void SearchDelegate(int value);
        public event SearchDelegate SearchRequested;

        public AsistantSearchCandidate()
        {
            InitializeComponent();
        }

        private int Search(string name, string surname, string city, string sex, string pesel)
        {
            CandidateAdapter can = new CandidateAdapter();
            can.SearchCommand(name, surname, city, sex, pesel);
            can.CreateList();
            dataGrid1.ItemsSource = can.GetList();
            return can.GetList().Count;
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            int result = Search(textBoxName.Text, textBoxSurname.Text, textBoxCity.Text, textBoxSex.Text, textBoxPesel.Text);
            FireSearchEvent(result);
        }

        public void FireSearchEvent(int value)
        {
            if (null != SearchRequested)
            {
                SearchRequested(value);
            }
        }

    }
}
