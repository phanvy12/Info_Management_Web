function showPopupSuccess(formId) {
    var form = document.getElementById(formId);
    const popup = document.getElementById('successPopup');
    popup.style.display = 'flex';

    // Automatically hide popup after 2 seconds
    setTimeout(() => {
        popup.style.display = 'none';
        form.submit();
    }, 2000);
}

function showPopupError() {
    const popup = document.getElementById('errorPopup');
    popup.style.display = 'flex';

    // Automatically hide popup after 2 seconds
    setTimeout(() => {
        popup.style.display = 'none';
    }, 2000);
}

// Close popup when clicking outside
document.getElementById('successPopup').addEventListener('click', function (e) {
    if (e.target === this) {
        this.style.display = 'none';
    }
});
// Close popup when clicking outside
document.getElementById('errorPopup').addEventListener('click', function (e) {
    if (e.target === this) {
        this.style.display = 'none';
    }
});