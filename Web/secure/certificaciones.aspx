<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="certificaciones.aspx.cs" Inherits="web.secure.certificaciones"
    UICulture="es" Culture="es-MX" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="padding-top: 60px;">
        <div class="col-md-10 col-md-offset-1">
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="height: 40px;">
                        <h4>Certificaciones del Empleado</h4>
                        <p>&nbsp;</p>
                    </div>


                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-md-8 col-md-offset-1" style="padding-top: 10px;">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>
                                            Legajo : 
                                        </label>
                                        <asp:TextBox ID="txtLegajo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label>
                                            Nombre : 
                                        </label>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>

                </div>
                <!-- ///////////////////////////////////////////////////////////////////////////////////////// -->
                <!-- ////////////////////////////// DETALLE ////////////////////////////////////////////////// -->
                <div class="panel panel-primary">
                    <div class="panel-heading" style="height: 40px;">
                        <h4>Movimientos</h4>
                    </div>
                    <!-- ////////////////////////////// GRILLA DETALLE /////////////////////////////////////// -->
                    <asp:UpdatePanel ID="PanelDetalle" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnExporCtaCte" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="panel-body">
                                <div class="row" style="padding: 20px; padding-top: 10px;">
                                    <div style="overflow: scroll; height: 300px;">
                                        <asp:GridView ID="gvDetalle" CssClass="" runat="server" Width="100%"
                                            CellPadding="4" ForeColor="Black" GridLines="Horizontal"
                                            AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                            <Columns>
                                                <asp:BoundField HeaderText="Legajo" DataField="legajo">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre"></asp:BoundField>
                                                <asp:BoundField DataField="anio" HeaderText="Año"></asp:BoundField>
                                                <asp:BoundField DataField="periodo" HeaderText="Periodo"></asp:BoundField>
                                                <asp:BoundField DataField="des_liquidacion" HeaderText="Des Liq"></asp:BoundField>
                                                <asp:BoundField DataField="cargo" HeaderText="Cargo"></asp:BoundField>
                                                <asp:BoundField DataField="tarea" HeaderText="Tarea"></asp:BoundField>
                                                <asp:BoundField DataField="importe" HeaderText="Importe"></asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black"></FooterStyle>
                                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                            <PagerStyle HorizontalAlign="Right" BackColor="White" ForeColor="Black"></PagerStyle>
                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                                            <SortedAscendingCellStyle BackColor="#F7F7F7"></SortedAscendingCellStyle>
                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B"></SortedAscendingHeaderStyle>
                                            <SortedDescendingCellStyle BackColor="#E5E5E5"></SortedDescendingCellStyle>
                                            <SortedDescendingHeaderStyle BackColor="#242121"></SortedDescendingHeaderStyle>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <!-- ////////////////////////////////// BOTONES ////////////////////////////////////////////// -->
                    <div class="panel-footer" style="text-align: right;">
                        <div class="form-group">
                            <asp:Button ID="btnConsultar" CssClass="btn btn-info" runat="server" Text="Consultar Movimientos" CausesValidation="False"
                                OnClick="btnConsultar_Click" />
                            <asp:Button ID="btnExporCtaCte" CssClass="btn btn-success" runat="server" Text="Exportar a Excel" CausesValidation="False"
                                OnClick="btnExporCtaCte_Click" />
                            <asp:Button ID="btnSalir" runat="server" CssClass="btn btn-warning" Text="Salir" CausesValidation="False" OnClick="btnSalir_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
