
<%@ Page  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="S2B2015.AdminPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="btnRelatorioItem" runat="server" Text="Gerar Relatório" OnClick="btnRelatorioItem_Click" />
    </br>
    <asp:Label ID="lblRelatorioItem" runat="server" Text=""></asp:Label>
    </br>
    <asp:Label ID="lblCriarCategoria" runat="server" Text="Editar Categorias"></asp:Label>
    <asp:DropDownList ID="ListaCategorias" runat="server" OnSelectedIndexChanged="ListaCategorias_SelectedIndexChanged">
        <asp:ListItem>Nova Categoria</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="btnCriar" runat="server" Text="Salvar" OnClick="btnCriar_Click" />
    </br>
    <asp:Label ID="lblNome" runat="server" Text="Nome"></asp:Label><br />
    <asp:TextBox ID="txtNomeCategoria" runat="server" Width="240px"></asp:TextBox>
    <asp:Label ID="lblerrorname" runat="server" ForeColor="Red"></asp:Label>
    </br>
    <asp:Label ID="lblDescrição" runat="server" Text="Descrição"></asp:Label><br />

    <asp:TextBox ID="txtDescricaoCategoria" runat="server" Height="56px" Width="326px"></asp:TextBox>
    <asp:Label ID="lblerroDescr" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <br />
    <br />

    <asp:Label ID="lblRelatorioUsuarios" runat="server" Text="Relatório por usuários"></asp:Label>
    <asp:DropDownList ID="drpListaUsuarios" runat="server" OnSelectedIndexChanged="drpListaUsuarios_SelectedIndexChanged"></asp:DropDownList>
    <asp:Button ID="btnRelatorioUsuarios" runat="server" Text="ver Relatorio" OnClick="btnRelatorioUsuarios_Click" Width="126px" /><br />
    <asp:Label ID="lblResultadoUsuarios" runat="server" Text=""></asp:Label>
    <asp:LinkButton ID="lnkItensOfertados" runat="server" OnClick="lnkItensOfertados_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkVendidos" runat="server" OnClick="lnkVendidos_Click"></asp:LinkButton>
</asp:Content>
