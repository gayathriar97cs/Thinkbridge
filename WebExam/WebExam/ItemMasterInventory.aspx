<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ItemMasterInventory.aspx.cs" Inherits="WebExam.ItemMasterInventory" %>

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



    <script type="text/javascript">

        function InputValidate(myButton) {

            var txtItemName = document.getElementById("txtItemName").value;
            document.getElementById("txtItemName").style.borderColor = "";
            if (txtItemName == "") {
                alert("First  Enter Item Name.");
                document.getElementById("txtItemName").focus();
                document.getElementById("txtItemName").style.borderColor = "Red";
                return false;
            }
            var txtItemDescription = document.getElementById("txtItemDescription").value;
            document.getElementById("txtItemDescription").style.borderColor = "";
            if (txtItemDescription == "") {
                alert("First  Enter Item Description.");
                document.getElementById("txtItemDescription").focus();
                document.getElementById("txtItemDescription").style.borderColor = "Red";
                return false;
            }

            var txtPrice = document.getElementById("txtPrice").value;
            document.getElementById("txtPrice").style.borderColor = "";
            if (txtPrice == "") {
                alert("First  Enter Price.");
                document.getElementById("txtPrice").focus();
                document.getElementById("txtPrice").style.borderColor = "Red";
                return false;
            }

          
            if (btnOK == "Update") {
                if (confirm('Are you sure you want to Update the Record ?')) {
                    myButton.disabled = true;
                    myButton.value = "processing...";
                    document.getElementById('Button2').click();
                    return true;
                } else {
                    return false;
                }
            }
            else {
                if (confirm('Are you sure you want to save?')) {
                    myButton.disabled = true;
                    myButton.value = "processing...";
                    document.getElementById('Button2').click();
                    return true;
                } else {
                    return false;
                }
            }

            return true;
        }
        function InputValidateCancel(myButton) {

            if (confirm('Are you sure you want to Cancel?')) {
                return true;
            } else {
                return false;
            }

            return true;

        }

    </script>

