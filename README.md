# ITX Async
This repository contains an implementation of an Azure Function accepts a list of filenames array and a filename pattern.
It will output the result of matching filenames for the given pattern.

The function can be invoked with a POST request to Uri ```http://localhost:7276/api/RegexFileNameMatch``` and body

```json
{
	"files": [{
			"name": "Archive",
			"type": "Folder"
		},
		{
			"name": "TempPartner_F_I_20221127.txt",
			"type": "File"
		},
		{
			"name": "TempPartner_C_I_20221127.txt",
			"type": "File"
		},
		{
			"name": "TempPartner_K_I_20221128.txt",
			"type": "File"
		},
		{
			"name": "TempPartner_T_I_20221129.txt",
			"type": "File"
		}
	],
	"pattern": "TempPartner_T_I_.+.txt$"
}
```

### Success

```json
[
  "TempPartner_T_I_20221129.txt"
]
```


# Usage
To use this function, you will need an Azure account and the Azure Functions runtime.
