
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
	}
	public class CSGendocBuildConfig
	{
		public string[] scripts;
		public string dirOut;
	}
}
