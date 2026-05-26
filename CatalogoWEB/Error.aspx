<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="CatalogoWEB.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="max-width:480px; margin:4rem auto; text-align:center;">
        <i class="bi bi-exclamation-circle" style="font-size:2.75rem; opacity:0.25; display:block; margin-bottom:1rem;"></i>
        <h2 style="font-family:'Playfair Display',serif; font-size:1.75rem; font-weight:400; margin:0 0 0.75rem;">
            Ocurrió un problema
        </h2>
        <asp:Label ID="lblError" Text="" runat="server"
            style="color:var(--text-secondary); font-size:14px; display:block; margin-bottom:1.75rem;" />
        <a href="Default.aspx" class="btn-primary" style="text-decoration:none; padding:0.5rem 1.5rem;">
            Volver al inicio
        </a>
    </div>

</asp:Content>
