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
    public partial class ManageCustomerForm : Form
    {
        Customer _selectedCustomer = null;
        Customer _newCustomer = null;
        Customer _deleteCustomer = null;
        Customer _editCustomer = null;
        private Queries _newQuery;
        List<String> _textboxList = new List<string>() {"CustID", "CustFirstName", "CustLastName", "CustPhoneNumber" , "CustStreetAddress", "CustCity", "CustState", "CustAreaCode"};

        public ManageCustomerForm(string id)
        {
            InitializeComponent();
            label1.Text = id;
            _newQuery = new Queries();
            dataGridView1.DataSource = _newQuery.GetAllCustomers();
            dataGridView1.ReadOnly = true;
        }

        private void ManageCustomerForm_Load(object sender, EventArgs e)
        {

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            ManageEmployeeForm _newManageEmployeeForm = new ManageEmployeeForm(label1.Text);
            _newManageEmployeeForm.Show();
            _newManageEmployeeForm.SetDesktopLocation(150, 75);
            this.Hide();
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            ManageCategoryForm _newManageCategoryForm = new ManageCategoryForm(label1.Text);
            _newManageCategoryForm.Show();
            _newManageCategoryForm.SetDesktopLocation(150, 75);
            this.Hide();
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            ManageProductForm _newManageProductForm = new ManageProductForm(label1.Text);
            _newManageProductForm.Show();
            _newManageProductForm.SetDesktopLocation(150, 75);
            this.Hide();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            ManageSupplierForm _newManageSupplierForm = new ManageSupplierForm(label1.Text);
            _newManageSupplierForm.Show();
            _newManageSupplierForm.SetDesktopLocation(150, 75);
            this.Hide();
        }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            ManageOrdersForm _newManageOrderForm = new ManageOrdersForm(label1.Text);
            _newManageOrderForm.Show();
            _newManageOrderForm.SetDesktopLocation(150, 75);
            this.Hide();
        }

        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            ManageMainForm _newManageMainForm = new ManageMainForm(label1.Text);
            _newManageMainForm.Show();
            _newManageMainForm.SetDesktopLocation(150, 75);
            this.Hide();
        }

        private void ClearCustomerData()
        {
            foreach (Control c in Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox")
                {
                    ClearText(c as TextBox);
                }
            }
        }

        private void ClearText(TextBox t)
        {
            t.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearCustomerData();
        }

       private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IDataReader reader = _newQuery.SearchCustomer(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())).CreateDataReader();
            reader.Read();
            _selectedCustomer = new Customer(int.Parse(reader["CustID"].ToString()), reader["CustFirstName"].ToString(), reader["CustLastName"].ToString(), Int64.Parse(reader["CustPhoneNumber"].ToString()), reader["CustStreetAddress"].ToString(), reader["CustCity"].ToString(), reader["CustState"].ToString(), int.Parse(reader["CustAreaCode"].ToString()));
            CustID.Text = _selectedCustomer.ID.ToString();
            CustFirstName.Text = _selectedCustomer.FirstName;
            CustLastName.Text = _selectedCustomer.LastName;
            CustPhoneNumber.Text = _selectedCustomer.PhoneNumber.ToString();
            CustStreetAddress.Text = _selectedCustomer.Street;
            CustCity.Text = _selectedCustomer.City;
            CustState.Text = _selectedCustomer.State;
            CustAreaCode.Text = _selectedCustomer.AreaCode.ToString();
            label16.Text = _selectedCustomer.CalculateTotalOrdersPlaced();
            label17.Text = _selectedCustomer.CalculateTotalSales();
            label18.Text = _selectedCustomer.GetLastOrderDate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                _newCustomer = new Customer(int.Parse(CustID.Text), CustFirstName.Text, CustLastName.Text, Int64.Parse(CustPhoneNumber.Text), CustStreetAddress.Text, CustCity.Text, CustState.Text, int.Parse(CustAreaCode.Text));
                _newCustomer.Add();
                dataGridView1.DataSource = _newQuery.GetAllCustomers();
                ClearCustomerData();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to Add Customer!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _deleteCustomer = new Customer(int.Parse(CustID.Text), CustFirstName.Text, CustLastName.Text, Int64.Parse(CustPhoneNumber.Text), CustStreetAddress.Text, CustCity.Text, CustState.Text, int.Parse(CustAreaCode.Text));
            DialogResult result = MessageBox.Show("Delete Customer", "You Are Sure?", MessageBoxButtons.YesNo);  // correct this later
            if (result == DialogResult.Yes)
            {
                _deleteCustomer.Delete("Customer", "CustID");

            }
            dataGridView1.DataSource = _newQuery.GetAllCustomers();
            ClearCustomerData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm _newLoginForm = new LoginForm();
            this.Hide();
            _newLoginForm.Show();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Focused)
            {
                if (textBox9.TextLength == 0)
                {
                    dataGridView1.DataSource = _newQuery.GetAllCustomers();
                }
                if (textBox9.TextLength != 0)
                {
                    dataGridView1.DataSource = _newQuery.SearchCustomerLike(textBox9.Text);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _editCustomer = new Customer(int.Parse(CustID.Text), CustFirstName.Text, CustLastName.Text, Int64.Parse(CustPhoneNumber.Text), CustStreetAddress.Text, CustCity.Text, CustState.Text, int.Parse(CustAreaCode.Text));

            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox" && _textboxList.Contains(c.Name.ToString()))
                {
                    _editCustomer.Edit(c.Name.ToString(), c.Text);
                }
            }
            dataGridView1.DataSource = _newQuery.GetAllCustomers();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //does nothing
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            //does nothing
        }
    }
}
