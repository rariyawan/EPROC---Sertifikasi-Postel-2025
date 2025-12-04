using Company.Apps.Utility;
using Company.Apps.WcfDataPIPS;
using Company.Apps.WcfMembershipProvider;
using Company.Apps.WsEmail;
using DevExpress.Xpo.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Vendor_PODetailCertRequest : System.Web.UI.Page
{
    ClsUtility ClsUtil = new ClsUtility();
    WcfDataPIPSClient WsDataPipsData = new WcfDataPIPSClient();
	
	protected void Page_Load(object sender, EventArgs e)
    {
		int intVendorId = Convert.ToInt32(Session["VendorId"].ToString());
		if (!IsPostBack)
        {
			ClsUtil.CheckSession();

			ViewData(1);
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

        dsData = WsDataPipsData.RunSQL(ClsCryptoEngine.Encrypt("begin " +
                            "declare " +
                            "@rowamount numeric, " +
                            "@pagecount numeric; " +
                            "exec pips.stp_GetPODetailCertRequestVendor_fetch " +
                            "'" + txtSearch.Text.Trim() + "', " +
                            "'" + Session["UserName"] + "', " +
                            "" + Session["VendorId"] + ", " +
                            "" + intCurrPage + ", " +
                            "20, " +
                            "@rowamount out, " +
                            "@pagecount out; " +
                            "end;"));
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

        DataSet dsData = WsDataPipsData.RunSQL(ClsCryptoEngine.Encrypt("begin " +
                            "exec pips.stp_GetPODetailCertRequestVendor_info " + intRequestId.ToString() + "; " +
                            "end;"));
        DataTable dtData = dsData.Tables[0];

        txtRequestId.Text = intRequestId.ToString();    
        lblRequestNumber.Text = dtData.Rows[0]["RequestNumber"].ToString();
        lblRequestDate.Text = dtData.Rows[0]["RequestDate"].ToString();
        lblTaskDate.Text = dtData.Rows[0]["TaskDate"].ToString();
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
        btnSubmit.Visible = true;
        btnSave.Visible = true;
        pnlVendor.Visible = true;

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

        DataSet dsV = WsDataPipsData.RunSQL(ClsCryptoEngine.Encrypt("select " +
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
                                "order by a.PODetailCertRequestVendorId_PK desc"));
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
                string strUploadExist = WsDataPipsData.ExecuteScalar(ClsCryptoEngine.Encrypt("select " +
                                                        "isnull(InternalUploadFilePath,'') " +
                                                        "from " +
                                                        "pips.tblT_PODetailCertificateRequest a " +
                                                        "where " +
                                                        "a.PODetailCertificateRequestId_PK=" + txtRequestId.Text));
            
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

                string strPathUpload = WsDataPipsData.ExecuteScalar(ClsCryptoEngine.Encrypt("select " +
                                                    "(case when isnull(a.GoodsStdId_FK,0) > 0 then 'PATHSP_GR_DOC' else 'PATHSP_SA_DOC' end) " +
                                                    "from " +
                                                    "pips.tblT_PODetailCertificateRequest a " +
                                                    "where " +
                                                    "a.PODetailCertificateRequestId_PK=" + txtRequestId.Text));
		                            
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

                string strPathUpload2 = WsDataPipsData.ExecuteScalar(ClsCryptoEngine.Encrypt("select " +
                                                    "(case when isnull(a.GoodsStdId_FK,0) > 0 then 'PATHSP_GR_DOC' else 'PATHSP_SA_DOC' end) " +
                                                    "from " +
                                                    "pips.tblT_PODetailCertificateRequest a " +
                                                    "where " +
                                                    "a.PODetailCertificateRequestId_PK=" + txtRequestId.Text));
		                            
		        //upload file
		        DocURL2 = UploadHelper.uploadToFlatSP(strFileName2, strPathUpload2, txtCertJustificationUpload.PostedFile.InputStream);
            }

            string strPostelSave = WsDataPipsData.ExecuteScalar(ClsCryptoEngine.Encrypt("begin " +
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
                                                    "end;"));

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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strReturn = WsDataPipsData.ExecuteScalar(ClsCryptoEngine.Encrypt("begin " +
                                            "declare " +
                                            "@strOutMsg varchar(max); " +
                                            "exec pips.stp_PODetailCertificateVendorSubmit " +
                                            "" + txtRequestId.Text + ",'" + txtNotesVendor.Text.Trim().Replace("'","") + "','" + Session["UserName"] + "', " +
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
            ViewData(1);
        }
    }

    protected void lnkSame_Click(object sender, EventArgs e)
    {
         string strReturn = WsDataPipsData.ExecuteScalar(ClsCryptoEngine.Encrypt("begin " +
                                            "declare " +
                                            "@strOutMsg varchar(max); " +
                                            "exec pips.stp_PODetailCertificateRequestDataAuto " +
                                            "" + txtRequestId.Text + ",'AUTO','" + Session["UserName"] + "', " +
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
            ViewDetail(Convert.ToInt64(txtRequestId.Text));
        }
    }

    protected void lnkReset_Click(object sender, EventArgs e)
    {
        string strReturn = WsDataPipsData.ExecuteScalar(ClsCryptoEngine.Encrypt("begin " +
                                            "declare " +
                                            "@strOutMsg varchar(max); " +
                                            "exec pips.stp_PODetailCertificateRequestDataAuto " +
                                            "" + txtRequestId.Text + ",'RESET','" + Session["UserName"] + "', " +
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
            ViewDetail(Convert.ToInt64(txtRequestId.Text));
        }
    }
	
}