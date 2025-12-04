//Using Statements @1-86CFB0D3
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Globalization;
using pips;
using pips.Data;
using pips.Configuration;
using pips.Security;
using pips.Controls;

//End Using Statements
//-- start custom 
using Company.Apps.WsAuctionMaster;
using Company.Apps.WsDataPIPS;
using Company.Apps.WsAuctionSave;
using Company.Apps.Utility;
using ExcelLibrary.BinaryFileFormat;

//--end custom
namespace pips.sourcing.AuctionFormPRDetailPostel
{ //Namespace @1-AEBB3D24


    public partial class AuctionFormPRDetailPostelPage : CCPage
    {
        WsAuctionSave MyAuctionSave = new WsAuctionSave();
        ClsUtility ClsUtil = new ClsUtility();
        WsDataPIPS myWsDataPIPS = new WsDataPIPS();

        private void Page_Load(object sender, System.EventArgs e)
        {
            int prID = Convert.ToInt32(GetRequest.GetQueryString("ParamPRDetailId"));
            int AuctPRID = 0;
            if (!string.IsNullOrEmpty(GetRequest.GetQueryString("ParamAuctionPRId")))
            {
                AuctPRID = Convert.ToInt32(GetRequest.GetQueryString("ParamAuctionPRId"));
            }
            if (!IsPostBack)
            {
                showData(prID, AuctPRID);
            }

        }

        private void showData(int prId, int AuctPRID)
        {
            DataSet dsDtl;
            DataTable dtDtl;

            if (AuctPRID != 0)
            {
                string strPlantChange = myWsDataPIPS.ExecuteScalar("select " +
                                                            "isnull(max(b.PlantCode + ' - ' + b.PlantName + ' # (Reason : ' + a.PlantReason + ')'), '') as Plant " +
                                                            "from " +
                                                            "pips.tblT_AuctionPR a " +
                                                            "left outer join pips.tblM_Plant b on a.PlantId_FK = b.PlantId_PK " +
                                                            "where " +
                                                            "a.AuctionPRId_PK = " + AuctPRID.ToString());
                
            }

            dsDtl = myWsDataPIPS.RunSQL("select " +
                                        "a.PlantCode, " +
                                        "a.PlantName, " +
                                        "pips.trim(a.DeliveryAddress + ' ' + isnull(a.DeliveryAddrName1, '') + ' ' + isnull(a.DeliveryAddrName2, '')) + ' ' + isnull(a.DeliveryRegionName, '') as DeliveryAddress, " +
                                        "a.RequisitionerName," +
                                        "a.PurchaseGroupCode, " +
                                        "a.PurchaseGroupName, " +
                                        "a.ProcureMethodTitle, " +
                                        "a.BrandCategTitle, " +
                                        "a.AdditionalInfo, " +
                                        "a.MaterialServiceText, " +
                                        "isnull(b.PriceReference,'') as PriceReference, isnull(a.IsPostelCertificate,0) as IsPostelCertificate, isnull(a.PostelNotes,'') as PostelNotes " +
                                        "from pips.vwT_PurchaseRequestDetail a " +
                                        "inner join pips.tblT_PurchaseRequestDetail b on a.PurchaseRequestDetailId_PK=b.PurchaseRequestDetailId_PK " +
                                        "where a.PurchaseRequestDetailId_PK = " + prId);

            dtDtl = dsDtl.Tables[0];

            lblItemName.Text = dtDtl.Rows[0]["MaterialServiceText"].ToString();
            pips_vw_PlantCode.Text = dtDtl.Rows[0]["PlantCode"].ToString();
            pips_vw_PlantName.Text = dtDtl.Rows[0]["PlantName"].ToString();
            pips_vw_DeliveryAddress.Text = dtDtl.Rows[0]["DeliveryAddress"].ToString();
            pips_vw_Requisitioner.Text = dtDtl.Rows[0]["RequisitionerName"].ToString();
            pips_vw_PurchaseGroupCode.Text = dtDtl.Rows[0]["PurchaseGroupCode"].ToString();
            pips_vw_PurchaseGroupName.Text = dtDtl.Rows[0]["PurchaseGroupName"].ToString();
            pips_vw_Method.Text = dtDtl.Rows[0]["ProcureMethodTitle"].ToString();
            pips_vw_Brand.Text = dtDtl.Rows[0]["BrandCategTitle"].ToString();
            pips_vw_ItemInfo.Text = dtDtl.Rows[0]["AdditionalInfo"].ToString();
            lblPriceReference.Text = dtDtl.Rows[0]["PriceReference"].ToString();

            //chkPostel.Checked = false;
            rdoPostel.SelectedValue = "0";
            if (dtDtl.Rows[0]["IsPostelCertificate"].ToString() == "1")
            {
                //chkPostel.Checked = true;
                rdoPostel.SelectedValue = dtDtl.Rows[0]["IsPostelCertificate"].ToString();
                DIV_POSTEL_NOTES.Attributes.Remove("style");
            }
            txtPostelNotes.Text = dtDtl.Rows[0]["PostelNotes"].ToString();

            txtPRDetailId.Text = prId.ToString();
        }



        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            string strIsPostel = "0";
            /*
            if (chkPostel.Checked)
            {
                strIsPostel = "1";
            }
            */
            strIsPostel = rdoPostel.SelectedValue;
            string strSave = myWsDataPIPS.ExecuteScalar("begin " +
                                                    "declare  " +
                                                    "@strOutMsg varchar(max); " +
                                                    "exec pips.stp_PRDetailPostelFlag " + txtPRDetailId.Text + "," + strIsPostel + ",'" + txtPostelNotes.Text.Trim() + "','" + ClsAuthSession.USERNAME + "',@strOutMsg out; " +
                                                    "select @strOutMsg; " +
                                                    "end;");
            ClientScriptManager cs = Page.ClientScript;
			Type cstype = this.GetType();
            if (strSave == "1")
            {   
			    cs.RegisterStartupScript(cstype,null,"<script language='javascript'>alert('Proses Simpan berhasil');document.location.href='AuctionFormPRDetailPostel.aspx?" + Request.QueryString.ToString() + "'</script>");
			    
            }
            else
            {
                cs.RegisterStartupScript(cstype,null,"<script language='javascript'>alert('GAGAL Simpan');document.location.href='AuctionFormPRDetailPostel.aspx?" + Request.QueryString.ToString() + "'</script>");
            }
            return;
        }
    }
}
//End Page class tail

