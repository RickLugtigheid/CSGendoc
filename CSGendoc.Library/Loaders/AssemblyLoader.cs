using CSGendoc.Library.Common;
using Serilog;
using System.Reflection;

namespace CSGendoc.Library.Loaders
{
	public static class AssemblyLoader
	{
		private static ILogger Log { get; set; }
		public static List<Assembly> Assemblies { get; } = new List<Assembly>();
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
	}
}
