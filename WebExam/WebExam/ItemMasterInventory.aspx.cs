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
using System.IO;
using System.Drawing;

namespace WebExam
{
    public partial class ItemMasterInventory : System.Web.UI.Page
    {

        DBOperation dbop = new DBOperation();
       SqlConnection db;
       SqlTransaction transaction;
        protected void Page_Load(object sender, EventArgs e)
        {

            Session["strSQLConnection"] = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            ScriptManager.GetCurrent(this).RegisterPostBackControl(this.Button1);
            
            if (!IsPostBack)
            {
              collapse1.Visible = false;

           

              hdfEdit.Value = "hidden";
              hdfView.Value = "hidden";
              hdfDelete.Value = "hidden";
            

              }

             FillData();
             
            
        }
       

      
        private DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var rowView = (DataRowView)e.Row.DataItem;
                (e.Row.FindControl("lnkDelete") as LinkButton).CommandArgument = rowView.Row.Field<Int32>("id").ToString();
                (e.Row.FindControl("lnkEdit") as LinkButton).CommandArgument = rowView.Row.Field<Int32>("id").ToString();
                //(e.Row.FindControl("lnkView") as LinkButton).CommandArgument = rowView.Row.Field<Int32>("id").ToString();

                
                    if (hdfEdit.Value == "")
                    {
                        (e.Row.FindControl("lnkEdit") as LinkButton).CssClass = "hidden";
                    }

                    if (hdfDelete.Value == "")
                    {
                        (e.Row.FindControl("lnkDelete") as LinkButton).CssClass = "hidden";
                    }

                    //if (hdfView.Value == "")
                    //{
                    //    (e.Row.FindControl("lnkView") as LinkButton).CssClass = "hidden";
                    //}

               

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            FillData();
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                int m = e.Row.Cells.Count;
                for (int i = m - 1; i >= 1; i += -1)
                {
                    e.Row.Cells.RemoveAt(i);
                }
                e.Row.Cells[0].ColumnSpan = m;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MyDelete")
            {
                string id = Convert.ToString(e.CommandArgument);
                dbop.ExecuteNonQuery("uds_DelItemMasterInventory '" + id +  "'"); FillData();
               
            }
            else if (e.CommandName == "Edit")
            {
                btnAddNew.Visible = false;
                collapse1.Visible = true;
                lblmode.InnerText = "Edit";
                btnOK.Visible = true;
                EnableControl();
                ClearControl();

                LinkButton btn = (LinkButton)e.CommandSource;
                GridViewRow grdrow = ((GridViewRow)btn.NamingContainer);
                int rowIndex = grdrow.RowIndex;
                string id = Convert.ToString(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];
                String strItemFill = "uds_SelItemMasterInventoryFill'" + id + "'";
                DataSet dsItemList = new DataSet();
                dbop.GetDataSet(strItemFill, "Data", ref dsItemList);
         
                txtItemName.Text = Server.HtmlDecode(row.Cells[2].Text).Trim();
                if (dsItemList.Tables[0].Rows.Count > 0)
                {

                    txtImageFile.Text = dsItemList.Tables[0].Rows[0]["ImageFile"].ToString();
                    String folderPath1 = "../../Employee Document/" + txtItemName.Text + "/" + txtImageFile.Text;
                    Image1.ImageUrl = folderPath1;
                    txtItemDescription.Text = dsItemList.Tables[0].Rows[0]["ItemDescription"].ToString();
                    txtPrice.Text = dsItemList.Tables[0].Rows[0]["price"].ToString();
                  


                }
                btnOK.ToolTip = "Update";
                btnOK.Text = "Update";
                btnCancel.Visible = true;
                txtROWNO.Text = id.ToString();
            }

           

        }
        protected void UploadFile10(object sender, EventArgs e)
        {
            try
            {
                if (txtItemName.Text != "" )
                {
                    string folderPath = Server.MapPath("~/Employee Document/" + txtItemName.Text + "/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string ext = System.IO.Path.GetExtension(FileUpload10.PostedFile.FileName);
                    txtImageFile.Text = txtItemName.Text + "_Image" + ext;

                    FileUpload10.SaveAs(folderPath + Path.GetFileName(txtImageFile.Text));
                    String folderPath1 = "../../Employee Document/" + txtItemName.Text + "/" + txtImageFile.Text;
                    Image1.ImageUrl = folderPath1;
                }
                else
                {
                    String message = "Please Generate Employee Code and Enter Employee Name";
                    lblPoupmsg1.InnerHtml = message;
                    mpemsg1.Show();
                }
            }
            catch { }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillData();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
      

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text.ToUpper() == "SAVE".ToUpper())
            {
                btnOK.Enabled = false;
                string strMessage = "";
                string Rowid = txtROWNO.Text;
                try
                {
                    db = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);
                    db.Open();
                    transaction = db.BeginTransaction();

                    if (dbop.GiveField1("Select id from tblItemMasterInventory where deleted=0 and ItemName='" + txtItemName.Text + "'") != "")
                    {
                        strMessage = "Item Details allready Exits in database.";
                      
                        lblPoupmsg1.InnerHtml = strMessage;
                        mpemsg1.Show();
                        transaction.Rollback();
                        db.Close();
                        btnOK.Enabled = true;
                        return;
                    }

                    AddItemMasterInventory();
                    strMessage = "Item Name " + txtItemName.Text + " saved Successfully.";
                    transaction.Commit();
                    db.Close();
                    lblPoupmsg.InnerHtml = strMessage;
                    mpemsg.Show();
                }
                catch
                {
                    //transaction.Rollback();
                    db.Close();
                    strMessage = "Details Not Saved.Please Try aganin.";
                    lblPoupmsg1.InnerHtml = strMessage; mpemsg1.Show();
                    btnOK.Enabled = true;
                }
            }

