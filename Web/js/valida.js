var cEMPTY = "Empty";
var cNONE_SELECTED = "NoneSelected";
var cNUMBER = "Number";
var cDATE = "Date";
var cMAIL = "Mail";
var cLEN_OK = "LenOK";
var cHOUR = "Hour";


function isValidMail(entered, alertbox)
{
	// */*/*/*/*/*/*/*/*/*/*/* Funciones JavaScript RD */*/*/*/*/*/*/*/*/*/*/*
	strRegExp= /^[\w\-\.]+@[a-z0-9]+[\-]?[a-z0-9]+((\.(com|net|org|edu|int|mil|gov))|(\.(com|net|org|edu|int|mil|gov)\.[a-z]{2})|(\.[a-z]{2}))$/;
	var valor=entered.value;
	if (!strRegExp.test(valor))
	{
		if (alertbox) 
			{alert(alertbox); return false;}
	}
	else {return true;}
}
/*function isValidName(entered, alertbox)
{
  var checkOK = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-_ ·ÈÌÛ˙A…Õ⁄”Á«„√ı’¸‹‡¿Í ";
  var checkStr = entered.value;
  var allValid = true;
  var decPoints = 0;
  var allNum = "";
  for (i = 0;  i < checkStr.length;  i++)
  {
    ch = checkStr.charAt(i);
    for (j = 0;  j < checkOK.length;  j++)
      if (ch == checkOK.charAt(j))
        break;
    if (j == checkOK.length)
    {
      allValid = false;
      break;
    }
    allNum += ch;
  }
  if (!allValid)
  {
	if (alertbox!="") 
	{
		alert(alertbox);
	} 
    return (false);
  }
  else 
  {
	return true;
  }

}*/

/*function isValidFullName(entered, alertbox)
{
  var checkOK = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_ ·ÈÌÛ˙A…Õ⁄”Á«„√ı’¸‹‡¿Í ";
  var checkStr = entered.value;
  var allValid = true;
  var decPoints = 0;
  var allNum = "";
  for (i = 0;  i < checkStr.length;  i++)
  {
    ch = checkStr.charAt(i);
    for (j = 0;  j < checkOK.length;  j++)
      if (ch == checkOK.charAt(j))
        break;
    if (j == checkOK.length)
    {
      allValid = false;
      break;
    }
    allNum += ch;
  }
  if (!allValid)
  {
	if (alertbox!="") 
	{
		alert(alertbox);
	} 
    return (false);
  }
  else 
  {
	return true;
  }

}*/

function isValidNumber(entered, alertbox)
{
  var checkOK = "1234567890.,";
  var checkStr = entered.value;
  var allValid = true;
  var decPoints = 0;
  var allNum = "";
  for (i = 0;  i < checkStr.length;  i++)
  {
    ch = checkStr.charAt(i);
    for (j = 0;  j < checkOK.length;  j++)
      if (ch == checkOK.charAt(j))
        break;
    if (j == checkOK.length)
    {
      allValid = false;
      break;
    }
    allNum += ch;
  }
  if (!allValid)
  {
	if (alertbox!="") 
	{
		alert(alertbox);
	} 
    return (false);
  }
  else 
  {
	return true;
  }

}


function isEmpty(entered, alertbox)
{
	with (entered)
	{
		if (value==null || value=="")
		{
			if (alertbox!="") 
			{
				alert(alertbox);
			} 
			return true;
		}
		else 
		{
			return false;
		}
	}
}

/*function isValidAlfaNum(entered, alertbox)
{
  var checkOK = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890\.\,-_ ·ÈÌÛ˙A…Õ⁄”Á«„√ı’¸‹‡¿Í ";
  var checkStr = entered.value;
  var allValid = true;
  var decPoints = 0;
  var allNum = "";
  for (i = 0;  i < checkStr.length;  i++)
  {
    ch = checkStr.charAt(i);
    for (j = 0;  j < checkOK.length;  j++)
      if (ch == checkOK.charAt(j))
        break;
    if (j == checkOK.length)
    {
      allValid = false;
      break;
    }
    allNum += ch;
  }
  if (!allValid)
  {
	if (alertbox!="") 
	{
		alert(alertbox);
	} 
    return (false);
  }
  else 
  {
	return true;
  }

}*/

function isNoneSelected(entered, nullVal, alertbox)
{
  if (entered.value == nullVal)
  {
    if (alertbox!="") 
    {
	alert(alertbox);
    }
    return (true);  
  }
  else
    return false;
}

