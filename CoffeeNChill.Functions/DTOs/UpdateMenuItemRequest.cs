using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeNChill.Functions.DTOs
{
    public class UpdateMenuItemRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
    }
}
