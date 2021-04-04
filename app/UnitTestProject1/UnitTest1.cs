using Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private int orderHeaderID;
        private LogicLayer layer;
        private int quantity_of_stockitem_tested;
        private int id_of_stockitem_tested;

        /// <summary>
        /// Connecting to the logic layer
        /// The quantity of the stock item that will be added/removed is 2
        /// The id of the stock item that will be added/removed is 1
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            LogicLayer layer = new LogicLayer();
            this.layer = layer;
            quantity_of_stockitem_tested = 2;
            id_of_stockitem_tested = 1;
        }

        /// <summary>
        /// Creating an order header
        /// Retrieving the price of the stock item that is going to be tested
        /// Creating an object "input" which contains information about stock item
        /// Inserting that input object into the order header
        /// Checking that the output object is same as input object (additionally checking that the total is correct)
        /// </summary>


        [TestMethod]
        public void TestOne()
        {

            int orderHeaderID = layer.CreateNewOrderHeader();
            this.orderHeaderID = orderHeaderID;

            IEnumerable<StockItem> allItems = layer.GetStockItems();

            double Price = allItems.ElementAt(0).Price;

            OrderItem input = new OrderItem("In_stock", orderHeaderID, Price,
                quantity_of_stockitem_tested, id_of_stockitem_tested, (Price * quantity_of_stockitem_tested));

            layer.UpsertOrderItem(input.Description, input.Price, input.OrderHeaderId,
                input.StockItemId, input.Quantity);

            OrderItem output = layer.ProcessOrder(orderHeaderID).ElementAt(0);

            //create an order header object and compare it 

            Assert.AreEqual(input.Description + " " + input.Price + " " + input.OrderHeaderId +
                " " + input.StockItemId + " " + input.Quantity + " " + input.Total,
                output.Description + " " + output.Price + " " + output.OrderHeaderId + " " +
                output.StockItemId + " " + output.Quantity + " " + output.Total);
        }


        /// <summary>
        /// Opening the order that only has one item
        /// Checking that that item with index 0 exists
        /// Deleting the item
        /// Checking that the item with index 0 does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestTwo()
        {
            bool item_has_been_retrieved = false;
            layer.DeleteOrderItem(orderHeaderID, id_of_stockitem_tested, quantity_of_stockitem_tested, "In_stock");

            try
            {
                layer.ProcessOrder(orderHeaderID).ElementAt(0);
                item_has_been_retrieved = true;
            }
            catch (Exception) { };

            Assert.AreEqual(item_has_been_retrieved, false);

        }






    }
}
