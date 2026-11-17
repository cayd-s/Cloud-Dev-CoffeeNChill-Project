using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeNChill.Functions.DTOs
{
    public class MenuItemResponseDto
    {
        public string Category { get; set; } = string.Empty;

        public string SKU { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
    }
}
