<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="CatalogoWEB.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <div>
            <h1 class="page-title">Favoritos</h1>
            <p class="page-subtitle">Tus artículos guardados</p>
        </div>
    </div>

    <div class="empty-state">
        <i class="bi bi-heart"></i>
        <p>Todavía no tenés favoritos guardados.</p>
        <a href="Default.aspx" style="font-size:14px; color:var(--text-primary); font-weight:600; margin-top:0.75rem; display:inline-block;">
            Explorar catálogo →
        </a>
    </div>

</asp:Content>
