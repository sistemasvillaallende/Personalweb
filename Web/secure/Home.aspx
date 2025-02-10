<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs"
    Inherits="web.secure.Home" UICulture="es" Culture="es-MX" %>

    <%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

        <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />

            <div class="" style="margin-top: 20px; padding-top: 20px">
                <div class="row">

                    <div class="botones-home">
                        <div>
                            <asp:LinkButton ID="lnbAdd_legajo" OnClick="lnbAdd_legajo_Click" runat="server"
                                CssClass="home-button">
                                <span class="fa fa-male icon-style"></span>
                                Agregar Empleados
                            </asp:LinkButton>
                        </div>
                        <div>
                            <asp:LinkButton ID="lnbBuscar_legajo" OnClick="lnbBuscar_legajo_Click" runat="server"
                                CssClass="home-button">
                                <span class="fa fa-search icon-style"></span>
                                Buscar Empleado
                            </asp:LinkButton>
                        </div>
                        <div>
                            <asp:LinkButton ID="lnbConceptos_lq" OnClick="lnbConceptos_lq_Click" runat="server"
                                CssClass="home-button">
                                <span class="fa fa-align-justify icon-style"></span>
                                Conceptos Liquidacion
                            </asp:LinkButton>
                        </div>
                        <div>
                            <asp:LinkButton ID="lnkCategorias" runat="server" OnClick="lnkCategorias_Click"
                                CssClass="home-button">
                                <span class="fa fa-th icon-style"></span>
                                Categorias
                            </asp:LinkButton>
                        </div>
                        <div>
                            <asp:LinkButton ID="lnbLiquidacion" runat="server" OnClick="lnbLiquidacion_Click"
                                CssClass="home-button">
                                <span class="fa fa-money icon-style"></span>
                                Liquidaciones
                            </asp:LinkButton>
                        </div>
                        <div>
                            <asp:LinkButton ID="lnbNovedades_liq" runat="server" OnClick="lnbNovedades_liq_Click"
                                CssClass="home-button">
                                <span class="fa fa-list icon-style"></span>
                                Carga Novedades de Liquidación
                            </asp:LinkButton>
                        </div>
                        <div>
                            <asp:LinkButton ID="lnkAportes_Jubilatorios" runat="server"
                                OnClick="lnkAportes_Jubilatorios_Click" CssClass="home-button">
                                <span class="fa fa-book icon-style"></span>
                                Aportes Jubilatorios
                            </asp:LinkButton>
                        </div>
                        <div>
                            <asp:LinkButton ID="lnbInformes" runat="server" OnClick="lnbInformes_Click"
                                CssClass="home-button">
                                <span class="fa fa-list-alt icon-style"></span>
                                Informe de Liq. Detallado
                            </asp:LinkButton>
                        </div>
                        <div>
                            <asp:LinkButton ID="lnkReportes_sueldos" runat="server" OnClick="lnkReportes_sueldos_Click"
                                CssClass="home-button">
                                <span class="fa fa-print icon-style"></span>
                                Recibos de Sueldos
                            </asp:LinkButton>
                        </div>
                        <div>
                            <asp:LinkButton ID="lnkReportes_liq" runat="server" OnClick="lnkReportes_liq_Click"
                                CssClass="home-button">
                                <span class="fa fa-folder-open icon-style"></span>
                                Informes de Liquidaciones
                            </asp:LinkButton>
                        </div>
                        <div>
                            <asp:LinkButton ID="lnkAcreditacion_bancos" runat="server"
                                OnClick="lnkAcreditacion_bancos_Click" CssClass="home-button">
                                <span class="fa fa-bank icon-style"></span>
                                Acreditaciones Bancos
                            </asp:LinkButton>
                        </div>
                        <div>
                            <asp:LinkButton ID="lnkSijcor" runat="server" OnClick="lnkSijcor_Click"
                                CssClass="home-button">
                                <span class="fa fa-folder-open icon-style"></span>
                                Sijcor
                            </asp:LinkButton>
                        </div>
                        <div>
                            <asp:LinkButton ID="LinkConsulta" runat="server" OnClick="LinkConsulta_Click"
                                CssClass="home-button">
                                <span class="fa fa-briefcase icon-style"></span>
                                Informes de Conceptos de Liq.
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Content>