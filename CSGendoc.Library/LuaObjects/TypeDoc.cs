using CSGendoc.Library.Loaders;
using NLua;
using System.Text.RegularExpressions;
using System.Xml;

namespace CSGendoc.Library.LuaObjects
{
	public static class IdString
	{
		public const string Type		= "T:";
		public const string Method		= "M:";
		public const string Property	= "P:";
	}
	/// <summary>
	/// All <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/">XML documentation</see> for the given Type selected from a {AssemblyName}.xml file (if exists).
	/// </summary>
	public class TypeDoc
	{
		public Type Type { get; }
		public string Summary { get; }
		public TypeDocItem[] Methods { get; }
		public TypeDocItem[] Properties { get; }

		public TypeDoc(Type type, Lua instance, XmlNode memberSelf, XmlElement docMembers)
		{
			if (memberSelf == null)
			{
				return;
			}

			Type		= type;
			Summary		= FormatText(memberSelf?["summary"]?.InnerText ?? string.Empty);
			Methods		= SelectInstanceOf(IdString.Method, docMembers, instance);
			Properties	= SelectInstanceOf(IdString.Property, docMembers, instance);
		}
		private TypeDocItem[] SelectInstanceOf(string idString, XmlElement docMembers, Lua instance)
		{
			List<TypeDocItem> result = new List<TypeDocItem>();

			// Find all methods for the given Type
			//
			foreach (XmlNode member in docMembers)
			{
				if (member.Attributes == null || member.Attributes["name"] == null)
				{
					continue;
				}

				// Check if member matches {idString}{Type.FullName} (for more info check 'https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/#id-strings')
				//
				if (member.Attributes["name"].Value.StartsWith(idString + Type.FullName))
				{
					TypeDocItem methodItem = new TypeDocItem(member);
					result.Add(methodItem);
				}
			}
			return result.ToArray();
		}
		/// <summary>
		/// Formats texts from xml documentation.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string FormatText(string input)
		{
			// Remove all tabs, newlines, and spaces where there are more than 2.
			return Regex.Replace(input, @"\t|\n|\r|[ ]{2,}", "");
		}
	}

	/// <summary>
	/// An Documentation item (Method or Property) based on <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/recommended-tags">XML documentation member</see>.
	/// </summary>
	public class TypeDocItem
	{
		public string Name { get; }
		public string FullName { get; }
		public string Summary { get; }
		public string Returns { get; }
		public TypeDocItem[] Params { get; }

		public TypeDocItem(XmlNode docMember, bool isSubItem = false)
		{
			FullName = docMember.Attributes?["name"]?.Value ?? string.Empty;
			Name	 = GetName(FullName);

			// Check if our element is a subItem of an other TypeDocItem
			//
			if (!isSubItem)
			{
				Summary = TypeDoc.FormatText(docMember?["summary"]?.InnerText ?? string.Empty);
				Returns = TypeDoc.FormatText(docMember?["returns"]?.InnerText ?? string.Empty);
				Params = SelectChildren("param", docMember);
				// TODO: Add <typeparam>(s), <exception>(s)
			}
			else
			{
				Summary = docMember?.InnerText ?? string.Empty;
			}
		}

		/// <summary>
		/// Select all children where the node name matches the given nodeName
		/// </summary>
		/// <param name="nodeName"></param>
		/// <param name="memberSelf"></param>
		/// <returns></returns>
		private TypeDocItem[] SelectChildren(string nodeName, XmlNode? memberSelf)
		{
			List<TypeDocItem> children = new List<TypeDocItem>();

			if (memberSelf == null)
			{
				return children.ToArray();
			}

			foreach (XmlNode child in memberSelf)
			{
				if (child.Name == nodeName)
				{
					children.Add(new TypeDocItem(child, true));
				}
			}

			return children.ToArray();
		}

		/// <summary>
		/// Gets the name of a method or property from a full assembly name.
		/// </summary>
		/// <param name="fullName">Full assembly name. Like: Project.Common.MyClass.MyPropertyOrMethod</param>
		/// <returns>Name of property or method</returns>
		private static string GetName(string fullName)
		{
			if (fullName == string.Empty)
			{
				return string.Empty;
			}

			// Remove parameters if in name. Example: MyMethod(String.string).
			//
			string name = fullName;
			if (name.IndexOf('(') != -1)
			{
				name = name.Remove(name.IndexOf('('), name.Length - name.IndexOf('('));
			}

			// Get the last part of the full name. FullName example: Project.Common.MyClass.MyMethod
			name = name.Split('.').Last();

			// Return the name found in the given FullName
			return name;
		}
	}
}
