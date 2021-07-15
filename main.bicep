param cosmosDbAccountName string = 'cosmos-${uniqueString(resourceGroup().id)}'

param staticAppName string = 'stap-${uniqueString(resourceGroup().id)}'

@allowed([
  'centralus'
  'eastus2'
  'eastasia'
  'westeurope'
  'westus2'
])
param staticAppLocation string = 'eastasia'

param appInsightsName string = 'appi-${uniqueString(resourceGroup().id)}'
param logAnalyticsWorkspaceName string = 'log-${uniqueString(resourceGroup().id)}'

param location string = staticAppLocation

resource cosmosDbAccount 'Microsoft.DocumentDB/databaseAccounts@2021-03-01-preview' = {
  name: cosmosDbAccountName
  location: location
  kind: 'GlobalDocumentDB'
  properties: {
    createMode: 'Default'
    databaseAccountOfferType: 'Standard'
    locations: [
      {
        locationName: location
        failoverPriority: 0
        isZoneRedundant: false
      }
    ]
    backupPolicy: {
      type: 'Periodic'
      periodicModeProperties: {
        backupIntervalInMinutes: 240
        backupRetentionIntervalInHours: 8
        backupStorageRedundancy: 'Local'
      }
    }
    networkAclBypass: 'None'
    ipRules: [
      {
        ipAddressOrRange: '104.42.195.92' // Azure Portal https://docs.microsoft.com/ja-jp/azure/cosmos-db/how-to-configure-firewall#allow-requests-from-the-azure-portal
      }
      {
        ipAddressOrRange: '40.76.54.131' // Azure Portal
      }
      {
        ipAddressOrRange: '52.176.6.30' // Azure Portal
      }
      {
        ipAddressOrRange: '52.169.50.45' // Azure Portal
      }
      {
        ipAddressOrRange: '52.187.184.26' // Azure Portal
      }
      {
        ipAddressOrRange: '0.0.0.0' // Azure DataCenter https://docs.microsoft.com/ja-jp/azure/cosmos-db/how-to-configure-firewall#allow-requests-from-global-azure-datacenters-or-other-sources-within-azure
      }
    ]
    capabilities: [
      {
        name: 'EnableServerless'
      }
    ]
    enableFreeTier: false
  }
  tags: {
    defaultExperience: 'Core (SQL)'
  }
}

resource staticApp 'Microsoft.Web/staticSites@2020-12-01' = {
  name: staticAppName
  location: staticAppLocation
  properties: {}
  sku: {
    tier: 'Free'
    name: 'Free'
  }
}

resource staticApp_functionappsettings 'Microsoft.Web/staticSites/config@2020-12-01' = {
  parent: staticApp
  name: 'functionappsettings'
  properties: {
    APPINSIGHTS_INSTRUMENTATIONKEY: appInsights.properties.InstrumentationKey
    LineChannelId: ''
    LiffChannelId: ''
    LineLiffId: ''
    CosmosDbAccount: cosmosDbAccount.properties.documentEndpoint
    CosmosDbKey: listKeys(cosmosDbAccount.id, '2021-03-15').primaryMasterKey
  }
}

resource appInsights 'Microsoft.Insights/components@2020-02-02-preview' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalyticsWorkspace.id
  }
}

resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2020-08-01' = {
  name: logAnalyticsWorkspaceName
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 30
  }
}
