<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="BasicChatSystem._default" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Join | Web Ninjas</title>
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
	<link rel="stylesheet" type="text/css" href="scripts/styles.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <!-- Page Header -->
        <header class="jumbotron">
            <h1>Join Web Ninjas</h1>
        </header>
        
        <!-- Main Content: User login -->
        <main class="facebook-signin">
            <div>
                <asp:Label runat="server" ID="lblSignIn" Text="Please sign in with your Facebook account to join Web Ninjas Chatrooms." />
            </div>
            <div>
                <asp:LinkButton runat="server" ID="btnSignIn" OnClick="btnJoin_Click" CssClass="btn btn-primary btn-fb"><i class="fa fa-facebook left"></i> Join Web Ninjas</asp:LinkButton>
            </div>
        </main>
       
         <!-- Page Footer -->
        <footer class="footer">
            Copyright &copy; 2017 | HTTP 5203 - Assignment 03 - Irfaan Auhammad
        </footer>
    </div>
    </form>
</body>
</html>