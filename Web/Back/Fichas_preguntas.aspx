<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MP_Desempenio.Master"
    AutoEventWireup="True" ValidateRequest="false"
    CodeBehind="Fichas_preguntas.aspx.cs" Inherits="web.secure.Fichas_preguntas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hIdFicha" runat="server" />
    <asp:HiddenField ID="hIdPregunta" runat="server" />
    <asp:HiddenField ID="hEdita" runat="server" />
    <asp:HiddenField ID="hIdRespuesta" runat="server" />
    <asp:HiddenField ID="hIdSeccion" runat="server" />
    <div class="alert alert-danger alert-dismissible" id="divError" runat="server" visible="false">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
        <h4><i class="icon fa fa-ban"></i>Error!</h4>
        <p id="lblError" runat="server"></p>
    </div>

    <div class="panel-body" id="divList" runat="server">
        <div class="container-fluid shadow p-3 mb-5 bg-white rounded"
            style="background-color: white; padding-left: 25px !important; padding-top: 5px !important;">
            <div class="box-header with-border">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-md-8" style="padding-top: 20px;">
                        <div class="input-group">
                            <span class="input-group-addon" style="padding-top: 15px;">
                                <i id="iIcono2" style="font-size: 24px; color: var(--secondary);" runat="server"></i>
                            </span>
                            <asp:TextBox ID="txtIcono"
                                Style="font-size: 24px; color: var(--secondary); background-color: transparent; border: none;"
                                Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4" style="margin-top: 25px; text-align: right;">
                        <!--<asp:LinkButton ID="btnSecciones" runat="server"
                            OnClick="btnSecciones_Click"
                            CssClass="btn btn-default-uotline-small">
                            Secciones
                        </asp:LinkButton>-->
                        <asp:LinkButton ID="btnAddPregunta" runat="server" OnClick="btnAddPregunta_Click"
                            CssClass="btn btn-default-uotline-small">
                            Agregar Pregunta
                        </asp:LinkButton>
                        <asp:Button ID="btnSalirSecciones" CssClass="btn btn-secondary"
                            runat="server" Text="Salir" OnClick="btnSalirSecciones_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <hr style="margin-top: 5px; margin-bottom: 1rem; border: 0; border-top: 2px solid gray;" />
                        <h3 style="font-size: 20px; color: var(--secondary); background-color: transparent; border: none; margin-top: 30px;">Preguntas</h3>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvPreguntas"
                            CssClass="table"
                            runat="server"
                            EmptyDataText="Aun no se han agregado preguntas a la evaluación"
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
                                <asp:TemplateField HeaderText="Pregunta">
                                    <ItemTemplate>
                                        <div><%# Eval("PREGUNTA") %></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipoPregunta" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="dropdown">
                                            <div class="btn-group dropleft">
                                                <button type="button" class="btn btn-secondary" data-toggle="dropdown" aria-expanded="false">
                                                    ...
                                                </button>
                                                <div class="dropdown-menu"
                                                    style="width: 250px; position: absolute; top: 0px; left: 0px; transform: translate3d(-252px, 0px, 0px); padding: 15px;">

                                                    <asp:LinkButton ID="btnActivar"
                                                        Style="display: block; padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                        ToolTip="Activar"
                                                        CommandName="activar"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        runat="server">
                                <span class="fa fa-eye-slash"></span>&nbsp; Activar
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnDesactivar"
                                                        Style="display: block; padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                        ToolTip="Desactivar"
                                                        CommandName="desactivar"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        runat="server">
                                <span class="fa fa-eye"></span>&nbsp; Desactivar
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnConfigurar"
                                                        Style="display: block; padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                        ToolTip="Configurar"
                                                        CommandName="configurar"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        runat="server">
                                <span class="fa fa-cog"></span>&nbsp; Configurar
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminar"
                                                        Style="display: block; padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                        ToolTip="Eliminar"
                                                        CommandName="eliminar"
                                                        OnClientClick="return confirm('¿Esta seguro de la pregunta?. Esta opcion eliminara todas las opciones de respuesta asociadas');"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        runat="server">
                                <span class="fa fa-remove"></span>&nbsp; Eliminar
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Button Visible="false" Text="Actualizar Orden" ID="ActualizarReferencia" runat="server" OnClick="ActualizarReferencia_Click" />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="panel-body" id="divSecciones" runat="server" visible="false">
        <div class="container-fluid shadow p-3 mb-5 bg-white rounded"
            style="background-color: white; padding-left: 25px !important; padding-top: 5px !important;">
            <div class="box-header with-border">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-md-8" style="padding-top: 20px;">
                        <div class="input-group">
                            <span class="input-group-addon" style="padding-top: 15px;">
                                <i id="iIcono3" style="font-size: 24px; color: var(--secondary);" runat="server"></i>
                            </span>
                            <asp:TextBox ID="txtIcono3"
                                Style="font-size: 24px; color: var(--secondary); background-color: transparent; border: none;"
                                Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4" style="margin-top: 25px; text-align: right;">
                        <a id="btnAddSeccion"
                            data-target="#modalSeccion"
                            data-toggle="modal"
                            data-id=''
                            class="btn btn-default-uotline-small"
                            data-nombre=''
                            data-icono=''
                            href="#"
                            title="Agregar Sección"><span class="fa fa-plus"></span>&nbsp; Agregar Sección</a>

                        <a href="Fichas.aspx" class="btn btn-secondary">Salir</a>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <hr style="margin-top: 5px; margin-bottom: 1rem; border: 0; border-top: 2px solid gray;" />
                        <h3 style="font-size: 20px; color: var(--secondary); background-color: transparent; border: none; margin-top: 30px;">Secciones</h3>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvSecciones"
                            CssClass="table"
                            runat="server"
                            EmptyDataText="Aun no se han agregado secciones a la evaluación"
                            CellPadding="4"
                            AutoGenerateColumns="false"
                            OnRowCommand="gvSecciones_RowCommand"
                            OnRowDataBound="gvSecciones_RowDataBound"
                            ForeColor="#333333"
                            GridLines="None">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <%# Eval("ID") %>
                                        <input type="hidden" name="LocationId" value='<%# Eval("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NOMBRE_GRUPO" HeaderText="Sección" />
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
                                                        style="display: block; padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                        data-target="#modalSeccion"
                                                        data-toggle="modal"
                                                        data-id='<%# Eval("ID") %>'
                                                        data-nombre='<%# Eval("NOMBRE_GRUPO") %>'
                                                        href="#"
                                                        title="Editar Sección">
                                                        <span class="fa fa-edit"></span>
                                                        &nbsp;Editar
                                                    </a>
                                                    <asp:LinkButton ID="btnConfigurarPregunta"
                                                        Style="display: block; padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                        ToolTip="Configurar preguntas"
                                                        CommandName="preguntas"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        runat="server">
                                                                    <span class="fa fa-cogs"></span>
                                                                    &nbsp;Configurar preguntas
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnActivar"
                                                        Style="display: block; padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                        ToolTip="Activar"
                                                        CommandName="activar"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        runat="server">
                                                                    <span class="fa fa-eye-slash"></span>
                                                                    &nbsp;Activar
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnDesactivar"
                                                        Style="display: block; padding: 10px; padding-left: 0; color: gray; text-decoration: none !important; border-bottom: solid 2px lightgray;"
                                                        ToolTip="Desactivar"
                                                        CommandName="desactivar"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        runat="server">
                                                                    <span class="fa fa-eye"></span>
                                                                    &nbsp;Desactivar
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminar"
                                                        OnClientClick="confirm('Esta seguro de eliminar la sección')"
                                                        Style="display: block; padding: 10px; padding-left: 0; color: gray; text-decoration: none !important;"
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
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="panel-body" id="divPreguntas" visible="false" runat="server">
        <div class="container-fluid shadow p-3 mb-5 bg-white rounded"
            style="background-color: white; padding-left: 25px !important; padding-top: 5px !important;">
            <div class="box-header with-border">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-md-12">
                        <h3 id="lblTitulo"></h3>
                    </div>
                </div>
                <div class="row">
                    <!--<div class="col-md-6">
                        <label>Sección</label>
                        <asp:DropDownList ID="DDLSeccion" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>-->
                    <div class="col-md-12">
                        <label>Tipo Pregunta</label>
                        <asp:DropDownList ID="DDLTipo" CssClass="form-control" runat="server">
                            <asp:ListItem Text="Multiple Opcion - Respuesta Unica" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Multiple Opcion - Respuesta Multiple" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Respuesta Numerica" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Respuesta de Texto" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Calificacion 100-75-50-25" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-top: 25px;">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label>Texto Pregunta</label>
                            <asp:TextBox TextMode="MultiLine" 
                                ID="txtTextoPregunta" CssClass="form-control" 
                                 Mode="Encode" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: 25px;">
                    <div class="col-md-12">
                        <asp:LinkButton ID="btnAceptaPregunta" OnClick="btnAceptaPregunta_Click"
                            Style="height: 100%; padding-top: 10px;"
                            CssClass="btn btn-default-uotline-small" runat="server">
                            <span class="fa fa-check">&nbsp;Guardar</span>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="box-body" id="divRespuestas" runat="server" visible="false" style="margin-top: 45px; padding: 15px; border: solid lightgray; border-radius: 15px;">
                <div class="row">
                    <div class="col-md-8">
                        <h3 class="box-title" style="font-size: 24px; color: var(--secondary);">Opciones de respuesta</h3>
                    </div>
                    <div class="col-md-4" style="text-align: right;">
                        <a data-target="#modalUpdate"
                            data-toggle="modal"
                            data-id=''
                            data-nombre=''
                            data-icono=''
                            href="#"
                            title="Editar Pregunta" class="btn btn-default-uotline-small">Agregar Opcion de respuesta
                        </a>
                    </div>
                </div>
                <div class="row" style="margin-top: 15px;">
                    <div class="col-md-12">
                        <asp:GridView ID="gvRespuestas"
                            CssClass="table"
                            runat="server"
                            EmptyDataText="Aun no se han agregado opciones de respuesta a la pregunta"
                            CellPadding="4"
                            AutoGenerateColumns="false"
                            OnRowCommand="gvRespuestas_RowCommand"
                            OnRowDataBound="gvRespuestas_RowDataBound"
                            ForeColor="#333333"
                            GridLines="None">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <%# Eval("ID") %>
                                        <input type="hidden" name="LocationId2" value='<%# Eval("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TEXTO" HeaderText="Texto" />
                                <asp:TemplateField HeaderText="Tipo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipoPregunta" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                                                        data-target="#modalUpdate"
                                                        data-toggle="modal"
                                                        data-id='<%# Eval("ID") %>'
                                                        data-nombre='<%# Eval("TEXTO") %>'
                                                        href="#"
                                                        title="Editar Pregunta"><span class="fa fa-edit"></span></a>
                                                    <asp:LinkButton ID="btnActivar"
                                                        ToolTip="Activar"
                                                        CommandName="activar"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        runat="server">
                                <span class="fa fa-eye-slash"></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnDesactivar"
                                                        ToolTip="Desactivar"
                                                        CommandName="desactivar"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        runat="server">
                                <span class="fa fa-eye"></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnEliminar"
                                                        ToolTip="Eliminar"
                                                        OnClientClick="return confirm('¿Esta seguro de borrar la opcion de respuesta?');"
                                                        CommandName="eliminar"
                                                        CommandArgument='<%# Eval("ID") %>'
                                                        runat="server">
                                <span class="fa fa-remove"></span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Button Text="Actualizar Orden" ID="ActualizarReferencia2" runat="server" OnClick="ActualizarReferencia2_Click" />
                    </div>
                </div>
            </div>
            <div class="box-footer" style="text-align: right; margin-top: 35px;">
                <button type="button" id="btnAceptar" runat="server" onserverclick="btnAceptar_ServerClick"
                    data-dismiss="modal" class="btn btn-secondary">
                    Salir
                </button>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="modal fade" id="modalUpdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="myModalLabel">Agregar opcion de Respuesta
                            </h5>
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label>Respuesta</label>
                                <asp:TextBox ID="txtRespuesta" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">
                                Cancelar
                            </button>
                            <button type="button" id="btnAceptarRespuesta" runat="server" onserverclick="btnAceptarRespuesta_ServerClick"
                                data-dismiss="modal" class="btn btn-default-uotline-small">
                                Aceptar
                            </button>
                        </div>
                    </div>

                </div>

            </div>

        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="modal fade" id="modalSeccion" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="myModalSeccion">Agregar sección
                            </h5>
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Nombre sección</label>
                                        <asp:TextBox ID="txtNombreSeccion" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Tipo</label>
                                        <asp:DropDownList ID="DDLTipoSeccion" CssClass="form-control"
                                            runat="server">
                                            <asp:ListItem Value="1" Text="Calificacion 100-75-50-25"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Libre"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">
                                Cancelar
                            </button>
                            <button type="button" id="btnCrearSeccion" runat="server"
                                onserverclick="btnCrearSeccion_ServerClick"
                                data-dismiss="modal" class="btn btn-default-uotline-small">
                                Aceptar
                            </button>
                        </div>
                    </div>

                </div>

            </div>

        </div>
    </div>
    <script src="../App_Themes/bower_components/jquery/dist/jquery.slim.js"></script>
    <script src="../App_Themes/bower_components/ckeditor/ckeditor.js"></script>
    <script>
        CKEDITOR.replace('ContentPlaceHolder1_txtTextoPregunta');
    </script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            $('#modalUpdate').on('show.bs.modal', function (e) {
                var id = $(e.relatedTarget).data().id;
                $("#ContentPlaceHolder1_hIdRespuesta").val(id);

                var nombre = $(e.relatedTarget).data().nombre;
                $(e.currentTarget).find('#ContentPlaceHolder1_txtRespuesta').val(nombre);
            });
            $('#modalSeccion').on('show.bs.modal', function (e) {
                var id = $(e.relatedTarget).data().id;
                $("#ContentPlaceHolder1_hIdSeccion").val(id);

                var nombre = $(e.relatedTarget).data().nombre;
                $(e.currentTarget).find('#ContentPlaceHolder1_txtNombreSeccion').val(nombre);
            });
        });
        function cambiarIcono(icono) {
            $("#iIcono").removeClass();
            $("#iIcono").addClass(icono);
            $('#ContentPlaceHolder1_txtIcono').val(icono);
            $("#ContentPlaceHolder1_hIdIcono").val(icono);
        }
    </script>
    <!--
    <script src="../App_Themes/order/jquery.min.js"></script>
    <link href="../App_Themes/order/jquery-ui.css" rel="stylesheet" />
    <script src="../App_Themes/order/jquery-ui.min.js"></script>
    <script>
        var $jq = jQuery.noConflict();
    </script>
    <script type="text/javascript">
        $jq(function () {
            $jq("[id*=gvPreguntas]").sortable({
                items: 'tr:not(tr:first-child)',
                cursor: 'pointer',
                axis: 'y',
                dropOnEmpty: false,
                start: function (e, ui) {
                    ui.item.addClass("selected");
                },
                stop: function (e, ui) {
                    ui.item.removeClass("selected");
                },
                receive: function (e, ui) {
                    $jq(this).find("tbody").append(ui.item);
                }
            });
        });
    </script>
    <script type="text/javascript">
        $jq(function () {
            $jq("[id*=gvRespuestas]").sortable({
                items: 'tr:not(tr:first-child)',
                cursor: 'pointer',
                axis: 'y',
                dropOnEmpty: false,
                start: function (e, ui) {
                    ui.item.addClass("selected");
                },
                stop: function (e, ui) {
                    ui.item.removeClass("selected");
                },
                receive: function (e, ui) {
                    $jq(this).find("tbody").append(ui.item);
                }
            });
        });
    </script>
    -->
</asp:Content>
