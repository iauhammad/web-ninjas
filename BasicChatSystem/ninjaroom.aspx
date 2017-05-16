<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ninjaroom.aspx.cs" Inherits="BasicChatSystem.ninjaroom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chat | Web Ninjas</title>
    <meta name="description" content="A chatroom interface" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="scripts/jquery-3.1.1.min.js"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
	<link rel="stylesheet" type="text/css" href="scripts/styles.css" />
</head>
<body>
    <form id="form1" runat="server" class="container main-messages">
        <asp:ScriptManager ID="myScriptManager" runat="server" EnablePageMethods="true" />
        <!-- Page Header -->
        <header class="room jumbotron">
            <h1 runat="server" id="chatRoomName">Chat room</h1>
            <div class="room-details">
                Administrator <span runat="server" id="chatRoomAdmin"></span> | 
                Chat created on <span runat="server" id="chatRoomCreatedOn"></span>
            </div>
        </header>
        
        <!-- Container to hold chat messages -->
        <div runat="server" id="messagesContainer" class="chat-messages"></div>
        <!-- Container with form to allow user to submit a new message -->
        <div class="compose-message row">
            <div class="col-sm-11">
                <asp:TextBox runat="server" ID="txtSendMessage" TextMode="MultiLine" Rows="3" placeholder="Type a message..." CssClass="form-control" />
            </div>
            <div class="col-sm-1">
                <button type="button" id="btnSendMessage" class="btn btn-primary">Send</button>
            </div>
        </div>

        <!-- Page Footer -->
        <footer class="footer">
            <div class="footer__back-link"><asp:HyperLink runat="server" ID="linkBack" Text="Back to Lobby" NavigateUrl="~/chatrooms.aspx" /></div>
            <div class="footer__text">Copyright &copy; 2017 | HTTP 5203 - Assignment 03 - Irfaan Auhammad</div>
            <div class="footer__text"><span class="currentUserDetails">Click me to log current user details</span></div>
        </footer>
    </form>
    <!-- Link JS file to page -->
    <script type="text/javascript" src="scripts/chatroom.js"></script>
</body>
</html>