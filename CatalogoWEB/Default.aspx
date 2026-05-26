<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CatalogoWEB.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />

    <div class="page-header">
        <div>
            <h1 class="page-title">Catálogo</h1>
            <p class="page-subtitle">Explorá todos los artículos disponibles</p>
        </div>
    </div>

    <div class="search-bar">
        <asp:TextBox ID="txtBoxFiltro" runat="server" CssClass="form-control" placeholder="Buscar artículos..." />
        <asp:Button ID="btnBuscarFiltro" runat="server" Text="Buscar" OnClick="btnBuscarFiltro_Click" CssClass="btn-primary" />
        <asp:Button ID="btnBorrarFiltro" runat="server" Text="Limpiar" OnClick="btnBorrarFiltro_Click" CssClass="btn-outline" />
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row row-cols-1 row-cols-sm-2 row-cols-lg-3 row-cols-xl-4 g-3">
                <asp:Repeater runat="server" ID="repRepetidor">
                    <ItemTemplate>
                        <div class="col">
                            <div class="catalog-card h-100">
                                <img src="<%#Eval("UrlImagen")%>" alt="<%#Eval("Nombre")%>" onerror="this.onerror=null;this.src='data:image/svg+xml,%3Csvg xmlns=%22http://www.w3.org/2000/svg%22 width=%22400%22 height=%22300%22%3E%3Crect width=%22400%22 height=%22300%22 fill=%22%23edeae3%22/%3E%3Ctext x=%2250%25%22 y=%2250%25%22 dominant-baseline=%22middle%22 text-anchor=%22middle%22 font-family=%22sans-serif%22 font-size=%2214%22 fill=%22%23a09d97%22%3ESin imagen%3C/text%3E%3C/svg%3E'" />
                                <div class="catalog-card-body">
                                    <p class="catalog-card-title"><%#Eval("Nombre")%></p>
                                    <p class="catalog-card-desc"><%#Eval("Descripcion")%></p>
                                    <p style="font-size:14px; font-weight:600; color:var(--text-primary); margin:0 0 0.85rem;">$<%#Eval("Precio")%></p>
                                    <asp:Button ID="btnDetalle" Text="Ver detalle →" runat="server"
                                        OnClick="btnDetalle_Click"
                                        CommandArgument='<%#Eval("Id")%>'
                                        CommandName="ArticuloId"
                                        CssClass="btn-outline"
                                        style="width:100%;" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
