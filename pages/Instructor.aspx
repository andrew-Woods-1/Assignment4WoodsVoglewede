<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Instructor.aspx.cs" Inherits="Assignment4WoodsVoglewede.pages.Instructor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
&nbsp;<asp:Label ID="Label2" runat="server"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </p>
    <p>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/pages/Logon.aspx">Log Out</asp:HyperLink>
    </p>
</asp:Content>
