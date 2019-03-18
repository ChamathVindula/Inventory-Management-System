using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace InventoryManagementSystem
{
    public class Queries
    {
        private String _connectionString;
        private SqlConnection _newConnection;

        public Queries()
        {
            _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CompanyDB.mdf;Integrated Security=True";
            //_newConnection = new SqlConnection(_connectionString);
        }


        // Method to add a new customer
        public Boolean AddCustomer(int id, String fname, String lname, Int64 phone, String street, String city, String state, int areaCode)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("INSERT INTO [dbo].[Customer] ([CustID], [CustFirstName], [CustLastName], [CustPhoneNumber], [CustStreetAddress], [CustCity], [CustState], [CustAreaCode]) VALUES ('" + id + "', '" + fname + "', '" + lname + "', '" + phone + "', '" + street + "', '" + city + "', '" + state + "', '" + areaCode + "')", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

        // Method to add a new employee
        public Boolean AddEmployee(int id, String fname, String lname, int privilage, String street, String city, String state, int areaCode, String role, Int64 phone, String pass, String dob)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("INSERT INTO [dbo].[Employee] ([EmpID], [EmpFirstName], [EmpLastName], [EmpPrivilegeLevel], [EmpStreetAddress], [EmpCity], [EmpState], [EmpAreaCode], [Role], [EmpPhoneNumber], [EmpPassword], [EmpDOB]) VALUES ('" + id + "', '" + fname + "', '" + lname + "', '" + privilage + "', '" + street + "', '" + city + "', '" + state + "', '" + areaCode + "', '" + role + "', '" + phone + "', '" + pass + "', '" + dob + "')", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

        // Method to add a new supplier
        public Boolean AddSupplier(int id, String name, String street, String city, String state, int areaCode, Int64 phone, String email)
        {
            int temp = 0;
            using(_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("INSERT INTO [dbo].[Supplier] ([SupID], [SupName], [SupPhoneNumber], [SupEmail], [SupStreetAddress], [SupCity], [SupState], [SupAreaCode]) VALUES ('" + id + "', '" + name + "', '" + phone + "', '" + email + "', '" + street + "', '" + city + "', '" + state + "', '" + areaCode + "')", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

       
        // Method to add a new category of product
        public Boolean AddCategory(int id, String name, String description)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("INSERT INTO [dbo].[Category] ([CatID], [CatName], [CatDescription]) VALUES ('" + id + "', '" + name + "', '" + description + "')", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

        // Method to add a new product
        public Boolean AddProduct(int id, String name, String description, float price, int stockQty, int catID, float discount, int sold)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("INSERT INTO [dbo].[Product] ([ProdID], [ProdName], [ProdDescription], [ProdRetailPrice], [ProdStockQuantity], [CatID], [Discount], [SoldQty]) VALUES ('" + id + "', '" + name + "', '" + description + "', '" + price + "', '" + stockQty + "', '" + catID + "', '" + discount + "', '" + sold + "')", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

        // working and is used
        // Method to add a new work log every time an employee logs into the system
        public Boolean AddWorkLog(int logID, int empID, String workDate, String startTime)
        {
            int temp1 = 0;
            bool temp2 = false;
            string query = "INSERT INTO [dbo].[EmployeeWorkLog] ([LogID], [EmpID], [WorkDate], [StartTime], [EndTime]) VALUES (" + logID + "," + empID + ", '" + workDate + "', '" + startTime + "', null)";
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand(query, _newConnection);
                int temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp1 == 1) temp2 = true;
            return temp2;
        }

        // Method to add a new order
        public Boolean AddOrder(int id, String ordDate, String shipDate, int custID, int empID)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("INSERT INTO [dbo].[Orders] ([OrdID], [OrdDate], [ShipDate], [CustID], [EmpID]) VALUES ('" + id + "', '" + ordDate + "', '" + shipDate + "', '" + custID + "', '" + empID + "')", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

        // Method to add a new order detail (single order can contain multiple order details)
        public Boolean AddOrderDetails(int ordID, int prodID, float price)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("INSERT INTO [dbo].[OrderDetails] ([OrdID], [ProdID], [PricePerUnit]) VALUES ('" + ordID + "', '" + prodID + "', '" + price + "')", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

        // Method to add a new Product supplier
        public Boolean AddProductSupplier(int prodID, int supID, float price)
        {
            _newConnection.Open();
            SqlCommand _newCommand = new SqlCommand("INSERT INTO [dbo].[ProductSupplier] ([ProdID], [SupID], [WholesalePrice]) VALUES ('" + prodID + "', '" + supID + "', '" + price + "')", _newConnection);
            int temp = _newCommand.ExecuteNonQuery();
            _newConnection.Close();
            if (temp == 1) return true;
            else return false;
        }

        // Method to delete a customer using id
        public Boolean DeleteCustomer(int id)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("DELETE FROM Customer WHERE CustID = '" + id + "'", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

        // Method to delete an employee using id
        public Boolean DeleteEmployee(int id)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("DELETE FROM Employee WHERE EmpID = '" + id + "'", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

        // Method to delete a category using id
        public Boolean DeleteCategory(int id)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("DELETE FROM Category WHERE CatID = '" + id + "'", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

        // Method to delete a product using id
        public Boolean DeleteProduct(int id)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("DELETE FROM Product WHERE ProdID = '" + id + "'", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

        public Boolean DeleteEntity(string table, string field, int id)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("DELETE FROM " + table + " WHERE " + field + " = '" + id + "'", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

        // Method to delete oder details using id
        public Boolean DeleteOrderDetails(int id)
        {
            _newConnection.Open();
            SqlCommand _newCommand = new SqlCommand("DELETE FROM OrderDetails WHERE OrdID = '" + id + "'", _newConnection);
            int temp = _newCommand.ExecuteNonQuery();
            _newConnection.Close();
            if (temp != 0) return true;
            else return false;
        }

        // Method to delete a product supplier using either product id or supplier id or both
        public Boolean DeleteProductSupplier(int ProdID, int SupID, String selection)
        {
            String query = "";
            _newConnection.Open();
            if (selection == "both")
            {
                query = "DELETE FROM ProductSupplier WHERE SupID = '" + SupID + "' AND ProdID = '" + ProdID + "'";
            }
            if (selection == "product")
            {
                query = "DELETE FROM ProductSupplier WHERE ProdID = '" + ProdID + "'";
            }
            else if (selection == "supplier")
            {
                query = "DELETE FROM ProductSupplier WHERE SupID = '" + SupID + "'";
            }
            SqlCommand _newCommand = new SqlCommand(query, _newConnection);
            int temp = _newCommand.ExecuteNonQuery();
            _newConnection.Close();
            if (temp != 0) return true;
            else return false;
        }

        // Method to search a customer using id (returns a table containing customer data)
        public DataTable SearchCustomer(int id)
        {
            DataTable _newTable = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT * FROM Customer WHERE CustID = '" + id + "'", _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        // Method to search a customer using name (returns a table containing customer data)
        public DataTable SearchCustomer(String fname, String lname)
        {
            DataTable _newTable = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT * FROM Customer WHERE CustFirstName = '" + fname + "' AND CustLastName = '" + lname + "'", _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        // Method to search an employee using id (returns a table containing employee data)
        public DataTable SearchEmployee(int id)
        {
            DataTable _newTable = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT * FROM Employee WHERE EmpID = '" + id + "'", _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public DataTable SearchEntityById(int id)
        {
            DataTable _newTable = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT * FROM Employee WHERE EmpID = '" + id + "'", _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public DataTable SearchEmployeeLike(string name)
        {
            DataTable _newTable = null;
            string query = "SELECT * FROM Employee WHERE (EmpFirstName LIKE \'%" + (name + "%\')");
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand(query, _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        // Method to search an employee using name (returns a table containing employee data)
        public DataTable SearchEmployee(String fname, String lname)
        {
            _newConnection.Open();
            SqlCommand _newCommand = new SqlCommand("SELECT * FROM Customer WHERE EmpFirstName = '" + fname + "' AND EmpLastName = '" + lname + "'", _newConnection);
            _newCommand.ExecuteNonQuery();
            SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
            DataTable _newTable = new DataTable();
            _newAdapter.Fill(_newTable);
            _newConnection.Close();
            return _newTable;
        }

        // Method to search a supplier using id (returns a table containing supplier data)
        public DataTable SearchSupplier(int id)
        {
            _newConnection.Open();
            SqlCommand _newCommand = new SqlCommand("SELECT * FROM Supplier WHERE SupID = '" + id + "'", _newConnection);
            _newCommand.ExecuteNonQuery();
            SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
            DataTable _newTable = new DataTable();
            _newAdapter.Fill(_newTable);
            _newConnection.Close();
            return _newTable;
        }

        // Method to search a supplier using name (returns a table containing supplier data)
        public DataTable SearchSupplier(String name)
        {
            DataTable _newTable;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT * FROM Supplier WHERE SupName = '" + name + "'", _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public int GetProductSoldQunatity(int id)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT SoldQty FROM Product WHERE ProdID = '" + id + "'", _newConnection);
                SqlDataReader reader = _newCommand.ExecuteReader();
                reader.Read();
                temp = int.Parse(reader["SoldQty"].ToString());
                _newConnection.Close();
            }
            return temp;
        }

        public int GetProductStockQunatity(int id)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT ProdStockQuantity FROM Product WHERE ProdID = '" + id + "'", _newConnection);
                SqlDataReader reader = _newCommand.ExecuteReader();
                reader.Read();
                temp = int.Parse(reader["ProdStockQuantity"].ToString());
                _newConnection.Close();
            }
            return temp;
        }

        // wromking and is used
        public DataTable SearchProductLike(string name, string selectCriteria)
        {
            string query;
            DataTable _newTable = null;
            if (selectCriteria == "all")
            {
                query = "SELECT * FROM Product WHERE (ProdName LIKE \'%" + (name + "%\')");
            }
            else
            {
                query = "SELECT ProdName, ProdRetailPrice, ProdID FROM Product WHERE (ProdName LIKE \'%" + (name + "%\')");
            }
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand(query, _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        // working and is used
        // Method to search a product using id (returns a table containing product data)
        public DataTable SearchProductByCategory(int id, string selectCriteria)
        {
            DataTable _newTable = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                SqlCommand _newCommand;
                _newConnection.Open();
                if (selectCriteria == "selected")
                {
                   _newCommand = new SqlCommand("SELECT ProdName, ProdRetailPrice, ProdID FROM Product WHERE CatID = '" + id + "'", _newConnection);
                }
                else
                {
                   _newCommand = new SqlCommand("SELECT * FROM Product WHERE CatID = '" + id + "'", _newConnection);
                }
                
                _newCommand.ExecuteNonQuery();
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        // Method to search a product using name (returns a table containing product data)
        public DataTable SearchProduct(String name)
        {
            DataTable _newTable = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT * FROM Product WHERE ProdName = '" + name + "'", _newConnection);
                _newCommand.ExecuteNonQuery();
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        // Method to search an order using id (returns a table containing order data)
        public DataTable SearchOrder(int id)
        {
            DataTable _newTable = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT * FROM Orders WHERE OrdID = '" + id + "'", _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public int GetSoldQuantity(int id)
        {
            int value = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT SoldQty FROM Product WHERE ProdID = '" + id + "'", _newConnection);
                SqlDataReader reader = _newCommand.ExecuteReader();
                reader.Read();
                value = int.Parse(reader["SoldQty"].ToString());
                _newConnection.Close();
            }
            return value;
        }


        // Method to search an order using customer id and order date (returns a table containing order data)
        public DataTable GetAllOrder()
        {
            DataTable _newTable = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT * FROM Orders", _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public DataTable GetAllSuppliers()
        {
            DataTable _newTable = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT * FROM Supplier", _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public DataTable GetAllProducts()
        {
            DataTable _newTable = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT * FROM Product", _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public DataTable SearchCategory(int id)
        {
            DataTable _newTable = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT * FROM Category WHERE CatID = '" + id + "'", _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public String SearchCategory(string name)
        {
            string id = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT CatID FROM Category WHERE CatName = '" + name + "'", _newConnection);
                SqlDataReader reader = _newCommand.ExecuteReader();
                while (reader.Read())
                {
                    id = reader["CatID"].ToString();
                }
            }
            return id;
        }

        //working and is used
        public DataTable SearchCategoryLike(string id)
        {
            DataTable _newTable = null;
            string query = "SELECT * FROM Category WHERE (CatName LIKE \'%" + (id + "%\')");
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand(query, _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public DataTable SearchOrderLike(string id)
        {
            DataTable _newTable = null;
            string query = "SELECT * FROM Orders WHERE (CustID LIKE \'%" + (id + "%\')");
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand(query, _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public DataTable SearchCategoryLike(string id, string name)
        {
            DataTable _newTable = null;
            string query = "SELECT CatName FROM Category WHERE (CatName LIKE \'%" + (id + "%\')");
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand(query, _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public DataTable SearchCustomerLike(string name)
        {
            DataTable _newTable = null;
            string query = "SELECT * FROM Customer WHERE (CustFirstName LIKE \'%" + (name + "%\')");
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand(query, _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public DataTable SearchSupplierLike(string name)
        {
            DataTable _newTable = null;
            string query = "SELECT * FROM Supplier WHERE (SupName LIKE \'%" + (name + "%\')");
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand(query, _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        // Method to update any single field value of a given table
        public Boolean UpdateEntity(String table, int id, String field, String value, String idField)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("UPDATE " + table + " SET " + field + " = '" + value + "' WHERE " + idField + " = '" + id + "'", _newConnection);
                temp = _newCommand.ExecuteNonQuery();
                _newConnection.Close();
            }
            if (temp == 1) return true;
            else return false;
        }

        //working and is used
        // returns true if the employee is in the database (for employee login purposes)
        public int LoginEmployee(int id, String password)
        {
            int temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                this._newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT EmpPrivilegeLevel FROM Employee WHERE EmpID = '" + id + "' AND EmpPassword = '" + password + "'", _newConnection);
                SqlDataReader _newReader = _newCommand.ExecuteReader();
                while (_newReader.Read())
                {
                    if (_newReader["EmpPrivilegeLevel"].ToString() == "1") temp = 1;

                    else if (_newReader["EmpPrivilegeLevel"].ToString() == "2") temp = 2;
                }
                _newConnection.Close();
                return temp;
            }
        }

        // working and is used
        public string GetLastEnteredID(string table, string field)
        {
            string query = "SELECT MAX(" + field + ") FROM " + table;
            string temp = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand(query, _newConnection);
                SqlDataReader reader = _newCommand.ExecuteReader();
                while (reader.Read())
                {
                    temp = reader[0].ToString();
                }
                _newConnection.Close();
            }
            return temp;
        }

        public DataTable GetAllCustomers()
        {
            DataTable _newTable = null;
            string query = "SELECT * FROM Customer";
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand(query, _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public DataTable GetAllCategories()
        {
            DataTable _newTable = null;
            string query = "SELECT * FROM Category";
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand(query, _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }


        public DataTable GetAllEmployees()
        {
            DataTable _newTable = null;
            string query = "SELECT * FROM Employee";
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand(query, _newConnection);
                SqlDataAdapter _newAdapter = new SqlDataAdapter(_newCommand);
                _newTable = new DataTable();
                _newAdapter.Fill(_newTable);
                _newConnection.Close();
            }
            return _newTable;
        }

        public List<float> GetTotalSales(int id)
        {
            List<float> _newList = new List<float>();
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT PricePerUnit FROM Customer c INNER JOIN Orders o ON c.CustID = o.CustID INNER JOIN OrderDetails od ON o.OrdID = od.OrdID WHERE c.CustID = '" + id + "' ", _newConnection);
                SqlDataReader _newReader = _newCommand.ExecuteReader();
                while (_newReader.Read())
                {
                    _newList.Add(float.Parse(_newReader["PricePerUnit"].ToString()));
                }
                _newConnection.Close();
            }
            return _newList;
        }

        public List<float> GetTotalOrders(int id)
        {
            List<float> _newList = new List<float>();
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT OrdID FROM Orders o INNER JOIN Customer c ON c.CustID = o.CustID WHERE c.CustID = '" + id + "' ", _newConnection);
                SqlDataReader _newReader = _newCommand.ExecuteReader();
                while (_newReader.Read())
                {
                    _newList.Add(float.Parse(_newReader["OrdID"].ToString()));
                }
                _newConnection.Close();
            }
            return _newList;
        }

        public string GetLastOrderDate(int id)
        {
            string temp = null;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT MAX(OrdDate) AS OrdDate FROM Orders o INNER JOIN Customer c ON o.CustID = c.CustID WHERE c.CustID = '" + id + "' ", _newConnection);
                SqlDataReader _newReader = _newCommand.ExecuteReader();
                _newReader.Read();
                temp = _newReader["OrdDate"].ToString();
                _newConnection.Close();
            }
            return temp;

        }

        public int GetTotalShiftsWorked(int id)
        {
            int _temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT COUNT(wl.WorkDate) AS Total FROM EmployeeWorkLog wl INNER JOIN Employee e ON wl.EmpID = e.EmpID WHERE e.EmpID = '" + id + "' ", _newConnection);
                SqlDataReader _newReader = _newCommand.ExecuteReader();
                _newReader.Read();
                _temp = int.Parse(_newReader["Total"].ToString());
                _newConnection.Close();
            }
            return _temp;
        }

        public int GetTotalProductsSupplied(int id)
        {
            int _temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT COUNT(ProdID) AS Total FROM ProductSupplier ps INNER JOIN Supplier s ON ps.SupID = s.SupID WHERE s.SupID = '" + id + "' ", _newConnection);
                SqlDataReader _newReader = _newCommand.ExecuteReader();
                _newReader.Read();
                _temp = int.Parse(_newReader["Total"].ToString());
                _newConnection.Close();
            }
            return _temp;
        }

        public int GetTotalProductsOrdered(int id)
        {
            int _temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT COUNT(od.OrdID) AS Total FROM OrderDetails od INNER JOIN Orders o ON od.OrdID = o.OrdID WHERE o.OrdID = '" + id + "' ", _newConnection);
                SqlDataReader _newReader = _newCommand.ExecuteReader();
                _newReader.Read();
                _temp = int.Parse(_newReader["Total"].ToString());
                _newConnection.Close();
            }
            return _temp;
        }

        public int GetTotalOrderSales(int id)
        {
            int _temp = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT SUM(PricePerUnit) AS Total FROM OrderDetails od INNER JOIN Orders o ON od.OrdID = o.OrdID WHERE o.OrdID = '" + id + "' ", _newConnection);
                SqlDataReader _newReader = _newCommand.ExecuteReader();
                _newReader.Read();
                _temp = int.Parse(_newReader["Total"].ToString());
                _newConnection.Close();
            }
            return _temp;
        }

        public int GetTotalProductSales(int id)
        {
            int value = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT (SoldQty * ProdRetailPrice) AS Total FROM Product WHERE ProdID = '" + id + "'", _newConnection);
                SqlDataReader reader = _newCommand.ExecuteReader();
                reader.Read();
                value = int.Parse(reader["Total"].ToString());
                _newConnection.Close();
            }
            return value;
        }

        public int GetTotalHoursWorked(int id)
        {
            int value = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT SUM(StartTime-EndTime) AS TotalTime FROM EmployeeWorkLog WHERE EmpID = '" + id + "'", _newConnection);
                SqlDataReader reader = _newCommand.ExecuteReader();
                reader.Read();
                value = int.Parse(reader["TotalTime"].ToString());
                _newConnection.Close();
            }
            return value;
        }
        
        public int GetTotalProductValue(int id)
        {
            int value = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT SUM(WholesalePrice) AS Total FROM ProductSupplier ps INNER JOIN Supplier s ON ps.SupID = s.SupIID WHERE s.SupID = '" + id + "' ", _newConnection);
                SqlDataReader reader = _newCommand.ExecuteReader();
                reader.Read();
                value = int.Parse(reader["Total"].ToString());
                _newConnection.Close();
            }
            return value;
        }

        public int GetLastServedEmployee(int id)
        {
            int value = 0;
            using (_newConnection = new SqlConnection(_connectionString))
            {
                _newConnection.Open();
                SqlCommand _newCommand = new SqlCommand("SELECT EmpID FROM OrderDetails od INNER JOIN Customer c ON od.CustID = c.CustID WHERE c.CustID = '" + id + "' AND MAX(OrdDate)", _newConnection);
                SqlDataReader reader = _newCommand.ExecuteReader();
                reader.Read();
                value = int.Parse(reader["Total"].ToString());
                _newConnection.Close();
            }
            return value;
        }
    }
}
