using System.Windows;
using MahApps.Metro.Controls;
using FortressCraftEvolved_Modding_Tool.Forms;
using FortressCraftEvolved_Modding_Tool.XmlLogic;

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

            ResearchReader.ReadResearchXML();
            
            //Let the user know to update the paths, mainly ownly shows on first time use!
            if (User.Default.ResearchXmlPath == "")
            {
                MessageBox.Show("Research.XML path not found, please check settings!");
            }
            if (User.Default.ManufactorerXmlPath == "")
            {
                MessageBox.Show("ManufacturerRecipes.xml path not found, please check settings!");
            }
            //A nice welcome message! :D -> Could display version name here?!
            textBlock_Welcome.Text += "\n Browse the application by using the buttons below!";
        }


        //When user clicks the settings button currently only shows path. Maybe add possible change of style (Dark / Light?)
        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            //Prompts people with the fileselector Windows Form. This is a windows form cause they have OpenFileDialog Controls!!
            Form_PathSelector Settings = new Forms.Form_PathSelector();
            Settings.ShowDialog();
        }

        private void button_LoadResearch_Click(object sender, RoutedEventArgs e)
        {
            //This takes the UserControl_Research and displays it on the MainWindow! No need for millions of windows! Woot!
            ContentMain.Content = new UserControl_Research();
        }
    }
}
