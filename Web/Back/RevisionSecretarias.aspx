<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MP_Desempenio.Master" AutoEventWireup="true"
    CodeBehind="RevisionSecretarias.aspx.cs" Inherits="web.secure.RevisionSecretarias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        @font-face {
            font-family: 'Roboto';
            src: url('../App_Themes/NewTheme/fonts/VastagoGrotesk-Regular.otf');
        }

        .secretaria {
            box-shadow: 12px 13px 11px -8px rgba(0,0,0,0.5);
            width: 100%;
            border-radius: 15px;
            padding: 20px;
            text-align: center;
            min-height: 160px;
            max-height: 160px;
            border: solid 1px gray;
        }

        .desarrollo_humano {
            background-color: #ff0000;
        }

        .servicios_publicos {
            background-color: #a4c515;
        }

        .gobierno {
            background-color: #8400A8;
        }

        .salud {
            background-color: #00A0FF;
        }

        .planificacion {
            background-color: #0000d1;
        }

        .hacienda {
            background-color: #00C374;
        }

        .desarrollo-social {
            background-color: #d27530;
        }

        .img-secretaria {
            max-height: 60px;
            max-width: 90%;
        }

        .obras-publicas {
            background-color: #EB1E79;
        }

        .desarrollo_social {
            background-color: #d27530;
        }

        .obras-publicas {
            background-color: #EB1E79;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div style="margin-top: 0px;" class="col-md-12">
                <h3 style="padding-left: 5px !important; font-size: 20px !important; color: var(--primary-color) !important; font-weight: 600 !important;">Secretarias</h3>
                <hr style="margin-top: 10px; border-top: 3px solid lightgray; opacity: 1; margin-left: 5px; margin-right: 5px;">
            </div>
        </div>
        <div class="row">
            <div style="margin-top: 15px;" class="col-xl-2 col-lg-3 col-md-4 col-sm-4 col-xs-6">
                <a href="../Autoridades/DashboardSecretaria.aspx?idSec=46">
                    <div class="secretaria">
                        <img src="../imagenes/intendencia.png?v=1" class="img-secretaria" />
                        <label style="margin-top: 15px !important; color: #495057; font-size: 20px; font-weight: 700; text-align: center; line-height: 1.1; margin-top: 10px; font-family: 'VastagoGrotesk', sans-serif;">
                            Intendencia</label>
                    </div>
                </a>
            </div>
            <div style="margin-top: 15px;" class="col-xl-2 col-lg-3 col-md-4 col-sm-4 col-xs-6">
                <a href="../Autoridades/DashboardSecretaria.aspx?idSec=51">
                    <div class="secretaria">
                        <img src="../imagenes/planificacion.png?v=1" class="img-secretaria" />
                        <label style="margin-top: 15px !important; color: #0000d1; font-size: 20px; font-weight: 700; text-align: center; line-height: 1.1; margin-top: 10px; font-family: 'VastagoGrotesk', sans-serif;">
                            Planif. y Modernización</label>
                    </div>
                </a>
            </div>           
            <div style="margin-top: 15px;" class="col-xl-2 col-lg-3 col-md-4 col-sm-4 col-xs-6">
                <a href="../Autoridades/DashboardSecretaria.aspx?idSec=48">
                    <div class="secretaria">
                        <img src="../imagenes/gobierno.png?v=1" class="img-secretaria" />
                        <label style="margin-top: 15px !important; color: #9B00AD; font-size: 20px; font-weight: 700; text-align: center; line-height: 1.1; margin-top: 10px; font-family: 'VastagoGrotesk', sans-serif;">
                            Gobierno y Seguridad</label>
                    </div>
                </a>
            </div>     
            <div style="margin-top: 25px;" class="col-xl-2 col-lg-3 col-md-4 col-sm-4 col-xs-6">
                <a href="../Autoridades/DashboardSecretaria.aspx?idSec=47">
                    <div class="secretaria">
                        <img src="../imagenes/hacienda.png?v=2" class="img-secretaria" />
                        <label style="margin-top: 15px !important; color: #00C374; font-size: 20px; font-weight: 700; text-align: center; line-height: 1.1; margin-top: 10px; font-family: 'VastagoGrotesk', sans-serif;">
                            Economía y Finanzas</label>
                    </div>
                </a>
            </div>            
            <div style="margin-top: 15px;" class="col-xl-2 col-lg-3 col-md-4 col-sm-4 col-xs-6">
                <a href="../Autoridades/DashboardSecretaria.aspx?idSec=53">
                    <div class="secretaria">
                        <img src="../imagenes/salud.png?v=1" class="img-secretaria" />
                        <label style="width: 100%; margin-top: 15px !important; color: #00A0FF; font-size: 20px; font-weight: 700; text-align: center; line-height: 1.1; margin-top: 10px; font-family: 'VastagoGrotesk', sans-serif;">
                            Salud</label>
                    </div>
                </a>
            </div>            
            <div style="margin-top: 15px;" class="col-xl-2 col-lg-3 col-md-4 col-sm-4 col-xs-6">
                <a href="../Autoridades/DashboardSecretaria.aspx?idSec=54">
                    <div class="secretaria">
                        <img src="../imagenes/desarrollo_humano.png?v=1" class="img-secretaria" />
                        <label style="margin-top: 15px !important; color: #FF0000; font-size: 20px; font-weight: 700; text-align: center; line-height: 1.1; margin-top: 10px; font-family: 'VastagoGrotesk', sans-serif;">
                            Desarrollo Humano</label>
                    </div>
                </a>
            </div>
            <div style="margin-top: 25px;" class="col-xl-2 col-lg-3 col-md-4 col-sm-4 col-xs-6">
                <a href="../Autoridades/DashboardSecretaria.aspx?idSec=49">
                    <div class="secretaria">
                        <img src="../imagenes/desarrollo_social.png?v=2" class="img-secretaria" />
                        <label style="margin-top: 15px !important; color: #EC7624; font-size: 20px; font-weight: 700; text-align: center; line-height: 1.1; margin-top: 10px; font-family: 'VastagoGrotesk', sans-serif;">
                            Promoc. Fliar.
                        </label>
                    </div>
                </a>
            </div>            
            <div style="margin-top: 15px;"
                class="col-xl-2 col-lg-3 col-md-4 col-sm-4 col-xs-6">
                <a href="../Autoridades/DashboardSecretaria.aspx?idSec=50">
                    <div class="secretaria">
                        <img src="../imagenes/obras_publicas.png?v=1" class="img-secretaria" />
                        <label style="margin-top: 15px !important; color: white; font-size: 20px; font-weight: 700; text-align: center; line-height: 1.1; margin-top: 10px; font-family: 'VastagoGrotesk', sans-serif; color: #A4C515;">
                            Obras Publicas</label>
                    </div>
                </a>
            </div>
            <div style="margin-top: 25px;" class="col-xl-2 col-lg-3 col-md-4 col-sm-4 col-xs-6">
                <a href="../Autoridades/DashboardSecretaria.aspx?idSec=52">
                    <div class="secretaria">
                        <img src="../imagenes/obras_publicas.png?v=2" class="img-secretaria" />
                        <label style="margin-top: 15px !important; color: #EB1E79; font-size: 20px; font-weight: 700; text-align: center; line-height: 1.1; margin-top: 10px; font-family: 'VastagoGrotesk', sans-serif;">
                            Obras Públicas</label>
                    </div>
                </a>
            </div>
        </div>
    </div>
</asp:Content>
