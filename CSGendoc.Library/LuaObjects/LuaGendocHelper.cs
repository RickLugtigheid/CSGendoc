
using Scriban;

namespace CSGendoc.Library.LuaObjects
{
	public class LuaGendocHelper
	{
		/// <inheritdoc cref="Activator.CreateInstance(Type)"/>
		public object? CreateInstanceOf(Type type) => Activator.CreateInstance(type);

		/// <summary>
		/// Builds documentation with the given template and data.
		/// </summary>
		/// <param name="template">Template to render</param>
		/// <param name="data">Data to render into template</param>
		/// <returns>Success</returns>
		public bool Build(string outputName, string templatePath, object data)
		{
			// Check if our template path exists
			//
			templatePath = Path.Combine(CSGendoc.WorkDirectory, templatePath);
			if (!File.Exists(templatePath))
			{
				CSGendoc.Log.Error("Gendoc:Build() -> Error no template '" + templatePath + "' found.");
				return false;
			}

			// Make sure our output directory exists
			//
			string dirOut = Path.Combine(CSGendoc.WorkDirectory, CSGendoc.config.build.dirOut);
			if (!Directory.Exists(dirOut))
			{
				Directory.CreateDirectory(dirOut);
			}

			// Read our template and create a Scriban Template object
			Template template = Template.ParseLiquid(
				File.ReadAllText(templatePath)
			);

			// Build our documentation file
			//
			string fileOutPath = Path.Combine(dirOut, outputName);
			try
			{
				File.WriteAllText(
					fileOutPath,
					template.Render(data, member => member.Name)
				);
			}
			catch (Exception ex)
			{
				CSGendoc.Log.Error("Gendoc:Build() -> " + ex.ToString());
				return false;
			}
			return true;
		}
	}
}
