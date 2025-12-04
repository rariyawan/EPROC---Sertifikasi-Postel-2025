<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODeliveryPlanTechApprRqst.aspx.cs" Inherits="SAGR_PODeliveryPlanTechApprRqst" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link rel="stylesheet" type="text/css" href="../Styles/pips/Style_doctype.css"/>
<script lang="javascript" type="text/javascript" src="../js/jquery-1.2.3.js"></script>
<script type="text/javascript" src="../js/calendarWidget.js"></script>
<script lang="javascript" type="text/javascript" src="../js/modal-window.js"></script>
<script lang="javascript" type="text/javascript" src="../js/utils.js"></script>
<script language="javascript" type="text/javascript">
    function clickSave() {
        return (confirm("Are you sure to Submit ?"));
    }
</script>
</head>
<body style="MARGIN: 0px" marginheight="0" marginwidth="0">
    <form id="form1" runat="server">
<asp:Panel runat="server" ID="pnlTabList">
<table cellspacing="0" cellpadding="0" width="100%" border="0">
  <tr>
    <td><img border="0" src="../images/tab_s_on.gif"></td> 
    <td class="TabOn" background="../images/tab_on.gif" nowrap align="center">Request Approval DO without Postel Certificate</td> 
      <td><img border="0" src="../images/tab_e_on.gif"></td> 
    <td background="../images/tab_back.gif" width="100%"></td>
  </tr>
</table>
</asp:Panel>
    
    <asp:Panel ID="pnlForm" runat="server">
    
    <table border="0" cellpadding="0" cellspacing="0" width="99%" align="center">
          <tr>
            <td>
                <table class="Header" border="0" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                    <td class="HeaderLeft"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td> 
                    <td class="th">
                        <center>
                        <strong>Request Approval DO without Postel Certificate</strong> 
                        </center>
                    </td> 
                    <td class="HeaderRight"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td>
                    </tr>
                </table>
            </td>
        </tr>
          <tr>
            <td valign="top">
              <table cellspacing="0" cellpadding="5" width="100%">
                <tr class="Controls">
                  <td class="th" colspan="2"><asp:Label ID="lblMessage" runat="server" CssClass="ErrorMsg"></asp:Label></td> 
                </tr>
                <tr class="Controls">
                  <td class="th" width="20%">DO Number</td> 
                  <td nowrap="nowrap">
                      <asp:Label ID="lblDONumber" runat="server"></asp:Label>
                    </td> 
                </tr>
                  <tr class="Controls">
                      <td class="th" valign="top">
                          Memo
                      </td>
                      <td nowrap="nowrap" valign="top">
                          <asp:TextBox ID="txtNotes" runat="server" Columns="50" CssClass="ControlsRQ" onkeypress="return imposeMaxLength(this, 255);" onpaste="return maxLengthPaste(this, 255);" Rows="5" TextMode="MultiLine"></asp:TextBox>
                      </td>
                  </tr>

                  
                  <tr class="Bottom">
                       <td colspan="2" style="text-align:center">

                           <asp:Button ID="btnSave" OnClientClick="return clickSave()" runat="server" CssClass="Button" Text="Submit Request Approval" 
                               onclick="btnSave_Click" />

                           <asp:TextBox ID="txtPODeliveryPlanId" runat="server" CssClass="ControlsIN" Visible="False"></asp:TextBox>

                       </td>
                  </tr>
            </table>
        </td>
        </tr>
    </table>
    </asp:Panel>

    </form>
</body>
</html>
