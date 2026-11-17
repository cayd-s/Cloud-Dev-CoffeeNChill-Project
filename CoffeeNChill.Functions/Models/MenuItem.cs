using System;
using System.Collections.Generic;
using System.Text;
using Azure;
using Azure.Data.Tables;

namespace CoffeeNChill.Functions.Models
{
    public class MenuItem : ITableEntity 
    {
        //Azure Tbale storage partition key

        public string PartitionKey { get; set; } = string.Empty;

        //Azure Tbale storage partition key

        public string RowKey { get; set; } = string.Empty;

        //Menu item name

        public string Name { get; set; } = string.Empty;

        //Menu item description

        public string Description { get; set; } = string.Empty;

        //Selling price

        public decimal Price { get; set; }

        //Indicates whether the item is avaialable

        public bool IsAvailable { get; set; }

        //automatically maintained by azure table storage
        public DateTimeOffset? Timestamp { get; set; }

        //Entity Tag used used for concurrency

        public ETag ETag { get; set; }

    }
}
