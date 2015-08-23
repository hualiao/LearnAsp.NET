using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace FrontEnd
{
    public partial class GraphficsDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Server.MapPath(".") + "<br/>");
            Response.Write(Server.MapPath("~") + "<br/>");
            Response.Write(Server.MapPath("/") + "<br/>");
            Response.Write(Server.MapPath("copy.jpg") + "<br/>");
            Response.Write(System.Web.Hosting.HostingEnvironment.MapPath("/copy.jpg") + "<br/>");
            System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath("copy.jpg"));
            Graphics g = Graphics.FromImage(img);
            g.FillEllipse(Brushes.Red, 0, 0, 100, 100);
            img.Save(Server.MapPath("copy1.jpg"));
            ImageHandler imgHander = new ImageHandler();
            imgHander.Save((Bitmap)img, 300, 300, 90, Server.MapPath("resize.jpg"));
            imgHander.Resize((Bitmap)img, 300, 300, Server.MapPath("resize1.jpg"));
            img.Dispose();
            g.Dispose();
        }
    }
}