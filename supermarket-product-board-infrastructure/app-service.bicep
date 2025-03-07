@description('The name of the App Service')
param appServiceName string

@description('The location of the App Service')
param location string = resourceGroup().location

@description('The name of the App Service Plan')
param appServicePlanName string = 'spb-prod-asp'

@description('The SKU/pricing tier of the App Service Plan')
param skuName string = 'D1'

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: skuName
    tier: 'Shared'
    size: 'D1'
    capacity: 1
  }
  properties: {
    perSiteScaling: false
    reserved: false
  }
}

resource appService 'Microsoft.Web/sites@2022-03-01' = {
  name: appServiceName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
  }
}
