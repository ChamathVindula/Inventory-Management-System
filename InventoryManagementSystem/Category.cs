using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace InventoryManagementSystem
{
    public class Category : User
    {
        private string _name;
        private string _description;
        private List<Product> _productsList;

        public Category(int id, string name, string desc) : base(id)
        {
            _name = name;
            _description = desc;
            _productsList = new List<Product>();
        }

        // properties to return category specific attributes 
        public string Name
        {
            get { return _name; }
        }

        public String Description
        {
            get { return _description; }
        }

        public void AddProduct(Product p)
        {
            _productsList.Add(p);
        }

        public List<Product> ProductList
        {
            get { return _productsList; }
        }

        // Adds a new category to the database
        public override void Add()
        {
            base.newQuery.AddCategory(ID, _name, _description);
        }

       // Alters any field of a given category
        public override void Edit(string field, string value)
        {
            base.newQuery.UpdateEntity("Category", ID, field, value, "CatID");
        }

        // Currently it is set to search for the total number of products available in each category
        public override int Search()
        {
            return this._productsList.Count();
        }

        // Populates the list with all products of the same category
        public void PopulateProductList()
        {
            DataTable dt = base.newQuery.SearchProductByCategory(base.ID, null);
            DataRowCollection dc = dt.Rows;
            foreach (DataRow d in dc)
            {
                Product p = new Product(int.Parse(d["ProdID"].ToString()), d["ProdName"].ToString(), d["ProdDescription"].ToString(), float.Parse(d["ProdRetailPrice"].ToString()), int.Parse(d["ProdStockQuantity"].ToString()), float.Parse(d["Discount"].ToString()), int.Parse(d["SoldQty"].ToString()), int.Parse(d["CatID"].ToString()));
                _productsList.Add(p);
            }
            
        }

        // locates and retrives a product of the same category from the list
        public Product GetProduct(int id)
        {
            foreach (Product p in _productsList)
            {
                if (p.ID == id)
                {
                    return p;
                }
            }
            return null;
        }
    }
}
