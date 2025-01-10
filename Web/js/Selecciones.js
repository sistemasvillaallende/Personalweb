function ConfirmAlterObject(vName) {
	if (confirm('Desea ' + vName + ' este elemento?'))
		return true;
	else
		return false;
}

function ChangePage(url) {
  location.href = url;
}

function SetSelectedFromValue(obj,val)
{

	for (i=0; i < obj.length;i++)
	{
		if (obj.options[i].value == val)
			obj.options[i].selected = true;
	}
}

function SetCheckedFromValue(obj,list)
{
var objlist = list.split(',');
	if (obj){
	if (objlist.length) {
		for (i=0; i < objlist.length; i++)
		{
			for (j=0; j < obj.length; j++){
				if (obj[j].value == objlist[i])
					obj[j].checked = true;
			}
		}
	}
	}
}

function SetObjList(frm)
{
  var strlist = '';
  if ( frm.checkbox.length > 0 )
    for (counter = 0; counter < frm.checkbox.length; counter++)
    {
      if ( frm.checkbox[counter].checked )
        strlist = strlist + frm.checkbox[counter].value + ',';
    }
  else
  {
    if ( frm.checkbox.checked )
      strlist = strlist + frm.checkbox.value;
  }
  frm.objlist.value = strlist;
}

function SetFrmAction(frm, action)
{
  frm.action.value = action;
}

function GetDate()
{
  GetDate = date.year();
}

function ValidateSelected(frm, straccion)
{
  var anyChecked;
  anyChecked = false;

  if ( frm.checkbox.length > 0 )
  {
    for (counter = 0; counter < frm.checkbox.length; counter++)
    {
      if ( frm.checkbox[counter].checked )
        anyChecked = true;
    }
  }
  else
  {
    if ( frm.checkbox.checked )
      anyChecked = true;
  }
  if (anyChecked)
  {
    if (confirm("Desea " + straccion + " los elementos seleccionados?"))
	    return true;
    else
	    return false;
  }
  else
  {
    alert("Debe marcar al menos un elemento!");    
    return false;
  }
}
function SelectAll(frm)
{
  if ( frm.selector.value == "Marcar todos" )
  {
    for (counter = 0; counter < frm.checkbox.length; counter++)
    {
      frm.checkbox[counter].checked = true;
    }
    frm.selector.value = "Quitar marcas";
  }
  else
  {
    for (counter = 0; counter < frm.checkbox.length; counter++)
    {
      frm.checkbox[counter].checked = false;
    }
    frm.selector.value = "Marcar todos";
  }

}

function ToggleChecks(check, checkSet)
{

  if (check.checked) //esta marcado
  {
//    check.checked = false;
    if (checkSet.length > 0){
      for (counter = 0; counter < checkSet.length; counter++){
        checkSet[counter].checked = true;
      }
    }
    else{
		checkSet.checked = true;
    }
  }
  else //no esta marcado
  {
//    check.checked = true;
    if (checkSet.length > 0){
      for (counter = 0; counter < checkSet.length; counter++){
        checkSet[counter].checked = false;
      }
    }
    else{
		checkSet.checked = false;
    }
  }
}

function TogleAll(frm)
{
    for (counter = 0; counter < frm.checkbox.length; counter++)
    {
      if (frm.checkbox[counter].checked )
        frm.checkbox[counter].checked = false;
      else
        frm.checkbox[counter].checked = true;
    }

}

function GetCheckedIDs(checkSet)
{
var CheckedIDs = '';

  if (checkSet.length > 0){
    for (counter = 0; counter < checkSet.length; counter++){
      if (checkSet[counter].checked)
        CheckedIDs = CheckedIDs + checkSet[counter].value + ',';
    }
  }
  else{
      if (checkSet.checked)
        CheckedIDs = checkSet.value + ',';
  }
  return CheckedIDs;
}