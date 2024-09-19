# LoginAPI

# .Net 8 後端登入系統API

appsettings.json 須設定

ORM：EFCORE CodeFirst 開發
DB：MsSQL Server 
ApiEndpoint：http://hostname:<port>/apiname

```
{
	"ConnectionStrings": {
		"DefaultConnection": "放入MSSQL連線字串"
	},
    "JwtSettings":{
        
        "securityKey":"隨意填入值最好是32字節",
        "Issuer":"隨意填入值",
        "Audience":"隨意填入值",
        "ExpireMinutes":30 (Default)
    }
}

```