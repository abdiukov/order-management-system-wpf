using Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomatedTesting
{
    [TestClass]
    public class Test
    {
        private int orderHeaderId;
        private LogicLayer layer;
        private int quantityOfStockItemTested;
        private int idOfStockItemTested;

        /// <summary>
        /// Connecting to the logic layer
        /// The quantity of the stock item that will be added/removed is 2
        /// The id of the stock item that will be added/removed is 1
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            this.layer = new LogicLayer();
            quantityOfStockItemTested = 2;
            idOfStockItemTested = 1;
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
            this.orderHeaderId = layer.CreateNewOrderHeader();

            IEnumerable<StockItem> allItems = layer.GetStockItems();

            double price = allItems.ElementAt(0).Price;

            OrderItem input = new OrderItem("In_stock", orderHeaderId, price,
                quantityOfStockItemTested, idOfStockItemTested, price * quantityOfStockItemTested);

            layer.UpsertOrderItem(input.Description, input.Price, input.OrderHeaderId,
                input.StockItemId, input.Quantity);

            OrderItem output = layer.ProcessOrder(orderHeaderId).ElementAt(0);

            //checking that the input and output are equal
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
        public void TestTwo()
        {
            bool itemExists = false;
            layer.DeleteOrderItem(orderHeaderId, idOfStockItemTested,
                quantityOfStockItemTested, "In_stock");
            try
            {
                layer.ProcessOrder(orderHeaderId).ElementAt(0);
                itemExists = true;
            }
            catch (Exception) { };
            Assert.AreEqual(itemExists, false);
        }

    }
}