<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Despesa.aspx.cs" Inherits="WebApplication1.Despesa1" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
    <div class="card shadow-sm mx-auto w-100">
        <div class="card-header bg-primary text-white text-center">
            <h2 class="mb-0">📝 Cadastro de Despesas</h2>
        </div>
        
        <div class="card-body p-4">
            <p class="text-muted text-center small">Preencha os campos abaixo para processar o cadastro.</p>
            <hr />

            <div class="form-group mb-3">
                <label class="form-label font-weight-bold">Descrição:</label>
                <asp:TextBox ID="txtDescricao" runat="server" CssClass="form-control" placeholder="Digite a descrição"></asp:TextBox>
                 <small class="text-danger">
                  campo obrigatório
                 </small>
            </div>

             <div class="form-group mb-3">
                 <label class="form-label font-weight-bold"> Categoria:</label>
                 <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                     <asp:ListItem Text="Selecione..." Value="" />
                     <asp:ListItem Text="Material" Value="Material" />
                     <asp:ListItem Text="Manutenção" Value="Manutenção" />
                     <asp:ListItem Text="Transporte" Value="Transporte" />
                     <asp:ListItem Text="Alimentação" Value="Alimentação" />
                     <asp:ListItem Text="Outro" Value="Outro" />
                 </asp:DropDownList>
                  <small class="text-danger">
                  campo obrigatório
                 </small>
             </div>

                <asp:TextBox ID="txtOutraCategoria" runat="server" Visible="false" placeholder="Digite a categoria">
                    
                </asp:TextBox>

            <div class="row">
                <div class="col-md-6 form-group mb-3">
                    <label class="form-label font-weight-bold">Valor:</label>
                    <asp:TextBox ID="txtValor" runat="server" CssClass="form-control" placeholder="Digite o valor"></asp:TextBox>
                     <small class="text-danger">
                      campo obrigatório
                     </small>
                </div>

                <div class="col-md-6 form-group mb-3">
                    <label class="form-label font-weight-bold">Projetos:</label>
                    <asp:DropDownList ID="ddlProjetos" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                     <small class="text-danger">
                      campo obrigatório
                     </small>
                </div>
            </div>

            <div class="form-group mb-4">
                <label class="form-label font-weight-bold">Data</label>
                <asp:TextBox ID="txtData" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                 <small class="text-danger">
                  campo obrigatório
                 </small>
            </div>

            <div class="d-grid gap-2">
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" 
                    CssClass="btn btn-success btn-lg w-100" OnClick="btnSalvar_Click" />
                <asp:Button ID="btnLimpar" runat="server" Text="Limpar Campos" 
                    CssClass="mt-2 btn btn-outline-secondary btn-lg btn-block" OnClick="btnLimpar_Click" />


                <div class="mt-4 text-center">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                </div>
            </div>
         </div>
    </div>
 </div>

</asp:Content>
