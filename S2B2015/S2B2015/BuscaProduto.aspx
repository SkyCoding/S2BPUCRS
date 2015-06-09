<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuscaProduto.aspx.cs" Inherits="S2B2015.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                        <a style="display:inline-block" href="DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>">
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
                        <a href="DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>"><%#: Item.strTitulo%></a>
                    </td>
                    <td style="width:100px">
                        <%#:String.Format("{0:c}", Item.Preco)%>
                    </td>
                    <td style="width:300px">
                        <a href="BuscaProduto.aspx?Categoria=<%#:Item.CategoriaId%>"><%#: Item.oCategoria.strTitulo%></a>                       
                    </td>                    
                    <%--<td style="width:200px">
                        <%#: Item.nEstado%>
                    </td>--%>
                          
            </table>
                </p>
            </td>
        </ItemTemplate>
    </asp:ListView>


</asp:Content>