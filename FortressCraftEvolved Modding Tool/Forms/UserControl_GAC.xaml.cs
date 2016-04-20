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
using Common.Data;
using Common.XmlLogic;

namespace FortressCraftEvolved_Modding_Tool.Forms
{
    /// <summary>
    /// Interaction logic for UserControl_GAC.xaml
    /// </summary>
    public partial class UserControl_GAC : UserControl
    {
        public UserControl_GAC()
        {
            InitializeComponent();
            //GACReader.ReadGAC(User.Default.GACXmlPath);

            textBlock_FriendlyName.Text = "";// GACDataHolder.GAC.FriendlyName;
            textBlock_SpawnObject.Text = "";// GACDataHolder.GAC.SpawnObject.ToString();
            textBlock_LoadingAnim.Text = "";//GACDataHolder.GAC.LoadingAnimation;
            textBlock_WorkingAnim.Text = "";//GACDataHolder.GAC.WorkingAnimation;
            textBlock_UnloadingAnim.Text = "";//GACDataHolder.GAC.UnloadingAnimation;
            textBlock_CraftingString.Text = "";//GACDataHolder.GAC.CraftingString;
            textBlock_Value.Text = "";//GACDataHolder.GAC.Value;
            textBlock_PowerUsePerSec.Text = "";//GACDataHolder.GAC.PowerUsePerSecond.ToString();
            textBlock_PowerTransferPerSec.Text = "";//GACDataHolder.GAC.PowerTransferPerSecond.ToString();
            textBlock_CraftTime.Text = "";//GACDataHolder.GAC.CraftTime.ToString();
            textBlock_MaxPowerStorage.Text = "";//GACDataHolder.GAC.MaxPowerStorage.ToString();
            textBlock_OptionalIngredients.Text = "";//GACDataHolder.GAC.OptionalIngredients.ToString();

            //Recipe Start;
            textBlock_RecipeKey.Text = "";//GACDataHolder.GAC.Recipe.Key;
            textBlock_RecipeCraftedKey.Text = "";//GACDataHolder.GAC.Recipe.CraftedKey;
            textBlock_RecipeCraftedAmount.Text = "";//GACDataHolder.GAC.Recipe.CraftedAmount.ToString();
            textBlock_RecipeDesc.Text = "";// GACDataHolder.GAC.Recipe.Description;
            //listBox_RecipeCosts.Items.Clear();
            //for (int i = 0; i < GACDataHolder.GAC.Recipe.Costs.Count; i++)
            //{
            //    listBox_RecipeCosts.Items.Add(GACDataHolder.GAC.Recipe.Costs[i].Text());
            //}

        }

        private void button_SelectGAC_Click(object sender, RoutedEventArgs e)
        {
            Form_GACSelector Dialog = new Form_GACSelector();
            Dialog.ShowDialog();
            textBlock_FriendlyName.Text = GACDataHolder.GAC.FriendlyName;
            textBlock_SpawnObject.Text = GACDataHolder.GAC.SpawnObject.ToString();
            textBlock_LoadingAnim.Text = GACDataHolder.GAC.LoadingAnimation;
            textBlock_WorkingAnim.Text = GACDataHolder.GAC.WorkingAnimation;
            textBlock_UnloadingAnim.Text = GACDataHolder.GAC.UnloadingAnimation;
            textBlock_CraftingString.Text = GACDataHolder.GAC.CraftingString;
            textBlock_Value.Text = GACDataHolder.GAC.Value;
            textBlock_PowerUsePerSec.Text = GACDataHolder.GAC.PowerUsePerSecond.ToString();
            textBlock_PowerTransferPerSec.Text = GACDataHolder.GAC.PowerTransferPerSecond.ToString();
            textBlock_CraftTime.Text = GACDataHolder.GAC.CraftTime.ToString();
            textBlock_MaxPowerStorage.Text = GACDataHolder.GAC.MaxPowerStorage.ToString();
            textBlock_OptionalIngredients.Text = GACDataHolder.GAC.OptionalIngredients.ToString();

            //Recipe Start;
            if (GACDataHolder.GAC.Recipe != null)
            {
                textBlock_RecipeKey.Text = GACDataHolder.GAC.Recipe.Key;
                string lsCraftedName = "";
                if (GACDataHolder.GAC.Recipe.CraftedKey == null)
                {
                    lsCraftedName = GACDataHolder.GAC.Recipe.CraftedName;
                }
                else
                {
                    lsCraftedName = GACDataHolder.GAC.Recipe.CraftedKey;
                }

                textBlock_RecipeCraftedKey.Text = lsCraftedName;

                textBlock_RecipeCraftedAmount.Text = GACDataHolder.GAC.Recipe.CraftedAmount.ToString();
                textBlock_RecipeDesc.Text = GACDataHolder.GAC.Recipe.Description;
                listBox_RecipeCosts.Items.Clear();
                for (int i = 0; i < GACDataHolder.GAC.Recipe.Costs.Count; i++)
                {
                    listBox_RecipeCosts.Items.Add(GACDataHolder.GAC.Recipe.Costs[i].Text());
                }
            }
        }
    }
}
