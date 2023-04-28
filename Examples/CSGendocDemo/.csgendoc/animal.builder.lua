--[[

	A builder script that will build documentation for all Animal classes (classes that extend BaseAnimal).

	We first run a SelectChildrenOf query on all loaded assemblies.
	Than for each animal class found we build documentation files.

]]--

function build_animal(animalType)

	-- Create an new instance of this animal and set it's default values
	local data = Gendoc:CreateInstanceOf(animalType)
	data:SetDefaults()

	-- Build a documentation file for the animal
	print('Building \''..data.Name..'.md\'...')
	Gendoc:Build(data.Name..'.md', 'template/animal.md', data)
end

-- Select all animals
local animals = Query:SelectChildrenOf('BaseAnimal')

-- Build documentation for each animal in animals
for _, animal in ipairs(animals) do
	build_animal(animal)
end
