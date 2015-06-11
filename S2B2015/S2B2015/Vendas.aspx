<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vendas.aspx.cs" Inherits="S2B2015.Vendas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="panel">
    </div>
    

        
            <p>Alertas:</p>


            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
            
            <div class="panel panel-default">
            <div class="panel-heading" role="tab" id="headingOne">
                <h4 class="panel-title">
                <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                    Vendas em aberto
                </a>
                </h4>
            </div>
            <div   id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
               
                 <div class="panel-body">
                
                    
                    <div class="row">
                        <div class="col-md-12">

                            <asp:ListView ID="produtList" runat="server"
                                DataKeyNames="produtoId" GroupItemCount="1"
                                ItemType="S2B2015.Models.ProdutoViewModel" SelectMethod="GetVendas" OnSelectedIndexChanged="produtList_SelectedIndexChanged">
                                <EmptyDataTemplate>
                                    <table>
                                        <tr>
                                             <td>Sem novas negociações.</td> 
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

                                            <td style="width:400px">                        
                                                <a href="DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>"><%#: Item.strTitulo%></a>
                                            </td>
                                            <td style="width:100px">
                                                <%#:String.Format("{0:c}", Item.Preco)%>
                                            </td>
                                            <td style="width:300px">
                                                <a href="BuscaProduto.aspx?Categoria=<%#:Item.CategoriaId%>"><%#: Item.oCategoria.strTitulo%></a>                       
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

                

            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingTwo">
                    <h4 class="panel-title">
                    <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                        Perguntas em aberto
                    </a>
                    </h4>
                </div>
                <div  id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                    <div class="panel-body">
                        <asp:ListView ID="LstPerguntas" runat="server"
                            DataKeyNames="produtoId" GroupItemCount="1"
                            ItemType="S2B2015.Models.ProdutoViewModel" SelectMethod="GetPerguntas" OnSelectedIndexChanged="produtList_SelectedIndexChanged">
                            <EmptyDataTemplate>
                                <table>
                                    <tr>
                                         <td>Sem novas perguntas aos seus anúncios</td> 
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

                                        <td style="width:400px">                        
                                            <a href="DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>"><%#: Item.strTitulo%></a>
                                        </td>
                                        <td style="width:100px">
                                            <%#:String.Format("{0:c}", Item.Preco)%>
                                        </td>
                                        <td style="width:300px">
                                             <%#:(Item.strPergunta)%>
                                        </td>    
                                        <td style="width:300px">
                                            <a href="Responder.aspx?PerguntaId=<%#:Item.nPtId%>"><%#: "Responder"%></a>      
                                        </td>        
                                </table>
                                    </p>
                                </td>
                            </ItemTemplate>
                        </asp:ListView>
                        
                    </div>
                </div>
            </div>



            <hr />


            <p>Seus dados como vendedor</p>

            <div class="row">

                <div class="col-md-6">
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

                              
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="True" 
                                            Enabled="False">
                        <ContentTemplate>
                            <ul>
                                <li> Avaliação dos clientes:
                                    <br />                       


                                    <ajaxToolkit:Rating 
                                            ID="RatingUsuario"
                                            runat="server"
                                            MaxRating="5"
                                            CurrentRating="0"
                                            StarCssClass="StarCss"
                                            FilledStarCssClass="FilledStarCss"
                                            EmptyStarCssClass="EmptyStarCss"
                                            WaitingStarCssClass="WaitingStarCss"
                                            AutoPostBack="true"
                                            OnChanged="Rating1_Changed"
                                            Enabled="False">
                                        </ajaxToolkit:Rating>

                            
                                         </li>
                                 </ul>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <br />  

                
                <div class="col-md-6">
                    <asp:Button ID="btnNovo" CssClass="btn  btn-success col-md-6" runat="server" Text="Anunciar novo produto" OnClick="btnNovo_Click" />                
                </div>
            </div>
                <br />  
            
                

            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingProdutos">
                    <h4 class="panel-title">
                    <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseProdutos" aria-expanded="false" aria-controls="collapseProdutos">
                        
                        <asp:Label ID="lblAnunciosValidos" runat="server" Text="Seus anuncios validos."></asp:Label> 
                        
                    </a>
                    </h4>
                </div>

                <div  id="collapseProdutos" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingProdutos">
                    <div class="panel-body">                
                    
                        <div class="row">
                            <div class="col-md-12">

                                <asp:ListView ID="ListView3" runat="server"
                                    DataKeyNames="produtoId" GroupItemCount="1"
                                    ItemType="S2B2015.Models.ProdutoViewModel" SelectMethod="GetProdutosValidos">
                                    <EmptyDataTemplate>
                                        <table>
                                            <tr>
                                                 <td>Você não possui nem um anuncio.</td> 
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

                                                <td style="width:400px">                        
                                                    <a href="DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>"><%#: Item.strTitulo%></a>
                                                </td>
                                                <td style="width:100px">
                                                    <%#:String.Format("{0:c}", Item.Preco)%>
                                                </td>
                                                <td style="width:300px">
                                                    <a href="BuscaProduto.aspx?Categoria=<%#:Item.CategoriaId%>"><%#: Item.oCategoria.strTitulo%></a>                       
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
                

            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="heading3">
                    <h4 class="panel-title">
                    <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapse3" aria-expanded="false" aria-controls="collapse3">
                        Suas perguntas respondidas.
                    </a>
                    </h4>
                </div>

                <div  id="collapse3" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading3">

                
                     <div class="panel-body">                
                    
                        <div class="row">
                            <div class="col-md-12">

                                <asp:ListView ID="ListViewPerguntasRespondidas" runat="server"
                                    DataKeyNames="produtoId" GroupItemCount="1"
                                    ItemType="S2B2015.Models.PerguntaViewModel" SelectMethod="GetPerguntasRespondidas">
                                    <EmptyDataTemplate>
                                        <table>
                                            <tr>
                                                 <td>Você ainda não respondeu nem uma pergunta</td> 
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
                                                            ImageUrl='<%#: Item.StrLinkProduto%>' />
                                                    </a>
                                                </td>

                                                <td style="width:400px">                        
                                                    <a href="DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>"><%#: Item.StrTituloProduto%></a>
                                                </td>
                                            
                                            
                                                <td style="width:400px">   
                                                    <b>Pergunta feita em:  <%#: Item.dtPergunta%> </b>
                                                    <br />
                                                    <b>Pergunta:</b>
                                                     <%#: Item.strPergunta%>  
                                                    <hr />  
                                                    <b>Respondido em:  <%#: Item.dtResposta%> </b>
                                                    <br />
                                                    <b>Resposta:</b>       
                                                     <%#: Item.strRespostas%>  
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


            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="heading6">
                    <h4 class="panel-title">
                    <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapse6" aria-expanded="false" aria-controls="collapse6">                        
                        <asp:Label ID="lblProdutosVendidos" runat="server" Text="Seus produtos vendidos."></asp:Label> 
                    </a>
                    </h4>
                </div>
                <div  id="collapse6" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading6">
                    <div class="panel-body">                
                    
                        <div class="row">
                            <div class="col-md-12">

                                <asp:ListView ID="ListView2" runat="server"
                                    DataKeyNames="produtoId" GroupItemCount="1"
                                    ItemType="S2B2015.Models.ProdutoViewModel" SelectMethod="GetProdutosVendidos">
                                    <EmptyDataTemplate>
                                        <table>
                                            <tr>
                                                 <td>Você ainda não concretizou nem uma venda.</td> 
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

                                                <td style="width:400px">                        
                                                    <a href="DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>"><%#: Item.strTitulo%></a>
                                                </td>
                                                <td style="width:100px">
                                                    <%#:String.Format("{0:c}", Item.Preco)%>
                                                </td>
                                                <td style="width:300px">
                                                    <a href="BuscaProduto.aspx?Categoria=<%#:Item.CategoriaId%>"><%#: Item.oCategoria.strTitulo%></a>                       
                                                </td>    
                                                                                    
                                                <td style="width:100px">
                                                    <b>Voce recebeu uma avaliação de <%#:Item.nAvaliacao%> estrelas</b>
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


            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingCancelados">
                    <h4 class="panel-title">
                    <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseCancelados" aria-expanded="false" aria-controls="collapseCancelados">
                        Seus produtos cancelados.
                    </a>
                    </h4>
                </div>
                <div  id="collapseCancelados" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingCancelados">
                    <div class="panel-body">                
                    
                        <div class="row">
                            <div class="col-md-12">

                                <asp:ListView ID="ListView4" runat="server"
                                    DataKeyNames="produtoId" GroupItemCount="1"
                                    ItemType="S2B2015.Models.ProdutoViewModel" SelectMethod="GetProdutosCancelados">
                                    <EmptyDataTemplate>
                                        <table>
                                            <tr>
                                                 <td>Você ainda não concretizou nem uma venda.</td> 
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

                                                <td style="width:400px">                        
                                                    <a href="DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>"><%#: Item.strTitulo%></a>
                                                </td>
                                                <td style="width:100px">
                                                    <%#:String.Format("{0:c}", Item.Preco)%>
                                                </td>
                                                <td style="width:300px">
                                                    <a href="BuscaProduto.aspx?Categoria=<%#:Item.CategoriaId%>"><%#: Item.oCategoria.strTitulo%></a>                       
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

                
            <hr />


                <p>Seus dados como Comprador</p>




            
            <div class="panel panel-default">
            <div class="panel-heading" role="tab" id="heading4">
                <h4 class="panel-title">
                <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapse4" aria-expanded="false" aria-controls="collapse4">
                    Suas perguntas em feitas
                </a>
                </h4>
            </div>
            <div  id="collapse4" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading4">
                <div class="panel-body">                
                    
                    <div class="row">
                        <div class="col-md-12">

                            <asp:ListView ID="ListViewPerguntasFeitas" runat="server"
                                DataKeyNames="produtoId" GroupItemCount="1"
                                ItemType="S2B2015.Models.PerguntaViewModel" SelectMethod="GetPerguntasFeitas">
                                <EmptyDataTemplate>
                                    <table>
                                        <tr>
                                             <td>Você ainda não respondeu nem uma pergunta</td> 
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
                                                        ImageUrl='<%#: Item.StrLinkProduto%>' />
                                                </a>
                                            </td>

                                            <td style="width:400px">                        
                                                <a href="DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>"><%#: Item.StrTituloProduto%></a>
                                            </td>
                                            
                                            
                                            <td style="width:400px">   
                                                <b>Pergunta feita em:  <%#: Item.dtPergunta%> </b>
                                                <br />
                                                <b>Pergunta:</b>
                                                 <%#: Item.strPergunta%>  
                                                <hr />  
                                                <b>Respondido em:  <%#: Item.dtResposta%> </b>
                                                <br />
                                                <b>Resposta:</b>       
                                                 <%#: Item.strRespostas%>  
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



                





            <div class="panel panel-default">
            <div class="panel-heading" role="tab" id="heading5">
                <h4 class="panel-title">
                <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapse5" aria-expanded="false" aria-controls="collapse5">
                    <asp:Label ID="lblProdutosComprados" runat="server" Text="Seus produtos comprados."></asp:Label> 
                    
                </a>
                </h4>
            </div>
            <div  id="collapse5" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading5">
                <div class="panel-body">                
                    
                    <div class="row">
                        <div class="col-md-12">

                            <asp:ListView ID="ListViewProdutosComprados" runat="server"
                                    DataKeyNames="produtoId" GroupItemCount="1"
                                    ItemType="S2B2015.Models.ProdutoViewModel" SelectMethod="GetProdutosComprados">
                                    <EmptyDataTemplate>
                                        <table>
                                            <tr>
                                                 <td>Você ainda não realizou nem uma compra.</td> 
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

                                                <td style="width:400px">                        
                                                    <a href="DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>"><%#: Item.strTitulo%></a>
                                                </td>
                                                <td style="width:100px">
                                                    <%#:String.Format("{0:c}", Item.Preco)%>
                                                </td>
                                                <td style="width:300px">
                                                    <a href="BuscaProduto.aspx?Categoria=<%#:Item.CategoriaId%>"><%#: Item.oCategoria.strTitulo%></a>                       
                                                </td>      
                                                

                                                <td style="width:100px">
                                                    <p>Você deu nota: <%#:Item.nAvaliacao%> para esta transação.</p>
                                                    
                                                </td>

                                                <td style="width:300px">
                                                    <div runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
                                                        <ContentTemplate>
                                                            <ul>
                                                                <li> Avalie o vendedor!:<%#:Convert.ToString(Item.nAvaliacao)%>
                                                                    <br />           

                                                                    
                                                                    <ajaxToolkit:Rating
                                                                            ClientIDMode="AutoID"
                                                                            ID="RatingCompra"
                                                                            Tag="<%#:Item.ProdutoId%>"
                                                                            runat="server"
                                                                            MaxRating="5"
                                                                            CurrentRating="2"
                                                                            StarCssClass="StarCss"
                                                                            FilledStarCssClass="FilledStarCss"
                                                                            EmptyStarCssClass="EmptyStarCss"
                                                                            WaitingStarCssClass="WaitingStarCss"
                                                                            AutoPostBack="true"
                                                                            OnChanged="AvaliarCompra"
                                                                            >
                                                                        </ajaxToolkit:Rating>

                                                                         </li>
                                                                 </ul>
                                                        </ContentTemplate>
                                                    </div>  
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

                    

            


        </div>
     

</asp:Content>
