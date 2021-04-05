using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace View
{
    /// <summary>
    /// Interaction logic for AddOrderView.xaml
    /// </summary>
    public partial class AddOrderView : Window
    {
        private readonly LogicLayer layer;
        private OrderHeader currentOrderHeader;
        private List<OrderItem> currentOrders;
        private readonly int orderHeaderId;

        /// <summary>
        /// Constructor for the class
        /// Assigns the orderheaderID
        /// Afterwards calls the function to update all the information on the page
        /// </summary>
        /// <param name="OrderHeaderId">The ID of the orderheaderID</param>
        public AddOrderView(int OrderHeaderId)
        {
            InitializeComponent();
            this.orderHeaderId = OrderHeaderId;
            layer = new LogicLayer();
            UpdateInfo();
        }

        /// <summary>
        /// 1. Assigns the orderheader object to the order header id
        /// 2. If an orderheader(from 1. ) is not found, then a new one is created
        /// 3. Hides "Add" button and "Submit" button if order state is "Complete".
        /// 4. Displays the order items inside the datagrid
        /// 5. Adds each order item price and calculates the total
        /// 6. Displays all the information inside textboxes.
        /// </summary>
        public void UpdateInfo()
        {
            bool exitLoop = false;
            List<OrderHeader> allOrders = layer.GetOrderHeaders();
            DateTime orderTime;
            int orderHeaderState;

            //1. Assigns the orderheader object to the order header id
            for (int i = 0; i < allOrders.Count; i++)
            {
                if (allOrders.ElementAt(i).Id == orderHeaderId)
                {
                    orderTime = allOrders.ElementAt(i).DateTime;
                    orderHeaderState = allOrders.ElementAt(i).State;
                    currentOrderHeader = new OrderHeader(orderTime, orderHeaderId, orderHeaderState);
                    exitLoop = true;
                }
            }
            // 2. If an orderheader(from 1. ) is not found, then a new one is created

            switch (exitLoop)
            {
                case false:
                    orderTime = DateTime.Now;
                    orderHeaderState = (int)Enum.Parse(typeof(OrderStates), "New");
                    currentOrderHeader = new OrderHeader(orderTime, orderHeaderId, orderHeaderState);
                    break;
            }

            // 3. Hides "Add" button and "Submit" button if order state is "Complete".
            if (currentOrderHeader.State == (int)Enum.Parse(typeof(OrderStates), "Complete"))
            {
                Btn_Add_Order.Visibility = Visibility.Hidden;
                Btn_Submit.Visibility = Visibility.Hidden;
            }

            // 4. Displays the order items inside the datagrid
            currentOrders = layer.ProcessOrder(currentOrderHeader.Id);
            dgOrderItem.ItemsSource = currentOrders;

            // 5. Adds each order item price and calculates the total
            double total = 0;
            for (int i = 0; i < currentOrders.Count; i++)
            {
                total += currentOrders.ElementAt(i).Total;
            }

            // 6. Displays all the information inside textboxes.
            textbox_order.Text = "Order : #" + orderHeaderId;
            OrderStates currentState = (OrderStates)currentOrderHeader.State;

            textbox_datetime.Text = "Order Time : " + currentOrderHeader.DateTime;
            textbox_state.Text = "State : " + currentState;
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
            MainView pageobj = new MainView();
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
            AddOrderItemView pageobj = new AddOrderItemView(currentOrderHeader.Id);
            pageobj.Show();
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
            if (currentOrderHeader.State != (int)Enum.Parse(typeof(OrderStates), "Complete"))
            {
                int itemToBeDeletedId = currentOrders.ElementAt(dgOrderItem.SelectedIndex).StockItemId;
                int itemToBeDeletedQuantity = currentOrders.ElementAt(dgOrderItem.SelectedIndex).Quantity;
                string itemToBeDeletedDescription = currentOrders.ElementAt(dgOrderItem.SelectedIndex).Description;

                layer.DeleteOrderItem(currentOrderHeader.Id, itemToBeDeletedId,
                    itemToBeDeletedQuantity, itemToBeDeletedDescription);
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
            bool allItemsAreInStock = true;
            for (int i = 0; i < currentOrders.Count; i++)
            {
                switch (currentOrders[i].Description)
                {
                    case "Not_in_stock":
                        allItemsAreInStock = false;
                        break;
                }
            }

            //if there is a single item that is not in stock OR there is less than 1 item, then the order gets automatically rejected
            if ((allItemsAreInStock == false || currentOrders.Count < 1) && comparison.ToString() != "New")
            {
                currentOrderHeader.State = (int)Enum.Parse(typeof(OrderStates), "Rejected");
            }
            else
            {
                switch (comparison.ToString())
                {
                    case "New":
                    case "Rejected":
                        currentOrderHeader.State = (int)Enum.Parse(typeof(OrderStates), "Pending");
                        break;
                    default:
                        currentOrderHeader.State = (int)Enum.Parse(typeof(OrderStates), "Complete");
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
