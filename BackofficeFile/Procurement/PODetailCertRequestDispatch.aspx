<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODetailCertRequestDispatch.aspx.cs" Inherits="procurement_PODetailCertRequestDispatch" %>

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
    function clickInfo(strParam) {
        openMyModal('../procurement/PRMonitoringInfo.aspx?' + strParam, 1000, 450, "auto");
    }

    function clickFlow(strParam) {
        openMyModal('../procurement/FlowControlmonitoring_Fetch.aspx?' + strParam, 1000, 450, "auto");
    }

    function clickAll(obj) {
        cek = obj.checked;
        objDisp = document.getElementsByName("chkDisp");
        for (d = 0; d < objDisp.length; d++) {
            if (cek) {
                document.getElementsByName("chkDisp")[d].checked = true;
            } else {
                document.getElementsByName("chkDisp")[d].checked = false;
            }
        }
    } 

    function dispatchConfirm() {

        objdtl = document.getElementsByName("chkDisp");
        var intCek = 0;
        for (d = 0; d < objdtl.length; d++) {
            strValDtl = document.getElementsByName("chkDisp")[d].value;
            if (document.getElementsByName("chkDisp")[d].checked) {
                if ($("#cboDispatchTo_" + strValDtl).val() == "0" || $("#cboDispatchTo_" + strValDtl).val() == "") {
                    alert("PIC not filled yet in some selected item ...!");
                    return false;
                }

                intCek = 1;
            }
        }

        if (intCek == 0) {
            alert("No Data selected");
            return false;
        } else {
            return confirm("Are you sure to dispatch selected data ?");
        }

    }

</script>
</head>
<body style="MARGIN: 0px">
<form id="form1" runat="server">
<table class="add_full-width-table"><caption style="display:none">Data</caption><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
  <tr>
    <td>
        <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
        <tr>
          <td><img border="0" src="../images_tab/tab_s_on.gif" alt=""></td> 
          <td class="add_tab-active"><b>Postel Certificate Request Dispatch</b></td> 
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
                      &nbsp;&nbsp;<asp:Button ID="btnFilter" runat="server" CssClass="Button" Text="Find" onclick="btnFilter_Click" />&nbsp;&nbsp;
                      </td> 
                  <td style="display:none" class="th">Status&nbsp;&nbsp; </td> 
                  <td style="display:none">
                      <asp:DropDownList ID="ddlStatus" CssClass="ControlsIN" runat="server">
                          <asp:ListItem Value="-1">- ALL STATUS -</asp:ListItem>
                          <asp:ListItem Value="0">UN-DISPATCHED</asp:ListItem>
                          <asp:ListItem Value="1">PROCUREMENT PROCESS</asp:ListItem>
                          <asp:ListItem Value="6">PROCUREMENT VERIFICATION</asp:ListItem>
                          <asp:ListItem Value="2">FINISH</asp:ListItem>
                          <asp:ListItem Value="4">CANCEL</asp:ListItem>
                      </asp:DropDownList>
                    &nbsp;</td> 
                </tr><!--
                <tr class="Controls">
                  <td colspan="4" style="text-align: right">&nbsp; 
                    <asp:Button ID="btnFilterx" runat="server" CssClass="Button" Text="Find" 
                          onclick="btnFilter_Click" />
                      &nbsp;&nbsp;</td></tr>-->
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
                    Postel Certificate Request Dispatch
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
            <asp:BoundField DataField="PODetailCertificateRequestId_PK" HeaderText="ID"></asp:BoundField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="2%" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="false" onclick="javascript:clickAll(this)" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblDetail" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RequestNumber" HeaderText="Request Number"></asp:BoundField>
            <asp:BoundField DataField="RequestDate" HeaderText="Request Date"></asp:BoundField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                        Description
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="white-space: normal; word-wrap: break-word;">
                        <%# Eval("RequestDesc") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RequestStatusName" HeaderText="Status"></asp:BoundField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="2%" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                        Dispatch To
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPIC" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PONumber" HeaderText="PO number"></asp:BoundField>
            <asp:BoundField DataField="SpecificationCode" HeaderText="Spec. Code"></asp:BoundField>
            <asp:BoundField DataField="MaterialServiceText" HeaderText="Item Name"></asp:BoundField>
            <asp:BoundField DataField="RequestStatus" HeaderText="RequestStatus"></asp:BoundField>
            <asp:BoundField DataField="VendorName" HeaderText="VendorName"></asp:BoundField>
        </Columns>
        </asp:GridView>
      <asp:Label ID="lblNoRow" runat="server"></asp:Label>
      <br />
      </asp:Panel>
        <asp:Panel ID="pnlPaging" runat="server">
        <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
        <tr>
        <td>
            <asp:Button ID="btnDispatch" CssClass="Button" runat="server" OnClientClick="return dispatchConfirm()" Text="Dispatch" OnClick="btnDispatch_Click" /></td>
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