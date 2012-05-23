using System.Collections.Generic;
using System.Windows.Controls;

namespace ProjektBD.Asistant
{
    /// <summary>
    /// Interaction logic for AsistantAddDocument.xaml
    /// </summary>
    public partial class AsistantAddDocument : UserControl
    {
        private AsistantAddDocumentPanel docControl;

        public AsistantAddDocument()
        {
            InitializeComponent();
            initDataGridCan();
            docControl = new AsistantAddDocumentPanel();
            mainPanel.Children.Add(docControl);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CandidateAdapter can = (CandidateAdapter)dataGridCanList.SelectedItem;
            int id = can.GetID();
            docControl.UpdateData(id, can.Name, can.Surname);
        }
        
        private void initDataGridCan()
        {
            dataGridCanList.ItemsSource = GetCanList();
        }

        private List<CandidateAdapter> GetCanList()
        {
            CandidateAdapter dataCan = new CandidateAdapter();
            dataCan.SearchCommand();
            dataCan.CreateList();
            return dataCan.GetList();
        }

    }

}
