<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageV2.master" AutoEventWireup="true" CodeFile="PODetailCertRequest.aspx.cs" Inherits="Vendor_PODetailCertRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">
    <script type="text/javascript" src="../Common/js/calendarWidget.js"></script>
    <script lang="javascript" type="text/javascript" src="../common/js/modal-window.js"></script>
    <script language="javascript" type="text/javascript">


        function toView(filePath) {
            window.open('../invoice/InvoiceDokView.aspx?' + filePath, 'OpenFile', 'width=800,height=550,toolbar=no,resizable=yes');
        }

        function clickSame() {
            return confirm("Are you sure you want to make the data the same as the current data ?");
        }

        function clickReset() {
            return confirm("Are you sure reset current data ?");
        }

        function clickSubmit() {
            return confirm("Are you sure to Submit Certificate ?")
        }

    </script>
   <asp:Label ID="lblTitle" runat="server"></asp:Label>
   <asp:Label ID="lblBR" runat="server"></asp:Label>
     <table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td valign="top">
          <asp:Panel id="pnlFilter" runat="server">
          <table cellspacing="0" cellpadding="0">
            <tr>
              <td>Cari &nbsp;&nbsp; </td> 
              <td><asp:TextBox ID="txtSearch" runat="server" Columns="32" 
                      CssClass="PortalInput"></asp:TextBox>
                  &nbsp;</td> 
              <td colspan="4" style="text-align: right">&nbsp; 
  <asp:Button ID="btnFilter" runat="server" CssClass="PortalButton" Text="Find" 
        onclick="btnFilter_Click" />
    &nbsp;&nbsp;</td>
            </tr>
          </table>
          </asp:Panel>
        </td>
      </tr>
    </table>
    <br />
   <table id="tableCert" runat="server" width="99%" border="0" cellspacing="0" cellpadding="0">
       <tr>
           <td>
               <asp:Panel id="pnlGrid" runat="server">
               <table border="0" cellspacing="0" cellpadding="0" width="100%">
                   <tr>
                      <td><img border="0" src="../Common/Images/bar_left.gif"></td> 
                      <td class="PortalBarMain" width="100%"><strong>Postel Certificate Process</strong></td> 
                      <td><img border="0" src="../Common/Images/bar_right.gif"></td>
                   </tr>
               </table>
               <asp:GridView ID="dgvList" runat="server" AutoGenerateColumns="false" 
                    CellPadding="5" CellSpacing="0" PageSize="20" AllowPaging="false" PagerSettings-Visible="false" AllowCustomPaging="false"
                    UseAccessibleHeader="true" Width="100%" OnRowCommand="dgvList_RowCommand" OnRowDataBound="dgvList_RowDataBound">
                    <HeaderStyle CssClass="PortalCaption" />
                    <RowStyle CssClass="PortalRow"/>
                    <AlternatingRowStyle CssClass="PortalRow"/>
                <Columns>
                    <asp:BoundField DataField="PODetailCertificateRequestId_PK" HeaderText="ID"></asp:BoundField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="2%" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            #
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="imbDetail" runat="server" ImageUrl="~/Common/Images/table.gif" CommandName="Detail" CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RequestNumber" HeaderText="Request Number" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                    <asp:BoundField DataField="RequestDate" HeaderText="Request Date" ItemStyle-VerticalAlign="Top" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="TaskDate" HeaderText="Task Date" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                                Description
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div style="white-space: normal; word-wrap: break-word;">
                                <%# Eval("RequestDesc") %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RequestStatusName" HeaderText="Status" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                    <asp:BoundField DataField="PONumber" HeaderText="PO number" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                    <asp:BoundField DataField="SpecificationCode" HeaderText="Spec. Code" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                    <asp:BoundField DataField="MaterialServiceText" HeaderText="Item Name" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                    <asp:BoundField DataField="RequestStatus" HeaderText="RequestStatus" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                    <asp:BoundField DataField="VendorName" HeaderText="VendorName" ItemStyle-VerticalAlign="Top"></asp:BoundField>
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
                                     <td><img border="0" src="../Common/Images/bar_left.gif"></td> 
                                    <td class="PortalBarMain" width="100%"><strong>Postel Certificate Process</strong></td> 
                                    <td><img border="0" src="../Common/Images/bar_right.gif"></td>
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr>
                              <td valign="top">
            
                                <table cellspacing="0" cellpadding="5" width="100%">
                                    <tr>
                                        <td style="width:30%"><strong>Request Number</strong></td>
                                        <td nowrap="nowrap">
                                            <asp:Label ID="lblRequestNumber" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="display:none">
                                        <td><strong>Request Date</strong></td>
                                        <td nowrap="nowrap">
                                            <asp:Label ID="lblRequestDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Task Date</strong></td> 
                                        <td nowrap="nowrap">
                                            <asp:Label ID="lblTaskDate" runat="server"></asp:Label>
                                        </td> 
                                    </tr>
                                    <tr>
                                        <td><strong>Description</strong></td> 
                                        <td nowrap="nowrap">
                                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                        </td> 
                                    </tr>
                                    <tr>
                                        <td><strong>PO Number / Vendor</strong></td> 
                                        <td nowrap="nowrap">
                                            <asp:Label ID="lblPONumber" runat="server"></asp:Label>
                                            &nbsp;/
                                            <asp:Label ID="lblVendorName" runat="server"></asp:Label>
                                        </td> 
                                    </tr>
                                    <tr>
                                        <td><strong>Spec. Code / Item Name</strong></td>
                                        <td nowrap="nowrap">
                                            <asp:Label ID="lblSpecCode" runat="server"></asp:Label>
                                            &nbsp;/
                                            <asp:Label ID="lblItemName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Status</strong></td>
                                        <td nowrap="nowrap">
                                            <asp:Label ID="lblRequestStatus" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                  <br />
                                  <asp:Label ID="lblMessage" ForeColor="Red" runat="server" Font-Size="Medium"></asp:Label>
                                <table cellspacing="0" cellpadding="5" width="100%">
                                    <tr>
                                       <td colspan="2" width="50%" style="background-color:lightblue; text-align:center"><strong>CERTIFICATE DATA CURRENT</strong></td>
                                        <td colspan="2" style="background-color:lightblue; text-align:center"><strong>CERTIFICATE DATA NEW</strong></td>
                                    </tr>
                                    <tr>
                                        <td style="width:15%"><strong>Certificate Number</strong></td>
                                        <td>
                                            <asp:Label ID="lblPrev_CertNo" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:15%"><strong>Certificate Number</strong></td>
                                        <td nowrap="nowrap">

                                            <asp:TextBox ID="txtCertNo" runat="server" Columns="32" CssClass="PortalInput"></asp:TextBox>

                                            &nbsp;
                                            <asp:LinkButton ID="lnkSame" runat="server" OnClientClick="return clickSame()" OnClick="lnkSame_Click"><u>Same as current</u></asp:LinkButton>&nbsp;&nbsp;|&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkReset" runat="server" OnClientClick="return clickReset()" OnClick="lnkReset_Click"><u>Reset</u></asp:LinkButton>

                                         </td>
                                  </tr>
                                  <tr>
                                      <td><strong>SDPPI Online Certificate URL</strong></td>
                                      <td>
                                          <asp:Label ID="lblPrev_CertSDPPI" runat="server"></asp:Label>
                                      </td>
                                      <td><strong>SDPPI Online Certificate URL</strong></td>
                                      <td nowrap="nowrap">
                                          <asp:TextBox ID="txtCertSDPPI" runat="server" Columns="64" CssClass="PortalInput"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td><strong>Period</strong></td>
                                      <td>
                                          <asp:Label ID="lblPrev_CertStart" runat="server"></asp:Label>
                                          &nbsp; to&nbsp;
                                          <asp:Label ID="lblPrev_CertEnd" runat="server"></asp:Label>
                                      </td>
                                      <td><strong>Period</strong></td>
                                      <td nowrap="nowrap">
                                          <asp:TextBox ID="txtCertFrom" runat="server" Columns="15" CssClass="PortalInput"></asp:TextBox>
                                          <img onclick="scwShow(document.getElementById('cphContent_txtCertFrom'),event);" border="0" src="../images/calendar.gif" runat="server" ID="imgFrom">
                                          &nbsp;to
                                          <asp:TextBox ID="txtCertTo" runat="server" Columns="15" CssClass="PortalInput"></asp:TextBox>
                                          <img onclick="scwShow(document.getElementById('cphContent_txtCertTo'),event);" border="0" src="../images/calendar.gif" runat="server" ID="imgTo">
                                      </td>
                                  </tr>
                                  <tr>
                                      <td><strong>Upload File Certificate</strong></td>
                                      <td>
                                          <asp:Label ID="lblPrev_DocURL" runat="server"></asp:Label>
                                      </td>
                                      <td><strong>Upload File Certificate</strong></td>
                                      <td nowrap="nowrap">
                                          <asp:FileUpload ID="txtCertFileUpload" runat="server" CssClass="PortalInput" Width="260px"/>
                                          &nbsp;&nbsp;
                                          <asp:Label ID="lblDownloadCert" runat="server"></asp:Label>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td><strong>Additional Notes</strong></td>
                                      <td style="vertical-align:top">
                                          <asp:Label ID="lblPrev_AddNotes" runat="server"></asp:Label>
                                      </td>
                                      <td><strong>Additional Notes</strong></td>
                                      <td nowrap="nowrap">
                                          <asp:TextBox ID="txtCertNotes" runat="server" Columns="50" CssClass="PortalInput" MaxLength="18" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td><strong>Upload Justification File</strong><br />&nbsp;<em>* if only certificate is valid forever</em></td>
                                      <td>
                                          <asp:Label ID="lblPrev_JustificationURL" runat="server"></asp:Label>
                                      </td>
                                      <td nowrap="nowrap"><strong>Upload Justification File</strong><br /> <em>* if only previous certificate is valid forever</em></td>
                                      <td nowrap="nowrap">
                                          <asp:FileUpload ID="txtCertJustificationUpload" runat="server" CssClass="PortalInput" Width="260px" />
                                          &nbsp;&nbsp;
                                          <asp:Label ID="lblDownloadJust" runat="server"></asp:Label>
                                      </td>
                                  </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td nowrap="nowrap"><strong>Update By</strong></td>
                                        <td nowrap="nowrap">
                                            <asp:Label ID="lblUpdateBy" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" nowrap="nowrap" style="text-align:right">
                                            <asp:Button ID="btnSave" runat="server" CssClass="PortalButton" OnClick="btnSave_Click" OnClientClick="return clickSave()" Text="Save" />
                                            &nbsp;
                                            <asp:Button ID="btnBack" runat="server" CssClass="PortalButton" OnClick="btnBack_Click" Text="Back" />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <asp:Panel ID="pnlVendor" runat="server">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2"  style="background-color:lightblue; text-align:center" nowrap="nowrap"><b>SUBMIT CERTIFICATE</b></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td nowrap="nowrap"><strong>Submit Notes</strong></td>
                                        <td nowrap="nowrap">
                                            <asp:TextBox ID="txtNotesVendor" runat="server" Columns="50" CssClass="PortalInput" MaxLength="18" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" nowrap="nowrap" style="text-align:center">
                                            <asp:Button ID="btnSubmit" runat="server" BackColor="Green" CssClass="PortalButton" OnClick="btnSubmit_Click" OnClientClick="return clickSubmit()" Text="Submit Certificate" />
                                        </td>
                                    </tr>
                                    </asp:Panel>
                                    <tr><td colspan="2"><br /></td><td colspan="2"><br /></td></tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2" style="text-align:center" nowrap="nowrap"><b>REQUEST TO VENDOR HISTORY</b></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2"nowrap="nowrap">
                                            <asp:Label ID="lblVendorHist" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                  <tr class="Bottom">
                                      <td align="center" colspan="4">
                                          &nbsp; &nbsp; &nbsp;&nbsp;
                                          <asp:TextBox ID="txtRequestId" runat="server" CssClass="PortalInput" Visible="False"></asp:TextBox>
                                      </td>
                                  </tr>
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
   
</asp:Content>