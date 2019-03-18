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
    public partial class ManageSupplierForm : Form
    {
        private Queries _newQuery = null;

        private Supplier _newSupplier, _deleteSupplier, _editSupplier, _selectedSupplier;

        private List<String> _textboxList = new List<string>() { "SupID", "SupName", "SupPhoneNumber", "SupEmail", "SupStreetAddress", "SupCity", "SupState", "SupAreaCode" };

        public ManageSupplierForm(string id)
        {
            InitializeComponent();
            _newQuery = new Queries();
            dataGridView1.DataSource = _newQuery.GetAllSuppliers();
            label1.Text = id;
            dataGridView1.ReadOnly = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm _newLoginForm = new LoginForm();
            this.Hide();
            _newLoginForm.Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Focused)
            {
                if (textBox3.TextLength == 0)
                {
                    dataGridView1.DataSource = _newQuery.GetAllSuppliers();
                }
                if (textBox3.TextLength != 0)
                {
                    dataGridView1.DataSource = _newQuery.SearchSupplierLike(textBox3.Text);
                }
            }
        }

        private void ClearSupplierData()
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
            ClearSupplierData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IDataReader reader = _newQuery.SearchSupplier(dataGridView1.CurrentRow.Cells[1].Value.ToString()).CreateDataReader();
            reader.Read();
            _selectedSupplier = new Supplier(int.Parse(reader["SupID"].ToString()), reader["SupName"].ToString(), reader["SupStreetAddress"].ToString(), reader["SupCity"].ToString(), reader["SupState"].ToString(), int.Parse(reader["SupAreaCode"].ToString()), reader["SupEmail"].ToString(), long.Parse(reader["SupPhoneNumber"].ToString()));
            SupID.Text = _selectedSupplier.ID.ToString();
            SupName.Text = _selectedSupplier.Name;
            SupStreetAddress.Text = _selectedSupplier.Street;
            SupCity.Text = _selectedSupplier.City;
            SupState.Text = _selectedSupplier.State;
            SupAreaCode.Text = _selectedSupplier.AreaCode.ToString();
            SupPhoneNumber.Text = _selectedSupplier.PhoneNumber.ToString();
            SupEmail.Text = _selectedSupplier.Email;
            label16.Text = _selectedSupplier.ProductsSupplied();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _newSupplier = new Supplier(int.Parse(SupID.Text), SupName.Text, SupStreetAddress.Text, SupCity.Text, SupState.Text, int.Parse(SupAreaCode.Text), SupEmail.Text, long.Parse(SupPhoneNumber.Text));
            _newSupplier.Add();
            dataGridView1.DataSource = _newQuery.GetAllSuppliers();
            ClearSupplierData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _editSupplier = new Supplier(int.Parse(SupID.Text), SupName.Text, SupStreetAddress.Text, SupCity.Text, SupState.Text, int.Parse(SupAreaCode.Text), SupEmail.Text, long.Parse(SupPhoneNumber.Text));

            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox" && _textboxList.Contains(c.Name.ToString()))
                {
                    _editSupplier.Edit(c.Name.ToString(), c.Text);
                }
            }
            dataGridView1.DataSource = _newQuery.GetAllSuppliers();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _deleteSupplier = new Supplier(int.Parse(SupID.Text), SupName.Text, SupStreetAddress.Text, SupCity.Text, SupState.Text, int.Parse(SupAreaCode.Text), SupEmail.Text, long.Parse(SupPhoneNumber.Text));
            DialogResult result = MessageBox.Show("Delete Supplier", "You Are Sure?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                _deleteSupplier.Delete("Supplier", "SupID");

            }
            dataGridView1.DataSource = _newQuery.GetAllSuppliers();
            ClearSupplierData();
        }
    }
}
