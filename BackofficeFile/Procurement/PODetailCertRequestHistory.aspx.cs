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

public partial class procurement_PODetailCertRequestHistory : System.Web.UI.Page
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

            if (!string.IsNullOrEmpty(GetRequest.GetQueryString("LinkFromMaster")))
            {
                
                if (!string.IsNullOrEmpty(GetRequest.GetQueryString("S_STATUSID")))
                {
                    string strStatusId = GetRequest.GetQueryString("S_STATUSID");
                    ddlStatus.SelectedValue = strStatusId;
                }

                if (!string.IsNullOrEmpty(GetRequest.GetQueryString("S_SEARCHTEXT")))
                {
                    txtSearch.Text = GetRequest.GetQueryString("S_SEARCHTEXT");
                }


                ViewData(Convert.ToInt32(GetRequest.GetQueryString("PageFromMaster")));
            }
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

       
        string strStatusId = ddlStatus.SelectedValue;


        dsData = WsData.RunSQL("begin " +
                            "declare " +
                            "@rowamount numeric, " +
                            "@pagecount numeric; " +
                            "exec pips.stp_GetPODetailCertRequestHistory_fetch " +
                            "'" + txtSearch.Text.Trim() + "', " +
                            "" + strStatusId + ", " +
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


    protected void ViewDetail(long intRequestId)
    {
        pnlGrid.Visible = false;
        pnlFilter.Visible = false;
        pnlPaging.Visible = false;
        pnlForm.Visible = true;

        lblMessage.Text = "";
        lblMessage.Visible = false;

        DataSet dsData = WsData.RunSQL("begin " +
                            "exec pips.stp_GetPODetailCertRequestProcess_info " + intRequestId.ToString() + "; " +
                            "end;");
        DataTable dtData = dsData.Tables[0];

        txtRequestId.Text = intRequestId.ToString();    
        lblRequestNumber.Text = dtData.Rows[0]["RequestNumber"].ToString();
        lblRequestDate.Text = dtData.Rows[0]["RequestDate"].ToString();
        lblDispatchDate.Text = dtData.Rows[0]["DispatchDate"].ToString();
        lblDescription.Text = dtData.Rows[0]["RequestDesc"].ToString();
        lblPONumber.Text = dtData.Rows[0]["PONumber"].ToString();
        lblVendorName.Text = dtData.Rows[0]["VendorName"].ToString();
        lblSpecCode.Text = dtData.Rows[0]["SpecificationCode"].ToString();
        lblItemName.Text = dtData.Rows[0]["MaterialServiceText"].ToString();
        lblRequestStatus.Text = dtData.Rows[0]["RequestStatusName"].ToString();

        lblPrev_CertNo.Text = dtData.Rows[0]["Prev_CertNumber"].ToString();
        lblPrev_CertSDPPI.Text = dtData.Rows[0]["Prev_URLSDPPI"].ToString();
        lblPrev_CertStart.Text = dtData.Rows[0]["Prev_StartPeriod"].ToString();
        lblPrev_CertEnd.Text = dtData.Rows[0]["Prev_EndPeriod"].ToString();
        lblPrev_AddNotes.Text = dtData.Rows[0]["Prev_AddNotes"].ToString();

        lblPrev_DocURL.Text = "";
        if (!string.IsNullOrEmpty(dtData.Rows[0]["Prev_UploadFilePath"].ToString())) { 
            string strURL = GetRequest.EncryptQueryString("NamaFile=" + dtData.Rows[0]["Prev_UploadFilePath"].ToString());
            lblPrev_DocURL.Text = "<a href=\"javascript:toView('" + strURL + "')\"><u>Download File</u></a>";
        }

        lblPrev_JustificationURL.Text = "";
        if (!string.IsNullOrEmpty(dtData.Rows[0]["Prev_JustificationFilePath"].ToString())) { 
            string strURL2 = GetRequest.EncryptQueryString("NamaFile=" + dtData.Rows[0]["Prev_JustificationFilePath"].ToString());
            lblPrev_JustificationURL.Text = "<a href=\"javascript:toView('" + strURL2 + "')\"><u>Download File</u></a>";
        }


        txtCertNo.Text = dtData.Rows[0]["CertificateNumber"].ToString();
        txtCertSDPPI.Text = dtData.Rows[0]["CertificateURL"].ToString();
        txtCertFrom.Text = dtData.Rows[0]["PeriodStart"].ToString();
        txtCertFrom.Attributes.Add("readOnly", "true");
        txtCertTo.Text = dtData.Rows[0]["PeriodEnd"].ToString();
        txtCertTo.Attributes.Add("readOnly", "true");
        txtCertNotes.Text = dtData.Rows[0]["AdditionalNotes"].ToString();
        lblDownloadCert.Text = "";
        if (!string.IsNullOrEmpty(dtData.Rows[0]["InternalUploadFilePath"].ToString())) { 
            string strURL = GetRequest.EncryptQueryString("NamaFile=" + dtData.Rows[0]["InternalUploadFilePath"].ToString());
            lblDownloadCert.Text = "<a href=\"javascript:toView('" + strURL + "')\"><u>Download File</u></a>";
        }
        lblDownloadJust.Text = "";
        if (!string.IsNullOrEmpty(dtData.Rows[0]["JustificationFilePath"].ToString())) { 
            string strURL = GetRequest.EncryptQueryString("NamaFile=" + dtData.Rows[0]["JustificationFilePath"].ToString());
            lblDownloadJust.Text = "<a href=\"javascript:toView('" + strURL + "')\"><u>Download File</u></a>";
        }

        txtCertNo.Enabled = true;
        txtCertSDPPI.Enabled = true;
        txtCertNotes.Enabled = true;
        txtCertFileUpload.Enabled = true;
        txtCertJustificationUpload.Enabled = true;
        imgFrom.Visible = true;
        imgTo.Visible = true;
        //if (dtData.Rows[0]["RequestStatus"].ToString() == "2" || dtData.Rows[0]["RequestStatus"].ToString() == "3" || dtData.Rows[0]["RequestStatus"].ToString() == "4")
        //{
            txtCertNo.Enabled = false;
            txtCertSDPPI.Enabled = false;
            txtCertNotes.Enabled = false;
            txtCertFileUpload.Enabled = false;
            txtCertJustificationUpload.Enabled = false;
            imgFrom.Visible = false;
            imgTo.Visible = false;
        //}

        lblUpdateBy.Text = "";
        if (!string.IsNullOrEmpty(txtCertNo.Text))
        {
            lblUpdateBy.Text = dtData.Rows[0]["UpdateBy"].ToString() + " # " + dtData.Rows[0]["UpdateDate"].ToString();
        }

        lblVendorHist.Text = "-";

        lblVendorHist.Text = getRequestVendorHistory(Convert.ToInt64(txtRequestId.Text));
    }

    protected string getRequestVendorHistory(long intRequestId)
    {
        string strRet = string.Empty;

        strRet += "<table cellspacing='0' cellpadding='5' width='100%'>";
        strRet += "<tr>";
        strRet += "<td width='20%'><b>Time</b></td>";
        strRet += "<td width='30%'><b>By</b></td>";
        strRet += "<td><b>Notes</b></td>";

        DataSet dsV = WsData.RunSQL("select " +
                                "format(a.CreationDate,'dd-MMM-yyyy HH:mm:ss') as TaskDate, " +
                                "a.RequestStatus, " +
                                "a.RequestNotes, " +
                                "a.CreatedBy, " +
                                "(case a.RequestStatus when 3 then  " +
                                "  (select x.FullName+' ('+ x.UserName +')' from pips.tblM_User x where x.userName=a.CreatedBy) " +
                                "else " +
                                "  a.CreatedBy " +
                                "end) as PICTask " +
                                "from " +
                                "pips.tblT_PODetailCertRequestVendor a " +
                                "where " +
                                "a.PODetailCertificateRequestId_FK=" + intRequestId.ToString() + " " +
                                "order by a.PODetailCertRequestVendorId_PK desc");
        DataTable dtV = dsV.Tables[0];  
        if (dtV.Rows.Count == 0)
        {
             strRet += "<tr>";
             strRet += "<td colspan='3'>No History</td>";
             strRet += "</tr>";
        }
        else
        {
            for (var v=0;v<=dtV.Rows.Count-1;v++)
            {
                strRet += "<tr>";
                strRet += "<td>" + dtV.Rows[v]["TaskDate"].ToString() + "</td>";
                strRet += "<td>" + dtV.Rows[v]["PICTask"].ToString() + "</td>";
                strRet += "<td>" + dtV.Rows[v]["RequestNotes"].ToString() + "</td>";
                strRet += "</tr>";
            }
        }

        strRet += "</tr>";
        strRet += "</table>";

        return strRet;
    }


    protected void dgvList_RowDataBound(Object sender, GridViewRowEventArgs e)
    {

        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strRequestId = e.Row.Cells[0].Text;
            string strStatusCode = e.Row.Cells[11].Text;

            e.Row.Cells[8].Text = e.Row.Cells[8].Text + "<br>" + e.Row.Cells[12].Text;
        }

        e.Row.Cells[0].Visible = false;
        e.Row.Cells[11].Visible = false;
        e.Row.Cells[12].Visible = false;

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

}
