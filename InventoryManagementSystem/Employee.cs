using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public class Employee : User
    {
        private string _firstName;
        private string _lastName;
        private Int64 _phoneNumber;
        private string _street;
        private string _city;
        private string _state;
        private int _areaCode;
        private int _privilage;
        private string _role;
        private string _password;
        private string _dob;
        private List<string> _fieldNameList;

        public Employee(int id, string fname, string lname, Int64 phone, string street, string city, string state, int area, int privilage, string role, string password, string dob) : base(id)
        {
            _firstName = fname;
            _lastName = lname;
            _phoneNumber = phone;
            _street = street;
            _city = city;
            _state = state;
            _areaCode = area;
            _privilage = privilage;
            _role = role;
            _password = password;
            _dob = dob;
            _fieldNameList = new List<string>() { "EmpID", "EmpFirstName", "EmpLastName", "EmpPhoneNumber", "EmpStreetAddress", "EmpCity", "EmpState", "EmpAreaCode", "EmpPrivilegeLevel", "Role", "EmpPassword", "EmpDOB" };
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

        public int Privilage
        {
            get { return _privilage; }
        }

        public string Role
        {
            get { return _role; }
        }

        public string Password
        {
            get { return _password; }
        }

        public string DOB
        {
            get { return _dob; }
        }

        // adds a new employee to the database
        public override void Add()
        {
            base.newQuery.AddEmployee(ID, _firstName, _lastName, _privilage, _street, _city, _state, _areaCode, _role, _phoneNumber, _password, _dob);
        }

        // alters any field of any given employee
        public override void Edit(string field, string value)
        {
            if (_fieldNameList.Contains(field))
            {
                base.newQuery.UpdateEntity("Employee", ID, field, value, "EmpID");
            }
        }

        // currently it is set to search for the total hours worked by each employee
        public override int Search()                      
        {
            return base.newQuery.GetTotalHoursWorked(ID);
        }

        // returns the total number of shifts worked by a cashier
        public string TotalShiftsWorked()
        {
            return base.newQuery.GetTotalShiftsWorked(this.ID).ToString();
        }

        // returns the full user address as one string
        // this method is virtual in case if a derived class wants to set its own version of the address
        public string UserAddress()
        {
            return _street + "" + _city + "" + _state + "" + _areaCode;
        }
    }
}
