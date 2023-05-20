using CommandLine;
using Gendoc;

namespace CSGendoc
{
	public class Program
	{
		static void Main(string[] args)
		{
			Parser.Default.ParseArguments<Options>(args)
				.WithParsed(HandleCommand);
		}

		static void HandleCommand(Options opts)
		{
			// Get the work directory
			//
			if (opts.WorkDirectory == null)
			{
				opts.WorkDirectory = Environment.CurrentDirectory;
			}

			// Find the .csgendoc directory
			//
			string gendocDir = Path.Combine(opts.WorkDirectory, ".csgendoc");
			if (!Directory.Exists(gendocDir))
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"No .csgendoc directory found in '{gendocDir}'");
				Console.ResetColor();
				return;
			}

			// Load/Initialize CSGendoc
			Library.CSGendoc.Load(gendocDir, opts.Verbose);

			if (!Library.CSGendoc.Initialized)
			{
				return;
			}

			// Start build
			Library.CSGendoc.StartBuild();
		}
	}
}