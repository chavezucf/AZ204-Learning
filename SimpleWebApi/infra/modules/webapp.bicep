param appName string
param environment string
param location string
param imageName string
param imageTag string
param acrLoginServer string
param acrName string
param skuName string

var fullImagePath = '${acrLoginServer}/${imageName}:${imageTag}'
var appServicePlanName = '${appName}-plan-${environment}'
var webAppName = '${appName}-${environment}'

// Get credentials from the existing ACR
resource acr 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' existing = {
  name: acrName
}

var acrCreds = listCredentials(acr.id, acr.apiVersion)
var dockerRegistryUsername = acrCreds.username
var dockerRegistryPassword = acrCreds.passwords[0].value

resource plan 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: skuName
    tier: skuName == 'B1' ? 'Basic' : 'Standard'
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

resource app 'Microsoft.Web/sites@2022-09-01' = {
  name: webAppName
  location: location
  kind: 'app,linux,container'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: plan.id
    siteConfig: {
      linuxFxVersion: 'DOCKER|${fullImagePath}'
      appSettings: [
        {
          name: 'WEBSITES_PORT'
          value: '8080'
        }
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: environment
        }
        {
          name: 'DOCKER_REGISTRY_SERVER_URL'
          value: 'https://${acrLoginServer}'
        }
        {
          name: 'DOCKER_REGISTRY_SERVER_USERNAME'
          value: dockerRegistryUsername
        }
        {
          name: 'DOCKER_REGISTRY_SERVER_PASSWORD'
          value: dockerRegistryPassword
        }
      ]
    }
    httpsOnly: true
  }
}
