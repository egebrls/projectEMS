<%@ Page Title="Projects - EMS" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="projectEMS.projects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #projects {
            background-color: blue
        }

        .closeButton {
            height: 20px;
            width: 20px;
            margin-right: 15px;
        }

        .addButton {
            height: 25px;
            width: 25px;
            margin-right: 15px;
            margin-top: 25px;
        }

        .customBtn {
            border-radius: 10px;
            border: 1.5px solid #000000;
        }

            .customBtn:hover {
                background-color: #f2f2f2;
                box-shadow: inset 0px 2px 4px rgba(0, 0, 0, 0.3);
            }
    </style>
    <script type="text/javascript">
        function openPopup() {

            var popupURL = 'AddProject.aspx';

            var screenWidth = window.screen.width;
            var screenHeight = window.screen.height;
            var popupWidth = 600;
            var popupHeight = 600;
            var leftPosition = (screenWidth - popupWidth) / 2;
            var topPosition = (screenHeight - popupHeight) / 2;

            var popupWindow = window.open(
                popupURL,
                'PopupWindow',
                'width=' + popupWidth + ',height=' + popupHeight + ',top=' + topPosition + ',left=' + leftPosition
            );
            return false;
        }
        function hideAsgAddMessage() {
            var lblAddAsgMsg = document.getElementById('<%= lblAddAsgMsg.ClientID %>');
            lblAddAsgMsg.style.display = 'none';
        }
        function hideAsgErrorMessage() {
            var lblSqlEx = document.getElementById('<%= lblSqlEx.ClientID %>');
            lblSqlEx.style.display = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnlProjects" runat="server">
        <ContentTemplate>
            <div style="width: 50%; float: left; margin-top: 30px; border-right: solid 3px #0080C0">
                <asp:ImageButton ID="imgbtnAddPrj" runat="server" class="addButton" ImageAlign="Right" ImageUrl="Images1/add.png" OnClientClick="return openPopup();" Visible="false" />
                <center>
                    <h2>PROJECTS</h2>
                </center>
                <asp:Repeater ID="repeaterProjects" runat="server">
                    <ItemTemplate>
                        <div>
                            <strong>Project ID:</strong> <%# Eval("ProjectID") %><br />
                            <strong>Project Name:</strong> <%# Eval("ProjectName") %><br />
                            <strong>Start Date:</strong> <%# Eval("PrjStartDate", "{0:dd-MM-yyyy}") %>
                            <asp:Button ID="btnPrjDetails" runat="server" class="customBtn" Text="Show details" Style="float: right; margin-right: 50px" OnClick="btnPrjDetails_Click" CommandArgument='<%# Eval("ProjectID") %>' />
                            <br />
                            <strong>End Date:</strong> <%# Eval("PrjEndDate", "{0:dd-MM-yyyy}") %><br />
                            <strong>Status:</strong> <%# Eval("PrjStatus") %><br />
                            <hr style="margin-right: 10px" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </div>
            <div class="vl"></div>
            <div style="width: 49.5%; float: right; margin-top: 30px; margin-bottom: 120px">
                <asp:Panel ID="pnlPrjDetails" runat="server" Visible="false">
                    <asp:ImageButton ID="imgbtnCloseDetails" runat="server" ImageUrl="Images1/close.png" class="closeButton" ImageAlign="right" OnClick="imgbtnCloseDetails_Click" />
                    <center>
                        <h2>PROJECT ASSIGNMENTS</h2>
                    </center>
                    <asp:Repeater ID="repeaterAsg" runat="server">
                        <ItemTemplate>
                            <div style="margin-left: 10px">
                                <strong>Assignment Name: </strong><%# Eval("AssignmentName") %><br />
                                <strong>Assigned Employee: </strong><%# Eval("FirstName") %> <%# Eval("LastName") %>
                                <br />
                                <strong>Start Date: </strong><%# Eval("AsgStartDate", "{0:dd-MM-yyyy}") %><br />
                                <strong>End Date: </strong><%# Eval("AsgEndDate", "{0:dd-MM-yyyy}") %><br />
                                <strong>Status: </strong><%# Eval("AsgStatus") %><hr />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Button ID="btnOpenAddAsg" runat="server" class="customBtn" Text="ADD NEW ASSIGNMENT" OnClick="btnOpenAddAsg_Click" Visible="false" />

                </asp:Panel>
                <asp:Panel ID="pnlAddAsg" runat="server" Visible="false">
                    <asp:ImageButton ID="imgbtnCloseAsg" runat="server" ImageUrl="Images1/close.png" class="closeButton" ImageAlign="right" OnClick="imgbtnCloseAsg_Click" />
                    <center>
                        <h2>ADD NEW ASSIGNMENT</h2>
                    </center>
                    <asp:Label ID="lblAsgPrjName" runat="server" Style="font-family: Consolas; font-weight: bold; text-decoration: underline; font-size: 20pt"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblNewAsgName" runat="server" Text="Assignment Name <sup>(*)</sup>: " Width="170px"></asp:Label>
                    <asp:TextBox ID="txtNewAsgName" runat="server" Width="170px"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="lblNewAsgEmp" runat="server" Text="Assigned Employee <sup>(*)</sup>: " Width="170px"></asp:Label>
                    <asp:DropDownList ID="ddlNewAsgEmp" runat="server" Width="175px"></asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="lblNewAsgStart" runat="server" Text="Assignment Start Date <sup>(*)</sup>: " Width="170px"></asp:Label>
                    <asp:TextBox ID="txtNewAsgStart" runat="server" TextMode="Date" Width="170px"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="lblNewAsgEnd" runat="server" Text="Assignment End Date <sup>(*)</sup>: " Width="170px"></asp:Label>
                    <asp:TextBox ID="txtNewAsgEnd" runat="server" TextMode="Date" Width="170px"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="btnAddNewAsg" runat="server" Text="ADD ASSIGNMENT" OnClick="btnAddNewAsg_Click" />
                    <br />
                    <br />
                    <asp:Label ID="lblAddAsgMsg" runat="server" Text="Assignment added." Visible="false"></asp:Label>
                    <asp:Label ID="lblErrorMsg" runat="server" Visible="false"></asp:Label>
                    <br />
                    <asp:Label ID="lblSqlEx" runat="server"></asp:Label>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
