<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexPage.aspx.cs" Inherits="ImageSaver.Pages.IndexPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ObjectDataSource runat="server" ID="ImageItemDataSource"
                DataObjectTypeName="ImageSaver.DAL.Entities.ImageItem"
                DeleteMethod="DeleteImageItem"
                InsertMethod="InsertImageItem"
                SelectMethod="GetAllImageItems"
                TypeName="ImageSaver.DAL.ImageDAO" OnDeleting="ImageItemDataSource_Deleting"></asp:ObjectDataSource>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ImageItemDataSource" DataKeyNames="ID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="False"></asp:BoundField>
                    <asp:BoundField DataField="Url" HeaderText="Url" SortExpression="Url"></asp:BoundField>
                    <asp:TemplateField HeaderText="ImageFromFile">
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%#"/Downloads/"+(string)Eval("FileName") %>' Width="100" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ImageFromDataBase">
                        <ItemTemplate>
                            <asp:Image runat="server" ImageUrl='<%# "data:image/jpg;base64," + Convert.ToBase64String((byte[])Eval("ImageData")) %>' Width="100" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066"></FooterStyle>

                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"></HeaderStyle>

                <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066"></PagerStyle>

                <RowStyle ForeColor="#000066"></RowStyle>

                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White"></SelectedRowStyle>

                <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>

                <SortedAscendingHeaderStyle BackColor="#007DBB"></SortedAscendingHeaderStyle>

                <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>

                <SortedDescendingHeaderStyle BackColor="#00547E"></SortedDescendingHeaderStyle>
            </asp:GridView>

        </div>
    </form>
</body>
</html>
