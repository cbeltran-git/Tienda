<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productos.aspx.cs" Inherits="Tienda.productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <h2>Productos</h2>
    <hr />
    <a href="producto_crear.aspx" class="boton">Nuevo Producto</a>
    <h3>GridView</h3>
    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre Producto" />
            <asp:BoundField DataField="Marca" HeaderText="Marca" />
            <asp:BoundField DataField="PrecioProducto" HeaderText="Precio" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <h3>Repeater</h3>
    
        <h4>Filtrar: </h4>
    <div class="filtro"> 
        
        <asp:DropDownList ID="ddlFiltrarCategoria" runat="server" >
            <asp:ListItem Text="Seleccione Categoría" Value=""></asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="ddlFiltrarMarca" runat="server">
            <asp:ListItem Text="Seleccione Marca" Value=""></asp:ListItem>
        </asp:DropDownList>
       
    </div>
    
     <asp:Button CssClass="boton" ID="btnBuscar" runat="server" Text="Buscar" OnClick="filtro" />
    <div class="productos">
            <asp:Repeater ID="rptDatos" runat="server">
        <ItemTemplate>
            <div class="producto" >
                <h3><%#Eval("Nombre") %></h3>
                <p><%#Eval("Marca") %></p>
                <p><%#Eval("PrecioProducto") %></p>
                <img src="<%#Eval("Foto") %>" style="width:250px;height:200px;margin-left:5rem"/>
                <a href="producto.aspx?id=<%#Eval("Id") %>"> Editar </a>
                
            </div>
            
        </ItemTemplate>
    </asp:Repeater>
    </div>

    
</asp:Content>