            else if (btnOK.Text.ToUpper() == "UPDATE".ToUpper())
            {
                btnOK.Enabled = false;
                string strMessage = "";

                try
                {
                    db = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);
                    db.Open();
                    transaction = db.BeginTransaction();

                    if (dbop.GiveField1("Select id from tblItemMasterInventory where id !='" + txtROWNO.Text + "' and deleted=0 and ItemName='" + txtItemName.Text + "'") != "")
                    {
                        strMessage = "Item Details allready Exits in database.";
                        lblPoupmsg1.InnerHtml = strMessage;
                        mpemsg1.Show();
                        transaction.Rollback();
                        db.Close();
                        btnOK.Enabled = true;
                        return;
                    }

                    UpdateItemMasterInventory();

                    strMessage = "Item Code " + txtItemName.Text + " Updated Successfully.";
                    transaction.Commit();
                    db.Close();
                    lblPoupmsg.InnerHtml = strMessage;
                    mpemsg.Show();
                    btnOK.Enabled = true;



                    btnOK.ToolTip = "Save";
                    btnOK.Text = "Save";
                    ClearControl();
                    EnableControl();
                    txtROWNO.Text = "";
                    collapse1.Visible = false;
                    btnCancel.Visible = false;
                    btnAddNew.Visible = true;
                    btnOK.Visible = false;
                    lblmode.InnerText = "";
                    FillData();
                }
                catch
                {
                    transaction.Rollback();
                    db.Close();
                    strMessage = "Details Not Saved.Please Try aganin.";
                    lblPoupmsg1.InnerHtml = strMessage; mpemsg1.Show();
                    btnOK.Enabled = true;
                }
            }

        }

        protected void popupclose(object sender, EventArgs e)
        {
            string sPath = Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            Response.Redirect(sRet, false);
        }
        private void FillData()
        {
            DataSet dsFetchData = new DataSet();
            dbop.GetDataSet("uds_SelItemMasterInventoryList ", "ItemMasterInventory", ref dsFetchData);
            DataTable dttable = dsFetchData.Tables["ItemMasterInventory"];
            GridView1.DataSource = dttable;
            GridView1.DataBind();
            ViewState["DisplayData"] = dttable;
        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dttable = new DataTable();
            dttable = (DataTable)ViewState["DisplayData"];
            if (dttable.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    dttable.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sortdr"] = "Desc";
                }
                else
                {
                    dttable.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sortdr"] = "Asc";
                }
                GridView1.DataSource = dttable;
                GridView1.DataBind();
            }
        }
        private void UpdateItemMasterInventory()
        {
            String strInsQry = " uds_UpdItemMasterInventory '" + txtROWNO.Text + "', '" + txtItemName.Text + "','" + txtItemDescription.Text + "',"
                               + " '" + txtPrice.Text + "','" + txtImageFile.Text +  "' ";

            new SqlCommand(strInsQry, db, transaction).ExecuteNonQuery();
        }
        private void AddItemMasterInventory()
        {
            String strInsQry = " uds_InsItemMasterInventory '" + txtItemName.Text + "',"
                               + " '" + txtItemDescription.Text + "','" + txtPrice.Text + "','" + txtImageFile.Text + "' ";
                            

            new SqlCommand(strInsQry, db, transaction).ExecuteNonQuery();
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.NewEditIndex = -1;
        }
        


      
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
         
            btnOK.ToolTip = "Save";
            btnOK.Text = "Save";
            btnCancel.Visible = true;
            btnAddNew.Visible = false;
            btnOK.Visible = true;
            collapse1.Visible = true;
            EnableControl();
            lblmode.InnerText = "Create New";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnOK.ToolTip = "Save";
            btnOK.Text = "Save";
            ClearControl();
            EnableControl();
            txtROWNO.Text = "";
            DisableControl();
            btnCancel.Visible = false;
            collapse1.Visible = false;
            btnAddNew.Visible = true;
            lblmode.InnerText = "";
        }
       
        private void ClearControl()
        {
            txtItemName.Text = "";
            txtItemDescription.Text = "";
            txtPrice.Text = "";
        }
        private void EnableControl()
        {
          
            txtItemName.Enabled = true;
            txtItemDescription.Enabled = true;
            txtPrice.Enabled = true;
           
        }
        private void DisableControl()
        {
            
            txtItemName.Enabled = false;
            txtItemDescription.Enabled = false;
            txtPrice.Enabled = false;
        }

       
       
    }
}