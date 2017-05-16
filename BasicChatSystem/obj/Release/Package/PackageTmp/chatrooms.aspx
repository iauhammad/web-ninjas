<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chatrooms.aspx.cs" Inherits="BasicChatSystem.chatrooms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chatrooms | Web Ninjas</title>
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
	<link rel="stylesheet" type="text/css" href="scripts/styles.css" />
</head>
<body>
    <form id="form1" runat="server" class="container">
        <!-- Page Header -->
        <header class="jumbotron">
            <h1>Welcome to Web Ninjas</h1>
        </header>

        <!-- Container to show user connected -->
        <div class="connected-user">
            You are connected as <span class="connected-user__name"><em><asp:Label runat="server" ID="lblUsername" /></em></span>
            <asp:LinkButton runat="server" ID="LogoutLink" OnClick="LogoutLink_Click" Text="Logout" ToolTip="Please note that this link will redirect to Facebook to logout." />
        </div>

        <!-- Main Content: List of available chatrooms -->
        <main>
            <h2 class="header-h2">List of open chatrooms</h2>
            <asp:XmlDataSource runat="server" ID="srcChatrooms" DataFile="App_Data/chatrooms.xml" XPath="/chatRooms/chatRoom" />
            <asp:GridView runat="server" ID="grdChatrooms" DataSourceID="srcChatrooms" AutoGenerateColumns="false" CssClass="table table-hover">
                <%--  Chatroom Name     |     Administrator     |     Created On     |     Join Chat Room  --%>
                <Columns>
                    <asp:TemplateField HeaderText="Chatroom Name">
                        <ItemTemplate><%#XPath("chatRoomName/text()") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Administrator">
                        <ItemTemplate><%#XPath("administrators/administrator/displayName/text()") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Created On">
                        <ItemTemplate><%#XPath("createdAt/text()") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="hypGoToChat"
                                           NavigateUrl='<%#string.Concat("ninjaroom.aspx?roomid=", XPath("@roomId")) %>'
                                           Text="Join Chat Room"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </main>

        <!-- Page Footer -->
        <footer class="footer">
            Copyright &copy; 2017 | HTTP 5203 - Assignment 03 - Irfaan Auhammad
        </footer>
    </form>
</body>
</html>
