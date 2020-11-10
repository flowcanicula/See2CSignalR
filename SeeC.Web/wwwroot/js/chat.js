"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

var masterRoomId = null;
//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

$('#joinGroup').click(function (e) {
    e.preventDefault();
    var roomId = document.getElementById("roomId").value;
    masterRoomId = roomId;
    connection.invoke("JoinRoom", roomId).catch(function (err) {
        return console.error(err.toString());
    });
});

$('#sendMachineInformation').click(function (e) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var machineInformation = user + " " + message;
    connection.invoke("SendMachineInformation", masterRoomId, machineInformation).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("OnReceiveMachineInformation", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var li = document.createElement("li");
    li.textContent = msg;
    document.getElementById("machineMessageList").appendChild(li);
});

