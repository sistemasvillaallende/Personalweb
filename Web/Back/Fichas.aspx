<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MP_Desempenio.Master"
    AutoEventWireup="True" ValidateRequest="false"
    CodeBehind="Fichas.aspx.cs" Inherits="web.secure.Fichas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .dropdown-menu.show {
            display: grid;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hIdFicha" runat="server" />
    <asp:HiddenField ID="hIdIcono" runat="server" />
    <div class="panel-body">
        <div class="container-fluid shadow p-3 mb-5 bg-white rounded"
            style="background-color: white; padding-left: 25px !important; padding-top: 5px !important;">
            <div class="box-header with-border">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-md-9">
                        <h3 class="box-title" style="font-size: 24px; color: var(--secondary);">EVALUACIONES DE DESEMPEÑO</h3>
                    </div>
                    <div class="col-md-3" style="text-align: right;">
                        <a id="btnEdit"
                            data-target="#modalUpdate"
                            data-toggle="modal"
                            data-id=''
                            class="btn btn-default-uotline-small"
                            data-nombre=''
                            data-icono=''
                            href="#"
                            title="Editar Pregunta"><span class="fa fa-plus"></span>&nbsp; Agregar Evaluación</a>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <hr style="border-top: 1px solid gray;" />
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-xs-12">
                        <div id="divList" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvPreguntas"
                                        CssClass="table"
                                        runat="server"
                                        CellPadding="4"
                                        AutoGenerateColumns="false"
                                        OnRowCommand="gvPreguntas_RowCommand"
                                        OnRowDataBound="gvPreguntas_RowDataBound"
                                        ForeColor="#333333"
                                        GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <%# Eval("ID") %>
                                                    <input type="hidden" name="LocationId" value='<%# Eval("ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Icono">
                                                <ItemTemplate>
                                                    <span class='<%# Eval("ICONO") %>'></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="Acciones">
                                                <ItemTemplate>
                                                    <div class="dropdown">
                                                        <div class="btn-group dropleft">
                                                            <button type="button" class="btn btn-secondary" data-toggle="dropdown" aria-expanded="false">
                                                                ...
                                                            </button>
                                                            <div class="dropdown-menu"
                                                                style="width: 250px; position: absolute; top: 0px; left: 0px; transform: translate3d(-252px, 0px, 0px); padding: 15px;">
                                                                <a id="btnEdit"
                                                                    style="padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                                    data-target="#modalUpdate"
                                                                    data-toggle="modal"
                                                                    data-id='<%# Eval("ID") %>'
                                                                    data-nombre='<%# Eval("NOMBRE") %>'
                                                                    data-icono='<%# Eval("ICONO") %>'
                                                                    href="#"
                                                                    title="Editar Pregunta">
                                                                    <span class="fa fa-edit"></span>
                                                                    &nbsp;Editar
                                                                </a>
                                                                <asp:LinkButton ID="btnActivar"
                                                                    Style="padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                                    ToolTip="Activar"
                                                                    CommandName="activar"
                                                                    CommandArgument='<%# Eval("ID") %>'
                                                                    runat="server">
                                                                    <span class="fa fa-eye-slash"></span>
                                                                    &nbsp;Activar
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="btnDesactivar"
                                                                    Style="padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                                    ToolTip="Desactivar"
                                                                    CommandName="desactivar"
                                                                    CommandArgument='<%# Eval("ID") %>'
                                                                    runat="server">
                                                                    <span class="fa fa-eye"></span>
                                                                    &nbsp;Desactivar
                                                                </asp:LinkButton>
                                                                <a id="btnConfig"
                                                                    style="padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                                    href="Fichas_preguntas.aspx?id=<%# Eval("ID") %>"
                                                                    title="Configurar Pregunta">
                                                                    <span class="fa fa-cog"></span>
                                                                    &nbsp;Configurar secciones
                                                                </a>
                                                                <a id="btnNotificacionConfig"
                                                                    style="padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                                    data-target="#modalNotificacion"
                                                                    data-toggle="modal"
                                                                    data-id='<%# Eval("ID") %>'
                                                                    data-notificacion='<%# Eval("NOTIFICACION") %>'
                                                                    href="#"
                                                                    title="Configurar Notificación">
                                                                    <span class="fa fa-cogs"></span>
                                                                    &nbsp;Configurar Notificación
                                                                </a>
                                                                <asp:LinkButton ID="btnEliminar"
                                                                    Style="padding: 10px; padding-left: 0; color: gray; text-decoration: none !important;"
                                                                    ToolTip="Eliminar"
                                                                    CommandName="eliminar"
                                                                    CommandArgument='<%# Eval("ID") %>'
                                                                    runat="server">
                                                                    <span class="fa fa-remove"></span>
                                                                    &nbsp;Eliminar
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button Text="Actualizar Orden " Visible="false"
                                        ID="ActualizarReferencia" runat="server" OnClick="ActualizarReferencia_Click" />
                                    <div class="modal fade" id="modalNotificacion" role="dialog"
                                        aria-labelledby="myModalLabel2" aria-hidden="true">
                                        <div class="modal-dialog" style="min-width: 1000px;">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title" id="exampleModalLabel">Texto Notificacion</h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    <label>Texto Notificación</label>
                                                    <asp:TextBox TextMode="MultiLine"
                                                        ID="txtTextoNtificacion" CssClass="form-control"
                                                        Mode="Encode" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                                                    <button id="btnAceptarNotificacion" runat="server"
                                                        onserverclick="btnAceptarNotificacion_ServerClick"
                                                        type="button" class="btn btn-default-uotline-small">
                                                        Aceptar</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAdd" runat="server" visible="false">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="modal fade" id="modalUpdate" role="dialog"
                aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="myModalLabel">Cambiar nombre
                            </h5>
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label>Nombre</label>
                                <asp:TextBox ID="txtActualizaPregunta" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <label>Icono</label>
                            <div class="input-group">
                                <span style="margin-right: 10px; padding-top: 10px;"
                                    class="input-group-addon"><i id="iIcono"></i></span>
                                <asp:TextBox ID="txtIcono" Enabled="false"
                                    Style="background-color: transparent; border: none;"
                                    CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <hr style="border-top: 1px solid gray; margin-top: 0;" />
                            <div id="divIconos" runat="server" class="row"
                                style="height: 300px; overflow-y: scroll;">
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">
                                Cancelar
                            </button>
                            <button type="button" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick"
                                data-dismiss="modal" class="btn btn-default-uotline-small">
                                Aceptar
                            </button>
                        </div>
                    </div>

                </div>

            </div>

        </div>
    </div>
    <script src="../App_Themes/jQuery-2.1.4.min.js"></script>
    <script src="../App_Themes/bower_components/bootstrap/dist/js/bootstrap.js"></script>
    <script type="text/javascript">   
        function cambiarIcono(icono) {
            $("#iIcono").removeClass();
            $("#iIcono").addClass(icono);
            $('#ContentPlaceHolder1_txtIcono').val(icono);
            $("#ContentPlaceHolder1_hIdIcono").val(icono);
        }
        function cambiarNotificacion(id, texto) {
            $("#ContentPlaceHolder1_hIdFicha").val(id);
            $("#ContentPlaceHolder1_txtTextoNtificacion").val(texto);
        }
    </script>

    <script src="../App_Themes/order/jquery.min.js"></script>
    <link href="../App_Themes/order/jquery-ui.css" rel="stylesheet" />
    <script src="../App_Themes/order/jquery-ui.min.js"></script>
    <script>
        // Give $ to prototype.js
        var $jq = jQuery.noConflict();
    </script>
    <script type="text/javascript">
        $(document).ready(function (e) {

            $('#modalUpdate').on('show.bs.modal', function (e) {
                var id = $(e.relatedTarget).data().id;
                $("#ContentPlaceHolder1_hIdFicha").val(id);

                var nombre = $(e.relatedTarget).data().nombre;
                $(e.currentTarget).find('#ContentPlaceHolder1_txtActualizaPregunta').val(nombre);
                var icono = $(e.relatedTarget).data().icono;
                $("#ContentPlaceHolder1_hIdIcono").val(icono);
                $(e.currentTarget).find('#ContentPlaceHolder1_txtIcono').val(icono);
                $(e.currentTarget).find('#iIcono').removeClass();
                $(e.currentTarget).find('#iIcono').addClass(icono);

            });

            $('#modalNotificacion').on('show.bs.modal', function () {
                alert();
                var id = $(e.relatedTarget).data().id;

                $("#ContentPlaceHolder1_hIdFicha").val(id);

                var txtTextoNtificacion = $(e.relatedTarget).data().notificacion;
                $(e.currentTarget).find('#ContentPlaceHolder1_txtTextoNtificacion').val(txtTextoNtificacion);
            });
        });

    </script>
    <script src="../App_Themes/bower_components/jquery/dist/jquery.slim.js"></script>
    <script src="../App_Themes/bower_components/ckeditor/ckeditor.js"></script>
    <script>
        CKEDITOR.replace('ContentPlaceHolder1_txtTextoNtificacion');
    </script>
</asp:Content>
