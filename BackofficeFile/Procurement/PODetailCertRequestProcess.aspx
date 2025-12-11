<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODetailCertRequestProcess.aspx.cs" Inherits="procurement_PODetailCertRequestProcess" %>

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

    function clickSame() {
        return confirm("Are you sure you want to make the data the same as the current data ?");
    }

    function clickReset() {
        return confirm("Are you sure reset current data ?");
    }

    function clickVendor() {
        return confirm("Are you sure to Request Certificate to Vendor ?")
    }

    function clickIntan() {
        if (confirm("Are you sure to Send Certificate to INTAN ?")) {
            document.getElementById("btnSendIntan").style.display = "none";
            document.getElementById("textLoading").style.display = "";
            return true;
        } else {
            return false;
        }
    }
</script>
    
</head>
<body style="MARGIN: 0px">
<form id="form1" runat="server" enctype="multipart/form-data">
<table class="add_full-width-table"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
  <tr>
    <td>
        <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
        <tr>
          <td><img border="0" src="../images_tab/tab_s_on.gif" alt=""></td> 
          <td class="add_tab-active"><b>Postel Certificate Process</b></td> 
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
                      &nbsp;</td> 
                  <td class="th">Status&nbsp;&nbsp; </td> 
                  <td>
                      <asp:DropDownList ID="ddlStatus" CssClass="ControlsIN" runat="server">
                          <asp:ListItem Value="-1">- ALL STATUS -</asp:ListItem>
                          <asp:ListItem Value="1">PROCUREMENT PROCESS</asp:ListItem>
                          <asp:ListItem Value="3">VENDOR PROCESS</asp:ListItem>
                          <asp:ListItem Value="6">PROCUREMENT VERIFICATION</asp:ListItem>
                      </asp:DropDownList>&nbsp;&nbsp;<asp:Button ID="btnFilter" runat="server" CssClass="Button" Text="Find" onclick="btnFilter_Click" />&nbsp;&nbsp;
                  </td> 
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
                    Postel Certificate Process
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
            <asp:BoundField DataField="PODetailCertificateRequestId_PK" HeaderText="ID"></asp:BoundField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="2%" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                    #
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="imbDetail" runat="server" ImageUrl="~/Images/table.gif" CommandName="Detail" CommandArgument="<%# Container.DataItemIndex %>" />
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
            <asp:BoundField DataField="DispatchDate" HeaderText="Dispatch Date"></asp:BoundField>
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


 <table class="add_full-width-table"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
