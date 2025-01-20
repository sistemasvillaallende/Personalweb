function ChangePage(url) {
  location.href = url;
}

function PopUp(url, winname, width, height)
{
	if(navigator.appName == "Microsoft Internet Explorer")
	{
		screenY = document.body.offsetHeight;
		screenX = window.screen.availWidth;
	}
	else
	{ // Navigator coordinates
		screenY = screen.height;
		screenX = screen.width;
	}
	leftvar = (screenX - width) / 2;
	rightvar = (screenY - height) / 2;
		
	if(navigator.appName == "Microsoft Internet Explorer")
	{
		leftprop = leftvar;
		topprop = rightvar;
	}
	else
	{ // adjust Netscape coordinates for scrolling
		leftprop = (leftvar - pageXOffset);
		topprop = (rightvar - pageYOffset);
	}

	winprop="height="+height+",width="+width+",left="+leftprop+",top="+topprop+",status=no,toolbar=no,menubar=no,location=no, scrollbars=yes";
	window.open(url,winname,winprop);

}

function HiTab(vDiv){
	if (document.all.item("tab"))
		document.all.item("tab").value = vDiv;
	tabs = document.all.item("cTAB").length;
	if (!tabs)
		tabs = 1;

	for (i=0; i < tabs; i++){
		obj = document.all.item("cTAB",i);
		//hacer hi el div
		if (document.all.item("cTAB",i).name == vDiv){
			obj.className = "tabHi";
		}
	}

}

function LoTab(vDiv){
	if (document.all.item("tab"))
		document.all.item("tab").value = vDiv;
	tabs = document.all.item("cTAB").length;
	if (!tabs)
		tabs = 1;

	for (i=0; i < tabs; i++){
		obj = document.all.item("cTAB",i);
		if (document.all.item("cTAB",i).name == vCurrentTAB)
			obj.className = "tabOn";
		else
			obj.className = "tabOff";
		
	}

}
function ShowTab(vDiv){
	vCurrentTAB = vDiv;

	ns = (document.layers)? true:false
	ie = (document.all)? true:false

	if (document.all.item("tab"))
		document.all.item("tab").value = vDiv;
	divs = document.all.item("dTAB").length;
	tabs = document.all.item("cTAB").length;
	if (!divs)
		divs = 1;
	if (!tabs)
		tabs = 1;

	for (i=0; i < divs; i++){
		obj = document.all.item("dTAB",i);
		//hacer visible el div
		if (document.all.item("dTAB",i).name == vDiv){
			obj.style.display = 'block';
			obj.style.visibility = 'visible';
		}
		//hacer invisible el div
		else{
			obj.style.display = 'none';
			obj.style.visibility = 'hidden';
		}
	}

	for (i=0; i < tabs; i++){
		obj = document.all.item("cTAB",i);
		//hacer visible el div
		if (document.all.item("cTAB",i).name == vDiv){
			obj.className = "tabOn";
			//obj.style.display = 'block';
			//obj.style.visibility = 'visible';
		}
		//hacer invisible el div
		else{
			obj.className = "tabOff";
			//obj.style.display = 'none';
			//obj.style.visibility = 'hidden';
		}
//alert(obj.className);
		//alert(obj.style.display);
	}

//		alert(document.all.item("tabLeft").bgColor);
}


function ShowAllTabs(vDiv){
	vCurrentTAB = vDiv;
	if (document.all.item("tab"))
		document.all.item("tab").value = vDiv;
	divs = document.all.item("dTAB").length;
	tabs = document.all.item("cTAB").length;
	if (!divs)
		divs = 1;
	if (!tabs)
		tabs = 1;

	for (i=0; i < divs; i++){
		obj = document.all.item("dTAB",i);
		//hacer visible el div
		obj.style.display = 'block';
		obj.style.visibility = 'visible';
	}

	for (i=0; i < tabs; i++){
		obj = document.all.item("cTAB",i);
		//hacer visible el div
		if (document.all.item("cTAB",i).name == vDiv){
			obj.className = "tabOn";
			//obj.style.display = 'block';
			//obj.style.visibility = 'visible';
		}
		//hacer invisible el div
		else{
			obj.className = "tabOff";
			//obj.style.display = 'none';
			//obj.style.visibility = 'hidden';
		}
//alert(obj.className);
		//alert(obj.style.display);
	}

//		alert(document.all.item("tabLeft").bgColor);
}

function OpenWin(url,width,height){
	window.open(url,null,"height="+height+",width="+width+",left=200,top=200,scrollbars=yes,status=no,toolbar=no,menubar=no,location=no");
}

function setFocus(vField){
	vField.focus(); //Focus on the Field
	if (vField.type == "text" || vField.type == "textarea")
		vField.select();//Select the text in field
}

