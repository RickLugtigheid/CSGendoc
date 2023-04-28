using CommandLine;

namespace Gendoc
{
	public class Options
	{
		[Option('V', "version", Required = false, HelpText = "Prints the version")]
		public bool PrintVersion { get; set; }

		[Option('v', "verbose", Required = false, HelpText = "Enables verbose logging")]
		public bool Verbose { get; set; }

		[Option('d', "directory", Required = false, HelpText = "Set the work directory")]
		public string WorkDirectory { get; set; }
	}
}
