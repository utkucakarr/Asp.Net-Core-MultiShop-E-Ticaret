using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MultiShop.Catalog.Exceptions;

namespace MultiShop.Catalog.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; private set; }

        public string ProductName { get; private set; }

        public decimal ProductPrice { get; private set; }

        public string? ProductImageUrl { get; private set; }

        public string? ProductDescription { get; private set; }

        public string CategoryId { get; private set; }

        // --- STOK YÖNETİMİ ALANLARI ---
        // Set işlemi dışarıya kapatıldı (private set)
        public int AvailableStock { get; private set; }

        public int RestockThreshold { get; private set; } // Minimum güvenli stok seviyesi

        public int MaxStockThreshold { get; private set; } // Depoda tutulacak maksimum stok miktarıdır.

        public bool OnReorder { get; private set; }

        [BsonIgnore]
        public Category Category { get; set; }

        public Product(string productName, decimal productPrice, string categoryId, int initialStock, int restockThreshold, int maxStockThreshold, string? productDescription = null, string? productImageUrl = null)
        {
            ProductId = ObjectId.GenerateNewId().ToString();
            ProductName = productName;
            ProductPrice = productPrice;
            CategoryId = categoryId;
            AvailableStock = initialStock;
            RestockThreshold = restockThreshold;
            MaxStockThreshold = maxStockThreshold;
            OnReorder = false;

            ProductDescription = productDescription;
            ProductImageUrl = productImageUrl;
        }

        public int RemoveStock(int quantityDesired)
        {
            if(AvailableStock == 0)
            {
                throw new CatalogDomainException($"Empty stock, product item {ProductName} is sold out");
            }

            if(quantityDesired <= 0)
            {
                throw new CatalogDomainException($"Item units desired should be greater than zero");
            }

            int removed = Math.Min(quantityDesired, this.AvailableStock);

            this.AvailableStock -= removed;

            return removed;
        }

        public int AddStock(int quantity)
        {
            int original = this.AvailableStock;

            if((this.AvailableStock + quantity) > this.MaxStockThreshold)
            {
                this.AvailableStock += (this.MaxStockThreshold - this.AvailableStock);
            }
            else
            {
                this.AvailableStock += quantity;
            }

            this.OnReorder = false;
            return this.AvailableStock - original;
        }

        public void UpdateProductDetails(string? description, string? imageUrl)
        {
            // İleride buraya kural ekleyebilirsin: 
            // Örneğin "description boş olamaz" veya "imageUrl http ile başlamalı" gibi.

            ProductDescription = description;
            ProductImageUrl = imageUrl;
        }
    }
}
