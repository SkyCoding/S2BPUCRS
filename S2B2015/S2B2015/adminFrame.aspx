<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminFrame.aspx.cs" Inherits="S2B2015.adminFrame" %>
<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> - My ASP.NET Application</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>


    <div>
    
        <div class="panel">
        <asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lblNumeroResultados" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lblValores" runat="server" Text="Label"></asp:Label>
    </div>
    

    <asp:ListView ID="produtList" runat="server"
        DataKeyNames="produtoId" GroupItemCount="1"
        ItemType="S2B2015.Models.ProdutoViewModel" SelectMethod="GetAlbuns">
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td>Nehum produto encontrado.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <EmptyItemTemplate>
            <td />
        </EmptyItemTemplate>
    
        <ItemTemplate>
            <td runat="server">
                
                <table class="table table-striped table-bordered table-hover" style="align-items:center">

                    
                    <td style="width:100px">
                        <a style="display:inline-block" target="_blank" href="DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>">
                            <asp:Image runat="server" ID="Image2" Height="160px"
                                ImageUrl='<%#: Item.strLink%>' />
                        </a>
                    </td>


                    <%--<td style="width:100px">
                        <a href="Editar.aspx?id=<%#:Item.ProdutoId%>">editar</a>
                    </td>
                    <td style="width:100px">
                        <a href="Remover.aspx?id=<%#:Item.ProdutoId%>">remover</a>
                    </td>--%>
                    <td style="width:400px">                        
                        <a target="_blank" href="DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>"><%#: Item.strTitulo%></a>
                    </td>
                    <td style="width:100px">
                        <%#:String.Format("{0:c}", Item.Preco)%>
                    </td>
                    <td style="width:300px">
                        <a target="_blank" href="BuscaProduto.aspx?Categoria=<%#:Item.CategoriaId%>"><%#: Item.oCategoria.strTitulo%></a>                       
                    </td>                    
                    <%--<td style="width:200px">
                        <%#: Item.nEstado%>
                    </td>--%>
                          
            </table>
                </p>
            </td>
        </ItemTemplate>
    </asp:ListView>

    </div>
    </form>
</body>
</html>
