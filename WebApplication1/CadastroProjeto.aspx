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
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="row">
                <div class="col-md-6 form-group mb-3">
                    <label class="form-label font-weight-bold">Verba:</label>
                    <asp:TextBox ID="txtVerba" runat="server" CssClass="form-control" placeholder="00"></asp:TextBox>
                </div>

                <div class="row">
                <div class="col-md-6 form-group mb-3">
                    <label class="form-label font-weight-bold">Valor da Bolsa:</label>
                    <asp:TextBox ID="txtValorBolsa" runat="server" CssClass="form-control" placeholder="00"></asp:TextBox>
                </div>
                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Coordenador:</label>
                        <asp:DropDownList ID="Coordenadores" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-6 form-group mb-3">
                        <label class="form-label font-weight-bold">Bolsista:</label>
                        <asp:DropDownList ID="Bolsistas" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
            </div>

            <div class="form-group mb-3">
                <label class="form-label font-weight-bold">Área de Atuação:</label>
                <asp:TextBox ID="txtArea" runat="server" CssClass="form-control" placeholder="Ex: Engenharia de Software"></asp:TextBox>
            </div>


            <div class="d-grid gap-2">
                <asp:Button ID="btnSalvar" runat="server" Text="Cadastrar Coordenador" 
                    CssClass="btn btn-dark btn-lg w-100" OnClick="btnSalvar_Click"/>
            </div>

            <hr />
                <div class="mt-4">
                    <h4 class="text-secondary">Lista de Coordenadores</h4>
      
                    <asp:GridView ID="gridProjetos" runat="server" 
                        CssClass="table table-hover table-bordered mt-2" 
                        AutoGenerateColumns="true">
                        <HeaderStyle CssClass="table-dark" />
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
