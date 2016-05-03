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
using Common;
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
            Refresh();
            EditMode(false);
        }

        private void Refresh()
        {
            comboBox_SpawnObject.Items.Clear();
            foreach (SpawnableObjectEnum enumValue in Enum.GetValues(typeof(SpawnableObjectEnum)))
            {
                comboBox_SpawnObject.Items.Add(enumValue);
            }
        }

        private void EditMode(bool lbIsEditing)
        {
            textBox_FriendlyName.IsEnabled = lbIsEditing ? true : false;
            comboBox_SpawnObject.IsEnabled = lbIsEditing ? true : false;
            textBox_LoadingAnim.IsEnabled = lbIsEditing ? true : false;
            textBox_WorkingAnim.IsEnabled = lbIsEditing ? true : false;
            textBox_UnloadingAnim.IsEnabled = lbIsEditing ? true : false;
            textBox_CraftingString.IsEnabled = lbIsEditing ? true : false;
            textBox_Value.IsEnabled = lbIsEditing ? true : false;
            textBox_PowerUsePerSec.IsEnabled = lbIsEditing ? true : false;
            textBox_PowerTransferPerSec.IsEnabled = lbIsEditing ? true : false;
            textBox_MaxPowerStorage.IsEnabled = lbIsEditing ? true : false;
            textBox_CraftTime.IsEnabled = lbIsEditing ? true : false;
            checkBox_OptionalIngredients.IsEnabled = lbIsEditing ? true : false;

            //Recipe Boxes:
            textBox_RecipeKey.IsEnabled = lbIsEditing ? true : false;
            textBox_RecipeCraftedKey.IsEnabled = lbIsEditing ? true : false;
            textBox_RecipeCraftedAmount.IsEnabled = lbIsEditing ? true : false;
            textBox_RecipeDesc.IsEnabled = lbIsEditing ? true : false;
            listBox_RecipeCraftCost.IsEnabled = lbIsEditing ? true : false;
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
                //Common.Error.Log(SelectPath.FileName);
                try
                {
                    mActiveGAC = XMLSerializer.Deserialize<GenericAutoCrafterDataEntry>(System.IO.File.ReadAllText(SelectPath.FileName));
                    lGACPath = SelectPath.FileName;
                    //Common.Error.Log("GAC PATH IS:" + lGACPath);
                }
                catch (Exception x)
                {
                    Error.Log("Failed to load GAC in ModGAC" + x);
                }
                //Lets put all the values into the boxes here!

                textBox_FriendlyName.Text = mActiveGAC.FriendlyName;
                comboBox_SpawnObject.SelectedItem = mActiveGAC.SpawnObject;
                textBox_LoadingAnim.Text = mActiveGAC.LoadingAnimation;
                textBox_WorkingAnim.Text = mActiveGAC.WorkingAnimation;
                textBox_UnloadingAnim.Text = mActiveGAC.UnloadingAnimation;
                textBox_CraftingString.Text = mActiveGAC.CraftingString;
                textBox_Value.Text = mActiveGAC.Value;
                textBox_PowerUsePerSec.Text = mActiveGAC.PowerUsePerSecond.ToString();
                textBox_PowerTransferPerSec.Text = mActiveGAC.PowerTransferPerSecond.ToString();
                textBox_MaxPowerStorage.Text = mActiveGAC.MaxPowerStorage.ToString();
                textBox_CraftTime.Text = mActiveGAC.CraftTime.ToString();
                checkBox_OptionalIngredients.IsChecked = mActiveGAC.OptionalIngredients;

                //Recipe Boxes:
                textBox_RecipeKey.Text = mActiveGAC.Recipe.Key;
                textBox_RecipeCraftedKey.Text = mActiveGAC.Recipe.CraftedKey;
                textBox_RecipeCraftedAmount.Text = mActiveGAC.Recipe.CraftedAmount.ToString();
                textBox_RecipeDesc.Text = mActiveGAC.Recipe.Description;
                listBox_RecipeCraftCost.Items.Clear();
                for (int i = 0; i < mActiveGAC.Recipe.Costs.Count; i++)
                {
                    listBox_RecipeCraftCost.Items.Add(mActiveGAC.Recipe.Costs[i].ToString());
                }
                button_SelectGAC.Visibility = Visibility.Hidden;
            }
        }

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditMode(true);
        }
    }
}
