# Web Scraper Pipeline
## Setup Environment
Run the following line in order in the infrastructure folder:

1. az login
2. run the setup.sh script to create the account storage for Terraform
3. terrafrom init
4. terraform plan 
5. terraform apply  -var backend_subscription_id=<subscription id>
6. run output.ps1
 
## Applications
  
1. Deply ScraperFunction to the time-triggerd-azure-functions resource
2. Deply DataServerFuntion to the http-triggerd-azure-functions resource
  
## How to use

The time trigger will scrape and send the data and event hub that will 
subsequential move it to a table storage.

The service function will read anything in the azure table storage and
present the data as a json object.

The service function is locked behind a token bearer scheme, to use the api
just generate any valid jwt token.
  
The current example api is running as:
https://http-triggerd-azure-functions.azurewebsites.net/api/books
