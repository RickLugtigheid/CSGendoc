
namespace CSGendoc.Library.Common
{
	public class FileSelector
	{
		private string _workDirectory;

		public readonly string selector;
		public string FullPath => Path.Combine(CSGendoc.WorkDirectory, selector);

		public FileSelector(string selector, string workDirectory)
		{
			this.selector = selector;
			this._workDirectory = workDirectory;
		}

		public List<string> FindWithExtension(string extension)
		{
			List<string> result = new List<string>();

			if (Path.GetExtension(FullPath) == extension)
			{
				result.Add(FullPath);
			}
			else if (Directory.Exists(FullPath))
			{
				foreach (string path in Directory.GetFiles(FullPath, "*" + extension, SearchOption.AllDirectories))
				{
					result.Add(path);
				}
			}
			return result;
		}
	}
}
