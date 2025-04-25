<%@ Page Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true"
    CodeBehind="Empleado_categoria_detalle.aspx.cs" Inherits="web.secure.Empleado_categoria_detalle" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />
    </asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Empleados por categoria</h3>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1" style="padding: 0; padding-left: 1px;">
                            &nbsp;&nbsp;
                        </div>
                        <div class="col-md-11">
                            <div class="row">
                                <div class="col-md-6" style="text-align: left;">
                                    <h5
                                        style="color: #212529; font-size: 20px; font-weight: 700; margin-bottom: 5px;">
                                        <span style="color: #dc3545; font-size: 20px; font-weight: 700; margin-left: 0;">#Codigo de Categoria: </span>
                                        <span style="color: #dc3545; font-size: 20px; font-weight: 700" runat="server" id="txtCodigo"></span></h5>
                                </div>
                                <div class="col-md-6" style="text-align: left;">
                                    &nbsp;&nbsp;
                                </div>
                            </div>

                        </div>
                    </div>
                    <div>
                        <asp:GridView ID="gvCategoriasEmple" CssClass="table table-bordered" runat="server"
                            OnRowDataBound="gvCategoriasEmple_RowDataBound"
                            OnRowCommand="gvCategoriasEmple_RowCommand" CellPadding="4"
                            AutoGenerateColumns="False" ForeColor="#333333" GridLines="None"
                            DataKeyNames="legajo" AllowPaging="True"
                            OnPageIndexChanging="gvCategoriasEmple_PageIndexChanging" PageSize="8">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Legajo" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <p>
                                            <asp:Label ID="lblLegajo" runat="server" Text=""></asp:Label>
                                        </p>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <p>
                                            <asp:Label ID="lblNombre" runat="server" Text="">
                                            </asp:Label>
                                        </p>
                                    </ItemTemplate>
                                    <ItemStyle Width="40%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nro de documento" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <p>
                                            <asp:Label ID="lblNroDocumento" runat="server" Text="">
                                            </asp:Label>
                                        </p>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateField>

                            </Columns>
                            <EditRowStyle BackColor="#999999"></EditRowStyle>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <PagerStyle CssClass="gridview-category"></PagerStyle>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="text-align: right;">
                            <asp:LinkButton ID="lbtnVolver" CssClass="btn btn-outline-primary" runat="server" OnClick="lbtnVolver_Click">
                                                        <i class="fa fa-sign-out"></i>&nbsp;Volver
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
