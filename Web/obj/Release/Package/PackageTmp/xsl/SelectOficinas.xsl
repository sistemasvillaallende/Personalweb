<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/">
		<xsl:for-each select="Oficinas/Oficina">
			<option><xsl:attribute name="value"><xsl:value-of select="codigo_oficina"/></xsl:attribute><xsl:value-of select="nombre_oficina"/></option>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>