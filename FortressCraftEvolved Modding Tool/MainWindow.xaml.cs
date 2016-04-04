using System.Windows;
using MahApps.Metro.Controls;
using FortressCraftEvolved_Modding_Tool.Data;

namespace FortressCraftEvolved_Modding_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            XmlLogic.ResearchReader.ReadResearchXML();
            if (User.Default.ResearchXmlPath == "")
            {
                MessageBox.Show("Research.XML path not found, please check settings!");
            }
            if (User.Default.ManufactorerXmlPath == "")
            {
                MessageBox.Show("ManufacturerRecipes.xml path not found, please check settings!");
            }
        }

        //When user clicks the settings button: Do this;
        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("This Will Prompt people with settings window for selecting File paths!");
            Forms.Form_PathSelector Popup = new Forms.Form_PathSelector();
            Popup.ShowDialog();
        }

        private void button_LoadResearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
