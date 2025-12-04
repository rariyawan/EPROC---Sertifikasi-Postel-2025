using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Company.Apps.Utility;
using Company.Apps.WsDataPIPS;
using Company.Apps.WsWorkflowEngine;
using System.Configuration;
using System.Web.Script.Serialization;
using System.IO;
using Company.Apps.WsEmail;
using System.Net;
using ExcelLibrary.BinaryFileFormat;

public partial class sourcing_AuctionEvalTeknisPostelVerify : System.Web.UI.Page
{

    ClsUtility ClsUtil = new ClsUtility();
    WsDataPIPS WsData = new WsDataPIPS();
    WsEmail wsemail = new WsEmail();

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
            
            ViewData(Convert.ToInt64(GetRequest.GetQueryString("AuctionId")));
        }
    }
    

    protected void ViewData(long intAuctionId)
    {
        
         string isiTable="";
       string mySQL = "";

       mySQL=mySQL+
       "select PurchaseRequestDetailId_FK, PRNumber, MaterialServiceText, "+
       "format(Quantity,'#,0.00')+' '+QtyUnit Qty, "+ 
       "format(ValuationPrice,'#,0.00') Price, "+
       "format(Quantity*ValuationPrice,'#,0.00') TotalPrice, PurchaseRequestId_FK, AuctionPRId_PK, " +
       "IsPostelCertificate, PostelNotes " +
       "from pips.vwT_AuctionPR "+
       "where AuctionId_FK="+intAuctionId+" "+
       "order by PurchaseRequestId_FK, PurchaseRequestDetailId_FK";       

       DataSet ds = WsData.RunSQL(mySQL);
       DataTable dt = null;
       dt = ds.Tables[0];

        string strDatekEPM = string.Empty;
        string strLinkDatekEPM = string.Empty;
        string strIsPostel = string.Empty;

       for (int i = 0; i < dt.Rows.Count; i++)
       {

            strDatekEPM = WsData.ExecuteScalar("select isnull(max(x.DatekFileURL), '-') " +
                                            " from pips.tblT_PurchaseRequestDetailEPM x " +
                                            " where x.PurchaseRequestDetailId_FK = " + dt.Rows[i].ItemArray[0].ToString());

            strLinkDatekEPM = "";
            if (strDatekEPM != "-")
            {
                strDatekEPM = Server.UrlEncode(strDatekEPM);
                strLinkDatekEPM = "&nbsp;&nbsp;|&nbsp;&nbsp;<a style='color:red;' href=\"javascript:clickDatekEPM('" + strDatekEPM + "')\"><u>Info Datek EPM</u></a>";
            }

            strIsPostel = "";
            if (dt.Rows[i]["IsPostelCertificate"].ToString() == "1")
            {
                strIsPostel = "<br><i><font color=blue>* need postel certificate</font></i>";
            }

             isiTable=isiTable+"<tr class=\"Row\">";	
             isiTable=isiTable+ "<td>&nbsp;" +dt.Rows[i].ItemArray[0].ToString()+"</td>";
 	         isiTable=isiTable+ "<td>&nbsp;<a href ='javascript:showDetail("+ Server.UrlEncode(dt.Rows[i].ItemArray[6].ToString()) + ");'><u>" +dt.Rows[i].ItemArray[1].ToString()+ "</u></a></td>";
             isiTable = isiTable + "<td style=\"white-space:normal\"><u><a href='javascript:ShowItemDetail(" + dt.Rows[i].ItemArray[0].ToString() + "," + dt.Rows[i].ItemArray[7].ToString() + ") '>" + dt.Rows[i]["MaterialServiceText"].ToString() + "</u>" + strLinkDatekEPM + strIsPostel + "</td>";
             isiTable =isiTable+ "<td style=\"text-align:right\">" +dt.Rows[i].ItemArray[3].ToString()+"&nbsp;</td>";
             isiTable=isiTable+ "<td style=\"text-align:right\">" +dt.Rows[i].ItemArray[4].ToString()+"&nbsp;</td>";
             isiTable=isiTable+ "<td style=\"text-align:right\">" +dt.Rows[i].ItemArray[5].ToString()+"&nbsp;</td>";
             isiTable=isiTable+"</tr> \n";	
       }

       dataPR.Text=isiTable; 
        
    }



    protected void lnbData_Click(object sender, EventArgs e)
    {
        string strParam = "AuctionId_PK=" + GetRequest.GetQueryString("AuctionId_PK") + "&" +
                            "TransactionId=" + GetRequest.GetQueryString("TransactionId") + "&" +
                            "AuctionId=" + GetRequest.GetQueryString("AuctionId");
        Response.Redirect("AuctionMasterUser.aspx?" + GetRequest.EncryptQueryString(strParam));
    }

    protected void lnbTech_Click(object sender, EventArgs e)
    {
        string strParam = "AuctionId_PK=" + GetRequest.GetQueryString("AuctionId_PK") + "&" +
                            "TransactionId=" + GetRequest.GetQueryString("TransactionId") + "&" +
                            "AuctionId=" + GetRequest.GetQueryString("AuctionId");
        Response.Redirect("AuctionEvalTeknisScoreInit.aspx?" + GetRequest.EncryptQueryString(strParam));
}
}
