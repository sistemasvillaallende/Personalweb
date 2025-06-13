<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="cargar_conceptos.aspx.cs" Inherits="web.secure.cargar_conceptos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../styles/AdminLTE.min.css" rel="stylesheet" />

     <style type="text/css">
        .textoBlanco{
            color: azure;
        }
    </style>
  
</asp:Content>

 

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="container py-5">
    <div class="row justify-content-center g-4">

      <!-- Tarjeta 1 -->
      <div class="col-md-6 col-lg-4">
        <div class="card shadow h-100 border-0 bg-primary text-white text-center">
          <div class="card-body d-flex flex-column justify-content-center">
            <div class="mb-3">
              <i class="ion ion-android-list fs-1"></i>
            </div>
            <p class="card-text textoBlanco">Carga de Concepto Manual</p>
          </div>
          <div class="card-footer bg-transparent border-0">
            <a href="novedades.aspx" class="btn btn-light w-100">
              Ingresar <i class="fa fa-arrow-circle-right ms-2"></i>
            </a>
          </div>
        </div>
      </div>

      <!-- Tarjeta 2 -->
      <div class="col-md-6 col-lg-4">
        <div class="card shadow h-100 border-0 bg-success text-white text-center">
          <div class="card-body d-flex flex-column justify-content-center">
            <div class="mb-3">
              <i class="ion ion-filing fs-1"></i>
            </div>
            <p class="card-text textoBlanco">Carga de Concepto vía Excel</p>
          </div>
          <div class="card-footer bg-transparent border-0">
            <a href="novedades2.aspx" class="btn btn-light w-100">
              Ingresar <i class="fa fa-arrow-circle-right ms-2"></i>
            </a>
          </div>
        </div>
      </div>

      <!-- Tarjeta 3 -->
      <div class="col-md-6 col-lg-4">
        <div class="card shadow h-100 border-0 bg-warning text-dark text-center">
          <div class="card-body d-flex flex-column justify-content-center">
            <div class="mb-3">
              <i class="ion ion-android-arrow-dropleft fs-1"></i>
            </div>            
            <p class="card-text textoBlanco">Volver al Menú</p>
          </div>
          <div class="card-footer bg-transparent border-0">
            <a href="home.aspx" class="btn btn-dark w-100">
              Ingresar <i class="fa fa-arrow-circle-right ms-2"></i>
            </a>
          </div>
        </div>
      </div>

    </div>
  </div>
</asp:Content>

