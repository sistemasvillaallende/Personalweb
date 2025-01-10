<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="familiares.aspx.cs"
    Inherits="web.secure.familiares" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .pry {
            background-color: #3c8dbc;
            border-color: #083048;
            color: white;
        }

        .auto-style2 {
            width: 60px;
            height: 59px;
        }
    </style>

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
    </style>

    <style type="text/css">
        .gridview {
            background-color: #fff;
            height: 60px;
            padding: 2px;
            margin: 4% auto;
        }

            .gridview a {
                margin: 5px;
                border-radius: 50%;
                background-color: #444;
                padding: 5px 10px 5px 10px;
                color: #fff !important;
                text-decoration: none;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridview a:hover {
                    background-color: #1e8d12;
                    color: #fff;
                }

            .gridview span {
                background-color: #ae2676;
                color: #fff;
                /*-o-box-shadow: 1px 1px 1px #111;*/
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 10px 5px 10px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="uPanelCliente" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row" style="margin-top: 10px; padding-top: 10px">
                <div class="col-md-12 col-md-offset-0">
                    <div class="col-md-12">
                        <div class="row" style="margin-top: 20px; padding-top: 20px">
                            <asp:UpdatePanel ID="PanelInfomacion" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <div class="alert alert-success alert-dismissable" runat="server" id="divConfirma"
                                        visible="false" role="alert">
                                        <button type="button" class="close" data-dismiss="alert"
                                            onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Confirma');">
                                            <span aria-hidden="true">×</span></button>
                                        </button>
                                <h4>Aviso Importante!</h4>
                                        <p id="msjConfirmar" runat="server">
                                        </p>
                                    </div>

                                    <div class="alert alert-warning alert-dismissible" runat="server" id="divError"
                                        visible="false" role="alert">
                                        <button type="button" class="close" data-dismiss="alert"
                                            onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Alerta');">
                                            <span aria-hidden="true">×</span></button>
                                        </button>
                                <h4>Error!</h4>
                                        <p id="txtError" runat="server">
                                        </p>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="outer_div">
                            <div class="row">
                                <div class="col-md-10 col-md-offset-1">
                                    <div class="box" style="margin-top: 10px;">
                                        <div class="box-header with-border">
                                            <div class="row">
                                                <h3 class="box-title">Empleado</h3>
                                            </div>
                                        </div>
                                        <!-- /.box-header -->
                                        <div class="box-body" style="margin-top: 10px;">
                                            <div class="row">
                                                <div class="user-block">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <img class="auto-style2" src="../App_Themes/images/usuario.png" alt="user image" />
                                                            <p class="username">
                                                                <a href="#" id="lblNombreEmpleado" runat="server"></a>
                                                            </p>
                                                            <p class="description" id="lblLegajo" runat="server"></p>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-6">
                                                            <%--&nbsp;--%>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="btn-group pull-right" id="div1" runat="server">
                                                                <asp:LinkButton ID="LinkButtonVolver" CssClass="btn btn-default" runat="server" OnClick="LinkButtonVolver_Click">
                                                            <i class="fa fa-sign-out"></i> Volver a Empleados
                                                                </asp:LinkButton>
                                                            </div>
                                                            &nbsp;
                                                            <div class="btn-group pull-right" id="div2" runat="server">
                                                                <asp:LinkButton ID="lbtnAddFamilia" CssClass="btn btn-default" runat="server" OnClick="lbtnAddFamilia_Click">
                                                            <i class="fa fa-plus"></i>&nbsp;Agregar Familiares
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="box box-primary">
                                                    <div class="box-header with-border" style="border-bottom: 1px solid #ddd;">
                                                        <div class="row">
                                                            <div class="col-xs-12">
                                                                <div class="input-group">
                                                                    <h4 id="lblTitulo" runat="server">Familiares a Cargo</h4>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="auto-style1" style="margin-top: 20px;">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:GridView ID="gvFamiliares" CssClass="table" runat="server"
                                                        CellPadding="4"
                                                        EmptyDataText="No hay resultados..."
                                                        OnRowDataBound="gvFamiliares_RowDataBound"
                                                        OnRowCommand="gvFamiliares_RowCommand"
                                                        AutoGenerateColumns="false"
                                                        ForeColor="#333333"
                                                        DataKeyNames="legajo, nro_familiar, fecha_alta_registro, nombre, cod_tipo_documento, nro_documento, fecha_nacimiento, parentezco, sexo, salario_familiar, incapacitado, opcion"
                                                        AllowPaging="true"
                                                        OnPageIndexChanging="gvFamiliares_PageIndexChanging"
                                                        PageSize="6"
                                                        GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                        <Columns>
                                                            <asp:BoundField DataField="legajo" HeaderText="Legajo" Visible="false" />
                                                            <asp:BoundField DataField="fecha_alta_registro" HeaderText="Fecha Alta" />
                                                            <%--DataFormatString="{0:d}"--%>
                                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                                            <asp:BoundField DataField="nro_documento" HeaderText="Nro. Documento" />
                                                            <asp:BoundField DataField="fecha_nacimiento" HeaderText="Fecha Nac" />
                                                            <%--DataFormatString="{0:d}"--%>
                                                            <asp:TemplateField HeaderText="Edad">
                                                                <ItemTemplate>
                                                                    <p id="lblEdad" runat="server"></p>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sexo">
                                                                <ItemTemplate>
                                                                    <p id="lblSexo" runat="server"></p>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="parentezco" HeaderText="Parentezco" />
                                                            <asp:TemplateField HeaderText="Salario Hijo">
                                                                <ItemTemplate>
                                                                    <p><i class="fa fa-check-square"></i>&nbsp;<%# (Convert.ToInt16(Eval("salario_familiar"))) == 1 ? "Si" : "No" %></p>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Incapacitado">
                                                                <ItemTemplate>
                                                                    <p><i class="fa fa-check-square"></i>&nbsp;<%# (Convert.ToInt16(Eval("incapacitado"))) == 1 ? "Si" : "No" %></p>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Salario">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSalario"
                                                                        CssClass="form-control"
                                                                        Enabled="true"
                                                                        runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <%--<asp:TemplateField HeaderText="Incapacitado">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkIncapacitado"
                                                                        CssClass="form-control"
                                                                        Enabled="true"
                                                                        runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:BoundField DataField="opcion" HeaderText="opcion" Visible="False" />
                                                            <asp:TemplateField HeaderText="Accion">
                                                                <HeaderStyle />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbEdit" runat="server" CommandName="editar"
                                                                        ImageUrl="~/App_Themes/Tema1/Images/editar.gif"
                                                                        OnClientClick="return confirm('¿Está seguro de Modificar este registro?');"
                                                                        CommandArgument="<%# Container.DataItemIndex %>"
                                                                        CausesValidation="False" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Accion">
                                                                <HeaderStyle />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbDelete" runat="server" CommandName="elimina"
                                                                        ImageUrl="~/App_Themes/Tema1/Images/delete.gif"
                                                                        OnClientClick="return confirm('¿Está seguro de Eliminar este registro?');"
                                                                        CommandArgument="<%# Container.DataItemIndex %>"
                                                                        CausesValidation="False" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                        <EditRowStyle BackColor="#999999"></EditRowStyle>
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                                        <%--<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>--%>
                                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /.box-body -->
                                        <div class="box-footer clearfix">
                                        </div>
                                    </div>
                                    <!-- /.box -->
                                </div>
                                <!-- /.col -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hID" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="Button" Style="visibility: hidden;" />
            <ajaxToolkit:ModalPopupExtender runat="server"
                BackgroundCssClass="modalBackground"
                PopupControlID="modalDatosFamiliares"
                BehaviorID="Datos_ModalPopupExtender"
                TargetControlID="Button1"
                ID="Datos_ModalPopupExtender">
            </ajaxToolkit:ModalPopupExtender>
            <div class="modal-dialog" id="modalDatosFamiliares" runat="server" style="background-color: white; padding: 20px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button"
                            runat="server"
                            id="btnCloseModal"
                            onserverclick="btnCloseModal_ServerClick"
                            class="close" data-dismiss="modal"
                            aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">
                            <asp:Label ID="lblTituloFormModal" runat="server" Text="Label"></asp:Label>
                            <h4 class="modal-title">Agregar Items</h4>
                        </h4>
                    </div>

                    <div class="modal-body" id="activity" style="min-height: 320px;">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Nombre Familiar</label>
                                    <asp:TextBox ID="txtNombre" runat="server" autocomplete="false"
                                        placeholder="Ingrese Nombre de la Persona" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="GroupItems"
                                        ControlToValidate="txtNombre" ErrorMessage="Debe Ingresar el Nombre" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-6">
                                    <label>Tipo Doc</label>
                                    <asp:DropDownList ID="ddTipoDNI" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddTipoDNI"
                                        ErrorMessage="Debe Seleccionar el Tipo De Doc" ForeColor="#FF3300" InitialValue="0"
                                        ValidationGroup="GroupItems" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <label>Nro Documento</label>
                                    <asp:TextBox ID="txtNrodocumento" runat="server" Width="90%" autocomplete="false"
                                        placeholder="Nro Documento" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtNrodocumento"
                                        ErrorMessage="Debe ingresar Nro Documento" SetFocusOnError="True" ValidationGroup="GroupItems">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-6">
                                    <label>Sexo</label>
                                    <asp:DropDownList ID="ddSexo" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="1">FEMENINO</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="2">MASCULINO</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddSexo"
                                        ErrorMessage="Debe Seleccionar Sexo" ForeColor="#FF3300" InitialValue="0"
                                        ValidationGroup="GroupItems" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <label>Fecha Nac</label>
                                    <asp:TextBox ID="txtFecha_nacimiento" runat="server" Width="90%" autocomplete="false" CssClass="form-control"
                                        placeholder="Fecha Nacimiento"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtFecha_nacimiento"
                                        ErrorMessage="Debe ingresar Fecha de Nacimiento" SetFocusOnError="True" ValidationGroup="GroupItems">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <label>Parentezco</label>
                                    <asp:DropDownList ID="ddParentezco" CssClass="form-control" runat="server" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="1">HIJO/A</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="2">CONYUGUE</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="3">CONCUBINO/A</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="4">NIETO/A</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="5">PADRE/MADRE</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="6">FAMILIAR A CARGO</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="7">HIJASTRO/A</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="8">SOBRINO/A</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="9">TIO/A</asp:ListItem>
                                        <asp:ListItem Selected="False" Value="10">HERMANO/A</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddParentezco"
                                        ErrorMessage="Debe Seleccionar" ForeColor="#FF3300" InitialValue="0"
                                        ValidationGroup="GroupItems" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>Salario Fam</label>
                                    <div class="input-group">
                                        <asp:CheckBox ID="chkSalario" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Incapacitado</label>
                                    <div class="input-group">
                                        <asp:CheckBox ID="chkIncapacitado" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="GroupItems" />
                        <asp:Button ID="CancelarFam" runat="server"
                            CssClass="btn btn-default" Text="Cancelar"
                            OnClick="CancelarFam_Click" />
                        <asp:Button ID="AceptarFam" runat="server"
                            ValidationGroup="GroupItems"
                            CssClass="btn btn-primary" Text="Aceptar"
                            OnClick="AceptarFam_Click" />
                    </div>
                </div>
            </div>
            <%-- /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// --%>
            <asp:HiddenField ID="HiddenLegajo" runat="server" />
            <asp:HiddenField ID="HiddenNro_fam" runat="server" />
            <%-- /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// --%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:Button ID="Button2" runat="server" Text="Button" Style="visibility: hidden;" />
    <ajaxToolkit:ModalPopupExtender runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="myModalMessage"
        BehaviorID="popUpMSJ"
        TargetControlID="Button2"
        ID="popUpMSJ">
    </ajaxToolkit:ModalPopupExtender>--%>
    <div id="modalMSJ" runat="server">
        <asp:UpdatePanel ID="updateModalMSJ2" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <!-- Bootstrap Pop-up Box -->
                        <div class="modal fade" id="myModalMessage" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false" aria-labelledby="myModalLabel">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" id="btnModelX" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                        <h4 class="modal-title" id="myModalMessageTitle">Aviso Importante!</h4>
                                    </div>
                                    <div class="modal-body" id="myModalMessageContent">
                                        <h4>
                                            <strong>
                                                <asp:Label ID="lblPregunta" runat="server" Text="Mensaje Pregunta">
                                                </asp:Label>
                                            </strong></h4>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnModalYes" runat="server" onserverclick="btnModalYes_ServerClick"
                                            class="btn btn-primary" data-dismiss="modal">
                                            Si</button>
                                        <button type="button" id="btnModalClose" class="btn btn-default" data-dismiss="modal">No</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:Button ID="Button5" runat="server" Text="Button" Style="visibility: hidden;" />
    <ajaxToolkit:ModalPopupExtender runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="modalMSJAlerta"
        BehaviorID="popUpAlerta"
        TargetControlID="Button5"
        ID="popUpAlerta">
    </ajaxToolkit:ModalPopupExtender>
    <div id="modalMSJAlerta" runat="server">
        <asp:UpdatePanel ID="uPanelAlerta" runat="server">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="alert alert-danger fade in" runat="server" id="divAlerta"
                        visible="false" role="alert">
                        <button type="button" class="close" runat="server" data-dismiss="alert" onclick="__doPostBack('<%=uPanelMSj.ClientID%>', 'AlertaMSJ');">
                            <span arial-hidden="true">&times;</span> <span class="sr-only">Cerrar</span>
                        </button>
                        <strong>Mensaje! </strong>
                        <br />
                        <p id="msj" runat="server">
                        </p>
                        <br />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <!-- /.content -->

    <script type="text/javascript">

        //$('#myModalMessage').modal('hide');
        //if ($('.modal-backdrop').is(':visible')) {
        //    $('body').removeClass('modal-open');
        //    $('.modal-backdrop').remove();
        //};
        //$(document).ready(function () {
        //    $("#btnModalYes").click(function () {
        //        $('#myModalMessage').modal({ backdrop: false });
        //        alert('shufafija');
        //    });
        //});

        $(document).ready(function () {
            //If user click 'Yes'...
            $('#btnModalYes').click(function () {
                alert('Do something...');
            });
        });

    </script>
</asp:Content>



