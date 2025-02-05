<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true"
    CodeBehind="MovimEmpleados.aspx.cs" Inherits="web.secure.MovimEmpleados" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
            html {
                font-size: 14px;
            }

            body {
                background: #f6f9fc;
                font-family: "Open Sans", sans-serif;
                color: #525f7f;
            }

            h2 {
                margin: 5%;
                text-align: center;
                font-size: 2rem;
                font-weight: 100;
            }

            .timeline {
                display: flex;
                flex-direction: column;
            }

            .timeline__event {
                background: #fff;
                margin-bottom: 20px;
                position: relative;
                display: flex;
                /*margin: 20px 0;*/
                border-radius: 8px;
                border: solid 1px gray;
            }

            .timeline__event__title {
                font-size: 16px;
                line-height: 1.4;
                text-transform: uppercase;
                font-weight: 600;
                color: darkcyan;
                letter-spacing: 1.5px;
            }

            .timeline__event__content {
                padding: 10px;
            }

            .timeline__event__date {
                color: darkslategrey;
                font-size: 16px;
                font-weight: 600;
                white-space: nowrap;
            }

            .timeline__event__icon {
                border-radius: 8px 0 0 8px;
                background: white;
                border-right: solid 1px gray;
                display: flex;
                align-items: center;
                justify-content: center;
                /*flex-basis: 40%;*/
                font-size: 2rem;
                color: #9251ac;
                padding: 20px;
                padding-bottom: 10px;
                padding-top: 10px;
            }

            .timeline__event__icon i {
                position: absolute;
                top: 50%;
                left: -65px;
                font-size: 2.5rem;
                transform: translateY(-50%);
            }

            .timeline__event__description {
                flex-basis: 60%;
            }

            .timeline__event--type2 .timeline__event__date {
                color: darkslategrey;
            }

            .timeline__event--type2 .timeline__event__icon {
                background: white;
                color: darkcyan;
            }

            .timeline__event--type2 .timeline__event__title {
                color: darkcyan;
            }



            .timeline__event--type3 .timeline__event__date {
                color: darkslategrey;
            }

            .timeline__event--type3 .timeline__event__icon {
                background: white;
                color: #24b47e;
                padding-top: 10px;
                padding-bottom: 10px
            }

            .timeline__event--type3 .timeline__event__title {
                color: darkcyan;
            }



            @media (max-width: 786px) {
                .timeline__event {
                    flex-direction: column;
                }

                .timeline__event__icon {
                    border-radius: 4px 4px 0 0;
                }
            }
        </style>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container-fluid"
            style="padding: 20px; border: solid; border-radius: 15px; box-shadow: 0px 2px 16px -2px rgba(0, 0, 0, 0.75);">
            <div class="row" style="padding: 15px;">
                <div class="col-md-12"
                    style="display: flex; padding: 15px; background-color: white; padding-top: 0; -moz-box-shadow: 0px 2px 16px -2px rgba(0, 0, 0, 0.75); border-bottom: solid;">
                    <p style="width: 33%; border-right: solid; margin-bottom: 0">
                        <strong style="display: block; font-size: 18px;">
                            Nombre: <asp:Label ID="lblNombre" runat="server" Text="nombre"></asp:Label>
                        </strong>
                        <span style="font-size: 16px; display: block;">Legajo: <asp:Label ID="lblLegajo" runat="server"
                                Text="-"></asp:Label></span>
                        <span style="font-size: 16px; display: block;">Paso a contrato: <asp:Label ID="lblPasoContrato"
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
                        style="margin-top: 20px; margin-bottom: 20px; font-size: 24px !important; font-weight: 600 !important;">
                        Movimientos internos</h3>
                    <asp:PlaceHolder ID="phMovimientosInternos" runat="server"></asp:PlaceHolder>
                </div>
                <div class="col-md-6">
                    <h3
                        style="margin-top: 20px; margin-bottom: 20px; font-size: 24px !important; font-weight: 600 !important;">
                        Variación Conceptos de Sueldo</h3>
                    <asp:GridView ID="gvConceptoMov" CssClass="table table-hover table-bordered" runat="server"
                        OnRowDataBound="gvConceptoMov_RowDataBound" OnRowCommand="gvConceptoMov_RowCommand"
                        CellPadding="4" AutoGenerateColumns="False" ForeColor="#333333" GridLines="None"
                        DataKeyNames="FECHA" AllowPaging="True" OnPageIndexChanging="gvConceptoMov_PageIndexChanging"
                        PageSize="8">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Fecha Movimiento" ItemStyle-Width="50%">
                                <ItemTemplate>
                                    <p></p>
                                    <asp:Label ID="lblFechaMov" runat="server" Text=""></asp:Label>
                                    </p>
                                </ItemTemplate>
                                <ItemStyle Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Usuario Carga" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <p>
                                        <asp:Label ID="lblUsuarioCarga" runat="server" Text=""></asp:Label>
                                    </p>
                                </ItemTemplate>
                                <ItemStyle Width="30%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TipoMovimiento" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <p>
                                        <asp:Label ID="lblTipoMov" runat="server" Text=""></asp:Label>
                                    </p>
                                </ItemTemplate>
                                <ItemStyle Width="30%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cod. Concepto" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <p>
                                        <asp:Label ID="lblCodConcepto" runat="server" Text=""></asp:Label>
                                    </p>
                                </ItemTemplate>
                                <ItemStyle Width="30%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Concepto" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <p>
                                        <asp:Label ID="lblConcepto" runat="server" Text=""></asp:Label>
                                    </p>
                                </ItemTemplate>
                                <ItemStyle Width="30%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor Concepto" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <p>
                                        <asp:Label ID="lblValorConcepto" runat="server" Text=""></asp:Label>
                                    </p>
                                </ItemTemplate>
                                <ItemStyle Width="30%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Observacion" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <p>
                                        <asp:Label ID="lblObservacion" runat="server" Text=""></asp:Label>
                                    </p>
                                </ItemTemplate>
                                <ItemStyle Width="30%" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999"></EditRowStyle>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
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
    </asp:Content>