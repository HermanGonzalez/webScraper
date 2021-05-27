resource "azurerm_stream_analytics_job" "analytics_job" {
  name                                     = "bookstorageJob"
  location            					   = azurerm_resource_group.scraper_test.location
  resource_group_name 					   = azurerm_resource_group.scraper_test.name
  compatibility_level                      = "1.1"
  data_locale                              = "en-GB"
  events_late_arrival_max_delay_in_seconds = 60
  events_out_of_order_max_delay_in_seconds = 50
  events_out_of_order_policy               = "Adjust"
  output_error_policy                      = "Drop"
  streaming_units                          = 3

  transformation_query = <<QUERY
    SELECT *
    INTO books
    FROM input
	QUERY

}


resource "azurerm_stream_analytics_stream_input_eventhub" "device_telemetry_eventhub" {
  name                         = "input"
  stream_analytics_job_name    = azurerm_stream_analytics_job.analytics_job.name
  resource_group_name          = azurerm_stream_analytics_job.analytics_job.resource_group_name
  eventhub_consumer_group_name = azurerm_eventhub_consumer_group.scraper_consumer.name
  eventhub_name                = azurerm_eventhub_authorization_rule.scraper_rule.eventhub_name
  servicebus_namespace         = azurerm_eventhub_authorization_rule.scraper_rule.namespace_name
  shared_access_policy_key     = azurerm_eventhub_authorization_rule.scraper_rule.primary_key
  shared_access_policy_name    = azurerm_eventhub_authorization_rule.scraper_rule.name

  serialization {
    type     = "Json"
    encoding = "UTF8"
  }
}