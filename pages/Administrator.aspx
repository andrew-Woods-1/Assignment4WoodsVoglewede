<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Administrator.aspx.cs" Inherits="Assignment4WoodsVoglewede.pages.Adminstrator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        td{
            text-align: center;
            vertical-align: middle;

          }

        .auto-style2 {
            width: 401px;
        }
        .auto-style3 {
            height: 23px;
            width: 401px;
        }
        .auto-style6 {
            width: 400px;
        }
        .auto-style7 {
            height: 23px;
            width: 400px;
        }
        .auto-style8 {
            height: 23px;
        }
        .auto-style9 {
            height: 23px;
            width: 525px;
        }
        .auto-style10 {
            width: 525px;
        }
        .auto-style11 {
            height: 23px;
            width: 817px;
        }
        .auto-style12 {
            width: 817px;
        }

    </style>
    <p>
        <br />
    </p>
    <p">
        <strong>Member Information</strong> <asp:GridView ID="memberGridView" runat="server">
        </asp:GridView>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <strong>Instructor Information</strong><asp:GridView ID="instructorGridView" runat="server">
        </asp:GridView>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh" />
    </p>
    <p>
        &nbsp;</p>
    <hr />
    <p>
        <strong>Edit Members</strong></p>
    <p>
        &nbsp;</p>
    <p>
        Insert A New Member:<br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style2">First Name</td>
                <td class="auto-style6">Last Name</td>
                <td class="auto-style6">Date Joined</td>
                <td class="auto-style2">Phone #</td>
                <td class="auto-style6">Email</td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:TextBox ID="tbMemberInsertFN" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style7">
                    <asp:TextBox ID="tbMemberInsertLN" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style7">
                    <asp:TextBox ID="tbMemberInsertDate" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="tbMemberInsertPhone" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style7">
                    <asp:TextBox ID="tbMemberInsertEmail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style6">Username</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style2">Password</td>
                <td class="auto-style6">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style6">
                    <asp:TextBox ID="tbMemberInsertUsername" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style2">
                    <asp:TextBox ID="tbMemberInsertPassword" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style6">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style6">
                    <asp:Button ID="btnMemberInsert" runat="server" Text="Insert" OnClick="btnMemberInsert_Click" />
                </td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
            </tr>
        </table>
    </p>
    <p>
        &nbsp;</p>
    <p>
        Delete A Member:</p>
    <p>
        <table style="width:100%;">
            <tr>
                <td>&nbsp;</td>
                <td>User ID</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="tbMemberDeleteID" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnMemberDelete" runat="server" Text="Delete" OnClick="btnMemberDelete_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </p>
    <p>
        &nbsp;</p>
    <p>
        Member Section Assignment:</p>
    <p>
        <table style="width:100%;">
            <tr>
                <td class="auto-style9">MemberID</td>
                <td class="auto-style11"></td>
                <td class="auto-style8">InstructorID</td>
            </tr>
            <tr>
                <td class="auto-style10">
                    <asp:TextBox ID="tbSectionMemberID" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style12">
                    <asp:RadioButtonList ID="rbKarateType" runat="server" CssClass="td" RepeatDirection="Horizontal" Style="list-style=center" align="center" ViewStateMode="Enabled">
                        <asp:ListItem>Karate Age-Uke</asp:ListItem>
                        <asp:ListItem>Karate Chudan-Uke</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:TextBox ID="tbSectionInstructorID" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style10">&nbsp;</td>
                <td class="auto-style12">
                    <asp:Button ID="btnAssignSection" runat="server" Text="Assign" OnClick="btnAssignSection_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </p>
    <p>
        &nbsp;</p>
    <hr />
    <p>
        <strong>Edit Instructors</strong></p>
    <p>
        &nbsp;</p>
    <p>
        Insert New Instructor:</p>
    <p>
        <table style="width:100%;">
            <tr>
                <td class="auto-style8">First Name</td>
                <td class="auto-style8">Last Name</td>
                <td class="auto-style8">Phone #</td>
                <td class="auto-style8">Username</td>
                <td class="auto-style8">Password</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="tbInstructorInsertFN" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="tbInstructorInsertLN" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="tbInstructorInsertPhone" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="tbInstructorInsertUsername" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="tbInstructorInsertPassword" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnInstructorInsert" runat="server" Text="Insert" OnClick="btnInstructorInsert_Click" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            </table>
    </p>
    <p>
        &nbsp;</p>
    <p>
        Delete Instructor:
    <p>
        <table style="width:100%;">
            <tr>
                <td>&nbsp;</td>
                <td>User ID</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="tbInstructorDeleteID" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnInstructorDelete" runat="server" Text="Delete" OnClick="btnInstructorDelete_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:LinkButton ID="lbLogOut" runat="server" OnClick="lbLogOut_Click">Log Out</asp:LinkButton>
    </p>
</asp:Content>
