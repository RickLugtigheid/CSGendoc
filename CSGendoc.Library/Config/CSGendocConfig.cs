
namespace CSGendoc.Library.Config
{
	public class CSGendocConfig
	{
		public CSGendocAssemblyConfig assembly;
		public CSGendocBuildConfig build;
	}
	public class CSGendocAssemblyConfig
	{
		public string[] include;
		public bool includeDocFiles = false;
	}
	public class CSGendocBuildConfig
	{
		public string[] scripts;
		public string dirOut = "./build";
	}
}
