<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="S2B2015._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1 style="align-content:center">Bem vindo ao S2B Market!</h1>
        
    <div class="row">
        
        <div class="col-md-12 list-group">            
           
            <ul>
              <asp:ListView ID="categoryList"  
                        ItemType="S2B2015.Models.Categoria" 
                        runat="server"
                        SelectMethod="GetCategories" >
                        <ItemTemplate>
                            <b style="font-size: large; font-style: normal">
                            <li>
                             <a class="list-group-item" href="BuscaProduto.aspx?Categoria=<%#:Item.CategoriaId %>">
                                <%#: Item.strTitulo %>
                            </a>
                            </li>
                            </b>
                        </ItemTemplate>
                </asp:ListView>
            </ul>


        </div>
       
    </div>
    </div>

</asp:Content>
