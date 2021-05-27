resource "azurerm_storage_account" "scraper_backend" {
  name                     = "scraperbackend"
  resource_group_name      = azurerm_resource_group.scraper_test.name
  location                 = azurerm_resource_group.scraper_test.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_table" "table_books" {
  name                 = "books"
  storage_account_name = azurerm_storage_account.scraper_backend.name
}