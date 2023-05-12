using CSGendoc.Library.Common;
using Serilog;

namespace CSGendoc.Library.Loaders
{
	public static class BuilderScriptLoader
	{
		private static ILogger Log { get; set; }

		public static readonly List<string> scripts = new List<string>();
		public static bool Load()
		{
			if (CSGendoc.config == null)
			{
				return false;
			}

			Log = SerilogLoader.GetLogger(typeof(BuilderScriptLoader));
			Log.Information("Loading Builder Script(s)...");

			foreach (string selectorStr in CSGendoc.config.build.scripts)
			{
				FileSelector selector = new FileSelector(selectorStr, CSGendoc.WorkDirectory);

				scripts.AddRange(selector.FindWithExtension(".lua"));
			}

			if (scripts.Count < 1)
			{
				return false;
			}
			return true;
		}
	}
}
