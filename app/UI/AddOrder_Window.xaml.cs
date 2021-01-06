using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ApplicationLogicLayer;
using Domain;

namespace UI
{
    /// <summary>
    /// Interaction logic for AddOrder_Window.xaml
    /// </summary>
    public partial class AddOrder_Window : Window
    {
        private LogicLayer layer;
        private OrderHeader currentOrderHeader;
        private List<OrderItem> current_orders;
        private readonly int OrderHeaderID;



        /// <summary>
        /// Constructor for the class
        /// Assigns the orderheaderID
        /// Afterwards calls the function to update all the information on the page
        /// </summary>
        /// <param name="OrderHeaderID">The ID of the orderheaderID</param>
        public AddOrder_Window(int OrderHeaderID)
        {
            InitializeComponent();
            this.OrderHeaderID = OrderHeaderID;
            UpdateInfo();
        }


        /// <summary>
        /// UPDATES EVERYTHING!!!
        /// The most important function. here is what it does:
        /// 1. Assigns the orderheader object to the order header id
        /// 2. If an orderheader(from 1. ) is not found, then a new one is created
        /// 3. Hides "Add" button and "Submit" button if order state is "Complete".
        /// 4. Displays the order items inside the datagrid
        /// 5. Adds each order item price and calculates the total
        /// 6. Displays all the information inside textboxes.
        /// </summary>
        public void UpdateInfo()
        {
            layer = new LogicLayer();
            bool exit_loop = false;
            List<OrderHeader> allOrders = layer.GetOrderHeaders();

            //1. Assigns the orderheader object to the order header id
            for (int i = 0; i < allOrders.Count && exit_loop == false; i++)
            {
                if (allOrders.ElementAt(i).Id == OrderHeaderID)
                {
                    DateTime time_of_order = allOrders.ElementAt(i).DateTime;
                    int orderHeaderState = allOrders.ElementAt(i).State;
                    currentOrderHeader = new OrderHeader(time_of_order, OrderHeaderID, orderHeaderState);
                    exit_loop = true;
                }
            }
            // 2. If an orderheader(from 1. ) is not found, then a new one is created

            switch (exit_loop)
            {
                case false:
                    DateTime time_of_order = DateTime.Now;
                    int orderHeaderState = (int)Enum.Parse<OrderStates>("New");
                    currentOrderHeader = new OrderHeader(time_of_order, OrderHeaderID, orderHeaderState);
                    break;
            }

            // 3. Hides "Add" button and "Submit" button if order state is "Complete".
            if (currentOrderHeader.State == (int)Enum.Parse<OrderStates>("Complete"))
            {
                Btn_Add_Order.Visibility = Visibility.Hidden;
                Btn_Submit.Visibility = Visibility.Hidden;
            }

            // 4. Displays the order items inside the datagrid
            current_orders = layer.ProcessOrder(currentOrderHeader.Id);
            dgOrderItem.ItemsSource = current_orders;

            // 5. Adds each order item price and calculates the total
            double total = 0;
            for (int i = 0; i < current_orders.Count; i++)
            {
                total += current_orders.ElementAt(i).Total;
            }

            // 6. Displays all the information inside textboxes.
            textbox_order.Text = "Order : #" + OrderHeaderID;
            OrderStates current_state = (OrderStates)currentOrderHeader.State;

            textbox_datetime.Text = "Order Time : " + currentOrderHeader.DateTime;
            textbox_state.Text = "State : " + current_state;
            textbox_total.Text = "Total : " + String.Format("{0:c}", total);
        }

        /// <summary>
        /// Calls function that navigates back to main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MainPage_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack();
        }

        /// <summary>
        /// Navigates back to main window and closes the current page
        /// </summary>
        private void NavigateBack()
        {
            MainWindow pageobj = new MainWindow();
            pageobj.Show();
            Close();
        }


        /// <summary>
        /// Adds an order item to the order header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Add_Order_Click(object sender, RoutedEventArgs e)
        {
            AddOrderItem_Window add_order = new AddOrderItem_Window(currentOrderHeader.Id);
            add_order.Show();
            Close();
        }

        /// <summary>
        /// Deletes the order item from the order header
        /// If the order state is "Complete" then the button would not work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (currentOrderHeader.State != (int)Enum.Parse<OrderStates>("Complete"))
            {
                int item_toBeDeleted_ID = current_orders.ElementAt(dgOrderItem.SelectedIndex).StockItemId;
                int item_toBeDeleted_Quantity = current_orders.ElementAt(dgOrderItem.SelectedIndex).Quantity;
                string item_toBeDeleted_Description = current_orders.ElementAt(dgOrderItem.SelectedIndex).Description;

                layer.DeleteOrderItem(currentOrderHeader.Id, item_toBeDeleted_ID, item_toBeDeleted_Quantity, item_toBeDeleted_Description);
                UpdateInfo();
            }
        }

        /// <summary>
        /// Upon clicking the submit button, the order state gets updated
        /// If there is a single item that is not in stock OR there is less than 1 item, then the order gets automatically rejected
        /// If all items are in stock the order becomes "Pending" and then (after clicking the button again) gets submitted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Submit_Click(object sender, RoutedEventArgs e)
        {

            OrderStates comparison = (OrderStates)currentOrderHeader.State;


            //check whether there is a single item that is not in stock
            bool allItems_inStock = true;
            for (int i = 0; i < current_orders.Count; i++)
            {
                switch (current_orders[i].Description)
                {
                    case "Not_in_stock":
                        allItems_inStock = false;
                        break;
                }
            }

            //if there is a single item that is not in stock OR there is less than 1 item, then the order gets automatically rejected
            if ((allItems_inStock == false || current_orders.Count < 1) && comparison.ToString() != "New")
            {
                currentOrderHeader.State = (int)Enum.Parse<OrderStates>("Rejected");
            }
            else
            {
                switch (comparison.ToString())
                {
                    case "New":
                    case "Rejected":
                        currentOrderHeader.State = (int)Enum.Parse<OrderStates>("Pending");
                        break;
                    default:
                        currentOrderHeader.State = (int)Enum.Parse<OrderStates>("Complete");
                        break;
                }
            }
            //submitting the state after it has been determined.
            layer.SubmitOrder(currentOrderHeader.Id, currentOrderHeader.State);
            UpdateInfo();
        }


        /// <summary>
        /// Upon clicking on the "Cancel" button, the order header and all the order items get erased from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            layer.DeleteOrderHeaderAndOrderItems(currentOrderHeader.Id);
            NavigateBack();
        }
    }
}
