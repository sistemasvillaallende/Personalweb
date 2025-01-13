<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Web.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sueldos Web - Inicio de Sesion</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport' />
    <!-- Bootstrap 3.3.4 -->
    <link href="App_Themes/Bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome Icons -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="styles/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- iCheck -->
    <link href="styles/skin-blue.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .sombra {
            -webkit-box-shadow: 10px 10px 5px 0px rgba(0,0,0,0.75);
            -moz-box-shadow: 10px 10px 5px 0px rgba(0,0,0,0.75);
            box-shadow: 10px 10px 5px 0px rgba(0,0,0,0.75);
            border-radius: 10px 10px 10px 10px;
            -moz-border-radius: 10px 10px 10px 10px;
            -webkit-border-radius: 10px 10px 10px 10px;
            border: 0px solid #000000;
        }
    </style>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->






</head>
<body class="login-page">
    <form id="form1" runat="server">

        <script type="text/javascript">

</script>

        <div class="login-box">

            <div class="login-logo">
                <a href="#" style="color: #3C8DBC;"><b>Sueldos</b> Web</a>
            </div>
            <!-- /.login-logo -->
            <div class="box box-warning" id="divError" runat="server" visible="false">
                <div class="box-header">
                </div>
                <div class="box-body">
                    <div class="alert alert-dismissible" role="alert" style="border-color: red;">
                        <strong style="font-size: 18px;">Sr. Usuario:</strong><br />
                        <p style="font-size: 16px;" id="lblError" runat="server"></p>
                        <div style="text-align: center; padding-top: 30px;">
                            <asp:Button ID="btnOkError" CssClass="btn btn-danger" OnClick="btnOkError_Click"
                                runat="server" Text="Aceptar" />
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                </div>
            </div>
            <div id="divLogIn" class="login-box-body sombra" runat="server">
                <h2 class="login-box-msg">Inicie su Sesion</h2>
                <div class="form-group">
                    <input type="text" runat="server" class="form-control" placeholder="Nombre de Usuario" id="txtUsuario" />
                    <%--<span class="glyphicon glyphicon-envelope form-control-feedback"></span>--%>
                </div>
                <div class="form-group">
                    <input type="password" class="form-control" placeholder="Contraseña" id="txtPass" runat="server" />
                    <%--<span class="glyphicon glyphicon-lock form-control-feedback"></span>--%>
                </div>
                <br />
                <%--<div class="form-goup">
                    <label>Selecione Base de Datos</label>
                        <asp:DropDownList ID="DDLServidor" CssClass="form-control" runat="server">
                            <asp:ListItem Text="Base Produccion" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Base de Prueba" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                </div>--%>
                <div class="row">
                    <div class="col-xs-8">
                    </div>
                    <!-- /.col -->
                    <div class="col-xs-4">
                        <button type="button" runat="server" id="btnIngresar" onserverclick="btnIngresar_ServerClick" class="btn btn-primary btn-block btn-flat">Ingresar</button>
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.social-auth-links -->
            </div>
            <!-- /.login-box-body -->
        </div>
        <!-- /.login-box -->

        <!-- jQuery 2.1.4 -->
        <script src="js/jQuery-2.1.4.min.js"></script>

        <!-- Bootstrap 3.3.2 JS -->
        <script src="App_Themes/Bootstrap/js/bootbox.min.js" type="text/javascript"></script>
        <!-- iCheck -->
        <script src="js/icheck.js" type="text/javascript"></script>
        <script>
            $(function () {
                $('input').iCheck({
                    checkboxClass: 'icheckbox_square-blue',
                    radioClass: 'iradio_square-blue',
                    increaseArea: '20%' // optional
                });
            });
        </script>
    </form>
</body>
</html>