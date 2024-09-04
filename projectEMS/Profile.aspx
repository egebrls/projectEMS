<%@ Page Title="Profile - EMS" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="projectEMS.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #profile {
            background-color: blue
        }

        .profilePic {
            width: 200px;
            height: 200px;
        }

        .gv td {
            padding-left: 10px;
            padding-right: 10px;
            border: 1px solid black;
        }

        .gv th {
            padding-left: 10px;
            padding-right: 10px;
            border: 1px solid black;
        }

        #infoContainer {
            width: 50%;
            float: left;
            margin-top: 30px;
            border-right: solid 3px #0080C0;
        }

    </style>
    <script>
        function hideMessage() {
            var lblSaveMsg = document.getElementById('<%= lblSaveMsg.ClientID %>')
            lblSaveMsg.style.display = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:UpdatePanel ID="upnlProfile" runat="server">
            <ContentTemplate>
                <div id="infoContainer">
                    <h2>PROFILE</h2>
                    <br />
                    <asp:Image ID="imgProfile" runat="server" CssClass="profilePic" />
                    <br />
                    <br />
                    <asp:Label ID="lblEmployeeID" runat="server" Style="font-weight: normal"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblFirstName" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblLastName" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblPosition" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblSalary" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblHireDate" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnUpdateInfo" runat="server" Text="UPDATE PERSONAL INFORMATION" OnClick="btnUpdateInfo_Click" />
                    <asp:Button ID="btnChangePass" runat="server" Text="CHANGE PASSWORD" OnClick="btnChangePass_Click" />
                </div>
                <div class="vl"></div>
                <div id="projectsContainer" style="float: right; width: 49%; height: 50%; margin-top: 30px">
                    <h2>PROJECTS AND ASSIGNMENTS</h2>
                    <br />
                    <br />
                    <asp:GridView ID="gvEmpProjects" runat="server" CssClass="gv" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" />
                            <asp:BoundField DataField="AssignmentName" HeaderText="Assignment Name" />
                            <asp:BoundField DataField="AsgStartDate" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="AsgEndDate" HeaderText="End Date" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="AsgStatus" HeaderText="Status" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <br />
                    <hr />
                </div>
                <div id="editContainer" style="float: right; width: 49%; height: 50%;">
                    <asp:Panel ID="pnlEditInfo" runat="server" Visible="false" DefaultButton="btnSave">
                        <h2>EDIT PERSONAL INFORMATION</h2>
                        <br />
                        <asp:Label ID="lblEditFirstName" runat="server" Text="New First Name: " Width="170px"></asp:Label>
                        <asp:TextBox ID="txtEditFirstName" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblEditLastName" runat="server" Text="New Last Name: " Width="170px"></asp:Label>
                        <asp:TextBox ID="txtEditLastName" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblEditPhone" runat="server" Text="New Phone Number: " Width="170px"></asp:Label>
                        <asp:TextBox ID="txtEditPhone" runat="server" MaxLength="11"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblEditEmail" runat="server" Text="New E-Mail Address: " Width="170px" TextMode="Email"></asp:Label>
                        <asp:TextBox ID="txtEditEmail" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblPassword" runat="server" Text="Enter password to save: " Width="170px"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="8"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btnSave" runat="server" Text="SAVE CHANGES" OnClick="btnSave_Click" />
                        <br />
                        <br />
                        <asp:Label ID="lblSaveMsg" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
