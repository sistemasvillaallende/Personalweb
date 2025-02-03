<%@ Page Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="Empleado_categoria_detalle.aspx.cs" Inherits="web.secure.Empleado_categoria_detalle" %>

        <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
            <style type="text/css">
                .gridview {
                    background-color: #fff;
                    height: 60px;
                    padding: 2px;
                    margin: 4% auto;
                }

                .gridview a {
                    margin: 5px;
                    border-radius: 50%;
                    background-color: #444;
                    padding: 5px 10px 5px 10px;
                    color: #fff !important;
                    text-decoration: none;
                    box-shadow: 1px 1px 1px #111;
                }

                .gridview a:hover {
                    background-color: #1e8d12;
                    color: #fff;
                }

                .gridview span {
                    background-color: #ae2676;
                    color: #fff;
                    box-shadow: 1px 1px 1px #111;
                    border-radius: 50%;
                    padding: 5px 10px 5px 10px;
                }
            </style>
        </asp:Content>
        <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header">
                                <h3 class="card-title">Empleados por categoria</h3>
                            </div>
                            <div class="card-body">
                                <asp:UpdatePanel ID="PanelInfomacion" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="alert alert-success alert-dismissible fade show" runat="server"
                                            id="divConfirma" visible="false" role="alert">
                                            <strong>Aviso Importante!</strong>
                                            <p id="msjConfirmar" runat="server"></p>
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"
                                                onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Confirma');">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="alert alert-warning alert-dismissible fade show" runat="server"
                                            id="divError" visible="false" role="alert">
                                            <strong>Error!</strong>
                                            <p id="txtError" runat="server"></p>
                                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"
                                                onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Alerta');">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div>
                                    <asp:GridView ID="gvCategoriasEmple" CssClass="table table-hover table-bordered"
                                        runat="server" OnRowDataBound="gvCategoriasEmple_RowDataBound"
                                        OnRowCommand="gvCategoriasEmple_RowCommand" CellPadding="4"
                                        AutoGenerateColumns="False" ForeColor="#333333" GridLines="None"
                                        DataKeyNames="legajo" AllowPaging="True"
                                        OnPageIndexChanging="gvCategoriasEmple_PageIndexChanging" PageSize="8">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775">
                                        </AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Legajo" ItemStyle-Width="50%">
                                                <ItemTemplate>
                                                    <p>
                                                        <asp:Label ID="lblLegajo" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </ItemTemplate>
                                                <ItemStyle Width="50%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <p>
                                                        <asp:Label ID="lblNombre" runat="server" Text="">
                                                        </asp:Label>
                                                    </p>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
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
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White">
                                        </FooterStyle>
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White">
                                        </HeaderStyle>
                                        <PagerStyle CssClass="gridview"></PagerStyle>
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
