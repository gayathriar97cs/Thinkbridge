using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Text;

namespace WebExam.Inventroy
{
    public partial class ItemView : System.Web.UI.Page
    {
        DBOperation dbop = new DBOperation();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

          

            ClearControl();

            String Rowid = Request.QueryString["Rowid"].ToString();
            FillDetails(Rowid);

        }
        private void FillDetails(String id)
        {
            DataSet dsData = new DataSet();
          
            String strSelData = "uds_SelItemMasterInventoryFill'" + id + "'";

            dbop.GetDataSet(strSelData, "data", ref dsData);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                txtItemName.Text = dsData.Tables[0].Rows[0]["ItemName"].ToString();
                txtImageFile.Text = dsData.Tables[0].Rows[0]["ImageFile"].ToString();
                String folderPath1 = "../../Employee Document/" + txtItemName.Text + "/" + txtImageFile.Text;
                Image1.ImageUrl = folderPath1;
                txtItemDescription.Text = dsData.Tables[0].Rows[0]["ItemDescription"].ToString();
                txtPrice.Text = dsData.Tables[0].Rows[0]["price"].ToString();

                txtPrice.Text = dsData.Tables[0].Rows[0]["price"].ToString();

            }
        }
        private void ClearControl()
        {
            try
            {
                
                txtItemName.Text = "";
                txtItemDescription.Text = "";
                txtPrice.Text = "";
               
            }
            catch { }
        }
    }
}