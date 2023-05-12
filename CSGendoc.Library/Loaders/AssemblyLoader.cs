using CSGendoc.Library.Common;
using Serilog;
using System.IO;
using System.Reflection;
using System.Xml;

namespace CSGendoc.Library.Loaders
{
	public static class AssemblyLoader
	{
		private static ILogger Log { get; set; }
		public static List<Assembly> Assemblies { get; } = new List<Assembly>();
		public static List<XmlElement> XmlDocumentation { get; } = new List<XmlElement>();

		/// <summary>
		/// 
		/// </summary>
		/// <returns>Success</returns>
		public static bool Load()
		{
			if (CSGendoc.config == null)
			{
				return false;
			}

			Log = SerilogLoader.GetLogger(typeof(AssemblyLoader));
			Log.Information("Loading Assembly(s)...");

			foreach (string selectorStr in CSGendoc.config.assembly.include)
			{
				FileSelector selector = new FileSelector(selectorStr, CSGendoc.WorkDirectory);

				// Find and load all dll files matching our selector
				//
				try
				{
					Log.Information("Loading Assembly(s) from '" + selector.FullPath + "'");

					foreach (string path in selector.FindWithExtension(".dll"))
					{
						Log.Debug("Loading Assembly: " + path);
						Assemblies.Add(Assembly.LoadFile(path));

						// Check if we should try to load a documentation.xml file for this assembly
						//
						if (CSGendoc.config.assembly.includeDocFiles)
						{
							TryLoadXmlDocForAssembly(path);
						}
					}
				}
				catch (Exception e)
				{
					Log.Error($"{e.GetType().Name} '{e.Message}' when trying to load Assembly(s) from '{selector.FullPath}'");
				}
			}

			if (Assemblies.Count == 0)
			{
				Log.Error("No Assembly(s) found");
				return false;
			}
			return true;
		}

		private static void TryLoadXmlDocForAssembly(string assemblyPath)
		{
			string xmlDocPath = Path.ChangeExtension(assemblyPath, ".xml");

			// Check if an xml documentation file is found on this path
			//
			if (!File.Exists(xmlDocPath))
			{
				return;
			}

			// Load the xml documentation file
			//
			try
			{
				Log.Debug("Loading Documentation: " + xmlDocPath);
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(xmlDocPath);

				// Add the doc element since we only need that element.
				XmlDocumentation.Add(xmlDoc["doc"]);
			}
			catch (Exception e)
			{
				Log.Error($"{e.GetType().Name} '{e.Message}' when trying to load Documentation from '{xmlDocPath}'");
			}
		}
	}
}
