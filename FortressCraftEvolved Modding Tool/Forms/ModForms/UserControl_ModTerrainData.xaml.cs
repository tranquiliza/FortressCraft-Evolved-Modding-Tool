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
using Common.XmlLogic;
using Common.ModWriter;
using Common.ModLogics;
using System.IO;

namespace FortressCraftEvolved_Modding_Tool.Forms.ModForms
{
    /// <summary>
    /// Interaction logic for UserControl_ModTerrainData.xaml
    /// </summary>
    public partial class UserControl_ModTerrainData : UserControl
    {
        private string TerrainDataXmlPath;
        public UserControl_ModTerrainData()
        {
            InitializeComponent();
            string[] Split = User.Default.ConfigFilePath.Split('\\');
            TerrainDataXmlPath = string.Empty;
            for (int i = 0; i < Split.Length - 1; i++)
            {
                TerrainDataXmlPath += Split[i] + "\\";
            }
            TerrainDataXmlPath += "Xml\\";
            TerrainDataXmlPath += "TerrainData.xml";
            try
            {
                ModWriterDataHolder.TerrainDataEntries = XMLSerializer.Deserialize<List<TerrainDataEntry>>(File.ReadAllText(TerrainDataXmlPath));
            }
            catch (Exception x)
            {
                Common.Error.Log("Error: Items modding interfaace was unable to Deserialize " + x);
                //File.WriteAllText("ModItemsCreatorError.txt", x.ToString());
            }
        }
    }
}
