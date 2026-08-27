<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroProjeto.aspx.cs" Inherits="WebApplication1.CadastroProjeto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
    <div class="card shadow-sm mx-auto w-100">
        <div class="card-header bg-dark text-white text-center">
            <h2 class="mb-0">👨‍🏫 Cadastro de Projeto</h2>
        </div>
        
        <div class="card-body p-4">
            <div class="form-group mb-3">
                <label class="form-label font-weight-bold">Titulo:</label>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Nome do Projeto"></asp:TextBox>
                <small class="text-danger">
                 campo obrigatório
                </small>
            </div>

            <div class="row">
                <div class="col-md-6 form-group mb-3">
                    <label class="form-label font-weight-bold">Verba:</label>
                    <asp:TextBox ID="txtVerba" runat="server" CssClass="form-control" placeholder="Verba do Projeto"></asp:TextBox>
                    <small class="text-danger">
                     campo obrigatório
                    </small>
                </div>

                <div class="row">
                <div class="col-md-6 form-group mb-3">
                    <label class="form-label font-weight-bold">Bolsa:</label>
                    <asp:TextBox ID="txtValorBolsa" runat="server" CssClass="form-control" placeholder="Valor da Bolsa"></asp:TextBox>
                    <small class="text-danger">
                     campo obrigatório
                    </small>
                </div>
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Coordenador:</label>
                        <asp:DropDownList ID="Coordenadores" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <small class="text-danger">
                         campo obrigatório
                        </small>
                    </div>

                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Bolsista:</label>
                                <asp:ListBox ID="ddlBolsistas" runat="server"  CssClass="form-control" SelectionMode="Multiple" Rows="1">    
                                </asp:ListBox>
                    </div>
            </div>

            <div class="form-group mb-3">
                <label class="form-label font-weight-bold">Área de Atuação:</label>
                <asp:TextBox ID="txtArea" runat="server" CssClass="form-control" placeholder="Ex: Engenharia de Software"></asp:TextBox>
                <small class="text-danger">
                 campo obrigatório
                </small>
            </div>


            <div class="d-grid gap-2">
                <asp:Button ID="btnSalvar" runat="server" Text="Cadastrar Projeto" 
                    CssClass="btn btn-dark btn-lg w-100" OnClick="btnSalvar_Click"/>
            </div>

            <hr />
                <div class="mt-4">
                    <h4 class="text-secondary">Lista de Projetos</h4>
      
                    <asp:GridView ID="gridProjetos" runat="server" 
                        CssClass="table table-hover table-bordered mt-2" 
                        AutoGenerateColumns="false"
                        OnRowCommand="gridProjetos_RowCommand">

                        <Columns>

                            <asp:BoundField DataField="Titulo" HeaderText="´Titulo" />
                            <asp:BoundField DataField="Area" HeaderText="Area" />
                            <asp:BoundField DataField="Verba" HeaderText="´Verba" />

                            <asp:TemplateField HeaderText="Ações">
                                <ItemTemplate>
                                   
                                  <div class="d-flex gap-2 align-items-center">

                                    <asp:Button
                                        ID= "btnDetalhes" runat="server" Text="Detalhes"
                                        CommandName="MostrarDetalhes" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>

                    <asp:Panel ID="pnlDetalhes" runat="server" Visible="false" CssClass="card mt-3">
                        <div class="card-header bg-dark text-white">
                            Detalhes do Projeto
                        </div>

                        <div class="card-body">

                          <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                          <br />
                          <asp:Label ID="lblArea" runat="server"></asp:Label>
                          <br />
                          <asp:Label ID="lblVerba" runat="server"></asp:Label>
                          <br />
                          <asp:Label ID="lblValorBolsa" runat="server"></asp:Label>
                          <br />
                          <asp:Label ID="lblCoordenador" runat="server"></asp:Label>
                          <br />
                          <asp:Label ID="lblBolsista" runat="server"></asp:Label>
                          <br />

                          <h5>Bolsistas:</h5>

                            <asp:Repeater ID="rptBolsistas" runat="server" OnItemCommand="rptBolsistas_ItemCommand">

                                <ItemTemplate>

                                    <div class="card mb-2 p-2">

                                        <div class="d-flex justify-content-between align-items-center">

                                        Nome:
                                        <%# Eval("Nome") %>


                                    <asp:Button ID="btnRemoverBolsista" 
                                        runat="server" 
                                        Text="Remover Bolsista"
                                        CssClass= "btn btn-danger btn-sm"
                                        CommandName="Remover Bolsista"
                                        CommandArgument='<%# Eval("Id") %>'/>

                                        </div>
                                        

                                </ItemTemplate>

                            </asp:Repeater>

                            <div class="mt-3">

                                <label class="form-label font-weight-bold">
                                    Adicionar Bolsista:
                                </label>

                                <asp:ListBox ID="lstBolsistasDisponiveis" 
                                    runat="server"
                                    CssClass="form-control"
                                    SelectionMode="Multiple"
                                    Rows="1">

                                </asp:ListBox>


                                <asp:Button ID="btnAdicionarBolsista" 
                                    runat="server"
                                    Text="Adicionar Bolsista:" 
                                    CssClass="btn btn-success mt-2" 
                                    OnClick="btnAdicionarBolsista_Click"/>

                            </div>

                            <asp:GridView ID="GridDespesas" runat="server" 
                                CssClass="table table-hover table-bordered mt-2" 
                                AutoGenerateColumns="false">

                                <Columns>

                                    <asp:BoundField DataField="Descricao" HeaderText="´Descricao" />
                                    <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                                    <asp:BoundField DataField="Valor" HeaderText="´VerValor" />
                                    <asp:BoundField DataField="Data" HeaderText="´Data" />
                                </Columns>
                            </asp:GridView>

                            <asp:Label ID="lblSemBolsista" runat="server" Text="⚠️ Este projeto não possui bolsistas cadastrados"
                                       CssClass="text-warning"
                                       Visible="false">
                            </asp:Label>
                            <asp:Button 
                                ID="btnFecharDetalhes" 
                                runat="server" 
                                Text="Fechar Detalhes"
                                CssClass="btn btn-secondary mt-3"
                                OnClick="btnFecharDetalhes_Click" />


                        </div>
                        </asp:Panel>


                    <asp:Label ID="lblAviso" runat="server" Text="Nenhum coordenador cadastrado." CssClass="text-muted small italic"></asp:Label>
                </div>

                <div class="mt-3 text-center">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                </div>
    </div>
    </div>
</div>
        </div>
</asp:Content>
