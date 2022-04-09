let userName;

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, theme, message, time, currentUser) {
    var li = document.createElement("li");
    document.getElementById("chatroom").appendChild(li);
    li.textContent = `${user}: send you mail with theme: ${theme}. Mail text: ${message}         at time:${time}`;
    connection.invoke("SaveMessage", currentUser, theme, message, user, time).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var theme = document.getElementById("themeOfMessage").value;
    var message = document.getElementById("messageToSend").value;
    let receiver = document.getElementById("receiver").value;
    connection.invoke("SendMessage", theme, message, receiver).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("receiver").value = "";
    document.getElementById("themeOfMessage").value = "";
    document.getElementById("messageToSend").value = "";
    event.preventDefault();
});
