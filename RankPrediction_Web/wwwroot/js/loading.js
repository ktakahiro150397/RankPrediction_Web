document.onreadystatechange = function () {
    const loader = document.getElementById("load-icon");
    var state = document.readyState
    if (state == 'interactive') {
        loader.classList.remove("icon-disable");
    } else if (state == 'loading') {
        loader.classList.remove("icon-disable");
    } else {
        loader.classList.add("icon-disable");
    }

    console.log("state:" + state);
}

document.onsubmit = function () {
    const loader = document.getElementById("load-icon");
    loader.classList.remove("icon-disable");
}

window.onbeforeunload = function () {
    const loader = document.getElementById("load-icon");
    loader.classList.remove("icon-disable");
}