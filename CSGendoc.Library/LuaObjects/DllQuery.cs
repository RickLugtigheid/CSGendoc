using Newtonsoft.Json;
using NLua;
using System.Collections;
using System.Reflection;
using System.Xml;

namespace CSGendoc.Library.LuaObjects
{
    public class DllQuery
    {
        private readonly Assembly[] _assemblies;
        public DllQuery(Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

		/// <summary>
		/// Selects all documentation for the given Type.
		/// <para>Note: This requires the build to contain 1 or more XML documentation files, and requires the .config.assembly.includeDocFiles to be True</para>
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public TypeDoc SelectDocForType(Type type)
        {
            foreach (XmlElement documentation in Loaders.AssemblyLoader.XmlDocumentation)
            {
                // Check if the documentation has members
                //
                if (documentation["members"] == null)
                {
                    continue;
                }

                // Check if our Type can be found in documentation members
                //
                foreach (XmlNode member in documentation["members"])
                {
                    // Check if the member has the name attribute
                    //
                    if (member.Attributes == null || member.Attributes["name"] == null)
                    {
                        continue;
                    }

					// Check if the name attribute matches our Type
                    // 
                    if (member.Attributes["name"].Value == "T:" + type.FullName)
                    {
                        // Return our Type documentation object
                        return new TypeDoc(type, CSGendoc.LuaExecutor, member, documentation["members"]);
                    }
				}
			}
            return null; // No documentation was found for this type
        }

        /// <summary>
        /// Find the class type by name.
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public Type? SelectClassByName(string className)
        {
            foreach (Assembly assembly in _assemblies)
            {
                try
                {
                    return assembly.GetTypes().FirstOrDefault(t => t.Name == className);
                }
                catch (Exception e)
                {
                    //Gendoc.Log.Error(e.ToString());
                    Console.WriteLine(e.ToString());
                }
            }
            return null;
        }


        /// <summary>
        /// Select all child classes that extend the given class.
        /// </summary>
        /// <param name="baseClassName">Name of the parent/base class</param>
        /// <returns>Children found</returns>
        public LuaTable SelectChildrenOf(string baseClassName)
        {
            Type? baseClass = SelectClassByName(baseClassName);

            if (baseClass == null)
            {
                return CSGendoc.LuaExecutor.ToTable(new Type[] { });
			}

            List<Type> children = new List<Type>();
            foreach (Assembly assembly in _assemblies)
            {
                try
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (type.BaseType == baseClass)
                        {
                            children.Add(type);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return CSGendoc.LuaExecutor.ToTable(children);
        }

        /// <summary>
        /// Select all <see cref="Type"/>s from the given namespace.
        /// </summary>
        /// <param name="namespacePath"></param>
        /// <returns></returns>
        public LuaTable SelectAllFromNamespace(string namespacePath)
        {
			List<Type> all = new List<Type>();
			foreach (Assembly assembly in _assemblies)
			{
				try
				{
					foreach (Type type in assembly.GetTypes())
					{
						if (type.Namespace == namespacePath)
						{
							all.Add(type);
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.ToString());
				}
			}
            return CSGendoc.LuaExecutor.ToTable(all);
		}
    }
}
