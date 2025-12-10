<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODetailSpecificationCert.aspx.cs" Inherits="procurement_PODetailSpecificationCert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us">
<head runat="server">
    <title></title>
<link rel="stylesheet" type="text/css" href="../Styles/pips/Style_doctype.css"/>
<script type="text/javascript" src="../js/calendarWidget.js"></script>
<script language="javascript" type="text/javascript" src="../js/jquery-1.2.3.js"></script>
<script language="javascript" type="text/javascript" src="../js/modal-window.js"></script>
<script language="javascript" type="text/javascript" src="../js/utils.js"></script>
<script language="javascript" type="text/javascript">
    function toView(filePath) {
        window.open('../include/DokView.aspx?' + filePath, 'OpenFile', 'width=800,height=550,toolbar=no,resizable=yes');
    }
</script>
</head>
<body style="MARGIN: 0px">
<form id="form1" runat="server">
<table class="add_full-width-table"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
  <tr>
    <td>
        <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
        <tr>
          <td><img border="0" src="../images_tab/tab_s_on.gif" alt=""></td> 
          <td class="add_tab-active"><b>Postel Certificate Data</b></td> 
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
                  <td class="th">Cari &nbsp;&nbsp; </td> 
                  <td><asp:TextBox ID="txtSearch" runat="server" Columns="32" 
                          CssClass="ControlsIN"></asp:TextBox>
                      &nbsp;&nbsp;<asp:Button ID="btnFilter" runat="server" CssClass="Button" Text="Find" onclick="btnFilter_Click" />&nbsp;&nbsp;</td> 
                </tr><!--
                <tr class="Controls">
                  <td colspan="4" style="text-align: right">&nbsp; 
                    <asp:Button ID="btnFilterx" runat="server" CssClass="Button" Text="Find" 
                          onclick="btnFilter_Click" />
                      &nbsp;&nbsp;</td>
                </tr>-->
              </table>
              </asp:Panel>
            </td>
          </tr>
        </table>
        <br /><br />
      <asp:Panel id="pnlGrid" runat="server">
          <asp:Label ID="lblMessage" ForeColor="Red" runat="server" Font-Size="Medium"></asp:Label>
          <table class="Header add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
            <tr>
            <td class="HeaderLeft"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td> 
            <td class="th">
                    Postel Certificate Data
            </td> 
            <td class="HeaderRight"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td>
            </tr>
        </table>
        <asp:GridView ID="dgvList" runat="server" AutoGenerateColumns="false" 
        CellPadding="5" CellSpacing="0" PageSize="20" AllowPaging="false" PagerSettings-Visible="false" AllowCustomPaging="false"
        UseAccessibleHeader="true" Width="100%" OnRowDataBound="dgvList_RowDataBound">
        <HeaderStyle CssClass="Caption" />
        <RowStyle CssClass="Row"/>
        <AlternatingRowStyle CssClass="AltRow"/>
        <Columns>
            <asp:BoundField DataField="PODetailSpecificationCertId_PK" HeaderText="ID"></asp:BoundField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="2%" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                        PO
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPO" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="2%" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                        Item / Specification Code
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblSpec" runat="server" style="white-space:normal"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="2%" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                        Certificate Info
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblCert" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PONumber" HeaderText="4PONumber"></asp:BoundField>
            <asp:BoundField DataField="MaterialServiceText" HeaderText="5MaterialServiceText"></asp:BoundField>
            <asp:BoundField DataField="SpecificationCode" HeaderText="6SpecificationCode"></asp:BoundField>
            <asp:BoundField DataField="GoodsServiceName" HeaderText="7GoodsServiceName"></asp:BoundField>
            <asp:BoundField DataField="VendorName" HeaderText="8VendorName"></asp:BoundField>
            <asp:BoundField DataField="CertificateNumber" HeaderText="9CertificateNumber"></asp:BoundField>
            <asp:BoundField DataField="CertificateURL" HeaderText="10CertificateURL"></asp:BoundField>
            <asp:BoundField DataField="PeriodStart" HeaderText="11PeriodStart"></asp:BoundField>
            <asp:BoundField DataField="PeriodEnd" HeaderText="12PeriodEnd"></asp:BoundField>
            <asp:BoundField DataField="InternalUploadFilePath" HeaderText="13InternalUploadFilePath"></asp:BoundField>
            <asp:BoundField DataField="AdditionalNotes" HeaderText="14AdditionalNotes"></asp:BoundField>
            <asp:BoundField DataField="JustificationFilePath" HeaderText="15JustificationFilePath"></asp:BoundField>
        </Columns>
        </asp:GridView>
      <asp:Label ID="lblNoRow" runat="server"></asp:Label>
      <br />
      </asp:Panel>
        <asp:Panel ID="pnlPaging" runat="server">
        <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
        <tr>
        <td>
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