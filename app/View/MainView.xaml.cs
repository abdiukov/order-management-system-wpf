using Controller;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly LogicLayer layer;
        private readonly List<OrderHeader> allOrderHeaders;
        /// <summary>
        /// Constructor.
        /// Connects to the Controller Layer
        /// Retrieves from the controller layer all order headers and shows them in the datagrid
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            this.layer = new LogicLayer();
            allOrderHeaders = layer.GetOrderHeaders();
            dgMainMenu.ItemsSource = allOrderHeaders;
            this.Topmost = true;
        }

        /// <summary>
        /// Creates a new order header (by using LogicLayer class)
        /// Retrieves the id from that new order header
        /// Passes that id to the new AddOrderView page
        /// Closes the current page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddPage_Click(object sender, RoutedEventArgs e)
        {
            int orderHeaderId = layer.CreateNewOrderHeader();
            AddOrderView pageobj = new AddOrderView(orderHeaderId);
            pageobj.Show();
            Close();
        }
        /// <summary>
        /// Retrieves the ID of the OrderHeader selected in the datagrid
        /// Passes that id to the new AddOrderView page
        /// Closes the current page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            int dgSelectedHeaderId = allOrderHeaders.ElementAt(dgMainMenu.SelectedIndex).Id;
            AddOrderView pageobj = new AddOrderView(dgSelectedHeaderId);
            pageobj.Show();
            Close();
        }
    }
}
