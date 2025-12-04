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

public partial class SAGR_PODelivPlanDetailCert : System.Web.UI.Page
{

    ClsUtility ClsUtil = new ClsUtility();
    WsDataPIPS WsData = new WsDataPIPS();

    protected void Page_Load(object sender, EventArgs e)
    {
        ClsUtil.CheckSession();
        if (!IsPostBack)
        {

            txtPODelivPlanDetailId.Text = GetRequest.GetQueryString("POPlanDetailId");

            ViewDetail(Convert.ToInt64(txtPODelivPlanDetailId.Text));

        }
    }
    

    protected void ViewDetail(long intPODelivPlanId)
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;

        DataSet dsData = WsData.RunSQL("select * from pips.fn_GetDOPlanPostelCert(" + intPODelivPlanId.ToString() + ")");
        DataTable dtData = dsData.Tables[0];

        txtCertId.Text = dtData.Rows[0]["PODelivPlanDetailCertId"].ToString();
        lblCertificateNumber.Text = dtData.Rows[0]["CertificateNumber"].ToString();
        lblCertificateURL.Text = dtData.Rows[0]["CertificateURL"].ToString();
        lblStartPeriod.Text = dtData.Rows[0]["PeriodStart2"].ToString();
        lblEndPeriod.Text = dtData.Rows[0]["PeriodEnd2"].ToString();
        lblAdditionalNotes.Text = dtData.Rows[0]["AdditionalNotes"].ToString();

        if (!string.IsNullOrEmpty(dtData.Rows[0]["InternalUploadFilePath"].ToString())) { 
            string strURL = GetRequest.EncryptQueryString("NamaFile=" + dtData.Rows[0]["InternalUploadFilePath"].ToString());
            lblDoc.Text = "<a href=\"javascript:toView('" + strURL + "')\"><u>Download File</u></a>";
        }

        lblMessage.Text = "";
        lblMessage.Visible = false;

    }
    

    
}