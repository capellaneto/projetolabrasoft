<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroBolsista.aspx.cs" Inherits="WebApplication1.CadastroBolsista" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow-sm mx-auto" style="max-width: 550px;">
            <div class="card-header bg-primary text-white text-center">
                <h2 class="mb-0">📝 Cadastro de Bolsista</h2>
            </div>
            
            <div class="card-body p-4">
                <p class="text-muted text-center small">Preencha os campos abaixo para processar o cadastro.</p>
                <hr />

                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Nome Completo:</label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Ex: João Silva"></asp:TextBox>
                </div>

                <div class="row">
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Matrícula:</label>
                        <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" placeholder="2024.X.XXXX"></asp:TextBox>
                    </div>

                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">CPF:</label>
                        <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" placeholder="000.000.000-00"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group mb-4">
                    <label class="form-label font-weight-bold">Data de Nascimento:</label>
                    <asp:TextBox ID="txtDataNasc" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group mb-3">
                    <label class="form-label font-weight-bold">Sexo:</label>
                    <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Selecione..." Value="" />
                        <asp:ListItem Text="Masculino" Value="M" />
                        <asp:ListItem Text="Feminino" Value="F" />
                        <asp:ListItem Text="Outro" Value="O" />
                    </asp:DropDownList>
                </div>


                <div class="d-flex gap-3 mt-4">
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar e Processar Cadastro" 
                        CssClass="btn btn-success btn-lg flex-fill" OnClick="btnSalvar_Click" />

                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar"
                          CssClass="btn btn-secondary flex-fill" OnClick="btnLimpar_Click" />
                </div>
              
                 <h4 class="fw-semibold text-title mb-4 mt-5"> Lista de Bolsistas </h4> 

                 <asp:Panel ID="pnlBolsistas"
                     runat="server"> 
                    <asp:Button ID="btnFiltroMasc" runat="server" Text="Filtro Masculino"
                         CssClass="btn btn-warning flex-fill" OnClick="btnFiltroMasc_Click" />
                     <asp:Button ID="btnFiltroFem" runat="server" Text="Filtro Feminino"
                         CssClass="btn btn-warning flex-fill" OnClick="btnFiltroFem_Click" />
                    <asp:Button ID="btnOrdemAlfabetica" runat="server" Text="Ordenar"
                         CssClass="btn btn-secondary flex-fill" OnClick="btnOrdemAlfabetica_Click" />
                     <asp:Button ID="btnOrigem" runat="server" Text="Lista Original"
                         CssClass="btn btn-success flex-fill" OnClick="btnOrigem_Click" />

                     
                    
                   <asp:GridView ID="gvBolsistas"
                    runat="server"
                    AutoGenerateColumns ="True"
                    CssClass="table table-striped table-bordered mt-4"
                   ></asp:GridView>
                  </asp:Panel>
                <div class="mt-4 text-center">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
