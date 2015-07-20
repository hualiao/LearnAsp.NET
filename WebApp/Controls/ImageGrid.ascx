<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageGrid.ascx.cs" Inherits="WebApp.Controls.ImageGrid" %>

<asp:ListView runat="server" ID="ImageListView" ItemPlaceholderID="itemPlaceHolder" 
     GroupPlaceholderID="groupPlaceHolder" OnItemCommand="ImageListView_ItemCommand">
    <LayoutTemplate>
        <h1>
            <asp:Label Text="" runat="server" ID="titleLabel" OnLoad="titleLabel_Load" />
        </h1>
        <div runat="server" id="groupPlaceHolder">
        </div>
    </LayoutTemplate>
    <GroupTemplate>
        <span>
            <div id="itemPlaceHolder" runat="server"></div>
        </span>
    </GroupTemplate>
    <ItemTemplate>
        <asp:ImageButton ID="itemImageButton" runat="server" 
          CommandArgument="<%# Container.DataItem %>" 
          ImageUrl="<%# Container.DataItem %>" Width="320" Height="240" 
          OnCommand="itemImageButton_Command"/>
        <asp:LinkButton ID="deleteLinkButton" runat="server" CommandName="Remove" 
          CommandArgument="<%# Container.DataItem %>" Text="Delete" Visible="false" 
          OnLoad="deleteLinkButton_Load"  />
    </ItemTemplate>
    <EmptyItemTemplate>
        <td />
    </EmptyItemTemplate>
    <EmptyDataTemplate>
        <h3>No images available</h3>
    </EmptyDataTemplate>
    <InsertItemTemplate>
        <p>
            <asp:Label Text="Please upload an image" runat="server" ID="imageUploadLabel" />
            <asp:FileUpload runat="server" ID="imageUpload" OnLoad="imageUpload_Load" />
            <asp:Button ID="uploadButton" Text="Upload" runat="server" />
        </p>
        <p>
            <asp:Label Text="" runat="server" ID="imageUploadStatusLabel" />
        </p>
    </InsertItemTemplate>
</asp:ListView>
