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
        <th align="left">Legajo</th>
        <th align="right">Nombre</th>
        <th align="center">Año</th>
        <th align="center">Periodo</th>
        <th align="right">Des Liq</th>
        <th align="right">Cargo</th>
        <th align="right">Tarea</th>
        <th align="right">Importe</th>



      </tr>
      <xsl:choose>
        <xsl:when test="//Datos/*">
          <xsl:apply-templates select="//Dato"/>
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

  <xsl:template match="Dato">
    <tr>
      <td align="left" width="4%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="legajo"/>&#xa0;
      </td>
      <td align="left" width="10%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="nombre"/>&#xa0;
      </td>
      <td align="left" width="4%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="anio"/>&#xa0;
      </td>
      <td align="left" width="5%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="periodo"/>&#xa0;
      </td>
      <td align="right" width="15%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="des_liquidacion"/>&#xa0;
      </td>
      <td align="right" width="15%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="desc_cargo"/>&#xa0;
      </td>
      <td align="right" width="15%" class="rowunderlined"  font-size="9">
        <xsl:value-of select="tarea"/>&#xa0;
      </td>
      <td align="right" width="6%" class="" nowrap="1"  font-size="9">
        <xsl:value-of select="importe"/>&#xa0;
      </td>

    </tr>

  </xsl:template>
</xsl:stylesheet>
