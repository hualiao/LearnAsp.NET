<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomConfiguration.aspx.cs" Inherits="WebApp.CustomConfiguration" %>

<!DOCTYPE html>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        WebApp.Helpers.PageAppearanceSection config =
            (WebApp.Helpers.PageAppearanceSection)System.Configuration.ConfigurationManager.GetSection(
            "pageAppearanceGroup/pageAppearance");

        Response.Write("<h2>Settings in the PageAppearance Section:</h2>");
        Response.Write(string.Format("RemoteOnly: {0}<br>",
            config.RemoteOnly));
        Response.Write(string.Format("Font name and size: {0} {1}<br>",
            config.Font.Name, config.Font.Size));
        Response.Write(
            string.Format("Background and foreground color: {0} {1}<br>",
            config.Color.Background, config.Color.Foreground));
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
    </div>
    </form>
</body>
</html>
