using System;
using System.Windows;
using System.Windows.Controls;

namespace ProjektBD.Asistant
{
    /// <summary>
    /// Interaction logic for AsistantButtonsControl.xaml
    /// </summary>
    public partial class AsistantButtonsControl : UserControl
    {
        //uchwyt do MainWindow.GridPanelFunctions alias
        private Grid gridDisplay;
        //referencja do aktualnie wyświetlanego obiektu w GridPanelFunctions
        private Object objRef = null; 

        public AsistantButtonsControl()
        {
            InitializeComponent();
            gridDisplay = ((MainWindow)Application.Current.MainWindow).GridPanelFunctions;
        }

        private void buttonAddCandidate_Click(object sender, RoutedEventArgs e)
        {
            if (gridDisplay.Children.Count > 0)
                gridDisplay.Children.Clear();
            AsistantAddCandidate addCan = new AsistantAddCandidate();
            gridDisplay.Children.Add(addCan);
        }

        private void buttonModifyCandidate_Click(object sender, RoutedEventArgs e)
        {
            if (gridDisplay.Children.Count > 0)
                gridDisplay.Children.Clear();
            AsistantModifyCandidate modifyCan = new AsistantModifyCandidate();
            gridDisplay.Children.Add(modifyCan);
        }

        private void MonitorAdd(Object obj)
        {
            objRef = obj;
        }

        private void buttonAddDocument_Click(object sender, RoutedEventArgs e)
        {
            if (gridDisplay.Children.Count > 0)
                gridDisplay.Children.Clear();
            AsistantAddDocument addDocument = new AsistantAddDocument();
            gridDisplay.Children.Add(addDocument);
        }

        private void buttonDeleteCandidate_Click(object sender, RoutedEventArgs e)
        {
            if (gridDisplay.Children.Count > 0)
                gridDisplay.Children.Clear();
            AsistantDeleteCandidate delCan = new AsistantDeleteCandidate();
            gridDisplay.Children.Add(delCan);
        }

        private void buttonDeleteDocument_Click(object sender, RoutedEventArgs e)
        {
            if (gridDisplay.Children.Count > 0)
                gridDisplay.Children.Clear();
            AsistantDeleteDocument delDoc = new AsistantDeleteDocument();
            gridDisplay.Children.Add(delDoc);
        }

        private void buttonModifyDocument_Click(object sender, RoutedEventArgs e)
        {
            if (gridDisplay.Children.Count > 0)
                gridDisplay.Children.Clear();
            AsistantModifyDocument modDoc = new AsistantModifyDocument();
            gridDisplay.Children.Add(modDoc);
        }
    }
}
