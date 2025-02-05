<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="MovimEmpleados.aspx.cs" Inherits="web.secure.MovimEmpleados" %>

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
            border:solid 1px gray;
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
    <div class="container-fluid" style="padding: 20px; border: solid; border-radius: 15px; box-shadow: 0px 2px 16px -2px rgba(0, 0, 0, 0.75);">
        <div class="row" style="padding: 15px;">
            <div class="col-md-12" style="display: flex; padding: 15px; background-color: white; padding-top: 0; -moz-box-shadow: 0px 2px 16px -2px rgba(0, 0, 0, 0.75); border-bottom: solid;">
                <p style="width: 33%; border-right: solid; margin-bottom: 0">
                    <strong style="display: block; font-size: 18px;">Velez Spitale, Ignacio Martin</strong>
                    <span style="font-size: 16px; display: block;">Legajo: 710</span>
                    <span style="font-size: 16px; display: block;">Paso a contrato: 20-08-2011</span>
                </p>
                <p style="width: 33%; padding-left: 20px; border-right: solid; margin-bottom: 0">
                    <span style="font-size: 16px; display: block;"><strong>Clasificacion Personal:</strong> Personal Contratado</span>
                    <span style="font-size: 16px; display: block;"><strong>Cargo:</strong> INSPECTOR I</span>
                    <span style="font-size: 16px; display: block;"><strong>Categoria:</strong> 4</span>
                </p>
                <p style="width: 33%; padding-left: 20px; margin-bottom: 0">
                    <span style="font-size: 16px; display: block;"><strong>Seccion:</strong> Personal Contratado Administrativo</span>
                    <span style="font-size: 16px; display: block;"><strong>Tarea:</strong> Administrativo Hospital Municipal Josefina Prieur</span>
                    <span style="font-size: 16px; display: block;"><strong>Liquidacion:</strong> EFECTIVOS</span>
                </p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <h3 style="margin-top: 20px; margin-bottom: 20px; font-size: 24px !important; font-weight: 600 !important;">Movimientos internos</h3>
                <div class="timeline">
                    <div class="timeline__event animated fadeInUp delay-2s timeline__event--type2">
                        <div class="timeline__event__icon">
                            <i class="lni-burger"></i>
                            <div class="timeline__event__date">
                                04-08-2014
                            </div>
                        </div>
                        <div class="timeline__event__content">
                            <div class="timeline__event__title">
                                Cambio de Tarea
                            </div>
                            <div class="timeline__event__description">
                                <p style="margin-bottom: 0;">Cambia a Oficina de Sistemas</p>
                            </div>
                        </div>
                    </div>
                    <div class="timeline__event animated fadeInUp delay-1s timeline__event--type3">
                        <div class="timeline__event__icon">
                            <i class="lni-slim"></i>
                            <div class="timeline__event__date">
                                23-10-2021
                            </div>
                        </div>
                        <div class="timeline__event__content">
                            <div class="timeline__event__title">
                                Camnbio de Categoria
                            </div>
                            <div class="timeline__event__description">
                                <p style="margin-bottom: 0;">Pasa a Categoria 10</p>
                            </div>
                        </div>
                    </div>
                    <div class="timeline__event animated fadeInUp timeline__event--type1">
                        <div class="timeline__event__icon">
                            <i class="lni-cake"></i>
                            <div class="timeline__event__date">
                                21-03-2021
                            </div>
                        </div>
                        <div class="timeline__event__content">
                            <div class="timeline__event__title">
                                Cambio Clasificación Personal
                            </div>
                            <div class="timeline__event__description">
                                <p style="margin-bottom: 0;">Pasa a: Personal Permanente</p>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
            <div class="col-md-6">
                <h3 style="margin-top: 20px; margin-bottom: 20px; font-size: 24px !important; font-weight: 600 !important;">Variación Conceptos de Sueldo</h3>
                <div class="timeline">
                    <div class="timeline__event animated fadeInUp delay-2s timeline__event--type2">
                        <div class="timeline__event__icon">
                            <i class="lni-burger"></i>
                            <div class="timeline__event__date">
                                04-08-2014
                            </div>
                        </div>
                        <div class="timeline__event__content">
                            <div class="timeline__event__title">
                                Cambio de Tarea
                            </div>
                            <div class="timeline__event__description">
                                <p style="margin-bottom: 0;">Cambia a Oficina de Sistemas</p>
                            </div>
                        </div>
                    </div>
                    <div class="timeline__event animated fadeInUp delay-1s timeline__event--type3">
                        <div class="timeline__event__icon">
                            <i class="lni-slim"></i>
                            <div class="timeline__event__date">
                                23-10-2021
                            </div>
                        </div>
                        <div class="timeline__event__content">
                            <div class="timeline__event__title">
                                Camnbio de Categoria
                            </div>
                            <div class="timeline__event__description">
                                <p style="margin-bottom: 0;">Pasa a Categoria 10</p>
                            </div>
                        </div>
                    </div>
                    <div class="timeline__event animated fadeInUp timeline__event--type1">
                        <div class="timeline__event__icon">
                            <i class="lni-cake"></i>
                            <div class="timeline__event__date">
                                21-03-2021
                            </div>
                        </div>
                        <div class="timeline__event__content">
                            <div class="timeline__event__title">
                                Cambio Clasificación Personal
                            </div>
                            <div class="timeline__event__description">
                                <p style="margin-bottom: 0;">Pasa a: Personal Permanente</p>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

