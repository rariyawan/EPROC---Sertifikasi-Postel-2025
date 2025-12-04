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

public partial class procurement_PODetailCertRequestDispatch : System.Web.UI.Page
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
        lblMessage.Visible = false;

        DataSet dsData = null;
        DataTable dtData0 = null;
        DataTable dtData1 = null;

       
        string strStatusId = ddlStatus.SelectedValue;


        dsData = WsData.RunSQL("begin " +
                            "declare " +
                            "@rowamount numeric, " +
                            "@pagecount numeric; " +
                            "exec pips.stp_GetPODetailCertRequestDispatch_fetch " +
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


    protected void dgvList_RowDataBound(Object sender, GridViewRowEventArgs e)
    {

        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strRequestId = e.Row.Cells[0].Text;
            string strStatusCode = e.Row.Cells[10].Text;

            Label lblDetail = (Label)e.Row.Cells[1].FindControl("lblDetail");
            Label lblPIC = (Label)e.Row.Cells[6].FindControl("lblPIC");
            
            /*
            string strParamDetail = GetRequest.EncryptQueryString("PODetailCertificateRequestId=" + strRequestId);
            lblDetail.Text = "<a href=\"javascript:clickDetail('" + strParamDetail + "')\"><img border=0 src='../images/table.gif'></a>";
            */

            

            DataSet dsPIC = WsData.RunSQL("select  " +
                                "a.UserName, a.EmployeeNo, a.FullName " +
                                "from pips.tblm_user a " +
                                "inner join pips.tblM_UserRole b on a.UserId_PK=b.UserId_FK " +
                                "inner join pips.tblM_Role c on b.RoleId_FK=c.RoleId_PK " +
                                "where  " +
                                "b.IsActive='Y' " +
                                "and pips.trunc(getdate()) between pips.trunc(b.ValidFrom) and pips.trunc(isnull(b.ValidTo,getdate()+1)) " +
                                "and c.RoleCode='SAGR_PROCUREMENT'");
            DataTable dtPIC = dsPIC.Tables[0];
            string strPIC = string.Empty;

            if ( strStatusCode != "0")
            {
                if (strStatusCode != "4")
                {
                    string strName = WsData.ExecuteScalar("select " +
                                                    "isnull(max(a.PICFullName),'-') " +
                                                    "from " +
                                                    "pips.tblT_PODetailCertRequestPIC a " +
                                                    "where " +
                                                    "a.PODetailCertificateRequestId_FK=" + strRequestId);
                    strPIC = strName;
                }

                lblDetail.Text = "";
            }
            else
            {

                lblDetail.Text = "<input type=checkbox name='chkDisp' id='chkDisp' value='" + strRequestId + "'>";

                strPIC = strPIC + "<option value=''>- Select -</option>";
                for (var p=0;p<dtPIC.Rows.Count;p++)
                {
                    strPIC = strPIC + "<option value='" + dtPIC.Rows[p]["UserName"].ToString() + "'>" + dtPIC.Rows[p]["FullName"].ToString() + "</option>";
                }

           
                strPIC = "<select class='ControlsIN' name='cboDispatchTo_" + strRequestId + "' id='cboDispatchTo_" + strRequestId + "'>" +
                            strPIC + 
                         "</select>";
            }  

            lblPIC.Text = strPIC;

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


    protected void btnDispatch_Click(object sender, EventArgs e)
    {
        //Response.Write(Request["chkDisp"]);
        //Response.End();

        string strParamDisp = string.Empty;
        string[] arrDisp = Request["chkDisp"].ToString().Split(',');
        for (var d=0;d<arrDisp.Length;d++)
        {
            strParamDisp = strParamDisp + arrDisp[d].ToString().Trim() + "|" + Request["cboDispatchTo_" + arrDisp[d].ToString().Trim()].ToString().Trim() + "#";
        }

        //Response.Write(strParamDisp);
        //Response.End();

        string strReturn = WsData.ExecuteScalar("begin " +
                                            "declare " +
                                            "@intOut numeric, @strOutMsg varchar(max); " +
                                            "exec pips.stp_PODetailCertRequestDispatchProcess " +
                                            "'" + strParamDisp + "','" + ClsAuthSession.USERNAME + "', " +
                                            "@intOut out, @strOutMsg out;" +
                                            "select convert(varchar(32), @intOut) + '#' + @strOutMsg;" +
                                            "end; ");
        string[] arrReturn = strReturn.Split('#');
        string strOutCode = arrReturn[0].ToString();
        string strOutMessage = arrReturn[1].ToString();

        if (strOutCode == "0")
        {
            lblMessage.Visible = true;
            lblMessage.Text = strOutMessage;
        }
        else
        {
            ClientScriptManager cs = Page.ClientScript;
			Type cstype = this.GetType();
			cs.RegisterStartupScript(cstype,null,"<script language='javascript'>alert('Data successfully dispatched');document.location.href='PODetailCertRequestDispatch.aspx?" + Request.QueryString.ToString() + "';</script>");
			return;
        }

    }
}
