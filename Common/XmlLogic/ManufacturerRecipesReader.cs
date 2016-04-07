using System;
using System.Collections.Generic;
using System.Xml;
using Common.Data;
using Common.GameLogics;
using System.IO;

namespace Common.XmlLogic
{
    public class ManufacturerRecipesReader
    {
		public static void ReadManufactoringXML(string ManufactorerXmlPath)
        {
            if (ManufactorerXmlPath == "")
            {
                return;
            }
            DataHolder.ManufacturerEntries = XMLSerializer.Deserialize<List<CraftData>>(File.ReadAllText(ManufactorerXmlPath));
            
            /* OLD CODE
            CraftData Item = new CraftData();
            String ItemName = null;
            uint Amount = 0;
            if (ManufactorerXmlPath == "")
            {
                return;
            }
            using (XmlReader reader = XmlReader.Create(ManufactorerXmlPath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "CraftData":
                                //Indicates the start of a new Entry
                                if (Item.IsDirty == true)
                                {
                                    DataHolder.ManufacturerEntries.Add(Item);
                                    Item = new CraftData();
                                }
                                Item.IsDirty = true;
                                break;

                            case "Key":
                                if (reader.Read())
                                {
                                    Item.Key = reader.Value;
                                }

                                break;
                            case "Category":
                                if (reader.Read())
                                {
                                    Item.Category = reader.Value;
                                }
                                break;

                            case "Tier":
                                if (reader.Read())
                                {
                                    Int32.TryParse(reader.Value, out Item.Tier);
                                }
                                break;

                            case "CraftedName":
                                if (reader.Read())
                                {
                                    Item.CraftedName = reader.Value;
                                }
                                break;

                            case "CraftedAmount":
                                if (reader.Read())
                                {
                                    Int32.TryParse(reader.Value, out Item.CraftedAmount);
                                }
                                break;

                            case "Costs":
                                //Indicates the start of crafting Costs.
                                if (reader.Read())
                                {
                                    Item.CraftingCosts = new List<CraftCost>();
                                }
                                break;

                            case "CraftCost":
                                //Indicates the start of a new item entry for crafting cost!
                                break;

                            case "Name":
                                if (reader.Read())
                                {
                                    ItemName = reader.Value;
                                }
                                break;

                            case "Amount":
                                if (reader.Read())
                                {
                                    UInt32.TryParse(reader.Value, out Amount);
                                }
                                Item.CraftingCosts.Add(new CraftCost(ItemName, Amount));
                                break;

                            case "Description":
                                if (reader.Read())
                                {
                                    Item.Description = reader.Value;
                                }
                                break;

                            case "Hint":
                                if (reader.Read())
                                {
                                    Item.Hint = reader.Value;
                                }
                                break;

                            case "ResearchCost":
                                if (reader.Read())
                                {
                                    Int32.TryParse(reader.Value, out Item.ResearchCost);
                                }
                                break;

                            case "Scan":
                                if (reader.Read())
                                {
                                    Item.ScanRequirement.Add(reader.Value);
                                }
                                break;

                            case "Research":
                                if (reader.Read())
                                {
                                    Item.ResearchRequirement.Add(reader.Value);
                                }
                                break;

                            case "CanCraftAnywhere":
                                if (reader.Read())
                                {
                                    Boolean.TryParse(reader.Value, out Item.CanCraftAnywhere);
                                }
                                break;


                            default:
                                //If nothing here, we just default.
                                break;
                        }
                    }
                }
            }
            */
        }
    }
}
