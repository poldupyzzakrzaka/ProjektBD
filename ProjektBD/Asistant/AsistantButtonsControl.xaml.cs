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
using System.Collections;

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

        private void buttonSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (gridDisplay.Children.Count > 0)
                gridDisplay.Children.Clear();
            AsistantSchedule schedule = new AsistantSchedule();
            gridDisplay.Children.Add(schedule);
        }

        private void buttonDeleteCandidate_Click(object sender, RoutedEventArgs e)
        {
            if (gridDisplay.Children.Count > 0)
                gridDisplay.Children.Clear();
            AsistantDeleteCandidate delCan = new AsistantDeleteCandidate();
            gridDisplay.Children.Add(delCan);
        }

    }
}
