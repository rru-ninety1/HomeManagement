﻿namespace HomeManagement.Client.Features.Catalog.ProductCategories.Models
{
    public record class ProductCategory
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
