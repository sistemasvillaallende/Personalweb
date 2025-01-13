<%@ Page Title="" Language="C#" MasterPageFile="~/Autoridades/Direcciones/MPDirecciones.Master" AutoEventWireup="true" CodeBehind="Evaluacion.aspx.cs" Inherits="web.Autoridades.Direcciones.Evaluacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #ContentPlaceHolder1_ddlAnteriores li a {
            color: var(--gray);
            text-decoration: none;
            padding: 10px;
        }

        label {
            padding-left: 5px;
            font-weight: 500;
        }

        td {
            padding-left: 0 !important;
            padding-right: 0 !important;
            padding-top: 0 !important;
            border-top-left-radius: 15px;
            border-top-right-radius: 15px;
            border: none;
        }

        input[type="radio"] {
            -ms-transform: scale(2); /* IE 9 */
            -webkit-transform: scale(2); /* Chrome, Safari, Opera */
            transform: scale(2);
            margin-right: 10px;
            margin-left: 6px;
        }

        input[type="checkbox"] {
            -ms-transform: scale(2); /* IE 9 */
            -webkit-transform: scale(2); /* Chrome, Safari, Opera */
            transform: scale(2);
            margin-right: 10px;
            margin-left: 6px;
        }

        .table tr td {
            border-color: transparent !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hCantPreguntas" runat="server" />
    <asp:HiddenField ID="hIdRelevamiento" runat="server" />
    <asp:HiddenField ID="hIdFicha" runat="server" />
    <asp:HiddenField ID="hIdEstado" runat="server" />
    <div class="container-fluid shadow p-3 mb-5 bg-white rounded"
        style="background-color: white; padding-left: 25px !important; padding-top: 5px !important;">
        <div class="box-header with-border" style="padding-top: 25px;">
            <div style="border-bottom: 3px solid lightgray; margin-bottom: 15px; padding-bottom: 15px; color: #666;">
                <div class="row">
                    <div class="col-md-4">
                        <div style="margin-bottom: 15px;">
                            <img
                                style="width: 60px; height: 60px; float: left; border: 2px solid #d2d6de; padding: 2px; border-radius: 50%; vertical-align: middle;"
                                src="../../dist/img/user1-128x128.jpg" alt="user image" runat="server" id="imgUser" />
                            <span style="font-size: 16px; font-weight: 600; display: block; margin-left: 70px;">
                                <a runat="server" id="lblNombre" href="#" style="color: var(--primary-color); text-decoration: none;"></a>
                            </span>
                            <span style="font-size: 14px; color: var(--bs-dark-border-subtle); font-weight: 500; width: 100%; display: block; margin-left: 70px;">Legajo: 
                                <span runat="server" id="lblLegajo" style="font-size: 14px; color: var(--secondary); font-weight: 500;"></span>
                            </span>
                            <span style="font-size: 14px; color: var(--bs-dark-border-subtle); font-weight: 500; width: 100%; display: block; margin-left: 70px;">Categoria:
                                <span runat="server" id="lblCategoria" style="font-size: 14px; color: var(--secondary); font-weight: 500;"></span>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <span style="font-size: 14px; font-weight: 600; display: block; color: var(--bs-dark-border-subtle)">Tipo contratación: 
                            <a runat="server" id="lblTipoLiq" href="#"
                                style="color: var(--bs-gray); text-decoration: none;"></a>
                        </span>
                        <span style="font-size: 14px; color: var(--bs-dark-border-subtle); font-weight: 500; width: 100%; display: block;">Fecha ingreso:
                            <span runat="server" id="lblFechaIngreso" style="margin-left: 5px; font-size: 14px; color: var(--secondary); font-weight: 500;"></span>
                        </span>
                        <span style="font-size: 14px; color: var(--bs-dark-border-subtle); font-weight: 500;">Oficina:
                            <span runat="server" id="lblOficinas" style="color: var(--bs-gray); text-decoration: none;"></span>
                        </span>
                    </div>
                    <div class="col-md-4">
                        <span style="font-size: 14px; color: var(--bs-dark-border-subtle); font-weight: 500; width: 100%; display: block;">Fecha:
                            <span runat="server" id="lblFecha" style="margin-left: 5px; font-size: 14px; color: var(--secondary); font-weight: 500;"></span>
                        </span>
                        <span style="font-size: 14px; color: var(--bs-dark-border-subtle); font-weight: 500;">Evaluador:
                            <span runat="server" id="lblNombreEvaluador" style="color: var(--bs-gray); text-decoration: none;"></span>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="margin-bottom: 20px; border: solid 3px lightgray; padding: 15px; border-radius: 15px; font-size: 18px; font-weight: 500; margin-left: 1px; margin-right: 5px;">
            <div class="col-md-4">
                <h5 id="lblNombreEvaluacion" runat="server" style="margin-bottom: 5px; font-size: 18px; color: var(--primary-color);">Nombre Evaluacion</h5>
                <div id="divResultadoTotal" visible="false" runat="server" style="margin-bottom: 5px; color: var(--secondary); font-size: 16px;">
                </div>
                <h3 class="box-title" id="lblTitulo"
                    style="font-size: 16px; color: var(--bs-secondary);"
                    runat="server"></h3>
            </div>
            <div class="col-md-4">
                <h3 class="box-title" id="lblEvaluadorOriginal"
                    style="font-size: 16px; color: var(--bs-secondary);"
                    runat="server"></h3>
                <h3 class="box-title" id="lblEvaluado"
                    style="font-size: 16px; color: var(--bs-secondary);"
                    runat="server">Evaluado: 
                    <span style="color: var(--primary-color)">Martin Velez</span></h3>
                <h3 class="box-title" id="lblConformidad"
                    style="font-size: 16px; color: var(--bs-secondary);"
                    runat="server">Estado: Aguardando conformidad</h3>
            </div>
            <div class="col-md-4">
                <div class="btn-group pull-right">
                    <button type="button" class="btn btn-secondary" id="btnNotificar"
                        runat="server"
                        onserverclick="btnNotificar_ServerClick">
                        Notificar</button>
                    <button type="button" class="btn btn-info" id="btnNuevo" runat="server"
                        onserverclick="btnNuevo_ServerClick">
                        Nuevo</button>
                    <a href="Desempeño.aspx" class="btn btn-danger" id="btnSalirEv" runat="server">
                        Cancelar</a>
                    <a  id="modal-60622" href="#modal-container-60622" role="button"
                        class="btn btn-danger" data-toggle="modal">Eliminar
                    </a>
                    <div class="btn-group">
                        <button type="button" class="btn btn-success dropdown-toggle"
                            data-toggle="dropdown" aria-expanded="true">
                            Anteriores <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu" id="ddlAnteriores" runat="server">
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <div class="row"
            id="divPreguntas" runat="server">
        </div>
        <div class="row" style="border: solid 3px lightgray; margin-left: 1px; margin-right: 5px; border-radius: 15px; padding-top: 10px;">
            <div class="box box-primary" id="divRespuestas" runat="server" visible="false">
                <div class="box-header ui-sortable-handle" style="cursor: move;">
                    <div class="row" style="padding-top: 15px; padding-left: 30px; padding-right: 20px;">
                        <div class="col-md-12">
                            <h5
                                style="font-size: 24px; color: var(--bs-secondary);">Resultado</h5>
                            <hr style="border: 0; border-top: 3px solid rgb(211 211 211); opacity: 1;" />

                        </div>
                    </div>
                    <div class="box-tools">
                        <asp:DropDownList Visible="false"
                            ID="DDLRelevamientos" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="slimScrollDiv" style="position: relative; padding: 20px; padding-top: 0">
                    <div id="chat-box">
                        <asp:GridView
                            ID="gvRespuestas"
                            runat="server"
                            CellPadding="4"
                            ForeColor="#333333"
                            ShowHeader="false"
                            ShowFooter="true"
                            AutoGenerateColumns="false"
                            OnRowDataBound="gvRespuestas_RowDataBound"
                            CssClass="table"
                            GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div id="divResultado" visible="false" runat="server"
                                            style="border: solid 1px darkgray; padding: 15px; border-radius: 15px; font-size: 18px; font-weight: 500;">
                                        </div>
                                        <div id="divGrupo" visible="false" runat="server" style="padding-left: 10px; padding-top: 10px;">
                                        </div>
                                        <div class="item" style="padding-left: 10px;">
                                            <span class="name" id="lblPregunta" runat="server" style="color: var(--bs-success);"><%# Eval("TEXTO_PREGUNTA") %></span>
                                            <span style="padding-left: 25px;" id="lblRespuesta" runat="server"><%# Eval("TEXTO_RESPUESTA") %></span>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <hr style="margin-top: 20px; margin-bottom: 40px; border: 0; border-top: 3px solid rgb(211 211 211); opacity: 1;" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <!-- /.chat -->
                <div class="box-footer">
                    <div class="input-group">
                    </div>
                </div>
            </div>
            <asp:GridView
                ID="gvPreguntas"
                CssClass="table"
                ShowHeader="false"
                AutoGenerateColumns="false"
                DataKeyNames="ID"
                runat="server"
                OnRowDataBound="gvPreguntas_RowDataBound"
                CellPadding="4"
                ForeColor="#333333"
                GridLines="none">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div id="divGrupo" runat="server" style="padding-left: 10px; padding-top: 20px;">
                            </div>
                            <div class="row" id="divPorcentual" runat="server" style="align-items: center;">
                                <div class="col-md-4" style="padding-left: 25px;">
                                    <p style="color: var(--secondary); padding: 10px; display: contents;">
                                        <asp:Label ID="lblPregunta" runat="server" Text='<%# Eval("PREGUNTA") %>'></asp:Label>
                                    </p>
                                </div>
                                <div class="col-md-8">
                                    <p style="margin-left: 25px; margin-right: 25px; margin-bottom: 0;"
                                        id="pRadio" runat="server">
                                        <asp:RadioButtonList ID="rbtn" runat="server"></asp:RadioButtonList>
                                    </p>
                                    <p id="pCheck" runat="server" style="margin-left: 25px; margin-right: 25px; margin-bottom: 0;">
                                        <asp:CheckBoxList ID="chkList" runat="server"></asp:CheckBoxList>
                                    </p>
                                </div>
                            </div>
                            <div class="row" id="divNormal" runat="server">
                                <div class="col-md-12" style="align-items: center; padding-left: 25px;">
                                    <p style="color: var(--secondary); padding: 10px; display: contents;">
                                        <asp:Label ID="lblPregunta2" runat="server" Text='<%# Eval("PREGUNTA") %>'></asp:Label>
                                    </p>
                                    <p style="margin-right: 25px; margin-bottom: 0;">
                                        <asp:TextBox TextMode="MultiLine" CssClass="form-control" ID="txtRespuesta" runat="server"></asp:TextBox>
                                    </p>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="row" style="margin-top: 30px;" runat="server" id="divError" visible="false">
            <div class="alert alert-danger alert-dismissible">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <p><strong>Error!</strong></p>
                <p><span id="lblError" runat="server"></span></p>
            </div>
        </div>
        <div class="row" style="margin-top: 30px;">
            <div class="col-md-col-12" style="text-align: right">
                <asp:LinkButton ID="btnCancelar" CssClass="btn btn-default" runat="server">Cancelar</asp:LinkButton>
                <asp:LinkButton ID="btnAceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" runat="server">Aceptar</asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">

            <div class="modal fade" id="modal-container-60622" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="myModalLabel">Confirme la eliminacion del relevamiento
                            </h5>
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            Esta seguro de eliminar el relevamiento
                        </div>
                        <div class="modal-footer">

                            <button type="button" class="btn btn-danger" id="btnEliminar" runat="server"
                                onserverclick="btnEliminar_ServerClick" data-dismiss="modal">
                                <span class="fa fa-remove"></span>&nbsp;Eliminar</button>
                            <button type="button" class="btn btn-secondary">
                                Cancelar
                            </button>
                        </div>
                    </div>

                </div>

            </div>

        </div>
    </div>
</asp:Content>
