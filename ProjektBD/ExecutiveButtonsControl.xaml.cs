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


namespace ProjektBD
{
    /// <summary>
    /// Interaction logic for ExecutiveButtonsControl.xaml
    /// </summary>
    public partial class ExecutiveButtonsControl : UserControl
    {
        const int AddNewRec = 1;
        const int EditRec = 2;
        const int CandidPrev = 3;

        public delegate void ExecutiveMenuButtonClickedEventHandler(object sender, ExecutiveMenuButtonClickedEventArgs fe);
        public event ExecutiveMenuButtonClickedEventHandler ExecutiveMenuButtonClickedEvent;

        public ExecutiveButtonsControl()
        {
            InitializeComponent();
        }

        private void buttonAddNewRec_Click(object sender, RoutedEventArgs e)
        {
            ExecutiveMenuButtonClickedEvent(this, new ExecutiveMenuButtonClickedEventArgs(AddNewRec));
        }

        private void buttonEditRecruitments_Click(object sender, RoutedEventArgs e)
        {
            ExecutiveMenuButtonClickedEvent(this, new ExecutiveMenuButtonClickedEventArgs(EditRec));
        }

        private void buttonCandidatePreview_Click(object sender, RoutedEventArgs e)
        {
            ExecutiveMenuButtonClickedEvent(this, new ExecutiveMenuButtonClickedEventArgs(CandidPrev));
        }


    }

    public class ExecutiveMenuButtonClickedEventArgs : RoutedEventArgs
    {
        public ExecutiveMenuButtonClickedEventArgs(int _buttonId)
        {
            buttonId = _buttonId;
        }
        public int buttonId;
    }
}
