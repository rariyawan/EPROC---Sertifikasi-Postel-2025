using Company.Apps.Utility;
using Company.Apps.WsDataPIPS;
using Company.Apps.WsEmail;
using Company.Apps.WsWorkflowEngine;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevExpress.Web.ASPxHtmlEditor.Internal;
using DevExpress.XtraRichEdit.Layout.Export;
using DotNet.Highcharts.Helpers;
using ExcelLibrary.BinaryFileFormat;
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
using System.Xml.Linq;

public partial class procurement_PODetailSpecificationCert : System.Web.UI.Page
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
        lblMessage.Visible = false;

        DataSet dsData = null;
        DataTable dtData0 = null;
        DataTable dtData1 = null;


        dsData = WsData.RunSQL("begin " +
                            "declare " +
                            "@rowamount numeric, " +
                            "@pagecount numeric; " +
                            "exec pips.stp_GetPODetailSpecificationCert_fetch " +
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


    protected void dgvList_RowDataBound(Object sender, GridViewRowEventArgs e)
    {

        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strId = e.Row.Cells[0].Text;

            Label lblPO = (Label)e.Row.Cells[1].FindControl("lblPO");
            Label lblSpec = (Label)e.Row.Cells[2].FindControl("lblSpec");
            Label lblCert = (Label)e.Row.Cells[2].FindControl("lblCert");

            /*
            string strParamDetail = GetRequest.EncryptQueryString("PODetailSpecificationCertId=" + strId);
            lblDetail.Text = "<a href=\"javascript:clickDetail('" + strParamDetail + "')\"><img border=0 src='../images/table.gif'></a>";
            */

            lblPO.Text = e.Row.Cells[4].Text + "<br><b>Vendor : </b>" + e.Row.Cells[8].Text;
            
            lblSpec.Text = "<b>Item PO :</b> " + e.Row.Cells[5].Text + "<br><b>Kode Spek :</b> " + e.Row.Cells[6].Text + "<br><b>Nama Barang/Jasa :</b> " + e.Row.Cells[7].Text;

            string strURLCert = string.Empty;
            string strURLJust = string.Empty;
            if (e.Row.Cells[13].Text.Trim() != "" && e.Row.Cells[13].Text.Trim() != "&nbsp;")
            {
                strURLCert = GetRequest.EncryptQueryString("NamaFile=" +  e.Row.Cells[13].Text);
                strURLCert = "<a href=\"javascript:toView('" + strURLCert + "')\"><u>Download File</u></a>";
            }
            if (e.Row.Cells[15].Text.Trim() != "" && e.Row.Cells[15].Text.Trim() != "&nbsp;")
            {
                strURLJust = GetRequest.EncryptQueryString("NamaFile=" +  e.Row.Cells[15].Text);
                strURLJust = "<a href=\"javascript:toView('" + strURLJust + "')\"><u>Download File</u></a>";
            }


            lblCert.Text = "<b>No. Sertifikat Postel :</b> " + e.Row.Cells[9].Text + "<br>" +
                           "<b>URL SDPPI :</b> " + e.Row.Cells[10].Text + "<br>" +
                           "<b>Masa Berlaku :</b> " + e.Row.Cells[11].Text + " s/d " + e.Row.Cells[12].Text + "<br>" +
                           "<b>File Sertifikat :</b> " + strURLCert + "<br>" +
                           "<b>Keterangan :</b> " + e.Row.Cells[14].Text + "<br><br>" +
                           "<b>File Justifikasi Sertifikat :</b> " + strURLJust + "<br><br>";

            e.Row.Cells[7].Text = e.Row.Cells[7].Text + "<br>" + e.Row.Cells[11].Text;
        }

        e.Row.Cells[0].Visible = false;
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[6].Visible = false;
        e.Row.Cells[7].Visible = false;
        e.Row.Cells[8].Visible = false;
        e.Row.Cells[9].Visible = false;
        e.Row.Cells[10].Visible = false;
        e.Row.Cells[11].Visible = false;
        e.Row.Cells[12].Visible = false;
        e.Row.Cells[13].Visible = false;
        e.Row.Cells[14].Visible = false;
        e.Row.Cells[15].Visible = false;


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


}
