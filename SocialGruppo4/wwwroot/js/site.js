function closeFlashMessage(event) {
    event.target.closest(".flash-message").style.display = "none";
}

function fillLike(event) {
    let likeSvg = event.target.querySelector("svg");
    likeSvg.setAttribute("fill", "currentColor");
}

function unFillLike(event) {
    let likeSvg = event.target.querySelector("svg");
    likeSvg.setAttribute("fill", "none");
}

function submitNuovoPost(event) {
    if (event.target.querySelector("textarea").value < 1) {
        event.preventDefault();
    }
}