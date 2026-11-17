using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeNChill.Functions.DTOs
{
    public class CreateMenuItemRequest
    {
        //Category of the menu item (used as PartitionKey)
        public string Category {  get; set; } = string.Empty;

        //Unique Stock Keeping Unit (used as Row Key)
        public string SKU { get; set; } = string.Empty;

        //Name of menu item
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

    }
}
