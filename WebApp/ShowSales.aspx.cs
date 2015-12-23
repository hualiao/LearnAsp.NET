using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Helpers;

namespace WebApp
{
    public partial class ShowSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch stopWatch =
                                new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            var sales = Sale.GetSales(null);

            var lastSales = sales.Last();

            stopWatch.Stop();

            lblResults.Text = string.Format(
                        "Count of Sales: {0}, Last DayCount: {1}, Total Sales: {2}. Query took {3} ms",
                        sales.Count(),
                        lastSales.DayCount,
                        lastSales.RunningTotal,
                        stopWatch.ElapsedMilliseconds);
        }
    }
}