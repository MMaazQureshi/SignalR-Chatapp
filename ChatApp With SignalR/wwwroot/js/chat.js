"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    debugger;
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});
connection.on("IsTyping", function (user, isTyping) {
    console.log("isTyping user:", user);
    if (isTyping) {
        document.getElementById("isTyping").innerHTML(`${user} is Typing`);
    }
    var encodedMsg = user + "is typing";
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
    //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    //var encodedMsg = user + " says " + msg;
    //var li = document.createElement("li");
    //li.textContent = encodedMsg;
    //document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var receiverId = document.getElementById("receiverId").value;
    //connection.invoke("SendMessage", user, message).catch(function (err) {
    //    return console.error(err.toString());
    //});
    $.ajax(
        $.post("Chat/SendMessage",
            {
                message: message,
                user: user,
                receiverId: receiverId
            }));
    event.preventDefault();
});
document.getElementById('messageInput').addEventListener('focus', function (event) {
    $.ajax(
        $.post("Chat/UserTyping", {
            isTyping: true
        })
    );
});
document.getElementById('messageInput').addEventListener('blur', function (event) {
    $.ajax(
        $.post("Chat/UserTyping", {
            isTyping: false
        })
    );
});