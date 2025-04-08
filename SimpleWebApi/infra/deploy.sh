#!/bin/bash

# Define variables
RESOURCE_GROUP="1-3370c829-playground-sandbox"
LOCATION="westus"
DEPLOYMENT_NAME="simple-web-api-deployment"

echo "🛠️ Creating resource group: $RESOURCE_GROUP"
az group create --name $RESOURCE_GROUP --location $LOCATION

echo "🚀 Deploying infrastructure using Bicep..."
az deployment group create \
  --resource-group $RESOURCE_GROUP \
  --name $DEPLOYMENT_NAME \
  --template-file main.bicep \
  --parameters @parameters/dev.parameters.json