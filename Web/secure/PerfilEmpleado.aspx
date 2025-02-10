<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="PerfilEmpleado.aspx.cs" Inherits="web.secure.PerfilEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        dt {
            font-weight: 400;
            color: black;
            font-size: 14px;
        }

        dd {
            font-weight: 500;
            color: darkcyan;
            font-size: 14px;
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
    </style>
    <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-3" style="padding-left: 0; padding-right: 0;">
                <div class="card card-primary card-outline">
                    <div class="card-body box-profile">
                        <div class="text-center">
                            <img class="profile-user-img img-fluid img-circle"
                                style="border-radius: 50%"
                                src="../App_Themes/AdminLTE/src/assets/img/user4-128x128.jpg"
                                alt="User profile picture" />
                        </div>

                        <h3 class="profile-username text-center">Andrea Brusa</h3>

                        <p class="text-muted text-center">Farmaceutica Hospital Municipal</p>

                        <ul class="list-group list-group-unbordered mb-3">
                            <li class="list-group-item">
                                <b style="font-weight: 500">Fecha Nacimiento:</b> <a class="float-right">19/10/1966</a>
                            </li>
                            <li class="list-group-item">
                                <b style="font-weight: 500">Sexo:</b> <a class="float-right">Femenino</a>
                            </li>
                            <li class="list-group-item">
                                <b style="font-weight: 500">Estado Civil:</b> <a class="float-right">Divorciada</a>
                            </li>
                            <li class="list-group-item">
                                <b style="font-weight: 500">DNI:</b> <a class="float-right">17.845.382</a>
                            </li>
                            <li class="list-group-item">
                                <b style="font-weight: 500">CUIT:</b> <a class="float-right">27-17.845.382-9</a>
                            </li>
                            <li class="list-group-item">
                                <b style="font-weight: 500;">Domicilio:</b>
                                <a class="float-right">Amazonas 125 B° ... </a>
                            </li>
                            <li class="list-group-item">
                                <b style="font-weight: 500">Código Postal:</b> <a class="float-right">5105</a>
                            </li>
                            <li class="list-group-item">
                                <b style="font-weight: 500">Telefono:</b> <a class="float-right">03543-439290 /035...</a>
                            </li>
                            <li class="list-group-item">
                                <b style="font-weight: 500">Mail:</b> <a class="float-right">Dirección Médica</a>
                            </li>
                        </ul>
                    </div>
                    <!-- /.card-body -->
                </div>
            </div>
            <div class="col-md-9">
                <ul class="nav nav-pills">
                    <li class="nav-item" style="border: solid 1px lightgray; font-family: 'Vastago Grotesk', sans-serif !important; font-weight: 500 !important; font-size: 14px !important; color: var(--color-dark) !important;">
                        <a class="nav-link active" href="#datosEmpleado"
                            style="padding-top: 5px; padding-bottom: 5px;"
                            data-toggle="tab">Datos Empleo</a></li>
                    <li class="nav-item" style="border: solid 1px lightgray;">
                        <a class="nav-link" style="padding-top: 5px; padding-bottom: 5px;"
                            href="#timeline" data-toggle="tab">Timeline</a></li>
                    <li class="nav-item" style="border: solid 1px lightgray;">
                        <a class="nav-link" href="#settings"
                            style="padding-top: 5px; padding-bottom: 5px;" data-toggle="tab">Settings</a></li>
                </ul>
                <div class="tab-content">
                    <div class="active tab-pane" id="datosEmpleado">
                        <div class="card" style="box-shadow: 0 3px 6px rgba(0, 0, 0, .16), 0 3px 6px rgba(0, 0, 0, .23) !important;">
                            <div class="card-header" style="padding-top: 10px; padding-bottom: 10px; border-bottom: solid 1px lightgray;">
                                <h3 class="card-title">
                                    <i class="fas fa-text-width"></i>
                                    Stuación Laboral
                                </h3>
                            </div>
                            <div class="card-body" style="padding-bottom: 0;">
                                <div class="row">
                                    <div class="col-md-6">
                                        <dl class="row" style="margin-bottom: 5px;">
                                            <dt class="col-sm-5">Fecha de Ingreso:</dt>
                                            <dd class="col-sm-7">01/03/2006</dd>
                                            <dt class="col-sm-5">Legajo:</dt>
                                            <dd class="col-sm-7">620</dd>
                                            <dt class="col-sm-5">Tarea:</dt>
                                            <dd class="col-sm-7">Farmaceutica Hospital Municipal</dd>
                                            <dt class="col-sm-5">Cargo</dt>
                                            <dd class="col-sm-7">46 - Jefe de Sección</dd>
                                            <dt class="col-sm-5">Seccion</dt>
                                            <dd class="col-sm-7">Profeciónal I</dd>
                                            <dt class="col-sm-5">Categoria</dt>
                                            <dd class="col-sm-7">19</dd>
                                            <dt class="col-sm-5">Clasificacion Personal</dt>
                                            <dd class="col-sm-7">Planta Permanente</dd>
                                            <dt class="col-sm-5">Tipo liquidación</dt>
                                            <dd class="col-sm-7">Efectivos</dd>
                                            <dt class="col-sm-5">Oficina de Trabajo</dt>
                                            <dd class="col-sm-7">Farmacia</dd>
                                            <dt class="col-sm-5">Programa</dt>
                                            <dd class="col-sm-7">Dirección Médica</dd>
                                            <dt class="col-sm-5">Dirección</dt>
                                            <dd class="col-sm-7">Dirección Médica</dd>
                                        </dl>
                                    </div>
                                    <div class="col-md-6">
                                        <dl class="row" style="margin-bottom: 5px;">
                                            <dt class="col-sm-5">Secretaria</dt>
                                            <dd class="col-sm-7">Secretaria de Salud</dd>
                                            <dt class="col-sm-5">Regimen</dt>
                                            <dd class="col-sm-7">SALUD Sev Dif ART 18 - Dto 964</dd>
                                            <dt class="col-sm-5">Escala Aumento</dt>
                                            <dd class="col-sm-7">Grupo 1 Aumento del 21 %</dd>
                                            <dt class="col-sm-5">Situacion de Revista</dt>
                                            <dd class="col-sm-7">Activo</dd>
                                            <dt class="col-sm-5">Fecha de Revista</dt>
                                            <dd class="col-sm-7">-</dd>
                                            <dt class="col-sm-5">Activo?</dt>
                                            <dd class="col-sm-7">Si</dd>
                                            <dt class="col-sm-5">Fecha Baja</dt>
                                            <dd class="col-sm-7">-</dd>
                                            <dt class="col-sm-5">Imprime Recibo?</dt>
                                            <dd class="col-sm-7">Si</dd>
                                            <dt class="col-sm-5">Nº Cta Sueldo Basico</dt>
                                            <dd class="col-sm-7">1.1.01.01.01.02.10 - JEFE DE SECCION</dd>
                                            <dt class="col-sm-5">Nº Cta Gastos</dt>
                                            <dd class="col-sm-7">-</dd>
                                        </dl>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 20px;">
                            <div class="col-md-6">
                                <div class="card" style="box-shadow: 0 3px 6px rgba(0, 0, 0, .16), 0 3px 6px rgba(0, 0, 0, .23) !important;">
                                    <div class="card-header" style="padding-top: 10px; padding-bottom: 10px; border-bottom: solid 1px lightgray;">
                                        <h3 class="card-title">
                                            <i class="fas fa-text-width"></i>
                                            Datos Bancarios
                                        </h3>
                                    </div>
                                    <div class="card-body" style="padding-bottom: 0;">
                                        <dl class="row" style="margin-bottom: 5px;">
                                            <dt class="col-sm-5">Banco:</dt>
                                            <dd class="col-sm-7">Bco.Macro S.A. Sucursal 051</dd>
                                            <dt class="col-sm-5">Cuenta:</dt>
                                            <dd class="col-sm-7">Caja de Ahorros N°: 800033/8</dd>
                                            <dt class="col-sm-5">CBU:</dt>
                                            <dd class="col-sm-7">3870051200801780003384</dd>
                                        </dl>
                                    </div>
                                </div>
                                <!-- /.card -->
                            </div>
                            <div class="col-md-6">
                                <div class="card" style="box-shadow: 0 3px 6px rgba(0, 0, 0, .16), 0 3px 6px rgba(0, 0, 0, .23) !important;">
                                    <div class="card-header" style="padding-top: 10px; padding-bottom: 10px; border-bottom: solid 1px lightgray;">
                                        <h3 class="card-title">
                                            <i class="fas fa-text-width"></i>
                                            Datos Bancarios
                                        </h3>
                                    </div>
                                    <div class="card-body" style="padding-bottom: 0;">
                                        <dl class="row" style="margin-bottom: 5px;">
                                            <dt class="col-sm-5">Banco:</dt>
                                            <dd class="col-sm-7">Bco.Macro S.A. Sucursal 051</dd>
                                            <dt class="col-sm-5">Cuenta:</dt>
                                            <dd class="col-sm-7">Caja de Ahorros N°: 800033/8</dd>
                                            <dt class="col-sm-5">CBU:</dt>
                                            <dd class="col-sm-7">3870051200801780003384</dd>
                                        </dl>
                                    </div>
                                </div>
                                <!-- /.card -->
                            </div>
                        </div>
                    </div>

                    <div class="active tab-pane" id="timeline">
                    </div>
                    <div class="active tab-pane" id="settings">
                    </div>

                </div>

            </div>
        </div>

    </div>

</asp:Content>
