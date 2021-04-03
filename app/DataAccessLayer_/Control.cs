using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class Control
    {
        readonly public string _connectionString;
        /// <summary>
        /// Constructor for Control class
        /// Connects to the Repository class and sets up the connection string
        /// </summary>
        public Control()
        {

            Repository conn_string = new Repository();
            _connectionString = conn_string._connectionString;

        }

        /// <summary>
        /// Creates a new order header and retrieves the ID of the new order header
        /// </summary>
        /// <returns>OrderHeader unique ID </returns>
        public int InsertOrderHeader()
        {
            int order_headerID = -9999;
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);

                SqlCommand cmd = new SqlCommand("exec sp_InsertOrderHeader", conn);

                conn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();

                order_headerID = Convert.ToInt32(dataReader.GetDecimal(0));

                //disposing
                conn.Dispose();
                cmd.Dispose();

                //return 

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured at InsertOrderHeader()\n" + ex);
            }
            return order_headerID;
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
            List<OrderItem> outputlist = new List<OrderItem>();
            SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand("exec sp_SelectOrderHeaderById " + id, conn);

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

                        OrderItem output = new OrderItem(dataReader.GetString(4), dataReader.GetInt32(0), (double)dataReader.GetDecimal(5), dataReader.GetInt32(6), dataReader.GetInt32(3), total);
                        outputlist.Add(output);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured in the GetOrderHeader()" +
                    "\nThis error most likely occured because there are no order headers that correspond to ID passed\n" +
                    ex);
            }

            //disposing
            conn.Dispose();
            cmd.Dispose();

            //output
            return outputlist;
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
                SqlConnection conn = new SqlConnection(_connectionString);

                //Execute query
                conn.Open();
                SqlCommand cmd = new SqlCommand("exec sp_SelectOrderHeaders", conn);
                SqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        OrderHeader output = new OrderHeader(dataReader.GetDateTime(2), dataReader.GetInt32(0), dataReader.GetInt32(1));
                        outputlist.Add(output);
                    }
                }
                //disposing
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured at the GetOrderHeaders\n" + ex);
            }
            //output
            return outputlist;
        }



        /// <summary>
        /// Inserts a new OrderItem
        /// </summary>
        /// <param name="Description">Description of the item. Can either be "In_stock" or "Not_in_stock"</param>
        /// <param name="Price">The price of the item.</param>
        /// <param name="orderHeaderId">The id of the order header where the order item is to be inserted into</param>
        /// <param name="stockItemId">The id of the stock item to be inserted</param>
        /// <param name="quantity">The quantity of the order item</param>
        public void UpsertOrderItem(string Description, double Price, int orderHeaderId, int stockItemId, int quantity)
        {
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);

                SqlCommand cmd = new SqlCommand
                    ("exec sp_UpsertOrderItem " + orderHeaderId + ", " + stockItemId + ", " +
                    Description + ", " + Price + ", " + quantity, conn);

                //Execute query
                conn.Open();
                cmd.ExecuteNonQuery();

                //disposing
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured at the UpserOrderItem()\n" + ex);
            }

        }


        /// <summary>
        /// Updates the order state of the current order
        /// </summary>
        /// <param name="Id">Id of the Order Header</param>
        /// <param name="State">State that needs to be updated to</param>
        public void UpdateOrderState(int Id, int State)
        {

            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);

                SqlCommand cmd = new SqlCommand("exec sp_UpdateOrderState " + Id + ", " + State, conn);

                //Execute query
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
                SqlConnection conn = new SqlConnection(_connectionString);

                SqlCommand cmd = new SqlCommand("exec sp_DeleteOrderHeaderAndOrderItems " + orderHeaderId, conn);

                //Execute query
                conn.Open();
                cmd.ExecuteNonQuery();
                //disposing
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured at the UpserOrderItem()\n" + ex);
            }
        }

        /// <summary>
        /// Deletes the ordered(stock) item
        /// </summary>
        /// <param name="OrderHeaderId">Order Header ID where the item is located</param>
        /// <param name="StockItemId">The ID of the stock item that needs to be deleted</param>
        public void DeleteOrderItem(int OrderHeaderId, int StockItemId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);

                SqlCommand cmd = new SqlCommand("exec sp_DeleteOrderItem " + OrderHeaderId + " , " + StockItemId, conn);

                //Execute query
                conn.Open();
                cmd.ExecuteNonQuery();
                //disposing
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured at the DeleteOrderItem()\n" + ex);
            }
        }


        /// <summary>
        /// Retrieves all the stock items in the database.
        /// This is used to display the stock items inside AddOrderItem_Window
        /// </summary>
        /// <returns>A list of all the stock items</returns>
        public IEnumerable<StockItem> GetStockItems()
        {
            var output = new List<StockItem>();

            try
            {

                SqlConnection conn = new SqlConnection(_connectionString);

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
                Console.WriteLine("An error has occured at the DeleteOrderItem()\n" + ex);
            }
            return output;
        }

        /// <summary>
        /// GetStockItem does not get used in this program, however this component is fully functional.
        /// Retrieves the stock item of the specific id
        /// </summary>
        /// <param name="id">Id of stock item</param>
        /// <returns>Stock item of that Id</returns>
        public StockItem GetStockItem(int id)
        {
            StockItem retrieveStockItem;
            SqlConnection conn = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand("exec sp_SelectStockItemById " + id, conn);

            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            {

                retrieveStockItem = new StockItem
                {
                    Id = dataReader.GetInt32(0),
                    Name = dataReader.GetString(1),
                    Price = Convert.ToDouble(dataReader.GetDecimal(2)),
                    InStock = dataReader.GetInt32(3)
                };
            }

            //Execute query
            conn.Open();
            cmd.ExecuteNonQuery();

            //disposing
            conn.Dispose();
            cmd.Dispose();

            return retrieveStockItem;
        }


        /// <summary>
        /// Updates the amount of stock item
        /// This is useful when the person orders something, to reflect that this item is no longer available.
        /// Try catch is used in case the quantity amount cannot be changed
        /// </summary>
        /// <param name="stockItemID">The ID of the stock item</param>
        /// <param name="quantity">The quantity to be updated by</param>
        public void UpdateStockItemAmount(int stockItemID, int quantity)
        {
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);

                SqlCommand cmd = new SqlCommand("exec sp_UpdateStockItemAmount " + stockItemID + ", " + quantity, conn);
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
                    ex);

            };
        }


    }

}