<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JunkieSanta._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
            <script type="text/javascript">
                function doPostBack(o) {
                    __doPostBack(o.id, '');
                } 

            </script>
        <h1>Secret Santa</h1>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Hi there! This is BFF Secret Santa home page. To be able to play, please, enter your russian name in the full regular form."></asp:Label>
        </p>          
        <p><asp:TextBox ID="TextBox1" runat="server"  EnableViewState="True" OnTextChanged="TextBox1_TextChanged" AutoPostBack="True" onkeyup="doPostBack(this);" ></asp:TextBox>
           <asp:RequiredFieldValidator ID="Value1RequiredValidator" ControlToValidate="TextBox1"
                ErrorMessage="Ho-ho-ho. It looks like you forgot something. Please, be careful... <br />" Display="Dynamic"
                 runat="server"/>
        </p>
        <p><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Content/santa1.jpg" OnClick="ImageButton1_Click" OnInit="ImageButton1_Init" /></p>
       <asp:UpdatePanel runat="server" ID="Update1" UpdateMode="Conditional">
        <ContentTemplate>
        </ContentTemplate>
        <Triggers><asp:AsyncPostBackTrigger ControlID="TextBox1" EventName="TextChanged" /></Triggers>
       </asp:UpdatePanel>
        <p><asp:Button ID="Button1" runat="server" Text="Play the game" Width="269px" OnClick="Button1_Click" /></p>
        <h2><asp:Label ID="Label2" runat="server" Visible="False"></asp:Label></h2>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p><a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a></p>
        </div>
    </div>
</asp:Content>
