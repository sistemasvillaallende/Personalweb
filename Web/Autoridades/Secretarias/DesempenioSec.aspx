<%@ Page Title="" Language="C#" MasterPageFile="~/Autoridades/Secretarias/MPSec.Master" AutoEventWireup="true" CodeBehind="DesempenioSec.aspx.cs" Inherits="web.Autoridades.Secretarias.DesempenioSec" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 100%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }

        #ContentPlaceHolder1_grdList_filter {
            margin-bottom: 15px;
            float: left;
            position: relative;
            width: 100%;
            padding-bottom: 15px;
            text-align: left;
        }

        thead {
            display: none;
        }

        table {
            border: none;
        }
    </style>
    <style type="text/css">
        .table tr th {
            border-color: var(--border-color);
            background-color: var(--border-color);
            color: var(--color-800);
            text-transform: uppercase;
            font-size: 12px;
            height: 40px;
            vertical-align: middle;
        }
    </style>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css"
        integrity="sha384-xOolHFLEh07PJGoPkLv1IbcEPTNtaed2xpHsD9ESMhqIYd0nLMwNLD69Npy4HI+N"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.7/css/jquery.dataTables.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.jquery.com/jquery.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-Fy6S3B9q64WdZWQUiU+q4/2Lc9npb8tCaSX9FK7E8HnRr0Jz8D6OP9dO5Vg3Q9ct"
        crossorigin="anonymous"></script>

    <div class="tab-pane" id="tabInforme">
        <asp:HiddenField ID="hIdEvaluacion" runat="server" />
        <div class="panel-body">
            <div class="container-fluid shadow p-3 mb-5 bg-white rounded"
                style="background-color: white; padding-left: 25px !important; padding-top: 5px !important;">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-md-12 col-md-offset-0" style="z-index: 500; margin-bottom: 20px;">
                        <div class="box-body">
                            <div class="row">
                                <div class="box-header with-border">
                                    <div class="row">
                                        <div class="col-md-6" style="padding-top: 10px;">
                                            <h3 style="font-size: 24px; color: var(--bs-gray-600);"
                                                id="lblSecretaria" runat="server"></h3>
                                        </div>
                                        <div class="col-md-6" style="padding-top: 10px; text-align: right;">
                                            <span id="lblFichaActiva" runat="server"
                                                style="padding: 12px; font-size: 16px; font-weight: 600; margin-right: 20px;"
                                                class="badge badge-pill badge-info">Evaluación de desempeño 2023</span>
                                            <div class="dropdown" style="display: none;">
                                                <button class="btn btn-secondary dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="false">
                                                    Seleccione la evaluación de desempeño a realizar
                                                </button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item" href="#">Action</a>
                                                    <a class="dropdown-item" href="#">Another action</a>
                                                    <a class="dropdown-item" href="#">Something else here</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- ////////////////////////////// GRILLA DETALLE /////////////////////////////////////// -->
                    <div class="auto-style1">
                        <nav>
                            <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                <button class="nav-link active" id="nav-home-tab" data-toggle="tab"
                                    data-target="#nav-home" type="button" role="tab"
                                    aria-controls="nav-home" aria-selected="true">
                                    Directores</button>
                                <button class="nav-link" id="nav-profile-tab" data-toggle="tab"
                                    data-target="#nav-profile" type="button" role="tab"
                                    aria-controls="nav-profile" aria-selected="false">
                                    Personal a cargo directo</button>
                            </div>
                        </nav>
                        <div class="tab-content" id="nav-tabContent">
                            <div class="tab-pane fade show active" id="nav-home" role="tabpanel"
                                aria-labelledby="nav-home-tab" style="padding: 20px; padding-top: 40px;">
                                <asp:GridView ID="grdList"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    EmptyDataText="No hay resultados..."
                                    Width="100%"
                                    CssClass="table"
                                    CellPadding="4" ForeColor="Black"
                                    OnRowDataBound="grdList_RowDataBound"
                                    DataKeyNames="legajo"
                                    GridLines="Horizontal"
                                    Font-Names="Ubuntu, sans-serif;"
                                    AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Empleado">
                                            <ItemTemplate>
                                                <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                                    <strong><%#Eval("nombre")%></strong>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                    Legajo: <%#Eval("legajo")%> - Categoria: <%#Eval("cod_categoria")%>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 0px;">
                                                    <div style="padding-top: 2px; padding-bottom: 2px; font-size: 14px;"
                                                        id="lblEstadoEvaluacion" runat="server"
                                                        role="alert">
                                                    </div>
                                                </p>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contacto" Visible="false">
                                            <ItemTemplate>
                                                <p style="font-size: 12px; margin-top: 0; margin-bottom: 5px;">
                                                    <strong><%#Eval("telefonos")%></strong>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                    <%#Eval("celular")%>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 0px;">
                                                    <%#Eval("email")%>
                                                </p>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contratación">
                                            <ItemTemplate>
                                                <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                                    <strong><%#Eval("des_tipo_liq")%></strong>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                    Fecha de ingreso 
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                    <%#Eval("fecha_ingreso")%>
                                                </p>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                                    <strong><%#Eval("secrectaria")%></strong>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                    <%#Eval("direccion")%>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 0px;">
                                                    <%#Eval("oficina")%>
                                                </p>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Datos Bancarios">
                                            <ItemTemplate>
                                                <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                                    <strong><%#Eval("nom_banco")%></strong>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                    Caja de ahorro: <%#Eval("nro_caja_ahorro")%>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                    CBU: <%#Eval("nro_cbu")%>
                                                </p>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Datos Bancarios">
                                            <ItemTemplate>
                                                <a class="btn btn-outline-primary" id="btnEnviarEvaluacion" runat="server"
                                                    href="#">
                                                    <span class="fa fa-graduation-cap"></span>
                                                    Evaluar
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="tab-pane fade" id="nav-profile" role="tabpanel"
                                aria-labelledby="nav-profile-tab">
                                <asp:GridView ID="gvDirectos"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    EmptyDataText="No hay resultados..."
                                    Width="100%"
                                    CssClass="table"
                                    CellPadding="4" ForeColor="Black"
                                    OnRowDataBound="gvDirectos_RowDataBound"
                                    DataKeyNames="legajo"
                                    GridLines="Horizontal"
                                    Font-Names="Ubuntu, sans-serif;"
                                    AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Empleado">
                                            <ItemTemplate>
                                                <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                                    <strong><%#Eval("nombre")%></strong>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                    Legajo: <%#Eval("legajo")%> - Categoria: <%#Eval("cod_categoria")%>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 0px;">
                                                    <div style="padding-top: 2px; padding-bottom: 2px; font-size: 14px;"
                                                        id="lblEstadoEvaluacion" runat="server"
                                                        role="alert">
                                                    </div>
                                                </p>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contacto" Visible="false">
                                            <ItemTemplate>
                                                <p style="font-size: 12px; margin-top: 0; margin-bottom: 5px;">
                                                    <strong><%#Eval("telefonos")%></strong>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                    <%#Eval("celular")%>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 0px;">
                                                    <%#Eval("email")%>
                                                </p>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contratación">
                                            <ItemTemplate>
                                                <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                                    <strong><%#Eval("des_tipo_liq")%></strong>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                    Fecha de ingreso 
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                    <%#Eval("fecha_ingreso")%>
                                                </p>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                                    <strong><%#Eval("secrectaria")%></strong>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                    <%#Eval("direccion")%>
                                                </p>
                                                <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 0px;">
                                                    <%#Eval("oficina")%>
                                                </p>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Datos Bancarios">
                                            <ItemTemplate>
                                                <a class="btn btn-outline-primary" id="btnEnviarEvaluacion" runat="server"
                                                    href="#">
                                                    <span class="fa fa-graduation-cap"></span>
                                                    Evaluar
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
