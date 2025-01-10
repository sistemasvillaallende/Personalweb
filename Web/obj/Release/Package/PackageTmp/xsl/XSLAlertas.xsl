<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format"
xmlns:msxsl="urn:schemas-microsoft-com:xslt"
xmlns:js="urn:custom-javascript">
  <xsl:include href="Functions.xsl" />
  <xsl:template match="/">

    <table id="tblmovimientos" class="grid">
      <tr>
        <th>#</th>
        <th align="left">Nº Exp</th>
        <th align="right">Nº Paso</th>
        <th align="left">Tipo</th>
        <th align="left">Asunto</th>
        <th align="right">Fec Pase</th>
        <th align="right">Destino</th>
        <th align="Left">Estado</th>
        <th align="center">Observaciones</th>
        <th align="center">Dias</th>
      </tr>
      <xsl:choose>
        <xsl:when test="//alertas/*">
          <xsl:apply-templates select="//alerta"/>
        </xsl:when>
        <xsl:otherwise>
          <tr>
            <td colspan="7" align="left">
              <b>No hay items en la lista</b>
            </td>
          </tr>
        </xsl:otherwise>
      </xsl:choose>
    </table>

  </xsl:template>

  <xsl:template match="alerta">
    <tr>
      <td width="1%" valign="middle" class="">
      </td>
      <td align="left" width="9%" class="" nowrap="1" >
        <xsl:value-of select="nro_expediente"/>-<xsl:value-of select="anio"/>&#xa0;
      </td>
      <td align="center" width="4%" class="" nowrap="1">
        <xsl:value-of select="nro_paso"/>&#xa0;
      </td>
      <td align="center" width="4%" class="" nowrap="1">
        <xsl:value-of select="tipo"/>&#xa0;
      </td>
      <td style="height: auto; width:14;" align="left">
        <xsl:value-of select="asunto"/>&#xa0;
      </td>
      <td align="left" width="9%" class="" nowrap="1">
        <xsl:value-of select="fecha_pase"/>&#xa0;
      </td>
      <td style="height: auto; width:9;" align="left">
        <xsl:value-of select="destino"/>&#xa0;
      </td>
      <td align="right" width="8%" class="" nowrap="1">
        <xsl:value-of select="estado"/>&#xa0;
      </td>
      <td style="height: auto; width:20;" align="left" >
        <xsl:value-of select="observaciones"/>&#xa0;
      </td>
      <td align="right" width="5%" class="" nowrap="1">
        <xsl:value-of select="dias"/>&#xa0;
      </td>
    </tr>

  </xsl:template>
</xsl:stylesheet>
