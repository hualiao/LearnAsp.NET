using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebApp.DemoObject;

namespace WebApp
{
    public partial class RepeaterInRepeater : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControl();
            }
        }

        protected void BindControl()
        {
            var source = RepeaterInRepeaterModel.GetData();
            this.ParentRepeater.DataSource = source;
            this.ParentRepeater.DataBind();

            this.parentRepeaterControlLevel.DataSource = source;
            this.parentRepeaterControlLevel.DataBind();
        }

        protected void ItemBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater childRepeater = (Repeater)args.Item.FindControl("ChildRepeater");
                Label lblParentId = (Label)args.Item.FindControl("lblParentId");
                int someIdFromParentDataSource = Int32.Parse(lblParentId.Text);
                childRepeater.DataSource = RepeaterInRepeaterModel.GetData().Single(x => x.ParentID == someIdFromParentDataSource).Children;
                childRepeater.DataBind();
            }
        }

        protected void childRepeaterControlLevel_DataBinding(object sender, EventArgs e)
        {
            Repeater rep = (Repeater)(sender);

            int someIdFromParentDataSource = (int)(Eval("ParentID"));

            // Assuming you have a function call `GetSomeData` that will return
            // the data you want to bind to your child repeater.
            rep.DataSource = RepeaterInRepeaterModel.GetData().Single(x => x.ParentID == someIdFromParentDataSource).Children; // GetSomeData(int);
            //rep.DataBind();
        }
    }
}