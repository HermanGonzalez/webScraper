az stream-analytics job stop --resource-group "dev-scraper-test" --name "bookstorageJob"

$key = az storage account keys list --account-name "scraperbackend" --query [0].value  -o tsv

$jsonBase = @{ "type" = "Microsoft.Storage/Table"; }
$properties = @{ "accountName" = "scraperbackend";
         "table" = "books";
         "partitionKey" = "id";
         "rowKey" = "id";
         "batchSize" = 100;
}

$properties.Add("accountKey", $key)

$jsonBase.Add("properties", $properties)

$jsonBase | ConvertTo-Json -Depth 10 | Out-File ".\datasource.json"

az stream-analytics output create --resource-group "dev-scraper-test" --job-name "bookstorageJob" --name "books2" --datasource `@datasource.json --serialization `@serialization.json

az stream-analytics job start --resource-group "dev-scraper-test" --name "bookstorageJob"