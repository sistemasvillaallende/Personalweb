</asp:Content>
</div>
</div>
</div>
</div>
</div>
</asp:UpdatePanel>
</ContentTemplate>
</div> <br /> </div>
</div> </button> <span class="glyphicon glyphicon glyphicon-new-window"></span>&nbsp;Volver <button type="button"
    id="cmdVolver4" runat="server" onserverclick="cmdVolver4_ServerClick" class="btn-control volver">
    <div class="col-md-12" style="text-align: right;"> <br />
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <asp:ValidationSummary ID="ValidationDatosParticulares" runat="server" ValidationGroup="ValidationDatosParticulares"
        ForeColor="Red" />
    <div class="col-sm-10" style="padding-left: 0px;">
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Email:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtCelular" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Celular:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Telefono:</label>
    <div class="col-md-4">
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <asp:TextBox ID="txtMonoBlock" CssClass="form-control" runat="server" ReadOnly="true" /> <label>MonoBlock:</label>
    <div class="col-md-2"> </div>
    <asp:TextBox ID="txtDpto" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Dpto:</label>
    <div class="col-md-2"> </div>
    <asp:TextBox ID="txtPiso" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Piso:</label>
    <div class="col-md-2">
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <asp:TextBox ID="txtCPostal" CssClass="form-control" runat="server" ReadOnly="true" /> <label>C.Postal:</label>
    <div class="col-md-2"> </div>
    <asp:TextBox ID="txtNro_domicilio" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Nro:</label>
    <div class="col-md-2"> </div>
    <asp:TextBox ID="txtCalle" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Calle:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtBarrio" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Barrio:</label>
    <div class="col-md-4">
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <asp:TextBox ID="txtCiudad" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Ciudad
        Domicilio:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtProvincia" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Provincia:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtPais" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Pais:</label>
    <div class="col-md-4">
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <label>Estado Civil:</label>
    <div class="col-md-4"> </div>
    <asp:DropDownList ID="ddEstadoCivil" CssClass="form-control dropdown-arrow" runat="server"
        AppendDataBoundItems="True" Enabled="false">
        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddSexo" CssClass="form-control dropdown-arrow" runat="server" AppendDataBoundItems="True"
        Enabled="false">
        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
    </asp:DropDownList>
    <label>Sexo:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtFecha_nacimiento" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Fecha
        Nacimiento:</label>
    <div class="col-md-4">
        <div class="row" style="margin-bottom: 25px;">
            <div class="panel-body" style="padding-left: 15px; padding-right: 15px;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="tab-pane fade" id="tab_Datos_Particulares" role="tabpanel"
                            aria-labelledby="tab_Datos_Particulares-tab">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
            </div>
        </div>
    </div>
</button> <span class="glyphicon glyphicon glyphicon-new-window"></span>&nbsp;Volver <button type="button"
    id="cmdVolver3" runat="server" onserverclick="cmdVolver3_ServerClick" class="btn-control volver">
    <div class="col-md-12" style="text-align: right;"> <br />
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <asp:ValidationSummary ID="ValidationDatos_Bco" runat="server" ValidationGroup="ValidationDatos_Bco"
        ForeColor="Red" />
    <div class="col-sm-10" style="padding-left: 0px;">
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <asp:TextBox ID="txtCbu" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Nro CBU:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtNro_caja_ahorro" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Nro Caja
        Ahorro:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtNro_sucursal" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Nro
        Sucursal:</label>
    <div class="col-md-4">
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <label>Tipo Cuenta:</label>
    <asp:DropDownList ID="ddTipo_cuenta" CssClass="form-control dropdown-arrow" runat="server"
        AppendDataBoundItems="True" Enabled="false">
        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
    </asp:DropDownList>
    <label>Banco:</label>
    <asp:DropDownList ID="ddBanco" CssClass="form-control dropdown-arrow" runat="server" AppendDataBoundItems="True"
        Enabled="false">
        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
    </asp:DropDownList>
    <div class="col-md-6">
        <div class="row" style="margin-bottom: 25px;">
            <div class="container">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="tab-pane fade" id="tab_Datos_Banco" role="tabpanel"
                            aria-labelledby="tab_Datos_Banco-tab">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
            </div>
        </div>
    </div>
