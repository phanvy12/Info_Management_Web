function showPopup(poupId) {
    document.getElementById(poupId).style.display = 'flex';
}

function hidePopup(poupId) {
    document.getElementById(poupId).style.display = 'none';
}

// Close popup when clicking outside
document.getElementById('createPopupOverlay').addEventListener('click', function (event) {
    if (event.target === this) {
        hidePopup('createPopupOverlay');
    }
});
// Close popup when clicking outside
document.getElementById('editPopupOverlay').addEventListener('click', function (event) {
    if (event.target === this) {
        hidePopup('editPopupOverlay');
    }
});
const select = document.getElementById('mySelect');
const selectedOption = document.getElementById('selectedOption');

select.addEventListener('change', function () {
    selectedOption.textContent = this.value ? `You selected: ${this.options[this.selectedIndex].text}` : '';
});
document.querySelectorAll('.close-button').forEach(button => {
    button.addEventListener('click', () => {
        button.closest('.message').style.display = 'none';
        button.classList.remove('fade-out');
    });
});

function closeMessage(type) {
    const messageElement = document.getElementById(`${type}Message`);
    messageElement.classList.add('fade-out');
    setTimeout(() => {
        messageElement.style.display = 'none';
        messageElement.classList.remove('fade-out');
    }, 3000); // Wait for fade-out animation to complete
}