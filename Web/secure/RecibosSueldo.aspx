<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="RecibosSueldo.aspx.cs"
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

        .dropdown-menu > li {
            padding: 5px !important;
            border-bottom: solid 1px lightgray;
        }

            .dropdown-menu > li > a {
                color: gray;
            }

        .btn-outline-primary:not(:disabled):not(.disabled).active, .btn-outline-primary:not(:disabled):not(.disabled):active, .show > .btn-outline-primary.dropdown-toggle {
            color: #fff !important;
            background-color: darkcyan !important;
            border-color: darkcyan !important;
        }

        .pry {
            background-color: white;
            color: dimgray !important;
            display: inline-grid !important;
            padding: 15px !important;
            margin-bottom: 20px !important;
            height: 110px !important;
            box-shadow: 0 .3rem 1rem rgba(0.15, 0.15, 0.15, .15) !important;
            border-radius: 15px !important;
            font-weight: 500 !important;
        }

        .fa {
            font-size: 24px !important;
            margin-bottom: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <div class="alert alert-danger alert-dismissible" id="divError" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                    <h4><i class="icon fa fa-ban"></i>Error!</h4>
                    <p id="txtError" runat="server"></p>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1" style="display: flex;">
                <img class="auto-style1" src="../App_Themes/images/usuario.png" alt="user image" />
            </div>
            <div class="col-md-11">
                <p class="username" style="margin-bottom: 0px; margin-top: 10px;">
                    <a href="#" id="lblNombreEmpleado" runat="server"
                        style="color: darkcyan; font-weight: 600;"></a>
                </p>
                <p class="description" id="lblLegajo" runat="server"></p>
            </div>
        </div>
        <hr style="margin-top: 0; margin-bottom: 25px; border: 0; border-top: 2px solid rgb(0 0 0);" />
        <div class="row" style="margin-bottom:25px;">
            <div class="col-md-8">
                <h4 id="lblTitulo" runat="server">Recibos de Sueldo</h4>
            </div>
            <div class="col-md-4">
                <div class="btn-group pull-right">
                    <div class="btn-group">
                        <button type="button" class="btn btn-outline-primary dropdown-toggle"
                            data-toggle="dropdown" aria-expanded="true">
                            Buscar Anteriores <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu" id="ddlAnteriores" runat="server">
                        </ul>
                        <asp:LinkButton ID="cmdSalir" runat="server" CssClass="btn btn-outline-dark" Text=" Volver "
                            OnClick="cmdSalir_Click">

                        </asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>
        <div id="divSueldos" runat="server" class="row">
        </div>
        <div class="row">
            <hr style="border-top: 2px solid hsl(0deg 0% 13%);" />
        </div>
        <div class="row" style="margin-bottom:25px;">
            <div class="col-md-12">
                <h4 id="H1" runat="server">Aginaldos</h4>
            </div>
        </div>
        <div class="row" id="divAguinaldos" runat="server">
        </div>
    </div>
</asp:Content>
