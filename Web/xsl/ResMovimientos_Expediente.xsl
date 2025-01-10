<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format"
xmlns:msxsl="urn:schemas-microsoft-com:xslt"
xmlns:js="urn:custom-javascript">
  <xsl:include href="Functions.xsl" />
  <xsl:template match="/">

    <table id="tblmovimientos" class="table">
      <tr>
        <th>#</th>
        <th align="left">Exp.</th>
        <th align="right">F.Pase</th>
        <th align="center">Paso</th>
        <th align="right">Origen</th>
        <th align="right">Destino</th>
        <th align="right">Estado</th>
        <th align="Left">Obs</th>
        <th align="center">Usuario</th>
        <th align="center">Atendido</th>
        <th align="center">Dias S/Mov.</th>

      </tr>
      <xsl:choose>
        <xsl:when test="//Circuitos/*">
          <xsl:apply-templates select="//Circuito"/>
        </xsl:when>
        <xsl:otherwise>
          <tr>
            <td colspan="7" align="left" font-size="9">
              <b>No hay items en la lista</b>
            </td>
          </tr>
        </xsl:otherwise>
      </xsl:choose>
    </table>

  </xsl:template>

  <xsl:template match="Circuito">
    <tr>
      <td width="1" valign="middle" class=""  font-size="9">
      </td>
      <td align="left" width="10%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="anio"/>-<xsl:value-of select="nro_expediente"/>&#xa0;
      </td>
      <td align="right" width="8%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="fecha_pase"/>&#xa0;
      </td>      
      <td align="center" width="4%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="nro_paso"/>&#xa0;
      </td>
      <td align="left" width="10%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="origen"/>&#xa0;
      </td>
      <td align="right" width="10%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="destino"/>&#xa0;
      </td>
      <td align="right" width="10%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="estado"/>&#xa0;
      </td>
      <td align="left" width="25%" class="rowunderlined"  font-size="9">
        <xsl:value-of select="observaciones"/>&#xa0;
      </td>
      <td align="right" width="5%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="usuario"/>&#xa0;
      </td>
      <td align="center" width="8%" class=""  font-size="9">
        <xsl:if test="atendido[.='True']">
          <img src="../imagenes/accept.gif" align="center" width="14" height="16" border="0"/>&#xa0;
        </xsl:if> 
      </td>
      <td align="right" width="5%" class="" nowrap="1">
        <xsl:value-of select="dias_sin_resolver"/>&#xa0;
      </td>
    
    </tr>

  </xsl:template>
</xsl:stylesheet>
