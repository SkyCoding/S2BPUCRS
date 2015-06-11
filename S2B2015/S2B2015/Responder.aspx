<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Responder.aspx.cs" Inherits="S2B2015.Responder" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


     <div class="container">

        
        <br />
        <br />
        <div class="row">
            <div class="col-md-12">
                <asp:Label  ID="lblHeader" runat="server" ></asp:Label><br />
                 <asp:Label CssClass="h1"  ID="lblPergunta" runat="server" ></asp:Label><br />
                <asp:TextBox ID="txtResposta" runat="server" Width="60%" ></asp:TextBox>
                <asp:Label ID="lblErroResposta" runat="server"  ForeColor="Red"></asp:Label>
                <asp:Button ID="btnResponder" CssClass="btn-lg btn-succes" Width="50%" runat="server" Text="Enviar resposta" OnClick="btnResponder_Click" />
                <br />
            </div>
        </div>

     </div>
</asp:Content>
