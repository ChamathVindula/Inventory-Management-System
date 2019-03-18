using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public class Customer : User
    {
        private string _firstName;
        private string _lastName;
        private Int64 _phoneNumber;
        private string _street;
        private string _city;
        private string _state;
        private int _areaCode;
        private List<string> _fieldNameList; 

        public Customer(int id, string fname, string lname, Int64 phone, string street, string city, string state, int area) : base(id)
        {
            _firstName = fname;
            _lastName = lname;
            _phoneNumber = phone;
            _street = street;
            _city = city;
            _state = state;
            _areaCode = area;
            _fieldNameList = new List<string>() { "CustID", "CustFirstName", "CustLastName", "CustPhoneNumber", "CustStreetAddress", "CustCity", "CustState", "CustAreaCode" };
        }

        // properties to return employee specific attributes
        public string FirstName
        {
            get { return _firstName; }
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public Int64 PhoneNumber
        {
            get { return _phoneNumber; }
        }

        public string Street
        {
            get { return _street; }
        }

        public string City
        {
            get { return _city; }
        }

        public string State
        {
            get { return _state; }
        }

        public int AreaCode
        {
            get { return _areaCode; }
        }

        // adds a new customer to the database
        public override void Add()
        {
            base.newQuery.AddCustomer(ID, _firstName, _lastName, _phoneNumber, _street, _city, _state, _areaCode);
        }

        // alters any field of any given customer
        public override void Edit(String field, String value)
        {
            if (_fieldNameList.Contains(field))
            {
                base.newQuery.UpdateEntity("Customer", ID, field, value, "CustID");
            }
        }

        // currently checks for the employee who has served the customer in the last order
        public override int Search()                  
        {
            return base.newQuery.GetLastServedEmployee(ID);
        }

        // returns the total amount of all sales of one customer
        public string CalculateTotalSales()
        {
            float _totalSales = 0;
            List<float> _newList = base.newQuery.GetTotalSales(this.ID);

            foreach(float value in _newList)
            {
                _totalSales += value;
            }
            return _totalSales.ToString();
        }

        // returns the total amount of orders placed by a customer
        public string CalculateTotalOrdersPlaced()
        {
            int _totalOrders = 0;
            List<float> _newList = base.newQuery.GetTotalOrders(this.ID);

            foreach(float value in _newList)
            {
                _totalOrders++;
            }
            return _totalOrders.ToString();
        }

        // returns the date of the most recent order placed
        public string GetLastOrderDate()
        {
            return base.newQuery.GetLastOrderDate(this.ID);
        }
    }
}
