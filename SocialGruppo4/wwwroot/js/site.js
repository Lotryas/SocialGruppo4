function closeFlashMessage(event) {
    event.target.closest(".flash-message").style.display = "none";
}

function fillLike(event, isLiked) {
    let likeSvg = event.target.querySelector("svg");
    if (isLiked == "False") {
        likeSvg.setAttribute("fill", "currentColor");
    } else {
        likeSvg.setAttribute("fill", "none");
    }
}

function unFillLike(event, isLiked) {
    let likeSvg = event.target.querySelector("svg");
    if (isLiked == "True") {
        likeSvg.setAttribute("fill", "currentColor");
    } else {
        likeSvg.setAttribute("fill", "none");
    }
}

function submitNuovoPost(event) {
    if (event.target.querySelector("textarea").value < 1) {
        event.preventDefault();
    }
}