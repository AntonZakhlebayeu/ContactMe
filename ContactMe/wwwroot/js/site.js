function showMessage (id) {
    let message = document.getElementById(id).style;
    message.display = (message.display == 'block') ? 'none' : 'block';
    let button = document.getElementById("button"+id);
    button.textContent = (button.textContent == 'Show less') ? 'Show more' : 'Show less';
}

function autoGrow(element) {
    element.style.height = "5px";
    element.style.height = (element.scrollHeight)+"px";
}