function DisplayTabset(vTabs){
	sHead = "<table cellspacing=0 cellpadding=0 width=100% border=0><tbody><tr><td colspan=3><table class=tabSpacer cellspacing=0 cellpadding=0 width=100% border=0><tbody> <tr> ";
	sTab = "";
	for (i=0; i<vTabs.length;i++){
		id = vTabs[i].substr(0,vTabs[i].indexOf(','));
		desc = vTabs[i].substr(vTabs[i].indexOf(',')+1,vTabs[i].length);
		sTab = sTab + "<td id=tabLeft valign=top align=left height=20><img height=5 src=images/lt_tabnotch.gif width=4 border=0></td><td class=tabOff valign=center noWrap align=middle height=20 id=tito><a class=tabOff href=javascript:void(0) onClick=\"ShowTab('" + id + "');alert(tito.style.backgroundColor);\"><font color=#FFFFFF>" + desc + "</font></a></td><td class=tabOff valign=top align=left bgcolor=#3366cc><img height=5 src=images/rt_tabnotch.gif width=4 border=0></td><td><img height=1 src=images/s.gif width=3></td>";
	}
	sFooter = "<td valign=top align=left width=100% colspan=40>&nbsp;</td></tr><tr> <td bgcolor=#3366cc colspan=8><img height=1 src=images/s.gif width=1></td><td class=accountsTab colspan=3 bgcolor=#3366cc><img height=1 src=images/s.gif width=1></td><td bgcolor=#3366cc colspan=18><img height=1 src=images/s.gif width=1></td><td width=100% bgcolor=#3366cc><img height=1 src=images/s.gif width=1></td></tr></tbody> </table></td></tr><tr><td class=accountsTab colspan=3 height=5 bgcolor=#3366cc></td></tr><tr> <td class=accountsTab colspan=3 height=1 bgcolor=#333399></td></tr></tbody> </table>";
	document.write (sHead+sTab+sFooter);
}

function RemoveKey(fromString, key){
		posStartPair = fromString.indexOf(key);
		if (posStartPair != -1){
			posEndPair = fromString.indexOf("&", posStartPair);
			if (posEndPair != -1)
				return fromString.substr(0,posStartPair) + fromString.substr(posEndPair+1,fromString.length-posEndPair);
			else
				return fromString.substr(0,posStartPair-1);
		}
		else
			return fromString;		
}

function SetAction(f, r){
	f.Action.value = r;
}

function GetUserDetails(){
	wUD=window.open("GetUserDetails.asp",null,"height=200,width=400,left=200,top=200,status=no,toolbar=no,menubar=no,location=no");
}

function SetDetails(vSave, vDetails){
	if (vSave){
//		alert('set');
		window.document.thisForm.Details.value=vDetails;
		window.document.thisForm.submit();
	}
//	else
//		alert('cancel');
}

function SwapDiv(vDiv){
	SwapDivState(vDiv);
//	SwapImg('btn'+vDiv);
}

function DoAction(frm, Action){
	splitAction = Action.split("-")
	Result = splitAction[0];
	NextTask = splitAction[1];

	switch (Result){
		case "ACCEPT":  SetAction(frm, Result); 
						frm.submit();
						break;
		case "DENY": 	SetAction(frm, Result); 
						GetUserDetails();
						break;
		default :  
						if( Validator(frm))
						{
							SetAction(frm, Result);
							frm.submit();
						}
						break;
	}

}

function RefreshPage(CurrentTab){
	if (CurrentTab && thisForm.tab)
		thisForm.tab.value = CurrentTab;
	document.thisForm.submit();
}

function SetFieldValue(vField, vValue){
	vField.value = vValue;
	
}

function ToggleVisibility(vDiv){
	btnExpand = 'images/boton_mas_2.gif';
	btnCollapse = 'images/boton_menos_2.gif';
	obj = document.all.item(vDiv);
	img = document.all.item('ctrl_' + vDiv);
	
	if (obj)
	{
		vVisible = (obj.style.visibility == 'visible')
		//hacer invisible el div
		if (vVisible){
			obj.style.display = 'none';
			obj.style.visibility = 'hidden';
			img.src = btnExpand;
		}
		//hacer visible el div
		else{
			obj.style.display = 'block';
			obj.style.visibility = 'visible';
			img.src = btnCollapse;
		}
	}
}
function SwapDivVisibility(vName){
	obj = document.all.item(vName);
	if (obj.style.display == 'none')
			SetDivVisibility(vName, true);
		else
			SetDivVisibility(vName,false);
}

function SetDivVisibility(vDivName, vExpanded){
	if (vExpanded){
		SetDivStyle(vDivName,'block','visible');
	}
	else{
		SetDivStyle(vDivName,'none','hidden');
	}
}

function WriteTab(vTabName, vTabTitle){
	var htmlStr = "";
	htmlStr += "    <!-- #BeginTab -->";
	htmlStr += "    <td> ";
	htmlStr += "      <div id=\"cTAB\" name=\"" + vTabName + "\" style=\"display:inline;\">"
	htmlStr += "      <table cellspacing=0 cellpadding=0 width=\"100%\" border=0>"
	htmlStr += "        <tr> "
	htmlStr += "              <td valign=top align=left height=20><img height=5 src=\"../images/lt_tabnotch.gif\" width=4 border=0></td>"
	htmlStr += "              <td valign=center noWrap align=middle height=20><a "
	htmlStr += "            class=tabOn href=\"javascript:void(0)\" onClick=\"ShowTab('" + vTabName + "');\" onMouseOver=\"HiTab('" + vTabName + "');\" onMouseOut=\"LoTab('" + vTabName + "');\">" + vTabTitle + "</a></td>"
	htmlStr += "              <td valign=top align=left><img height=5 src=\"../images/rt_tabnotch.gif\" width=4 border=0></td>"
	htmlStr += "        </tr>"
	htmlStr += "      </table>"
	htmlStr += "	  </div>"
	htmlStr += "    </td>"
	htmlStr += "    <!-- #EndTab -->"
	htmlStr += "    <!-- #BeginSingleSeparator -->"
	htmlStr += "          <td><img height=1 src=\"images/s.gif\" width=3></td>"
	htmlStr += "    <!-- #EndSingleSeparator -->"
	document.write(htmlStr);

}
