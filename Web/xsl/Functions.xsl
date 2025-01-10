<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
xmlns:fo="http://www.w3.org/1999/XSL/Format" 
xmlns:msxsl="urn:schemas-microsoft-com:xslt" 
xmlns:js="urn:custom-javascript">
<msxsl:script language="javascript" implements-prefix="js">
<![CDATA[
  var separator = ",";  // use comma as 000's separator
  var decpoint = ".";  // use period as decimal point
  var percent = "%";
  var currency = "$";  // use dollar sign for currency

    function fmtDate(e) {
	/* Desde fechas mdy
		var strDate = e.nextNode.text;
		var ar = strDate.split(" ");
		var mdy = ar[0].split("/");
		var day = mdy[0];
		var month = mdy[1];
		var year = mdy[2];
		var dDate = day + "/" + month + "/" + year ;
		return dDate;
	*/
	// Desde fechas YMD
		var strDate = e.nextNode.text;
		var dDate = "";
		if (strDate.length > 0 )
		{
			var ar = strDate.split(" ");
			var mdy = ar[0].split("-");
			var day = mdy[2];
			var month = mdy[1];
			var year = mdy[0];
			if (year == "1899" || year == "1900")
				dDate = "-";
			else
				dDate = day + "/" + month + "/" + year ;
		}
		else
			dDate = "-";
		return dDate;
    }
    function fmtDateTime(e) {
	// Desde fechas YMD HH:MM
		var strDate = e.nextNode.text;
		var dDate = "";
		if (strDate.length > 0 )
		{
			var ar = strDate.split(" ");
			var mdy = ar[0].split("-");
			var hm = ar[1].split(":");
			var day = mdy[2];
			var month = mdy[1];
			var year = mdy[0];
			var hour = hm[0];
			var minute = hm[1];
			if (year == "1899" || year == "1900")
				dDate = "-";
			else
				dDate = day + "/" + month + "/" + year + " " + hour + ":" + minute;
		}
		else
			dDate = "-";
		return dDate;
    }
    function fmtTime(e) {
		var strDate = e.nextNode.text;
		var ar = strDate.split(" ");
		var hms = ar[1].split(":");
		var hour = hms[0];
		var minute = hms[1];
		var second = hms[2];
		var dHour = hour + ":" + minute;
		return dHour;
    }
    function fmtSecToDay(e) {
		var Secs = e.nextNode.text;
		return Math.round(Secs / 28800 * 100)/100;
    }
	
	function getDateFromYMD(YMDDate){
		var ar = YMDDate.split(" ");
		var mdy = ar[0].split("-");
		var day = mdy[2];
		var month = mdy[1];
		var year = mdy[0];
		var newDate = new Date(parseInt(year,10), parseInt(month,10)-1, parseInt(day,10));
		return newDate;
	}

	function isNullDate(d)
	{
		if (d.getYear() <= 1900)
			return true;
		else
			return false;
	}	
	
	function formatYMD(d)
	{
		if (d.getYear() <= 1900)
			return "-";
		else
			return d.getYear() + '-' + formatNumber(d.getMonth()+1, "00") + '-' + formatNumber(d.getDate(), "00");
	}

	function formatDMY(d)
	{
		if (d.getYear() <= 1900)
			return "-";
		else
			return formatNumber(d.getDate(), "00")  + '/' + formatNumber(d.getMonth()+1, "00") + '/' + d.getYear();
	}
	
    function fmtProjectDate(eJD, eRD) {
	// Desde fechas YMD
		var strJD = eJD.nextNode.text;
		var strRD = eRD.nextNode.text;
		var JD = getDateFromYMD(strJD);
		var RD = getDateFromYMD(strRD);

		if (isNullDate(RD))
			return '<font color="#000099">' + formatDMY(JD) + '</font>';
		else
			return '<font color="#000000"><b>' + formatDMY(RD) + '</b></font>';
    }

    function fmtDelay(eD) {
	// Desde fechas YMD
		var Delay = eD.nextNode.text;

		if (Delay < 0)
			return '<font color="#336600">' + Delay + '</font>';
		else
			return '<font color="#FF0000"><b>' + '-' + Delay + '</b></font>';
    }

    function fmtDeviation2(eJD, ePD) {
	// Desde fechas YMD
		var strJD = eJD.nextNode.text;
		var strPD = ePD.nextNode.text;
		var JD = getDateFromYMD(strJD);
		var PD = getDateFromYMD(strPD);
		var one_day=1000*60*60*24;

		if (!isNullDate(JD))
		{
			var Deviation = Math.ceil( (PD.getTime()-JD.getTime())/(one_day) );
			if (Deviation >= 0)
				return '<font color="#336600">' + Deviation + '</font>';
			else
				return '<font color="#FF0000"><b>' + Deviation + '</b></font>';
			}
		else
			return "-";
    }

    function fmtDeviation(Deviation) {

		if (Deviation != "-")
		{
			if (Deviation >= 0)
				return '<font color="#336600">' + Deviation + '</font>';
			else
				return '<font color="#FF0000"><b>' + Deviation + '</b></font>';
		}
		else
			return '-';
    }

	function HasDeviation(eJD, ePD, eRD)
	{
		var Deviation = GetDeviation(eJD, ePD);
		if (Deviation > 0 || Deviation < 0)
			return "true";
		else
			return "false";
	}
	
	function GetDeviation(eJD, ePD)
	{
		var strJD = eJD.nextNode.text;
		var strPD = ePD.nextNode.text;
		var JD = getDateFromYMD(strJD);
		var PD = getDateFromYMD(strPD);
		var one_day=1000*60*60*24;

		if (!isNullDate(JD))
			var Deviation = Math.ceil( (PD.getTime()-JD.getTime())/(one_day) );
		else
			var Deviation = "-";
		return Deviation;
	}

    function Percent(e) {
		var Coef = e.nextNode.text;
		return (Coef * 100);
    }

    function getExtras(nPresenceNode, nNormalNode) {
		var nPresence = parseFloat(nPresenceNode.nextNode.text);
		var nNormal = parseFloat(nNormalNode.nextNode.text);
                var nExtras = 0;		
		if (nPresence > nNormal)
			nExtras = nPresence - nNormal;
		else
			nExtras = 0;
		return nExtras;
    }	
    function incompleteDedication(nPresenceNode, nDedicationNode) {
		var nPresence = parseFloat(nPresenceNode.nextNode.text);
		var nDedication = parseFloat(nDedicationNode.nextNode.text);
		return (nPresence != nDedication);
    }	
    function showNullNumber(nNumber, sFormat) {
		if (parseFloat(nNumber) == 0)
			return '';
		else
			return formatNumber(parseFloat(nNumber), sFormat);
    }
	
	function IsNull(e, r)
	{
		if (e)
		{
			if (e.nextNode.text.length == 0)
				return r;
			else
				return e;
		}
		else
			return r;
	}

  function formatNumber(number, format, print) {  // use: formatNumber(number, "format")
    //if (print) document.write("formatNumber(" + number + ", \"" + format + "\")<br>");

    if (number - 0 != number) return null;  // if number is NaN return null
    var useSeparator = format.indexOf(separator) != -1;  // use separators in number
    var usePercent = format.indexOf(percent) != -1;  // convert output to percentage
    var useCurrency = format.indexOf(currency) != -1;  // use currency format
    var isNegative = (number < 0);
    number = Math.abs (number);
    if (usePercent) number *= 100;
    format = strip(format, separator + percent + currency);  // remove key characters
    number = "" + number;  // convert number input to string

     // split input value into LHS and RHS using decpoint as divider
    var dec = number.indexOf(decpoint) != -1;
    var nleftEnd = (dec) ? number.substring(0, number.indexOf(".")) : number;
    var nrightEnd = (dec) ? number.substring(number.indexOf(".") + 1) : "";

     // split format string into LHS and RHS using decpoint as divider
    dec = format.indexOf(decpoint) != -1;
    var sleftEnd = (dec) ? format.substring(0, format.indexOf(".")) : format;
    var srightEnd = (dec) ? format.substring(format.indexOf(".") + 1) : "";

     // adjust decimal places by cropping or adding zeros to LHS of number
    if (srightEnd.length < nrightEnd.length) {
      var nextChar = nrightEnd.charAt(srightEnd.length) - 0;
      nrightEnd = nrightEnd.substring(0, srightEnd.length);
      if (nextChar >= 5) nrightEnd = "" + ((nrightEnd - 0) + 1);  // round up

 // patch provided by Patti Marcoux 1999/08/06
      while (srightEnd.length > nrightEnd.length) {
        nrightEnd = "0" + nrightEnd;
      }

      if (srightEnd.length < nrightEnd.length) {
        nrightEnd = nrightEnd.substring(1);
        nleftEnd = (nleftEnd - 0) + 1;
      }
    } else {
      for (var i=nrightEnd.length; srightEnd.length > nrightEnd.length; i++) {
        if (srightEnd.charAt(i) == "0") nrightEnd += "0";  // append zero to RHS of number
        else break;
      }
    }

     // adjust leading zeros
    sleftEnd = strip(sleftEnd, "#");  // remove hashes from LHS of format
    while (sleftEnd.length > nleftEnd.length) {
      nleftEnd = "0" + nleftEnd;  // prepend zero to LHS of number
    }

    if (useSeparator) nleftEnd = separate(nleftEnd, separator);  // add separator
    var output = nleftEnd + ((nrightEnd != "") ? "." + nrightEnd : "");  // combine parts
    output = ((useCurrency) ? currency : "") + output + ((usePercent) ? percent : "");
    if (isNegative) {
      // patch suggested by Tom Denn 25/4/2001
      output = (useCurrency) ? "(" + output + ")" : "-" + output;
    }
    return output;
  }

  function strip(input, chars) {  // strip all characters in 'chars' from input
    var output = "";  // initialise output string
    for (var i=0; i < input.length; i++)
      if (chars.indexOf(input.charAt(i)) == -1)
        output += input.charAt(i);
    return output;
  }

  function separate(input, separator) {  // format input using 'separator' to mark 000's
    input = "" + input;
    var output = "";  // initialise output string
    for (var i=0; i < input.length; i++) {
      if (i != 0 && (input.length - i) % 3 == 0) output += separator;
      output += input.charAt(i);
    }
    return output;
  }
 
  function WeekDay(dateFormat, nDay){
   var day = parseInt(nDay.nextNode.text);
   if(isNaN(day))
      return "N/D";
   var weekday_array_l = new Array("Lunes","Martes","Miércoles","Jueves","Viernes","Sábado","Domingo");
   var weekday_array_s = new Array("Lun","Mar","Mie","Jue","Vie","Sáb","Dom");

   if (dateFormat == 'short')
      return weekday_array_s[day-1];
   else
      return weekday_array_l[day-1];
   }		

  function GetNormalHours(TotalHours, AvailableHours){
   var TH = parseFloat(TotalHours); 
   var AH = parseFloat(AvailableHours); 
   if (TH > AH)
      return AH;
   else
      return TH;
   }		

  function GetExtraHours(TotalHours, AvailableHours){
   var TH = parseFloat(TotalHours); 
   var AH = parseFloat(AvailableHours); 
   if (TH > AH)
      return TH-AH;
   else
      return 0;
   }		

]]>
</msxsl:script>
</xsl:stylesheet> 