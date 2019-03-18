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
    public partial class NewOrExistingCustomerForm : Form
    {
        List<Product> _orderProducts;
        Customer _currentCustomer = null;
        Order _newOrder;
        String _orderDate;
        String _shippedDate;
        Queries _newQuery;
        DateTime _today;
        String _servingEmployee;

        public NewOrExistingCustomerForm(List<Product> _productList, string date, string month, string year, string empID)
        {
            InitializeComponent();
            _orderProducts = _productList;
            _newQuery = new Queries();
            _today = DateTime.Today;
            _orderDate = _today.ToString("MM/dd/yyyy");
            _shippedDate = month + "/" + date + "/" + year;
            _servingEmployee = empID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool newCustomer = false;
            try
            {
                if (radioButton1.Checked)
                {
                    if (textBox1.Text.Length != 0)
                    {
                        IDataReader reader = _newQuery.SearchCustomer(int.Parse(textBox1.Text)).CreateDataReader();
                        reader.Read();
                        _currentCustomer = new Customer(int.Parse(reader["CustID"].ToString()), reader["CustFirstName"].ToString(), reader["CustLastName"].ToString(), Int64.Parse(reader["CustPhoneNumber"].ToString()), reader["CustStreetAddress"].ToString(), reader["CustCity"].ToString(), reader["CustState"].ToString(), int.Parse(reader["CustAreaCode"].ToString()));
                    }
                    else if ((textBox10.Text.Length != 0) && (textBox9.Text.Length != 0))
                    {
                        IDataReader reader = _newQuery.SearchCustomer(textBox10.Text, textBox9.Text).CreateDataReader();
                        reader.Read();
                        _currentCustomer = new Customer(int.Parse(reader["CustID"].ToString()), reader["CustFirstName"].ToString(), reader["CustLastName"].ToString(), Int64.Parse(reader["CustPhoneNumber"].ToString()), reader["CustStreetAddress"].ToString(), reader["CustCity"].ToString(), reader["CustState"].ToString(), int.Parse(reader["CustAreaCode"].ToString()));
                    }
                }
                if (radioButton2.Checked)
                {
                    _currentCustomer = new Customer((int.Parse(_newQuery.GetLastEnteredID("Customer", "CustID"))) + 1, textBox2.Text, textBox3.Text, Int64.Parse(textBox4.Text), textBox5.Text, textBox6.Text, textBox7.Text, int.Parse(textBox8.Text));
                    newCustomer = true;
                }
                if (newCustomer)
                {
                    _currentCustomer.Add();                 // if customer is new then adds to the database
                }
                _newOrder = new Order(int.Parse(_newQuery.GetLastEnteredID("Orders", "OrdID") + 1), _orderDate, _shippedDate, _currentCustomer.ID, int.Parse(_servingEmployee));
                _newOrder.Add();
                foreach (Product p in _orderProducts)
                {
                    _newOrder.AddItem(p);
                }
                _newOrder.AddOrderItems();

                SalesForm newForm = new SalesForm(_servingEmployee);
                MessageBox.Show("Transaction Successful");
                newForm.Show();
                newForm.SetDesktopLocation(325, 100);
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
