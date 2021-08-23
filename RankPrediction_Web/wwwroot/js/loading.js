document.onreadystatechange = function () {
    const loader = document.getElementById("load-icon");
    var state = document.readyState
    if (state == 'interactive') {
        loader.classList.remove("icon-disable");
    } else if (state == 'loading') {
        loader.classList.remove("icon-disable");
    } else {
        loader.classList.add("icon-disable");


        //画面の指定されたタグにロードイベントを追加する
        var inputs = document.getElementsByTagName("a");

        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].hasAttribute("onClickLoad")) {
                inputs[i].addEventListener("click", showLoadingIcon);
            }
        }

    }

    console.log("state:" + state);
}

document.onsubmit = showLoadingIcon

window.onbeforeunload = showLoadingIcon

window.onpagehide = showLoadingIcon

function showLoadingIcon() {
    const loader = document.getElementById("load-icon");
    loader.classList.remove("icon-disable");
}