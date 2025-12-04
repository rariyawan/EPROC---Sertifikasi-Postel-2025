using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Company.Apps.Utility;
using Company.Apps.WsDataPIPS;
using System.Configuration;
using System.Web.Script.Serialization;

public partial class sourcing_AuctionPRCertPostelVendorConfirm : System.Web.UI.Page
{

    ClsUtility ClsUtil = new ClsUtility();
    WsDataPIPS WsData = new WsDataPIPS();

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
            
             ViewData();
        }
    }
    

    protected void ViewData()
    {
        pnlGrid.Visible = true;
        pnlFilter.Visible = true;
        pnlPaging.Visible = true;

        DataSet dsData = null;
        DataTable dtData = null;


        dsData = WsData.RunSQL("begin exec pips.stp_AuctionPRVendorCertConfirmList " + GetRequest.GetQueryString("AuctionId") + "," + GetRequest.GetQueryString("VendorId") + "; end;");
        dtData = dsData.Tables[0];
        dgvList.DataSource = dtData;
        dgvList.DataBind();

        if (dtData.Rows.Count == 0)
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

        long intPageCount;
        if (dgvList.PageCount == 0)
        {
            intPageCount = 1;
        }
        else
        {
            intPageCount = dgvList.PageCount;
        }
        ClsUtil.LoadImagePaging(dgvList.PageIndex + 1, dgvList.PageSize, intPageCount, imbFirst, imbPrev, imbNext, imbLast, lblPaging);
        
    }

    protected void dgvList_RowDataBound(Object sender, GridViewRowEventArgs e)
    {

        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text == "1")
            {
                e.Row.Cells[3].Text = "<font color=green><b>" + e.Row.Cells[3].Text + "</b></font>";
            }
            else if (e.Row.Cells[4].Text == "0")
            {
                e.Row.Cells[3].Text = "<font color=red><b>" + e.Row.Cells[3].Text + "</b></font>";
            }
            else
            {
                e.Row.Cells[3].Text = "<font color=orange><b>" + e.Row.Cells[3].Text + "</b></font>";
            }

        }

        e.Row.Cells[4].Visible = false;

    }

    protected void imbNext_Click(object sender, ImageClickEventArgs e)
    {
        if (dgvList.PageIndex < (dgvList.PageCount - 1))
        {
            dgvList.PageIndex += 1;
        }

        ViewData();
    }
    protected void imbLast_Click(object sender, ImageClickEventArgs e)
    {
        dgvList.PageIndex = (dgvList.PageCount - 1);
        ViewData();
    }
    protected void imbPrev_Click(object sender, ImageClickEventArgs e)
    {
        if (dgvList.PageIndex > 0)
        {
            dgvList.PageIndex -= 1;
        }
        ViewData();
    }
    protected void imbFirst_Click(object sender, ImageClickEventArgs e)
    {
        dgvList.PageIndex = 0;
        ViewData();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        dgvList.PageIndex = 0;
        ViewData();
    }

}
