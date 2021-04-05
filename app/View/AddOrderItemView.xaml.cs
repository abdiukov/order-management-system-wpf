using Controller;
using Model;
using System.Linq;
using System.Windows;

namespace View
{
    /// <summary>
    /// Interaction logic for AddOrderItemView.xaml
    /// </summary>
    public partial class AddOrderItemView : Window
    {
        private readonly LogicLayer layer = new LogicLayer();
        private readonly int orderHeaderId;

        /// <summary>
        /// Constructor for AddOrderItem
        /// </summary>
        /// <param name="orderHeaderId">The unique ID of the order header</param>
        public AddOrderItemView(int orderHeaderId)
        {
            InitializeComponent();
            this.orderHeaderId = orderHeaderId;
            dgStockItems.ItemsSource = layer.GetStockItems();
        }

        /// <summary>
        /// NavigateBack() is a function that navigates back to the main page
        /// </summary>
        private void NavigateBack()
        {
            AddOrderView newView = new AddOrderView(orderHeaderId);
            newView.Show();
            Close();
        }

        /// <summary>
        /// Calls the NavigateBack() function. That function closes the page and navigates back to AddOrder window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack();
        }

        /// <summary>
        /// Upon clicking that button inside the datagrid, a confirmation message pops up.
        /// After the confirmation message, the selected item and quantity get added to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            //by default, quantity is 1
            //this is done to prevent users from entering incorrect numbers
            //if the user leaves the textbox empty, it should be 1, if user does not enter a number, it will be 1.
            int quantity = 1;

            switch (int.TryParse(txtBox_quantity.Text, out int quantity_successfully_converted_into_int)
                && quantity_successfully_converted_into_int > 0)
            {
                case true:
                    quantity = quantity_successfully_converted_into_int;
                    break;
                case false:
                    MessageBox.Show("Please enter the Quantity. The Quantity must be greater than zero");
                    break;
            }

            //selecting the item that the user has clicked on in the datagrid (dgStockItems)
            StockItem selected_item = layer.GetStockItems().ElementAt(dgStockItems.SelectedIndex);

            //CHECKING WHETHER ITEM IS IN STOCK

            //retrieving the amount of items in stock
            int inStock = selected_item.InStock;
            MessageBoxResult userConfirmedToSave;
            string description;

            //If the quantity selected is more than in stock, the description of the item in the database changes to "Not_In_Stock"

            if (quantity > inStock)
            {
                userConfirmedToSave = MessageBox.Show("There are currently not enough items in stock." +
                    "\nRequested: " + quantity + " , In stock: " + inStock +
                    "\nThis order might be rejected if there is not enough stock on hand when the order is being processed." +
                    "\nDo you wish to proceed?", "Info", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                description = "Not_in_stock";
            }
            else
            {
                userConfirmedToSave = MessageBox.Show("You are about to add " + quantity +
                    " items to your order. Do you wish to proceed?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                description = "In_stock";
            }

            switch (userConfirmedToSave)
            {
                case MessageBoxResult.Yes:

                    layer.UpsertOrderItem(description, selected_item.Price,
                        orderHeaderId, selected_item.Id, quantity);
                    NavigateBack();
                    break;
            }
        }
    }
}
