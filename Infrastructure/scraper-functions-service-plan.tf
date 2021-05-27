resource "azurerm_app_service_plan" "funtion_service_plan" {
  name                = "azure-functions-test-service-plan"
  location            = azurerm_resource_group.scraper_test.location
  resource_group_name = azurerm_resource_group.scraper_test.name

  sku {
    tier = "Standard"
    size = "S1"
  }
}
