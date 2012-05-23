using System.Collections.Generic;
using System.Windows.Controls;

namespace ProjektBD.Asistant
{
    /// <summary>
    /// Interaction logic for AsistantCandidateData.xaml
    /// </summary>
    public partial class AsistantCandidateData : UserControl
    {
        private int id;
        public int ID { get { return id; } set { id = value; } }
        public new string Name { get { return textBoxName.Text; } set { textBoxName.Text = value; } }
        public string Surname { get { return textBoxSurname.Text; } set { textBoxSurname.Text = value; } }
        public string Pesel { get { return (string)maskedTextBoxPesel.Value; } set { maskedTextBoxPesel.Text = value; } }
        public string City { get { return textBoxCity.Text; } set { textBoxCity.Text = value; } }
        public char Sex { get { return (char)comboBoxSex.SelectedValue; } set { comboBoxSex.SelectedValue = value; } }
        public string Education { get { return textBoxEducation.Text; } set { textBoxEducation.Text = value; } }
        public string Skills { get { return textBoxSkills.Text; } set { textBoxSkills.Text = value; } }
        public string Experience { get { return textBoxExperience.Text; } set { textBoxExperience.Text = value; } }
        public string Courses { get { return textBoxCourses.Text; } set { textBoxCourses.Text = value; } }

        public AsistantCandidateData()
        {
            InitializeComponent();
            BindComboBox(comboBoxSex);
        }

        public ComboBox GetComboBoxDepart()
        {
            return ComboBoxDepartments;
        }

        private void BindComboBox(ComboBox comboBoxName)
        {
            Dictionary<char,string> dict = new Dictionary<char,string>();
            dict.Add('M',"Mezczyzna");
            dict.Add('K',"Kobieta");
            comboBoxName.ItemsSource = dict;
            comboBoxName.DisplayMemberPath = "Value";
            comboBoxName.SelectedValuePath = "Key";
        }

    }

}
