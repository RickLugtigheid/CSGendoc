--[[

	A builder script that will build documentation for all Classess in the CSGendocDemo.Common namespace.

	We first run a SelectAllFromNamespace query on all loaded assemblies.
	Than for each type found we build documentation files.

]]--

local types = Query:SelectAllFromNamespace('CSGendocDemo.Common')

-- Build documentation for each type in namespace
for _, type in ipairs(types) do
	print(type.Name)

	if type.IsClass then
		Gendoc:Build(type.Name..'.class.md', {
			type = type,
			doc  = {}
		});
	end
end
