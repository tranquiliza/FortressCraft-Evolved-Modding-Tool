using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common.ModLogics;
using Common.XmlLogic;
using Common.ModWriter;

namespace FortressCraftEvolved_Modding_Tool.Forms.ModForms
{
    /// <summary>
    /// Interaction logic for UserControl_ModGAC.xaml
    /// </summary>
    public partial class UserControl_ModGAC : UserControl
    {

        private GenericAutoCrafterDataEntry mActiveGAC = null;
        private string lGACPath = string.Empty;
        public UserControl_ModGAC()
        {
            InitializeComponent();
        }

        private void button_SelectGAC_Click(object sender, RoutedEventArgs e)
        {
            lGACPath = string.Empty;
            string[] Split = User.Default.ConfigFilePath.Split('\\');
            for (int i = 0; i < Split.Length - 1; i++)
            {
                lGACPath += Split[i] + "\\";
            }
            lGACPath += "Xml\\";
            lGACPath += "GenericAutoCrafter";

            Microsoft.Win32.OpenFileDialog SelectPath = new Microsoft.Win32.OpenFileDialog();
            SelectPath.DefaultExt = ".xml";
            SelectPath.Filter = "XML Files (*.xml)|*.xml";

            SelectPath.InitialDirectory = lGACPath;

            bool? lResult = SelectPath.ShowDialog();

            if (lResult == true)
            {
                Common.Error.Log(SelectPath.FileName);
                try
                {
                    mActiveGAC = XMLSerializer.Deserialize<GenericAutoCrafterDataEntry>(System.IO.File.ReadAllText(SelectPath.FileName));
                    lGACPath = SelectPath.FileName;
                    Common.Error.Log("GAC PATH IS:" + lGACPath);
                }
                catch (Exception x)
                {
                    Common.Error.Log("Failed to load GAC in ModGAC" + x);
                }
            }
        }
    }
}
