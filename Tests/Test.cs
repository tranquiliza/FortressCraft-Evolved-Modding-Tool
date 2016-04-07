using NUnit.Framework;
using System;
using Common.XmlLogic;
using Common.GameLogics;
using System.IO;
using System.Collections.Generic;

namespace Tests
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestCase ()
		{
			List<ItemEntry> arrayOfItemEntry = XMLSerializer.Deserialize<List<ItemEntry>>(File.ReadAllText("Resources\\Items.xml"));

			Assert.IsNotNull(arrayOfItemEntry);
			Assert.IsNotNull(arrayOfItemEntry[0].Name);
			Assert.IsNotNull(arrayOfItemEntry[0].ScanRequirements[0]);
		}

		[Test ()]
		public void TestCase2 ()
		{
            //ArrayOfItemEntry arrayOfItemEntry = new ArrayOfItemEntry ();
            //arrayOfItemEntry.ItemEntry = new ItemEntry[1];
            //arrayOfItemEntry.ItemEntry [0] = new ItemEntry ();
            //arrayOfItemEntry.ItemEntry[0].Name = "tranq";

            //File.WriteAllText ("banana.xml", XMLSerializer.Serialize (arrayOfItemEntry));
        }
        [Test ()]
        public void TestTerrainDataEntry()
        {
            string FilePath = "Resources\\TerrainData.xml";
            List<TerrainDataEntry> ListOfTerrainDataEntry = XMLSerializer.Deserialize<List<TerrainDataEntry>>(File.ReadAllText(FilePath));
            Assert.IsNotNull(ListOfTerrainDataEntry[0]);
            Assert.IsNotNull(ListOfTerrainDataEntry[0].tags[0]);
        }
	}
}