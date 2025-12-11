<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuctionEvalTeknisPostelVerify.aspx.cs" Inherits="sourcing_AuctionEvalTeknisPostelVerify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@Register TagPrefix="pips" TagName="AuctionMasterInfo" Src="AuctionMasterInfoNoProcu.ascx"%>
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
<body style="MARGIN: 0px">
<form id="form1" runat="server">
<table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
  <tr>
    <td><img border="0" src="../images_tab/tab_s_on.gif" alt=""></td> 
    <td class="add_tab-active"><b>Technical Evaluation</b></td> 
    <td><img border="0" src="../images_tab/tab_e_on.gif" alt=""></td> 
    <td class="add_tab-end">&nbsp;</td> 
    <td><img border="0" src="../images_tab/tab_end.gif" alt=""></td>
  </tr>
</table>
<table class="add_table-center"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
  <tr>
    <td>&nbsp;</td> 
    <td><pips:AuctionMasterInfo id="AuctionMasterInfo" runat="server"/> 
      <table class="add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
        <tr>
          <td><img border="0" src="../images_tab/tab_s_off.gif" alt=""></td> 
          <td class="add_tab-inactive"><asp:LinkButton ID="lnbData" runat="server" OnClick="lnbData_Click">Auction Data</asp:LinkButton></td> 
          <td><img border="0" src="../images_tab/tab_off_off.gif" alt=""></td> 
          <td class="add_tab-inactive"><asp:LinkButton ID="lnbTech" runat="server" OnClick="lnbTech_Click">Technical Evaluation</asp:LinkButton></td> 
          <td><img border="0" src="../images_tab/tab_off_on.gif" alt=""></td> 
          <td class="add_tab-active"><b>Verification Item Postel Certification</b></td> 
          <td><img border="0" src="../images_tab/tab_e_on.gif" alt=""></td> 
          <td class="add_tab-end"></td> 
          <td class="add_tab-back"><a id="Link_Close" href="" runat="server"  ><img border="0" src="../images_tab/tab_close.gif" alt=""/></a></td> 
          <td><img border="0" src="../images_tab/tab_end.gif" alt=""></td>
        </tr>
      </table>

      <table class="add_full-width-table"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
        <tr>
        <td>
       
            <table class="Header add_table-top"><caption style="display:none">Data</caption><tr style="display:none"><th></th></tr>
              <tr>
                <td class="th" style="text-align:center"><strong>Purchasing Requisition</strong> </td> 
              </tr>
            </table>
            <table class="Grid add_table-top"><caption style="display:none">Data</caption>
              <tr class="Caption">
                 <th class="add_no-wrap" scope="col">&nbsp;Reff. No</th>
                 <th class="add_no-wrap" scope="col">&nbsp;PR. No</th>
                 <th class="add_no-wrap" scope="col">&nbsp;Material/Service</th>
                 <th class="add_no-wrap" scope="col" style="text-align:right">Quantity&nbsp;</th>
                 <th class="add_no-wrap" scope="col" style="text-align:right">Price&nbsp;</th>
                 <th class="add_no-wrap" scope="col" style="text-align:right">Total Price&nbsp;</th>
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