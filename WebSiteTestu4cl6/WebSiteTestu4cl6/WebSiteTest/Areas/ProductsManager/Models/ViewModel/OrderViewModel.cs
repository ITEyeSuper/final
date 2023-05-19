namespace WebSiteTest.Areas.ProductsManager.Models.ViewModel
{
    public class OrderViewModel
    {
        //Orders
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public DateTime OrderOnDate { get; set; }
        public DateTime ShipDate { get; set; }
        public string OrderStatus { get; set; }
        public string ShipAddress { get; set; }
        public string ShippingMethod { get; set; }

        //products
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductUnitPrice { get; set; }
        public int ProductQty { get; set; }
        public string ProductCategory { get; set; }



    }
}
