<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatroomNotFound.aspx.cs" Inherits="BasicChatSystem.ChatroomNotFound" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NOT FOUND</title>
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
	<link rel="stylesheet" type="text/css" href="scripts/styles.css" />
</head>
<body>
    <form id="form1" runat="server" class="container">
    <div>
        <!-- Page Header -->
        <header class="jumbotron">
            <h1>Error: Chatroom Not Found</h1>
        </header>

        <!-- Page Content -->
        <main>
            <asp:Button runat="server" ID="btnBackToList" CssClass="btn btn-primary" Text="Back to List" ToolTip="Back to list of chatrooms" OnClick="btnBackToList_Click" />
        </main>

        <!-- Page Footer -->
        <footer class="footer">
            Copyright &copy; 2017 | HTTP 5203 - Assignment 03 - Irfaan Auhammad
        </footer>
    </div>
    </form>
</body>
</html>
