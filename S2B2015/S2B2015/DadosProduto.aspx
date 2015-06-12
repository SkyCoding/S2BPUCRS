<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DadosProduto.aspx.cs" Inherits="S2B2015.DadosProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <style type="text/css">
                .StarCss { 
                    background-image: url(/pic/EmptyStar.png);
                    height:24px;
                    width:24px;
                    font-size: 0pt;
                    margin: 0px;
                    padding: 0px;
                    cursor: pointer;
                    display: block;
                    background-repeat: no-repeat;
                }
                .FilledStarCss {
                    background-image: url(/pic/Filled_star.png);
                    height:24px;
                    width:24px;
                }
                .EmptyStarCss {
                    background-image: url(/pic/EmptyStar.png);
                    height:24px;
                    width:24px;
                }
                .WaitingStarCss {
                    background-image: url(/pic/SavedStar.png);
                    height:24px;
                    width:24px;
                }
            </style>
    
    <div class="container">
    <div class="row jumbotron">

        <div class="row" >
            <asp:Label ID="lblTitulo" CssClass="h1" runat="server" Text="lbl"></asp:Label>
        </div>

        <div class="col-md-8" >
            <asp:Image ID="imgProduto" CssClass="img-rounded img-responsive" runat="server" Height="500px" ImageUrl="https://www.upperty.com/images/misc/no-products-found.png" Width="500px" />
        </div>

        <div class="col-md-4" 
             
            <br />
            <div class="row">                           
                <asp:Label ID="lblPreco" CssClass="h1" runat="server" Text="$$$" ForeColor="Black"></asp:Label>
                <br />
                <asp:Label ID="lblFrete" CssClass="h1" runat="server" Text="Frete:" ForeColor="Black"></asp:Label>                
                <asp:Label ID="lblerrofrete"  runat="server" Text="" ForeColor="Red"></asp:Label>                
                <br />
             </div>      
                     
            <div class="row">
                <asp:TextBox ID="txtCep" CssClass="form-control" runat="server" Text="Digite seu CEP" Width="50%"> </asp:TextBox>  
                <asp:Button ID="btncep" CssClass="btn  btn-default col-md-6" runat="server" Text="Calcular" OnClick="btncep_Click" Width="25%" />              
             
            </div>    
            <div class="row">
                <asp:Button ID="btnComprar" CssClass="btn  btn-success col-md-6" runat="server" Text="COMPRAR" OnClick="btnComprar_Click" />                
            </div> 
            <div class="row">
                <asp:Button ID="btnVender" CssClass="btn  btn-success col-md-6" runat="server" Text="Confirmar Venda" OnClick="btnVender_Click" BackColor="Blue" />                
            </div>
            <div class="row">
                <asp:Button ID="btnCancelar" CssClass="btn  btn-danger col-md-6" runat="server" Text="Cancelar Anúncio" OnClick="btnCancelar_Click"  />                
            </div>

            <div class="row">
                <div runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
                    <ContentTemplate>
                        <ul>
                            <li> <asp:Label ID="lblRating" runat="server" Text="Avaliação do vendedor:" ForeColor="Black"></asp:Label>
                                <br />           

                                                                    
                                <ajaxToolkit:Rating
                                        ClientIDMode="AutoID"
                                        ID="RatingCompra"
                                        runat="server"
                                        MaxRating="5"
                                        CurrentRating="0"
                                        StarCssClass="StarCss"
                                        FilledStarCssClass="FilledStarCss"
                                        EmptyStarCssClass="EmptyStarCss"
                                        WaitingStarCssClass="WaitingStarCss"
                                        AutoPostBack="true"
                                        OnChanged="AvaliarCompra" Enabled="False"
                                        >
                                    </ajaxToolkit:Rating>

                                        </li>
                                </ul>
                    </ContentTemplate>
                </div>  
            </div>



            <br />

            <div class="row">
                <asp:Button ID="btnListaUsuario" CssClass="btn  btn-success col-md-12" runat="server" Text="Mais produtos anunciador por este usuario." OnClick="btnListaUsuario_Click" BackColor="Gray" />                
            </div>

            <br />

            <div class="row">
                <asp:Button ID="btnListaCategoria" CssClass="btn  btn-success col-md-12" runat="server" Text="Mais produtos dessa categoria" OnClick="btnListaCategoria_Click" BackColor="Gray" />                
            </div>

            
        </div> 
    </div>
        <div id="desc">

        </div>
    </div>

    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
            
            <div class="panel panel-default">
            <div class="panel-heading" role="tab" id="headingOne">
                <h4 class="panel-title">
                <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                    Descrição do Produto:
                </a>
                </h4>
            </div>
            <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                <div class="panel-body">
                
                   <div class="row jumbotron">
                        <div class="col-lg-12">
                            <asp:PlaceHolder ID="phDescricao" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>

                </div>
            </div>
            </div>

            <div class="panel panel-default">
            <div class="panel-heading" role="tab" id="headingTwo">
                <h4 class="panel-title">
                <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                    Perguntas e Respostas:
                </a>
                </h4>
            </div>
            <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                <div class="panel-body">
                    <div ID="dvPErgunta" class="row jumbotron" runat="server">
                        <div class="col-lg-12">
                        <asp:Label ID="lblPergunta" runat="server" Text="Deixe aqui sua pergunta:"></asp:Label>
                        <asp:TextBox CssClass="form-control" ID="txtPergunta" runat="server" Width="100%" ></asp:TextBox>
                         <asp:Button ID="btnSalvar" CssClass="btn btn-success form-control" runat="server" Text="Enviar" OnClick="btnEnviarPergunta_Click" />
                        </div>

                         <asp:ListView ID="listaPerguntas" runat="server"
                            DataKeyNames="produtoId" GroupItemCount="1"
                            ItemType="S2B2015.Models.PerguntaViewModel" SelectMethod="CarregaPerguntas">
                            <EmptyDataTemplate>
                                <table>
                                    <tr>
                                        <td>Seja o primeiro a fazer uma pergunta!</td>
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
                                            <%#:Item.strPergunta%>

                                            <br />
                                            <label runat="server"> <%#:Item.dtPergunta%></label>
                                            
                                            <br />
                                            <hr />
                                            <br />

                                            
                                            <%#:Item.strRespostas%>
                                            <br />
                                            <label runat="server"> <%#:Item.dtResposta%></label>

                                        </td>

                                        
                                </table>
                                    </p>
                                </td>
                            </ItemTemplate>
                        </asp:ListView>



                    </div>
                </div>
            </div>
            </div>
        </div>





    
    

    



   

</asp:Content>
