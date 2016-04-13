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
using Common.ModLogics;
using System.IO;
using Common.ModWriter;

namespace FortressCraftEvolved_Modding_Tool.Forms.ModForms
{
    /// <summary>
    /// Interaction logic for UserControl_ModItems.xaml
    /// </summary>
    public partial class UserControl_ModItems : UserControl
    {
        public UserControl_ModItems()
        {
            InitializeComponent();

            string[] Split = User.Default.ConfigFilePath.Split('\\');
            string XmlFilePath = "";

            for (int i = 0; i < Split.Length-1; i++)
            {
                XmlFilePath += Split[i] + "\\";
            }
            XmlFilePath += "Xml\\";

            string ItemsXmlPath = XmlFilePath + "Items.xml";
            ModWriterDataHolder.Items = XMLSerializer.Deserialize<List<ItemEntry>>(File.ReadAllText(ItemsXmlPath));
            for (int i = 0; i < ModWriterDataHolder.Items.Count; i++)
            {
                listBox_Items.Items.Add(ModWriterDataHolder.Items[i].Name);
            }
        }
    }
}
