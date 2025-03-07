@description('The name of the Static Web App.')
param staticWebAppName string

@description('The location for the Static Web App.')
param location string = resourceGroup().location

@description('The name of the Azure Static Web Apps Free SKU tier.')
param sku string = 'Free'

resource staticWebApp 'Microsoft.Web/staticSites@2023-12-01' = {
  name: staticWebAppName
  location: location
  sku: {
    name: sku
    tier: 'Free'
  }
  properties: {}
}
