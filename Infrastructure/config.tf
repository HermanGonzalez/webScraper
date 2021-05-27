terraform {
	backend "azurerm" { 
		resource_group_name      = "scraper-test"
		storage_account_name     = "scrapestorage"
		container_name = "terraform-state"
		key            = "terraform.tfstate"
	}

	required_providers {
		azurerm = {
			source = "hashicorp/azurerm"
			version = ">=2.34.0"
		}	
	}
	
	
}

provider "azurerm" {
  features {}
  subscription_id = var.backend_subscription_id
}