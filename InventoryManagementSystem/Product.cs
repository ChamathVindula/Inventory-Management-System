using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace InventoryManagementSystem
{
    public class Product : User
    {
        private string _name;
        private string _description;
        private float _price;
        private int _stockQty;
        private float _discount;
        private int _amountSold;
        private int _catID;

        public Product(int id, string name, string desc, float price, int stock, float discount, int sold, int catid) : base(id)
        {
            _name = name;
            _description = desc;
            _price = price;
            _stockQty = stock;
            _discount = discount;
            _amountSold = sold;
            _catID = catid;
        }

        // Properties to return Product specific attributes
        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public float Price
        {
            get { return _price; }
        }

        public int StockQty
        {
            get { return _stockQty; }
        }

        public float Discount
        {
            get { return _discount; }
        }

        public int AmountSold
        {
            get { return _amountSold; }
        }

        public int CatId
        {
            get { return _catID; }
        }

        // Adds a new product to the database
        public override void Add()
        {
            base.newQuery.AddProduct(ID, _name, _description, _price, _stockQty, _catID, _discount, _amountSold);
        }

        // Alters any field of a given product 
        public override void Edit(string field, string value)
        {
            base.newQuery.UpdateEntity("Product", ID, field, value, "ProdID");
        }

        // Currently it is set to search for the total sales of the current product
        public override int Search()
        {
            return base.newQuery.GetTotalProductSales(ID);
        }

        // Calculates the discounted proce of the product (if a discount is available)
        public float DiscountedPrice()
        {
            return _price - ((_price * _discount) / 100);
        }

        // returns the number of units sold of a specific product
        public string SoldQuantity()
        {
            return base.newQuery.GetSoldQuantity(base.ID).ToString();
        }
    }
}
