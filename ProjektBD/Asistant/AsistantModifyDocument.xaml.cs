using System.Windows.Controls;

namespace ProjektBD.Asistant
{
    /// <summary>
    /// Interaction logic for AsistantModifyDocument.xaml
    /// </summary>
    public partial class AsistantModifyDocument : UserControl
    {
        public AsistantModifyDocument()
        {
            InitializeComponent();
        }


        private void searchBy()
        {
            //(int) ((ComboBoxItem)comboBoxSearchBy.SelectedItem).Tag;
            //(string) ((ComboBoxItem)comboBoxSearchBy.SelectedItem).Content;
            switch ((int)((ComboBoxItem)comboBoxSearchBy.SelectedItem).Tag)
            {
                case 0:
                    // NIE WYBRANO
                    break;
                case 1:
                    AsistantSearchByCandidate sCan = new AsistantSearchByCandidate(2);
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
