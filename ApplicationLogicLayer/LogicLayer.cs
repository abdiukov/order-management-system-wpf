using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Domain;

namespace ApplicationLogicLayer
{
    public class LogicLayer
    {
        private readonly Control control;
        /// <summary>
        /// The consturctor links to the Control.cs in the Data Access Layer
        /// </summary>
        public LogicLayer()
        {
            control = new Control();
        }


        /// <summary>
        /// Retrieves the order headers and sorts them
        /// Sorting is done to make sure that each order header is unique, because some of the orderHeaders might get repeated otherwise.
        /// Try catch is to catch errors, as if there are 0 items in the database, the program crashes. That is prevented with try catch.
        /// </summary>
        /// <returns>List of sorted order headers</returns>
        public List<OrderHeader> GetOrderHeaders()
        {
            List<OrderHeader> sorted_list = new List<OrderHeader>();
            try
            {
                List<OrderHeader> unsorted_list = control.GetOrderHeaders();
                sorted_list.Add(unsorted_list[0]);
                for (int i = 1; i < unsorted_list.Count; i++)
                {
                    if (unsorted_list[i].Id != unsorted_list[i - 1].Id)
                    {
                        sorted_list.Add(unsorted_list[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured in the GetOrderHeaders()" +
                "\nThis error most likely occured because there are no order headers AT ALL inside the database\n" +
                ex);

            };
            return sorted_list;
        }

        /// <summary>
        /// Creates a new order header
        /// Also returns the ID of the new order header created.
        /// </summary>
        /// <returns>The OrderHeader ID of the new order header created</returns>
        public int CreateNewOrderHeader()
        {
            int output = control.InsertOrderHeader();
            return output;
        }


        /// <summary>
        /// Inserts a new order state into the database
        /// Updates the stock item amount. 
        /// So for example, if 5 items were added to the order header, then these 5 items are deducted from the stock item amount.
        /// If the person tries to order more than in stock (which is checked by looking at Description), then the stock item amount does not get changed.
        /// However, the person would not be able to submit order which has bigger quantity than the amount in stock anyway. 
        /// </summary>
        /// <param name="Description">The description of the stock item. In this case, it is either In_stock or Not_in_stock</param>
        /// <param name="Price">The price of the stock item</param>
        /// <param name="orderHeaderId">The id of the order header</param>
        /// <param name="stockItemId">The id of the stock item</param>
        /// <param name="quantity">The quantity of the sotck item</param>
        public void UpsertOrderItem(string Description, double Price, int orderHeaderId, int stockItemId, int quantity)
        {

            control.UpsertOrderItem(Description, Price, orderHeaderId, stockItemId, quantity);

            //because we are increasing by quantity amount, the number has to be negative
            quantity *= -1;

            switch (Description)
            {
                case "Not_in_stock":
                    //If the item is not in stock, it does not get updated 
                    break;
                default:
                    control.UpdateStockItemAmount(stockItemId, quantity);
                    break;
            }
        }


        /// <summary>
        /// Inputs the orderHeader id and gives back all the order items associated with that ID
        /// </summary>
        /// <param name="OrderHeaderId">The OrderHeaderId where to get the List OrderItem from</param>
        /// <returns>List of OrderItems that correspond to that ID</returns>
        public List<OrderItem> ProcessOrder(int OrderHeaderId)
        {
            return control.GetOrderHeader(OrderHeaderId);
        }

        /// <summary>
        /// The order gets submitted. 
        /// The OrderHeader gets its state changed.
        /// </summary>
        /// <param name="Id">The order header id</param>
        /// <param name="State">The new order header state</param>
        public void SubmitOrder(int Id, int State)
        {
            control.UpdateOrderState(Id, State);
        }

        /// <summary>
        /// DELETES THE ORDER HEADER AND ALL THE ITEMS ORDERED
        /// Gets all the OrderItems linked to the order header by using orderHeaderId
        /// Afterwards it updates the stock item amount, it returns the items taken back to the StockItem database.
        /// e.g if a person took 5 chairs to his order and then deleted the order, the 5 chairs go back to the StockItem table and can be ordered again.
        /// However if the order was error (description is "Not_in_stock"), then the items do not get returned
        /// After each item has  been returned, the order header and all items get removed from the database.
        /// </summary>
        /// <param name="orderHeaderId">The ID of the order header</param>
        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            List<OrderItem> toDelete = control.GetOrderHeader(orderHeaderId);

            for (int i = 0; i < toDelete.Count; i++)
            {
                switch (toDelete[i].Description)
                {
                    case "Not_in_stock":
                        //If the item is not in stock, it does not get updated 
                        break;
                    default:
                        control.UpdateStockItemAmount(toDelete[i].StockItemId, toDelete[i].Quantity);
                        break;
                }
            }
            control.DeleteOrderHeaderAndOrderItems(orderHeaderId);
        }


        /// <summary>
        /// DELETES THE item from the order header and returns the item back to the StockItem database.
        /// Afterwards it updates the stock item amount, it returns the items taken back to the StockItem database.
        /// e.g if a person took 5 chairs to his order and then deleted the order, the 5 chairs go back to the StockItem table and can be ordered again.
        /// However if the order was error (description is "Not_in_stock"), then the items do not get returned
        /// Afterwards, the item gets removed from the order header
        /// </summary>
        /// <param name="orderHeaderId">The id of the order header where the item is</param>
        /// <param name="stockItemId">The id of the stock item</param>
        /// <param name="quantity">The quantity to return</param>
        /// <param name="description">The description of the item (can be "Not_in_stock" or "In_stock")</param>


        public void DeleteOrderItem(int orderHeaderId, int stockItemId, int quantity, string description)
        {
            switch (description)
            {
                case "Not_in_stock":
                    //If the item is not in stock, it does not get updated 
                    break;
                default:
                    control.UpdateStockItemAmount(stockItemId, quantity);
                    break;
            }
            control.DeleteOrderItem(orderHeaderId, stockItemId);
        }

        /// <summary>
        /// Gets all the stock items
        /// </summary>
        /// <returns>List of stock items</returns>
        public IEnumerable<StockItem> GetStockItems()
        {
            return control.GetStockItems();
        }
    }
}
