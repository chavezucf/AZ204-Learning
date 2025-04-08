targetScope = 'resourceGroup'

param location string = resourceGroup().location
param environment string
param appName string
param imageName string
param imageTag string
param acrName string
param skuName string = 'B1'

module acr 'modules/acr.bicep' = {
  name: 'acrDeployment'
  params: {
    acrName: acrName
    location: location
    tags: {
      environment: environment
    }
  }
}

module webApp 'modules/webapp.bicep' = {
  name: 'webAppDeployment'
  params: {
    appName: appName
    environment: environment
    location: location
    imageName: imageName
    imageTag: imageTag
    acrLoginServer: acr.outputs.loginServer
    acrName: acrName
    skuName: skuName
  }
}