//Validador de campos, de diferentes caracteristicas
function ckField(entered, strOption, focusObject, fieldDesc)
{
  if (fieldDesc==null || fieldDesc=="")
    fieldDesc = entered.name;

  //Validacion de vacio
  if ( strOption.search("nonull") != -1 )
  {
    if ( isEmpty(entered,fieldDesc + ": debe ingresar un valor en el campo") )
    {
      if (focusObject) 
        entered.focus();
      return false;
    }
  }
  //Validacion de alfanumerico
  if ( strOption.search("alphanum") != -1 )
  {
    if ( ! isValidAlfaNum(entered,fieldDesc + ": debe ingresar un contenido correcto en el campo" ) )
    {
      if (focusObject)
        entered.focus();
      return false;
    }
  }
  //Validacion de no seleccionado valor 0
  if ( strOption.search("nozeroselected") != -1 )
  {
    if ( isNoneSelected(entered, "0", fieldDesc + ": debe seleccionar un valor para el campo" ) )
    {
      if (focusObject)
        entered.focus();
      return false;
    }
  }
  //Validacion de e-mail correcto
  if ( strOption.search("validmail") != -1 )
  {
    if ( ! isValidMail(entered,fieldDesc + ": debe seleccionar un valor para el campo" ) )
    {
      if (focusObject)
        entered.focus();
      return false;
    }
  }
  //Validacion de numerico
  if ( strOption.search("number") != -1 )
  {
    if ( ! isValidNumber(entered,fieldDesc + ": debe ingresar un contenido correcto en el campo" ) )
    {
      if (focusObject)
        entered.focus();
      return false;
    }
  }
  return true;
}

function validRequired(formField,fieldLabel)
{
	var result = true;
	
	if (formField.value == "")
	{
		alert('Please enter a value for the "' + fieldLabel +'" field.');
		formField.focus();
		result = false;
	}
	
	return result;
}


function allDigits(str)
{
	return inValidCharSet(str,"0123456789");
}

function inValidCharSet(str,charset)
{
	var result = true;
	
	for (var i=0;i<str.length;i++)
		if (charset.indexOf(str.substr(i,1))<0)
		{
			result = false;
			break;
		}
	
	return result;
}

function isValidExpDate(formField,fieldLabel,required)
{
	var result = true;
	var formValue = formField.value;

	if (required && !validRequired(formField,fieldLabel))
		result = false;
  
 	if (result && (formField.value.length>0))
 	{
 		var elems = formValue.split("/");
 		
 		result = (elems.length == 2); // should be two components
 		var expired = false;

 		if (result)
 		{
 			var month = parseInt(elems[0],10);
 			var year = parseInt(elems[1],10);
 			
 			//var month = elems[0];
 			//var year = elems[1];

 			if (elems[1].length == 2)
 				year += 2000;
 			
 			var now = new Date();
 			
 			var nowMonth = now.getMonth() + 1;
 			var nowYear = now.getFullYear();
 			
 			expired = (nowYear > year) || ((nowYear == year ) && (nowMonth > month));
			result = allDigits(elems[0]) && (month > 0) && (month < 13) &&
					 allDigits(elems[1]) && ((elems[1].length == 2) || (elems[1].length == 4));
 		}
 		
  		if (!result)
 		{
 			alert(fieldLabel + ': Por favor, ingrese una fecha en formato MM/AA');
			formField.focus();
		}
		else if (expired)
		{
 			result = false;
 			alert(fieldLabel + ': La fecha ha expirado');
			formField.focus();
		}
	} 
	
	return result;
}

function isValidCreditCardNumber(formField,ccType,fieldLabel,required)
{
	var result = true;
 	var ccNum = formField.value;

	if (required && !validRequired(formField,fieldLabel))
		result = false;
 
  	if (result && (formField.value.length>0))
 	{ 
 		if (!allDigits(ccNum))
 		{
 			alert(fieldLabel + ': Por favor, ingrese solo n˙meros');
			formField.focus();
			result = false;
		}	

		if (result)
 		{ 
 			
 			if (!LuhnCheck(ccNum) || !validateCCNum(ccType,ccNum))
 			{
 				alert(fieldLabel + ': Por favor, ingrese un n˙mero de tarjeta v·lido');
				formField.focus();
				result = false;
			}	
		} 

	} 
	
	return result;
}

function LuhnCheck(str) 
{
  var result = true;

  var sum = 0; 
  var mul = 1; 
  var strLen = str.length;
  
  for (i = 0; i < strLen; i++) 
  {
    var digit = str.substring(strLen-i-1,strLen-i);
    var tproduct = parseInt(digit ,10)*mul;
    if (tproduct >= 10)
      sum += (tproduct % 10) + 1;
    else
      sum += tproduct;
    if (mul == 1)
      mul++;
    else
      mul--;
  }
  if ((sum % 10) != 0)
    result = false;
    
  return result;
}



function GetRadioValue(rArray)
{
	for (var i=0;i<rArray.length;i++)
	{
		if (rArray[i].checked)
			return rArray[i].value;
	}
	
	return null;
}


function validateCCNum(cardType,cardNum)
{
	var result = false;
	cardType = cardType.toUpperCase();
	
	var cardLen = cardNum.length;
	var firstdig = cardNum.substring(0,1);
	var seconddig = cardNum.substring(1,2);
	var first4digs = cardNum.substring(0,4);

	switch (cardType)
	{
		case "VISA":
			result = ((cardLen == 16) || (cardLen == 13)) && (firstdig == "4");
			break;
		case "AMEX":
			var validNums = "47";
			result = (cardLen == 15) && (firstdig == "3") && (validNums.indexOf(seconddig)>=0);
			break;
		case "MASTERCARD":
			var validNums = "12345";
			result = (cardLen == 16) && (firstdig == "5") && (validNums.indexOf(seconddig)>=0);
			break;
		case "DISCOVER":
			result = (cardLen == 16) && (first4digs == "6011");
			break;
		case "DINERS":
			var validNums = "068";
			result = (cardLen == 14) && (firstdig == "3") && (validNums.indexOf(seconddig)>=0);
			break;
	}
	return result;
}

