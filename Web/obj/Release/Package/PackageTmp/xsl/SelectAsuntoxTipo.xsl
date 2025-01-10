<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/">
    <xsl:for-each select="Oficinas/Oficina">
      <option>

        
        <xsl:attribute name="value">
          <xsl:value-of select="cod_asunto"/>
        </xsl:attribute>

        <xsl:value-of select="cod_asunto"/>
        <xsl:value-of select="descripcion_asunto"/>


        
        
        <xsl:attribute name="value">
          <xsl:value-of select="cod_tipo_tramite"/>
        </xsl:attribute>

        <xsl:value-of select="0"/>
        <xsl:text> .... </xsl:text>
        <xsl:value-of select="descripcion_tramite"/>




      </option>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>





<!--<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/">
    <xsl:for-each select="Bancos/Banco">
      <option>
        <xsl:attribute name="value">
          <xsl:value-of select="cod_banco"/>
        </xsl:attribute>
        
        <xsl:value-of select="cod_banco"/>
        <xsl:text> - </xsl:text>
        <xsl:value-of select="nom_banco"/>
      </option>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>-->



<!--<xsl:choose>
  <xsl:when test="cod_tipo_tramite=1">

  </xsl:when>
  <xsl:otherwise>

  </xsl:otherwise>
</xsl:choose>

-->
<!--<xsl:if test="">
          <img src="../images/accept.gif" align="center" width="14" height="16" border="0"/>
        </xsl:if> -->





<!--<xsl:choose>
  <xsl:when test='$level=1'>
    <xsl:number format="i"/>
  </xsl:when>
  <xsl:when test='$level=2'>
    <xsl:number format="a"/>
  </xsl:when>
  <xsl:otherwise>
    <xsl:number format="1"/>
  </xsl:otherwise>
</xsl:choose>-->
