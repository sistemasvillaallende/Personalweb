<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" 
    AutoEventWireup="true" CodeBehind="PerfilEmpleado.aspx.cs" 
    Inherits="web.secure.PerfilEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        dt {
            font-weight: 300;
            color: darkcyan;
            font-size: 16px;
            margin-bottom: 10px;
            border-bottom: solid 1px lightgray;
            margin-left: 20px;
            padding-left: 0 !important;
        }

        dd {
            font-weight: 400;
            color: black;
            font-size: 16px;
            margin-bottom: 10px;
            border-bottom: solid 1px lightgray;
            text-align: left;
            margin-left: -40px;
            margin-right: 20px;
            padding-left: 35px !important;
        }

        a {
            color: darkcyan !important;
            border-radius: 0 !important
        }

        .list-group-item {
            padding: 0.5rem 1rem !important;
            padding-left: 0 !important;
            padding-right: 0 !important;
        }

        .nav-pills .nav-link.active, .nav-pills .show > .nav-link {
            color: black !important;
            background-color: white;
        }

        .texto-cortado {
            width: 200px; /* Ajusta según tu diseño */
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
            display: inline-block;
            cursor: pointer;
        }
    </style>
    <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />
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
            <div class="row">
                <div class="col-md-12">
                    <hr style="margin-top: 0; border: solid;" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <button type="button" style="padding-top: 5px; padding-bottom: 5px; border: solid darkcyan; border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: darkcyan; color: white; font-weight: 600;"
                        onclick="perfil()">
                        Información Laboral</button>
                    <button type="button"
                        style="border: solid 2px lightgray; font-family: 'Vastago Grotesk', sans-serif !important; font-weight: 500 !important; font-size: 14px !important; color: var(--color-dark) !important; padding-top: 5px; padding-bottom: 5px; height: 38.8px; margin-left: -5px; border-top-left-radius: 10px; border-top-right-radius: 10px; padding-left: 10px; padding-right: 10px;"
                        onclick="movimientos()">
                        Historial movimientos
                    </button>
                    <button type="button"
                        style="border: solid 2px lightgray; font-family: 'Vastago Grotesk', sans-serif !important; font-weight: 500 !important; font-size: 14px !important; color: var(--color-dark) !important; height: 38.5; border-top-left-radius: 10px; margin-left: -5px; border-top-right-radius: 10px; padding-left: 10px; padding-right: 10px;"
                        onclick="horarios()">
                        Horario y Marcaciones</button>
                    <div class="active tab-pane" id="datosEmpleado">
                        <div class="card" style="border-bottom: none;">
                            <div class="card-body" style="padding-bottom: 0;">
                                <div class="row" style="margin-top: 0px;">
                                    <div class="col-md-4" style="padding-right: 5px;">
                                        <div class="card-body box-profile"
                                            style="min-height: 420px; max-height: 420px; padding: 15px; padding-top: 15px; border-radius: 15px; border-top-right-radius: 0px; border-bottom-left-radius: 0px; background-color: white;">
                                            <dl style="margin-bottom: 5px;" class="row">
                                                <dt class="col-sm-4"><span>Fec. Ingreso</span></dt>
                                                <dd class="col-sm-8" style="text-align: left;" runat="server" id="txtfecha_ingreso"></dd>
                                                <dt class="col-sm-4">Categoría</dt>
                                                <dd class="col-sm-8" style="text-align: left;" runat="server" id="txtCategoria"></dd>
                                                <dt class="col-sm-4">Tarea</dt>
                                                <dd class="col-sm-8" style="text-align: left;" runat="server" id="txtTarea"></dd>
                                                <dt class="col-sm-4">Cargo</dt>
                                                <dd class="col-sm-8" style="text-align: left;" runat="server" id="txtCargo"></dd>
                                                <dt class="col-sm-4">Oficina</dt>
                                                <dd class="col-sm-8" style="text-align: left;" runat="server" id="txtOficina"></dd>
                                                <dt class="col-sm-4">Programa</dt>
                                                <dd class="col-sm-8" style="text-align: left;" runat="server" id="txtPrograma"></dd>
                                                <dt class="col-sm-4">Dirección</dt>
                                                <dd class="col-sm-8" style="text-align: left;" runat="server" id="txtDireccion"></dd>
                                                <dt class="col-sm-4">Secretaria</dt>
                                                <dd class="col-sm-8" style="text-align: left;" id="txtSecretaria" runat="server"></dd>
                                            </dl>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="padding-left: 5px; padding-right: 5px;">
                                        <div class="card-body box-profile"
                                            style="min-height: 420px; max-height: 420px; padding-left: 20px; padding-top: 15px; border-radius: 15px; background-color: white;">
                                            <dl style="margin-bottom: 5px;" class="row">
                                                <dt class="col-sm-4">Activo?</dt>
                                                <dd class="col-sm-8" id="txtActivo" runat="server"></dd>
                                                <dt class="col-sm-4">Fecha Baja</dt>
                                                <dd class="col-sm-8" id="txtFechaBaja" runat="server"></dd>
                                                <dt class="col-sm-5">Situacion Revista</dt>
                                                <dd class="col-sm-7" style="text-align: left;" runat="server" id="txtSituacionRevista"></dd>
                                                <dt class="col-sm-5">Fecha de Revista</dt>
                                                <dd class="col-sm-7" style="text-align: left;" runat="server" id="txtFechaRevista"></dd>
                                                <dt class="col-sm-5">Imprime Recibo</dt>
                                                <dd class="col-sm-7" style="text-align: left;" runat="server" id="txtImprimeResivo"></dd>
                                                <dt class="col-sm-5">Sección</dt>
                                                <dd class="col-sm-7" style="text-align: left;" runat="server" id="txtSeccion"></dd>
                                                <dt class="col-sm-5">Clasif. Personal</dt>
                                                <dd class="col-sm-7" style="text-align: left;" runat="server" id="txtClasificacionPersonal"></dd>
                                                <dt class="col-sm-5">Tipo Liquidación</dt>
                                                <dd class="col-sm-7" style="text-align: left;" runat="server" id="txtTipoLiq"></dd>
                                                <dt class="col-sm-5">Régimen</dt>
                                                <dd class="col-sm-7" style="text-align: left;" runat="server" id="txtRegimen"></dd>

                                            </dl>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="padding-right: 15px; padding-left: 5px;">
                                        <div class="card-body box-profile"
                                            style="padding: 15px; padding-bottom: 5px; padding-top: 15px; border-radius: 15px; background-color: white;">
                                            <dl class="row" style="margin-bottom: 5px;">
                                                <dt class="col-sm-5">Cta Suel. Basico</dt>
                                                <dd class="col-sm-7" style="text-align: left;" runat="server" id="txtCtaBasico"></dd>
                                                <dt class="col-sm-5">Cta Gastos</dt>
                                                <dd class="col-sm-7" style="text-align: left;" runat="server" id="txtNroCtaGastos"></dd>
                                                <dt class="col-sm-5">Escala Aumento</dt>
                                                <dd class="col-sm-7" style="text-align: left;" runat="server" id="txtEscalaAumento"></dd>
                                                <dt class="col-sm-5">Banco</dt>
                                                <dd class="col-sm-7" style="text-align: left" runat="server" id="lblBanco"></dd>
                                                <dt class="col-sm-5">Sucursal</dt>
                                                <dd class="col-sm-7" style="text-align: left" runat="server" id="lblSucursal"></dd>
                                                <dt class="col-sm-5">Caja de Ahorro N°</dt>
                                                <dd class="col-sm-7" style="text-align: left" runat="server" id="lblCuenta"></dd>
                                                <dt class="col-sm-5">CBU</dt>
                                                <dd class="col-sm-7" style="text-align: left" runat="server" id="lblCbu"></dd>
                                            </dl>
                                        </div>
                                        <div class="card-body box-profile" id="divContrato" runat="server"
                                            style="margin-top: 15px; padding: 15px; background-color: white; padding-bottom: 5px; padding-top: 15px; border-radius: 15px;">
                                            <dl class="row" style="margin-bottom: 5px;">
                                                <dt class="col-sm-8" style="padding-right: 0">Contrato N°:</dt>
                                                <dd class="col-sm-4" id="lblContrato" runat="server" style="padding-right: 0"></dd>
                                                <dt class="col-sm-8" style="padding-right: 0">Fecha Inicio:</dt>
                                                <dd class="col-sm-4" id="lblFecInicio" runat="server" style="padding-right: 0"></dd>
                                                <dt class="col-sm-8" style="padding-right: 0">Fecha Fin:</dt>
                                                <dd class="col-sm-4" id="lblFecFin" runat="server" style="padding-right: 0"></dd>
                                                <dt class="col-sm-8" style="padding-right: 0">Antiguedad Actual:</dt>
                                                <dd class="col-sm-4" id="lblAntiguedadActual" runat="server" style="padding-right: 0"></dd>
                                                <dt class="col-sm-8" style="padding-right: 0">Antiguedad Anterior:</dt>
                                                <dd class="col-sm-4" id="lblAntiguedadAnterior" runat="server" style="padding-right: 0"></dd>
                                                <dt class="col-sm-8" style="padding-right: 0">Nro Nombramiento:</dt>
                                                <dd class="col-sm-4" id="lblNroNombramiento" runat="server" style="padding-right: 0"></dd>
                                                <dt class="col-sm-8" style="padding-right: 0">Fecha Nombramiento:</dt>
                                                <dd class="col-sm-4" id="lblFechaNombramiento" runat="server" style="padding-right: 0"></dd>
                                            </dl>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
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
