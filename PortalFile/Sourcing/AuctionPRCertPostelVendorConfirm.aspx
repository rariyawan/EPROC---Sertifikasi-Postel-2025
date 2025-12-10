<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuctionPRCertPostelVendorConfirm.aspx.cs" Inherits="sourcing_AuctionPRCertPostelVendorConfirm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../Common/Styles/portal/Style.css">
    <script type="text/javascript" src="../Common/js/formatnumber.js"></script>
    <script lang="javascript" type="text/javascript" src="../Common/js/jquery-1.2.3.js"></script>
    <script language="javascript" type="text/javascript">
        function clickDelItem(strParamDel) {
            if (confirm("Are you sure to Delete this Item ?")) {
                document.location.href = "AuctionQuotPRItemManual.aspx?" + strParamDel;
            }
        }
    </script>
</head>
<body style="BACKGROUND-COLOR: #ffffff" bottommargin="3" leftmargin="3" rightmargin="3" topmargin="3">
    <form id="form1" runat="server">
    <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
      <tr>
        <td><img border="0" src="../Common/images/bar_left.gif" alt=""></td> 
        <td class="PortalBarMain" style="TEXT-ALIGN: center; width: 100%"><strong>Postel Certificate Confirmation</strong></td> 
        <td><img border="0" src="../Common/images/bar_right.gif" alt=""></td>
      </tr>
    </table>
    <br />
        <asp:Label ID="lblMessage" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
        <asp:Label ID="lblItem" runat="server"></asp:Label>
        <br /><br /><br />
    <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
    <tr>
    <td colspan="2" class="PortalFooter" style="TEXT-ALIGN: center; width: 100%">
        <asp:Button ID="btnSaveItem" runat="server" Text="Save" CssClass="PortalButton" OnClick="btnSaveItem_Click" />
    </td> 
    </tr>
    </table>
    </form>
</body>
</html>
