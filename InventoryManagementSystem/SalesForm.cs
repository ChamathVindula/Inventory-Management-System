using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class SalesForm : Form
    {
        private List<Product> _productList = new List<Product>();
        private List<Product> _tempProductList = new List<Product>();
        private Queries _newQuery;

        public SalesForm(String id)
        {
            InitializeComponent();
            label9.Text = id;
            _newQuery = new Queries();
            dataGridView2.ReadOnly = true;
            dataGridView1.ReadOnly = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int temp = 0;
            if (textBox1.Focused)
            {
                if (textBox1.TextLength > temp || textBox1.TextLength < temp)
                {
                    temp = textBox1.TextLength;
                    dataGridView2.DataSource = _newQuery.SearchCategoryLike(textBox1.Text, null);
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // must be fixed later
            dataGridView1.DataSource = _newQuery.SearchProductByCategory(int.Parse(_newQuery.SearchCategory(dataGridView2.CurrentCell.Value.ToString())), "selected");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int temp = 0;
            if (textBox2.Focused)
            {
                if (textBox2.TextLength > temp || textBox2.TextLength < temp)
                {
                    temp = textBox2.TextLength;
                    dataGridView1.DataSource = _newQuery.SearchProductLike(textBox2.Text, null);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            float _totalPrice = 0;
            listView1.View = View.List;
            IDataReader reader = _newQuery.SearchProduct(dataGridView1.CurrentRow.Cells[0].Value.ToString()).CreateDataReader();
            reader.Read();
            Product p = new Product(int.Parse(reader["ProdID"].ToString()), reader["ProdName"].ToString(), reader["ProdDescription"].ToString(), float.Parse(reader["ProdRetailPrice"].ToString()), int.Parse(reader["ProdStockQuantity"].ToString()), float.Parse(reader["Discount"].ToString()), int.Parse(reader["SoldQty"].ToString()), int.Parse(reader["CatID"].ToString()));
            _productList.Add(p);
            listView1.Items.Add(dataGridView1.CurrentCell.Value.ToString());  // doesn't work sometimes
            foreach (Product prd in _productList)
            {
                _totalPrice = _totalPrice + prd.DiscountedPrice();
            }
            textBox5.Text = _totalPrice.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float _totalPrice = 0;
            IDataReader reader = _newQuery.SearchProduct(listView1.SelectedItems[0].Text.ToString()).CreateDataReader();
            reader.Read();
            Product p = new Product(int.Parse(reader["ProdID"].ToString()), reader["ProdName"].ToString(), reader["ProdDescription"].ToString(), float.Parse(reader["ProdRetailPrice"].ToString()), int.Parse(reader["ProdStockQuantity"].ToString()), float.Parse(reader["Discount"].ToString()), int.Parse(reader["SoldQty"].ToString()), int.Parse(reader["CatID"].ToString()));
            _productList.Remove(p);
            foreach (Product prd in _productList)
            {
               if (prd.Name == p.Name)
                {
                    _tempProductList.Add(prd);
                    break;
                }
            }
            foreach (Product prd in _tempProductList)
            {
                _productList.Remove(prd);
            }
            listView1.Items.Remove(listView1.SelectedItems[0]);
            foreach (Product prd in _productList)
            {
                _totalPrice = _totalPrice + prd.DiscountedPrice();
            }
            textBox5.Text = _totalPrice.ToString();
            textBox5.Text = _totalPrice.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewOrExistingCustomerForm _newCustomer = new NewOrExistingCustomerForm(_productList, textBox11.Text, textBox12.Text, textBox14.Text, label9.Text);
            _newCustomer.Show();
            _newCustomer.SetDesktopLocation(500, 150);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm _newLoginForm = new LoginForm();
            this.Hide();
            _newLoginForm.Show();
        }
    }
}
