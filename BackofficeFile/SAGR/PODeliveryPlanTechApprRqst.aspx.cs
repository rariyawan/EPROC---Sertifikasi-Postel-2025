using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Company.Apps.Utility;
using Company.Apps.WsDataPIPS;
using Company.Apps.WsEmail;
using System.IO;
using System.Configuration;
using System.Net;
using System.Web.Script.Serialization;

public partial class SAGR_PODeliveryPlanTechApprRqst : System.Web.UI.Page
{

    ClsUtility ClsUtil = new ClsUtility();
    WsDataPIPS WsData = new WsDataPIPS();
    WsEmail WsSendMail = new WsEmail();

    protected void Page_Load(object sender, EventArgs e)
    {
        ClsUtil.CheckSession();
        if (!IsPostBack)
        {

            txtPODeliveryPlanId.Text = GetRequest.GetQueryString("PODeliveryPlanId");
            ViewDetail(Convert.ToInt64(txtPODeliveryPlanId.Text));

        }
    }
    

    protected void ViewDetail(long intPODeliveryPlanId)
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;

        DataSet dsData = WsData.RunSQL("select a.DONumber " +
                                    "from pips.tblT_PODeliveryPlan a " +
                                    "where " +
                                    "a.PODeliveryPlanId_PK=" + intPODeliveryPlanId.ToString());
        DataTable dtData = dsData.Tables[0];

        
        lblDONumber.Text = dtData.Rows[0]["DONumber"].ToString();

    }
    

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        string strError = "";

        
        if (strError != "")
        {
            lblMessage.Text = strError;
            lblMessage.Visible = true;
            return;
        }

        int intOut;
        string strOutMsg = string.Empty;
        if (!SaveData(out intOut, out strOutMsg))
        {
            if (intOut != 1)
            {   
                lblMessage.Text = strOutMsg;
                lblMessage.Visible = true;
                return;
            }
        }
        else
        {   
            Response.Write(
                "<script language='javascript'>"
                + " alert('Data Successfully submitted');"
                + " parent.modalWindow.closeRefreshParent();"
                + "</script>");

            return;
        }
    }

    protected bool SaveData(out int intOut, out string strOutMsg)
    {
        bool boolExec = false;
        intOut = 0;
        strOutMsg = "";

        long intDOPlanId = Convert.ToInt64(txtPODeliveryPlanId.Text);

        try
        {
            
            
            string strMessage = WsData.ExecuteScalar("begin " +
                                "declare " +
                                "@strOutMessage varchar(max); " +
                                "exec pips.stp_PODeliveryPlanTechApprRqst " + intDOPlanId.ToString() + ",'" + txtNotes.Text + "','" + ClsAuthSession.USERNAME + "',@strOutMessage out; " +
                                "select @strOutMessage;" +
                                "end; ");

            if (strMessage == "1")
            {



                intOut = 1;
                boolExec = true;
            }
            else
            {
                intOut = 0;
                strOutMsg = strMessage;
                boolExec = false;
            }


        }
        catch (Exception ex)
        {
            intOut = 0;
            strOutMsg = ex.Message;
            boolExec = false;
        }

        return boolExec;
    }

}
