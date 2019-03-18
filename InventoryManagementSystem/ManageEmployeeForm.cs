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
    public partial class ManageEmployeeForm : Form
    {
        Queries _newQuery;
        Employee _selectedEmployee;
        Employee _newEmployee = null;
        Employee _deleteEmployee = null;
        Employee _editEmployee = null;
        List<String> _textboxList = new List<string>() { "EmpID", "EmpFirstName", "EmpLastName", "EmpPhoneNumber", "EmpStreetAddress", "EmpCity", "EmpState", "EmpAreaCode", "Role", "EmpPassword", "EmpDOB", "EmpPrivilegeLevel"};


        public ManageEmployeeForm(string id)
        {
            InitializeComponent();
            label1.Text = id;
            _newQuery = new Queries();
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = _newQuery.GetAllEmployees();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IDataReader reader = _newQuery.SearchEmployee(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())).CreateDataReader();
            reader.Read();
            _selectedEmployee = new Employee(int.Parse(reader["EmpID"].ToString()), reader["EmpFirstName"].ToString(), reader["EmpLastName"].ToString(), Int64.Parse(reader["EmpPhoneNumber"].ToString()), reader["EmpStreetAddress"].ToString(), reader["EmpCity"].ToString(), reader["EmpState"].ToString(), int.Parse(reader["EmpAreaCode"].ToString()), int.Parse(reader["EmpPrivilegeLevel"].ToString()), reader["Role"].ToString(), reader["EmpPassword"].ToString(), reader["EmpDOB"].ToString());
            EmpID.Text = _selectedEmployee.ID.ToString();
            EmpFirstName.Text = _selectedEmployee.FirstName;
            EmpLastName.Text = _selectedEmployee.LastName;
            EmpPhoneNumber.Text = _selectedEmployee.PhoneNumber.ToString();
            EmpStreetAddress.Text = _selectedEmployee.Street;
            EmpCity.Text = _selectedEmployee.City;
            EmpState.Text = _selectedEmployee.State;
            EmpAreaCode.Text = _selectedEmployee.AreaCode.ToString();
            EmpDOB.Text = reader["EmpDOB"].ToString();
            EmpPrivilegeLevel.Text = _selectedEmployee.Privilage.ToString();
            Role.Text = _selectedEmployee.Role;
            EmpPassword.Text = _selectedEmployee.Password;
            label16.Text = _selectedEmployee.TotalShiftsWorked();
        }

        private void ClearEmployeeData()
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
            ClearEmployeeData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm newLoginForm = new LoginForm();
            this.Hide();
            newLoginForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _newEmployee = new Employee(int.Parse(EmpID.Text), EmpFirstName.Text, EmpLastName.Text, Int64.Parse(EmpPhoneNumber.Text), EmpStreetAddress.Text, EmpCity.Text, EmpState.Text, int.Parse(EmpAreaCode.Text), int.Parse(EmpPrivilegeLevel.Text), Role.Text, EmpPassword.Text, EmpDOB.Text);
            _newEmployee.Add();
            dataGridView1.DataSource = _newQuery.GetAllEmployees();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _editEmployee = new Employee(int.Parse(EmpID.Text), EmpFirstName.Text, EmpLastName.Text, Int64.Parse(EmpPhoneNumber.Text), EmpStreetAddress.Text, EmpCity.Text, EmpState.Text, int.Parse(EmpAreaCode.Text), int.Parse(EmpPrivilegeLevel.Text), Role.Text, EmpPassword.Text, EmpDOB.Text);

            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox" && _textboxList.Contains(c.Name.ToString()))
                {
                    _editEmployee.Edit(c.Name.ToString(), c.Text);
                }
            }
            dataGridView1.DataSource = _newQuery.GetAllEmployees();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _deleteEmployee = new Employee(int.Parse(EmpID.Text), EmpFirstName.Text, EmpLastName.Text, Int64.Parse(EmpPhoneNumber.Text), EmpStreetAddress.Text, EmpCity.Text, EmpState.Text, int.Parse(EmpAreaCode.Text), int.Parse(EmpPrivilegeLevel.Text), Role.Text, EmpPassword.Text, EmpDOB.Text);
            DialogResult result = MessageBox.Show("Delete Employee", "You Are Sure?", MessageBoxButtons.YesNo);  // correct this later
            if (result == DialogResult.Yes)
            {
                _deleteEmployee.Delete("Employee", "EmpID");

            }
            dataGridView1.DataSource = _newQuery.GetAllEmployees();
            ClearEmployeeData();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Focused)
            {
                if (textBox9.TextLength == 0)
                {
                    dataGridView1.DataSource = _newQuery.GetAllEmployees();
                }
                if (textBox9.TextLength != 0)
                {
                    dataGridView1.DataSource = _newQuery.SearchEmployeeLike(textBox9.Text);
                }
            }
        }
    }
}
