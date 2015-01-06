using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CKEditorDemo
{
    public partial class CKEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string content = Request.Form["editor1"];
            CKeditorControl1.Text = content;
        }

        protected void CKeditorControl1_Load(object sender, EventArgs e)
        {
            CKFinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
            _FileBrowser.BasePath = "PlugIns/ckfinder/";
            _FileBrowser.SetupCKEditor(CKeditorControl1);
        }
    }
}