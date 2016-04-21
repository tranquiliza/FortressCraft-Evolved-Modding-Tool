using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Win32;

namespace FortressCraftEvolved_Modding_Tool.Forms
{
	/// <summary>
	/// Interaction logic for PathSelectorDialog.xaml
	/// </summary>
	public partial class PathSelectorDialog
	{

		private readonly TaskCompletionSource<Int32> tcs; 
		public Task<Int32> Task => this.tcs.Task;

		#region .  DependencyProperties  .

		public static readonly DependencyProperty DataPathProperty = DependencyProperty.Register(
			"DataPath", typeof (string), typeof (PathSelectorDialog), new PropertyMetadata(default(string)));

		public static readonly DependencyProperty GamePathProperty = DependencyProperty.Register(
			"GamePath", typeof(string), typeof(PathSelectorDialog), new PropertyMetadata(default(string)));

		public static readonly DependencyProperty AuthorIdProperty = DependencyProperty.Register(
			"AuthorId", typeof(string), typeof(PathSelectorDialog), new PropertyMetadata(default(string)));

		public string DataPath
		{
			get { return (string) GetValue(DataPathProperty); }
			set { SetValue(DataPathProperty, value); }
		}

		public string GamePath
		{
			get { return (string) GetValue(GamePathProperty); }
			set { SetValue(GamePathProperty, value); }
		}

		public string AuthorId
		{
			get { return (string) GetValue(AuthorIdProperty); }
			set { SetValue(AuthorIdProperty, value); }
		}

		#endregion

		public PathSelectorDialog()
		{
			InitializeComponent();
			this.tcs = new TaskCompletionSource<Int32>();
			this.DoReset(null, null);
		}

		#region .  Commands  .

		private void CanBrowse(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void CanClose(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void CanReset(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void DoGamePathBrowse(object sender, ExecutedRoutedEventArgs e)
		{
			// TODO: Get rid of System.Windows.Forms FolderBrowserDialog for VistaBridge's OpenFolderDialog
			using (var ofd = new FolderBrowserDialog())
			{
				ofd.SelectedPath = this.GamePath;
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					this.GamePath = ofd.SelectedPath;
				}
			}
		}

		private void DoDataPathBrowse(object sender, ExecutedRoutedEventArgs e)
		{
			// TODO: Get rid of System.Windows.Forms FolderBrowserDialog for VistaBridge's OpenFolderDialog
			using (var ofd = new FolderBrowserDialog())
			{
				ofd.SelectedPath = this.DataPath;
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					this.DataPath = ofd.SelectedPath;
				}
			}
		}

		private void DoClose(object sender, ExecutedRoutedEventArgs e)
		{
			this.tcs.SetResult(0);
		}

		private void DoReset(object sender, ExecutedRoutedEventArgs e)
		{
			this.AuthorId = string.IsNullOrEmpty(User.Default.AuthorID)
				? this.AuthorId = Environment.UserName
				: User.Default.AuthorID;
			this.DataPath = string.IsNullOrEmpty(User.Default.WritePath)
				? Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) ?? Environment.CurrentDirectory, "Data")
				: User.Default.WritePath;

			this.GamePath = string.IsNullOrEmpty(User.Default.GameData)
				? this.GetGamePath()
				: User.Default.GameData;
		}

		#endregion

		private string GetGamePath()
		{
			var steamPath = this.GetSteamPath();
			if (steamPath == string.Empty)
				return steamPath;
			var fcPath = this.GetFCPath(steamPath);
			if (fcPath == string.Empty)
				return fcPath;

			// Assume 64, because who the hell has 32 bit noaw days?
			return Path.Combine(fcPath, "64", "Default", "Data"); 
		}

		private string GetSteamPath()
		{
			try
			{
				// Windows only, porting to Unix won't exactly work.
				var hive = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam");
				if (hive == null)
				{
					Debug.WriteLine("Unable to find Software\\Valve\\Steam Hive.");
					return String.Empty;
				}

				var values = hive.GetValueNames();
				var steamPath = values.Contains("SteamPath")
					? hive.GetValue("SteamPath")
					: null;
				var steamExe = values.Contains("SteamExe")
					? hive.GetValue("SteamExe")
					: null;

				if (steamPath != null)
					return Path.GetFullPath(steamPath.ToString());

				return steamExe != null 
					? Path.GetFullPath(steamExe.ToString()) 
					: string.Empty;
			}
			catch
			{
				// Silently ignore any Exceptions, and pretend steam doesn't exist.
				return string.Empty;
			}
		}

		private string GetFCPath(string steamPath)
		{
			// FUU No JSON.Net :L

			dynamic steamConfig;

			using (var fs = File.OpenRead(Path.Combine(steamPath, "config", "config.vdf")))
			using (var stream = new StreamReader(fs))
			{
				var jss = new JavaScriptSerializer();
				steamConfig = jss.Deserialize<dynamic>(this.HackVdfToJson(stream.ReadToEnd()));
			}

			dynamic config = steamConfig["InstallConfigStore"]["Software"]["Valve"]["Steam"];
			config["BaseInstallFolder_0"] = steamPath;

			var path = string.Empty;
			foreach (KeyValuePair<string, object> property in config)
			{
				if (property.Key.StartsWith("BaseInstallFolder_") && this.SteamLibrarySearch(property.Value.ToString(), "FortressCraft", out path))
					break;
			}

			return path;
		}

		private Boolean SteamLibrarySearch(string libraryPath, string gameName, out string gamePath)
		{
			gamePath = Path.GetFullPath(Path.Combine(libraryPath, "steamapps", "common", gameName));
			return Directory.Exists(gamePath);
		}

		// Only needed here? Not sure if you need to read VDF files elsewhere :P
		/// <summary>
		/// Based off of https://gist.github.com/AlienHoboken/5571903 , Which appears to belong to http://jwjdev.com/blog/dota-2-items_game-txt-json-for-developers
		/// </summary>
		private string HackVdfToJson(string input)
		{
			var output = $"{{\n{input}\n}}";
			output = Regex.Replace(output, "\"([^\"]*)\"(\\s*){", "\"${1}\": {");
			output = Regex.Replace(output, "\"([^\"]*)\"\\s*\"([^\"]*)\"", "\"${1}\": \"${2}\",");
			output = Regex.Replace(output, ",(\\s*[}\\]])", "${1}");
			output = Regex.Replace(output, "([}\\]])(\\s*)(\"[^\"]*\":\\s*)?([{\\[])", "${1},${2}${3}${4}");
			output = Regex.Replace(output, "}(\\s*\"[^\"]*\":)", "},${1}");
			return output;
		}
	}

	public static class CustomCommands
	{
		public static readonly RoutedUICommand GamePathBrowseCommand = new RoutedUICommand("Browse", "gamepath", typeof(CustomCommands));
		public static readonly RoutedUICommand DataPathBrowseCommand = new RoutedUICommand("Browse", "datapath", typeof(CustomCommands));
		public static readonly RoutedUICommand CloseCommand = new RoutedUICommand("Close", "close", typeof(CustomCommands));
		public static readonly RoutedUICommand ResetCommand = new RoutedUICommand("Reset", "reset", typeof(CustomCommands));
	}

}