</head>
<body style="background-color: #EAF1F6">
    <form id="form1" runat="server" >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <nav class="navbar navbar-inverse" style="margin-bottom: 0px; background-color: #ccdbf3">
            <div class="container-fluid">
                


                <div class="collapse navbar-collapse row" runat="server" id="myNavbar">
                    <ul class="nav navbar-nav  ">
                         <h2>INVENTORY</h2>
                         
                    </ul>
                    <div runat="server" id="NavElements">
                    </div>
                </div>
            </div>

        </nav>
        <div class="row">
            <div runat="server" class="cssmenu col-sm-4" style="padding-left: 12px; margin-left: 21px;" id="SubMenu">
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <div class="OuterContainer" style="border: none;">

                        <div class="col-sm-10" style="border: none;">

                            <div class="col-sm-12" style="border: none;">

                               

                                <div class="text-left" style="margin-top: 10px; background-color: #EAF1F6;">
                                    <asp:Button ID="btnAddNew" Style="font-family: 'Calibri'" Font-Size="18px" BorderColor="White" Font-Bold="true" runat="server" CssClass="btn btn-danger" UseSubmitBehavior="false" BackColor="#CBDBEA" ForeColor="Black" Text="New Item Inventory" OnClick="btnAddNew_Click" />
                                </div>
                                <div id="collapse1" runat="server" class="panel">
                                    <div class="panel-heading" style="font-weight: bold; background-color: #EAF1F6; border: none; color: #CBDBEA;">
                                        <label id="lblmode" runat="server" style="color: black; font-size: 14px"></label>
                                        <asp:HiddenField ID="hdfEdit" runat="server" />
                                        <asp:HiddenField ID="hdfView" runat="server" />
                                        <asp:HiddenField ID="hdfDelete" runat="server" />
                                    </div>



                                    <div class="panel-body">
                                        <div class="row with-margin w-top">
                                            <div class=" col-sm-1 control-label ">

                                                <label>Item Name</label>
                                                <label style="color: red;">*</label>
                                            </div>

                                            <div class="col-sm-3 ">
                                                <asp:TextBox onkeydown="return (event.keyCode!=13);" ID="txtItemName" MaxLength="250" runat="server" autocomplete="off"
                                                    CssClass="form-control input-sm" placeholder="Item Name" ></asp:TextBox>

                                            </div>
                                             <div class=" col-sm-1 control-label ">

                                                <label>Item Description</label>
                                                <label style="color: red;">*</label>
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:TextBox onkeydown="return (event.keyCode!=13);" ID="txtItemDescription" MaxLength="500" runat="server" autocomplete="off"  
                                                    CssClass="form-control input-sm" placeholder="Item Description" ></asp:TextBox>

                                            </div>
                                            
                                           
                                        </div>
                                         <div class="row with-margin w-top">
                                            
                                               <div class="col-sm-1 control-label">

                                                <label>Price</label>
                                                <label style="color: red;">*</label>
                                            </div>

                                            <div class="col-sm-1">

                                                <asp:TextBox onkeydown="return (event.keyCode!=13);" ID="txtPrice" runat="server" MaxLength="10" autocomplete="off" onkeypress="return validateFloatKeyPress(this,event);" AutoPostBack="true"
                                                    CssClass="form-control input-sm" placeholder="Price"></asp:TextBox>

                                            </div>
                                             <div class=" col-sm-3 control-label ">
                                                <asp:Image ID="Image1" ImageUrl="../../assets/images/avatars/male.png" runat="server" Width="96px" Height="92px" />
                                            </div>
                                              <div class="col-sm-1" style="margin-left:-230px;" >
                                                <asp:FileUpload ID="FileUpload10" runat="server" />
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:Label ID="lblImage" ForeColor="Green" runat="server" />
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:Button ID="Button1" Text="Upload" runat="server" CssClass="btn btn-primary" OnClick="UploadFile10" />
                                                <asp:TextBox ID="txtImageFile" runat="server" CssClass="hidden"></asp:TextBox>
                                            </div>
                                             </div>

                                        <div class="row with-margin w-top">



                                          

                                        </div>
                                        <div class="row with-margin w-top">
                                           

                                        </div>

                                        <div class="row with-margin w-top">
                                            <div class="col-sm-1"></div>
                                            <div class="col-sm-2">
                                                <div class=" text-left ">

                                                    <asp:TextBox autocomplete="off" ID="txtROWNO" CssClass="HideTxt" runat="server" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                    <asp:Button ID="btnOK" runat="server" CssClass="btn btn-primary" OnClientClick="return InputValidate(this);" Text="Save" Visible="false" />
                                                    <asp:Button ID="Button2" runat="server" CssClass="hidden" Text="SAVE" OnClick="btnOK_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary" OnClientClick="return InputValidateCancel(this);" Text="Cancel" OnClick="btnCancel_Click" Visible="False" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>


                               
                                <h1 style="font-size:larger; font-weight:bold">List of Items as shown below</h1>
                                <div class="GridContainer" style="margin-left: 0px;">

                                    <asp:GridView ID="GridView1" OnRowCreated="GridView1_RowCreated" CssClass="myGrid txtStyle" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                        DataKeyNames="ID" ForeColor="#333333" GridLines="None" ShowFooter="True" Width="70%" PageSize="100"
                                        AllowPaging="True" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand"
                                        AllowSorting="true" OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowEditing="GridView1_RowEditing">
                                        <PagerSettings PageButtonCount="25" />

                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                                SortExpression="ID" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Sl No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" CssClass="centerText" Width="50px" />
                                                <ItemStyle CssClass="centerText" Width="50px" />
                                            </asp:TemplateField>

                                           
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" ItemStyle-Width="250" SortExpression="ItemName">
                                                <HeaderStyle HorizontalAlign="Center" />

                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" ItemStyle-Width="350" SortExpression="ItemDescription">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>

                                        


                                            <asp:TemplateField ShowHeader="False" ItemStyle-Width="60">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                        Text="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField ShowHeader="False" ItemStyle-Width="60">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="MyDelete"
                                                        ForeColor="Red" Text="Delete"></asp:LinkButton>
                                                    <cc1:ModalPopupExtender BackgroundCssClass="ModalPopupBG" ID="lnkDelete_ModalPopupExtender"
                                                        runat="server" TargetControlID="lnkDelete" PopupControlID="DivDeleteConfirmation"
                                                        OkControlID="ButtonDeleleOkay" CancelControlID="ButtonDeleteCancel">
                                                    </cc1:ModalPopupExtender>
                                                    <cc1:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server" Enabled="True"
                                                        TargetControlID="lnkDelete" DisplayModalPopupID="lnkDelete_ModalPopupExtender"></cc1:ConfirmButtonExtender>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" ForeColor="Red" />
                                            </asp:TemplateField>
                                              <asp:TemplateField ShowHeader="False" ItemStyle-Width="90">
                                            <ItemTemplate>
                                                <a href="ItemView.aspx?RowId=<%# Eval("id") %>" target="_blank" style="height: 24px;">View Item</a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="true" BackColor="YellowGreen" />
                                        </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>No Records Found</EmptyDataTemplate>
                                        <EmptyDataRowStyle Font-Bold="true" HorizontalAlign="Left" />
                                        <FooterStyle BackColor="#6495ED" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <PagerStyle BackColor="#ccdbf3" ForeColor="Black" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="Black" />
                                        <HeaderStyle Font-Bold="False" ForeColor="Black" />
                                        <EditRowStyle BackColor="#CBDBEA" />
                                        <AlternatingRowStyle BackColor="White" Font-Bold="false" ForeColor="Black" />
                                        <RowStyle BackColor="White" ForeColor="Black" Font-Bold="false" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                    </div>

                    <%--============================================--%>

                    <asp:LinkButton ForeColor="#ff5050" Text="" ID="lnkFake1" runat="server" />
                    <cc1:ModalPopupExtender ID="mpemsg1" runat="server" PopupControlID="pnlPopup1" TargetControlID="lnkFake1"
                        BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="header">
                            Message
                        </div>
                        <div class="body">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div runat="server" class="msgpop" id="lblPoupmsg1"></div>
                                </ContentTemplate>

                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnoK" EventName="click" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                        <div class="footer" align="right">
                            <asp:Button ID="btnPopupClose1" runat="server" Text="OK" CssClass="btn btn-danger" />
                        </div>
                    </asp:Panel>

                    <%--============================================--%>
                    <asp:LinkButton ForeColor="#ff5050" Text="" ID="lnkFake" runat="server" />
                    <cc1:ModalPopupExtender ID="mpemsg" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake"
                        BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                        <div class="header">
                            Message
                        </div>
                        <div class="body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div runat="server" style="color: black;" class="msgpop" id="lblPoupmsg"></div>
                                </ContentTemplate>

                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnoK" EventName="click" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                        <div class="footer" align="right">
                            <asp:Button ID="btnPopupClose" OnClick="popupclose" runat="server" Text="Close" CssClass="btn btn-danger" />
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:Panel runat="server" ID="DivDeleteConfirmation" Style="display: none;" class="popupConfirmation">
                <div class="popup_Container" style="height: 156px;">
                    <div class="popup_Titlebar" id="PopupHeader">
                        <div class="TitlebarLeft">
                            Delete Item Inventory Details
                        </div>
                        <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
                        </div>
                    </div>
                    <div class="popup_Body">
                        <p>
                            Are you sure, you want to delete the Item Inventory Details?
                        </p>
                    </div>
                    <div class="popup_Buttons">
                        <input id="ButtonDeleleOkay" value="Okay" type="button" />
                        <input id="ButtonDeleteCancel" value="Cancel" type="button" />
                    </div>
                </div>
            </asp:Panel>

        </div>


    </form>
</body>
</html>
