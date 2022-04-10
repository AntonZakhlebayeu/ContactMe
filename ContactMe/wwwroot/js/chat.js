let userName;

let connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, theme, text, time, id) {
    alert("You just received a message from a user " + user);
    let messageDiv = document.createElement("div");
    messageDiv.className = "newMessage" + id.toString();
    document.getElementById("newMessages").appendChild(messageDiv);
    let li = document.createElement("li");
    messageDiv.appendChild(li);
    li.innerText = `${user}: send you mail!\nMail theme: ${theme}.\nAt time: ${time}.\nCheck this!`;
    let button = document.createElement("button");
    button.textContent = "Check!";
    button.className = "btn btn-primary";
    button.addEventListener("click", function (event) {
        messageDiv.removeChild(button);
        let messageText = document.createElement("p");
        messageText.innerText =  "Message text:\n\n"+ text;
        messageDiv.appendChild(messageText);
        let button2 = document.createElement('button');
        button2.textContent = "Seen";
        button2.className = "btn btn-primary";
        button2.addEventListener("click", function (event) {
            messageDiv.removeChild(button2);
            messageDiv.removeChild(messageText);
            messageDiv.removeChild(li);
            messageDiv.remove();
        });
        let br = document.createElement("br");
        messageDiv.appendChild(br);
        messageDiv.appendChild(button2);
    });
    let br = document.createElement("br");
    messageDiv.appendChild(br);
    messageDiv.appendChild(button);
});

connection.on("ErrorMessage", function (achiever) {
    alert('User ' + achiever + ' does not exist!');
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




