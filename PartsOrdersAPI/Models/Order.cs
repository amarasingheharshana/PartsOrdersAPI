namespace PartsOrdersAPI.Models
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public List<OrderLineItem> LineItems { get; set; } = new List<OrderLineItem>();
        public decimal TotalCost => LineItems.Sum(item => item.Total);
    }

    public class OrderLineItem
    {
        public int PartId { get; set; }
        public string PartDescription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }

    public class OrderRequest
    {
        public int PartId { get; set; }
        public int Quantity { get; set; }
    }
}
