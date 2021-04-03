namespace Domain
{
    public class OrderItem
    {
        /// <summary>
        /// Description of the Order Item
        /// Can be "Not_in_stock" or "In_stock"
        /// Description is used to determine whether the item is in stock
        /// If the item is not in stock, it does get added to the OrderHeader, however the Stock Item table does not get updated
        /// So if we add quantity of 12 to the order, when the stock quantity is 1, the Stock Item table does not become -11
        /// Vice versa happens when we remove an order that is not in stock - the Stock Item table does not get updated
        /// </summary>
        public string Description
        {
            get; set;
        }


        /// <summary>
        /// The unique ID of the order header
        /// </summary>
        public int OrderHeaderId
        {
            get; set;
        }

        /// <summary>
        /// The Price of the order item
        /// </summary>
        public double Price
        {
            get; set;
        }

        /// <summary>
        /// The quantity of the order item
        /// </summary>
        public int Quantity
        {
            get; set;
        }

        /// <summary>
        /// The id of the stock itme
        /// </summary>
        public int StockItemId
        {
            get; set;
        }

        /// <summary>
        /// The total calculated by = quantity * price
        /// </summary>
        public double Total
        {
            get; set;
        }


        /// <summary>
        /// Constructor for the order item class
        /// </summary>
        /// <param name="Description">Description of the Order Item. Can be "Not_in_stock" or "In_stock" </param>
        /// <param name="OrderHeaderId">The unique ID of the order header</param>
        /// <param name="Price">The Price of the order item</param>
        /// <param name="Quantity">The quantity of the order item</param>
        /// <param name="StockItemId">The id of the stock itme</param>
        /// <param name="Total">The total calculated by = quantity * price</param>
        public OrderItem(string Description, int OrderHeaderId, double Price, int Quantity, int StockItemId, double Total)
        {
            this.Description = Description;
            this.Quantity = Quantity;
            this.OrderHeaderId = OrderHeaderId;
            this.Price = Price;
            this.StockItemId = StockItemId;
            this.Total = Total;
        }


        //OrderHeader was not used, I am not sure why and when it should have been used

        //public string OrderHeader
        //{
        //    get;set;
        //}
    }
}
