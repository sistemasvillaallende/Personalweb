<%@ Page Language="C#" MasterPageFile="~/MP/MasterNew.Master"  AutoEventWireup="true" CodeBehind="Concepto_Liq_x_Emp_Mov.aspx.cs" Inherits="web.secure.Concepto_Liq_x_Emp_Mov" %>

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
                        <h3 class="card-title">Movimientos Historial de Empleado</h3>
                    </div>
                            <asp:GridView ID="gvConceptoMov" CssClass="table table-hover table-bordered"
                                runat="server" OnRowDataBound="gvConceptoMov_RowDataBound"
                                OnRowCommand="gvConceptoMov_RowCommand" CellPadding="4"
                                AutoGenerateColumns="False" ForeColor="#333333" GridLines="None"
                                DataKeyNames="FECHA" AllowPaging="True"
                                OnPageIndexChanging="gvConceptoMov_PageIndexChanging" PageSize="8">
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
                                    <asp:TemplateField HeaderText="Usuario Carga" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <p>
                                                <asp:Label ID="lblUsuarioCarga" runat="server" Text="">
                                                </asp:Label>
                                            </p>
                                        </ItemTemplate>
                                        <ItemStyle Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TipoMovimiento" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <p>
                                                <asp:Label ID="lblTipoMov" runat="server" Text="">
                                                </asp:Label>
                                            </p>
                                        </ItemTemplate>
                                        <ItemStyle Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cod. Concepto" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <p>
                                                <asp:Label ID="lblCodConcepto" runat="server" Text="">
                                                </asp:Label>
                                            </p>
                                        </ItemTemplate>
                                        <ItemStyle Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Concepto" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <p>
                                                <asp:Label ID="lblConcepto" runat="server" Text="">
                                                </asp:Label>
                                            </p>
                                        </ItemTemplate>
                                        <ItemStyle Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valor Concepto" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <p>
                                                <asp:Label ID="lblValorConcepto" runat="server" Text="">
                                                </asp:Label>
                                            </p>
                                        </ItemTemplate>
                                        <ItemStyle Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Observacion" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <p>
                                                <asp:Label ID="lblObservacion" runat="server" Text="">
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
