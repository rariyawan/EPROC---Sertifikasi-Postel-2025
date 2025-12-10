<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODelivPlanDetailCert.aspx.cs" Inherits="SAGR_PODelivPlanDetailCert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us">
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
<body style="MARGIN: 0px">
    <form id="form1" runat="server">
<asp:Panel runat="server" ID="pnlTabList">
<table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
  <tr>
    <td><img border="0" src="../images_tab/tab_s_on.gif" alt=""></td> 
    <td class="add_tab-active"><b>Sertifikat Postel</b></td> 
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
                        <strong>Sertifikat Postel</strong> 
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
                  <td class="th">Nomor Sertifikat</td> 
                  <td class="add_no-wrap">
                      <asp:Label ID="lblCertificateNumber" runat="server"></asp:Label>
                    </td> 
                </tr>
                  <tr class="Controls">
                      <td class="th">
                          SDPPI URL</td>
                      <td class="add_no-wrap">
                          <asp:Label ID="lblCertificateURL" runat="server"></asp:Label>
                      </td>
                  </tr>
                  <tr class="Controls">
                      <td class="th">Periode</td>
                      <td class="add_no-wrap">
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
                      <td class="th" style="vertical-align:top">
                          Keterangan <br />
                      </td>
                      <td class="add_no-wrap" style="vertical-align:top">
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
