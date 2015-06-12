<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NovoProduto.aspx.cs" Inherits="S2B2015.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">






    <div class="container">

        
        <br />
        <br />
        <div class="row">
            <div class="col-md-12">
                <asp:Label CssClass="h1" ID="lblHeader" runat="server" Text="Novo Anúncio"></asp:Label>
            </div>
        </div>

        <br />
      
        <div class="row form-group">
            <div class="col-md-4 form-group">
            <br />
                <div>
                    <asp:Label ID="lblCategoria" for="cboCategorias" runat="server" Text="Categoria:"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" ControlToValidate="cboCategorias" ErrorMessage="Campo obrigatório!" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:DropDownList id="cboCategorias" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
        
        <br />
                <div>
                    <asp:Label ID="lblPreco" for="txtPreco" runat="server" Text="Preço:"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvPreco" runat="server" ControlToValidate="txtPreco" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtPreco" CssClass="form-control" runat="server" OnTextChanged="txtPreco_TextChanged"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revPreco" runat="server" ControlToValidate="txtPreco" ErrorMessage="Valor monetário com dois dígitos após o ponto!. Ex: 1.99" ForeColor="Red" ValidationExpression="^\d+(\.\d\d)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>    
                
        <br />
                <div>
                    <asp:Label ID="lblTitulo" for="txtTitulo" runat="server" Text="Título:"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" ErrorMessage="Campo obrigatório!" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtTitulo" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                
                
        <br />
                
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblLinkImg" for="txtImage"  runat="server" Text="Link da Imagem:"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtImage" ErrorMessage="Campo obrigatório!" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtImage" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <!--<asp:Label ID="Label1" for="btnImage"  runat="server" Text="Pre-Visualizar"></asp:Label>
                        
                        <asp:Button ID="btnImage" CssClass="btn btn-primary" runat="server" Text="Imagem" OnClick="btnImage_Click" />
                        -->

                        
                    </div>
                </div>
                
        <br />
                <div >
                    <asp:Label ID="lblValidade" runat="server" Text="Validade:"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvValidade" runat="server" ControlToValidate="txtValidade" ErrorMessage="Campo obrigatório!" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtValidade" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                
                

            </div>

            <div class="col-md-8">
                <div >
                    <!--
                    <asp:Image ID="imgProduto" CssClass="img-rounded img-responsive" runat="server" Height="400px" ImageUrl="http://www.gazette-ariegeoise.fr/IMG/jpg/test.jpg" Width="500px" />
                     -->
                     </div>
                
            </div>
        
        </div>
        
        
        <br />
        
        <br />
        
        <br />

        <div class="row">
        
            <div class="col-md-12">
                <asp:Label ID="lblDescricao" for="txtDescricao" runat="server" Text="Descrição do Produto:"></asp:Label>                  
                <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" ErrorMessage="Campo obrigatório!" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtDescricao" CssClass="form-control" runat="server" Height="500px" Width="80%" ></asp:TextBox>

            
                <cc1:HtmlEditorExtender TargetControlID="txtDescricao"  ID="HtmlEditorExtenderDescricao" runat="server"></cc1:HtmlEditorExtender>
            </div>
        
        </div>

        <br />

        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="btnSalvar" CssClass="btn-lg btn-primary" Width="80%" runat="server" Text="Finalizar" OnClick="btnSalvar_Click" />
            </div>
        </div>
        
    </div>

    
</asp:Content>
