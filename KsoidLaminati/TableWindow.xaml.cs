using KsoidLaminati.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
            if (quantityText.Text == "")
            {
                item.Quantity = 0;
            }
            else
            {
                item.Quantity = Convert.ToDecimal(quantityText.Text);
            }

            SqlDataAccess.SaveItem(item);

            itemTypeText.Text = "";
            itemBrandText.Text = "";
            itemNameText.Text = "";
            quantityText.Text = "";

            DisplayTable();
        }
        
        private void BackToUI(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {

                var row = sender as DataGridRow;
                var item = row.DataContext as ItemModel;

                idText.Text = item.Id.ToString();
                itemTypeText.Text = item.ItemType.ToString();
                itemBrandText.Text = item.ItemBrand.ToString();
                itemNameText.Text = item.ItemName.ToString();
                quantityText.Text = item.Quantity.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void updateItemButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idText.Text != "")
                {
                    ItemModel item = new ItemModel();
                    item.Id = Convert.ToInt32(idText.Text);
                    item.ItemType = itemTypeText.Text;
                    item.ItemName = itemNameText.Text;
                    item.ItemBrand = itemBrandText.Text;
                    if (quantityText.Text == "")
                    {
                        item.Quantity = 0;
                    }
                    else
                    {
                        item.Quantity = Convert.ToDecimal(quantityText.Text);
                    }
                    SqlDataAccess.UpdateItem(item);

                    itemTypeText.Text = "";
                    itemBrandText.Text = "";
                    itemNameText.Text = "";
                    quantityText.Text = "";

                    DisplayTable();
                }
                else
                {
                    MessageBox.Show("Id je null.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
