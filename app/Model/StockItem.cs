namespace Model
{
    /// <summary>
    /// StockItem - used to pass on items inside the Control.cs
    /// Also used to display stock items inside the datagrid
    /// </summary>
    public class StockItem
    {
        /// <summary>
        /// The unique Id of the stock item
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Quantity of items are in stock for that StockItem.
        /// For example stock item is Table, and 20 is InStock.
        /// So there are 20 tables
        /// </summary>
        public int InStock { get; set; }
        /// <summary>
        /// The name of the stock item.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The price of each stock item. (price of 1 stock item)
        /// </summary>
        public double Price { get; set; }
    }
}