using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public class Supplier : User
    {
        private string _name;
        private Int64 _phoneNumber;
        private string _street;
        private string _city;
        private string _state;
        private int _areaCode;
        private string _email;
        private List<string> _fieldNameList;

        public Supplier(int id, string name, string street, string city, string state, int areaCode, string email, Int64 phone) : base(id)
        {
            _name = name;
            _phoneNumber = phone;
            _street = street;
            _city = city;
            _state = state;
            _areaCode = areaCode;
            _email = email;
            _fieldNameList = new List<string>() { "SupID", "SupName", "SupPhoneNumber", "SupStreetAddress", "SupCity", "SupState", "SupAreaCode", "SupEmail" };

        }

        // properties to return employee specific attributes
        public string Name
        {
            get { return _name; }
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

        public string Email
        {
            get { return _email; }
        }

        // adds a new supplier to the database
        public override void Add()
        {
            base.newQuery.AddSupplier(ID, _name, _street, _city, _state, _areaCode, _phoneNumber, _email);
        }

        // alters any field of a given customer
        public override void Edit(string field, string value)
        {
            if (_fieldNameList.Contains(field))
            {
                base.newQuery.UpdateEntity("Supplier", ID, field, value, "SupID");
            }
        }
        
        // currently it is set to search for the current supplier
        public override int Search()              
        {
            return base.newQuery.GetTotalProductValue(ID);
        }

        // returns the toatal number of products supplied a specified supplier
        public string ProductsSupplied()
        {
            return base.newQuery.GetTotalProductsSupplied(this.ID).ToString();
        }
    }
}
