<%@ Page Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true"
    CodeBehind="cambios_empleado.aspx.cs" Inherits="web.secure.cambios_empleado" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div>
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">Historial cambios empleado</h3>
                        </div>
                        <asp:GridView ID="gvCambios" CssClass="table table-hover table-bordered" runat="server"
                            OnRowDataBound="gvCambios_RowDataBound" OnRowCommand="gvCambios_RowCommand" CellPadding="4"
                            AutoGenerateColumns="False" ForeColor="#333333" GridLines="None" DataKeyNames="fecha_cambio"
                            AllowPaging="True" OnPageIndexChanging="gvCambios_PageIndexChanging" PageSize="8">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775">
                            </AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Fecha Movimiento" ItemStyle-Width="50%">
                                    <ItemTemplate>
                                        <p>
                                            <asp:Label ID="lblFechaMov" runat="server" Text=""></asp:Label>
                                        </p>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion movimientos" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <p>
                                            <asp:Label ID="lblDesCambios" runat="server" Text="">
                                            </asp:Label>
                                        </p>
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999"></EditRowStyle>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White">
                            </FooterStyle>
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White">
                            </HeaderStyle>
                            <PagerStyle CssClass="gridview-category"></PagerStyle>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333">
                            </SelectedRowStyle>
                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        </div>
        </div>
    </asp:Content>