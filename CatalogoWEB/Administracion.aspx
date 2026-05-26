<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Administracion.aspx.cs" Inherits="CatalogoWEB.Administracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />

    <div class="page-header">
        <div>
            <h1 class="page-title">Administración</h1>
            <p class="page-subtitle">Gestioná los artículos del catálogo</p>
        </div>
        <asp:Button ID="btnAgregarArticulo" Text="+ Agregar artículo" runat="server"
            OnClick="btnAgregarArticulo_Click" CssClass="btn-primary" />
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <%-- Barra de busqueda rapida --%>
            <div class="search-bar">
                <asp:TextBox ID="txtBoxFiltrar" runat="server" CssClass="form-control"
                    placeholder="Buscar por nombre..." AutoPostBack="true"
                    OnTextChanged="txtBoxFiltrar_TextChanged" />
                <asp:Button ID="btnBorrarFiltro" Text="Limpiar" runat="server"
                    OnClick="btnBorrarFiltro_Click" CssClass="btn-outline" />
                <div class="d-flex align-items-center gap-2 ms-1">
                    <asp:CheckBox ID="chBoxFiltroAvanzado" runat="server"
                        OnCheckedChanged="chBoxFiltroAvanzado_CheckedChanged" AutoPostBack="true" />
                    <label class="form-check-label mb-0" style="font-size: 13.5px; cursor: pointer;">
                        Filtro avanzado
                   
                    </label>
                </div>
            </div>

            <%-- Filtro avanzado --%>
            <%if (FiltroAvanzado)
                {%>
            <div class="filter-bar mb-3">
                <div class="row g-3 align-items-end">
                    <div class="col-auto">
                        <asp:Label Text="Filtrar por" runat="server" ID="lblDdlFiltrar" CssClass="form-label" />
                        <asp:DropDownList runat="server" ID="ddlFiltrar" CssClass="form-select"
                            OnSelectedIndexChanged="ddlFiltrar_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Código" />
                            <asp:ListItem Text="Precio" />
                            <asp:ListItem Text="Marca" />
                            <asp:ListItem Text="Categoría" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-auto">
                        <asp:Label Text="Criterio" runat="server" ID="lblDdlCriterio" CssClass="form-label" />
                        <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-select" AutoPostBack="true" />
                    </div>
                    <div class="col-auto">

                        <asp:Label Text="Marca" runat="server" ID="lblDdlMarca" Visible="false" CssClass="form-label" />
                        <asp:DropDownList runat="server" ID="ddlMarca" Visible="false" CssClass="form-select" />

                        <asp:Label Text="Categoría" runat="server" ID="lblDdlCategoria" Visible="false" CssClass="form-label" />
                        <asp:DropDownList runat="server" ID="ddlCategoria" Visible="false" CssClass="form-select" />

                        <asp:Label Text="Buscar" runat="server" ID="lblTxtBoxFiltrarAvanzado" Visible="true" CssClass="form-label" />
                        <asp:TextBox runat="server" ID="txtBoxFiltrarAvanzado" Visible="true" CssClass="form-control" />
                    </div>

                <div class="col-auto">
                    <asp:Button ID="btnFiltrar" Text="Aplicar" runat="server"
                        OnClick="btnFiltrar_Click" CssClass="btn-primary" />
                    <asp:Button ID="btnLimpiarFiltroAvanzado" Text="Limpiar" runat="server"
                        OnClick="btnLimpiarFiltroAvanzado_Click" CssClass="btn-outline" />
                </div>
            </div>
            </div>
            <%}%>

            <%-- Tabla de articulos --%>
            <div class="panel">
                <asp:GridView runat="server" ID="dgvArticulos" CssClass="table"
                    AutoGenerateColumns="false"
                    OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
                    OnPageIndexChanging="dgvArticulos_PageIndexChanging"
                    AllowPaging="true" PageSize="5"
                    PagerStyle-CssClass="gridview-pager"
                    PagerStyle-HorizontalAlign="Center">
                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="Id" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
                        <asp:BoundField HeaderText="Código" DataField="Codigo" />
                        <asp:BoundField HeaderText="Precio" DataField="Precio" />
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
                        <asp:BoundField HeaderText="Categoría" DataField="Categoria.Descripcion" />
                        <asp:CommandField HeaderText="" ShowSelectButton="true" SelectText="Editar" ControlStyle-CssClass="btn-select" />
                    </Columns>
                </asp:GridView>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
