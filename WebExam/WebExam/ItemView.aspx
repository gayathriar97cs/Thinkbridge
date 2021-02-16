<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ItemView.aspx.cs" Inherits="WebExam.Inventroy.ItemView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="../../CSS/bootstrap.min.css" />
    <script src="../../BootStrap/jquery.min.js"></script>
    <script src="../../BootStrap/bootstrap.min.js"></script>


    <script src="../../Scripts/Validation.js"></script>
    <script src="../../Scripts/jquery-latest.min.js"></script>
    <script src="../../Scripts/script.js"></script>
    <link href="../../CSS/StyleSidebar.css" rel="stylesheet" />
    <link href="../../CSS/AdminLTE.css" rel="stylesheet" />

</head>
<body style="background-color: #EAF1F6;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="width: 100%;">
                    <div class="col-sm-12" style="max-width: 1200px; left: 10%;">

                        <div class="panel panel-default">
                            <div class="panel-heading" style="font-weight: bold; color: black; background-color: #EAF1F6;">
                                Item Inventory View &nbsp;
                                 &nbsp;<label id="lblmode" runat="server" style="color: black;"></label>
                            </div>

                            <div id="collapse1" runat="server">
                                <div class="row with-margin w-top">
                                    <div class=" col-sm-1 control-label ">

                                        <label>Item Name</label>
                                        <label style="color: red;">*</label>
                                    </div>

                                    <div class="col-sm-1 ">
                                        <asp:TextBox onkeydown="return (event.keyCode!=13);" ID="txtItemName" MaxLength="150" runat="server" autocomplete="off"
                                            CssClass="form-control input-sm" placeholder="Item Name" Enabled="false"></asp:TextBox>

                                    </div>
                                    <div class=" col-sm-1 control-label ">

                                        <label>Item Description</label>
                                        <label style="color: red;">*</label>
                                    </div>
                                    <div class="col-sm-7">
                                        <asp:TextBox onkeydown="return (event.keyCode!=13);" ID="txtItemDescription" MaxLength="150" runat="server" autocomplete="off"
                                            CssClass="form-control input-sm" placeholder="Item Description" Enabled="false"></asp:TextBox>

                                    </div>


                                </div>
                                <div class="row with-margin w-top">

                                    <div class="col-sm-1 control-label">

                                        <label>Price</label>
                                        <label style="color: red;">*</label>
                                        
                                    </div>
                                    <div class="col-sm-1">

                                            <asp:TextBox onkeydown="return (event.keyCode!=13);" ID="txtPrice" runat="server" MaxLength="10" autocomplete="off" onkeypress="return validateFloatKeyPress(this,event);" AutoPostBack="true"
                                                CssClass="form-control input-sm" placeholder="Price" Enabled="false"></asp:TextBox>

                                        </div>

                                    <div class=" col-sm-3 control-label ">
                                        <asp:Image ID="Image1" ImageUrl="../../assets/images/avatars/male.png" runat="server" Width="96px" Height="92px" />
                                    </div>


                                    <div class="col-sm-1">
                                      
                                        <asp:TextBox ID="txtImageFile" runat="server" CssClass="hidden"></asp:TextBox>
                                    </div>
                                </div>


                            </div>


                        
                            <div class="text-center">
                            </div>

                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
