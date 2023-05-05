--[[

	A builder script that will build documentation for all Interfaces in the CSGendocDemo.inteface namespace.

	We first run a SelectAllFromNamespace query on all loaded assemblies.
	Than for each class found we build documentation files.

]]--

local interfaces = Query:SelectAllFromNamespace('CSGendocDemo.Interfaces')

-- Build documentation for each type in namespace
print(interfaces.Length)
for _, interface in ipairs(interfaces) do
	if interface.IsInterface then
		print("build documentation")
		local data = {
			type	= interface,
			methods = interface:GetMethods()
		}
		Gendoc:Build(interface.Name..'.interface.md', 'template/interface.template.md', data)
	end
end
