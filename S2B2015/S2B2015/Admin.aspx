
<%@ Page  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="S2B2015.AdminPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="jumbotron">


        <div class="row">
            <div class="col-md-12">
                <asp:Label CssClass="h1" ID="lblHeader" runat="server" Text="Configurações do sistema."></asp:Label>
            </div>
        </div>
        


        <br />



        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
            
            <div class="panel panel-default">
            <div class="panel-heading" role="tab" id="headingOne">
                <h4 class="panel-title">
                <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                    Relatorio Geral
                </a>
                </h4>
            </div>
            <div   id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                <div class="panel-body">
                
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnRelatorioItem" Width="100%" CssClass="btn btn-primary" runat="server" Text="Gerar Relatório Geral" OnClick="btnRelatorioItem_Click" />               
                            <asp:Label ID="lblRelatorioItem" runat="server" Text=""></asp:Label>                
                        </div>
                    </div>

                </div>
            </div>
            </div>


            <div class="panel panel-default">
            <div class="panel-heading" role="tab" id="headingTwo">
                <h4 class="panel-title">
                <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                    Editor de Categorias
                </a>
                </h4>
            </div>
            <div  id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                <div class="panel-body">
                
                    <div id="Panel1" class="panel panel-default">
                        <div class="panel-heading">Categorias:</div>
                        <div class="panel-body">
                            <div class="row form-group">
                                <div class="col-md-12 form-group">
                
                                    <div class="row">                    
                                        <div class="col-md-8">
                                            <asp:Label ID="lblListaCategorias" for="ListaCategorias" runat="server" Text="Editar:"></asp:Label>                    
                                            <asp:DropDownList CssClass="form-control" ID="ListaCategorias" runat="server" OnSelectedIndexChanged="ListaCategorias_SelectedIndexChanged">
                                                <asp:ListItem>Nova Categoria</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label ID="Label1" for="btnSalvar"  runat="server" Text="Salvar"></asp:Label>
                                            <asp:Button ID="btnSalvar" CssClass="btn btn-success form-control" runat="server" Text="Salvar" OnClick="btnCriar_Click" />
                                        </div>
                                    </div>   

                    
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
                
            
                            </div>

                        </div>
                    </div>



                </div>
            </div>
            </div>



            <div class="panel panel-default">
            <div class="panel-heading" role="tab" id="headingPerguntas">
                <h4 class="panel-title">
                <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapsePerguntas" aria-expanded="false" aria-controls="collapsePerguntas">
                    Perguntas e respostas.
                </a>
                </h4>
            </div>
            <div  id="collapsePerguntas" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingPerguntas">
                

                 <div class="panel-body">                
                    
                    <div class="row">
                        <div class="col-md-12">

                            <asp:ListView ID="ListViewPerguntasRespondidas" runat="server"
                                DataKeyNames="produtoId" GroupItemCount="1"
                                ItemType="S2B2015.Models.PerguntaViewModel" SelectMethod="GetPerguntasRespondidas" >
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
                                                <a style="display:inline-block" href="../DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>">
                                                    <asp:Image runat="server" ID="Image2" Height="160px"
                                                        ImageUrl='<%#: Item.StrLinkProduto%>' />
                                                </a>
                                            </td>

                                            <td style="width:400px">                        
                                                <a href="../DadosProduto.aspx?ProdutoId=<%#:Item.ProdutoId%>"><%#: Item.StrTituloProduto%></a>
                                            </td>
                                            
                                            
                                            <td style="width:400px">   
                                                <b>Pergunta feita em:  <%#: Item.dtPergunta%> </b>
                                                <br />
                                                <b>Pergunta:</b>
                                                 <%#: Item.strPergunta%>  
                                                
                                                <br />
                                                <b>ID de quem Perguntou: <%#: Item.quemPerguntou%></b>

                                                <hr />  


                                                <b>Respondido em:  <%#: Item.dtResposta%> </b>
                                                <br />
                                                <b>Resposta:</b>       
                                                 <%#: Item.strRespostas%>  

                                                <br />
                                                <b>ID de quem Respondeu: <%#: Item.quemRespondeu%></b>
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
                <div class="panel-heading" role="tab" id="headingThree">
                    <h4 class="panel-title">
                    <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                        Relatorios por Usuarios
                    </a>
                    </h4>
                </div>
                <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                    <div class="panel-body">
                
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Relatório por usuários:
                            </div>
                            <div class="panel-body">
                                <div class="row" >
                                    <div class="col-md-8">
                                        <asp:DropDownList CssClass="form-control" ID="drpListaUsuarios" runat="server" OnSelectedIndexChanged="drpListaUsuarios_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                                     </div>
                                    <div class="col-md-4">
                                           <asp:Button CssClass="btn btn-primary form-control" ID="btnRelatorioUsuarios" runat="server" Text="ver Relatorio" OnClick="btnRelatorioUsuarios_Click" Width="100%" /><br />
                                    </div>
                                </div>
        
        
                                <br />

                                <div class="panel panel-default">
                                    <div class="panel-heading">Interage com os seguintes produtos:</div>
                                    <div class="panel-body">
                                        <iframe visible="false" id="prodIframe" runat="server"  Width="100%" height="500px"></iframe>

                                    </div>
                                </div>
                                <br />


                                <div class="row">
            
                                    <div class="">
                                        <asp:Label ID="lblResultadoUsuarios" CssClass="h3"  runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="">
                                        <asp:LinkButton ID="lnkItensOfertados" target="_blank" CssClass="h3"  runat="server" OnClick="lnkItensOfertados_Click"></asp:LinkButton>
                                    </div>
                                    <div class="">
                                        <asp:LinkButton ID="lnkVendidos" target="_blank" runat="server" CssClass="h3"   OnClick="lnkVendidos_Click"></asp:LinkButton>
                                    </div>
                                    <div class="">
                                        <asp:LinkButton ID="lnkComprados" target="_blank" runat="server" CssClass="h3"   OnClick="lnkComprados_Click"></asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        
        
        
        </div>






        
        
        

        


    </div>

</asp:Content>
