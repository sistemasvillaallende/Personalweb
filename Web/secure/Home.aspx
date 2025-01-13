<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="web.secure.Home"
    UICulture="es" Culture="es-MX" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-12" style="margin-top: 20px; padding-top: 20px">
        <div class="row">
            <div class="row" style="padding-top: 50px;" id="divLink1">
                <div class="col-md-10 col-md-offset-1" style="padding-left: 20px;" id="divFiltros" runat="server">
                    <div class="row">
                        <div class="col-md-3 box0" id="divAdd">
                            <div class="box1">
                                <asp:LinkButton ID="lnbAdd_legajo" OnClick="lnbAdd_legajo_Click" runat="server">
                            <span class="fa fa-male" style="color: #3c763d;font-size: 30px;"></span>
                                </asp:LinkButton>
                                <h4>Agregar Empleados</h4>
                            </div>
                            <p runat="server" id="pProcesando"></p>
                        </div>
                        <div class="col-md-3 box0" id="divBuscar">
                            <div class="box1">
                                <asp:LinkButton ID="lnbBuscar_legajo" OnClick="lnbBuscar_legajo_Click" runat="server">
                            <span class="fa fa-search" style="color: #0094ff;font-size: 30px;"></span></asp:LinkButton>
                                <h4>Buscar Empleado</h4>
                            </div>
                            <p runat="server" id="pNew"></p>
                        </div>
                        <div class="col-md-3 box0" id="divConceptos_liq">
                            <div class="box1">
                                <asp:LinkButton ID="lnbConceptos_lq" OnClick="lnbConceptos_lq_Click" runat="server">
                            <span class="fa fa-align-justify" style="color:blue; font-size:30px"></span>
                                </asp:LinkButton>
                                <h4>Conceptos Liquidacion</h4>
                            </div>
                            <p runat="server" id="p4"></p>
                        </div>
                        <div class="col-md-3 box0" id="divParametros">
                            <div class="box1">
                                <asp:LinkButton ID="lnkCategorias" runat="server" OnClick="lnkCategorias_Click">
                            <span class="fa fa-th" style="color:darkgreen; font-size:30px"></span>
                                </asp:LinkButton>
                                <h4>Categorias</h4>
                            </div>
                            <p runat="server" id="p5"></p>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" style="padding-top: 50px;" id="divLink2">
                <div class="col-md-10 col-md-offset-1" style="padding-left: 20px;" id="div1" runat="server">
                    <div class="row">
                        <div class="col-md-3 box0" id="divLiquidacion">
                            <div class="box1">
                                <asp:LinkButton ID="lnbLiquidacion" runat="server" OnClick="lnbLiquidacion_Click">
                            <span class="fa fa-calendar-o" style="color:darkgreen;font-size:30px"></span>
                                </asp:LinkButton>
                                <h4>Liquidaciones</h4>
                            </div>
                            <p runat="server" id="p1"></p>
                        </div>
                        <div class="col-md-3 box0" id="divNovedades_liq">
                            <div class="box1">
                                <asp:LinkButton ID="lnbNovedades_liq" runat="server" OnClick="lnbNovedades_liq_Click">
                            <span class="glyphicon glyphicon-list-alt" style="color:chocolate; font-size:30px"></span>
                                </asp:LinkButton>
                                <h4>Carga Novedades de Liquidación</h4>
                            </div>
                            <p runat="server" id="p3"></p>
                        </div>
                        <div class="col-md-3 box0" id="divAportes">
                            <div class="box1">
                                <asp:LinkButton ID="lnkAportes_Jubilatorios" runat="server" OnClick="lnkAportes_Jubilatorios_Click">
                            <span class="fa fa-book" style="color:darkgreen;font-size:30px"></span>
                                </asp:LinkButton>
                                <h4>Aportes Jubilatorios</h4>
                            </div>
                            <p runat="server" id="p7"></p>
                        </div>
                        <div class="col-md-3 box0" id="divInformes">
                            <div class="box1">
                                <asp:LinkButton ID="lnbInformes" runat="server" OnClick="lnbInformes_Click">
                            <span class="fa fa-list-alt" style="color:burlywood; font-size:30px"></span>
                                </asp:LinkButton>
                                <h4>Informe de Liq. Detallado</h4>
                            </div>
                            <p runat="server" id="p2"></p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 50px;" id="divLink3">
                <div class="col-md-10 col-md-offset-1" style="padding-left: 20px;" id="div2" runat="server">
                    <div class="row">
                        <div class="col-md-3 box0" id="divL1">
                            <div class="box1">
                                <asp:LinkButton ID="lnkReportes_sueldos" runat="server" OnClick="lnkReportes_sueldos_Click">
                            <span class="fa fa-print" style="color:darkgreen;font-size:30px"></span>
                                </asp:LinkButton>
                                <h4>Recibos de Sueldos</h4>
                            </div>
                            <p runat="server" id="p10"></p>
                        </div>
                        <div class="col-md-3 box0" id="divReprt">
                            <div class="box1">
                                <asp:LinkButton ID="lnkReportes_liq" runat="server" OnClick="lnkReportes_liq_Click">
                            <span class="fa fa-folder-open" style="color:chocolate; font-size:30px"></span>
                                </asp:LinkButton>
                                <h4>Informes de Liquidaciones</h4>
                            </div>
                            <p runat="server" id="p9"></p>
                        </div>
                        <div class="col-md-3 box0" id="divAcreditacion_bancos">
                            <div class="box1">
                                <asp:LinkButton ID="lnkAcreditacion_bancos" runat="server" OnClick="lnkAcreditacion_bancos_Click">
                            <span class="fa fa-bank" style="color:darkgreen; font-size:30px"></span>
                                </asp:LinkButton>
                                <h4>Acreditaciones Bancos</h4>
                            </div>
                            <p runat="server" id="p8"></p>
                        </div>
                        <div class="col-md-3 box0" id="divSijcor">
                            <div class="box1">
                                <asp:LinkButton ID="lnkSijcor" runat="server" OnClick="lnkSijcor_Click">
                            <span class="fa fa-folder-open" style="color:chocolate; font-size:30px"></span>
                                </asp:LinkButton>
                                <h4>Sijcor</h4>
                            </div>
                            <p runat="server" id="p6"></p>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 50px;" id="divLink4">
                <div class="col-md-10 col-md-offset-1" style="padding-left: 20px;" id="div3" runat="server">
                    <div class="row">
                        <div class="col-md-3 box0" id="div10">
                            <div class="box1">
                                <asp:LinkButton ID="LinkConsulta" runat="server" OnClick="LinkConsulta_Click">
                            <span class="fa fa-briefcase" style="color:darkgreen;font-size:30px"></span>
                                </asp:LinkButton>
                                <h4>Informes de Conceptos de Liq.</h4>
                            </div>
                            <p runat="server" id="p11"></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
