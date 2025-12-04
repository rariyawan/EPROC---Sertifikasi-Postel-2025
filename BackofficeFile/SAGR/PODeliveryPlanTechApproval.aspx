<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODeliveryPlanTechApproval.aspx.cs" Inherits="SAGR_PODeliveryPlanTechApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link rel="stylesheet" type="text/css" href="../Styles/pips/Style_doctype.css"/>
<script type="text/javascript" src="../js/calendarWidget.js"></script>
<script language="javascript" type="text/javascript" src="../js/jquery-1.2.3.js"></script>
<script language="javascript" type="text/javascript" src="../js/modal-window.js"></script>
<script language="javascript" type="text/javascript" src="../js/utils.js"></script>
<script language="javascript" type="text/javascript">
 

    function clickAppr(decision) {
        strDecision = decision;
        if (confirm("Are you sure to " + strDecision + " ?")) {
            document.getElementById("btnSubmit").style.display = "none";
            document.getElementById("textLoading").style.display = "";
            return true;
        } else {
            return false;
        }
    }
</script>
    
</head>
<body style="MARGIN: 0px" marginheight="0" marginwidth="0">
<form id="form1" runat="server" enctype="multipart/form-data">
<table align="left" width="100%">
  <tr>
    <td>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
          <td><img border="0" src="../images_tab/tab_s_on.gif"></td> 
          <td background="../images_tab/tab_on.gif" nowrap align="center"><b>Approval Delivery Plan - Postel</b></td> 
          <td><img border="0" src="../images_tab/tab_e_on.gif"></td> 
          <td background="../images_tab/tab_back.gif" width="100%">&nbsp;</td> 
          <td><img border="0" src="../images_tab/tab_end.gif"></td>
        </tr>
      </table>
         <table border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="top">
              <asp:Panel id="pnlFilter" runat="server">
              <table class="Record" cellspacing="0" cellpadding="0">
                <tr class="Controls">
                  <td class="th">Cari &nbsp;&nbsp; </td> 
                  <td><asp:TextBox ID="txtSearch" runat="server" Columns="32" 
                          CssClass="ControlsIN"></asp:TextBox>
                      &nbsp;</td> 
                </tr>
                <tr class="Controls">
                  <td colspan="2" style="text-align: right">&nbsp; 
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
          
          <table class="Header" border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
            <td class="HeaderLeft"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td> 
            <td class="th">
                    Approval Delivery Plan - Postel
            </td> 
            <td class="HeaderRight"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td>
            </tr>
        </table>
        <asp:GridView ID="dgvList" runat="server" AutoGenerateColumns="false" 
        CellPadding="5" CellSpacing="0" PageSize="20" AllowPaging="false" PagerSettings-Visible="false" AllowCustomPaging="false"
        UseAccessibleHeader="true" Width="100%" OnRowCommand="dgvList_RowCommand" OnRowDataBound="dgvList_RowDataBound">
        <HeaderStyle CssClass="Caption" />
        <RowStyle CssClass="Row"/>
        <AlternatingRowStyle CssClass="AltRow"/>
        <Columns>
            <asp:BoundField DataField="PODelivPlanTechApprId_PK" HeaderText="ID"></asp:BoundField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="2%" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                    #
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="imbDetail" runat="server" ImageUrl="~/Images/table.gif" CommandName="Detail" CommandArgument="<%# Container.DataItemIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DONumber" HeaderText="DO Number"></asp:BoundField>
            <asp:BoundField DataField="DeliveryPlanDate" HeaderText="Delivery Date"></asp:BoundField>
            <asp:BoundField DataField="PONumber" HeaderText="PO Number"></asp:BoundField>
            <asp:BoundField DataField="PODate" HeaderText="PO Date"></asp:BoundField>
            <asp:BoundField DataField="TargetDeliveryDate" HeaderText="Target Delivery"></asp:BoundField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                        PO Description
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="white-space: normal; word-wrap: break-word;">
                        <%# Eval("PODescription") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="VendorName" HeaderText="Vendor"></asp:BoundField>
            <asp:BoundField DataField="Requester" HeaderText="Requester"></asp:BoundField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                        Memo
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="white-space: normal; word-wrap: break-word;">
                        <%# Eval("RequestNotes") %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
      <asp:Label ID="lblNoRow" runat="server"></asp:Label>
      <br />
      </asp:Panel>
        <asp:Panel ID="pnlPaging" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="99%" align="center">
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


 <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
