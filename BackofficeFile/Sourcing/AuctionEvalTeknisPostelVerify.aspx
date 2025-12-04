<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuctionEvalTeknisPostelVerify.aspx.cs" Inherits="sourcing_AuctionEvalTeknisPostelVerify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@Register TagPrefix="pips" TagName="AuctionMasterInfo" Src="AuctionMasterInfoNoProcu.ascx"%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link rel="stylesheet" type="text/css" href="../Styles/pips/Style_doctype.css"/>
<script type="text/javascript" src="../js/calendarWidget.js"></script>
<script language="javascript" type="text/javascript" src="../js/jquery-1.2.3.js"></script>
<script language="javascript" type="text/javascript" src="../js/modal-window.js"></script>
<script language="javascript" type="text/javascript" src="../js/utils.js"></script>
<script language="javascript" type="text/javascript">
    function clickInfo(strParam) {
        openMyModal('../po/POMonitoringInfo.aspx?' + strParam, 1000, 450, "auto");
    }

    function showDetail(strParamDtlId) {
        var myfileName = '../Sourcing/AuctionPRInformation.aspx?ParamPRDetailId=' + strParamDtlId;

        openMyModal(myfileName, "800", "400", "auto");
    }

    function ShowItemDetail(strParamDtlId, strParamAuctionPRId) {
        var myfileName = '../Sourcing/AuctionFormPRDetailPostel.aspx?ParamPRDetailId=' + strParamDtlId + "&ParamAuctionPRId=" + strParamAuctionPRId;

        openMyModal(myfileName, "800", "400", "auto");
    }
</script>
</head>
<body style="MARGIN: 0px" marginheight="0" marginwidth="0">
<form id="form1" runat="server">
<table cellspacing="0" cellpadding="0" width="100%" border="0">
  <tr>
    <td><img border="0" src="../images/tab_s_on.gif"></td> 
    <td class="TabOn" background="../images/tab_on.gif" nowrap align="center">Technical Evaluation</td> 
    <td><img border="0" src="../images/tab_e_on.gif"></td> 
    <td background="../images/tab_back.gif" width="100%"></td> 
    <td width="100%"></td>
  </tr>
</table>
<table align="center">
  <tr>
    <td>&nbsp;</td> 
    <td><pips:AuctionMasterInfo id="AuctionMasterInfo" runat="server"/> 
      <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
          <td><img border="0" src="../images_tab/tab_s_off.gif"></td> 
          <td background="../images_tab/tab_off.gif" nowrap align="center"><asp:LinkButton ID="lnbData" runat="server" OnClick="lnbData_Click">Auction Data</asp:LinkButton></td> 
          <td><img border="0" src="../images_tab/tab_off_off.gif"></td> 
          <td background="../images_tab/tab_off.gif" nowrap align="center"><asp:LinkButton ID="lnbTech" runat="server" OnClick="lnbTech_Click">Technical Evaluation</asp:LinkButton></td> 
          <td><img border="0" src="../images_tab/tab_off_on.gif"></td> 
            <td background="../images_tab/tab_on.gif" nowrap align="center"><b>Verification Item Postel Certification</b></td> 
          <td><img border="0" src="../images_tab/tab_e_on.gif"></td> 
          <td background="../images_tab/tab_back.gif" width="100%"></td> 
          <td background="../images_tab/tab_back.gif"><a id="Link_Close" href="" runat="server"  ><img border="0" src="../images_tab/tab_close.gif"/></a></td> 
          <td><img border="0" src="../images_tab/tab_end.gif"></td>
        </tr>
      </table>

      <table width="100%">
        <tr>
        <td valign="top">
       
            <table class="Header" cellspacing="0" cellpadding="0" border="0">
              <tr>
                <td class="th" style="text-align:center"><strong>Purchasing Requisition</strong> </td> 
              </tr>
            </table>
            <table class="Grid" cellspacing="0" cellpadding="0">
              <tr class="Caption">
                 <th nowrap scope="col">&nbsp;Reff. No</th>
                 <th nowrap scope="col">&nbsp;PR. No</th>
                 <th nowrap scope="col">&nbsp;Material/Service</th>
                 <th nowrap scope="col" style="text-align:right">Quantity&nbsp;</th>
                 <th nowrap scope="col" style="text-align:right">Price&nbsp;</th>
                 <th nowrap scope="col" style="text-align:right">Total Price&nbsp;</th>
              </tr>   
              <asp:Literal id="dataPR" runat="server"/>
            </table>

        </td>
        </tr>
    </table>
      
    </td>
  </tr>
</table>
    
</form>
</body>
</html>