<tr>
  <td>
    <asp:Panel ID="pnlForm" runat="server">

      <table class="add_full-width-table"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
      <tr>
          <td>
              <table class="Header add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
                  <tr>
                  <td class="HeaderLeft"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td> 
                  <td class="th">
                          Postel Certificate Process
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
                    <td class="th" style="width:30%"><strong>Request Number</strong></td>
                    <td class="add_no-wrap">
                        <asp:Label ID="lblRequestNumber" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="Controls">
                    <td class="th"><strong>Request Date</strong></td>
                    <td class="add_no-wrap">
                        <asp:Label ID="lblRequestDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="Controls">
                    <td class="th"><strong>Dispatch Date</strong></td> 
                    <td class="add_no-wrap">
                        <asp:Label ID="lblDispatchDate" runat="server"></asp:Label>
                    </td> 
                </tr>
                <tr class="Controls">
                    <td class="th"><strong>Description</strong></td> 
                    <td class="add_no-wrap">
                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                    </td> 
                </tr>
                <tr class="Controls">
                    <td class="th"><strong>PO Number / Vendor</strong></td> 
                    <td class="add_no-wrap">
                        <asp:Label ID="lblPONumber" runat="server"></asp:Label>
                        &nbsp;/
                        <asp:Label ID="lblVendorName" runat="server"></asp:Label>
                    </td> 
                </tr>
                <tr class="Controls">
                    <td class="th"><strong>Spec. Code / Item Name</strong></td>
                    <td class="add_no-wrap">
                        <asp:Label ID="lblSpecCode" runat="server"></asp:Label>
                        &nbsp;/
                        <asp:Label ID="lblItemName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="Controls">
                    <td class="th"><strong>Status</strong></td>
                    <td class="add_no-wrap">
                        <asp:Label ID="lblRequestStatus" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
              <br />
              <asp:Label ID="lblMessage" ForeColor="Red" runat="server" Font-Size="Medium"></asp:Label>
            <table class="add_table-form"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
                <tr class="Controls">
                   <td colspan="2" style="background-color:lightblue; text-align:center; width: 50%"><strong>CERTIFICATE DATA CURRENT</strong></td>
                    <td colspan="2" style="background-color:lightblue; text-align:center"><strong>CERTIFICATE DATA NEW</strong></td>
                </tr>
                <tr class="Controls">
                    <td class="th" style="width:15%"><strong>Certificate Number</strong></td>
                    <td>
                        <asp:Label ID="lblPrev_CertNo" runat="server"></asp:Label>
                    </td>
                    <td class="th" style="width:15%"><strong>Certificate Number</strong></td>
                    <td class="add_no-wrap">

                        <asp:TextBox ID="txtCertNo" runat="server" Columns="32" CssClass="ControlsIN"></asp:TextBox>

                        &nbsp;
                        <asp:LinkButton ID="lnkSame" runat="server" OnClientClick="return clickSame()" OnClick="lnkSame_Click"><u>Same as current</u></asp:LinkButton>&nbsp;&nbsp;|&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkReset" runat="server" OnClientClick="return clickReset()" OnClick="lnkReset_Click"><u>Reset</u></asp:LinkButton>

                     </td>
              </tr>
              <tr class="Controls">
                  <td class="th"><strong>SDPPI Online Certificate URL</strong></td>
                  <td>
                      <asp:Label ID="lblPrev_CertSDPPI" runat="server"></asp:Label>
                  </td>
                  <td class="th"><strong>SDPPI Online Certificate URL</strong></td>
                  <td class="add_no-wrap">
                      <asp:TextBox ID="txtCertSDPPI" runat="server" Columns="64" CssClass="ControlsIN"></asp:TextBox>
                  </td>
              </tr>
              <tr class="Controls">
                  <td class="th"><strong>Period</strong></td>
                  <td>
                      <asp:Label ID="lblPrev_CertStart" runat="server"></asp:Label>
                      &nbsp; to&nbsp;
                      <asp:Label ID="lblPrev_CertEnd" runat="server"></asp:Label>
                  </td>
                  <td class="th"><strong>Period</strong></td>
                  <td class="add_no-wrap">
                      <asp:TextBox ID="txtCertFrom" runat="server" Columns="15" CssClass="ControlsIN"></asp:TextBox>
                      <img onclick="scwShow(document.getElementById('txtCertFrom'),event);" border="0" src="../images/calendar.gif" runat="server" ID="imgFrom" alt="">
                      &nbsp;to
                      <asp:TextBox ID="txtCertTo" runat="server" Columns="15" CssClass="ControlsIN"></asp:TextBox>
                      <img onclick="scwShow(document.getElementById('txtCertTo'),event);" border="0" src="../images/calendar.gif" runat="server" ID="imgTo" alt="">
                  </td>
              </tr>
              <tr class="Controls">
                  <td class="th"><strong>Upload File Certificate</strong></td>
                  <td>
                      <asp:Label ID="lblPrev_DocURL" runat="server"></asp:Label>
                  </td>
                  <td class="th"><strong>Upload File Certificate</strong></td>
                  <td class="add_no-wrap">
                      <asp:FileUpload ID="txtCertFileUpload" runat="server" CssClass="ControlsIN" Width="260px"/>
                      &nbsp;&nbsp;
                      <asp:Label ID="lblDownloadCert" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr class="Controls">
                  <td class="th"><strong>Additional Notes</strong></td>
                  <td style="vertical-align:top">
                      <asp:Label ID="lblPrev_AddNotes" runat="server"></asp:Label>
                  </td>
                  <td class="th"><strong>Additional Notes</strong></td>
                  <td class="add_no-wrap">
                      <asp:TextBox ID="txtCertNotes" runat="server" Columns="50" CssClass="ControlsIN" MaxLength="18" Rows="5" TextMode="MultiLine"></asp:TextBox>
                  </td>
              </tr>
              <tr class="Controls">
                  <td class="th"><strong>Upload Justification File</strong><br />&nbsp;<em>* if only certificate is valid forever</em></td>
                  <td>
                      <asp:Label ID="lblPrev_JustificationURL" runat="server"></asp:Label>
                  </td>
                  <td class="th add_no-wrap"><strong>Upload Justification File</strong><br /> <em>* if only previous certificate is valid forever</em></td>
                  <td class="add_no-wrap">
                      <asp:FileUpload ID="txtCertJustificationUpload" runat="server" CssClass="ControlsIN" Width="260px" />
                      &nbsp;&nbsp;
                      <asp:Label ID="lblDownloadJust" runat="server"></asp:Label>
                  </td>
              </tr>
                <tr class="Controls">
                    <td class="th">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="th add_no-wrap"><strong>Update By</strong></td>
                    <td class="add_no-wrap">
                        <asp:Label ID="lblUpdateBy" runat="server"></asp:Label>
                    </td>
                </tr>
                
                <tr class="Controls">
                    <td class="th">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="th add_no-wrap" colspan="2" style="text-align:right">
                        <asp:Button ID="btnSave" runat="server" CssClass="Button" OnClick="btnSave_Click" OnClientClick="return clickSave()" Text="Save" />
                        &nbsp;
                        <asp:Button ID="btnSendIntan" runat="server" BackColor="Yellow" CssClass="Button" ForeColor="Black" OnClick="btnSendIntan_Click" OnClientClick="return clickIntan()" Text="Submit to INTAN" />
                        <a id="textLoading" style="display:none">Please wait while loading ...</a>&nbsp;
                        &nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="Button" OnClick="btnBack_Click" Text="Back" />
                        <br />
                        <br />
                    </td>
                </tr>
                <asp:Panel ID="pnlVendor" runat="server">
                <tr class="Controls">
                    <td class="th">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="th add_no-wrap" colspan="2"  style="background-color:lightblue; text-align:center"><b>REQUEST TO VENDOR</b></td>
                </tr>
                <tr class="Controls">
                    <td class="th">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="th add_no-wrap"><strong>Notes to Vendor</strong></td>
                    <td class="add_no-wrap">
                        <asp:TextBox ID="txtNotesVendor" runat="server" Columns="50" CssClass="ControlsIN" MaxLength="18" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr class="Controls">
                    <td class="th">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="th add_no-wrap" colspan="2" style="text-align:center">
                        <asp:Button ID="btnRequestVendor" runat="server" BackColor="Green" CssClass="Button" OnClick="btnRequestVendor_Click" OnClientClick="return clickVendor()" Text="Request to Vendor" />
                    </td>
                </tr>
                </asp:Panel>
                <tr class="Controls"><td colspan="2"><br /></td><td colspan="2"><br /></td></tr>
                <tr class="Controls">
                    <td class="th">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="th add_no-wrap" colspan="2" style="text-align:center"><b>REQUEST TO VENDOR HISTORY</b></td>
                </tr>
                <tr class="Controls">
                    <td class="th">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="th add_no-wrap" colspan="2">
                        <asp:Label ID="lblVendorHist" runat="server"></asp:Label>
                    </td>
                </tr>
              <tr class="Bottom">
                  <td colspan="4">
                      &nbsp; &nbsp; &nbsp;&nbsp;
                      <asp:TextBox ID="txtRequestId" runat="server" CssClass="ControlsIN" Visible="False"></asp:TextBox>
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