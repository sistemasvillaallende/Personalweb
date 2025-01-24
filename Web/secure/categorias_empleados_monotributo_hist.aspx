<%@ Page Language="C#" MasterPageFile="~/MP/MasterNew.Master"  AutoEventWireup="true" CodeBehind="categorias_empleados_monotributo_hist.aspx.cs" Inherits="web.secure.categorias_empleados_monotributo_hist" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridview a:hover {
                    background-color: #1e8d12;
                    color: #fff;
                }

            .gridview span {
                background-color: #ae2676;
                color: #fff;
                /*-o-box-shadow: 1px 1px 1px #111;*/
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 10px 5px 10px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
                <Triggers>
                    <asp:PostBackTrigger ControlID="LinkExportar" />
                </Triggers>
                <ContentTemplate>
                    <div class="row" style="margin-top: 30px; padding-top: 30px">
                        <!-- Content Header (Page header) -->
                        <%--<section class="content-header">--%>
                        <%--<div class="row" style="margin-top: 40px; padding-top: 40px">--%>
                        <asp:UpdatePanel ID="PanelInfomacion" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="alert alert-success alert-dismissable" runat="server" id="divConfirma"
                                    visible="false" role="alert">
                                    <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Confirma');">
                                        <span aria-hidden="true">×</span></button>
                                    </button>
                                <h4>Aviso Importante!</h4>
                                    <p id="msjConfirmar" runat="server">
                                    </p>
                                </div>

                                <div class="alert alert-warning alert-dismissible" runat="server" id="divError"
                                    visible="false" role="alert">
                                    <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Alerta');">
                                        <span aria-hidden="true">×</span></button>
                                    </button>
                                <h4>Error!</h4>
                                    <p id="txtError" runat="server">
                                    </p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--</div>--%>
                        <%--</section>--%>
                    </div>
                    <!-- Main content -->
                    <%--<section class="content">--%>

                    <div class="outer_div">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <%--style="margin-top: 10px;"--%>
                                    <div class="box-header with-border">
                                        <div class="col-md-12">
                                            <h3 class="box-title">Historial categoria empleados Monotributo</h3>
                                        </div>
                                        <br />
                                    </div>
                                 
                                    <div class="row" style="margin-top: 20px;">
                                        <%--<div class="col-md-10 col-md-offset-1" id="divLista" runat="server">--%>
                                        <div class="auto-style1" style="margin-top: 20px;">
                                            <asp:GridView ID="gvCategoriasMono"
                                                CssClass="table"
                                                runat="server"
                                                OnRowDataBound="gvCategoriasMono_RowDataBound"
                                                OnRowCommand="gvCategoriasMono_RowCommand"
                                                CellPadding="4"
                                                AutoGenerateColumns="False"
                                                ForeColor="#333333"
                                                GridLines="None" DataKeyNames="id_profesional_monotributo" AllowPaging="True"
                                                OnPageIndexChanging="gvCategoriasMono_PageIndexChanging" PageSize="8">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Id id_profesional_monotributo " ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblIdCateMono" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Fecha Movimiento" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblFecha_alta" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Id movimiento" ItemStyle-Width="50%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblCategoria" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="50%" />
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Monto" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <p>
                                                        <asp:Label ID="lblMonto" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>--%>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMonto"
                                                                CssClass="form-control"
                                                                Text='<%#Eval("monto")%>'
                                                                Enabled="false"
                                                                runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                
                                                </Columns>
                                                <EditRowStyle BackColor="#999999"></EditRowStyle>
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                <%--<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>--%>
                                                <PagerStyle CssClass="gridview"></PagerStyle>
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                            </asp:GridView>
                                        </div>

                                        <!-- /.box-body -->
                                        <div class="box-footer clearfix">
                                        </div>
                                    </div>
                                    <!-- /.box -->
                                </div>
                                <!-- /.col -->
                            </div>
                            <!-- /.row -->


                        </div>
                            </div>
                        </div>

                            
                </ContentTemplate>
        </div>
    </div>
</asp:Content>


