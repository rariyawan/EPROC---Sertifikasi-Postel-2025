<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODeliveryPlanTechApprRqst.aspx.cs" Inherits="SAGR_PODeliveryPlanTechApprRqst" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us">
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
<body style="MARGIN: 0px">
    <form id="form1" runat="server">
<asp:Panel runat="server" ID="pnlTabList">
<table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
  <tr>
    <td><img border="0" src="../images_tab/tab_s_on.gif" alt=""></td> 
    <td class="add_tab-active"><b>Request Approval DO without Postel Certificate</b></td> 
    <td><img border="0" src="../images_tab/tab_e_on.gif" alt=""></td> 
    <td class="add_tab-end">&nbsp;</td> 
    <td><img border="0" src="../images_tab/tab_end.gif" alt=""></td>
  </tr>
</table>
</asp:Panel>
    
    <asp:Panel ID="pnlForm" runat="server">
    <table class="add_full-width-table"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
          <tr>
            <td>
                <table class="Header add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
                    <tr>
                    <td class="HeaderLeft"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td> 
                    <td class="th">
                        <strong>Request Approval DO without Postel Certificate</strong> 
                    </td> 
                    <td class="HeaderRight"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td>
                    </tr>
                </table>
            </td>
        </tr>
          <tr>


            <td>
              <table class="add_table-form"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
                <tr class="Controls">
                  <td class="th" colspan="2"><asp:Label ID="lblMessage" runat="server" CssClass="ErrorMsg"></asp:Label></td> 
                </tr>
                <tr class="Controls">
                  <td class="th" style="width: 20%">DO Number</td> 
                  <td class="add_no-wrap">
                      <asp:Label ID="lblDONumber" runat="server"></asp:Label>
                    </td> 
                </tr>
                  <tr class="Controls">
                      <td class="th" style="vertical-align:top">
                          Memo
                      </td>
                      <td class="add_no-wrap" style="vertical-align:top">
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
