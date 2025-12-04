using Company.Apps.Utility;
using Company.Apps.WcfDataPIPS;
using DevExpress.Web.ASPxHtmlEditor.Internal;
using DevExpress.Web.ASPxObjectContainer;
using DevExpress.XtraRichEdit.Model;
using DevExpress.XtraRichEdit.Model.History;
using ExcelLibrary.BinaryFileFormat;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WinSCP;

public partial class sourcing_AuctionPRCertPostelVendorConfirm : System.Web.UI.Page
{

    WcfDataPIPSClient WsData = new WcfDataPIPSClient();
    ClsUtility ClsUtil = new ClsUtility();

    string AUCTION_ID;

    protected void Page_Load(object sender, EventArgs e)
    {

        ClsUtil.CheckSession();

        AUCTION_ID = GetRequest.GetQueryString("AuctionId");

        LoadData();
    }

    protected void LoadData()
    {
        DataSet dsData = null;
        DataTable dtData = null;

        DataSet dsRow = null;
        DataTable dtRow = null;

        dsData = WsData.RunSQL(ClsCryptoEngine.Encrypt("select  " +
                                                    "a.AuctionPRId_PK, a.PurchaseRequestDetailId_FK, a.GoodsName, a.Quantity, a.UnitShortCode, isnull(c.CertificateConfirm,-1) as CertificateConfirm " +
                                                    "from pips.vw_AuctionPR2 a  " +
                                                    "inner join pips.tblT_PurchaseRequestDetail b with (nolock) on a.PurchaseRequestDetailId_FK=b.PurchaseRequestDetailId_PK " +
                                                    "left join ( " +
                                                    "    select " +
                                                    "    x.AuctionPRId_FK, " +
                                                    "    x.CertificateConfirm " +
                                                    "    from " +
                                                    "    pips.tblT_AuctionPRVendorCertConfirm x " +
                                                    "    where " +
                                                    "    x.VendorId_FK=" + Session["VendorId"] + " " +
                                                    ") c on a.AuctionPRId_PK=c.AuctionPRId_FK " +
                                                    "where  " +
                                                    "a.AuctionId_FK=" + AUCTION_ID + " " +
                                                    "and b.IsPostelCertificate=1"));
        dtData = dsData.Tables[0];

        string strData = string.Empty;
        strData = "<table cellspacing='1' cellpadding='3' width='100%' border='0'> " + 
                    "<tr class='PortalRow'>" +
                    "  <td valign='top'><b>Reff No</b></td>" +
                    "  <td valign='top'><b>Item Request</b></td>" +
                    "  <td valign='top'><b>Qty</b></td>" +
                    "  <td valign='top'><b>UoM</b></td>" +
                    "  <td valign='top'><b>Certificate Confirm</b></td>" +
                    "</tr>";

        int i;
        string strConfirm = string.Empty;
        string rdoConf = string.Empty;
        string rdoNotConf = string.Empty;
        for (i=0;i<dtData.Rows.Count;i++)
        {

            rdoConf = "";
            rdoNotConf = "";
            if (dtData.Rows[i]["CertificateConfirm"].ToString() == "1")
            {
                rdoConf = "checked";
                rdoNotConf = "";
            }
            if (dtData.Rows[i]["CertificateConfirm"].ToString() == "0")
            {
                rdoConf = "";
                rdoNotConf = "checked";
            }

            strConfirm = "<input type=radio name='rdoConfirm_" + dtData.Rows[i]["AuctionPRId_PK"].ToString() + "' id='rdoConfirm_" + dtData.Rows[i]["AuctionPRId_PK"].ToString() + "' value=1 " + rdoConf + ">Confirm" + "&nbsp;&nbsp;&nbsp;" +
                         "<input type=radio name='rdoConfirm_" + dtData.Rows[i]["AuctionPRId_PK"].ToString() + "' id='rdoConfirm_" + dtData.Rows[i]["AuctionPRId_PK"].ToString() + "' value=0 " + rdoNotConf + ">Not Confirm";

            strData = strData +
                "<tr class='PortalAltRow'>" +
                "  <td valign='top' width='5%' nowrap><font color=blue><b>" + dtData.Rows[i]["PurchaseRequestDetailId_FK"].ToString() + "</b></font></td>" +
                "  <td valign='top'><font color=blue><b>" + dtData.Rows[i]["GoodsName"].ToString() + "</b></font></td>" +
                "  <td valign='top'><font color=blue><b>" + dtData.Rows[i]["Quantity"].ToString() + "</b></font></td>" +
                "  <td valign='top'><font color=blue><b>" + dtData.Rows[i]["UnitShortCode"].ToString() + "</b></font></td>" +
                "  <td valign='top'><font color=blue><b>" + strConfirm + "</b></font></td>" +
                "</tr>";
        }
                  
        strData = strData + "</table> ";

        lblItem.Text = strData;

    }  
    
    protected void btnSaveItem_Click(object sender, EventArgs e)
    {
        string strCheck = string.Empty;
        string strAllCheck = string.Empty;
        DataSet dsData = null;
        DataTable dtData = null;

        dsData = WsData.RunSQL(ClsCryptoEngine.Encrypt("select  " +
                                                    "a.AuctionPRId_PK, a.PurchaseRequestDetailId_FK, a.GoodsName, a.Quantity, a.UnitShortCode " +
                                                    "from pips.vw_AuctionPR2 a  " +
                                                    "inner join pips.tblT_PurchaseRequestDetail b with (nolock) on a.PurchaseRequestDetailId_FK=b.PurchaseRequestDetailId_PK " +
                                                    "where  " +
                                                    "a.AuctionId_FK=" + AUCTION_ID + " " +
                                                    "and b.IsPostelCertificate=1"));
        dtData = dsData.Tables[0];
        int i;
        string strConfirm = string.Empty;
        string strArraySave = string.Empty;
        for (i=0;i<dtData.Rows.Count;i++)
        {
            strConfirm = Request["rdoConfirm_" + dtData.Rows[i]["AuctionPRId_PK"].ToString()];

            if (!string.IsNullOrEmpty(strConfirm))
            {
                strArraySave = strArraySave + dtData.Rows[i]["AuctionPRId_PK"].ToString() + "_" + strConfirm + ";";
            }
        }

        string strReturn = WsData.ExecuteScalar(ClsCryptoEngine.Encrypt("begin " +
                                            "declare " +
                                            "@strOutMsg varchar(max); " +
                                            "exec pips.stp_AuctionPRVendorCertConfirm " +
                                            "'" + strArraySave + "'," + AUCTION_ID + ",'" + Session["VendorId"] + "','" + Session["UserName"] + "', " +
                                            "@strOutMsg out;" +
                                            "select @strOutMsg;" +
                                            "end; "));

        string strOutMessage = strReturn;

        if (strOutMessage != "")
        {
            lblMessage.Visible = true;
            lblMessage.Text = strOutMessage;
        }
        else
        {
           string strParam = GetRequest.EncryptQueryString("AuctionId=" + AUCTION_ID);
           ClientScriptManager cs = Page.ClientScript;
		   Type cstype = this.GetType();
		   cs.RegisterStartupScript(cstype,null,"<script language='javascript'>alert('Data successfully saved');document.location.href='AuctionPRCertPostelVendorConfirm.aspx?" + strParam + "'</script>");
		   return;
        }

    }

}