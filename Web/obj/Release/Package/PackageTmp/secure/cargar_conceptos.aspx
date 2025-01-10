<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="cargar_conceptos.aspx.cs" Inherits="web.secure.cargar_conceptos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../styles/AdminLTE.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="padding-top: 10em; width: 80%;">
        <div class="row">
        </div>
        <!-- ./col -->
        <div class="col-lg-4 col-xs-8">
            <!-- small box -->
            <div class="small-box bg-light-blue">
                <div class="inner">
                    <h3>1</h3>
                    <p>Carga de Concepto Manual</p>
                </div>
                <div class="icon">
                    <i class="ion ion-android-list"></i>
                </div>
                <a href="novedades.aspx" class="small-box-footer">Ingresar 
                            <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <div class="col-lg-4 col-xs-8">
            <!-- small box -->
            <div class="small-box bg-green">
                <div class="inner">
                    <h3>2<sup style="font-size: 20px"></sup></h3>
                    <p>Carga de Concepto via Excel</p>
                </div>
                <div class="icon">
                    <i class="ion ion-filing"></i>
                </div>
                <a href="novedades2.aspx" class="small-box-footer">Ingresar 
                            <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <div class="col-lg-4 col-xs-8">
            <!-- small box -->
            <div class="small-box bg-orange">
                <div class="inner">
                    <h3>2<sup style="font-size: 20px"></sup></h3>
                    <p>Volver al Menu</p>
                </div>
                <div class="icon">
                    <i class="ion ion-android-arrow-dropleft"></i>
                </div>
                <a href="home.aspx" class="small-box-footer">Ingresar 
                            <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
    </div>
</asp:Content>
