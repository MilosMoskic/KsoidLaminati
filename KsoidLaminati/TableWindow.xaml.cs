using KsoidLaminati.Classes;
using System;
using System.Collections.Generic;
using System.Windows;

namespace KsoidLaminati.UI
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        List<ItemModel> items= new List<ItemModel>();
        public TableWindow()
        {
            InitializeComponent();
            DisplayTable();
        }
        private void LoadItemsList()
        {
            items = SqlDataAccess.LoadItems();

        }
        private void DisplayTable()
        {
            try
            {
                var items = SqlDataAccess.LoadItems();

                DataGridItems.ItemsSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void addItemButtonClick(object sender, RoutedEventArgs e)
        {
            ItemModel item = new ItemModel();

            item.ItemType = itemTypeText.Text;
            item.ItemBrand = itemBrandText.Text;
            item.ItemName = itemNameText.Text;
            item.Quantity = Convert.ToDecimal(quantityText.Text);

            SqlDataAccess.SaveItem(item);

            itemTypeText.Text = "";
            itemBrandText.Text = "";
            itemNameText.Text = "";
            quantityText.Text = "";

            LoadItemsList();
        }
        
        private void BackToUI(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }
    }
}
