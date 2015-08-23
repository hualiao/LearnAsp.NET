using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrontEnd
{
    public partial class InlineExpression : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _testValue = "2";
        }
        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
             DataBind();
            _testValue = "3";
        }

        public string TestValue
        {
            get { return _testValue; }
        }

        private string _testValue = "1";
    }
}