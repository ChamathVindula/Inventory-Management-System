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
    public partial class LoginForm : Form
    {
        Queries _newQuery;
        public LoginForm()
        {
            InitializeComponent();
            _newQuery = new Queries();
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            string time = TimeZoneInfo.ConvertTime(DateTime.Now, timezone).ToString("HH:mm:ss");
            DateTime today = DateTime.Today;
            string date = today.ToString("MM/dd/yyyy");
            try
            {
                int temp = _newQuery.LoginEmployee(int.Parse(textBox1.Text), textBox2.Text);
                if (temp == 1)
                {
                _newQuery.AddWorkLog((int.Parse(_newQuery.GetLastEnteredID("EmployeeWorkLog", "LogID"))) + 1, int.Parse(textBox1.Text), date, time);
                SalesForm _newSalesForm = new SalesForm(textBox1.Text);
                _newSalesForm.Show();
                _newSalesForm.SetDesktopLocation(325, 100);
                 this.Hide();
            }

                else if (temp == 2)
                {
                   _newQuery.AddWorkLog((int.Parse(_newQuery.GetLastEnteredID("EmployeeWorkLog", "LogID"))) + 1, int.Parse(textBox1.Text), date, time);
                   ManageMainForm _newManageMainForm = new ManageMainForm(textBox1.Text);
                   _newManageMainForm.Show();
                   this.Hide();
                }
                else if (temp == 0)
                {
                    MessageBox.Show("Either Your User ID or Password is invalid!");
                    textBox1.Clear();
                    textBox2.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Enter the Correct Login Details!");
                textBox1.Clear();
                textBox2.Clear();
            }

        }
    }
}
