<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="projectEMS.ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .textbox {
            border-style: none;
            height: 5%;
            width: 60%;
            font-size: 14pt;
            font-family: Consolas;
            float: left;
            margin-left: 5%;
            margin-top: 2%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center; height: 375px; width: 100%">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Image ID="imgLogo" runat="server" src="Images1/tpr3.png" Style="float: left" Height="250px" Width="300px" />
        <asp:TextBox ID="txtAddress" runat="server" CssClass="textbox">10049 Sk. No:6 K: 1 Ocakcı Holding AOSB Çiğli/İZMİR</asp:TextBox>
        <asp:TextBox ID="txtPhone" runat="server" CssClass="textbox">0232 971 02 67</asp:TextBox>
        <asp:TextBox ID="txtContact" runat="server" CssClass="textbox" TextMode="MultiLine" style="height:30%">Bilgi Teknolojileri alanında, tüm ihtiyaçlarınızda size yardımcı olmak için buradayız. Gelin, bizi arayın veya bize bir e-posta gönderin. Normal çalışma saatleri içinde mümkün olan en kısa sürede size geri döneceğiz. Çalışma saatlerimiz dışında bizlere WhatsApp üzerinden yazdığınız taktirde, en kısa sürede sizlere dönüş sağlayacağız.</asp:TextBox>
    </div>
    <div>
        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d12496.131868742164!2d27.0440894!3d38.4638002!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14bbd985c570e4bd%3A0x3e2bbeb0eb766516!2sTPR%20Bilgi%20Teknolojileri%20A.%C5%9E.!5e0!3m2!1str!2str!4v1690551487465!5m2!1str!2str" width="400" height="400" style="border: 0; margin-left: 750px" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
    </div>
</asp:Content>
