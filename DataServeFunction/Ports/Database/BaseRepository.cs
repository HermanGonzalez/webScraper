using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataServeFunction.Ports.Database
{
    public class BaseRepository<TEntity> where TEntity : TableEntity, new()
    {
        protected CloudTable Table;

        public BaseRepository(BooksStorageContext context, string table) {
            var tableClient = context.storageAccount.CreateCloudTableClient();
            Table = tableClient.GetTableReference(table);
        }

        public async Task<IActionResult> GetAll() 
        {
            List<TEntity> result = new List<TEntity>();
            var query = new TableQuery<TEntity>();

            TableContinuationToken continuationToken = null;
            
            do
            {
                TableQuerySegment<TEntity> tableQueryResult = await Table.ExecuteQuerySegmentedAsync(query, continuationToken);

                result.AddRange(tableQueryResult.Results);

                continuationToken = tableQueryResult.ContinuationToken;
            } while (continuationToken != null);

            return new OkObjectResult(result);
        }
    }
}
