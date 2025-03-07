@minLength(3)
@maxLength(63)
param serverName string = 'spb-prod-sql' 
param adminUsername string = 'adminuser' 
@secure()
param adminPassword string 
param location string = resourceGroup().location 
param dbName string = 'spb-prod-db' 

// PostgreSQL Server Resource
resource sqlServer 'Microsoft.Sql/servers@2023-08-01-preview' = {
  name: serverName
  location: location
  properties: {
    administratorLogin: adminUsername
    administratorLoginPassword: adminPassword
  }
}

resource sqlDatabase 'Microsoft.Sql/servers/databases@2023-08-01-preview' = {
  parent: sqlServer
  location: location
  name: dbName
  properties: {
    zoneRedundant: false
    createMode: 'Default'
    licenseType: 'BasePrice'
    maxSizeBytes: 2147483648               
  }
  sku: {
    tier: 'Basic'
    capacity: 5
    name: 'Basic'
  }
}
