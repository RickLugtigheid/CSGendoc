using CSGendoc.Library.Config;
using CSGendoc.Library.Loaders;
using Newtonsoft.Json;
using NLua;
using Serilog;

namespace CSGendoc.Library
{
	public static class CSGendoc
	{
		public static string WorkDirectory { get; set; }
		public static ILogger Log { get; private set; }
		public static Lua LuaExecutor { get; } = new Lua();

		public static bool Initialized { get; private set; } = false;

		public static CSGendocConfig? config;

		/// <summary>
		/// Load/Initialize CSGendoc.
		/// </summary>
		/// <param name="gendocDirectory">.csgendoc directory to work from.</param>
		/// <param name="verbose">If verbose is enabled</param>
		public static void Load(string gendocDirectory, bool verbose = false)
		{
			WorkDirectory = gendocDirectory;

			// Load serilog
			//
			SerilogLoader.Load(verbose);
			Log = SerilogLoader.GetLogger(typeof(CSGendoc));

			if (verbose)
			{
				Log.Debug($"Verbose output enabled.");
			}

			// Try to load .config file
			//
			if (!File.Exists(Path.Combine(WorkDirectory, ".config")))
			{
				Log.Fatal($"No .config file found in '{WorkDirectory}'");
				return;
			}
			if (!TryLoadJson(Path.Combine(WorkDirectory, ".config"), out config))
			{
				Log.Fatal("Invalid .config file");
				return;
			}
			Log.Debug($"Found .config file");

			// Load all assemblies
			//
			if (!AssemblyLoader.Load())
			{
				return;
			}

			// Load all builder scripts
			//
			if (!BuilderScriptLoader.Load())
			{
				Log.Fatal("No builder scripts loaded.");
				return;
			}

			// Setup lua objects/references
			//
			LuaExecutor["Gendoc"]	= new LuaObjects.LuaGendocHelper();
			LuaExecutor["Query"]	= new LuaObjects.DllQuery(AssemblyLoader.Assemblies.ToArray());

			Log.Information("CSGendoc started" + (verbose ? " in Verbose mode" : string.Empty) + ".");
			Initialized = true;
		}

		/// <summary>
		/// Runs all builder scripts to build the documentation.
		/// </summary>
		public static void StartBuild()
		{
			Log.Debug("CSGendoc building...");
			foreach (string script in BuilderScriptLoader.scripts)
			{
				ExecuteBuilderScript(script);
			}
		}

		/// <summary>
		/// Execute a build script located on the given path.
		/// </summary>
		/// <param name="scriptPath"></param>
		public static void ExecuteBuilderScript(string scriptPath)
		{
			Log.Information($"Running builder script '{scriptPath}'");
			try
			{
				LuaExecutor.DoFile(scriptPath);
			}
			catch (Exception ex)
			{
				Log.Error(ex.ToString());
			}
		}

		#region Private Methods
		private static bool TryLoadJson<T>(string path, out T result)
		{
			try
			{
#pragma warning disable CS8601 // Possible null reference assignment.
				result = JsonConvert.DeserializeObject<T>(
					File.ReadAllText(path)
				);
				return true;
			}
			catch (Exception e)
			{
				Log.Error($"Error '{e.Message}' when parsing json file '{path}'");
			}
			result = default;
#pragma warning restore CS8601 // Possible null reference assignment.
			return false;
		}
		#endregion
	}
}
