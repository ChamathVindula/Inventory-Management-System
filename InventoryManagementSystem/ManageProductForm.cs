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
    public partial class ManageProductForm : Form
    {
        private Queries _newQuery = null;

        private Product _newProduct, _deleteProduct, _editProduct, _selectedProduct;

        private List<String> _textboxList = new List<string>() { "ProdID", "ProdName", "ProdDescription", "ProdRetailPrice", "ProdStockQuantity", "CatID" , "Discount", "SoldQty" };


        public ManageProductForm(string id)
        {
            InitializeComponent();
            _newQuery = new Queries();
            dataGridView1.DataSource = _newQuery.GetAllProducts();
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

        private void ClearProductData()
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
            ClearProductData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _editProduct = new Product(int.Parse(ProdID.Text), ProdName.Text, ProdDescription.Text, float.Parse(ProdRetailPrice.Text), int.Parse(ProdStockQuantity.Text), float.Parse(Discount.Text), int.Parse(SoldQty.Text), int.Parse(CatID.Text));

            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox" && _textboxList.Contains(c.Name.ToString()))
                {
                    _editProduct.Edit(c.Name.ToString(), c.Text);
                }
            }
            dataGridView1.DataSource = _newQuery.GetAllProducts();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _deleteProduct = new Product(int.Parse(ProdID.Text), ProdName.Text, ProdDescription.Text, float.Parse(ProdRetailPrice.Text),int.Parse(ProdStockQuantity.Text), float.Parse(Discount.Text), int.Parse(SoldQty.Text), int.Parse(CatID.Text));
            DialogResult result = MessageBox.Show("Delete Product", "You Are Sure?", MessageBoxButtons.YesNo);  // correct this later
            if (result == DialogResult.Yes)
            {
                _deleteProduct.Delete("Product", "ProdID");

            }
            dataGridView1.DataSource = _newQuery.GetAllProducts();
            ClearProductData();
        }

       private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IDataReader reader = _newQuery.SearchProduct(dataGridView1.CurrentRow.Cells[1].Value.ToString()).CreateDataReader();
            reader.Read();
            _selectedProduct = new Product(int.Parse(reader["ProdID"].ToString()), reader["ProdName"].ToString(), reader["ProdDescription"].ToString(), float.Parse(reader["ProdRetailPrice"].ToString()), int.Parse(reader["ProdStockQuantity"].ToString()), float.Parse(reader["Discount"].ToString()), int.Parse(reader["SoldQty"].ToString()), int.Parse(reader["CatID"].ToString()));
            ProdID.Text = _selectedProduct.ID.ToString();
            ProdName.Text = _selectedProduct.Name;
            ProdDescription.Text = _selectedProduct.Description;
            ProdRetailPrice.Text = _selectedProduct.Price.ToString();
            ProdStockQuantity.Text = _selectedProduct.StockQty.ToString();
            CatID.Text = _selectedProduct.CatId.ToString();
            Discount.Text = _selectedProduct.Discount.ToString();
            SoldQty.Text = _selectedProduct.AmountSold.ToString();
            label17.Text = _selectedProduct.SoldQuantity();
            label3.Text = _selectedProduct.SoldQuantity();
            label17.Text = _selectedProduct.Search().ToString();
        }

        private void ManageProductForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm _newLoginForm = new LoginForm();
            this.Hide();
            _newLoginForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _newProduct = new Product(int.Parse(ProdID.Text), ProdName.Text, ProdDescription.Text, float.Parse(ProdRetailPrice.Text), int.Parse(ProdStockQuantity.Text), float.Parse(Discount.Text), int.Parse(SoldQty.Text), int.Parse(CatID.Text));
            _newProduct.Add();
            dataGridView1.DataSource = _newQuery.GetAllProducts();
            ClearProductData();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Focused)
            {
                if (textBox9.TextLength == 0)
                {
                    dataGridView1.DataSource = _newQuery.GetAllProducts();
                }
                if (textBox9.TextLength != 0)
                {
                    dataGridView1.DataSource = _newQuery.SearchProductLike(textBox9.Text, "all");
                }
            }
        }

    }
}
