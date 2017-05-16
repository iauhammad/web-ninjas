
// When DOM fully loads
window.onload = loaded;
function loaded() {
    // Always scroll to the last/recent chat message when the page loads
    var elem = document.getElementById('messagesContainer');
    elem.scrollTop = elem.scrollHeight;


    // -- Variable to keep track of mouse position to prevent auto scroll
    var fMouseOnContainer = false;
    $("#messagesContainer")
        .mouseover(function () {
            fMouseOnContainer = true;
        })
        .mouseout(function () {
            fMouseOnContainer = false;
        });


    // -- AJAX call to add new submitted message to the XML file and return the DIV element
    $('#btnSendMessage').click(function () {
        var sMessage = $('#txtSendMessage').val();

        $.ajax({
            type: "POST",
            url: "ninjaroom.aspx/SaveMessage",
            data: '{sMessage: "' + sMessage + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var manipulated = JSON.stringify(response);
                var obj = JSON.parse(manipulated);

                //$('#messagesContainer').append(obj.d);
                $('#messagesContainer').html(obj.d);
                elem.scrollTop = elem.scrollHeight;
                $('#txtSendMessage').val('');
                $('#txtSendMessage').focus();
            },
            failure: function (response) {
                alert('Message was not sent due to some error.');
            }
        });

    });


    // -- Reload chat messages after a 2 seconds interval
    var tReloadInterval = setInterval(function () {
        $.ajax({
            type: "POST",
            url: "ninjaroom.aspx/ReloadMessage",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var manipulated = JSON.stringify(response);
                var obj = JSON.parse(manipulated);

                $('#messagesContainer').html(obj.d);
                if (fMouseOnContainer === false) {
                    elem.scrollTop = elem.scrollHeight;
                }
            }
        });
    }, 2000);


    // -- Calls function to log user details in the console
    $('.currentUserDetails').click(function () {
        $.ajax({
            type: "POST",
            url: "ninjaroom.aspx/GetSessionDetails",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var parsResponse = JSON.stringify(response);
                var obj = JSON.parse(parsResponse);
                console.log("Session Details-> " + obj.d);
            }
        });
    });

}