</button> <span class="glyphicon glyphicon glyphicon-new-window"></span>&nbsp;Volver <button type="button"
    id="cmdVolver2" runat="server" onserverclick="cmdVolver2_ServerClick" class="btn-control volver">
    <div class="col-md-12" style="text-align: right;"> <br />
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <asp:ValidationSummary ID="ValidationDatos_ObSocial" runat="server" ValidationGroup="ValidationDatos_ObSocial"
        ForeColor="Red" />
    <div class="col-sm-10" style="padding-left: 0px;">
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <asp:TextBox ID="txtFecha_nombramiento" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Fecha
        Nombramiento:</label>
    <div class="col-md-6"> </div>
    <asp:TextBox ID="txtNro_nombramiento" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Nro
        Nombramiento:</label>
    <div class="col-md-6">
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <asp:TextBox ID="txtFecha_fin_contrato" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Fecha Fin
        Contrato:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtFecha_inicio_contrato" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Fecha
        Inicio Contrato:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtNro_contrato" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Nro
        Contrato:</label>
    <div class="col-md-4">
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <asp:TextBox ID="txtAnt_actual" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Antiguedad
        Actual:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtAnt_anterior" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Antiguedad
        Anterior:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtNro_jubilacion" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Nro
        Jubilacion:</label>
    <div class="col-md-4"> </div>
    <asp:TextBox ID="txtNro_obra_social" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Nro Afiliado
        Obra Social:</label>
    <div class="col-md-4">
        <div class="row" style="margin-bottom: 25px;">
            <div class="panel-body">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="tab-pane fade" style="min-height: 300px;" id="tab_Datos_Contrato" role="tabpanel"
                            aria-labelledby="tab_Datos_Contrato-tab">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br /> <br />
            </div>
        </div>
    </div>
</button> <span class="glyphicon glyphicon-new-window"></span>&nbsp;Volver <button type="button" id="Button3"
    runat="server" onserverclick="cmdVolver4_ServerClick" class="btn-control volver">
    <div class="col-md-12" style="text-align: right;"> <br />
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    <asp:ValidationSummary ID="ValidationDatos_empleado" runat="server" ValidationGroup="ValidationDatos_empleado"
        ForeColor="Red" />
    <div class="col-sm-10" style="padding-left: 0px;">
        <div class="row" style="margin-bottom: 25px;"> </div>
    </div>
    </div> </label> <span class="ml-3">Imprime Recibo?</span>
    <asp:CheckBox ID="chkImprime" runat="server" Enabled="false" /> <label>
        <div class="form-group col-md-6">
            <div class="col-md-8 mt-4"> </div>
            <asp:TextBox ID="txtFecha_baja" CssClass="form-control" runat="server" ReadOnly="true" /> <label>Fecha
                Baja:</label>
            <div class="col-md-4">
                <div class="row" style="margin-bottom: 25px;"> </div>
            </div>
    </label> <span class="ml-3"> Activo?</span>
    <asp:CheckBox ID="ChkActivo" runat="server" Enabled="false" /> <label>
        <div class="col-md-4 pt-4"> </div>
        </asp:DropDownList>
        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
        <asp:DropDownList ID="ddCategoriaProfesional" CssClass="form-control dropdown-arrow" runat="server"
            AppendDataBoundItems="True" Visible="false" Enabled="false">
        </asp:DropDownList>
        <label ID="lblCategoriaProfesional" runat="server" Visible="false">
            Categoría Profesional Monotributo
        </label>
        <asp:TextBox ID="txtFecha_revista" CssClass="form-control" runat="server" ReadOnly="true" />
        <label>Fecha de Revista:</label>
        <div class="col-md-4"> </div>
        </asp:DropDownList>
        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
        <label>Situacion de Revista:</label>
        <asp:DropDownList ID="ddRevista" CssClass="form-control dropdown-arrow" runat="server"
            AppendDataBoundItems="True" Enabled="false">
        </asp:DropDownList>
        <div class="col-md-4">
            <div class="row" style="margin-bottom: 25px;"> </div>
        </div>
        </asp:DropDownList>
        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
        <label>Escala Aumento:</label>
        <asp:DropDownList ID="ddEscala" CssClass="form-control dropdown-arrow" runat="server"
            AppendDataBoundItems="True" Enabled="false">
            <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
        </asp:DropDownList>
        <div class="col-md-6"> </div>