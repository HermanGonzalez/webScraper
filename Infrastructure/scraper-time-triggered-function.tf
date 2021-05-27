resource "azurerm_function_app" "time_triggered_function" {
  name                       = "time-triggerd-azure-functions"
  location                   = azurerm_resource_group.scraper_test.location
  resource_group_name        = azurerm_resource_group.scraper_test.name
  app_service_plan_id        = azurerm_app_service_plan.funtion_service_plan.id
  storage_account_name       = azurerm_storage_account.scraper_backend.name
  storage_account_access_key = azurerm_storage_account.scraper_backend.primary_access_key
  
  app_settings = {
	"AzureFunctionsJobHost__logging__logLevel__Default": "Trace"
	"ScrapPageUrl": "https://books.toscrape.com"
    "LandingPage": "index.html"
    "EventHubName": azurerm_eventhub.scraper_hub.name
    "EventHubConnectionString": azurerm_eventhub_authorization_rule.scraper_rule.primary_connection_string
  }
}