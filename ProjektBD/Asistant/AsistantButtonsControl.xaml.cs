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
            gridDisplay = GetMainWindowGridHandle();
        }

        private void addToPanelWithTracking(UIElement elem)
        {
            gridDisplay.Children.Add(elem);
            MonitorAdd(elem);
        }

        private Grid GetMainWindowGridHandle()
        {
            return ((MainWindow)Application.Current.MainWindow).GridPanelFunctions;
        }
        
        private void buttonAddCandidate_Click(object sender, RoutedEventArgs e)
        {
            MonitorDelete();
            AsistantAddCandidate addCan = new AsistantAddCandidate();
            addToPanelWithTracking(addCan);
        }

        private void buttonModifyCandidate_Click(object sender, RoutedEventArgs e)
        {
            MonitorDelete();
            AsistantModifyCandidate modifyCan = new AsistantModifyCandidate();
            addToPanelWithTracking(modifyCan);
        }

        private void MonitorAdd(Object obj)
        {
            objRef = obj;
        }

        private void MonitorDelete()
        {
            if (objRef != null)
                gridDisplay.Children.Remove(objRef as UIElement);
        }

        private void buttonSchedule_Click(object sender, RoutedEventArgs e)
        {
            MonitorDelete();
            AsistantSchedule schedule = new AsistantSchedule();
            addToPanelWithTracking(schedule);
        }

        private void buttonDeleteCandidate_Click(object sender, RoutedEventArgs e)
        {
            MonitorDelete();
            AsistantDeleteCandidate schedule = new AsistantDeleteCandidate();
            addToPanelWithTracking(schedule);
        }

        public void DeleteItemsAddedToGrid()
        {
            MonitorDelete();
        }


    }
}
