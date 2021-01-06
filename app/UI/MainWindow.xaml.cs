using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ApplicationLogicLayer;
using Domain;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LogicLayer layer;
        private readonly List<OrderHeader> AllOrderHeaders;
        /// <summary>
        /// Constructor.
        /// Connects to the Controller Layer
        /// Retrieves from the controller layer all order headers and shows them in the datagrid
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            LogicLayer layer = new LogicLayer();
            this.layer = layer;
            AllOrderHeaders = layer.GetOrderHeaders();
            dgMainMenu.ItemsSource = AllOrderHeaders;
        }
         
        /// <summary>
        /// Creates a new order header (by using LogicLayer class)
        /// Retrieves the id from that new order header
        /// Passes that id to the new AddOrder_Window page
        /// Closes the current page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddPage_Click(object sender, RoutedEventArgs e)
        {
            int orderHeaderID = layer.CreateNewOrderHeader();
            AddOrder_Window pageobj = new AddOrder_Window(orderHeaderID);
            pageobj.Show();
            Close();
        }
        /// <summary>
        /// Retrieves the ID of the OrderHeader selected in the datagrid
        /// Passes that id to the new AddOrder_Window page
        /// Closes the current page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            int dataGrid_selectedHeaderID = AllOrderHeaders.ElementAt(dgMainMenu.SelectedIndex).Id;
            AddOrder_Window pageobj = new AddOrder_Window(dataGrid_selectedHeaderID);
            pageobj.Show();
            Close();
        }
    }
}