function validCCForm(ccTypeField,ccNumField,ccExpField)
{
	var result = isValidCreditCardNumber(ccNumField,ccTypeField.value,"Nro. Tarjeta",true) &&
		isValidExpDate(ccExpField,"Vto. Tarjeta",true);
	return result;
}

function isLenOK(entered, alertbox, MaxChars) {
	with (entered)
	{
		if (entered.value.length > MaxChars)
		{
			if (alertbox!="") 
			{
				alert(alertbox + '\n' + (entered.value.length-MaxChars) + ' caracteres de m·s.');
			} 
			return false;
		}
		else 
		{
			return true;
		}
	}
}

function isValidDate(entered, alertbox) {
	with (entered)
	{
		if (!isDate(entered.value))
		{
			if (alertbox!="") 
			{
				alert(alertbox);
			} 
			return false;
		}
		else 
		{
			return true;
		}
	}
}

function isValidHour(entered, alertbox) {
	with (entered)
	{
		if (!isHour(entered.value))
		{
			if (alertbox!="") 
			{
				alert(alertbox);
			} 
			return false;
		}
		else 
		{
			return true;
		}
	}
}

function isDate(dateStr) {

var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
var matchArray = dateStr.match(datePat); // is the format ok?

	if (matchArray == null) {
//		alert("Please enter date as either mm/dd/yyyy or mm-dd-yyyy.");
		return false;
	}

	day = matchArray[1]; // p@rse date into variables
	month = matchArray[3];
	year = matchArray[5];

	if (month < 1 || month > 12) { // check month range
//		alert("Month must be between 1 and 12.");
		return false;
	}

	if (day < 1 || day > 31) {
//		alert("Day must be between 1 and 31.");
		return false;
	}

	if ((month==4 || month==6 || month==9 || month==11) && day==31) {
//		alert("Month "+month+" doesn`t have 31 days!")
		return false;
	}

	if (month == 2) { // check for february 29th
		var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
		if (day > 29 || (day==29 && !isleap)) {
//			alert("February " + year + " doesn`t have " + day + " days!");
			return false;
		}
	}
return true; // date is valid
}

function isHour(dateStr) {
//var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
var datePat = /^(\d{1,2})(:)(\d{1,2})$/;
var matchArray = dateStr.match(datePat); // is the format ok?

	if (matchArray == null) {
//		alert("Please enter date as either mm/dd/yyyy or mm-dd-yyyy.");
		return false;
	}

	hour = matchArray[1]; // p@rse date into variables
	minute = matchArray[3];

	if (hour < 0 || hour > 23) { // check month range
//		alert("Month must be between 1 and 12.");
		return false;
	}

	if (minute < 0 || day > 59) {
//		alert("Day must be between 1 and 31.");
		return false;
	}

return true; // date is valid
}


function isValid(vField, vValidationType, vMsg, vFocus, vDiv, vMaxChars)
{       
  switch( vValidationType )
  {
    case cEMPTY: 
      if ( isEmpty(vField,vMsg) )
      {
        if (vFocus){
          if (vDiv)
            ShowTab(vDiv);
          vField.focus();
        }
        return (false);
      }
      else
        return (true);
      break;
    case cNONE_SELECTED: 
      if ( isNoneSelected(vField,0,vMsg) )
      {
        if (vFocus){
          if (vDiv)
            ShowTab(vDiv);
          vField.focus();
        }
        return (false);
      }
      else
        return (true);
      break;
    case cNUMBER: 
      if ( !isValidNumber(vField,vMsg) )
      {
        if (vFocus){
          if (vDiv)
            ShowTab(vDiv);
          vField.focus();
        }
        return (false);
      }
      else
        return (true);
      break;
    case cDATE: 
      if ( !isValidDate(vField,vMsg) )
      {
        if (vFocus){
          if (vDiv)
            ShowTab(vDiv);
          vField.focus();
        }
        return (false);
      }
      else{
        return (true);
	  }
      break;
    case cHOUR: 
      if ( !isValidHour(vField,vMsg) )
      {
        if (vFocus){
          if (vDiv)
            ShowTab(vDiv);
          vField.focus();
        }
        return (false);
      }
      else{
        return (true);
	  }
      break;
    case cMAIL: 
      if ( !isValidMail(vField,vMsg) )
      {
        if (vFocus){
          if (vDiv)
            ShowTab(vDiv);
          vField.focus();
        }
        return (false);
      }
      else{
        return (true);
	  }
      break;
	case cLEN_OK: 
      if ( !isLenOK(vField,vMsg,vMaxChars) )
      {
        if (vFocus){
          if (vDiv)
            ShowTab(vDiv);
          vField.focus();
        }
        return (false);
      }
      else
        return (true);
      break;
    default:
      break;
  }
}

