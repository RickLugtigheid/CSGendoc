--[[

A basic builder script example.
This script will build documentation for our Animal classes.

]]--

-- Select all types that extend the BaseAnimal class
local animals = query:SelectChildrenOf("BaseAnimal")

for _, animal in ipairs(animals) do
	-- Create an instance/object for our Type
	local data = gendoc:createInstance(animal)
	-- Run the SetDefaults method on this object
	data:SetDefaults()

	-- Build documentation for this object using a template.
	-- The object is passed to the template as an argument,
	-- and the contents of this object can be used for templating
	gendoc:("templates/animal.md", data)
end