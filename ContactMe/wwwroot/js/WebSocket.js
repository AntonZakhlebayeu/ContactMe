const uri = "wss://" + document.location.host + document.location.pathname;
console.log(uri);

function connect() {
    socket = new WebSocket(uri);

    socket.onopen = function (e) {
        console.log("Successfully connected!");
    };

    socket.onclose = function (e) {
        console.log("Disconnected!");
    }

    socket.onmessage = function (e) {
        appendItem(list, e.data);
        console.log(e.data);
    }
}

connect();

let list = document.getElementById("messages");
let button = document.getElementById("sendButton");

console.log(button);

if(button) {
    button.addEventListener("click", function () {
        let sendMessage = function (element) {
            console.log("sending!");
            socket.send(element);
        };
        let message = document.getElementById("messageToSend");
        sendMessage(message);
    });
}
function appendItem(list, message) {
    var item = document.createElement("li");
    item.appendChild(document.createTextNode(message));
    list.appendChild(item);
}

