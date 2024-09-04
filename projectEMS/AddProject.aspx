<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProject.aspx.cs" Inherits="projectEMS.AddProject" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Project</title>

    <style>
        .ustMenu {
            list-style-type: none;
            background-color: #0080C0;
            position: fixed;
            margin: 0px;
            padding: 0px;
            text-align: center;
            width: 100%;
            height: 46px;
            margin: 0 auto;
            top: 0;
            display: flex;
            justify-content: center;
        }

        .logout-button {
            font-weight: bold;
            cursor: pointer;
            padding: 0px 40px;
            margin-left: auto;
            z-index: 9999;
            height: 46px;
            border: none;
            background-color: #0080C0;
            color: white;
            text-decoration: underline;
            font-size: 14pt;
        }

            .logout-button:hover {
                background-color: #c1bd1d;
            }
    </style>
    <script>
        function hideMessage() {
            var lblPrjAdd = document.getElementById('<%= lblPrjAdd.ClientID %>')
            lblPrjAdd.style.display = 'none';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <div class="ustMenu" style="width: 100%; left: 0px; z-index: 1">
                <asp:Image ID="imgLogo" runat="server" ImageAlign="Middle" ImageUrl="Images1/tprlogo1.png" />
            </div>
        </header>
        <div style="margin-top: 50px">
            <center>
                <h2>ADD NEW PROJECT</h2>
            </center>
            <br />
            <asp:Label ID="lblProjectName" runat="server" Text="Project Name <sup>(*)</sup>: "></asp:Label>
            <asp:TextBox ID="txtProjectName" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblProjectStart" runat="server" Text="Project Start Date <sup>(*)</sup>: "></asp:Label>
            <asp:TextBox ID="txtProjectStart" runat="server" TextMode="Date"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblProjectEnd" runat="server" Text="Project End Date <sup>(*)</sup>: "></asp:Label>
            <asp:TextBox ID="txtProjectEnd" runat="server" TextMode="Date"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnAddProject" runat="server" Text="ADD PROJECT" OnClick="btnAddProject_Click" />
            <br />
            <br />
            <asp:Label ID="lblPrjAdd" runat="server" Text="Project added. " Visible="false"></asp:Label>
            <asp:Label ID="lblError" runat="server" Text="Please fill all the blanks  <sup>(*)</sup>" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
