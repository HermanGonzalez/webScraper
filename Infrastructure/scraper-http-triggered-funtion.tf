resource "azurerm_function_app" "http_triggered_function" {
  name                       = "http-triggerd-azure-functions"
  location                   = azurerm_resource_group.scraper_test.location
  resource_group_name        = azurerm_resource_group.scraper_test.name
  app_service_plan_id        = azurerm_app_service_plan.funtion_service_plan.id
  storage_account_name       = azurerm_storage_account.scraper_backend.name
  storage_account_access_key = azurerm_storage_account.scraper_backend.primary_access_key
  
  app_settings = {
	"AzureFunctionsJobHost__logging__logLevel__Default": "Trace"
	"StorageAccountConnectionString": azurerm_storage_account.scraper_backend.primary_connection_string
  }
}