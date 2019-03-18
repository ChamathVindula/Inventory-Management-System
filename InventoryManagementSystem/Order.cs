using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace InventoryManagementSystem
{
    public class Order : User
    {
        string _orderDate;
        string _shipDate;
        int _custID;
        int _empID;
       List<Product> _orderItems;

        public Order(int id, string ordDate, string shpDate, int custId, int empId) : base(id)
        {
            _orderDate =  ordDate;
            _shipDate = shpDate;
            _custID = custId;
            _empID = empId;
            _orderItems = new List<Product>();
        }
        
        // properties to return order specific attributes
        public string OrderDate
        {
            get { return _orderDate; }
        }

        public string ShipeDate
        {
            get { return _shipDate; }
        }

        public int CustID
        {
            get { return _custID; }
        }

        public int EmpID
        {
            get { return _empID; }
        }

        public List<Product> OrderItems
        {
            get { return _orderItems; }
            set { _orderItems = value; }
        }

        // Adds a new order to the database 
        public override void Add()
        {
            base.newQuery.AddOrder(base.ID, _orderDate, _shipDate, _custID, _empID);
        }

        // Alters any given field of an order
        public override void Edit(string field, string value)
        {
            base.newQuery.UpdateEntity("Orders", base.ID, field, value, "OrdID");
        }

       // Currently it is set to search for all the products ordered for a single order
        public override int Search()
        {
            return base.newQuery.GetTotalProductsOrdered(base.ID);
        }

        // Adds an item to the item list of the order
        public void AddItem(Product p)
        {
            _orderItems.Add(p);
        }

        // Adds all the items of the same order to the OrderDetails table
        public void AddOrderItems()
        {
            foreach (Product p in _orderItems)
            {
                base.newQuery.AddOrderDetails(base.ID, p.ID, p.Price);
                p.Edit("ProdStockQuantity", (newQuery.GetProductStockQunatity(p.ID) - 1).ToString());
                p.Edit("SoldQty", (newQuery.GetProductSoldQunatity(p.ID) + 1).ToString());
            }
        }

        // Gets the total sales of the current order
        public string GetTotalSales()
        {
            return base.newQuery.GetTotalOrderSales(base.ID).ToString();
        }
    }
}
