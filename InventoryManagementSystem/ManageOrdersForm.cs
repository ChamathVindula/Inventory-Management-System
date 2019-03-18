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
    public partial class ManageOrdersForm : Form
    {
        private Queries _newQuery = null;

        private Order _newOrder, _deleteOrder, _editOrder, _selectedOrder;

        private List<String> _textboxList = new List<string>() { "OrdID", "OrdDate", "ShipDate", "CustID", "EmpID" };
        
        public ManageOrdersForm(string id)
        {
            InitializeComponent();
            _newQuery = new Queries();
            dataGridView1.DataSource = _newQuery.GetAllOrder();
            dataGridView1.ReadOnly = true;
            label1.Text = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm newLoginForm = new LoginForm();
            this.Hide();
            newLoginForm.Show();
        }

        
        private void ClearOrderData()
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
            ClearOrderData();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

            ManageCustomerForm _newManageCustomerForm = new ManageCustomerForm(label1.Text);
            _newManageCustomerForm.Show();
            _newManageCustomerForm.SetDesktopLocation(150, 75);
            this.Hide();
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

        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            ManageMainForm _newManageMainForm = new ManageMainForm(label1.Text);
            _newManageMainForm.Show();
            _newManageMainForm.SetDesktopLocation(150, 75);
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Focused)
            {
                if (textBox3.TextLength == 0)
                {
                    dataGridView1.DataSource = _newQuery.GetAllOrder();
                }
                if (textBox3.TextLength != 0)
                {
                    dataGridView1.DataSource = _newQuery.SearchOrderLike(textBox3.Text);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _newOrder = new Order(int.Parse(OrdID.Text), OrdDate.Text, ShipDate.Text, int.Parse(CustID.Text), int.Parse(EmpID.Text));
            _newOrder.Add();
            dataGridView1.DataSource = _newQuery.GetAllOrder();
        }

       private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            _editOrder = new Order(int.Parse(OrdID.Text), OrdDate.Text, ShipDate.Text, int.Parse(CustID.Text), int.Parse(EmpID.Text));

            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox" && _textboxList.Contains(c.Name.ToString()))
                {
                    _editOrder.Edit(c.Name.ToString(), c.Text);
                }
            }
            dataGridView1.DataSource = _newQuery.GetAllOrder();
            ClearOrderData();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            _deleteOrder = new Order(int.Parse(OrdID.Text), OrdDate.Text, ShipDate.Text, int.Parse(CustID.Text), int.Parse(EmpID.Text));
            DialogResult result = MessageBox.Show("Delete Order", "You Are Sure?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                _deleteOrder.Delete("Orders", "OrdID");
            }
            dataGridView1.DataSource = _newQuery.GetAllOrder();
            ClearOrderData();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            IDataReader reader = _newQuery.SearchOrder(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())).CreateDataReader();
            reader.Read();
            _selectedOrder = new Order(int.Parse(reader["OrdID"].ToString()), reader["OrdDate"].ToString(), reader["ShipDate"].ToString(), int.Parse(reader["CustID"].ToString()), int.Parse(reader["EmpID"].ToString()));
            OrdID.Text = _selectedOrder.ID.ToString();
            OrdDate.Text = _selectedOrder.OrderDate;
            ShipDate.Text = _selectedOrder.ShipeDate;
            CustID.Text = _selectedOrder.CustID.ToString();
            EmpID.Text = _selectedOrder.EmpID.ToString();
            label7.Text = _selectedOrder.GetTotalSales();
            label16.Text = _selectedOrder.Search().ToString();
        }
    }
}
