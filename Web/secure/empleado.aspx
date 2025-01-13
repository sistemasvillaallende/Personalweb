<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="True" 
    CodeBehind="empleado.aspx.cs" Inherits="web.secure.empleado" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .margen {
            margin-left: 0px;
        }

        .nav-tabs .nav-link {
            margin-bottom: -1px;
            background-color: transparent;
            border: 1px solid transparent;
            border-top-left-radius: 0.25rem;
            border-top-right-radius: 0.25rem;
            border-bottom: solid 2px lightgray;
        }

            .nav-tabs .nav-item.show .nav-link, .nav-tabs .nav-link.active {
                color: #495057;
                background-color: #fff;
                border-color: #dee2e6 #dee2e6 #fff;
                border-width: 2px;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<div class="row">
        <div class="col-md-4" style="padding-top: 20px;">
            <div>
                <h4 runat="server">Gestión Empleados</h4>
            </div>
        </div>
    </div>--%>
    <div class="row">
        <div class="col-md-12">
            <div class="btn-group" style="padding-bottom: 20px;">
                <button type="button" class="btn btn-outline-primary" id="cmdConceptos" runat="server" onserverclick="cmdConceptos_ServerClick">
                    <span class="glyphicon glyphicon-unchecked"></span>&nbsp;Conceptos
                </button>
                <button type="button" class="btn btn-outline-primary" id="cmdConsLegajo" runat="server" onserverclick="cmdConsLegajo_ServerClick">
                    <span class="glyphicon glyphicon-asterisk"></span>&nbsp;Cons Legajo
                </button>
                <button type="button" class="btn btn-outline-primary" id="cmdCertificaciones" runat="server" onserverclick="cmdCertificaciones_ServerClick">
                    <span class="glyphicon glyphicon-certificate"></span>&nbsp;Certificaciones
                </button>
                <button type="button" class="btn btn-outline-primary" id="cmdAnses" runat="server">
                    <span class="glyphicon glyphicon-book"></span>&nbsp;Anses
                </button>
                <button type="button" class="btn btn-outline-primary" id="cmdFamiliares" runat="server" onserverclick="cmdFamiliares_ServerClick">
                    <span class="glyphicon glyphicon-picture"></span>&nbsp;Familiares
                </button>
                <button type="button" class="btn btn-outline-primary" id="cmdAportes">
                    <span class="glyphicon glyphicon-briefcase"></span>&nbsp;Aportes
                </button>
                <button type="button" class="btn btn-outline-primary" id="cmdAltaBcoprov">
                    <span class="glyphicon glyphicon-pencil"></span>&nbsp;Alta Bco Prov
                </button>

            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:UpdatePanel ID="PanelInfomacion" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="alert alert-success alert-dismissable" runat="server" id="divConfirma"
                        visible="false" role="alert">
                        <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Confirma');">
                            <span arial-hidden="true">&times;</span> <span class="sr-only">Cerrar</span>
                        </button>
                        <strong>Aviso Importante! </strong>
                        <p id="msjConfirmar" runat="server">
                        </p>
                    </div>

                    <div class="alert alert-warning alert-dismissible" runat="server" id="divAlerta"
                        visible="false" role="alert">
                        <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Alerta');">
                            <span arial-hidden="true">&times;</span> <span class="sr-only">Cerrar</span>
                        </button>
                        <strong>Aviso Importante! </strong>
                        <p id="msjAlerta" runat="server">
                        </p>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="container-fluid shadow p-3 mb-5 bg-white rounded"
        style="background-color: white; padding-left: 2px !important; padding-top: 0px !important; 
        padding-right: 2px !important; padding-bottom: 0 !important;">
        <div class="row">
            <%--<div class="col-md-12 col-md-offset-0">--%>
            <div class="box">
                <ul class="nav nav-tabs" id="myTab" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active" id="tab_Datos_Empleado-tab" data-toggle="tab"
                            data-target="#tab_Datos_Empleado" type="button" role="tab"
                            aria-controls="tab_Datos_Empleado" aria-selected="true">
                            Datos Empleado</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="tab_Datos_Contrato-tab" data-toggle="tab"
                            data-target="#tab_Datos_Contrato" type="button" role="tab"
                            aria-controls="tab_Datos_Contrato" aria-selected="false">
                            Datos de Contrato y Obra Social</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="tab_Datos_Banco-tab" data-toggle="tab"
                            data-target="#tab_Datos_Banco" type="button" role="tab"
                            aria-controls="tab_Datos_Banco" aria-selected="false">
                            Datos Bancarios</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="tab_Datos_Particulares-tab" data-toggle="tab"
                            data-target="#tab_Datos_Particulares" type="button" role="tab"
                            aria-controls="tab_Datos_Particulares" aria-selected="false">
                            Datos Particulares</button>
                    </li>
                </ul>
                <%--<div class="tab-content" style="min-height: 400px; padding-top: 25px">--%>
                <div class="tab-content" style="padding: 25px; background-color: white; border: solid 2px lightgray; border-top: none;">
                    <div class="tab-pane fade show active" id="tab_Datos_Empleado"
                        role="tabpanel" aria-labelledby="tab_Datos_Empleado-tab">
                        <div class="panel-body">
                            <!-- /.box-header -->
                            <div class="row" style="margin-bottom: 25px;">
                                <div class="col-md-2">
                                    <label>Legajo:</label>
                                    <asp:TextBox ID="txtLegajo" CssClass="form-control" runat="server" placeholder="Legajo">
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label>Fecha Ingreso:</label>
                                    <asp:TextBox ID="txtfecha_ingreso" CssClass="form-control" runat="server" placeholder="Ingrese Fecha"></asp:TextBox>
                                </div>
                                <div class="col-md-8">
                                    <label>Nombre:</label>
                                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" placeholder="Ingrese Nombre"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                                        ErrorMessage="Ingrese Nombre" ForeColor="#FF3300" ValidationGroup="ValidationDatos_empleado"
                                        Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 25px;">
                                <div class="col-md-4">
                                    <label>
                                        Tipo Dni:
                                    </label>
                                    <asp:DropDownList ID="ddTipoDNI" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddTipoDNI"
                                        ErrorMessage="Debe Seleccionar el Tipo De Doc" ForeColor="#FF3300" InitialValue="0"
                                        ValidationGroup="ValidationDatos_empleado" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Nro Doc:
                                    </label>
                                    <asp:TextBox ID="txtNro_documento" CssClass="form-control" runat="server" placeholder="Ingrese Nro"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtNro_documento"
                                        ErrorMessage="Debe Ingresar Nro Doc" ForeColor="#FF3300" Operator="DataTypeCheck" Type="Integer"
                                        ValidationGroup="ValidationDatos_empleado" Display="Dynamic">*</asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNro_documento"
                                        ErrorMessage="Debe Ingresar el Nro de Documento" ForeColor="#FF3300" ValidationGroup="ValidationDatos_empleado"
                                        Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>CUIL:</label>
                                    <asp:TextBox ID="txtCuil" CssClass="form-control" runat="server" placeholder="Ingrese Cuil"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCuil"
                                        ErrorMessage="Debe Ingresar el Nro de CUIL" ForeColor="#FF3300"
                                        ValidationGroup="ValidationDatos_empleado" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 25px;">
                                <div class="col-md-4">
                                    <label>
                                        Tarea
                                    </label>
                                    <asp:TextBox ID="txtTarea" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTarea"
                                        ErrorMessage="Debe Ingresar la Tarea"
                                        ForeColor="#FF3300" InitialValue="0" ValidationGroup="ValidationDatos_empleado" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Cargo
                                    </label>
                                    <asp:DropDownList ID="ddCargo" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                        OnSelectedIndexChanged="ddCargo_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddCargo"
                                        ErrorMessage="Debe Seleccionar el Cargo" ForeColor="#FF3300" InitialValue="0" ValidateRequestMode="Enabled"
                                        ValidationGroup="ValidationDatos_empleado" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Nº Cta Sueldo Basico
                                    </label>
                                    <asp:DropDownList ID="ddCargoCuenta" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddCargoCuenta"
                                        ErrorMessage="Debe Seleccionar Cuenta del Cargo" ForeColor="#FF3300" InitialValue="0"
                                        ValidationGroup="ValidationDatos_ObSocial" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 25px;">
                                <div class="col-md-4">
                                    <label>
                                        Seccion
                                    </label>
                                    <asp:DropDownList ID="ddSeccion" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddSeccion"
                                        ErrorMessage="Debe Seleccionar La Seccion"
                                        ForeColor="#FF3300" InitialValue="0" ValidationGroup="ValidationDatos_empleado" Display="Dynamic">*</asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Categoria
                                    </label>
                                    <asp:DropDownList ID="ddCategoria" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddCategoria"
                                        ErrorMessage="Debe Seleccionar la Categoria" ForeColor="#FF3300" InitialValue="0"
                                        ValidationGroup="ValidationDatos_empleado" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Nº Cta Gastos
                                    </label>
                                    <asp:TextBox ID="txtNro_cta_gastos" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Debe ingresar la Cta Gastos"
                                        ControlToValidate="txtNro_cta_gastos" Text="*" Type="String" ForeColor="#FF3300"
                                        ValidationGroup="ValidationDatos_ObSocial" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 25px;">
                                <div class="col-md-4">
                                    <label>
                                        Clasificacion Personal
                                    </label>
                                    <asp:DropDownList ID="ddClasificacion_personal" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddClasificacion_personal"
                                        ErrorMessage="Debe Seleccionar la Clasificacion" ForeColor="#FF3300" ValidationGroup="ValidationDatos_empleado"
                                        Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Tipo Liquidacion
                                    </label>
                                    <asp:DropDownList ID="ddTipo_liquidacion" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddTipo_liquidacion"
                                        ErrorMessage="Debe Seleccionar Tipo Liquidacion!" ForeColor="#FF3300"
                                        ValidationGroup="ValidationDatos_empleado" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Oficina de Trabajo
                                    </label>
                                    <asp:DropDownList ID="ddOficina" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddOficina"
                                        ErrorMessage="Debe Seleccionar Oficina de Trabajo!" ForeColor="#FF3300" InitialValue="0" Text="*"
                                        ValidationGroup="ValidationDatos_empleado" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 25px;">
                                <div class="col-md-4">
                                    <label>
                                        Secretaria
                                    </label>
                                    <asp:DropDownList ID="ddSecretaria" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                        OnSelectedIndexChanged="ddSecretaria_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddSecretaria"
                                        ErrorMessage="Debe Seleccionar Secretaria!" ForeColor="#FF3300" InitialValue="0"
                                        ValidationGroup="ValidationDatos_empleado" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Direccion
                                    </label>
                                    <asp:DropDownList ID="ddDireccion" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                        OnSelectedIndexChanged="ddDireccion_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                        ControlToValidate="ddDireccion" ErrorMessage="Debe Seleccionar la Direccion"
                                        ForeColor="#FF3300" InitialValue="0" ValidationGroup="ValidationDatos_empleado"
                                        Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Programa
                                    </label>
                                    <asp:DropDownList ID="ddPrograma" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddPrograma"
                                        ErrorMessage="Debe asignar el Programa al que pertenece!" ForeColor="#FF3300" InitialValue="0" Text="*"
                                        ValidationGroup="ValidationDatos_empleado" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 25px;">
                                <div class="col-md-6">
                                    <label>
                                        Regimen
                                    </label>
                                    <asp:DropDownList ID="ddRegimen" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddRegimen"
                                        ErrorMessage="Debe Seleccionar Regimen!" ForeColor="#FF3300" InitialValue="0"
                                        ValidationGroup="ValidationDatos_empleado" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        Escala Aumento
                                    </label>
                                    <asp:DropDownList ID="ddEscala" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 25px;">
                                <div class="col-md-4">
                                    <label>
                                        Situacion de Revista
                                    </label>
                                    <asp:DropDownList ID="ddRevista" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddRevista"
                                        ErrorMessage="Debe Seleccionar Situacion de Revista!" ForeColor="#FF3300" InitialValue="0"
                                        ValidationGroup="ValidationDatos_empleado" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Fecha de Revista
                                    </label>
                                    <asp:TextBox ID="txtFecha_revista" CssClass="form-control" runat="server"
                                        placeholder="Fecha de Revista"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtFecha_revista"
                                        ErrorMessage="Ingrese Fecha de Revista" ForeColor="#FF3300" ValidationGroup="ValidationDatos_empleado"
                                        Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>Activo?</label>
                                    <div class="input-group">
                                        <asp:CheckBox ID="ChkActivo" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 25px;">
                                <div class="col-md-4">
                                    <label>
                                        Fecha Baja
                                    </label>
                                    <asp:TextBox ID="txtFecha_baja" CssClass="form-control" runat="server"
                                        placeholder="Fecha de Baja"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group col-md-6">
                                        <label>Imprime Recibo?</label>
                                        <div class="input-group">
                                            <asp:CheckBox ID="chkImprime" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            <div class="row" style="margin-bottom: 25px;">
                                <div class="col-sm-10" style="padding-left: 0px;">
                                    <asp:ValidationSummary ID="ValidationDatos_empleado" runat="server"
                                        ValidationGroup="ValidationDatos_empleado" ForeColor="Red" />
                                </div>
                            </div>
                            <!-- /.box-body -->
                            <div class="row margen">
                                <br />
                                <div class="col-md-12" style="text-align: right;">
                                    <asp:Button ID="cmdCancelar_tab_empleado" CssClass="btn btn-info" runat="server"
                                        Text="Cancelar" OnClick="cmdCancelar_tab_empleado_Click" />
                                    <asp:Button ID="cmdAceptar_tab_empleado" CssClass="btn btn-primary" runat="server"
                                        ValidationGroup="ValidationDatos_empleado"
                                        Text="Aceptar" OnClick="cmdAceptar_tab_empleado_Click" />
                                    <button type="button" id="btnVolver1" runat="server"
                                        onserverclick="btnVolver1_ServerClick" class="btn btn-warning">
                                        <span class="glyphicon glyphicon glyphicon-new-window"></span>&nbsp;Volver
                                    </button>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                    <div class="tab-pane fade" style="min-height: 300px;" id="tab_Datos_Contrato"
                        role="tabpanel" aria-labelledby="tab_Datos_Contrato-tab">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="panel-body">
                                    <div class="row" style="margin-bottom: 25px;">
                                        <div class="col-md-4">
                                            <label>
                                                Nro Afiliado Obra Social
                                            </label>
                                            <asp:TextBox ID="txtNro_obra_social" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Debe ingresar el Nº Obra Social"
                                                ControlToValidate="txtNro_obra_social" ForeColor="#FF3300" Text="*" Type="String"
                                                ValidationGroup="ValidationDatos_ObSocial" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>

                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">
                                        <div class="col-md-4">
                                            <label>
                                                Nro Jubilacion
                                            </label>
                                            <asp:TextBox ID="txtNro_jubilacion" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Debe ingresar Nº jubilacion"
                                                ControlToValidate="txtNro_jubilacion" ForeColor="#FF3300" Text="*" Type="String"
                                                ValidationGroup="ValidationDatos_ObSocial" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                        </div>
                                        <div class="col-md-4">
                                            <label>
                                                Antiguedad Anterior
                                            </label>
                                            <asp:TextBox ID="txtAnt_anterior" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Debe ingresar Ant Anterior"
                                                ControlToValidate="txtAnt_anterior" ForeColor="#FF3300" Text="*" Type="Integer"
                                                ValidationGroup="ValidationDatos_ObSocial" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>
                                                Antiguedad Actual
                                            </label>
                                            <asp:TextBox ID="txtAnt_actual" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Debe ingresar Ant Actual"
                                                ControlToValidate="txtAnt_actual" ForeColor="#FF3300" Text="*" Type="Integer"
                                                ValidationGroup="ValidationDatos_ObSocial" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">
                                        <div class="col-md-4">
                                            <label>
                                                Nro Contrato
                                            </label>
                                            <asp:TextBox ID="txtNro_contrato" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Debe ingresar Nº Contrato"
                                                ControlToValidate="txtNro_contrato" ForeColor="#FF3300" Text="*" Type="Integer"
                                                ValidationGroup="ValidationDatos_ObSocial" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                        </div>
                                        <div class="col-md-4">
                                            <label>
                                                Fecha Inicio Contrato
                                            </label>
                                            <asp:TextBox ID="txtFecha_inicio_contrato" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="Debe ingresar Fec Inicio Contrato"
                                                ControlToValidate="txtFecha_inicio_contrato" ForeColor="#FF3300" Text="*" Type="Date"
                                                ValidationGroup="ValidationDatos_ObSocial" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                        </div>
                                        <div class="col-md-4">
                                            <label>
                                                Fecha Fin Contrato
                                            </label>
                                            <asp:TextBox ID="txtFecha_fin_contrato" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="Debe ingresar Fec Fin Contrato"
                                                ControlToValidate="txtFecha_fin_contrato" ForeColor="#FF3300" Text="*" Type="Date"
                                                ValidationGroup="ValidationDatos_ObSocial" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">
                                        <div class="col-md-6">
                                            <label>
                                                Nro Nombramiento
                                            </label>
                                            <asp:TextBox ID="txtNro_nombramiento" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="Debe ingresar Nº Nombramiento"
                                                ControlToValidate="txtNro_nombramiento" ForeColor="#FF3300" Text="*" Type="Integer"
                                                ValidationGroup="ValidationDatos_ObSocial" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                        </div>
                                        <div class="col-md-6">
                                            <label>
                                                Fecha Nombramiento
                                            </label>
                                            <asp:TextBox ID="txtFecha_nombramiento" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="Debe ingresar Fec Nombramiento"
                                                ControlToValidate="txtFecha_nombramiento" ForeColor="#FF3300" Text="*" Type="Date"
                                                ValidationGroup="ValidationDatos_ObSocial" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">
                                        <asp:ValidationSummary ID="ValidationDatos_ObSocial" runat="server"
                                            ValidationGroup="ValidationDatos_ObSocial" ForeColor="Red" />
                                    </div>

                                    <div class="row" style="margin-bottom: 25px;">
                                        <br />
                                        <div class="col-md-12" style="text-align: right;">
                                            <asp:Button ID="cmdCancelar_tab_obsocial" CssClass="btn btn-info" runat="server"
                                                Text="Cancelar" OnClick="cmdCancelar_tab_obsocial_Click" />
                                            <asp:Button ID="cmdAceptar_tab_obsocial" CssClass="btn btn-primary" runat="server"
                                                ValidationGroup="ValidationDatos_ObSocial"
                                                Text="Aceptar" OnClick="cmdAceptar_tab_contrato_Click" />
                                            <button type="button" id="cmdVolver2" runat="server"
                                                onserverclick="cmdVolver2_ServerClick" class="btn btn-warning">
                                                <span class="glyphicon glyphicon glyphicon-new-window"></span>&nbsp;Volver
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane fade" id="tab_Datos_Banco"
                        role="tabpanel" aria-labelledby="tab_Datos_Banco-tab">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="container">
                                    <div class="row" style="margin-bottom: 25px;">

                                        <div class="col-md-6">
                                            <label>
                                                Banco
                                            </label>
                                            <asp:DropDownList ID="ddBanco" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-6">
                                            <label>
                                                Tipo Cuenta
                                            </label>
                                            <asp:DropDownList ID="ddTipo_cuenta" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">

                                        <div class="col-md-4">
                                            <label>
                                                Nro Sucursal
                                            </label>
                                            <asp:TextBox ID="txtNro_sucursal" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="Debe ingresar Nº Sucursal"
                                                ControlToValidate="txtNro_sucursal" ForeColor="#FF3300" Text="*" Type="Integer"
                                                ValidationGroup="ValidationDatos_Bco" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>

                                        </div>
                                        <div class="col-md-4">
                                            <label>
                                                Nro Caja Ahorro
                                            </label>
                                            <asp:TextBox ID="txtNro_caja_ahorro" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="Debe ingresar Nº Caja Ahorro"
                                                ControlToValidate="txtNro_caja_ahorro" ForeColor="#FF3300" Text="*" Type="String"
                                                ValidationGroup="ValidationDatos_Bco" Operator="DataTypeCheck"></asp:CompareValidator>
                                        </div>
                                        <div class="col-md-4">
                                            <label>
                                                Nro CBU
                                            </label>
                                            <asp:TextBox ID="txtCbu" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator15" runat="server" ErrorMessage="Debe ingresar Nº CBU"
                                                ControlToValidate="txtCbu" ForeColor="#FF3300" Text="*" Type="String"
                                                ValidationGroup="ValidationDatos_Bco" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">
                                        <asp:ValidationSummary ID="ValidationDatos_Bco" runat="server"
                                            ValidationGroup="ValidationDatos_Bco" ForeColor="Red" />
                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">
                                        <br />
                                        <div class="col-md-12" style="text-align: right;">
                                            <asp:Button ID="cmdCancelar_tab_contrato" CssClass="btn btn-info" runat="server"
                                                Text="Cancelar" OnClick="cmdCancelar_tab_contrato_Click" />
                                            <asp:Button ID="cmdAceptar_tab_contrato" CssClass="btn btn-primary" runat="server"
                                                Text="Aceptar" ValidationGroup="ValidationDatos_Bco" OnClick="cmdAceptar_tab_Datos_Banco_Click" />

                                            <button type="button" id="cmdVolver3" runat="server"
                                                onserverclick="cmdVolver3_ServerClick" class="btn btn-warning">
                                                <span class="glyphicon glyphicon glyphicon-new-window"></span>&nbsp;Volver
                                            </button>



                                        </div>
                                    </div>
                                </div>
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <div class="tab-pane fade" id="tab_Datos_Particulares"
                        role="tabpanel" aria-labelledby="tab_Datos_Particulares-tab">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="panel-body" style="padding-left: 15px; padding-right: 15px;">
                                    <!-- /.box-header -->
                                    <div class="row" style="margin-bottom: 25px;">
                                        <div class="col-md-4">
                                            <label>
                                                Fecha Nacimiento:
                                            </label>
                                            <asp:TextBox ID="txtFecha_nacimiento" CssClass="form-control" runat="server" placeholder="Ingrese Fecha Nacimiento"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Debe Ingresar Fec Nacimiento"
                                                ControlToValidate="txtFecha_nacimiento" Text="*" ForeColor="#FF3300" Display="Dynamic"
                                                ValidationGroup="ValidationDatosParticulares">
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator16" runat="server" ErrorMessage="Debe ingresar Fec Nacimiento"
                                                ControlToValidate="txtFecha_nacimiento" ForeColor="#FF3300" Text="*" Type="Date"
                                                ValidationGroup="ValidationDatosParticulares" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                                        </div>

                                        <div class="col-md-4">
                                            <label>
                                                Sexo:
                                            </label>
                                            <asp:DropDownList ID="ddSexo" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddSexo"
                                                ErrorMessage="Debe Seleccionar Sexo" ForeColor="#FF3300" InitialValue="0"
                                                ValidationGroup="ValidationDatosParticulares" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-md-4">
                                            <label>
                                                Estado Civil
                                            </label>
                                            <asp:DropDownList ID="ddEstadoCivil" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddEstadoCivil"
                                                ErrorMessage="Debe Seleccionar Estado Civil" ForeColor="#FF3300" InitialValue="0"
                                                ValidationGroup="ValidationDatosParticulares" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">
                                        <div class="col-md-4">
                                            <label>
                                                Pais:
                                            </label>
                                            <asp:TextBox ID="txtPais" CssClass="form-control" runat="server" placeholder="Ingrese Pais Domicilio"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label>
                                                Provincia:
                                            </label>
                                            <asp:TextBox ID="txtProvincia" CssClass="form-control" runat="server" placeholder="Ingrese Provincia"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label>
                                                Ciudad Domicilio:
                                            </label>
                                            <asp:TextBox ID="txtCiudad" CssClass="form-control" runat="server" placeholder="Ingrese Ciudad"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">
                                        <div class="col-md-4">
                                            <label>
                                                Barrio:
                                            </label>
                                            <asp:TextBox ID="txtBarrio" CssClass="form-control" runat="server" placeholder="Ingrese Barrio"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label>
                                                Calle:
                                            </label>
                                            <asp:TextBox ID="txtCalle" CssClass="form-control" runat="server" placeholder="Ingrese Calle"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>
                                                Nro:
                                            </label>
                                            <asp:TextBox ID="txtNro_domicilio" CssClass="form-control" runat="server" placeholder="Ingrese Nro"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>
                                                C.Postal:
                                            </label>
                                            <asp:TextBox ID="txtCPostal" CssClass="form-control" runat="server" placeholder="Ingrese C.Postal"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">
                                        <div class="col-md-2">
                                            <label>
                                                Piso:
                                            </label>
                                            <asp:TextBox ID="txtPiso" CssClass="form-control" runat="server" placeholder="Ingrese Piso"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>
                                                Dpto:
                                            </label>
                                            <asp:TextBox ID="txtDpto" CssClass="form-control" runat="server" placeholder="Ingrese Dpto"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>
                                                MonoBlock:
                                            </label>
                                            <asp:TextBox ID="txtMonoBlock" CssClass="form-control" runat="server" placeholder="Ingrese MonoBlock"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="row" style="margin-bottom: 25px;">
                                        <div class="col-md-4">
                                            <label>
                                                Telefono
                                            </label>
                                            <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label>
                                                Celular
                                            </label>
                                            <asp:TextBox ID="txtCelular" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label>
                                                Email
                                            </label>
                                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                ErrorMessage="Ingrese Correctamente el Email" ForeColor="#FF3300"
                                                ValidationGroup="ValidationDatosParticulares" Display="Dynamic"
                                                ControlToValidate="txtEmail" Text="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                        </div>
                                    </div>

                                </div>
                                <div class="row" style="margin-bottom: 25px;">
                                    <div>
                                        <asp:ValidationSummary ID="ValidationDatosParticulares" runat="server"
                                            ValidationGroup="ValidationDatosParticulares"
                                            ForeColor="Red" />
                                    </div>
                                </div>
                                <div class="row" style="margin-bottom: 25px;">
                                    <br />
                                    <div class="col-md-12" style="text-align: right;">
                                        <asp:Button ID="cmdCancelar_tab_particulares" CssClass="btn btn-info" runat="server"
                                            Text="Cancelar" OnClick="cmdCancelar_tab_particulares_Click" />
                                        <asp:Button ID="cmdAceptar_tab_Datos_Particulares" CssClass="btn btn-primary" runat="server"
                                            ValidationGroup="ValidationDatosParticulares" OnClick="cmdAceptar_tab_Datos_Particulares_Click"
                                            Text="Aceptar" />
                                        <button type="button" id="cmdVolver4" runat="server"
                                            onserverclick="cmdVolver4_ServerClick" class="btn btn-warning">

                                            <span class="glyphicon glyphicon glyphicon-new-window"></span>&nbsp;Volver
                                        </button>
                                    </div>
                                </div>
                                <br />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>


                </div>
            </div>
            <%--</div>--%>
        </div>
    </div>

</asp:Content>
