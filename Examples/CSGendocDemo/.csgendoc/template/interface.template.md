# {{ doc.Type.Name }}
{{ doc.Summary }}

## Methods
{{ for method in doc.Methods }} 
### {{ type.Name }}.{{ method.Name }}
**Summary:** {{ method.Summary }}
{{ if method.Params.Length != 0 }}**Params:**{{ for param in method.Params }}
- {{ param.Name }}: {{ param.Summary }}{{ end }}{{ end }}
{{ if method.Returns != "" }}**Returns:** {{ method.Returns }}{{ end }}{{ end }}


## Properties
{{ for prop in doc.Properties }} 
### {{ type.Name }}.{{ prop.Name }}
**Summary:** {{ prop.Summary }}
{{ end }}
