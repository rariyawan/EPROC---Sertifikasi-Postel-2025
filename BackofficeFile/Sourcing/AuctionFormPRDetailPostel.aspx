<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd"> <!--Doctype @1-EF883F90-->

<!--ASPX page @1-19C2A667-->
<%@ Page language="c#" CodeFile="AuctionFormPRDetailPostel.aspx.cs" AutoEventWireup="true" Inherits="pips.sourcing.AuctionFormPRDetailPostel.AuctionFormPRDetailPostelPage" ResponseEncoding ="windows-1252"%>

<%@ Import namespace="pips.sourcing.AuctionFormPRDetailPostel" %>
<%@ Import namespace="pips.Configuration" %>
<%@ Import namespace="pips.Data" %>

<%@Register TagPrefix="CC" Namespace="pips.Controls" %>
<html lang="en-us">
<head>
<meta name="GENERATOR" content="CodeCharge Studio 4.3.00.7676"><meta http-equiv="content-type" content="text/html; charset=windows-1252">

<title>Auction</title>


<link rel="stylesheet" type="text/css" href="../Styles/pips/Style_doctype.css">
<script type="text/javascript" src="../js/qs.js"></script>
<script type="text/javascript" src="../js/calendarWidget.js"></script>
<script lang="javascript" type="text/javascript" src="../js/jquery-1.2.3.js"></script>
<script lang="javascript" type="text/javascript" src="../js/modal-window.js"></script>


</head>
<script language="javascript" type="text/javascript">
    function checkPostel(obj) {
        /*
        if (obj.checked) {
            document.getElementById("DIV_POSTEL_NOTES").style.display = "";
        } else {
            document.getElementById("DIV_POSTEL_NOTES").style.display = "none";
            document.getElementById("txtPostelNotes").value = "";
        }
        */

        if (obj.value == "1") {
            document.getElementById("DIV_POSTEL_NOTES").style.display = "";
        } else {
            document.getElementById("DIV_POSTEL_NOTES").style.display = "none";
            document.getElementById("txtPostelNotes").value = "";
        }
    }
</script>
<body style="MARGIN: 0px">
<form runat="server">
    <table class="add_full-width-table"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
          <tr>
            <td>&nbsp;&nbsp;&nbsp;<br>
              <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
                <tr>
                    <td><img border="0" src="../images_tab/tab_s_on.gif" alt=""></td> 
                    <td class="add_tab-active"><b>SR Item Information</b></td> 
                    <td><img border="0" src="../images_tab/tab_e_on.gif" alt=""></td> 
                    <td class="add_tab-end">&nbsp;</td> 
                    <td><img border="0" src="../images_tab/tab_end.gif" alt=""></td>
                </tr>
              </table>
              </td> 
          </tr>
        </table>
        <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
          <tr>
            <td>
              <table class="Header add_table-top" style="DISPLAY: none"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
                <tr>
                  <td class="HeaderLeft"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td> 
                  <td class="th"><strong>Auction Master</strong></td> 
                  <td class="HeaderRight"><img border="0" alt="" src="../Styles/pips/Images/Spacer.gif"></td>
                </tr>
              </table>
 
              <table class="Record add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
                <asp:PlaceHolder id="pips_vw_AuctionError" visible="False" runat="server">
                    <tr class="Error">
                        <td colspan="4"><asp:Label ID="pips_vw_AuctionErrorLabel" runat="server"/></td>
                    </tr>
                </asp:PlaceHolder>
                    <tr class="Controls">                
                       <td class="th">Item Name</td>
                        <td><asp:Label id="lblItemName" maxlength="100" Columns="30" runat="server"/>&nbsp;</td> 
                    </tr>
                    <tr class="Controls">
                        <td class="th">Plant from User</td> 
                        <td><asp:Label id="pips_vw_PlantCode" maxLength="100" Columns="30" runat="server"/>&nbsp;- <asp:Label id="pips_vw_PlantName" maxlength="100" Columns="30"	runat="server"/></td>
                    </tr>    
                    <tr class="Controls">                
                       <td class="th">Delivery Address</td>
                        <td><asp:Label id="pips_vw_DeliveryAddress" maxlength="100" Columns="30" runat="server"/>&nbsp;</td> 
                    </tr>
                    <tr class="Controls">
                        <td class="th">Requisitioner</td>
                        <td><asp:Label id="pips_vw_Requisitioner" maxlength="100" Columns="30"	runat="server"/></td> 
                    </tr>
                    <tr class="Controls">
                        <td class="th">Purchase Group Code</td>
                        <td><asp:Label id="pips_vw_PurchaseGroupCode" maxlength="100" Columns="30"	runat="server"/>&nbsp;</td> 
                    </tr>
                    <tr class="Controls">  
                        <td class="th">Purchase Group Name</td>
                        <td><asp:Label id="pips_vw_PurchaseGroupName" maxlength="100" Columns="30"	runat="server"/></td> 
                    </tr>
                    <tr class="Controls">
                        <td class="th">Method</td>
                        <td><asp:Label id="pips_vw_Method" maxlength="100" Columns="30"	runat="server"/>&nbsp;</td>
                    </tr>
                    <tr class="Controls">     
                        <td class="th">Brand</td>
                        <td><asp:Label id="pips_vw_Brand" maxlength="100" Columns="30"	runat="server"/>&nbsp;</td>
                     </tr>
                     <tr class="Controls"> 
                       <td class="th">Item Info</td>
                        <td><asp:Label id="pips_vw_ItemInfo" maxlength="100" Columns="30"	runat="server"/>&nbsp;</td>
                    </tr> 
                    <tr class="Controls"> 
                       <td class="th">Price Reference</td>
                        <td><asp:Label id="lblPriceReference" runat="server"/>&nbsp;</td>
                    </tr> 
                      <tr class="Controls">
                        <td class="th">Postel Certification</td> 
                        <td colspan="3">

                             <asp:RadioButtonList ID="rdoPostel" runat="server" RepeatDirection="Horizontal">
                                 <asp:ListItem Value="1" onclick="checkPostel(this)">Yes</asp:ListItem>
                                 <asp:ListItem Value="0" onclick="checkPostel(this)">No</asp:ListItem>
                             </asp:RadioButtonList>
                            <br />
                            <div ID="DIV_POSTEL_NOTES" runat="server" style="display:none">
                Notes :<br />
                            <asp:TextBox id="txtPostelNotes" onkeypress="return imposeMaxLength(this, 512);" onpaste="return maxLengthPaste(this, 512);" CssClass="ControlsIN" Columns="50" Rows="5" TextMode="MultiLine" runat="server"/>
                            </div>
        
                        </td>
                    </tr>
                     <tr class="Bottom">
                      <td colspan="2" style="TEXT-ALIGN:left">
                          <asp:TextBox ID="txtPRDetailId" style="display:none" runat="server"></asp:TextBox>
                          <asp:Button ID="btnConfirm" CssClass="Button" runat="server" Text="Confirm and Save" OnClick="btnConfirm_Click" />
                      </td>
                    </tr>
                </table>
                </td>
            </tr>
        </table>
</form>
</body>
</html>



