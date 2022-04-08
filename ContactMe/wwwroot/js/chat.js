let userName;

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("chatroom").appendChild(li);
    li.textContent = `${user}: says ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    userName = document.getElementById("userName").textContent;
    console.log(userName);
    var message = document.getElementById("messageToSend").value;
    connection.invoke("SendMessage", userName, message).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("messageToSend").value = "";
    event.preventDefault();
});