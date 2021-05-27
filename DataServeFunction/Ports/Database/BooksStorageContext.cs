using DataServeFunction.Configuration;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataServeFunction.Ports.Database
{
    public class BooksStorageContext
    {
        public CloudStorageAccount _storageAccount { get; set; }
        public CloudStorageAccount storageAccount { 
            get {
                if (_storageAccount == null)
                    _storageAccount = CreateStorageAccountFromConnectionString();

                return _storageAccount;
            }  
        }

        private readonly string _storageConnectionString;

        public BooksStorageContext(ServiceConfiguration configuration) 
        {
            _storageConnectionString = configuration.StorageAccountConnectionString;
        }

        private CloudStorageAccount CreateStorageAccountFromConnectionString()
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }
    }
}
