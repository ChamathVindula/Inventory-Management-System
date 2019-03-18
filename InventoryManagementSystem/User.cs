using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;

namespace InventoryManagementSystem
{
    public abstract class User
    {
        private int _id;
        protected Queries newQuery;   // this object is available to all derived classes of User class

        public User(int id)
        {
            _id = id;
            newQuery = new Queries();
        }

        // properties to return the common user attributes
        public int ID
        {
            get { return _id; }
        }

        // abstract method to add a specified entity to the database
        public abstract void Add();

        // abstract method to alter any field of any specified entity table in the database
        public abstract void Edit(String field, String value);

        // virtual method to delete any specified entity from any the database
        public virtual void Delete(string table, string field)
        {
            newQuery.DeleteEntity(table, field, this._id);
        }

        // abstract method to search for any related data of a specific enitity from any other schema
        public abstract int Search(); 
    }
}
