<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login_Cadastro.aspx.cs" Inherits="WebApplication1.Login_Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

        <asp:View ID="ViewLogin" runat="server">

            <h2>Login</h2>

             <asp:Label 
             ID="lblMensagemLogin" 
             runat="server" 
             CssClass="alert d-none">
         </asp:Label>


           <div>

            <asp:Label ID="lblEmail" runat="server" Text="E-mail:"></asp:Label>
               <br />

            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
           
           </div>


            
            <div>
                
                <asp:Label ID="lblSenha" runat="server" Text="Senha:"></asp:Label>
               <br />
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox> 
                
            </div>

            <asp:Button ID="btnLogar" runat="server" Text="Entrar" OnClick="btnLogar_Click" />

            <asp:Button ID="btnCadastro" runat="server" Text="Cadastre-se" OnClick="btnCadastro_Click" /> 

        </asp:View>

         
        
        
        <asp:View ID="ViewCadastro" runat="server">

             <h2>Cadastro</h2>

                    <asp:Label 
                    ID="lblMensagemCadastro" 
                    runat="server" 
                    CssClass="alert d-none">
                </asp:Label>

            

            <div>

             <asp:Label ID="lblNomeCadastro" runat="server" Text="Nome:"></asp:Label>
                <br />
                <asp:TextBox ID="txtNomeCadastro" runat="server"></asp:TextBox>
             </div>

             
             <div>

             <asp:Label ID="lblEmailCadastro" runat="server" Text="Email:"></asp:Label>
                 <br />
                <asp:TextBox ID="txtEmailCadastro" runat="server"></asp:TextBox>
            </div>
           
            <div>

            <asp:Label ID="Label1" runat="server" Text="Senha:"></asp:Label>
                 <br />
             <asp:TextBox ID="txtSenhaCadastro" runat="server" TextMode="Password"></asp:TextBox>
            
            </div>

             <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" OnClick="btnCadastrar_Click" />

             <asp:Button ID="btnVoltarLogin" runat="server" Text="Voltar" OnClick="btnVoltarLogin_Click"/> 

         </asp:View>




    </asp:MultiView>

</asp:Content>
