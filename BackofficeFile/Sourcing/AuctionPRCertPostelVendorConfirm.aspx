<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuctionPRCertPostelVendorConfirm.aspx.cs" Inherits="sourcing_AuctionPRCertPostelVendorConfirm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us">
<head runat="server">
    <title></title>
<link rel="stylesheet" type="text/css" href="../Styles/pips/Style_doctype.css"/>
<script type="text/javascript" src="../js/calendarWidget.js"></script>
<script language="javascript" type="text/javascript" src="../js/jquery-1.2.3.js"></script>
<script language="javascript" type="text/javascript" src="../js/modal-window.js"></script>
<script language="javascript" type="text/javascript" src="../js/utils.js"></script>
</head>
<body style="MARGIN: 0px">
<form id="form1" runat="server">
<table class="add_full-width-table"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
  <tr>
    <td>
        <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
        <tr>
          <td><img border="0" src="../images_tab/tab_s_on.gif" alt=""></td> 
          <td class="add_tab-active"><b>Postel Certificate Vendor Confirmation</b></td> 
          <td><img border="0" src="../images_tab/tab_e_on.gif" alt=""></td> 
          <td class="add_tab-end">&nbsp;</td> 
          <td><img border="0" src="../images_tab/tab_end.gif" alt=""></td>
        </tr>
      </table>
         <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
          <tr>
            <td>
              <asp:Panel id="pnlFilter" runat="server">
              <table class="Record add_table-real"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
                <tr class="Controls">
                  <td class="th">Search&nbsp;&nbsp; </td> 
                  <td><asp:TextBox ID="txtSearch" runat="server" Columns="32" 
                          CssClass="ControlsIN"></asp:TextBox>
                      &nbsp;</td> 
                  <td style="text-align: right">&nbsp; 
                  <asp:Button ID="btnFilter" runat="server" CssClass="Button" Text="Find" 
                        onclick="btnFilter_Click" />
                    &nbsp;&nbsp;</td>
                </tr>
              </table>
              </asp:Panel>
            </td>
          </tr>
        </table>
        <br /><br />
      <asp:Panel id="pnlGrid" runat="server">
          <table class="Header add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
            <tr>
            <td class="HeaderLeft"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td> 
            <td class="th">
                    Item with Postel Certification Request
            </td> 
            <td class="HeaderRight"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td>
            </tr>
        </table>
        <asp:GridView ID="dgvList" runat="server" AutoGenerateColumns="false" 
        CellPadding="5" CellSpacing="0" PageSize="1000" AllowPaging="false" PagerSettings-Visible="false" AllowCustomPaging="false"
        UseAccessibleHeader="true" Width="100%" OnRowDataBound="dgvList_RowDataBound">
        <HeaderStyle CssClass="Caption" />
        <RowStyle CssClass="Row"/>
        <AlternatingRowStyle CssClass="AltRow"/>
        <Columns>
            <asp:BoundField DataField="GoodsName" HeaderText="Item Name"></asp:BoundField>
            <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
            <asp:BoundField DataField="UnitShortCode" HeaderText="UoM"></asp:BoundField>
            <asp:BoundField DataField="ConfirmCode" HeaderText="Confirm ?"></asp:BoundField>
            <asp:BoundField DataField="CertificateConfirm" HeaderText="CertificateConfirm"></asp:BoundField>
        </Columns>
        </asp:GridView>
      <asp:Label ID="lblNoRow" runat="server"></asp:Label>
      <br />
      </asp:Panel>
        <asp:Panel ID="pnlPaging" runat="server">
        <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
        <tr>
        <td>
                &nbsp;
            <asp:ImageButton ID="imbFirst" runat="server" onclick="imbFirst_Click"/>
            <asp:ImageButton ID="imbPrev" runat="server" onclick="imbPrev_Click"/>
            &nbsp;
            <asp:Label ID="lblPaging" runat="server"></asp:Label>
            &nbsp;
            <asp:ImageButton ID="imbNext" runat="server" onclick="imbNext_Click"/>
            <asp:ImageButton ID="imbLast" runat="server" onclick="imbLast_Click"/>
        </td>
        </tr>
        </table>
        </asp:Panel>
    </td>
  </tr>
</table>
    
</form>
</body>
</html>
