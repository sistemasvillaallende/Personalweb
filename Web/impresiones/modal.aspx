<%@ Page Language="C#" AutoEventWireup="true" CodeFile="modal.aspx.cs" Inherits="impresiones_modal" %>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>
--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">

        function doSomething() {
            label = document.getElementById("lblCustomerDetail");
            label.innerText = "pruebaaaaaaaaaa";
        }
    
    </script>
</head>
<body>

    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <asp:GridView ID="GridView1" runat="server">
        <Columns>
        <asp:TemplateField>
        <ItemTemplate>
        <asp:Button ID="Button1" runat="server" OnClientClick="doSomething" Text="Dale" />
        </ItemTemplate>
        </asp:TemplateField>
        </Columns>
        </asp:GridView>
        
        

            

    </div>
    <%--<asp:Button id="btnShowPopup" runat="server" Text="Dame" 
        OnClientClick="doSomething()" onclick="btnShowPopup_Click" />
        
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlPopup">
        
    </ajaxToolkit:ModalPopupExtender>--%>
    
    <asp:Panel ID="pnlPopup" runat="server" Width="500px" style="display:none">
                <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblCustomerDetail" runat="server" Text="Customer Detail" BackColor="lightblue" Width="95%" />
                        <asp:DetailsView ID="dvCustomerDetail" runat="server" DefaultMode="Edit" Width="95%" BackColor="white" />
                    </ContentTemplate>                
                </asp:UpdatePanel>
                <div align="right" style="width:95%">
                    <asp:Button 
                        ID="btnSave" runat="server" Text="Save" 
                        OnClientClick="alert('Sorry, but I didnt implement save because I dont want my northwind database getting messed up.'); return false;" 
                        Width="50px" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" Width="50px" />
                </div>             
            </asp:Panel>
    </form>
</body>
</html>



