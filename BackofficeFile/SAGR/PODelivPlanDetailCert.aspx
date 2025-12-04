<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODelivPlanDetailCert.aspx.cs" Inherits="SAGR_PODelivPlanDetailCert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link rel="stylesheet" type="text/css" href="../Styles/pips/Style_doctype.css"/>
<script lang="javascript" type="text/javascript" src="../js/jquery-1.2.3.js"></script>
<script lang="javascript" type="text/javascript" src="../js/modal-window.js"></script>
<script lang="javascript" type="text/javascript" src="../js/utils.js"></script>
</head>
<script language="javascript" type="text/javascript">
    function toView(filePath) {
        window.open('../include/DokView.aspx?' + filePath, 'OpenFile', 'width=800,height=550,toolbar=no,resizable=yes');
    }
</script>
<body style="MARGIN: 0px" marginheight="0" marginwidth="0">
    <form id="form1" runat="server">
<asp:Panel runat="server" ID="pnlTabList">
<table cellspacing="0" cellpadding="0" width="100%" border="0">
  <tr>
    <td><img border="0" src="../images/tab_s_on.gif"></td> 
    <td class="TabOn" background="../images/tab_on.gif" nowrap align="center">Sertifikat Postel</td> 
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
                        <strong>Sertifikat Postel</strong> 
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
                  <td class="th">Nomor Sertifikat</td> 
                  <td nowrap="nowrap">
                      <asp:Label ID="lblCertificateNumber" runat="server"></asp:Label>
                    </td> 
                </tr>
                  <tr class="Controls">
                      <td class="th">
                          SDPPI URL</td>
                      <td nowrap="nowrap">
                          <asp:Label ID="lblCertificateURL" runat="server"></asp:Label>
                      </td>
                  </tr>
                  <tr class="Controls">
                      <td class="th">Periode</td>
                      <td nowrap="nowrap">
                          <asp:Label ID="lblStartPeriod" runat="server"></asp:Label>
                          &nbsp;s/d&nbsp;
                          <asp:Label ID="lblEndPeriod" runat="server"></asp:Label>
                      </td>
                  </tr>
                    <tr class="Controls">
                      <td class="th">File Sertifikat</td> 
                      <td><asp:Label ID="lblDoc" runat="server"></asp:Label>
                      </td>
                    </tr>
                  <tr class="Controls">
                      <td class="th" valign="top">
                          Keterangan <br />
                      </td>
                      <td nowrap="nowrap" valign="top">
                          <asp:Label ID="lblAdditionalNotes" runat="server"></asp:Label>
                      </td>
                  </tr>
                  <tr class="Bottom">
                       <td colspan="2" style="text-align:center">

                           <asp:TextBox ID="txtPODelivPlanDetailId" runat="server" CssClass="ControlsIN" Visible="False"></asp:TextBox>

                           <asp:TextBox ID="txtCertId" runat="server" CssClass="ControlsIN" Visible="False"></asp:TextBox>

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
