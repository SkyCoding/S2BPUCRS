<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuscaProduto.aspx.cs" Inherits="S2B2015.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel">
        <asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lblNumeroResultados" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lblValores" runat="server" Text="Label"></asp:Label>
    </div>
    <asp:GridView ID="grdProdutos" runat="server" OnRowCreated="grdProdutos_RowCreated">
    </asp:GridView>
</asp:Content>
