<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuscaProduto.aspx.cs" Inherits="S2B2015.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel">
        <asp:Label CssClass="h2" ID="lblTitulo" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label CssClass="h2" ID="lblNumeroResultados" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label CssClass="h2" ID="lblValores" runat="server" Text="Label"></asp:Label>
    </div>


    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
            
        <div class="panel panel-default">
        <div class="panel-heading" role="tab" id="headingOne">
            <h4 class="panel-title">
            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                Pesquisa avançada:
            </a>
            </h4>
        </div>
        <div   id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
            <div class="panel-body">
                <%--EXEMPLO--%>
                
                <div class="row">  
                    <div class="col-md-8">                
                        <div class="row">       
                            <div class="col-md-12">
                                <asp:Label ID="lblNome" runat="server" Text="Nome"></asp:Label><br />
                                <asp:TextBox CssClass="form-control" ID="txtNomeCategoria" runat="server" Width="100%"></asp:TextBox>
                                <asp:Label ID="lblerrorname" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                
                            <div class="col-md-12">
                                <asp:Label ID="lblDescrição" runat="server" Text="Descrição"></asp:Label><br />
                                <asp:TextBox CssClass="form-control" ID="txtDescricaoCategoria" runat="server" Width="100%"></asp:TextBox>
                                <asp:Label ID="lblerroDescr" runat="server" ForeColor="Red"></asp:Label>
                            </div>                
                        </div>            
                    </div>
                    <div class="col-md-4">      
                        <br />
                        <asp:Button ID="btnPesquisar" Height="100px" Width="100%" CssClass="btn btn-success form-control" runat="server" Text="Pesquisar" />
                    </div>

            </div>
        </div>
        </div>
    </div>
    
                        <br />

    <asp:ListView ID="produtList" runat="server"
        DataKeyNames="produtoId" GroupItemCount="1"
        ItemType="S2B2015.Models.ProdutoViewModel" SelectMethod="GetProdutos">
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