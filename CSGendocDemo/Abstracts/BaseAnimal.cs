using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gendoc.Example.Abstracts
{
	public abstract class BaseAnimal
	{
		public string Name { get; protected set; }
		public string Description { get; protected set; }
		public AnimalStats stats = new AnimalStats();
		public virtual void SetDefaults()
		{

		}
	}

	public struct AnimalStats
	{
		public int lifeExpectancyMin;
		public int lifeExpectancyMax;
	}
}
