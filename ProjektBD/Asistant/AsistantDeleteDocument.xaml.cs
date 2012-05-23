using System.Windows.Controls;

namespace ProjektBD.Asistant
{
    /// <summary>
    /// Interaction logic for AsistantDeleteDocument.xaml
    /// </summary>
    public partial class AsistantDeleteDocument : UserControl
    {
        public AsistantDeleteDocument()
        {
            InitializeComponent();
        }

        private void searchBy()
        {
            //(int) ((ComboBoxItem)comboBoxSearchBy.SelectedItem).Tag;
            //(string) ((ComboBoxItem)comboBoxSearchBy.SelectedItem).Content;
            switch ((int) ((ComboBoxItem)comboBoxSearchBy.SelectedItem).Tag)
            {
                case 0:
                    // NIE WYBRANO
                    break;
                case 1:
                    AsistantSearchByCandidate sCan = new AsistantSearchByCandidate();
                    mainPanel.Children.Add(sCan);
                    break;
                default:
                    break;
            }
        }


        private void comboBoxSearchBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBoxItem)comboBoxSearchBy.SelectedItem).Tag != null)
                searchBy();
        }
    }
}
