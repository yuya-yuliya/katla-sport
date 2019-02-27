namespace KatlaSport.DataAccess.ProductStore
{
    public class ProductStoreItemRequest
    {
        public int Id { get; set; }

        public int StoreItemId { get; set; }

        public virtual StoreItem Item { get; set; }

        public int Quantity { get; set; }

        public bool Completed { get; set; }
    }
}