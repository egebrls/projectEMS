<%@ Page Title="Employees - EMS" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="projectEMS.Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #employees {
            background-color: blue
        }

        .clickable-row {
            cursor: pointer;
        }

        .profilePic {
            width: 200px;
            height: 200px;
        }

        .closeButton {
            height: 20px;
            width: 20px;
            margin-right: 15px;
        }
    </style>
    <script>
        function hideAddMessage() {
            var lblInsertMsg = document.getElementById('<%= lblInsertMsg.ClientID %>');
            lblInsertMsg.style.display = 'none';
        }

        function hideDeleteMessage() {
            var lblDeleted = document.getElementById('<%= lblDeleted.ClientID %>');
            lblDeleted.style.display = 'none';
        }

        function hidePanel() {
            var pnlEmpInfo = document.getElementById('<%= pnlEmpInfo.ClientID %>');
            pnlEmpInfo.style.display = 'none';
        }
    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:UpdatePanel ID="upnlEmployees" runat="server">
            <ContentTemplate>
                <div id="employeeContainer" style="width: 50%; float: left; margin-top: 30px; border-right: solid 3px #0080C0">
                    <h2>EMPLOYEES LIST</h2>
                    <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter">
                        <asp:Label ID="lblFilterName" runat="server" Text="Enter a name to filter the results (optional): "></asp:Label>
                        <asp:TextBox ID="txtFilterName" runat="server"></asp:TextBox>
                        <asp:Label ID="lblSort" runat="server" Text="Sort by " Style="margin-left: 20px"></asp:Label>
                        <asp:DropDownList ID="ddlistFilter" runat="server">
                            <asp:ListItem>ID (asc.)</asp:ListItem>
                            <asp:ListItem>ID (desc.)</asp:ListItem>
                            <asp:ListItem>Name (asc.)</asp:ListItem>
                            <asp:ListItem>Name (desc.)</asp:ListItem>
                            <asp:ListItem>Surname (asc.)</asp:ListItem>
                            <asp:ListItem>Surname (desc.)</asp:ListItem>
                            <asp:ListItem>Department (asc.)</asp:ListItem>
                            <asp:ListItem>Department (desc.)</asp:ListItem>
                            <asp:ListItem>Position (asc.)</asp:ListItem>
                            <asp:ListItem>Position (desc.)</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnFilter" runat="server" Text="SORT & FILTER" Style="margin-left: 10px" OnClick="btnFilter_Click" />
                    </asp:Panel>
                    <br />
                    <br />
                    <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                        BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowPaging="True" PageSize="15"
                        OnPageIndexChanging="gvEmployees_PageIndexChanging" OnSelectedIndexChanged="gvEmployees_SelectedIndexChanged" BorderStyle="None">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                        <Columns>
                            <asp:BoundField DataField="EmployeeID" HeaderText="ID" />
                            <asp:BoundField DataField="FirstName" HeaderText="Name" />
                            <asp:BoundField DataField="LastName" HeaderText="Surname" />
                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                            <asp:BoundField DataField="Position" HeaderText="Position" />
                            <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="clickable-row">
                                <ControlStyle CssClass="clickable-row" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddEmployee" runat="server" Text="ADD EMPLOYEE" Visible="false" OnClick="btnAddEmployee_Click" />
                    <br />
                    <br />
                    <div>
                        <asp:Panel ID="pnlInsertInfo" runat="server" Visible="false" DefaultButton="btnInsertEmployee">
                            <hr style="margin-right: 10px" />
                            <asp:ImageButton ID="imgbtnCloseAdd" runat="server" ImageUrl="Images1/close.png" class="closeButton" ImageAlign="right" OnClick="imgbtnCloseAdd_Click" />
                            <h2>Employee Info</h2>
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name <sup>(*)</sup>: " Width="120px"></asp:Label>
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name <sup>(*)</sup>: " Width="120px"></asp:Label>
                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Label ID="lblPhone" runat="server" Text="Phone <sup>(*)</sup>: " Width="120px"></asp:Label>
                            <asp:TextBox ID="txtPhone" runat="server" TextMode="SingleLine" MaxLength="11"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Label ID="lblEmail" runat="server" Text="E-Mail <sup>(*)</sup>: " Width="120px"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Label ID="lblDepartment" runat="server" Text="Department <sup>(*)</sup>: " Width="120px"></asp:Label>
                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="170px"></asp:DropDownList>
                            <br />
                            <br />
                            <asp:Label ID="lblPosition" runat="server" Text="Position <sup>(*)</sup>: " Width="120px" Style="display: inline-block"></asp:Label>
                            <asp:DropDownList ID="ddlPosition" runat="server" Width="170px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Manager</asp:ListItem>
                                <asp:ListItem>Employee</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Label ID="lblSalary" runat="server" Text="Salary: " Width="120px"></asp:Label>
                            <asp:TextBox ID="txtSalary" runat="server" TextMode="Number"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Label ID="lblHireDate" runat="server" Text="Hire Date <sup>(*)</sup>: " Width="120px"></asp:Label>
                            <asp:TextBox ID="txtHireDate" runat="server" TextMode="Date" Width="170px"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Button ID="btnInsertEmployee" runat="server" Text="INSERT" OnClick="btnInsertEmployee_Click" />
                            <br />
                            <asp:Label ID="lblInsertMsg" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblErrorMsg" runat="server" Text="Please fill all the necessary boxes <sup>(*)</sup>" Visible="false"></asp:Label>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
                <div class="vl"></div>
                <div style="float: right; width: 49%; margin-top: 30px;">
                    <asp:Panel ID="pnlEmpInfo" runat="server" Visible="false" DefaultButton="btnSave">
                        <asp:ImageButton ID="imgbtnCloseInfo" runat="server" ImageUrl="Images1/close.png" class="closeButton" ImageAlign="right" OnClick="imgbtnCloseInfo_Click" />
                        <h2>EMPLOYEE INFO</h2>
                        <asp:Image ID="imgPic" runat="server" CssClass="profilePic" />
                        <br />
                        <asp:Label ID="lblEmpIDHdr" runat="server" Text="Employee ID: " Style="font-weight: bold"></asp:Label>
                        <asp:Label ID="lblEmpID" runat="server"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblEmpFirstNameHdr" runat="server" Text="First Name: " Style="font-weight: bold"></asp:Label>
                        <asp:Label ID="lblEmpFirstName" runat="server"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblEmpLastNameHdr" runat="server" Text="Last Name: " Style="font-weight: bold"></asp:Label>
                        <asp:Label ID="lblEmpLastName" runat="server"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblEmpPhoneHdr" runat="server" Text="Phone: " Style="font-weight: bold"></asp:Label>
                        <asp:Label ID="lblEmpPhone" runat="server"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblEmpEmailHdr" runat="server" Text="E-Mail: " Style="font-weight: bold"></asp:Label>
                        <asp:Label ID="lblEmpEmail" runat="server"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblEmpDepartmentHdr" runat="server" Text="Department: " Style="font-weight: bold"></asp:Label>
                        <asp:Label ID="lblEmpDepartment" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlistEmpDepartmentEdit" runat="server" Visible="false">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Label ID="lblEmpPositionHdr" runat="server" Text="Position: " Style="font-weight: bold"></asp:Label>
                        <asp:Label ID="lblEmpPosition" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlistEmpPositionEdit" runat="server" Visible="false">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Manager</asp:ListItem>
                            <asp:ListItem>Employee</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Label ID="lblEmpPrj" runat="server"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblEmpHireDateHdr" runat="server" Text="Hire Date: " Style="font-weight: bold" Visible="false"></asp:Label>
                        <asp:Label ID="lblEmpHireDate" runat="server" Visible="false"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblEmpSalaryHdr" runat="server" Text="Salary: " Style="font-weight: bold" Visible="false"></asp:Label>
                        <asp:Label ID="lblEmpSalary" runat="server" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtEmpSalaryEdit" runat="server" TextMode="Number" Visible="false"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btnDeleteEmployee" runat="server" Text="DELETE EMPLOYEE" OnClick="btnDeleteEmployee_Click" Visible="false" />
                        <asp:Button ID="btnEditEmployee" runat="server" Text="EDIT EMPLOYEE" OnClick="btnEditEmployee_Click" Visible="false" />
                        <asp:Button ID="btnSave" runat="Server" Text="SAVE CHANGES" Visible="false" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="CANCEL" Visible="false" OnClick="btnCancel_Click" />
                        <asp:Label ID="lblConfirm" runat="server" Text="Do you want to delete this employee?" Visible="false"></asp:Label>
                        <br />
                        <asp:Button ID="btnYes" runat="server" Text="YES" Visible="false" OnClick="btnYes_Click" />
                        <asp:Button ID="btnNo" runat="server" Text="NO" Visible="false" OnClick="btnNo_Click" />
                        <br />
                        <asp:Label ID="lblDeleted" runat="server" Text="Employee deleted." Visible="false"></asp:Label>

                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </center>

</asp:Content>

