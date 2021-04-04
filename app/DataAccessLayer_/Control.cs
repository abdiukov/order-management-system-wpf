using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class Control
    {
        readonly public string connectionString;
        /// <summary>
        /// Constructor for Control class
        /// Connects to the Repository class and sets up the connection string
        /// </summary>
        public Control()
        {
            Repository repository = new Repository();
            connectionString = repository.connectionString;
        }

        /// <summary>
        /// Creates a new order header and retrieves the ID of the new order header
        /// </summary>
        /// <returns>OrderHeader unique ID </returns>
        public int InsertOrderHeader()
        {
            int orderHeaderId = -9999;
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("exec sp_InsertOrderHeader", conn);

                conn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();

                orderHeaderId = Convert.ToInt32(dataReader.GetDecimal(0));

                //disposing
                conn.Dispose();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured at InsertOrderHeader()\n" + ex.Message);
            }
            return orderHeaderId;
        }


        /// <summary>
        /// Gets the list of OrderItems that are associated with specific OrderHeader
        /// try catch is used, as there may not be any order headers, which will result in error.
        /// Total is calculated by multiplying Quantity and Price and then is inserted into the object
        /// </summary>
        /// <param name="id">OrderHeader ID</param>
        /// <returns>OrderItems that are associated with specific OrderHeader</returns>
        public List<OrderItem> GetOrderHeader(int id)
        {
            List<OrderItem> outputList = new List<OrderItem>();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("exec sp_SelectOrderHeaderById @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            //Execute query
            conn.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();


            try
            {
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        double total = (double)(dataReader.GetDecimal(5) * dataReader.GetInt32(6));

                        OrderItem output = new OrderItem(dataReader.GetString(4), dataReader.GetInt32(0),
                            (double)dataReader.GetDecimal(5), dataReader.GetInt32(6), dataReader.GetInt32(3), total);
                        outputList.Add(output);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured in the GetOrderHeader()" +
                    "\nThis error most likely occured because there are no order headers that correspond to ID passed\n" +
                    ex.Message);
            }

            //disposing
            conn.Dispose();
            cmd.Dispose();

            //output
            return outputList;
        }


        /// <summary>
        /// Gets all the order headers
        /// </summary>
        /// <returns>List of OrderHeaders</returns>
        public List<OrderHeader> GetOrderHeaders()
        {
            List<OrderHeader> outputlist = new List<OrderHeader>();
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                //Execute query
                conn.Open();
                SqlCommand cmd = new SqlCommand("exec sp_SelectOrderHeaders", conn);
                SqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        OrderHeader output = new OrderHeader(dataReader.GetDateTime(2),
                            dataReader.GetInt32(0), dataReader.GetInt32(1));
                        outputlist.Add(output);
                    }
                }
                //disposing
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured at the GetOrderHeaders\n" + ex.Message);
            }
            //output
            return outputlist;
        }



        /// <summary>
        /// Inserts a new OrderItem
        /// </summary>
        /// <param name="description">Description of the item. Can either be "In_stock" or "Not_in_stock"</param>
        /// <param name="price">The price of the item.</param>
        /// <param name="orderHeaderId">The id of the order header where the order item is to be inserted into</param>
        /// <param name="stockItemId">The id of the stock item to be inserted</param>
        /// <param name="quantity">The quantity of the order item</param>
        public void UpsertOrderItem(string description, double price,
            int orderHeaderId, int stockItemId, int quantity)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand
                    ("exec sp_UpsertOrderItem @orderHeaderId, @stockItemId, @description, @price, @quantity", conn);
                cmd.Parameters.AddWithValue("@orderHeaderId", orderHeaderId);
                cmd.Parameters.AddWithValue("@stockItemId", stockItemId);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@quantity", quantity);

                //Execute query
                conn.Open();
                cmd.ExecuteNonQuery();

                //disposing
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured at the UpserOrderItem()\n" + ex.Message);
            }

        }


        /// <summary>
        /// Updates the order state of the current order
        /// </summary>
        /// <param name="orderHeaderId">Id of the Order Header</param>
        /// <param name="orderHeaderState">State that needs to be updated to</param>
        public void UpdateOrderState(int orderHeaderId, int orderHeaderState)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand
                    ("exec sp_UpdateOrderState @orderHeaderId, @stateId", conn);
                cmd.Parameters.AddWithValue("@orderHeaderId", orderHeaderId);
                cmd.Parameters.AddWithValue("@stateId", orderHeaderState);

                conn.Open();
                cmd.ExecuteNonQuery();
                //disposing
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured at the UpdateOrderItem()\n" + ex);
            }

        }

        /// <summary>
        /// Deletes all the order items and the order header
        /// </summary>
        /// <param name="orderHeaderId">The order header id to be deleted</param>
        public void DeleteOrderHeaderAndOrderItems(int orderHeaderId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("exec sp_DeleteOrderHeaderAndOrderItems @orderHeaderId", conn);
                cmd.Parameters.AddWithValue("@orderHeaderId", orderHeaderId);

                //Execute query
                conn.Open();
                cmd.ExecuteNonQuery();
                //disposing
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured at the UpserOrderItem()\n" + ex.Message);
            }
        }

        /// <summary>
        /// Deletes the ordered(stock) item
        /// </summary>
        /// <param name="orderHeaderId">Order Header ID where the item is located</param>
        /// <param name="stockItemId">The ID of the stock item that needs to be deleted</param>
        public void DeleteOrderItem(int orderHeaderId, int stockItemId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("exec sp_DeleteOrderItem @orderHeaderId, @stockItemId", conn);
                cmd.Parameters.AddWithValue("@orderHeaderId", orderHeaderId);
                cmd.Parameters.AddWithValue("@stockItemId", stockItemId);
                //Execute query
                conn.Open();
                cmd.ExecuteNonQuery();
                //disposing
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured at the DeleteOrderItem()\n" + ex.Message);
            }
        }


        /// <summary>
        /// Retrieves all the stock items in the database.
        /// This is used to display the stock items inside AddOrderItem_Window
        /// </summary>
        /// <returns>A list of all the stock items</returns>
        public IEnumerable<StockItem> GetStockItems()
        {
            List<StockItem> output = new List<StockItem>();
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("exec sp_SelectStockItems ", conn);
                conn.Open();

                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {

                        StockItem stockItem = new StockItem
                        {
                            Id = dataReader.GetInt32(0),
                            Name = dataReader.GetString(1),
                            Price = Convert.ToDouble(dataReader.GetDecimal(2)),
                            InStock = dataReader.GetInt32(3)
                        };
                        output.Add(stockItem);
                    }
                }

                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured at the DeleteOrderItem()\n" + ex.Message);
            }
            return output;
        }

        /// <summary>
        /// Updates the amount of stock item
        /// This is useful when the person orders something, to reflect that this item is no longer available.
        /// Try catch is used in case the quantity amount cannot be changed
        /// </summary>
        /// <param name="stockItemId">The ID of the stock item</param>
        /// <param name="quantity">The quantity to be updated by</param>
        public void UpdateStockItemAmount(int stockItemId, int quantity)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand("exec sp_UpdateStockItemAmount @id, @amount", conn);
                cmd.Parameters.AddWithValue("@id", stockItemId);
                cmd.Parameters.AddWithValue("@amount", quantity);
                //Execute query
                conn.Open();
                cmd.ExecuteNonQuery();
                //disposing
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured in the UpdateStockItemAmount()" +
                    "\nThis error most likely occured because the quantity of stock item cannot be updated successfully\n" +
                    ex.Message);

            };
        }


    }

}