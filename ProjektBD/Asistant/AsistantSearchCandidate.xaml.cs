using System.Windows;
using System.Windows.Controls;

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
