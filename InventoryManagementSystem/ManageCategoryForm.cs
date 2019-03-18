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
    public partial class ManageCategoryForm : Form
    {
        private Queries _newQuery = null;
        private Category _newCategory, _deleteCategory, _editCategory, _selectedCategory;
        private List<String> _textboxList = new List<string>() { "CatID", "CatName", "CatDescription" };

        public ManageCategoryForm(string id)
        {
            InitializeComponent();
            label1.Text = id;
            _newQuery = new Queries();
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = _newQuery.GetAllCategories();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm newLoginForm = new LoginForm();
            this.Hide();
            newLoginForm.Show();
        }

        private void ClearCategoryData()
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
            ClearCategoryData();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IDataReader reader = _newQuery.SearchCategory(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())).CreateDataReader();
            reader.Read();
            _selectedCategory = new Category(int.Parse(reader["CatID"].ToString()), reader["CatName"].ToString(), reader["CatDescription"].ToString());
            CatID.Text = _selectedCategory.ID.ToString();
            CatName.Text = _selectedCategory.Name;
            CatDescription.Text = _selectedCategory.Description;
            _selectedCategory.PopulateProductList();
            label16.Text = _selectedCategory.Search().ToString();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Focused)
            {
                if (textBox9.TextLength == 0)
                {
                    dataGridView1.DataSource = _newQuery.GetAllCategories();
                }
                if (textBox9.TextLength != 0)
                {
                    dataGridView1.DataSource = _newQuery.SearchCategoryLike(textBox9.Text);
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            _deleteCategory = new Category(int.Parse(CatID.Text), CatName.Text, CatDescription.Text);
            DialogResult result = MessageBox.Show("Delete Category", "You Are Sure?", MessageBoxButtons.YesNo); 
            if (result == DialogResult.Yes)
            {
                _deleteCategory.Delete("Category", "CatID");
            }
            dataGridView1.DataSource = _newQuery.GetAllCategories();
            ClearCategoryData();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            _editCategory = new Category(int.Parse(CatID.Text), CatName.Text, CatDescription.Text);

            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox" && _textboxList.Contains(c.Name.ToString()))
                {
                    _editCategory.Edit(c.Name.ToString(), c.Text);
                }
            }
            dataGridView1.DataSource = _newQuery.GetAllCategories();
            ClearCategoryData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _newCategory = new Category(int.Parse(CatID.Text), CatName.Text, CatDescription.Text);
            _newCategory.Add();
            dataGridView1.DataSource = _newQuery.GetAllCategories();
        }

    }
}
