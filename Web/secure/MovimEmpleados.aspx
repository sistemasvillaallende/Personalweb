<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true"
    CodeBehind="MovimEmpleados.aspx.cs" Inherits="web.secure.MovimEmpleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../App_Themes/Estilos2025/main.css" rel="stylesheet" />
    <style>
        .marco {
            padding: 20px;
            border-radius: 15px;
            box-shadow: 0 3px 6px rgba(0, 0, 0, .16), 0 3px 6px rgba(0, 0, 0, .23) !important;
            margin-bottom: 20px;
            border:none !important
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid marco">
        <div class="row" style="padding: 15px;">
            <div class="col-md-12"
                style="display: flex; padding: 15px; background-color: white; padding-top: 0; -moz-box-shadow: 0px 2px 16px -2px rgba(0, 0, 0, 0.75); border-bottom: solid;">
                <p style="width: 33%; border-right: solid; margin-bottom: 0">
                    <strong style="display: block; font-size: 18px;">Nombre:
                        <asp:Label ID="lblNombre" runat="server" Text="nombre"></asp:Label>
                    </strong>
                    <span style="font-size: 16px; display: block;">Legajo:
                        <asp:Label ID="lblLegajo" runat="server"
                            Text="-"></asp:Label></span>
                    <span style="font-size: 16px; display: block;">Paso a contrato:
                        <asp:Label ID="lblPasoContrato"
                            runat="server" Text="-"></asp:Label></span>
                </p>
                <p style="width: 33%; padding-left: 20px; border-right: solid; margin-bottom: 0">
                    <span style="font-size: 16px; display: block;"><strong>Clasificacion Personal:</strong>
                        <asp:Label ID="lblClasificacionPersonal" runat="server" Text="-">
                        </asp:Label>
                    </span>
                    <span style="font-size: 16px; display: block;"><strong>Cargo:</strong>
                        <asp:Label ID="lblCargo" runat="server" Text="-"></asp:Label>
                    </span>
                    <span style="font-size: 16px; display: block;"><strong>Categoria:</strong>
                        <asp:Label ID="lblCategoria" runat="server" Text="-"></asp:Label>
                    </span>
                </p>
                <p style="width: 33%; padding-left: 20px; margin-bottom: 0">
                    <span style="font-size: 16px; display: block;"><strong>Seccion:</strong>
                        <asp:Label ID="lblSeccion" runat="server" Text="-">
                        </asp:Label>
                    </span>
                    <span style="font-size: 16px; display: block;"><strong>Tarea:</strong>
                        <asp:Label ID="lblTarea" runat="server" Text="-"></asp:Label>
                    </span>
                    <span style="font-size: 16px; display: block;"><strong>Liquidacion:</strong>
                        <asp:Label ID="lblLiquidacion" runat="server" Text="-"></asp:Label>
                    </span>
                </p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <h3
                    style="margin-top: 20px; margin-bottom: 20px; font-size: 20px !important; font-weight: 600 !important;">Movimientos internos</h3>
                <asp:PlaceHolder ID="phMovimientosInternos" runat="server"></asp:PlaceHolder>
            </div>
            <div class="col-md-6">
                <h3
                    style="margin-top: 20px; margin-bottom: 20px; font-size: 20px !important; font-weight: 600 !important;">Variación Conceptos de Sueldo</h3>
                <asp:PlaceHolder ID="phVariacionConceptos" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </div>
</asp:Content>
