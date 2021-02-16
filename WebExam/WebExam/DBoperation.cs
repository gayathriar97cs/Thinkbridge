using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;


//using Microsoft.Win32;


public class DBOperation
{
    private SqlConnection SQLConn;
    private SqlCommand SQLCommand;
    private SqlDataReader SQLDataRead;
    private SqlDataAdapter SQLDataAdap;
    
    public DBOperation()
    {
       
    }
    public string GiveField1(string qry)
    {

        string str;
        DataSet ds = new DataSet();
        GetDataSet(qry, "ds", ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            str = ds.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            str = "";
        }
        return str;
    }

    public string GiveFieldQty1(string qry)
    {
        string str = "0.00";
        try
        {
            DataSet ds = new DataSet();
            GetDataSet(qry, "ds", ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                str = ds.Tables[0].Rows[0][0].ToString();
            }
        }
        catch (Exception)
        {
        }
        return str;
    }

    public SqlDataReader ExecuteQuery(string strSQLCmd)
    {
        SQLConn = new SqlConnection(Global.SqlString);
        SqlDataReader sqlRead;
        try
        {
            SQLConn.Open();
            SQLCommand = new SqlCommand(strSQLCmd, SQLConn);
            sqlRead = SQLCommand.ExecuteReader();
            return sqlRead;
        }
        catch (Exception ex)
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
            SQLDataRead.Close();
            throw ex;
        }
        finally
        {
            SQLConn = null;            
        }
    }
    public void ExecuteNonQuery(string strSQLCmd)
    {
        //RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
        //rkey.SetValue("sShortDate", "dd/MM/yyyy");
        //rkey.SetValue("sLongDate", "dd/MM/yyyy");

        SQLConn = new SqlConnection(HttpContext.Current.Session["strSQLConnection"].ToString());
        try
        {
            SQLConn.Open();
            SQLCommand = new SqlCommand(strSQLCmd, SQLConn);
            SQLCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
            throw ex;
        }
        finally
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
        }
    }

    public bool GetDataSet(string strSQLString, string strTableName, ref DataSet dsResults)
    {
        ExecuteNonQuery("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        SQLConn = new SqlConnection(HttpContext.Current.Session["strSQLConnection"].ToString());
        bool functionReturnValue = false;

        try
        {
            SQLConn.Open();
            SQLCommand = new SqlCommand(strSQLString, SQLConn);
            SQLDataAdap = new SqlDataAdapter(SQLCommand);
            SQLDataAdap.Fill(dsResults, strTableName);
            functionReturnValue = true;
        }
        catch (Exception ex)
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
            SQLDataAdap.Dispose();
            throw ex;
        }
        finally
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
            SQLDataAdap.Dispose();
        }
        return functionReturnValue;
    }

    public bool GetDataTable(string strSQLString, ref DataTable dtable)
    {
        ExecuteNonQuery("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        SQLConn = new SqlConnection(HttpContext.Current.Session["strSQLConnection"].ToString());
        bool functionReturnValue = false;

        try
        {
            SQLConn.Open();
            SQLCommand = new SqlCommand(strSQLString, SQLConn);
            SQLDataAdap = new SqlDataAdapter(SQLCommand);
            SQLDataAdap.Fill(dtable);
            functionReturnValue = true;
        }
        catch (Exception ex)
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
            SQLDataAdap.Dispose();
            throw ex;
        }
        finally
        {
            SQLConn.Close();
            SQLConn.Dispose();
            SQLCommand.Dispose();
            SQLDataAdap.Dispose();
        }
        return functionReturnValue;
    }

    public string NxtKeyCode(string KeyCode)
    {
        byte[] ASCIIValues = ASCIIEncoding.ASCII.GetBytes(KeyCode);
        int StringLength = ASCIIValues.Length;
        bool isAllZed = true;
        bool isAllNine = true;
        //Check if all has ZZZ.... then do nothing just return empty string.

        for (int i = 0; i < StringLength - 1; i++)
        {
            if (ASCIIValues[i] != 90)
            {
                isAllZed = false;
                break;
            }
        }
        if (isAllZed && ASCIIValues[StringLength - 1] == 57)
        {
            ASCIIValues[StringLength - 1] = 64;
        }

        // Check if all has 999... then make it A0
        for (int i = 0; i < StringLength; i++)
        {
            if (ASCIIValues[i] != 57)
            {
                isAllNine = false;
                break;
            }
        }
        if (isAllNine)
        {
            ASCIIValues[StringLength - 1] = 47;
            ASCIIValues[0] = 65;
            for (int i = 1; i < StringLength - 1; i++)
            {
                ASCIIValues[i] = 48;
            }
        }


        for (int i = StringLength; i > 0; i--)
        {
            if (i - StringLength == 0)
            {
                ASCIIValues[i - 1] += 1;
            }
            if (ASCIIValues[i - 1] == 58)
            {
                ASCIIValues[i - 1] = 48;
                if (i - 2 == -1)
                {
                    break;
                }
                ASCIIValues[i - 2] += 1;
            }
            else if (ASCIIValues[i - 1] == 91)
            {
                ASCIIValues[i - 1] = 65;
                if (i - 2 == -1)
                {
                    break;
                }
                ASCIIValues[i - 2] += 1;

            }
            else
            {
                break;
            }

        }
        KeyCode = ASCIIEncoding.ASCII.GetString(ASCIIValues);
        return KeyCode;
    }

    public Double StockMinus(Double dbDelqty, string strItemId, SqlConnection db, SqlTransaction transaction, String strFor, String TransNo, String strinvid, String BranchCode, String InvoiceItemId)
    {
        Double dbAMountValue = 0.00;
        if (dbDelqty > 0)
        {
            DataSet dsStockdata = new DataSet();
            String StrSelStokItem = "select id,saleqty,basicprice from tblstocklist where itemcode='" + strItemId + "' and saleqty>0 and BranchCode='" + BranchCode + "' order by id";
            Double dbadstock = dbDelqty;
            GetDataSet(StrSelStokItem, "data", ref dsStockdata);

            for (int j = 0; j < dsStockdata.Tables[0].Rows.Count; j++)
            {
                Double dbstkqty = Convert.ToDouble(dsStockdata.Tables[0].Rows[j]["saleqty"].ToString());
                if (dbadstock > 0)
                {
                    if (dbstkqty < dbadstock)
                    {
                        new SqlCommand("update tblStocklist set saleqty=saleqty - '" + dbstkqty + "' where id='" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "'", db, transaction).ExecuteNonQuery();

                        if (strFor.ToLower() == "Invoice".ToLower())
                        {
                            new SqlCommand("INSERT INTO tblInvoiceStock(invoiceid,stockid,qty,itemid,InvoiceItemId)VALUES('" + strinvid + "','" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "','" + dbstkqty + "','" + strItemId + "','" + InvoiceItemId + "')", db, transaction).ExecuteNonQuery();
                        }

                        dbAMountValue = dbAMountValue + (dbstkqty * Convert.ToDouble(dsStockdata.Tables[0].Rows[j]["basicprice"].ToString()));

                        dbadstock = dbadstock - dbstkqty;
                    }
                    else
                    {
                        new SqlCommand("update tblStocklist set saleqty=saleqty - '" + dbadstock + "' where id='" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "'", db, transaction).ExecuteNonQuery();

                        if (strFor.ToLower() == "Invoice".ToLower())
                        {
                            new SqlCommand("INSERT INTO tblInvoiceStock(invoiceid,stockid,qty,itemid,InvoiceItemId)VALUES('" + strinvid + "','" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "','" + dbadstock + "','" + strItemId + "','" + InvoiceItemId + "')", db, transaction).ExecuteNonQuery();
                        }

                        dbAMountValue = dbAMountValue + (dbadstock * Convert.ToDouble(dsStockdata.Tables[0].Rows[j]["basicprice"].ToString()));
                        dbadstock = 0.00;
                    }
                }
            }

            DataSet dsdata = new DataSet();
            string strSelTrans = "select top 1 id,rate,balqty,balvalue,purchaseprice from tbltransaction where itemid='" + strItemId + "' and BranchCode='" + BranchCode + "' order by id desc ";
            GetDataSet(strSelTrans, "Transection", ref dsdata);
            if (dsdata.Tables[0].Rows.Count > 0)
            {
                Double dbRate = Convert.ToDouble(dsdata.Tables[0].Rows[0]["rate"].ToString());
                Double dbBalqty = Convert.ToDouble(dsdata.Tables[0].Rows[0]["balqty"].ToString());
                Double dbBalValue = Convert.ToDouble(dsdata.Tables[0].Rows[0]["balvalue"].ToString());
                Double dbPurchasePrice = Convert.ToDouble(dsdata.Tables[0].Rows[0]["purchaseprice"].ToString());
                Double dbnewbalqty = dbBalqty - dbDelqty;
                Double dbnewbalVal = Math.Round((Math.Round(dbBalValue, 2) - Math.Round(dbAMountValue, 2)), 2);
                if (dbBalqty <= 0)
                {
                    dbnewbalVal = 0.00;
                }
                else if (dbnewbalVal <= 0)
                {
                    dbnewbalVal = dbPurchasePrice * dbnewbalqty;
                }
                string strInsTransQry = "";
                strInsTransQry = "INSERT INTO tbltransaction(itemid,DocNo,rate,grnqty,grnvalue,saleqty,salevalue,balqty,balvalue,purchaseprice,ReciptDetails,BranchCode) values (";
                strInsTransQry = strInsTransQry + "'" + strItemId + "','" + TransNo + "','" + dbRate + "','0.00','0.00','" + dbDelqty + "','" + dbRate * dbDelqty + "','" + dbnewbalqty + "','" + dbnewbalVal + "','" + dbPurchasePrice + "','" + strFor + "','" + BranchCode + "')";
                new SqlCommand(strInsTransQry, db, transaction).ExecuteNonQuery();

            }
        }
        return dbAMountValue;
    }

    public void StockMinusINVEdit(Double dbDelqty, string strItemId, SqlConnection db, SqlTransaction transaction, String strFor, String TransNo, String strinvid, String BranchCode, String InvoiceItemId)
    {
        Double dbAMountValue = 0.00;
        if (dbDelqty > 0)
        {

            DataSet dsStockdata = new DataSet();
            String StrSelStokItem = "select id,saleqty,basicprice from tblstocklist where itemcode='" + strItemId + "' and saleqty>0  and BranchCode='" + BranchCode + "' order by id";
            Double dbadstock = dbDelqty;
            GetDataSet(StrSelStokItem, "data", ref dsStockdata);

            for (int j = 0; j < dsStockdata.Tables[0].Rows.Count; j++)
            {
                Double dbstkqty = Convert.ToDouble(dsStockdata.Tables[0].Rows[j]["saleqty"].ToString());
                if (dbadstock > 0)
                {
                    if (dbstkqty < dbadstock)
                    {
                        new SqlCommand("update tblStocklist set saleqty=saleqty - '" + dbstkqty + "' where id='" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "'", db, transaction).ExecuteNonQuery();

                        if (strFor.ToLower() == "Invoice".ToLower())
                        {
                            new SqlCommand("INSERT INTO tblInvoiceStock(invoiceid,stockid,qty,itemid,InvoiceItemId)VALUES('" + strinvid + "','" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "','" + dbstkqty + "','" + strItemId + "','" + InvoiceItemId + "')", db, transaction).ExecuteNonQuery();
                        }

                        dbAMountValue = dbAMountValue + (dbstkqty * Convert.ToDouble(dsStockdata.Tables[0].Rows[j]["basicprice"].ToString()));

                        dbadstock = dbadstock - dbstkqty;
                    }
                    else
                    {
                        new SqlCommand("update tblStocklist set saleqty=saleqty - '" + dbadstock + "' where id='" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "'", db, transaction).ExecuteNonQuery();

                        if (strFor.ToLower() == "Invoice".ToLower())
                        {
                            new SqlCommand("INSERT INTO tblInvoiceStock(invoiceid,stockid,qty,itemid,InvoiceItemId)VALUES('" + strinvid + "','" + dsStockdata.Tables[0].Rows[j]["id"].ToString() + "','" + dbadstock + "','" + strItemId + "','" + InvoiceItemId + "')", db, transaction).ExecuteNonQuery();
                        }

                        dbAMountValue = dbAMountValue + (dbadstock * Convert.ToDouble(dsStockdata.Tables[0].Rows[j]["basicprice"].ToString()));
                        dbadstock = 0.00;
                    }
                }
            }

            Double balqty = 0.00; Double Strbalqty = 0.00;
            int transid = 0;

            DataSet dstoptransection = new DataSet();

            GetDataSet("Select top 1 id,(balqty + saleqty)balqty from tbltransaction where itemid= '" + strItemId + "' and DocNo='" + TransNo + "' and BranchCode='" + BranchCode + "' order by id desc ", "data", ref dstoptransection);
            if (dstoptransection.Tables[0].Rows.Count > 0)
            {
                transid = Convert.ToInt32(dstoptransection.Tables[0].Rows[0]["id"].ToString());
                balqty = Convert.ToDouble(dstoptransection.Tables[0].Rows[0]["balqty"].ToString());
            }

            balqty = balqty - dbDelqty;

            new SqlCommand("Update tbltransaction Set saleqty='" + dbDelqty + "',balqty='" + balqty + "', balvalue=purchaseprice*'" + balqty + "' where id='" + transid + "'", db, transaction).ExecuteNonQuery();

            string strSelTrans = "select top 1 id,rate,balqty,balvalue,purchaseprice,saleqty from tbltransaction where itemid='" + strItemId + "' and DocNo='" + TransNo + "' order by id desc ";
            DataSet dstransection = new DataSet();
            GetDataSet("Select * from tbltransaction where itemid= '" + strItemId + "' and id > '" + transid + "' and BranchCode='" + BranchCode + "' order by id desc ", "data", ref dstransection);
            for (int j = 0; j < dstransection.Tables[0].Rows.Count; j++)
            {
                if (Convert.ToDouble(dstransection.Tables[0].Rows[j]["grnqty"].ToString()) == 0.00)
                {
                    Double StrSaleqty = Convert.ToDouble(dstransection.Tables[0].Rows[j]["saleqty"].ToString());
                    Double StrPurchsePrice = Convert.ToDouble(dstransection.Tables[0].Rows[j]["purchaseprice"].ToString());

                    Strbalqty = balqty - StrSaleqty;
                    Double Strbalvalue = Strbalqty * StrPurchsePrice;
                    new SqlCommand("Update tbltransaction Set balqty='" + Strbalqty + "', balvalue='" + Strbalvalue + "' where id='" + dstransection.Tables[0].Rows[j]["id"].ToString() + "'", db, transaction).ExecuteNonQuery();
                }
                else
                {
                    Double StrGrnqty = Convert.ToDouble(dstransection.Tables[0].Rows[j]["grnqty"].ToString());
                    Double StrPurchsePrice = Convert.ToDouble(dstransection.Tables[0].Rows[j]["purchaseprice"].ToString());

                    Strbalqty = balqty + StrGrnqty;
                    Double Strbalvalue = Strbalqty * StrPurchsePrice;
                    new SqlCommand("Update tbltransaction Set balqty='" + Strbalqty + "', balvalue='" + Strbalvalue + "' where id='" + dstransection.Tables[0].Rows[j]["id"].ToString() + "'", db, transaction).ExecuteNonQuery();
                }
            }
        }
    }


    public Double onlyDecimal(String strval)
    {
        Double val = 0.00;
        if (strval != null)
        {
            if (strval != "")
            {
                val = Convert.ToDouble(strval);
            }
        }
        return val;
    }
}