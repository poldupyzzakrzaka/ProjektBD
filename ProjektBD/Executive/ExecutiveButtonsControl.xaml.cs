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


namespace ProjektBD.Executive
{
    /// <summary>
    /// Interaction logic for ExecutiveButtonsControl.xaml
    /// </summary>
    public partial class ExecutiveButtonsControl : UserControl
    {
        //uchwyt do MainWindow.GridPanelFunctions alias
        private Grid gridDisplay;

        public ExecutiveButtonsControl()
        {
            InitializeComponent();
            gridDisplay = ((MainWindow)Application.Current.MainWindow).GridPanelFunctions;
        }

        private void buttonAddNewRec_Click(object sender, RoutedEventArgs e)
        {
            if (gridDisplay.Children.Count > 0)
                gridDisplay.Children.Clear();
            ExecutiveAddNewRecruitment addRec = new ExecutiveAddNewRecruitment();
            addRec.VerticalAlignment = VerticalAlignment.Center;
            addRec.HorizontalAlignment = HorizontalAlignment.Center;
            gridDisplay.Children.Add(addRec);
        }

        private void buttonEditRecruitments_Click(object sender, RoutedEventArgs e)
        {
            if (gridDisplay.Children.Count > 0)
                gridDisplay.Children.Clear();
            ExecutiveModifyRecruitment modifyCan = new ExecutiveModifyRecruitment();
            gridDisplay.Children.Add(modifyCan);
        }

        private void buttonCandidatePreview_Click(object sender, RoutedEventArgs e)
        {
            if (gridDisplay.Children.Count > 0)
                gridDisplay.Children.Clear();
            ExecutiveCandidatePreview canPre = new ExecutiveCandidatePreview();
            gridDisplay.Children.Add(canPre);
        }

    }
}