<tr>
  <td>
    <asp:Panel ID="pnlForm" runat="server">

      <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
      <tr>
          <td>
              <table class="Header" border="0" cellspacing="0" cellpadding="0" width="100%">
                  <tr>
                  <td class="HeaderLeft"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td> 
                  <td class="th">
                      <center>
                          Approval Delivery Plan - Postel</center>
                  </td> 
                  <td class="HeaderRight"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td>
                  </tr>
              </table>
          </td>
      </tr>
      <tr>
          <td valign="top">
            <asp:Label ID="lblMessage" ForeColor="Red" runat="server" Font-Size="Medium"></asp:Label>
            <table cellspacing="0" cellpadding="5" width="100%">
                <tr class="Controls">
                    <td class="th" style="width:30%"><strong>DO Number</strong></td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblDONumber" runat="server"></asp:Label> / <asp:Label ID="lblDeliveryDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="Controls">
                    <td class="th"><strong>PO Number</strong></td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblPONumber" runat="server"></asp:Label> / <asp:Label ID="lblPODate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="Controls">
                     <td class="th"><strong>Vendor</strong></td> 
                     <td nowrap="nowrap">
                         <asp:Label ID="lblVendorName" runat="server"></asp:Label>
                     </td> 
                 </tr>
                <tr class="Controls">
                    <td class="th"><strong>PO Description</strong></td> 
                    <td nowrap="nowrap">
                        <asp:Label ID="lblPODescription" runat="server"></asp:Label>
                    </td> 
                </tr>
               
                <tr class="Controls">
                    <td class="th"><strong>Target Delivery</strong></td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblTargetDelivery" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="Controls">
                    <td class="th"><strong>Requester</strong></td>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblRequester" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr class="Controls">
                     <td class="th"><strong>Memo Request</strong></td> 
                     <td nowrap="nowrap">
                         <asp:Label ID="lblMemo" runat="server"></asp:Label>
                     </td> 
                 </tr>
                <tr class="Controls">
                    <td class="th">&nbsp;</td>
                    <td nowrap="nowrap">
                        <br />
                         <asp:GridView ID="dgvItem" runat="server" AutoGenerateColumns="false" 
                         CellPadding="5" CellSpacing="0" AllowPaging="false" PagerSettings-Visible="false" AllowCustomPaging="false"
                         UseAccessibleHeader="true" Width="100%">
                         <HeaderStyle CssClass="Caption" Font-Bold="true" />
                         <RowStyle CssClass="Row"/>
                         <AlternatingRowStyle CssClass="Row"/>
                         <Columns>
                             <asp:BoundField DataField="PODeliveryPlanDetailId_PK" HeaderText="ID" Visible="false"></asp:BoundField>
                             <asp:BoundField DataField="MaterialServiceText" HeaderText="Item Name"></asp:BoundField>
                             <asp:BoundField DataField="AdditionalNotes" HeaderText="Notes Postel"></asp:BoundField>
                         </Columns>
                         </asp:GridView>
                    </td>
                </tr>
            </table>
              <br />
              
            <table cellspacing="0" cellpadding="5" width="100%">
                <tr class="Controls">
                   <td colspan="2" style="background-color:lightblue; text-align:center"><strong>SUBMIT</strong></td>
                </tr>
                <tr class="Controls">
                    <td class="th" width="30%" nowrap="nowrap"><strong>Memo Approval</strong></td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtMemo" runat="server" Columns="50" CssClass="ControlsIN" MaxLength="18" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Controls">
                    <td class="th" colspan="2" nowrap="nowrap" style="text-align:center">
                        <asp:Button ID="btnApprove" runat="server" BackColor="Green" CssClass="Button" OnClick="btnApprove_Click" OnClientClick="return clickAppr('APPROVE')" Text="Approve" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnReject" runat="server" BackColor="Red" CssClass="Button" OnClick="btnReject_Click" OnClientClick="return clickAppr('REJECT')" Text="Reject" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="Button" OnClick="btnBack_Click" Text="Back" />
                    </td>
                </tr>
              <tr class="Bottom">
                  <td align="center" colspan="3">
                      &nbsp; &nbsp; &nbsp;&nbsp;
                      <asp:TextBox ID="txtPODelivPlanTechApprId" runat="server" CssClass="ControlsIN" Visible="False"></asp:TextBox>
                  </td>
              </tr>
          </table>
          </td>
     </tr>
     </table>

    </asp:Panel>
    </td>
  </tr>
</table>
      
    </td>
  </tr>
</table>
    
</form>
</body>
</html>