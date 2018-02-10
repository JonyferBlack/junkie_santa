<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="JunkieSanta.Result" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="jumbotron">
            <h2><asp:Label ID="Label1" runat="server" Text="text"></asp:Label></h2>
            <p>
                <asp:ImageButton ID="ImageButton1" runat="server" Height="247px" Width="444px" ImageUrl="~/Content/santa11.jpg" />
            </p>
            <p><asp:Label ID="Label3" runat="server" Text="Oh, please, remeber your choice, because you can see this page only once. This is due to technical limitations and for purpose of Art. Merry Christmas and Happy New Year!"></asp:Label></p>
        </div>
</asp:Content>

