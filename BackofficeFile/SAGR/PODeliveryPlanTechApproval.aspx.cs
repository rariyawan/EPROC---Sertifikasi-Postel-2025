using Company.Apps.Utility;
using Company.Apps.WsDataPIPS;
using Company.Apps.WsEmail;
using Company.Apps.WsWorkflowEngine;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevExpress.Web.ASPxHtmlEditor.Internal;
using DevExpress.Web.ASPxUploadControl;
using DevExpress.XtraRichEdit.Layout.Export;
using DotNet.Highcharts.Helpers;
using ExcelLibrary.BinaryFileFormat;
using ExcelLibrary.CompoundDocumentFormat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SAGR_PODeliveryPlanTechApproval : System.Web.UI.Page
{

    ClsUtility ClsUtil = new ClsUtility();
    WsDataPIPS WsData = new WsDataPIPS();
    WsWorkflowEngine WsWF = new WsWorkflowEngine();
    WsEmail wsemail = new WsEmail();

    string DELETE_CONFIRM_MSG = ClsUtility.MessageDeleteConfirm();

    protected void Page_Load(object sender, EventArgs e)
    {
        ClsUtil.CheckSession();
        if (!IsPostBack)
        {
            /*
            string strEncryptedString = Request.QueryString[ConfigurationManager.AppSettings["ParamEncrypt"].ToString()];
            string strDecryptedString = Server.UrlDecode(ClsCryptoEngine.Decrypt(strEncryptedString.Replace(" ", "+")).Replace("&amp;", "&"));
            Response.Write(strDecryptedString);
            */
            
            ViewData(1);
            //pnlGrid.Visible = false;
            //pnlPaging.Visible = false;
        }
    }
    

    protected void ViewData(int intCurrPage)
    {
        pnlGrid.Visible = true;
        pnlFilter.Visible = true;
        pnlPaging.Visible = true;
        pnlForm.Visible = false;
        lblMessage.Visible = false;

        DataSet dsData = null;
        DataTable dtData0 = null;
        DataTable dtData1 = null;


        dsData = WsData.RunSQL("begin " +
                            "declare " +
                            "@rowamount numeric, " +
                            "@pagecount numeric; " +
                            "exec pips.stp_PODeliveryPlanTechApproval_fetch " +
                            "0," +
                            "'" + txtSearch.Text.Trim() + "', " +
                            "'" + ClsAuthSession.USERNAME + "', " +
                            "" + intCurrPage + ", " +
                            "20, " +
                            "@rowamount out, " +
                            "@pagecount out; " +
                            "end;");
        dtData0 = dsData.Tables[0];
        dtData1 = dsData.Tables[1];
        dgvList.DataSource = dtData0;
        dgvList.DataBind();

        long intRowAmount = Convert.ToInt64(dtData1.Rows[0]["RowAmount"]);
        long intPageCount = Convert.ToInt64(dtData1.Rows[0]["PageCount"]);
        ViewState["pageCount"] = intPageCount;
        ViewState["currPage"] = intCurrPage;

        if (dtData0.Rows.Count == 0)
        {
            lblNoRow.Text = ClsUtility.MessageNoData();
            dgvList.Visible = false;
            lblNoRow.Visible = true;
        }
        else
        {
            dgvList.Visible = true;
            lblNoRow.Text = "";
            lblNoRow.Visible = false;
        }

        ClsUtil.LoadImagePaging(intCurrPage, 20, intPageCount, imbFirst, imbPrev, imbNext, imbLast, lblPaging);
        
    }

    protected void dgvList_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int index = Convert.ToInt32(e.CommandArgument) - (dgvList.PageSize * dgvList.PageIndex);
            GridViewRow row = dgvList.Rows[index];

            string strId = dgvList.Rows[index].Cells[0].Text;

            ViewDetail(Convert.ToInt64(strId));
        }
    }


    protected void ViewDetail(long intApprId)
    {
        pnlGrid.Visible = false;
        pnlFilter.Visible = false;
        pnlPaging.Visible = false;
        pnlForm.Visible = true;

        lblMessage.Text = "";
        lblMessage.Visible = false;

        DataSet dsData = WsData.RunSQL("begin " +
                            "declare " +
                            "@rowamount numeric, " +
                            "@pagecount numeric; " +
                            "exec pips.stp_PODeliveryPlanTechApproval_fetch " +
                            "" + intApprId + "," +
                            "'', " +
                            "'" + ClsAuthSession.USERNAME + "', " +
                            "1, " +
                            "20, " +
                            "@rowamount out, " +
                            "@pagecount out; " +
                            "end;");
        DataTable dtData = dsData.Tables[0];

        txtPODelivPlanTechApprId.Text = intApprId.ToString();    
        lblDONumber.Text = dtData.Rows[0]["DONumber"].ToString();
        lblDeliveryDate.Text = dtData.Rows[0]["DeliveryPlanDate"].ToString();
        lblPONumber.Text = dtData.Rows[0]["PONumber"].ToString();
        lblPODate.Text = dtData.Rows[0]["PODate"].ToString();
        lblPODescription.Text = dtData.Rows[0]["PODescription"].ToString();
        lblVendorName.Text = dtData.Rows[0]["VendorName"].ToString();
        lblTargetDelivery.Text = dtData.Rows[0]["TargetDeliveryDate"].ToString();
        lblRequester.Text = dtData.Rows[0]["Requester"].ToString();
        lblMemo.Text = dtData.Rows[0]["RequestNotes"].ToString();

        ViewItem(intApprId);

    }

    protected void ViewItem(long intApprId)
    {
     
        DataSet dsData = null;
        DataTable dtData = null;


        dsData = WsData.RunSQL("select " +
                        "a.PODeliveryPlanDetailId_PK, " +
                        "c.MaterialServiceText,  " +
                        "e.AdditionalNotes " +
                        "from " +
                        "pips.tblT_PODelivPlanTechAppr appr " +
                        "inner join pips.tblT_PODeliveryPlan do on appr.PODeliveryPlanId_FK=do.PODeliveryPlanId_PK " +
                        "inner join pips.tblT_PODeliveryPlanDetail a on do.PODeliveryPlanId_PK=a.PODeliveryPlanId_FK " +
                        "inner join pips.tblT_PurchaseOrderDtlDelivSched b with (nolock)  on a.PurchaseOrderDtlDelivSchedId_FK=b.PurchaseOrderDtlDelivSchedId_PK  " +
                        "inner join pips.tblT_PurchaseOrderDetail c with (nolock)  on b.PurchaseOrderDetailId_FK=c.PurchaseOrderDetailId_PK  " +
                        "inner join pips.tblT_PurchaseRequestDetail d with (nolock)  on c.PurchaseRequestDetailId_FK=d.PurchaseRequestDetailId_PK  " +
                        "left outer join pips.tblT_PODelivPlanDetailCert e with (nolock)  on a.PODeliveryPlanDetailId_PK=e.PODeliveryPlanDetailId_FK  " +
                        "where " +
                        "appr.PODelivPlanTechApprId_PK=" + intApprId.ToString() + " " +
                        "and d.IsPostelCertificate=1  " +
                        "and isnull(e.CertificateNumber,'')='' ");
        dtData = dsData.Tables[0];
        dgvItem.DataSource = dtData;
        dgvItem.DataBind();

    }

    protected void dgvList_RowDataBound(Object sender, GridViewRowEventArgs e)
    {

        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strApprId = e.Row.Cells[0].Text;

        }

        e.Row.Cells[0].Visible = false;

    }

    protected void imbNext_Click(object sender, ImageClickEventArgs e)
    {
        int intCurrPage = Convert.ToInt32(ViewState["currPage"]) + 1;
        ViewState["currPage"] = intCurrPage;
        ViewData(intCurrPage);
    }
    protected void imbLast_Click(object sender, ImageClickEventArgs e)
    {
        int intCurrPage = Convert.ToInt32(ViewState["pageCount"]);
        ViewState["currPage"] = intCurrPage;
        ViewData(intCurrPage);
    }
    protected void imbPrev_Click(object sender, ImageClickEventArgs e)
    {
        int intCurrPage = Convert.ToInt32(ViewState["currPage"]) - 1;
        ViewState["currPage"] = intCurrPage;
        ViewData(intCurrPage);
    }
    protected void imbFirst_Click(object sender, ImageClickEventArgs e)
    {
        int intCurrPage = 1;
        ViewState["currPage"] = intCurrPage;
        ViewData(intCurrPage);
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        int intCurrPage = 1;
        ViewState["currPage"] = intCurrPage;
        ViewData(intCurrPage);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = true;
        pnlFilter.Visible = true;
        pnlPaging.Visible = true;
        lblNoRow.Visible = true;
        pnlForm.Visible = false;
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string strReturn = WsData.ExecuteScalar("begin " +
                                            "declare " +
                                            "@strOutMsg varchar(max); " +
                                            "exec pips.stp_PODeliveryPlanTechDecision " +
                                            "" + txtPODelivPlanTechApprId.Text + ",'" + txtMemo.Text.Trim().Replace("'","") + "','APPROVE','" + ClsAuthSession.USERNAME + "', " +
                                            "@strOutMsg out;" +
                                            "select @strOutMsg;" +
                                            "end; ");
        string strOutMessage = strReturn;

        if (strOutMessage != "1")
        {
            lblMessage.Visible = true;
            lblMessage.Text = strOutMessage;
        }
        else
        {
            ClientScriptManager cs = Page.ClientScript;
			Type cstype = this.GetType();
			cs.RegisterStartupScript(cstype,null,"<script language='javascript'>alert('Data successfully APPROVED');document.location.href='PODeliveryPlanTechApproval.aspx'</script>");
			return;
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        string strReturn = WsData.ExecuteScalar("begin " +
                                            "declare " +
                                            "@strOutMsg varchar(max); " +
                                            "exec pips.stp_PODeliveryPlanTechDecision " +
                                            "" + txtPODelivPlanTechApprId.Text + ",'" + txtMemo.Text.Trim().Replace("'","") + "','REJECT','" + ClsAuthSession.USERNAME + "', " +
                                            "@strOutMsg out;" +
                                            "select @strOutMsg;" +
                                            "end; ");
        string strOutMessage = strReturn;

        if (strOutMessage != "1")
        {
            lblMessage.Visible = true;
            lblMessage.Text = strOutMessage;
        }
        else
        {
            ClientScriptManager cs = Page.ClientScript;
			Type cstype = this.GetType();
			cs.RegisterStartupScript(cstype,null,"<script language='javascript'>alert('Data successfully REJECTED');document.location.href='PODeliveryPlanTechApproval.aspx'</script>");
			return;
        }
    }
}
