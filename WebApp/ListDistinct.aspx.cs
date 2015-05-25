using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class ListDistinct1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListDistinct repo = new ListDistinct();
                //Option 1: Using a combination of LINQ GroupBy and Select operators
                grv_Products.DataSource = repo.GetProducts()
                                          .GroupBy(o => new { o.Make, o.Model })
                                          .Select(o => o.FirstOrDefault());
                //Option 2: Using a combination of LINQ Select and Distinct operators
                grv_Products.DataSource = repo.GetProducts()
                                          .Select(o => new { o.Make, o.Model })
                                          .Distinct();
                //Option 3: Using the IEqualityCompare<T> interface
                grv_Products.DataSource = repo.GetProducts()
                                            .Distinct(new ProductComparer());
                grv_Products.DataBind();

                //Option 1: Using GroupBy and Select operators
                drp_ProductNames.DataSource = repo.GetProducts()
                                                  .GroupBy(o => o.Make)
                                                  .Select(o => o.FirstOrDefault());
                //Option 2: Using Select and Distinct operators
                drp_ProductNames.DataSource = repo.GetProducts()
                                                  .Select(o => new { o.Make })
                                                  .Distinct();

                drp_ProductNames.DataTextField = "Make";
                drp_ProductNames.DataValueField = "Make";
                drp_ProductNames.DataBind();
            }
        }
    }
}