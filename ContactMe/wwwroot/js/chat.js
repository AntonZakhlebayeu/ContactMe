let userName;

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, theme, text, time, id) {
    var li = document.createElement("li");
    document.getElementById("newMessages").appendChild(li);
    li.textContent = `${user}: send you mail with theme: ${theme}. At time: ${time}. Check this!`;
    var button = document.createElement("button");
    button.textContent = "Check!";
    button.className = "btn btn-primary";
    button.addEventListener("click", function (event) {
        document.getElementById("newMessages").removeChild(button);
        var messageText = document.createElement("p");
        messageText.textContent = text;
        document.getElementById("newMessages").appendChild(messageText);
        let button2 = document.createElement('button');
        button2.textContent = "Seen";
        button2.className = "btn btn-primary";
        button2.addEventListener("click", function (event) {
            var head = document.getElementById("newMessages");
            head.removeChild(button2);
            head.removeChild(messageText);
            head.removeChild(li);
        });
        document.getElementById("newMessages").appendChild(button2);
    });
    document.getElementById("newMessages").appendChild(button);
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


