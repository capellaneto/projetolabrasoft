<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroCoordenador.aspx.cs" Inherits="WebApplication1.CadastroCoordenador" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow-sm mx-auto w-100">
            <div class="card-header bg-dark text-white text-center">
                <h2 class="mb-0">👨‍🏫 Cadastro de Coordenador</h2>
            </div>
            
            <div class="card-body p-4">
                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Nome Completo:</label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Nome do Professor"></asp:TextBox>
                     <small class="text-danger">
                      campo obrigatório
                     </small>
                </div>

                <div class="row">
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">CPF:</label>
                        <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" placeholder="000.000.000-00"></asp:TextBox>
                         <small class="text-danger">
                          campo obrigatório
                         </small>
                    </div>
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Titulação:</label>
                        <asp:DropDownList ID="ddlTitulacao" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Selecione..." Value="" />
                            <asp:ListItem Text="Especialista" Value="Especialista" />
                            <asp:ListItem Text="Mestre" Value="Mestre" />
                            <asp:ListItem Text="Doutor" Value="Doutor" />
                            <asp:ListItem Text="Pós-Doutor" Value="Pós-Doutor" />
                        </asp:DropDownList>
                         <small class="text-danger">
                          campo obrigatório
                         </small>
                    </div>
                </div>

                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Área de Atuação:</label>
                    <asp:TextBox ID="txtArea" runat="server" CssClass="form-control" placeholder="Ex: Engenharia de Software"></asp:TextBox>
                     <small class="text-danger">
                      campo obrigatório
                     </small>
                </div>

                <div class="form-group mb-4">
                    <label class="form-label font-weight-bold">E-mail Institucional:</label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="email@instituicao.edu.br"></asp:TextBox>
                     <small class="text-danger">
                      campo obrigatório
                     </small>
                </div>

                <div class="d-grid gap-2">
                    <asp:Button ID="btnSalvar" runat="server" Text="Cadastrar Coordenador" 
                        CssClass="btn btn-dark btn-lg w-100" OnClick="btnSalvar_Click" />
                </div>

                <hr />

                <asp:Panel ID="pnlBusca" runat="server">
                    <div class="row align-items-center mb-2">

                        <div class="col-md-6">
                            <h4 class="text-secondary mb-0">
                                Lista de Coordenadores
                            </h4>
                        </div>

                        <div class="col-md-6 d-flex justify-content-end">
                            <asp:TextBox ID="txtFiltro"
                                runat="server"
                                placeholder="Digite nome ou titulação..."
                                CssClass="form-control me-2"
                                style="max-width: 280px;">
                            </asp:TextBox>

                            <asp:Button ID="btnFiltrarNomeTitulacao" 
                                runat="server" 
                                Text="Filtrar" 
                                OnClick="btnFiltrarNomeTitulacao_Click"
                                CssClass="btn btn-dark" />
                        </div>

    </div>
</asp:Panel>
                    
                    
                    <asp:GridView ID="gridCoordenadores" 
                        runat="server" 
                        CssClass="table table-hover table-bordered mt-2" 
                        AutoGenerateColumns="false"
                        OnRowCommand="gridCoordenadores_RowCommand">
                        
                        <HeaderStyle CssClass="table-dark" />

                        <Columns>

                            <asp:BoundField
                                DataField="Nome"
                                HeaderText="Nome" />

                            <asp:BoundField
                                DataField="CPF"
                                HeaderText="CPF" />

                            <asp:BoundField
                                DataField="Titulacao"
                                HeaderText="Titulação" />

                            <asp:BoundField
                                DataField="AreaAtuacao"
                                HeaderText="Area de Atuação" />

                            <asp:BoundField
                                DataField="Email"
                                HeaderText="Email" />

                            <asp:TemplateField HeaderText="Ação">

                                <ItemTemplate>

                                <div class="d-flex gap-2 align-items-center">
                                    
                                    <asp:Button ID="btnAtualizar" 
                                     runat="server" 
                                     Text="Atualizar"
                                     CommandName="Atualizar"
                                     CommandArgument='<%# Eval("Id") %>'
                                     CssClass="btn btn-warning btn-sm" />

                                     <asp:Button ID="btnExcluirCoordenador" 
                                         runat="server" 
                                         Text="Excluir Coordenador"
                                         CommandName="Excluir Coordenador"
                                         CommandArgument='<%# Eval("Id") %>'
                                         CssClass="btn btn-warning btn-sm" />

                                <asp:Panel ID="pnlAtualizarEmail"
                                    runat="server"
                                    Visible="false"
                                     CssClass="mt-2 p-3 border rounded">

                                <div class="mb-2">

                                    <label class ="form-label">
                                        Novo Email:
                                    </label>

                                    <asp:TextBox ID="txtNovoEmail"
                                        runat="server"
                                        Text='<%# Eval("Email") %>'
                                        TextMode="Email"
                                        CssClass="form-control">
                                    </asp:TextBox>

                                </div>


                                <div class="mt-2">

                                    <asp:Button ID="btnConfirmarAtualizacao"
                                        runat="server"
                                        Text="Confirmar"
                                        CommandName="Confirmar"
                                        CommandArgument='<%# Eval("Id") %>'
                                        CssClass="btn btn-success btn-sm" />

                                    
                                    <asp:Button ID="btnCancelarAtualizacao"
                                        runat="server"
                                        Text="Cancelar"
                                        CommandName="Cancelar"
                                        CommandArgument='<%# Eval("Id") %>'
                                        CssClass="btn btn-secondary btn-sm ms-1" /> 

                                </div>


                                </asp:Panel>
                                  

                                 </ItemTemplate>
                               

                            </asp:TemplateField>

                        </Columns>




                    </asp:GridView>
                    <asp:Label ID="lblAviso" runat="server" Text="Nenhum coordenador cadastrado." CssClass="text-muted small italic"></asp:Label>
                </div>

                <div class="mt-3 text-center">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>