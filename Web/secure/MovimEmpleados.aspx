<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true"
    CodeBehind="MovimEmpleados.aspx.cs" Inherits="web.secure.MovimEmpleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../App_Themes/Estilos2025/main.css" rel="stylesheet" />
    <style>
        .timeline__event__title {
            font-size: 16px;
            line-height: 1.4;
            text-transform: capitalize;
            font-weight: 600;
            color: darkcyan;
            letter-spacing: 1.5px;
        }

        .marco {
            padding: 35px;
            border-radius: 15px;
            box-shadow: 0 3px 6px rgba(0, 0, 0, .16), 0 3px 6px rgba(0, 0, 0, .23) !important;
            margin-bottom: 20px;
            border: none !important;
            padding-top: 15px;
        }

        .timeline__event {
            margin-left: 10px !important;
            background: #fff;
            margin-bottom: 0;
            position: relative;
            display: flex;
            border-radius: 0;
            border-left: none;
            box-shadow: none !important;
            min-height: 90px;
        }

            .timeline__event::before {
                position: absolute;
                left: -10px;
                top: 0;
                content: " ";
                border: 8px solid #d48d31;
                border-image-slice: 1;
                border-radius: 50%;
                background: white;
                height: 25px;
                width: 25px;
                margin-right: 10px;
                z-index: 5;
            }

        .timeline__event__icon {
            border-radius: 8px 0 0 8px !important;
            background: white !important;
            border-right: none !important;
            display: flex !important;
            font-size: 2rem !important;
            padding: 20px !important;
            padding-bottom: 10px !important;
            padding-top: 2px !important;
            align-items: baseline !important;
            justify-content: start !important;
            padding-left: 30px !important;
        }

            .timeline__event__icon::before {
                content: "";
                position: absolute;
                top: 0;
                left: 0;
                width: 5px;
                height: 100%;
                background: linear-gradient(to bottom, red, yellow);
                z-index: 0;
            }

        .timeline__event__content {
            padding: 10px;
            padding-top: 2px;
            width: 100%;
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
            <div class="row">
                <div class="col-md-12">
                    <hr style="margin-top: 0; border: solid;" />
                    <button type="button"
                        style="border: solid 2px lightgray; font-family: 'Vastago Grotesk', sans-serif !important; font-weight: 500 !important; font-size: 14px !important; color: var(--color-dark) !important; padding-top: 5px; padding-bottom: 5px; height: 38.8px; border-top-left-radius: 10px; border-top-right-radius: 10px; padding-left: 10px; padding-right: 10px;"
                        onclick="perfil()">
                        Información Laboral
                    </button>
                    <button type="button" style="margin-left: -5px; padding-top: 5px; padding-bottom: 5px; border: solid darkcyan; border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: darkcyan; color: white; font-weight: 600;"
                        onclick="movimientos()">
                        Historial movimientos</button>

                    <button type="button"
                        style="border: solid 2px lightgray; font-family: 'Vastago Grotesk', sans-serif !important; font-weight: 500 !important; font-size: 14px !important; color: var(--color-dark) !important; height: 38.5; border-top-left-radius: 10px; margin-left: -5px; border-top-right-radius: 10px; padding-left: 10px; padding-right: 10px;"
                        onclick="horarios()">
                        Horario y Marcaciones</button>
                </div>
            </div>
            <div class="row" style="padding: 15px; padding-top: 0;">
                <div class="col-md-6" style="padding-left: 0; border: solid 1px lightgray;">
                    <div style="background-color: white; border-radius: 15px; padding: 20px;">
                        <h3
                            style="margin-bottom: 20px; font-size: 20px !important; font-weight: 600 !important; border-bottom: solid 2px lightgray; border-right: none; padding-bottom: 5px; color: #d48d31 !important;">Movimientos internos</h3>
                        <asp:PlaceHolder ID="phMovimientosInternos" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
                <div class="col-md-6" style="background-color: white; border: solid 1px lightgray; border-left:none !important">
                    <h3
                        style="margin-bottom: 20px; font-size: 20px !important; font-weight: 600 !important; border-bottom: solid 2px lightgray; padding-bottom: 5px; color: #d48d31 !important; margin-top: 20px;">Variación Conceptos de Sueldo</h3>
                    <asp:PlaceHolder ID="phVariacionConceptos" runat="server"></asp:PlaceHolder>

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
