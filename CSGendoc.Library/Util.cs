using Newtonsoft.Json.Linq;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGendoc.Library
{
	public static class Util
	{
		/// <summary>
		/// Convert an array to a lua table object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static LuaTable ToTable<T>(this Lua instance, IEnumerable<T> array)
		{
			instance.NewTable("t" + array.GetHashCode());
			LuaTable table = instance.GetTable("t" + array.GetHashCode());
			int i = 1;
			foreach (T item in array)
			{
				table[i++] = item;
			}
			return table;
		}
	}
}
