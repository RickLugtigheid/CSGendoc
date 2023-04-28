using Gendoc.Example.Abstracts;

namespace Gendoc.Example.Animals
{
	internal class Fish : BaseAnimal
	{
		public override void SetDefaults()
		{
			Name = "Fish";
			Description = "Fish are aquatic vertebrate animals that have gills but lack limbs with digits, like fingers or toes.";

			stats.lifeExpectancyMin = 3;
			stats.lifeExpectancyMax = 5;
		}

		public void Swim()
		{
			// ...
		}
	}
}
