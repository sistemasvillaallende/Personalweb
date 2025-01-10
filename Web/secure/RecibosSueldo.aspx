<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="RecibosSueldo.aspx.cs"
    Inherits="web.secure.RecibosSueldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .pry {
            background-color: #3c8dbc;
            border-color: #367fa9;
            color: white;
        }

        .auto-style1 {
            width: 60px;
            height: 59px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12">
        <div class="alert alert-danger alert-dismissible" id="divError" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
            <h4><i class="icon fa fa-ban"></i>Error!</h4>
            <p id="txtError" runat="server"></p>
        </div>
    </div>
    <div class="col-md-12" style="padding-top: 80px;">
        <div class="box box-primary">
            <div class="box-body box-profile">
                <div class="user-block">
                    <div class="row">
                        <div class="col-md-12">
                            <img class="auto-style1" src="../App_Themes/images/usuario.png" alt="user image" />
                            <p class="username">
                                <a href="#" id="lblNombreEmpleado" runat="server"></a>
                            </p>
                            <p class="description" id="lblLegajo" runat="server"></p>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary" style="box-shadow: 0 1px 1px rgba(0, 0, 0, 0.77);">
                            <div class="box-header with-border" style="border-bottom: 1px solid #ddd;">
                                <div class="col-md-8">
                                    <h4 id="lblTitulo" runat="server">Recibos de Sueldo</h4>
                                </div>
                                <div class="col-md-4">
                                    <div class="btn-group pull-right">
                                        <div class="btn-group">
                                            <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-expanded="true">
                                                Buscar Anteriores <span class="caret"></span>
                                            </button>
                                            <ul class="dropdown-menu" id="ddlAnteriores" runat="server">
                                            </ul>
                                        </div>
                                    </div>
                                </div>


                            </div>
                            <div class="box-body">
                                <div id="divSueldos" runat="server">
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <hr style="border-top: 1px solid #cacaca;" />
                                    </div>
                                    <div class="col-md-12" id="divAguinaldos" runat="server">
                                    </div>

                                </div>
                                <div class="box" style="text-align: right;">
                                    <div class="form-group">
                                        <asp:Button ID="cmdSalir" runat="server" CssClass="btn btn-warning" Text=" Volver " OnClick="cmdSalir_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.box-body -->
            </div>
        </div>
    </div>
</asp:Content>
