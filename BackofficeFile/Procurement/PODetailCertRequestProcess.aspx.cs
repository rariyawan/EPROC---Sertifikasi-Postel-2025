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

public partial class procurement_PODetailCertRequestProcess : System.Web.UI.Page
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
                            "exec pips.stp_GetPODetailCertRequestProcess_fetch " +
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
        lnkSame.Visible = true;
        lnkReset.Visible = true;
        btnRequestVendor.Visible = true;
        btnSave.Visible = true;
        btnSendIntan.Visible = true;
        pnlVendor.Visible = true;
        if (dtData.Rows[0]["RequestStatus"].ToString() == "2" || dtData.Rows[0]["RequestStatus"].ToString() == "3" || dtData.Rows[0]["RequestStatus"].ToString() == "4")
        {
            txtCertNo.Enabled = false;
            txtCertSDPPI.Enabled = false;
            txtCertNotes.Enabled = false;
            txtCertFileUpload.Enabled = false;
            txtCertJustificationUpload.Enabled = false;
            imgFrom.Visible = false;
            imgTo.Visible = false;
            lnkSame.Visible = false;
            lnkReset.Visible = false;
            btnRequestVendor.Visible = false;
            btnSave.Visible = false;
            btnSendIntan.Visible = false;
            pnlVendor.Visible = false;
        }

        lblUpdateBy.Text = "";
        if (!string.IsNullOrEmpty(txtCertNo.Text))
        {
            lblUpdateBy.Text = dtData.Rows[0]["UpdateBy"].ToString() + " # " + dtData.Rows[0]["UpdateDate"].ToString();
        }

        txtNotesVendor.Text = "";
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
            string strStatusCode = e.Row.Cells[10].Text;

            e.Row.Cells[7].Text = e.Row.Cells[7].Text + "<br>" + e.Row.Cells[11].Text;
        }

        e.Row.Cells[0].Visible = false;
        e.Row.Cells[10].Visible = false;
        e.Row.Cells[11].Visible = false;

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strErrorMessage = string.Empty;
        if (!string.IsNullOrEmpty(txtCertNo.Text.Trim()))
        {
            if (string.IsNullOrEmpty(txtCertSDPPI.Text.Trim()) || string.IsNullOrEmpty(txtCertFrom.Text.Trim()) || string.IsNullOrEmpty(txtCertTo.Text.Trim()))
            {   
                strErrorMessage = "Please Complete All Certificate's Info<br>";
            }
            else
            {
                string strUploadExist = WsData.ExecuteScalar("select " +
                                                        "isnull(InternalUploadFilePath,'') " +
                                                        "from " +
                                                        "pips.tblT_PODetailCertificateRequest a " +
                                                        "where " +
                                                        "a.PODetailCertificateRequestId_PK=" + txtRequestId.Text);
            
                if (string.IsNullOrEmpty(strUploadExist) && txtCertFileUpload.PostedFile.ContentLength == 0)
                {
                     strErrorMessage = "Please Complete All Certificate's Info<br>";
                }
            }
        } 
        else
        {
            if (txtCertFileUpload.PostedFile != null && txtCertFileUpload.PostedFile.ContentLength > 0)
            {

                string fx = Path.GetExtension(txtCertFileUpload.PostedFile.FileName);

                if (fx.ToLower() != ".pdf")
                {
                    strErrorMessage = "File Upload certificate must be .pdf<br>";
                }
            }
        }

        
        if (txtCertJustificationUpload.PostedFile != null && txtCertJustificationUpload.PostedFile.ContentLength > 0)
        {

            string fx2 = Path.GetExtension(txtCertJustificationUpload.PostedFile.FileName);

            if (fx2.ToLower() != ".pdf")
            {
                strErrorMessage = "File Upload Justification must be .pdf<br>";
            }
        }

        if (!string.IsNullOrEmpty(strErrorMessage))
        {
            lblMessage.Visible = true;
            lblMessage.Text = strErrorMessage;
        }
        else
        {
            string DocURL = string.Empty;
            if (txtCertFileUpload.PostedFile != null && txtCertFileUpload.PostedFile.ContentLength > 0) { 

                //saving + naming file
		        string strFileName;
		        strFileName = Path.GetFileName(txtCertFileUpload.PostedFile.FileName);
                strFileName = "Dok_POSTEL_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf"; // + strFileName.Replace(" ", "_");
		
                string fx = Path.GetExtension(txtCertFileUpload.PostedFile.FileName);
                string ftype = txtCertFileUpload.PostedFile.ContentType;
                string fsize = txtCertFileUpload.PostedFile.ContentLength.ToString();

                string strPathUpload = WsData.ExecuteScalar("select " +
                                                    "(case when isnull(a.GoodsStdId_FK,0) > 0 then 'PATHSP_GR_DOC' else 'PATHSP_SA_DOC' end) " +
                                                    "from " +
                                                    "pips.tblT_PODetailCertificateRequest a " +
                                                    "where " +
                                                    "a.PODetailCertificateRequestId_PK=" + txtRequestId.Text);
		                            
		        //upload file
		        DocURL = UploadHelper.uploadToFlatSP(strFileName, strPathUpload, txtCertFileUpload.PostedFile.InputStream);
            }

            string DocURL2 = string.Empty;
            if (txtCertJustificationUpload.PostedFile != null && txtCertJustificationUpload.PostedFile.ContentLength > 0) { 

                //saving + naming file
		        string strFileName2;
		        strFileName2 = Path.GetFileName(txtCertJustificationUpload.PostedFile.FileName);
                strFileName2 = "Doc_POSTELJUSTIFIKASI_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf"; // + strFileName.Replace(" ", "_");
		
                string fx2 = Path.GetExtension(txtCertJustificationUpload.PostedFile.FileName);
                string ftype2 = txtCertJustificationUpload.PostedFile.ContentType;
                string fsize2 = txtCertJustificationUpload.PostedFile.ContentLength.ToString();

                string strPathUpload2 = WsData.ExecuteScalar("select " +
                                                    "(case when isnull(a.GoodsStdId_FK,0) > 0 then 'PATHSP_GR_DOC' else 'PATHSP_SA_DOC' end) " +
                                                    "from " +
                                                    "pips.tblT_PODetailCertificateRequest a " +
                                                    "where " +
                                                    "a.PODetailCertificateRequestId_PK=" + txtRequestId.Text);
		                            
		        //upload file
		        DocURL2 = UploadHelper.uploadToFlatSP(strFileName2, strPathUpload2, txtCertJustificationUpload.PostedFile.InputStream);
            }

            string strPostelSave = WsData.ExecuteScalar("begin " +
                                                    "declare " +
                                                    "@strOutMessage varchar(max); " +
                                                    "exec pips.stp_PODetailCertificateRequestSave " +
                                                    "    " + txtRequestId.Text + ", " +
                                                    "    '" + txtCertNo.Text.Trim() + "', " +
                                                    "    '" + txtCertSDPPI.Text.Trim() + "', " +
                                                    "    '" + txtCertFrom.Text.Trim() + "', " +
                                                    "    '" + txtCertTo.Text.Trim() + "', " +
                                                    "    '" + DocURL + "', " +
                                                    "    '" + DocURL2 + "', " +
                                                    "    '" + txtCertNotes.Text.Trim().Replace("'","") + "', " +
                                                    "    '" + Session["UserName"].ToString() + "', " +
                                                    "    @strOutMessage out; " +
                                                    "select @strOutMessage; " +
                                                    "end;");

            if (strPostelSave != "")
            {
                lblMessage.Visible = true;
                lblMessage.Text = strPostelSave;
            }
            else
            {
                
                ViewDetail(Convert.ToInt64(txtRequestId.Text));
                ClientScriptManager cs = Page.ClientScript;
			    Type cstype = this.GetType();
			    cs.RegisterStartupScript(cstype,null,"<script language='javascript'>alert('Data successfully saved');</script>");
			    return;
            }
        }

        
    }

    protected void btnRequestVendor_Click(object sender, EventArgs e)
    {
        string strReturn = WsData.ExecuteScalar("begin " +
                                            "declare " +
                                            "@strOutMsg varchar(max); " +
                                            "exec pips.stp_PODetailCertificateRequestToVendor " +
                                            "" + txtRequestId.Text + ",'" + txtNotesVendor.Text.Trim().Replace("'","") + "','" + ClsAuthSession.USERNAME + "', " +
                                            "@strOutMsg out;" +
                                            "select @strOutMsg;" +
                                            "end; ");
        string strOutMessage = strReturn;

        if (strOutMessage != "")
        {
            lblMessage.Visible = true;
            lblMessage.Text = strOutMessage;
        }
        else
        {
            ViewDetail(Convert.ToInt64(txtRequestId.Text));
        }
    }

    protected void lnkSame_Click(object sender, EventArgs e)
    {
         string strReturn = WsData.ExecuteScalar("begin " +
                                            "declare " +
                                            "@strOutMsg varchar(max); " +
                                            "exec pips.stp_PODetailCertificateRequestDataAuto " +
                                            "" + txtRequestId.Text + ",'AUTO','" + ClsAuthSession.USERNAME + "', " +
                                            "@strOutMsg out;" +
                                            "select @strOutMsg;" +
                                            "end; ");
        string strOutMessage = strReturn;

        if (strOutMessage != "")
        {
            lblMessage.Visible = true;
            lblMessage.Text = strOutMessage;
        }
        else
        {
            ViewDetail(Convert.ToInt64(txtRequestId.Text));
        }
    }

    protected void lnkReset_Click(object sender, EventArgs e)
    {
        string strReturn = WsData.ExecuteScalar("begin " +
                                            "declare " +
                                            "@strOutMsg varchar(max); " +
                                            "exec pips.stp_PODetailCertificateRequestDataAuto " +
                                            "" + txtRequestId.Text + ",'RESET','" + ClsAuthSession.USERNAME + "', " +
                                            "@strOutMsg out;" +
                                            "select @strOutMsg;" +
                                            "end; ");
        string strOutMessage = strReturn;

        if (strOutMessage != "")
        {
            lblMessage.Visible = true;
            lblMessage.Text = strOutMessage;
        }
        else
        {
            ViewDetail(Convert.ToInt64(txtRequestId.Text));
        }
    }

    protected void btnSendIntan_Click(object sender, EventArgs e)
    {

        // start SAVE

        string strErrorMessage = string.Empty;
        if (!string.IsNullOrEmpty(txtCertNo.Text.Trim()))
        {
            if (string.IsNullOrEmpty(txtCertSDPPI.Text.Trim()) || string.IsNullOrEmpty(txtCertFrom.Text.Trim()) || string.IsNullOrEmpty(txtCertTo.Text.Trim()))
            {   
                strErrorMessage = "Please Complete All Certificate's Info<br>";
            }
            else
            {
                string strUploadExist = WsData.ExecuteScalar("select " +
                                                        "isnull(InternalUploadFilePath,'') " +
                                                        "from " +
                                                        "pips.tblT_PODetailCertificateRequest a " +
                                                        "where " +
                                                        "a.PODetailCertificateRequestId_PK=" + txtRequestId.Text);
            
                if (string.IsNullOrEmpty(strUploadExist) && txtCertFileUpload.PostedFile.ContentLength == 0)
                {
                     strErrorMessage = "Please Complete All Certificate's Info<br>";
                }
            }
        } 
        else
        {
            if (txtCertFileUpload.PostedFile != null && txtCertFileUpload.PostedFile.ContentLength > 0)
            {

                string fx = Path.GetExtension(txtCertFileUpload.PostedFile.FileName);

                if (fx.ToLower() != ".pdf")
                {
                    strErrorMessage = "File Upload certificate must be .pdf<br>";
                }
            }
        }

        
        if (txtCertJustificationUpload.PostedFile != null && txtCertJustificationUpload.PostedFile.ContentLength > 0)
        {

            string fx2 = Path.GetExtension(txtCertJustificationUpload.PostedFile.FileName);

            if (fx2.ToLower() != ".pdf")
            {
                strErrorMessage = "File Upload Justification must be .pdf<br>";
            }
        }

        if (!string.IsNullOrEmpty(strErrorMessage))
        {
            lblMessage.Visible = true;
            lblMessage.Text = strErrorMessage;
        }
        else
        {
            string DocURL = string.Empty;
            if (txtCertFileUpload.PostedFile != null && txtCertFileUpload.PostedFile.ContentLength > 0) { 

                //saving + naming file
		        string strFileName;
		        strFileName = Path.GetFileName(txtCertFileUpload.PostedFile.FileName);
                strFileName = "Dok_POSTEL_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf"; // + strFileName.Replace(" ", "_");
		
                string fx = Path.GetExtension(txtCertFileUpload.PostedFile.FileName);
                string ftype = txtCertFileUpload.PostedFile.ContentType;
                string fsize = txtCertFileUpload.PostedFile.ContentLength.ToString();

                string strPathUpload = WsData.ExecuteScalar("select " +
                                                    "(case when isnull(a.GoodsStdId_FK,0) > 0 then 'PATHSP_GR_DOC' else 'PATHSP_SA_DOC' end) " +
                                                    "from " +
                                                    "pips.tblT_PODetailCertificateRequest a " +
                                                    "where " +
                                                    "a.PODetailCertificateRequestId_PK=" + txtRequestId.Text);
		                            
		        //upload file
		        DocURL = UploadHelper.uploadToFlatSP(strFileName, strPathUpload, txtCertFileUpload.PostedFile.InputStream);
            }

            string DocURL2 = string.Empty;
            if (txtCertJustificationUpload.PostedFile != null && txtCertJustificationUpload.PostedFile.ContentLength > 0) { 

                //saving + naming file
		        string strFileName2;
		        strFileName2 = Path.GetFileName(txtCertJustificationUpload.PostedFile.FileName);
                strFileName2 = "Doc_POSTELJUSTIFIKASI_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf"; // + strFileName.Replace(" ", "_");
		
                string fx2 = Path.GetExtension(txtCertJustificationUpload.PostedFile.FileName);
                string ftype2 = txtCertJustificationUpload.PostedFile.ContentType;
                string fsize2 = txtCertJustificationUpload.PostedFile.ContentLength.ToString();

                string strPathUpload2 = WsData.ExecuteScalar("select " +
                                                    "(case when isnull(a.GoodsStdId_FK,0) > 0 then 'PATHSP_GR_DOC' else 'PATHSP_SA_DOC' end) " +
                                                    "from " +
                                                    "pips.tblT_PODetailCertificateRequest a " +
                                                    "where " +
                                                    "a.PODetailCertificateRequestId_PK=" + txtRequestId.Text);
		                            
		        //upload file
		        DocURL2 = UploadHelper.uploadToFlatSP(strFileName2, strPathUpload2, txtCertJustificationUpload.PostedFile.InputStream);
            }

            string strPostelSave = WsData.ExecuteScalar("begin " +
                                                    "declare " +
                                                    "@strOutMessage varchar(max); " +
                                                    "exec pips.stp_PODetailCertificateRequestSave " +
                                                    "    " + txtRequestId.Text + ", " +
                                                    "    '" + txtCertNo.Text.Trim() + "', " +
                                                    "    '" + txtCertSDPPI.Text.Trim() + "', " +
                                                    "    '" + txtCertFrom.Text.Trim() + "', " +
                                                    "    '" + txtCertTo.Text.Trim() + "', " +
                                                    "    '" + DocURL + "', " +
                                                    "    '" + DocURL2 + "', " +
                                                    "    '" + txtCertNotes.Text.Trim().Replace("'","") + "', " +
                                                    "    '" + Session["UserName"].ToString() + "', " +
                                                    "    @strOutMessage out; " +
                                                    "select @strOutMessage; " +
                                                    "end;");

            if (strPostelSave != "")
            {
                lblMessage.Visible = true;
                lblMessage.Text = strPostelSave;
            }
            else
            {

                 DataSet dsSendCert = WsData.RunSQL("begin " +
                                                    "declare " +
                                                    "@strOutResult varchar(max), " +
                                                    "@strOutJson varchar(max), " +
                                                    "@strOutMessage varchar(max); " +
                                                    "exec pips.stp_APIToINTAN_PostelCertRegistration " +
                                                    "'" + ClsAuthSession.USERNAME + "', " + txtRequestId.Text + ", " +
                                                    "@strOutResult out, " +
                                                    "@strOutJson out, " +
                                                    "@strOutMessage out; " +
                                                    "select @strOutResult as strOutResult,@strOutJson as strOutJson,@strOutMessage as strOutMessage; " +
                                                    "end; ");
                 DataTable dtSendCert = dsSendCert.Tables[0];

                 if (dtSendCert.Rows.Count > 0)
                 {
                    if (dtSendCert.Rows[0]["strOutResult"].ToString() == "false")
                    {

                        DataSet dsMsg = WsData.RunSQL("select StringValue from pips.parseJSON('" + dtSendCert.Rows[0]["strOutJson"].ToString().Replace('"', '\"') + "') where parent_ID=1 and KeyName is null");
                        DataTable dtMsg = dsMsg.Tables[0];
                        int m;
                        string strMsg = string.Empty;
                        for (m = 0; m < dtMsg.Rows.Count; m++)
                        {
                            strMsg = strMsg + dtMsg.Rows[m]["StringValue"].ToString() + "; ";
                        }

                        lblMessage.Visible = true;
                        lblMessage.Text = strMsg;
                    }
                    else
                    {
                        ViewDetail(Convert.ToInt64(txtRequestId.Text));
                        ClientScriptManager cs = Page.ClientScript;
			            Type cstype = this.GetType();
			            cs.RegisterStartupScript(cstype,null,"<script language='javascript'>alert('Data successfully send to INTAN');</script>");
			            return;
                    }
                 }
                
            }
        }

        // end SAVE
    }
}
