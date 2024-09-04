<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="projectEMS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>TPR Employee Management System</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 100%; background-color: #0080C0; padding: 1% 0; border: 0; margin-top: 10%">
            <center>
                <h1 style="color: #c1bd1d">LOGIN</h1>
                <img src="Images1/tpr3.png" style="width: 50%; max-width: 300px; display: block; margin: 0 auto;" />
                <br />
                <br />
                <asp:Label ID="lblEmail" runat="server" Text="E-Mail: " style="width:4%; padding-right:10px"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" Width="150px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" Text="Please enter your e-mail"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" Text="Enter your e-mail in the correct format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                <br />
                <br />
                <asp:Label ID="lblPassword" runat="server" Text="Password: " style="width:4%; padding-right:10px"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="8" Width="150px"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" Text="Please enter your password" Display="Dynamic"></asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                <br />
                <br />
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </center>
        </div>
    </form>
</body>
</html>
