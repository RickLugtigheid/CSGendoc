using Gendoc.Example.Abstracts;

namespace Gendoc.Example.Animals
{
	internal class Bird : BaseAnimal
	{
		public override void SetDefaults()
		{
			Name = "Bird";
			Description = "Birds are a group of warm-blooded vertebrates, characterised by feathers, toothless beaked jaws, the laying of hard-shelled eggs, a high metabolic rate, a four-chambered heart, and a strong yet lightweight skeleton";

			stats.lifeExpectancyMin = 4;
			stats.lifeExpectancyMax = 100;
		}

		public void Fly()
		{
			// ...
		}
	}
}
