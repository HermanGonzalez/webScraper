resource "azurerm_eventhub_namespace" "scraper_event_hub" {
  name                = "scraper-test-hub"
  location            = azurerm_resource_group.scraper_test.location
  resource_group_name = azurerm_resource_group.scraper_test.name
  sku                 = "Standard"
  capacity            = 1
}

resource "azurerm_eventhub" "scraper_hub" {
  name                = "input"
  namespace_name      = azurerm_eventhub_namespace.scraper_event_hub.name
  resource_group_name = azurerm_resource_group.scraper_test.name
  partition_count     = 2
  message_retention   = 1
}

resource "azurerm_eventhub_authorization_rule" "scraper_rule" {
  name                = "test"
  namespace_name      = azurerm_eventhub_namespace.scraper_event_hub.name
  eventhub_name       = azurerm_eventhub.scraper_hub.name
  resource_group_name = azurerm_resource_group.scraper_test.name

  listen              = true
  send                = true
}

resource "azurerm_eventhub_consumer_group" "scraper_consumer" {
  name                = "scraper-consumer"
  namespace_name      = azurerm_eventhub_namespace.scraper_event_hub.name
  eventhub_name       = azurerm_eventhub.scraper_hub.name
  resource_group_name = azurerm_resource_group.scraper_test.name
}