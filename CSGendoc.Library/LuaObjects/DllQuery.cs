using System.Collections;
using System.Reflection;

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
        public Type[] SelectChildrenOf(string baseClassName)
        {
            Type? baseClass = SelectClassByName(baseClassName);

            if (baseClass == null)
            {
                return new Type[] { };
            }

            List<Type> children = new List<Type>();
            children.Add(null); // For lua
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
                    //Gendoc.Log.Error(e.ToString());
                    Console.WriteLine(e.ToString());
                }
            }
            return children.ToArray();
        }
    }
}
