using NUnit.Framework;
using System;

using FortressCraftEvolved_Modding_Tool.XmlLogic;
using FortressCraftEvolved_Modding_Tool.GameLogics;
using System.IO;

namespace Tests
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestCase ()
		{
			ArrayOfItemEntry arrayOfItemEntry = XMLSerializer.Deserialize<ArrayOfItemEntry>(File.ReadAllText("Items.xml"));
			Assert.IsNotNull(arrayOfItemEntry);
			Assert.IsNotNull(arrayOfItemEntry.ItemEntries[0]);
			Assert.IsNotNull(arrayOfItemEntry.ItemEntries[0].ScanRequirements[0].Scan[0]);
		}
	}
}

