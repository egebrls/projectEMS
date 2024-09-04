<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="projectEMS.ChangePass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <script>
        function visibleAnchor() {
            var anchor = document.getElementById("redirect");
            anchor.style.display = 'inline';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scrptChangePass" runat="server"></asp:ScriptManager>
        <div style="height: 650px; background-color: #0080C0">

            <center style="margin-top: 5%">
                <asp:UpdatePanel ID="upnlChangePass" runat="server">
                    <ContentTemplate>
                        <br />
                        <img src="Images1/tpr3.png" />
                        <br />
                        <br />
                        <asp:Label ID="lblWarning" runat="server" Style="font-weight: bold; font-size: 14pt">Create a new password</asp:Label>
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblOldPassword" runat="server" Text="Old Password: " Width="150px"></asp:Label>
                        <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" MaxLength="8"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword" ValidationGroup="ChangePassGroup" Display="Dynamic" Text="Please enter your old password"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <asp:Label ID="lblNewPassword" runat="server" Text="New Password: " Width="150px"></asp:Label>
                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" MaxLength="8"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword" ValidationGroup="ChangePassGroup" Text="Please enter your new password" Display="Dynamic"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm New Password: " Width="150px"></asp:Label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="8"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ValidationGroup="ChangePassGroup" Text="Please confirm your new password" Display="Dynamic"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <asp:Button ID="btnChangePass" runat="server" Text="Change Password" OnClick="btnChangePass_Click" ValidationGroup="ChangePassGroup" />
                        <asp:Button ID="btnLogout" runat="server" Text="Log out" OnClick="btnLogout_Click" />
                        <br />
                        <br />
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        <br />
                        <br />
                        <a id="redirect" href="Profile.aspx" style="display: none; color: white">Proceed to EMS</a>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </center>


        </div>

    </form>
</body>
</html>
