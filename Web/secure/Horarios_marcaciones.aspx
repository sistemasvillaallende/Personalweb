<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="Horarios_marcaciones.aspx.cs" Inherits="web.secure.Horarios_marcaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        ul {
            margin-bottom: 5px !important;
        }

        .table tr th {
            border-color: var(--border-color) !important;
            background-color: transparent !important;
            color: var(--color-800) !important;
            text-transform: capitalize !important;
            font-size: 14px !important;
        }

        .table tr td {
            border-color: var(--border-color);
            color: var(--color-500);
            background-color: transparent;
            padding: 5px;
            font-size: 14px;
            text-transform: capitalize;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="background-color: lightgray; padding: 15px;">
        <div class="card-body box-profile"
            style="padding-bottom: 5px; background-color: white; border-radius: 15px; box-shadow: 0 3px 6px rgba(0, 0, 0, .16), 0 3px 6px rgba(0, 0, 0, .23) !important;">
            <div class="row">
                <div class="col-md-1" style="padding: 0; padding-left: 5px;">
                    <img class="profile-user-img img-fluid img-circle"
                        style="border-radius: 15px; border: none;"
                        src="../img/usuario.png"
                        alt="User profile picture" />
                </div>
                <div class="col-md-11">
                    <div class="row">
                        <div class="col-md-6">
                            <h5
                                style="color: #212529; font-size: 20px; font-weight: 700; margin-bottom: 5px;">
                                <span id="txtNombre" runat="server"></span>
                                <span style="color: #dc3545; font-size: 20px; font-weight: 700; margin-left: 10px;">#Legajo: </span>
                                <span style="color: #dc3545; font-size: 20px; font-weight: 700" runat="server" id="txtLegajo"></span></h5>
                            <p>
                                <span style="font-size: 14px; font-weight: 400;" id="lblDomicilio" runat="server"></span>
                                CP: <span id="lblCodPostal" runat="server"></span>
                            </p>
                        </div>
                        <div class="col-md-2" style="text-align: left;">
                            <p style="margin-bottom: 1px;">
                                <span
                                    style="color: darkcyan; font-weight: 300;">Fec. Nacimiento</span>
                            </p>
                            <p id="lblFecha_nacimiento" runat="server"
                                style="font-weight: 600; text-align: left;">
                            </p>
                        </div>
                        <div class="col-md-2" style="text-align: left;">
                            <p style="margin-bottom: 1px;">
                                <span
                                    style="color: darkcyan; font-weight: 300;">Sexo</span>
                            </p>
                            <p id="lblSexo" runat="server" style="font-weight: 600;"></p>
                        </div>
                        <div class="col-md-2" style="text-align: left;">
                            <p style="margin-bottom: 1px;">
                                <span
                                    style="color: darkcyan; font-weight: 300;">Estado Civil</span>

                            </p>
                            <p id="lblEstadoCivil" runat="server" style="font-weight: 600;"></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <p>
                                <span class="fa fa-id-card"
                                    style="color: darkcyan; margin-right: 5px;"></span>
                                <span
                                    style="color: darkcyan; margin-right: 5px;">CUIT: </span><span style="font-size: 14px; font-weight: 400; margin-right: 25px"
                                        id="lblCuit" runat="server"></span>
                                <span class="fa fa-phone"
                                    style="color: darkcyan; margin-right: 5px;"></span>
                                <span
                                    style="color: darkcyan; margin-right: 5px;">Telefonos: </span><span style="font-size: 14px; font-weight: 400; margin-right: 25px"
                                        id="lblTelefono" runat="server"></span>
                                <span class="fa fa-envelope"
                                    style="color: darkcyan; margin-right: 5px;"></span>
                                <span
                                    style="color: darkcyan; margin-right: 5px;">Mail: </span><span style="font-size: 14px; font-weight: 400; margin-right: 25px"
                                        id="lblMail" runat="server"></span>

                                <span class="fa fa-envelope"
                                    style="color: darkcyan; margin-right: 5px;"></span>
                                <span
                                    style="color: darkcyan; margin-right: 5px;">N° Afiliado OS: </span><span style="font-size: 14px; font-weight: 400; margin-right: 25px"
                                        id="lblOS" runat="server"></span>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-bottom:10px;">
                <div class="col-md-12">
                    <hr style="margin-top: 0; border: solid;" />
                    <button style="border: solid 2px lightgray; font-family: 'Vastago Grotesk', sans-serif !important; font-weight: 500 !important; font-size: 14px !important; color: var(--color-dark) !important; padding-top: 5px; padding-bottom: 5px; height: 38.8px; margin-left: 0px; border-top-left-radius: 10px; border-top-right-radius: 10px; padding-left: 10px; padding-right: 10px;"
                        onclick="perfil()">
                        Información Laboral</button>
                    <button type="button"
                        style="border: solid 2px lightgray; font-family: 'Vastago Grotesk', sans-serif !important; font-weight: 500 !important; font-size: 14px !important; color: var(--color-dark) !important; padding-top: 5px; padding-bottom: 5px; height: 38.8px; margin-left: -5px; border-top-left-radius: 10px; border-top-right-radius: 10px; padding-left: 10px; padding-right: 10px;"
                        onclick="movimientos()">
                        Historial movimientos
                    </button>
                    <button
                        style="padding-top: 5px; padding-bottom: 5px;  margin-left: -5px; border: solid darkcyan; border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: darkcyan; color: white; font-weight: 600;"
                        onclick="horarios()">
                        Horario y Marcaciones</button>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <div class="box" style="background-color: white; border: solid 1px lightgray; padding: 15px; border-radius: 15px; margin-bottom: 20px;">
                        <div class="box-header">
                            <h3 class="box-title" id="lblTurno" style="font-size: 18px;" runat="server">Hoario Laboral</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body no-padding">
                            <table class="table">
                                <tbody id="divTablaHorarios" runat="server">
                                </tbody>
                            </table>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <div class="box" style="border: solid 1px lightgray; padding: 15px; border-radius: 15px; margin-bottom: 20px; background-color: white">
                        <div class="box-header">
                            <h3 class="box-title" style="font-size: 18px;" id="H1" runat="server">Dias de Razones Particulares <span id="lblAnio" runat="server"></span></h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body no-padding">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>Utilizados</th>
                                        <th>Disponibles</th>
                                    </tr>
                                </thead>
                                <tbody id="Tbody1" runat="server">
                                </tbody>
                            </table>
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>Fechas</th>
                                    </tr>
                                </thead>
                                <tbody id="Tbody2" runat="server">
                                </tbody>
                            </table>
                        </div>
                        <!-- /.box-body -->
                    </div>
                </div>
                <div class="col-md-9">
                    <asp:UpdatePanel ID="uPanelFichadas" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div class="box box-primary" style="border: solid 1px lightgray; border-radius: 15px; padding: 15px; background-color: white;">
                                <div class="box-header" style="display: flex;">
                                    <div class="col-md-6">
                                        <h4 style="font-size: 18px;">Marcaciones</h4>
                                    </div>

                                    <div class="col-md-3">
                                        <asp:DropDownList ID="DDLAnio" OnSelectedIndexChanged="DDLAnio_SelectedIndexChanged"
                                            AutoPostBack="true"
                                            CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="DDLMes" CssClass="form-control" runat="server"
                                            AutoPostBack="true" OnSelectedIndexChanged="DDLMes_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <asp:GridView
                                        ID="gvFichadas"
                                        CssClass="table"
                                        OnRowDataBound="gvFichadas_RowDataBound"
                                        runat="server"
                                        CellPadding="4"
                                        ForeColor="#333333"
                                        AutoGenerateColumns="false"
                                        GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="E1" HeaderText="Entra" />
                                            <asp:BoundField DataField="S1" HeaderText="Sale" />
                                            <asp:BoundField DataField="E2" HeaderText="Entra" />
                                            <asp:BoundField DataField="S2" HeaderText="Sale" />
                                            <asp:BoundField DataField="HORAS" HeaderText="Horas" />
                                            <asp:BoundField DataField="HORAS_EXTRAS" HeaderText="Horas Extras" />
                                            <asp:TemplateField HeaderText="Novedades">
                                                <ItemTemplate>
                                                    <span id="lblNovedad" runat="server"></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="alert alert-danger alert-dismissible" id="divError" runat="server" visible="false">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                        <h4><i class="icon fa fa-ban"></i>Error!</h4>
                        <p id="txtError" runat="server"></p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function movimientos() {
            const parametros = new URLSearchParams(window.location.search);
            const leg = parametros.get("legajo");

            if (leg) {
                // Redirige a la nueva página con el query string obtenido
                window.location.href = `./MovimEmpleados.aspx?legajo=${encodeURIComponent(leg)}`;
            } else {
                console.warn(`El parámetro "${leg}" no existe en la URL.`);
            }
        }
        function perfil() {
            const parametros = new URLSearchParams(window.location.search);
            const leg = parametros.get("legajo");

            if (leg) {
                // Redirige a la nueva página con el query string obtenido
                window.location.href = `./PerfilEmpleado.aspx?legajo=${encodeURIComponent(leg)}`;
            } else {
                console.warn(`El parámetro "${leg}" no existe en la URL.`);
            }
        }
        function horarios() {
            const parametros = new URLSearchParams(window.location.search);
            const leg = parametros.get("legajo");

            if (leg) {
                // Redirige a la nueva página con el query string obtenido
                window.location.href = `./Horarios_marcaciones.aspx?legajo=${encodeURIComponent(leg)}`;
            } else {
                console.warn(`El parámetro "${leg}" no existe en la URL.`);
            }
        }
    </script>
</asp:Content>
