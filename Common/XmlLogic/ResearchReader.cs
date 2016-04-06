using System;
using System.Xml;
using FortressCraftEvolved_Modding_Tool.GameLogics;

namespace FortressCraftEvolved_Modding_Tool.XmlLogic
{
    static class ResearchReader
    {
		public static void ReadResearchXML(string ResearchXmlPath)
        {
            ResearchEntry NewResearchEntry = new ResearchEntry();
            //NewResearchEntry.dirty = true;
            //These are only used when we're reading Research Reqs!
            bool ReadingLabReq = false;
            int Amount = -1;
            String ResearchKey = "Empty";
            if (ResearchXmlPath == "")
            {
                return;
            }
            using (XmlReader reader = XmlReader.Create(ResearchXmlPath)) //Currently Reads The Debug Folder. Perhabs can make a static string for specifying path
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "ResearchDataEntry":
                                if (NewResearchEntry.dirty == true)
                                {
                                    Data.DataHolder.ResearchEntries.Add(NewResearchEntry);
                                    NewResearchEntry = new ResearchEntry();
                                }
                                NewResearchEntry.dirty = true;

                                break;

                            case "Key":
                                if (reader.Read())
                                {
                                    if (ReadingLabReq == false)
                                    {
                                        NewResearchEntry.Key = reader.Value;
                                    }
                                    else
                                    {
                                        ResearchKey = reader.Value;
                                    }
                                }
                                break;

                            case "Name":
                                if (reader.Read())
                                {
                                    NewResearchEntry.Name = reader.Value;
                                }
                                break;

                            case "IconName":
                                if (reader.Read())
                                {
                                    NewResearchEntry.IconName = reader.Value;
                                }
                                break;

                            case "ResearchCost":
                                if (reader.Read())
                                {
                                    UInt32.TryParse(reader.Value, out NewResearchEntry.ResearchCost);
                                }
                                break;

                            case "PreDescription":
                                if (reader.Read())
                                {
                                    NewResearchEntry.PreDescription = reader.Value;
                                }
                                break;

                            case "PostDescription":
                                if (reader.Read())
                                {
                                    NewResearchEntry.PostDescription = reader.Value;
                                }
                                break;

                            //This part handles Research Requirements:
                            case "ResearchRequirements":
                                //Indicates Start of Research List:
                                break;
                            case "Research":
                                //This is the Key of the Research Required:
                                if (reader.Read())
                                {
                                    NewResearchEntry.ResearchRequirements.Add(reader.Value);
                                }
                                break;
                            // Consider Research Requirements Handled!

                            //This part handles Scan Requirements!
                            case "ScanRequirements":
                                //Indicates start of Scan Requirements!
                                break;
                            case "Scan":
                                //Tells us what needs scanning!
                                if (reader.Read())
                                {
                                    NewResearchEntry.ScanRequirements.Add(reader.Value);
                                }
                                break;
                            //Scanrequirements Handled!

                            //Lab Requirements Start!
                            case "ProjectItemRequirements":
                                // This Indicates start of entries that the lab requires for research!
                                break;
                            case "Requirement":
                                // This Indicates start of an item which is needed for the lab!
                                ReadingLabReq = true;
                                break;
                            case "Amount":
                                if (reader.Read())
                                {
                                    Int32.TryParse(reader.Value, out Amount);
                                }
                                // After having read amount, we are now done reading a requirement
                                ReadingLabReq = false;
                                ProjectItemRequirements NewItemReq = new ProjectItemRequirements(ResearchKey, Amount);
                                NewResearchEntry.LabResearchItems.Add(NewItemReq);
                                break;
                            //Lab Requirements end!

                            default:
                                //In case nothing happens?! 
                                break;
                        }
                    }
                }
            }
        }

    }
}
